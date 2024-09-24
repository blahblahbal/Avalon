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
using Avalon.Tiles.GemTrees;

namespace Avalon.Common.Templates
{
    public abstract class AvalonModTree : ModTile
    {
		public virtual string TexturePath => "Avalon/Tiles/GemTrees/TourmalineTree";

		public static void SetGemTreeDrops(int gemType, int seedType, Tile tileCache, ref int dropItem, ref int secondaryItem)
		{
			if (Main.rand.NextBool(10))
			{
				dropItem = gemType;
			}
			else
			{
				dropItem = ItemID.StoneBlock;
			}
			if (tileCache.TileFrameX >= 22 && tileCache.TileFrameY >= 198 && Main.rand.NextBool(2))
			{
				secondaryItem = seedType;
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
							dropItem = ItemID.Wood;
							secondaryItem = 27;
						}
					}
					else
					{
						dropItem = ItemID.Wood;
					}
				}
			}
			else
			{
				dropItem = ItemID.Wood;
			}
			if (dropItem != 9)
			{
				return;
			}
			GetTreeBottom(i, j, out var x, out var y);
			if (Main.tile[x, y].HasTile)
			{
				dropItem = ItemID.Wood;
			}
			int num = Player.FindClosest(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16);
			int axe = Main.player[num].inventory[Main.player[num].selectedItem].axe;
			if (genRand.Next(100) < axe || Main.rand.NextBool(3))
			{
				bonusWood = true;
			}
		}

		public static bool GrowModdedTreeWithSettings(int checkedX, int checkedY, GrowTreeSettings settings)
		{
			int i;

			for (i = checkedY; Main.tile[checkedX, i].TileType == settings.SaplingTileType; i++)
			{
			}

			if (Main.tile[checkedX - 1, i - 1].LiquidAmount != 0 || Main.tile[checkedX, i - 1].LiquidAmount != 0 || Main.tile[checkedX + 1, i - 1].LiquidAmount != 0)
			{
				return false;
			}

			Tile tile = Main.tile[checkedX, i];
			if (!tile.HasUnactuatedTile || tile.IsHalfBlock || tile.Slope != 0)
			{
				return false;
			}
			bool flag = settings.WallTest(Main.tile[checkedX, i - 1].WallType);
			if (!settings.GroundTest(tile.TileType) || !flag)
			{
				return false;
			}
			if ((!Main.tile[checkedX - 1, i].HasTile || !settings.GroundTest(Main.tile[checkedX - 1, i].TileType)) && (!Main.tile[checkedX + 1, i].HasTile || !settings.GroundTest(Main.tile[checkedX + 1, i].TileType)))
			{
				return false;
			}
			TileColorCache cache = Main.tile[checkedX, i].BlockColorAndCoating();
			if (Main.tenthAnniversaryWorld && !gen && (settings.TreeTileType == 596 || settings.TreeTileType == 616))
			{
				cache.Color = (byte)genRand.Next(1, 13);
			}
			int num = 2;
			int num2 = genRand.Next(settings.TreeHeightMin, settings.TreeHeightMax + 1);
			int num3 = num2 + settings.TreeTopPaddingNeeded;
			if (!EmptyTileCheck(checkedX - num, checkedX + num, i - num3, i - 1, 20))
			{
				return false;
			}
			bool flag2 = false;
			bool flag3 = false;
			int num4;
			for (int j = i - num2; j < i; j++)
			{
				Tile tile2 = Main.tile[checkedX, j];
				tile2.TileFrameNumber = (byte)genRand.Next(3);
				tile2.HasTile = true;
				tile2.TileType = settings.TreeTileType;
				tile2.UseBlockColors(cache);
				num4 = genRand.Next(3);
				int num5 = genRand.Next(10);
				if (j == i - 1 || j == i - num2)
				{
					num5 = 0;
				}
				while (((num5 == 5 || num5 == 7) && flag2) || ((num5 == 6 || num5 == 7) && flag3))
				{
					num5 = genRand.Next(10);
				}
				flag2 = false;
				flag3 = false;
				if (num5 == 5 || num5 == 7)
				{
					flag2 = true;
				}
				if (num5 == 6 || num5 == 7)
				{
					flag3 = true;
				}
				switch (num5)
				{
					case 1:
						if (num4 == 0)
						{
							tile2.TileFrameX = 0;
							tile2.TileFrameY = 66;
						}
						if (num4 == 1)
						{
							tile2.TileFrameX = 0;
							tile2.TileFrameY = 88;
						}
						if (num4 == 2)
						{
							tile2.TileFrameX = 0;
							tile2.TileFrameY = 110;
						}
						break;
					case 2:
						if (num4 == 0)
						{
							tile2.TileFrameX = 22;
							tile2.TileFrameY = 0;
						}
						if (num4 == 1)
						{
							tile2.TileFrameX = 22;
							tile2.TileFrameY = 22;
						}
						if (num4 == 2)
						{
							tile2.TileFrameX = 22;
							tile2.TileFrameY = 44;
						}
						break;
					case 3:
						if (num4 == 0)
						{
							tile2.TileFrameX = 44;
							tile2.TileFrameY = 66;
						}
						if (num4 == 1)
						{
							tile2.TileFrameX = 44;
							tile2.TileFrameY = 88;
						}
						if (num4 == 2)
						{
							tile2.TileFrameX = 44;
							tile2.TileFrameY = 110;
						}
						break;
					case 4:
						if (num4 == 0)
						{
							tile2.TileFrameX = 22;
							tile2.TileFrameY = 66;
						}
						if (num4 == 1)
						{
							tile2.TileFrameX = 22;
							tile2.TileFrameY = 88;
						}
						if (num4 == 2)
						{
							tile2.TileFrameX = 22;
							tile2.TileFrameY = 110;
						}
						break;
					case 5:
						if (num4 == 0)
						{
							tile2.TileFrameX = 88;
							tile2.TileFrameY = 0;
						}
						if (num4 == 1)
						{
							tile2.TileFrameX = 88;
							tile2.TileFrameY = 22;
						}
						if (num4 == 2)
						{
							tile2.TileFrameX = 88;
							tile2.TileFrameY = 44;
						}
						break;
					case 6:
						if (num4 == 0)
						{
							tile2.TileFrameX = 66;
							tile2.TileFrameY = 66;
						}
						if (num4 == 1)
						{
							tile2.TileFrameX = 66;
							tile2.TileFrameY = 88;
						}
						if (num4 == 2)
						{
							tile2.TileFrameX = 66;
							tile2.TileFrameY = 110;
						}
						break;
					case 7:
						if (num4 == 0)
						{
							tile2.TileFrameX = 110;
							tile2.TileFrameY = 66;
						}
						if (num4 == 1)
						{
							tile2.TileFrameX = 110;
							tile2.TileFrameY = 88;
						}
						if (num4 == 2)
						{
							tile2.TileFrameX = 110;
							tile2.TileFrameY = 110;
						}
						break;
					default:
						if (num4 == 0)
						{
							tile2.TileFrameX = 0;
							tile2.TileFrameY = 0;
						}
						if (num4 == 1)
						{
							tile2.TileFrameX = 0;
							tile2.TileFrameY = 22;
						}
						if (num4 == 2)
						{
							tile2.TileFrameX = 0;
							tile2.TileFrameY = 44;
						}
						break;
				}
				if (num5 == 5 || num5 == 7)
				{
					Tile tile3 = Main.tile[checkedX - 1, j];
					tile3.HasTile = true;
					tile3.TileType = settings.TreeTileType;
					tile3.UseBlockColors(cache);
					num4 = genRand.Next(3);
					if (genRand.Next(3) < 2)
					{
						if (num4 == 0)
						{
							tile3.TileFrameX = 44;
							tile3.TileFrameY = 198;
						}
						if (num4 == 1)
						{
							tile3.TileFrameX = 44;
							tile3.TileFrameY = 220;
						}
						if (num4 == 2)
						{
							tile3.TileFrameX = 44;
							tile3.TileFrameY = 242;
						}
					}
					else
					{
						if (num4 == 0)
						{
							tile3.TileFrameX = 66;
							tile3.TileFrameY = 0;
						}
						if (num4 == 1)
						{
							tile3.TileFrameX = 66;
							tile3.TileFrameY = 22;
						}
						if (num4 == 2)
						{
							tile3.TileFrameX = 66;
							tile3.TileFrameY = 44;
						}
					}
				}
				if (num5 != 6 && num5 != 7)
				{
					continue;
				}
				Tile tile4 = Main.tile[checkedX + 1, j];
				tile4.HasTile = true;
				tile4.TileType = settings.TreeTileType;
				tile4.UseBlockColors(cache);
				num4 = genRand.Next(3);
				if (genRand.Next(3) < 2)
				{
					if (num4 == 0)
					{
						tile4.TileFrameX = 66;
						tile4.TileFrameY = 198;
					}
					if (num4 == 1)
					{
						tile4.TileFrameX = 66;
						tile4.TileFrameY = 220;
					}
					if (num4 == 2)
					{
						tile4.TileFrameX = 66;
						tile4.TileFrameY = 242;
					}
				}
				else
				{
					if (num4 == 0)
					{
						tile4.TileFrameX = 88;
						tile4.TileFrameY = 66;
					}
					if (num4 == 1)
					{
						tile4.TileFrameX = 88;
						tile4.TileFrameY = 88;
					}
					if (num4 == 2)
					{
						tile4.TileFrameX = 88;
						tile4.TileFrameY = 110;
					}
				}
			}
			bool flag4 = false;
			bool flag5 = false;
			if (Main.tile[checkedX - 1, i].HasUnactuatedTile && !Main.tile[checkedX - 1, i].IsHalfBlock && Main.tile[checkedX - 1, i].Slope == 0 && IsTileTypeFitForTree(Main.tile[checkedX - 1, i].TileType))
			{
				flag4 = true;
			}
			if (Main.tile[checkedX + 1, i].HasUnactuatedTile && !Main.tile[checkedX + 1, i].IsHalfBlock && Main.tile[checkedX + 1, i].Slope == 0 && IsTileTypeFitForTree(Main.tile[checkedX + 1, i].TileType))
			{
				flag5 = true;
			}
			if (genRand.NextBool(3))
			{
				flag4 = false;
			}
			if (genRand.NextBool(3))
			{
				flag5 = false;
			}
			if (flag5)
			{
				Tile HasTile1 = Main.tile[checkedX + 1, i - 1];
				HasTile1.HasTile = true;
				Main.tile[checkedX + 1, i - 1].TileType = settings.TreeTileType;
				Main.tile[checkedX + 1, i - 1].UseBlockColors(cache);
				num4 = genRand.Next(3);
				if (num4 == 0)
				{
					Main.tile[checkedX + 1, i - 1].TileFrameX = 22;
					Main.tile[checkedX + 1, i - 1].TileFrameY = 132;
				}
				if (num4 == 1)
				{
					Main.tile[checkedX + 1, i - 1].TileFrameX = 22;
					Main.tile[checkedX + 1, i - 1].TileFrameY = 154;
				}
				if (num4 == 2)
				{
					Main.tile[checkedX + 1, i - 1].TileFrameX = 22;
					Main.tile[checkedX + 1, i - 1].TileFrameY = 176;
				}
			}
			if (flag4)
			{
				Tile HasTile2 = Main.tile[checkedX - 1, i - 1];
				HasTile2.HasTile = true;
				Main.tile[checkedX - 1, i - 1].TileType = settings.TreeTileType;
				Main.tile[checkedX - 1, i - 1].UseBlockColors(cache);
				num4 = genRand.Next(3);
				if (num4 == 0)
				{
					Main.tile[checkedX - 1, i - 1].TileFrameX = 44;
					Main.tile[checkedX - 1, i - 1].TileFrameY = 132;
				}
				if (num4 == 1)
				{
					Main.tile[checkedX - 1, i - 1].TileFrameX = 44;
					Main.tile[checkedX - 1, i - 1].TileFrameY = 154;
				}
				if (num4 == 2)
				{
					Main.tile[checkedX - 1, i - 1].TileFrameX = 44;
					Main.tile[checkedX - 1, i - 1].TileFrameY = 176;
				}
			}
			num4 = genRand.Next(3);
			if (flag4 && flag5)
			{
				if (num4 == 0)
				{
					Main.tile[checkedX, i - 1].TileFrameX = 88;
					Main.tile[checkedX, i - 1].TileFrameY = 132;
				}
				if (num4 == 1)
				{
					Main.tile[checkedX, i - 1].TileFrameX = 88;
					Main.tile[checkedX, i - 1].TileFrameY = 154;
				}
				if (num4 == 2)
				{
					Main.tile[checkedX, i - 1].TileFrameX = 88;
					Main.tile[checkedX, i - 1].TileFrameY = 176;
				}
			}
			else if (flag4)
			{
				if (num4 == 0)
				{
					Main.tile[checkedX, i - 1].TileFrameX = 0;
					Main.tile[checkedX, i - 1].TileFrameY = 132;
				}
				if (num4 == 1)
				{
					Main.tile[checkedX, i - 1].TileFrameX = 0;
					Main.tile[checkedX, i - 1].TileFrameY = 154;
				}
				if (num4 == 2)
				{
					Main.tile[checkedX, i - 1].TileFrameX = 0;
					Main.tile[checkedX, i - 1].TileFrameY = 176;
				}
			}
			else if (flag5)
			{
				if (num4 == 0)
				{
					Main.tile[checkedX, i - 1].TileFrameX = 66;
					Main.tile[checkedX, i - 1].TileFrameY = 132;
				}
				if (num4 == 1)
				{
					Main.tile[checkedX, i - 1].TileFrameX = 66;
					Main.tile[checkedX, i - 1].TileFrameY = 154;
				}
				if (num4 == 2)
				{
					Main.tile[checkedX, i - 1].TileFrameX = 66;
					Main.tile[checkedX, i - 1].TileFrameY = 176;
				}
			}
			if (!genRand.NextBool(13))
			{
				num4 = genRand.Next(3);
				if (num4 == 0)
				{
					Main.tile[checkedX, i - num2].TileFrameX = 22;
					Main.tile[checkedX, i - num2].TileFrameY = 198;
				}
				if (num4 == 1)
				{
					Main.tile[checkedX, i - num2].TileFrameX = 22;
					Main.tile[checkedX, i - num2].TileFrameY = 220;
				}
				if (num4 == 2)
				{
					Main.tile[checkedX, i - num2].TileFrameX = 22;
					Main.tile[checkedX, i - num2].TileFrameY = 242;
				}
			}
			else
			{
				num4 = genRand.Next(3);
				if (num4 == 0)
				{
					Main.tile[checkedX, i - num2].TileFrameX = 0;
					Main.tile[checkedX, i - num2].TileFrameY = 198;
				}
				if (num4 == 1)
				{
					Main.tile[checkedX, i - num2].TileFrameX = 0;
					Main.tile[checkedX, i - num2].TileFrameY = 220;
				}
				if (num4 == 2)
				{
					Main.tile[checkedX, i - num2].TileFrameX = 0;
					Main.tile[checkedX, i - num2].TileFrameY = 242;
				}
			}
			RangeFrame(checkedX - 2, i - num2 - 1, checkedX + 2, i + 1);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, checkedX - 1, i - num2, 3, num2);
			}
			return true;
		}

		public static bool IsTileTypeFitForTree(ushort type)
		{
			return type == TileID.Stone;
		}

		public static bool GetGemTreeFoliageData(int i, int j, int xoffset, ref int treeFrame, out int floorY, out int topTextureFrameWidth, out int topTextureFrameHeight)
		{
			int num = i + xoffset;
			topTextureFrameWidth = 116;
			topTextureFrameHeight = 96;
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
				texture2D = (Texture2D)ModContent.Request<Texture2D>(TexturePath + "_Tops");
			}
			return texture2D;
		}

		public Texture2D GetTreeBranchTexture(int tileType, int treeTextureStyle, byte tileColor)
		{
			Texture2D texture2D = TryGetTreeBranchAndRequestIfNotReady(tileType, treeTextureStyle, tileColor);
			if (texture2D == null)
			{
				texture2D = (Texture2D)ModContent.Request<Texture2D>(TexturePath + "_Branches");
			}
			return texture2D;
		}

		//Paint Content
		public static TreePaintingSettings GetTreeFoliageSettings(int tileType)
		{
			if (tileType == ModContent.TileType<TourmalineTree>())
			{
				return TourmalineTree.GemTreeTourmaline;
			}
			else if (tileType == ModContent.TileType<PeridotTree>())
			{
				return PeridotTree.GemTreePeridot;
			}
			else if (tileType == ModContent.TileType<ZirconTree>())
			{
				return ZirconTree.GemTreeZircon;
			}
			else
			{
				return null;
			}
		}

		public class TreeTopRenderTargetHolder : ARenderTargetHolder
		{
			public TreeFoliageVariantKey Key;

			public override void Prepare()
			{
				Asset<Texture2D> asset;
				if (Key.TextureIndex == ModContent.TileType<TourmalineTree>())
				{
					asset = ModContent.Request<Texture2D>("Avalon/Tiles/GemTrees/TourmalineTree_Tops");
				}
				else if (Key.TextureIndex == ModContent.TileType<PeridotTree>())
				{
					asset = ModContent.Request<Texture2D>("Avalon/Tiles/GemTrees/PeridotTree_Tops");
				}
				else
				{
					asset = ModContent.Request<Texture2D>("Avalon/Tiles/GemTrees/ZirconTree_Tops");
				}
				if (asset == null)
				{
					asset = TextureAssets.TreeTop[0];
				}
				asset.Wait?.Invoke();
				PrepareTextureIfNecessary(asset.Value);
			}

			public override void PrepareShader()
			{
				PrepareShader(Key.PaintColor, GetTreeFoliageSettings(Key.TextureIndex));
			}
		}

		public class TreeBranchTargetHolder : ARenderTargetHolder
		{
			public TreeFoliageVariantKey Key;

			public override void Prepare()
			{
				Asset<Texture2D> asset;
				if (Key.TextureIndex == ModContent.TileType<TourmalineTree>())
				{
					asset = ModContent.Request<Texture2D>("Avalon/Tiles/GemTrees/TourmalineTree_Branches");
				}
				else if (Key.TextureIndex == ModContent.TileType<PeridotTree>())
				{
					asset = ModContent.Request<Texture2D>("Avalon/Tiles/GemTrees/PeridotTree_Branches");
				}
				else
				{
					asset = ModContent.Request<Texture2D>("Avalon/Tiles/GemTrees/ZirconTree_Branches");
				}
				if (asset == null)
				{
					asset = TextureAssets.TreeBranch[0];
				}
				asset.Wait?.Invoke();
				PrepareTextureIfNecessary(asset.Value);
			}

			public override void PrepareShader()
			{
				PrepareShader(Key.PaintColor, GetTreeFoliageSettings(Key.TextureIndex));
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
	}
}
