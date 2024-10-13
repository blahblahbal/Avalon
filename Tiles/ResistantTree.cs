using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.GameContent.TilePaintSystemV2;
using static Terraria.WorldGen;

namespace Avalon.Tiles
{
	public class ResistantTree : ModTile
	{
		public static GrowTreeSettings Tree_Resistant = new GrowTreeSettings
		{
			GroundTest = ResistantTreeGroundTest,
			WallTest = DefaultTreeWallTest,
			TreeHeightMax = 12,
			TreeHeightMin = 7,
			TreeTileType = (ushort)ModContent.TileType<ResistantTree>(),
			TreeTopPaddingNeeded = 4,
			SaplingTileType = (ushort)ModContent.TileType<ResistantSapling>()
		};

		public static TreePaintingSettings TreeResistant = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f
		};

		public override void SetStaticDefaults()
		{
			Main.tileAxe[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileID.Sets.IsATreeTrunk[Type] = true;
			TileID.Sets.IsShakeable[Type] = true;
			TileID.Sets.GetsDestroyedForMeteors[Type] = true;
			TileID.Sets.GetsCheckedForLeaves[Type] = true;
			TileID.Sets.PreventsTileRemovalIfOnTopOfIt[Type] = true;
			TileID.Sets.PreventsTileReplaceIfOnTopOfIt[Type] = true;
			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(55, 55, 45), name);
			DustType = ModContent.DustType<ResistantWoodDust>();
			HitSound = SoundID.Tink;
			MineResist = 10f;
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			width = 20;
			height = 20;
		}
		
		public static void CheckTreeWithSettings(int x, int y, CheckTreeSettings settings)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			Tile tile = Main.tile[x, y];
			int type = tile.TileType;
			int frameX = tile.TileFrameX;
			int frameY = tile.TileFrameY;
			if (Main.tile[x + 1, y] != null && Main.tile[x + 1, y].HasTile)
			{
				num2 = Main.tile[x + 1, y].TileType;
			}
			if (Main.tile[x - 1, y] != null && Main.tile[x - 1, y].HasTile)
			{
				num3 = Main.tile[x - 1, y].TileType;
			}
			if (Main.tile[x, y + 1] != null && Main.tile[x, y + 1].HasTile)
			{
				num = Main.tile[x, y + 1].TileType;
			}
			if (Main.tile[x, y - 1] != null && Main.tile[x, y - 1].HasTile)
			{
				num4 = Main.tile[x, y - 1].TileType;
			}
			bool flag = settings.IsGroundValid(num4);
			bool flag2 = num3 == type;
			bool flag3 = num2 == type;
			if (!flag && num4 != type && ((tile.TileFrameX == 0 && tile.TileFrameY <= 130) || (tile.TileFrameX == 22 && tile.TileFrameY <= 130) || (tile.TileFrameX == 44 && tile.TileFrameY <= 130)))
			{
				WorldGen.KillTile(x, y);
			}
			if (tile.TileFrameX >= 22 && tile.TileFrameX <= 44 && tile.TileFrameY >= 132 && tile.TileFrameY <= 176)
			{
				if (!flag)
				{
					WorldGen.KillTile(x, y);
				}
				else if (!(tile.TileFrameX == 22 && flag3) && !(tile.TileFrameX == 44 && flag2))
				{
					WorldGen.KillTile(x, y);
				}
			}
			else if ((tile.TileFrameX == 88 && tile.TileFrameY >= 0 && tile.TileFrameY <= 44) || (tile.TileFrameX == 66 && tile.TileFrameY >= 66 && tile.TileFrameY <= 130) || (tile.TileFrameX == 110 && tile.TileFrameY >= 66 && tile.TileFrameY <= 110) || (tile.TileFrameX == 132 && tile.TileFrameY >= 0 && tile.TileFrameY <= 176))
			{
				if (flag3 && flag2)
				{
					if (tile.TileFrameNumber == 0)
					{
						tile.TileFrameX = 110;
						tile.TileFrameY = 66;
					}
					if (tile.TileFrameNumber == 1)
					{
						tile.TileFrameX = 110;
						tile.TileFrameY = 88;
					}
					if (tile.TileFrameNumber == 2)
					{
						tile.TileFrameX = 110;
						tile.TileFrameY = 110;
					}
				}
				else if (flag3)
				{
					if (tile.TileFrameNumber == 0)
					{
						tile.TileFrameX = 88;
						tile.TileFrameY = 0;
					}
					if (tile.TileFrameNumber == 1)
					{
						tile.TileFrameX = 88;
						tile.TileFrameY = 22;
					}
					if (tile.TileFrameNumber == 2)
					{
						tile.TileFrameX = 88;
						tile.TileFrameY = 44;
					}
				}
				else if (flag2)
				{
					if (tile.TileFrameNumber == 0)
					{
						tile.TileFrameX = 66;
						tile.TileFrameY = 66;
					}
					if (tile.TileFrameNumber == 1)
					{
						tile.TileFrameX = 66;
						tile.TileFrameY = 88;
					}
					if (tile.TileFrameNumber == 2)
					{
						tile.TileFrameX = 66;
						tile.TileFrameY = 110;
					}
				}
				else
				{
					if (tile.TileFrameNumber == 0)
					{
						tile.TileFrameX = 0;
						tile.TileFrameY = 0;
					}
					if (tile.TileFrameNumber == 1)
					{
						tile.TileFrameX = 0;
						tile.TileFrameY = 22;
					}
					if (tile.TileFrameNumber == 2)
					{
						tile.TileFrameX = 0;
						tile.TileFrameY = 44;
					}
				}
			}
			if (tile.TileFrameY >= 132 && tile.TileFrameY <= 176 && (tile.TileFrameX == 0 || tile.TileFrameX == 66 || tile.TileFrameX == 88))
			{
				if (!flag)
				{
					WorldGen.KillTile(x, y);
				}
				if (!flag3 && !flag2)
				{
					if (tile.TileFrameNumber == 0)
					{
						tile.TileFrameX = 0;
						tile.TileFrameY = 0;
					}
					if (tile.TileFrameNumber == 1)
					{
						tile.TileFrameX = 0;
						tile.TileFrameY = 22;
					}
					if (tile.TileFrameNumber == 2)
					{
						tile.TileFrameX = 0;
						tile.TileFrameY = 44;
					}
				}
				else if (!flag3)
				{
					if (tile.TileFrameNumber == 0)
					{
						tile.TileFrameX = 0;
						tile.TileFrameY = 132;
					}
					if (tile.TileFrameNumber == 1)
					{
						tile.TileFrameX = 0;
						tile.TileFrameY = 154;
					}
					if (tile.TileFrameNumber == 2)
					{
						tile.TileFrameX = 0;
						tile.TileFrameY = 176;
					}
				}
				else if (!flag2)
				{
					if (tile.TileFrameNumber == 0)
					{
						tile.TileFrameX = 66;
						tile.TileFrameY = 132;
					}
					if (tile.TileFrameNumber == 1)
					{
						tile.TileFrameX = 66;
						tile.TileFrameY = 154;
					}
					if (tile.TileFrameNumber == 2)
					{
						tile.TileFrameX = 66;
						tile.TileFrameY = 176;
					}
				}
				else
				{
					if (tile.TileFrameNumber == 0)
					{
						tile.TileFrameX = 88;
						tile.TileFrameY = 132;
					}
					if (tile.TileFrameNumber == 1)
					{
						tile.TileFrameX = 88;
						tile.TileFrameY = 154;
					}
					if (tile.TileFrameNumber == 2)
					{
						tile.TileFrameX = 88;
						tile.TileFrameY = 176;
					}
				}
			}
			if ((tile.TileFrameX == 66 && (tile.TileFrameY == 0 || tile.TileFrameY == 22 || tile.TileFrameY == 44)) || (tile.TileFrameX == 44 && (tile.TileFrameY == 198 || tile.TileFrameY == 220 || tile.TileFrameY == 242)))
			{
				if (!flag2)
				{
					WorldGen.KillTile(x, y);
				}
			}
			else if ((tile.TileFrameX == 88 && (tile.TileFrameY == 66 || tile.TileFrameY == 88 || tile.TileFrameY == 110)) || (tile.TileFrameX == 66 && (tile.TileFrameY == 198 || tile.TileFrameY == 220 || tile.TileFrameY == 242)))
			{
				if (!flag3)
				{
					WorldGen.KillTile(x, y);
				}
			}
			else if (num4 == -1)
			{
				WorldGen.KillTile(x, y);
			}
			else if (num != type && tile.TileFrameY < 198 && ((tile.TileFrameX != 22 && tile.TileFrameX != 44) || tile.TileFrameY < 132))
			{
				if (flag3 || flag2)
				{
					if (num4 == type)
					{
						if (flag3 && flag2)
						{
							if (tile.TileFrameNumber == 0)
							{
								tile.TileFrameX = 132;
								tile.TileFrameY = 132;
							}
							if (tile.TileFrameNumber == 1)
							{
								tile.TileFrameX = 132;
								tile.TileFrameY = 154;
							}
							if (tile.TileFrameNumber == 2)
							{
								tile.TileFrameX = 132;
								tile.TileFrameY = 176;
							}
						}
						else if (flag3)
						{
							if (tile.TileFrameNumber == 0)
							{
								tile.TileFrameX = 132;
								tile.TileFrameY = 0;
							}
							if (tile.TileFrameNumber == 1)
							{
								tile.TileFrameX = 132;
								tile.TileFrameY = 22;
							}
							if (tile.TileFrameNumber == 2)
							{
								tile.TileFrameX = 132;
								tile.TileFrameY = 44;
							}
						}
						else if (flag2)
						{
							if (tile.TileFrameNumber == 0)
							{
								tile.TileFrameX = 132;
								tile.TileFrameY = 66;
							}
							if (tile.TileFrameNumber == 1)
							{
								tile.TileFrameX = 132;
								tile.TileFrameY = 88;
							}
							if (tile.TileFrameNumber == 2)
							{
								tile.TileFrameX = 132;
								tile.TileFrameY = 110;
							}
						}
					}
					else if (flag3 && flag2)
					{
						if (tile.TileFrameNumber == 0)
						{
							tile.TileFrameX = 154;
							tile.TileFrameY = 132;
						}
						if (tile.TileFrameNumber == 1)
						{
							tile.TileFrameX = 154;
							tile.TileFrameY = 154;
						}
						if (tile.TileFrameNumber == 2)
						{
							tile.TileFrameX = 154;
							tile.TileFrameY = 176;
						}
					}
					else if (flag3)
					{
						if (tile.TileFrameNumber == 0)
						{
							tile.TileFrameX = 154;
							tile.TileFrameY = 0;
						}
						if (tile.TileFrameNumber == 1)
						{
							tile.TileFrameX = 154;
							tile.TileFrameY = 22;
						}
						if (tile.TileFrameNumber == 2)
						{
							tile.TileFrameX = 154;
							tile.TileFrameY = 44;
						}
					}
					else if (flag2)
					{
						if (tile.TileFrameNumber == 0)
						{
							tile.TileFrameX = 154;
							tile.TileFrameY = 66;
						}
						if (tile.TileFrameNumber == 1)
						{
							tile.TileFrameX = 154;
							tile.TileFrameY = 88;
						}
						if (tile.TileFrameNumber == 2)
						{
							tile.TileFrameX = 154;
							tile.TileFrameY = 110;
						}
					}
				}
				else
				{
					if (tile.TileFrameNumber == 0)
					{
						tile.TileFrameX = 110;
						tile.TileFrameY = 0;
					}
					if (tile.TileFrameNumber == 1)
					{
						tile.TileFrameX = 110;
						tile.TileFrameY = 22;
					}
					if (tile.TileFrameNumber == 2)
					{
						tile.TileFrameX = 110;
						tile.TileFrameY = 44;
					}
				}
			}
			if (tile.TileFrameX != frameX && tile.TileFrameY != frameY && frameX >= 0 && frameY >= 0)
			{
				WorldGen.TileFrame(x - 1, y);
				WorldGen.TileFrame(x + 1, y);
				WorldGen.TileFrame(x, y - 1);
				WorldGen.TileFrame(x, y + 1);
			}
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
							IsGroundValid = ResistantTreeGroundTest
						});
					}
				}
			}
			return false;
		}

		public static void GetTreeBottom(int i, int j, out int x, out int y)
		{
			x = i;
			y = j;
			Tile tileSafely = Framing.GetTileSafely(x, y);
			int num = tileSafely.TileFrameX / 22;
			int num2 = tileSafely.TileFrameY / 22;
			if (num == 3 && num2 <= 2)
			{
				x--;
			}
			else if (num == 4 && num2 >= 3 && num2 <= 5)
			{
				x++;
			}
			else if (num == 1 && num2 >= 6 && num2 <= 8)
			{
				x++;
			}
			else if (num == 2 && num2 >= 6 && num2 <= 8)
			{
				x--;
			}
			else if (num == 2 && num2 >= 9)
			{
				x--;
			}
			else if (num == 3 && num2 >= 9)
			{
				x++;
			}
			tileSafely = Framing.GetTileSafely(x, y);
			while (y > 50 && (!tileSafely.HasTile || TileID.Sets.IsATreeTrunk[tileSafely.TileType]))
			{
				y++;
				tileSafely = Framing.GetTileSafely(x, y);
			}
		}

		public static void KillTile_GetTreeDrops(int i, int j, Tile tileCache, ref bool bonusWood, ref int dropItem, ref int secondaryItem)
		{
			if (tileCache.TileFrameX >= 22 && tileCache.TileFrameY >= 198)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					if (genRand.NextBool(2))
					{
						int k;
						for (k = j; Main.tile[i, k] != null && (!Main.tile[i, k].HasTile || !Main.tileSolid[Main.tile[i, k].TileType] || Main.tileSolidTop[Main.tile[i, k].TileType]); k++)
						{
						}
						if (Main.tile[i, k] != null)
						{
							dropItem = 9;
							secondaryItem = 27;
						}
					}
					else
					{
						dropItem = 9;
					}
				}
			}
			else
			{
				dropItem = 9;
			}
			if (dropItem != 9)
			{
				return;
			}
			GetTreeBottom(i, j, out var x, out var y);
			if (Main.tile[x, y].HasTile)
			{
				dropItem = ModContent.ItemType<Items.Placeable.Tile.ResistantWood>();
			}
			int num = Player.FindClosest(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16);
			int axe = Main.player[num].inventory[Main.player[num].selectedItem].axe;
			if (genRand.Next(100) < axe || Main.rand.NextBool(3))
			{
				bonusWood = true;
			}
		}

		public override IEnumerable<Item> GetItemDrops(int i, int j)
		{
			int dropItem = ItemID.None;
			int dropItemStack = 1;
			int secondaryItem = ItemID.None;
			Tile tileCache = Main.tile[i, j];
			bool bonusWood = false;
			KillTile_GetTreeDrops(i, j, tileCache, ref bonusWood, ref dropItem, ref secondaryItem);
			if (bonusWood)
			{
				dropItemStack++;
			}
			yield return new Item(dropItem, dropItemStack);
			yield return new Item(secondaryItem);
		}

		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			Tile tile = Main.tile[i, j];
			if (fail)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient && TileID.Sets.IsShakeable[tile.TileType])
				{
					ShakeTree(i, j);
				}
			}
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);
			DrawTrees(i, j, spriteBatch);
			spriteBatch.End();
			spriteBatch.Begin(); //No params as PostDraw doesn't use spritebatch with params
		}

		private static void ShakeTree(int i, int j)
		{
			FieldInfo numTreeShakesReflect = typeof(WorldGen).GetField("numTreeShakes", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
			int numTreeShakes = (int)numTreeShakesReflect.GetValue(null);
			int maxTreeShakes = (int)typeof(WorldGen).GetField("maxTreeShakes", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(null);
			int[] treeShakeX = (int[])typeof(WorldGen).GetField("treeShakeX", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(null);
			int[] treeShakeY = (int[])typeof(WorldGen).GetField("treeShakeY", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(null);
			if (numTreeShakes == maxTreeShakes)
			{
				return;
			}
			GetTreeBottom(i, j, out var x, out var y);
			for (int k = 0; k < numTreeShakes; k++)
			{
				if (treeShakeX[k] == x && treeShakeY[k] == y)
				{
					return;
				}
			}
			treeShakeX[numTreeShakes] = x;
			treeShakeY[numTreeShakes] = y;
			numTreeShakesReflect.SetValue(null, ++numTreeShakes);
			y--;
			while (y > 10 && Main.tile[x, y].HasTile && TileID.Sets.IsShakeable[Main.tile[x, y].TileType])
			{
				y--;
			}
			y++;
			if (!IsTileALeafyTreeTop(x, y) || Collision.SolidTiles(x - 2, x + 2, y - 2, y + 2))
			{
				return;
			}
			if (Main.netMode == NetmodeID.Server)
			{
				//NetMessage.SendData(MessageID.SpecialFX, -1, -1, null, 1, x, y, 1f, ModContent.GoreType<PetrifiedTreeLeaf>());
			}
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				//TreeGrowFX(x, y, 1, ModContent.GoreType<PetrifiedTreeLeaf>(), hitTree: true);
			}
		}

		private static void EmitResistantLeaves(int tilePosX, int tilePosY, int grassPosX, int grassPosY)
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
				//Gore.NewGoreDirect(new EntitySource_Misc(""), vector, Utils.RandomVector2(Main.rand, -num3, num3), ModContent.GoreType<Avalon.Gores.leaf>(), 0.7f + Main.rand.NextFloat() * 0.6f).Frame.CurrentColumn = Main.tile[tilePosX, tilePosY].TileColor;
			}
		}

		public static bool ResistantTreeGroundTest(int tileType)
		{
			if (tileType < 0)
			{
				return false;
			}
			if (tileType == ModContent.TileType<Ectograss>())
			{
				return true;
			}
			return false;
		}

		public static bool GetResistantTreeFoliageData(int i, int j, int xoffset, ref int treeFrame, out int floorY, out int topTextureFrameWidth, out int topTextureFrameHeight)
		{
			int num = i + xoffset;
			topTextureFrameWidth = 80;
			topTextureFrameHeight = 80;
			floorY = j;
			for (int k = 0; k < 100; k++)
			{
				floorY = j + k;
				Tile tile2 = Main.tile[num, floorY];
				if (tile2 == null)
				{
					return false;
				}
			}
			return true;
		}

		public Texture2D GetTreeTopTexture(int tileType, int treeTextureStyle, byte tileColor)
		{
			Texture2D texture2D = TryGetTreeTopAndRequestIfNotReady(tileType, treeTextureStyle, tileColor);
			if (texture2D == null)
			{
				texture2D = (Texture2D)ModContent.Request<Texture2D>(Texture + "_Tops");
			}
			return texture2D;
		}

		public Texture2D GetTreeBranchTexture(int tileType, int treeTextureStyle, byte tileColor)
		{
			Texture2D texture2D = TryGetTreeBranchAndRequestIfNotReady(tileType, treeTextureStyle, tileColor);
			if (texture2D == null)
			{
				texture2D = (Texture2D)ModContent.Request<Texture2D>(Texture + "_Branches");
			}
			return texture2D;
		}

		//Paint Content
		public class TreeTopRenderTargetHolder : ARenderTargetHolder
		{
			public TreeFoliageVariantKey Key;

			public override void Prepare()
			{
				Asset<Texture2D> asset;
				asset = ModContent.Request<Texture2D>("Avalon/Tiles/ResistantTree_Tops");
				if (asset == null)
				{
					asset = TextureAssets.TreeTop[0];
				}
				asset.Wait?.Invoke();
				PrepareTextureIfNecessary(asset.Value);
			}

			public override void PrepareShader()
			{
				PrepareShader(Key.PaintColor, TreeResistant);
			}
		}

		public class TreeBranchTargetHolder : ARenderTargetHolder
		{
			public TreeFoliageVariantKey Key;

			public override void Prepare()
			{
				Asset<Texture2D> asset;
				asset = ModContent.Request<Texture2D>("Avalon/Tiles/ResistantTree_Branches");
				if (asset == null)
				{
					asset = TextureAssets.TreeBranch[0];
				}
				asset.Wait?.Invoke();
				PrepareTextureIfNecessary(asset.Value);
			}

			public override void PrepareShader()
			{
				PrepareShader(Key.PaintColor, TreeResistant);
			}
		}

		private Dictionary<TreeFoliageVariantKey, TreeBranchTargetHolder> _treeBranchRenders = new Dictionary<TreeFoliageVariantKey, TreeBranchTargetHolder>();

		private Dictionary<TreeFoliageVariantKey, TreeTopRenderTargetHolder> _treeTopRenders = new Dictionary<TreeFoliageVariantKey, TreeTopRenderTargetHolder>();

		public void RequestTreeTop(ref TreeFoliageVariantKey lookupKey)
		{
			List<ARenderTargetHolder> _requests = (List<ARenderTargetHolder>)typeof(TilePaintSystemV2).GetField("_requests", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilePaintSystem);
			if (!_treeTopRenders.TryGetValue(lookupKey, out var value))
			{
				value = new TreeTopRenderTargetHolder
				{
					Key = lookupKey
				};
				_treeTopRenders.Add(lookupKey, value);
			}
			if (!value.IsReady)
			{
				_requests.Add(value);
			}
		}

		public void RequestTreeBranch(ref TreeFoliageVariantKey lookupKey)
		{
			List<ARenderTargetHolder> _requests = (List<ARenderTargetHolder>)typeof(TilePaintSystemV2).GetField("_requests", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilePaintSystem);
			if (!_treeBranchRenders.TryGetValue(lookupKey, out var value))
			{
				value = new TreeBranchTargetHolder
				{
					Key = lookupKey
				};
				_treeBranchRenders.Add(lookupKey, value);
			}
			if (!value.IsReady)
			{
				_requests.Add(value);
			}
		}

		public Texture2D TryGetTreeTopAndRequestIfNotReady(int treeTopIndex, int treeTopStyle, int paintColor)
		{
			TreeFoliageVariantKey treeFoliageVariantKey = default(TreeFoliageVariantKey);
			treeFoliageVariantKey.TextureIndex = treeTopIndex;
			treeFoliageVariantKey.TextureStyle = treeTopStyle;
			treeFoliageVariantKey.PaintColor = paintColor;
			TreeFoliageVariantKey lookupKey = treeFoliageVariantKey;
			if (_treeTopRenders.TryGetValue(lookupKey, out var value) && value.IsReady)
			{
				return (Texture2D)(object)value.Target;
			}
			RequestTreeTop(ref lookupKey);
			return null;
		}

		public Texture2D TryGetTreeBranchAndRequestIfNotReady(int tiletype, int treeTopStyle, int paintColor)
		{
			TreeFoliageVariantKey treeFoliageVariantKey = default(TreeFoliageVariantKey);
			treeFoliageVariantKey.TextureStyle = treeTopStyle;
			treeFoliageVariantKey.PaintColor = paintColor;
			treeFoliageVariantKey.TextureIndex = tiletype; //We use the tiletype as the index instead of the actual tree index since we can't insert our own index
			TreeFoliageVariantKey lookupKey = treeFoliageVariantKey;
			if (_treeBranchRenders.TryGetValue(lookupKey, out var value) && value.IsReady)
			{
				return (Texture2D)(object)value.Target;
			}
			RequestTreeBranch(ref lookupKey);
			return null;
		}


		private void DrawTrees(int k, int l, SpriteBatch spriteBatch)
		{
			double _treeWindCounter = (double)typeof(TileDrawing).GetField("_treeWindCounter", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance).GetValue(Main.instance.TilesRenderer);
			Vector2 unscaledPosition = Main.screenPosition;
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
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (frameY >= 198 && frameX >= 22)
			{
				int treeFrame = WorldGen.GetTreeFrame(tile);
				switch (frameX)
				{
					case 22:
						{
							int num5 = 0;
							int grassPosX = x + num5;
							if (!GetResistantTreeFoliageData(x, y, num5, ref treeFrame, out int floorY3, out int topTextureFrameWidth3, out int topTextureFrameHeight3))
							{
								return;
							}
							EmitResistantLeaves(x, y, grassPosX, floorY3);
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
							if (tile.IsTileFullbright)
							{
								color6 = Color.White;
							}
							spriteBatch.Draw(treeTopTexture, vector, (Rectangle?)new Rectangle(treeFrame * (topTextureFrameWidth3 + 2), 0, topTextureFrameWidth3, topTextureFrameHeight3), color6, num7 * num15, new Vector2((float)(topTextureFrameWidth3 / 2), (float)topTextureFrameHeight3), 1f, spriteEffects, 0f);
							break;
						}
					case 44:
						{
							int num21 = x;
							int num2 = 1;
							if (!GetResistantTreeFoliageData(x, y, num2, ref treeFrame, out int floorY2, out _, out _))
							{
								return;
							}
							EmitResistantLeaves(x, y, num21 + num2, floorY2);
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
							if (tile.IsTileFullbright)
							{
								color4 = Color.White;
							}
							spriteBatch.Draw(treeBranchTexture2, position2, (Rectangle?)new Rectangle(0, treeFrame * 42, 40, 40), color4, num4 * num16, new Vector2(40f, 24f), 1f, spriteEffects, 0f);
							break;
						}
					case 66:
						{
							int num17 = x;
							int num18 = -1;
							if (!GetResistantTreeFoliageData(x, y, num18, ref treeFrame, out int floorY, out _, out _))
							{
								return;
							}
							EmitResistantLeaves(x, y, num17 + num18, floorY);
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
							if (tile.IsTileFullbright)
							{
								color2 = Color.White;
							}
							spriteBatch.Draw(treeBranchTexture, position, (Rectangle?)new Rectangle(42, treeFrame * 42, 40, 40), color2, num20 * num16, new Vector2(0f, 30f), 1f, spriteEffects, 0f);
							break;
						}
				}
			}
		}
	}
}
