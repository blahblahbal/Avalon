using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Drawing;
using System;
using Terraria.GameContent;
using Terraria.Utilities;
using Terraria.DataStructures;
using System.Reflection;
using Terraria.GameContent.Skies.CreditsRoll;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.GameContent.Tile_Entities;
using static Terraria.WorldGen;
using Terraria.Enums;
using System.Threading;
using ReLogic.Content;
using static Terraria.GameContent.TilePaintSystemV2;
using Terraria.Graphics.Capture;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Avalon.Items.Material.Ores;
using Avalon.Items.Placeable.Tile;
using Avalon.Systems;

namespace Avalon.Tiles.GemTrees
{
    public class ZirconTree : AvalonModTree
    {
		public override string TexturePath => "Avalon/Tiles/GemTrees/ZirconTree";

		public static GrowTreeSettings GemTree_Zircon = new GrowTreeSettings
		{
			GroundTest = GemTreeGroundTest,
			WallTest = GemTreeWallTest,
			TreeHeightMax = 12,
			TreeHeightMin = 7,
			TreeTileType = (ushort)ModContent.TileType<ZirconTree>(),
			TreeTopPaddingNeeded = 4,
			SaplingTileType = (ushort)ModContent.TileType<ZirconSapling>()
		};

		public static TreePaintingSettings GemTreeZircon = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		public override void SetStaticDefaults()
        {
            Main.tileAxe[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.CountsAsGemTree[Type] = true;
            TileID.Sets.IsATreeTrunk[Type] = true;
            TileID.Sets.IsShakeable[Type] = true;
            TileID.Sets.GetsDestroyedForMeteors[Type] = true;
            TileID.Sets.GetsCheckedForLeaves[Type] = true;
            TileID.Sets.PreventsTileRemovalIfOnTopOfIt[Type] = true;
            TileID.Sets.PreventsTileReplaceIfOnTopOfIt[Type] = true;
			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(128, 128, 128), name);
			DustType = DustID.Stone;
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			width = 20;
			height = 20;
		}

		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			Tile tile = Main.tile[i, j];
			if (i > 5 && j > 5 && i < Main.maxTilesX - 5 && j < Main.maxTilesY - 5 && Main.tile[i, j] != null)
			{
				if (tile.HasTile)
				{
					if (Main.tileFrameImportant[Type])
					{
						CheckTreeWithSettings(i, j, new CheckTreeSettings
						{
							IsGroundValid = GemTreeGroundTest
						});
					}
				}
			} 
			return false;
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			int dustType = ((!WorldGen.genRand.NextBool(10)) ? 1 : ModContent.DustType<ZirconDust>());
			Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, dustType);
			return false;
		}

		public override IEnumerable<Item> GetItemDrops(int i, int j)
		{
			Tile tileCache = Main.tile[i, j];
			int dropItem = ItemID.None;
			int secondaryItem = ItemID.None;
			int dropItemStack = 1;
			SetGemTreeDrops(ModContent.ItemType<Zircon>(), ModContent.ItemType<ZirconGemcorn>(), tileCache, ref dropItem, ref secondaryItem);
			if (dropItem == 3)
			{
				dropItemStack = Main.rand.Next(1, 3);
			}
			yield return new Item(dropItem, dropItemStack);
			yield return new Item(secondaryItem);
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);
			DrawTrees(i, j, spriteBatch);
			spriteBatch.End();
			spriteBatch.Begin(); //No params as PostDraw doesn't use spritebatch with params
		}

		private static void EmitZirconLeaves(int tilePosX, int tilePosY, int grassPosX, int grassPosY)
		{
			bool _isActiveAndNotPaused = (bool)typeof(TileDrawing).GetField("_isActiveAndNotPaused", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
			int _leafFrequency = (int)typeof(TileDrawing).GetField("_leafFrequency", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
			UnifiedRandom _rand = (UnifiedRandom)typeof(TileDrawing).GetField("_rand", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
			if (!_isActiveAndNotPaused)
			{
				return;
			}
			Tile tile = Main.tile[tilePosX, tilePosY];
			if (tile.LiquidAmount > 0)
			{
				return;
			}
			int num = 0;
			bool flag = (byte)num != 0;
			int num2 = _leafFrequency;
			bool flag2 = tilePosX - grassPosX != 0;
			if (flag)
			{
				num2 /= 2;
			}
			if (!WorldGen.DoesWindBlowAtThisHeight(tilePosY))
			{
				num2 = 10000;
			}
			if (flag2)
			{
				num2 *= 3;
			}
			if (!_rand.NextBool(num2))
			{
				return;
			}
			int num3 = 2;
			Vector2 vector = new((float)(tilePosX * 16 + 8), (float)(tilePosY * 16 + 8));
			if (flag2)
			{
				int num4 = tilePosX - grassPosX;
				vector.X += num4 * 12;
				int num5 = 0;
				if (tile.TileFrameY == 220)
				{
					num5 = 1;
				}
				else if (tile.TileFrameY == 242)
				{
					num5 = 2;
				}
				if (tile.TileFrameX == 66)
				{
					switch (num5)
					{
						case 0:
							vector += new Vector2(0f, -6f);
							break;
						case 1:
							vector += new Vector2(0f, -6f);
							break;
						case 2:
							vector += new Vector2(0f, 8f);
							break;
					}
				}
				else
				{
					switch (num5)
					{
						case 0:
							vector += new Vector2(0f, 4f);
							break;
						case 1:
							vector += new Vector2(2f, -6f);
							break;
						case 2:
							vector += new Vector2(6f, -6f);
							break;
					}
				}
			}
			else
			{
				vector += new Vector2(-16f, -16f);
				if (flag)
				{
					vector.Y -= Main.rand.Next(0, 28) * 4;
				}
			}
			if (!WorldGen.SolidTile(vector.ToTileCoordinates()))
			{
				Gore.NewGoreDirect(new EntitySource_Misc(""), vector, Utils.RandomVector2(Main.rand, -num3, num3), ModContent.GoreType<ZirconGemLeaves>(), 0.7f + Main.rand.NextFloat() * 0.6f).Frame.CurrentColumn = Main.tile[tilePosX, tilePosY].TileColor;
			}
		}

		private void DrawTrees(int k, int l, SpriteBatch spriteBatch)
		{
			
			double _treeWindCounter = (double)typeof(TileDrawing).GetField("_treeWindCounter", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			float num15 = 0.08f;
			float num16 = 0.06f;
			int x = k;
			int y = l;
			Tile tile = Main.tile[x, y];
			if (tile == null || !tile.HasTile)
			{
				return;
			}
			short frameX = tile.TileFrameX;
			short frameY = tile.TileFrameY;
			bool flag = tile.WallType > 0;
			if (frameY >= 198 && frameX >= 22)
			{
				int treeFrame = WorldGen.GetTreeFrame(tile);
				switch (frameX)
				{
					case 22:
						{
							int num5 = 0;
							int grassPosX = x + num5;
							if (!GetGemTreeFoliageData(x, y, num5, ref treeFrame, out int floorY3, out int topTextureFrameWidth3, out int topTextureFrameHeight3))
							{
								return;
							}
							EmitZirconLeaves(x, y, grassPosX, floorY3);
							byte tileColor3 = tile.TileColor;
							Texture2D treeTopTexture = GetTreeTopTexture(Type, 0, tileColor3);
							Vector2 vector = new Vector2((float)(x * 16 - (int)unscaledPosition.X + 8), (float)(y * 16 - (int)unscaledPosition.Y + 16)) + zero;
							float num7 = 0f;
							if (!flag)
							{
								num7 = Main.instance.TilesRenderer.GetWindCycle(x, y, _treeWindCounter);
							}
							vector.X += num7 * 2f;
							vector.Y += Math.Abs(num7) * 2f;
							Color color6 = Lighting.GetColor(x, y);
							color6 = TileGlowDrawing.ActuatedColor(color6, tile);
							if (tile.IsTileFullbright)
							{
								color6 = Color.White;
							}
							spriteBatch.Draw(treeTopTexture, vector, (Rectangle?)new Rectangle(treeFrame * (topTextureFrameWidth3 + 2), 0, topTextureFrameWidth3, topTextureFrameHeight3), color6, num7 * num15, new Vector2((float)(topTextureFrameWidth3 / 2), (float)topTextureFrameHeight3), 1f, (SpriteEffects)0, 0f);
							break;
						}
					case 44:
						{
							int num21 = x;
							int num2 = 1;
							if (!GetGemTreeFoliageData(x, y, num2, ref treeFrame, out int floorY2, out _, out _))
							{
								return;
							}
							EmitZirconLeaves(x, y, num21 + num2, floorY2);
							byte tileColor2 = tile.TileColor;
							Texture2D treeBranchTexture2 = GetTreeBranchTexture(Type, 0, tileColor2);
							Vector2 position2 = new Vector2((float)(x * 16), (float)(y * 16)) - unscaledPosition.Floor() + zero + new Vector2(16f, 12f);
							float num4 = 0f;
							if (!flag)
							{
								num4 = Main.instance.TilesRenderer.GetWindCycle(x, y, _treeWindCounter);
							}
							if (num4 > 0f)
							{
								position2.X += num4;
							}
							position2.X += Math.Abs(num4) * 2f;
							Color color4 = Lighting.GetColor(x, y);
							color4 = TileGlowDrawing.ActuatedColor(color4, tile);
							if (tile.IsTileFullbright)
							{
								color4 = Color.White;
							}
							spriteBatch.Draw(treeBranchTexture2, position2, (Rectangle?)new Rectangle(0, treeFrame * 42, 40, 40), color4, num4 * num16, new Vector2(40f, 24f), 1f, (SpriteEffects)0, 0f);
							break;
						}
					case 66:
						{
							int num17 = x;
							int num18 = -1;
							if (!GetGemTreeFoliageData(x, y, num18, ref treeFrame, out int floorY, out _, out _))
							{
								return;
							}
							EmitZirconLeaves(x, y, num17 + num18, floorY);
							byte tileColor = tile.TileColor;
							Texture2D treeBranchTexture = GetTreeBranchTexture(Type, 0, tileColor);
							Vector2 position = new Vector2((float)(x * 16), (float)(y * 16)) - unscaledPosition.Floor() + zero + new Vector2(0f, 18f);
							float num20 = 0f;
							if (!flag)
							{
								num20 = Main.instance.TilesRenderer.GetWindCycle(x, y, _treeWindCounter);
							}
							if (num20 < 0f)
							{
								position.X += num20;
							}
							position.X -= Math.Abs(num20) * 2f;
							Color color2 = Lighting.GetColor(x, y);
							color2 = TileGlowDrawing.ActuatedColor(color2, tile);
							if (tile.IsTileFullbright)
							{
								color2 = Color.White;
							}
							spriteBatch.Draw(treeBranchTexture, position, (Rectangle?)new Rectangle(42, treeFrame * 42, 40, 40), color2, num20 * num16, new Vector2(0f, 30f), 1f, (SpriteEffects)0, 0f);
							break;
						}
				}
			}
		}
	}
}
