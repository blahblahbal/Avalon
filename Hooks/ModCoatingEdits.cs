using Avalon.Common;
using Avalon.Items.Other;
using Avalon.Systems;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	public class ModCoatingEdits : ModHook
	{
		public static bool[][]? IsActuated;

		protected override void Apply()
		{
			On_Main.DrawTileEntities += SetDrawTileEntitiesActuator;
			On_Main.DrawTiles += SetDrawActuator;
			On_Tile.CopyPaintAndCoating += CopyModCoating;
			On_WorldGen.paintCoatTile += paintModCoatingTile;
			On_WorldGen.paintCoatWall += paintModCoatingWall;
			On_Player.PlaceThing_PaintScrapper_TryScrapping += RemoveModCoatings;
			On_TileLightScanner.GetTileMask += LightTileMask;
			On_WorldGen.coatingColor += modCoatingColor;
			On_WorldGen.coatingColors += modCoatingColors;
		}

		private List<Color> modCoatingColors(On_WorldGen.orig_coatingColors orig, Tile tile, bool block)
		{
			List<Color> _coatingColors = (List<Color>)typeof(WorldGen).GetField("_coatingColors", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(null);
			orig.Invoke(tile, block);
			if ((block && tile.Get<AvalonTileData>().IsTileActupainted) || (!block && tile.Get<AvalonTileData>().IsWallActupainted))
			{
				_coatingColors.Add(WorldGen.coatingColor(AvalonCoatingsID.ActuatorCoating));
			}
			return _coatingColors;
		}

		private Color modCoatingColor(On_WorldGen.orig_coatingColor orig, int coating)
		{
			if (coating == AvalonCoatingsID.ActuatorCoating)
			{
				Color newColor;
				if (WorldGen.genRand.NextBool(2))
				{
					newColor = new Color(172, 2, 5);
				}
				else
				{
					newColor = new Color(52, 52, 52);
				}
				return newColor;
			}
			else
				return orig.Invoke(coating);
		}

		private LightMaskMode LightTileMask(On_TileLightScanner.orig_GetTileMask orig, TileLightScanner self, Tile tile)
		{
			if (tile.IsActuated && tile.Get<AvalonTileData>().IsTileActupainted && Main.tileBlockLight[tile.TileType])
			{
				return LightMaskMode.Solid;
			}
			return orig.Invoke(self, tile);
		}

		private void RemoveModCoatings(On_Player.orig_PlaceThing_PaintScrapper_TryScrapping orig, Player self, int x, int y)
		{
			Tile tile = Main.tile[x, y];
			if (tile.Get<AvalonTileData>().IsTileActupainted || tile.Get<AvalonTileData>().IsWallActupainted)
			{
				self.cursorItemIconEnabled = true;
				if (self.ItemTimeIsZero && self.itemAnimation > 0 && self.controlUseItem)
				{
					if (WorldGen.paintTile(x, y, 0, broadCast: true) || WorldGen.paintCoatTile(x, y, 0, broadcast: true))
					{
						self.ApplyItemTime(self.inventory[self.selectedItem], self.tileSpeed);
					}
					else if (WorldGen.paintWall(x, y, 0, broadCast: true) || WorldGen.paintCoatWall(x, y, 0, broadcast: true))
					{
						self.ApplyItemTime(self.inventory[self.selectedItem], self.wallSpeed);
					}
				}
			}
			orig.Invoke(self, x, y);
		}

		private bool paintModCoatingWall(On_WorldGen.orig_paintCoatWall orig, int x, int y, byte paintCoatId, bool broadcast)
		{
			Tile tile = Main.tile[x, y];
			if (tile == null || tile.WallType == 0)
			{
				return false;
			}
			List<Color> oldColors = WorldGen.coatingColors(tile, block: false);
			if (paintCoatId == 0)
			{
				if (!tile.Get<AvalonTileData>().IsWallActupainted)
				{
					return orig.Invoke(x, y, paintCoatId, broadcast);
				}
				tile.Get<AvalonTileData>().IsWallActupainted = false;

				WorldGen.SquareTileFrame(x, y, resetFrame: false);
			}
			else if (paintCoatId == AvalonCoatingsID.ActuatorCoating)
			{
				if (tile.Get<AvalonTileData>().IsWallActupainted)
				{
					return false;
				}
				tile.Get<AvalonTileData>().IsWallActupainted = true;
			}
			if (broadcast)
			{
				NetMessage.SendData(MessageID.PaintWall, -1, -1, null, x, y, (int)paintCoatId, 1f);
			}
			if (paintCoatId == 0 || paintCoatId == AvalonCoatingsID.ActuatorCoating)
			{
				WorldGen.paintCoatEffect(x, y, paintCoatId, oldColors);
			}
			if (paintCoatId == AvalonCoatingsID.ActuatorCoating)
			{
				return true;
			}
			return orig.Invoke(x, y, paintCoatId, broadcast);
		}

		private bool paintModCoatingTile(On_WorldGen.orig_paintCoatTile orig, int x, int y, byte paintCoatId, bool broadcast)
		{
			Tile tile = Main.tile[x, y];
			if (tile == null || !tile.HasTile)
			{
				return false;
			}
			List<Color> oldColors = WorldGen.coatingColors(tile, block: true);
			if (paintCoatId == 0)
			{
				if (!tile.Get<AvalonTileData>().IsTileActupainted)
				{
					return orig.Invoke(x, y, paintCoatId, broadcast);
				}
				tile.Get<AvalonTileData>().IsTileActupainted = false;

				WorldGen.SquareTileFrame(x, y, resetFrame: false);
			}
			else if (paintCoatId == AvalonCoatingsID.ActuatorCoating)
			{
				if (tile.Get<AvalonTileData>().IsTileActupainted)
				{
					return false;
				}
				tile.Get<AvalonTileData>().IsTileActupainted = true;
			}
			if (broadcast)
			{
				NetMessage.SendData(MessageID.PaintTile, -1, -1, null, x, y, (int)paintCoatId, 1f);
			}
			if (paintCoatId == 0 || paintCoatId == AvalonCoatingsID.ActuatorCoating)
			{
				WorldGen.paintCoatEffect(x, y, paintCoatId, oldColors);
			}
			if (paintCoatId == AvalonCoatingsID.ActuatorCoating)
			{
				return true;
			}
			return orig.Invoke(x, y, paintCoatId, broadcast);
		}

		private void CopyModCoating(On_Tile.orig_CopyPaintAndCoating orig, ref Tile self, Tile other)
		{
			orig.Invoke(ref self, other);
			self.Get<AvalonTileData>().IsTileActupainted = other.Get<AvalonTileData>().IsTileActupainted;
		}

		private void SetDrawTileEntitiesActuator(On_Main.orig_DrawTileEntities orig, Main self, bool solidLayer, bool overRenderTargets, bool intoRenderTargets)
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 vector = new((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				vector = Vector2.Zero;
			}
			GetScreenDrawArea(unscaledPosition, vector + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out var firstTileX, out var lastTileX, out var firstTileY, out var lastTileY);
			SetVisualActuation(firstTileX, firstTileY, lastTileX, lastTileY);
			orig.Invoke(self, solidLayer, overRenderTargets, intoRenderTargets);
			ResetActuators(firstTileX, firstTileY, lastTileX, lastTileY);
		}

		private void SetDrawActuator(On_Main.orig_DrawTiles orig, Main self, bool solidLayer, bool forRenderTargets, bool intoRenderTargets, int waterStyleOverride)
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 vector = new((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				vector = Vector2.Zero;
			}
			GetScreenDrawArea(unscaledPosition, vector + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out var firstTileX, out var lastTileX, out var firstTileY, out var lastTileY);
			SetVisualActuation(firstTileX, firstTileY, lastTileX, lastTileY);
			orig.Invoke(self, solidLayer, forRenderTargets, intoRenderTargets, waterStyleOverride);
			ResetActuators(firstTileX, firstTileY, lastTileX, lastTileY);
		}

		private void SetVisualActuation(int firstTileX, int firstTileY, int lastTileX, int lastTileY)
		{
			for (int j = firstTileX - 2; j < lastTileX + 2; j++)
			{
				for (int i = firstTileY; i < lastTileY + 4; i++)
				{
					Tile tile = Main.tile[j, i];
					if (tile != null)
					{
						if (IsActuated == null)
						{
							ResizeActuators(Main.maxTilesX, Main.maxTilesY);
						}
						else
						{
							IsActuated[j][i] = tile.IsActuated;
							if (tile.Get<AvalonTileData>().IsTileActupainted)
							{
								tile.IsActuated = !tile.IsActuated;
							}
						}
					}
				}
			}
		}

		private void ResetActuators(int firstTileX, int firstTileY, int lastTileX, int lastTileY)
		{
			
			for (int j = firstTileX - 2; j < lastTileX + 2; j++)
			{
				for (int i = firstTileY; i < lastTileY + 4; i++)
				{
					Tile tile = Main.tile[j, i];
					if (tile != null)
					{
						tile.IsActuated = IsActuated[j][i];
					}
				}
			}
		}

		private void GetScreenDrawArea(Vector2 screenPosition, Vector2 offSet, out int firstTileX, out int lastTileX, out int firstTileY, out int lastTileY)
		{
			firstTileX = (int)((screenPosition.X - offSet.X) / 16f - 1f);
			lastTileX = (int)((screenPosition.X + (float)Main.screenWidth + offSet.X) / 16f) + 2;
			firstTileY = (int)((screenPosition.Y - offSet.Y) / 16f - 1f);
			lastTileY = (int)((screenPosition.Y + (float)Main.screenHeight + offSet.Y) / 16f) + 5;
			if (firstTileX < 4)
			{
				firstTileX = 4;
			}
			if (lastTileX > Main.maxTilesX - 4)
			{
				lastTileX = Main.maxTilesX - 4;
			}
			if (firstTileY < 4)
			{
				firstTileY = 4;
			}
			if (lastTileY > Main.maxTilesY - 4)
			{
				lastTileY = Main.maxTilesY - 4;
			}
			if (Main.sectionManager.AnyUnfinishedSections)
			{
				TimeLogger.DetailedDrawReset();
				WorldGen.SectionTileFrameWithCheck(firstTileX, firstTileY, lastTileX, lastTileY);
				TimeLogger.DetailedDrawTime(5);
			}
			if (Main.sectionManager.AnyNeedRefresh)
			{
				WorldGen.RefreshSections(firstTileX, firstTileY, lastTileX, lastTileY);
			}
		}

		private static void ResizeActuators(int maxX, int maxY)
		{
			IsActuated = new bool[maxX][];
			for (int i = 0; i < IsActuated.Length; i++)
			{
				IsActuated[i] = new bool[maxY];
				for (int j = 0; j < IsActuated[i].Length; j++)
				{
					IsActuated[i][j] = false;
				}
			}
		}

		public static bool SolidTileNoActuator(int i, int j, bool noDoors = false)
		{
			try
			{
				if (Main.tile[i, j] == null)
				{
					return true;
				}
				if (Main.tile[i, j].HasTile && Main.tileSolid[Main.tile[i, j].TileType] && !Main.tileSolidTop[Main.tile[i, j].TileType] && !Main.tile[i, j].IsHalfBlock && Main.tile[i, j].Slope == 0)
				{
					if (noDoors && Main.tile[i, j].TileType == 10)
					{
						return false;
					}
					return true;
				}
			}
			catch
			{
			}
			return false;
		}
	}
}
