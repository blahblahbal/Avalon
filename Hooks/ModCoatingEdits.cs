using Avalon.Common;
using Avalon.Reflection;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.Graphics.Light;
using Terraria.ID;

namespace Avalon.Hooks
{
	public class ModCoatingEdits : ModHook
	{
		protected override void Apply()
		{
			IL_NetMessage.CompressTileBlock_Inner += SendTIleData; //Please see Avalon.Systems.SyncAvalonWorldData for what tile data is being synced
			IL_NetMessage.DecompressTileBlock_Inner += RecieveTileData;

			On_Tile.CopyPaintAndCoating += CopyModCoating;
			On_WorldGen.paintCoatTile += paintModCoatingTile;
			On_WorldGen.paintCoatWall += paintModCoatingWall;
			On_Player.PlaceThing_PaintScrapper_TryScrapping += RemoveModCoatings;
			On_TileLightScanner.GetTileMask += LightTileMask;
			On_WorldGen.coatingColor += modCoatingColor;
			On_WorldGen.coatingColors += modCoatingColors;

			MassTileRenderingsILHook.TileRenderingMethods += EditActuationOfAllTileDrawing;
			IL_Tile.actColor_Color += actColorModifer;
			IL_Tile.actColor_refVector3 += actColorModifer;
			
			//For some dogshit reason, sliced block and the ref in getfinallight get inlined, this means tiles such as dirt and grass refuse to get actuated
			//here we rejit them so actuation edits still allow the tiles to get tinted by the actucoating
			IL_TileDrawing.DrawSingleTile_SlicedBlock += _ => { };
			IL_TileDrawing.GetFinalLight_Tile_ushort_refVector3_refVector3 += _ => { };
		}

		protected override void UnApply()
		{
			MassTileRenderingsILHook.TileRenderingMethods -= EditActuationOfAllTileDrawing;
		}

		private void actColorModifer(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchCall<Tile>("inActive"));
			c.EmitLdarg(0);
			c.EmitDelegate((bool inActive, ref Tile self) =>
			{
				bool isActuated = inActive;
				if (self.Get<AvalonTileData>().IsTileActupainted)
				{
					isActuated = !isActuated;
				}
				return isActuated;
			});
		}

		private void EditActuationOfAllTileDrawing(MonoMod.Cil.ILContext il)
		{
			ILCursor c = new(il);
			while (c.TryGotoNext(MoveType.After, i => i.MatchCall<Tile>("inActive")))
			{
				c.EmitDelegate((bool inActive) =>
				{
					return true;
				}); 
			}
		}

		#region Paint/Coating stuff
		private void RecieveTileData(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.Before, i => i.MatchLdsfld<Main>(nameof(Main.sectionManager)));
			c.EmitLdarg(0);
			c.EmitLdarg(1);
			c.EmitLdarg(2);
			c.EmitLdarg(3);
			c.EmitLdarg(4);
			c.EmitDelegate((BinaryReader reader, int xStart, int yStart, int width, int height) =>
			{
				for (int i = yStart; i < yStart + height; i++)
				{
					for (int j = xStart; j < xStart + width; j++)
					{
						Tile tile = Main.tile[j, i];
						SyncAvalonWorldData.RecieveTIleData(tile, reader);
					}
				}
			});
		}

		private void SendTIleData(ILContext il)
		{
			ILCursor c = new(il);
			while (c.TryGotoNext(MoveType.Before, i => i.MatchRet())) ;
			c.EmitLdarg(0);
			c.EmitLdarg(1);
			c.EmitLdarg(2);
			c.EmitLdarg(3);
			c.EmitLdarg(4);
			c.EmitDelegate((System.IO.BinaryWriter writer, int xStart, int yStart, int width, int height) =>
			{
				for (int i = yStart; i < yStart + height; i++)
				{
					for (int j = xStart; j < xStart + width; j++)
					{
						Tile tile2 = Main.tile[j, i];
						SyncAvalonWorldData.SendTileData(tile2, writer);
					}
				}
			});
		}

		private List<Color> modCoatingColors(On_WorldGen.orig_coatingColors orig, Tile tile, bool block)
		{
			List<Color> _coatingColors = (List<Color>)typeof(WorldGen).GetField("_coatingColors", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(null);
			orig.Invoke(tile, block);
			if ((block && tile.Get<AvalonTileData>().IsTileActupainted) || (!block && tile.Get<AvalonTileData>().IsWallActupainted))
			{
				_coatingColors.Add(WorldGen.coatingColor(Data.Sets.AvalonCoatingsID.ActuatorCoating));
			}
			return _coatingColors;
		}

		private Color modCoatingColor(On_WorldGen.orig_coatingColor orig, int coating)
		{
			if (coating == Data.Sets.AvalonCoatingsID.ActuatorCoating)
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

		//1.4.5 GlobalTile will have a hook for this
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
			else if (paintCoatId == Data.Sets.AvalonCoatingsID.ActuatorCoating)
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
			if (paintCoatId == 0 || paintCoatId == Data.Sets.AvalonCoatingsID.ActuatorCoating)
			{
				WorldGen.paintCoatEffect(x, y, paintCoatId, oldColors);
			}
			if (paintCoatId == Data.Sets.AvalonCoatingsID.ActuatorCoating)
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
			else if (paintCoatId == Data.Sets.AvalonCoatingsID.ActuatorCoating)
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
			if (paintCoatId == 0 || paintCoatId == Data.Sets.AvalonCoatingsID.ActuatorCoating)
			{
				WorldGen.paintCoatEffect(x, y, paintCoatId, oldColors);
			}
			if (paintCoatId == Data.Sets.AvalonCoatingsID.ActuatorCoating)
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
		#endregion
	}
}
