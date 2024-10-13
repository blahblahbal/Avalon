using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.WorldGen;

namespace Avalon.Tiles;

public class ResistantSapling : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = true;

		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.AnchorValidTiles = new[] { ModContent.TileType<Ectograss>() };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.DrawFlipHorizontal = true;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.RandomStyleRange = 3;
		TileObjectData.newTile.StyleMultiplier = 3;

		TileObjectData.addTile(Type);

		LocalizedText name = CreateMapEntryName();
		AddMapEntry(new Color(42, 43, 51), name);

		//TileID.Sets.TreeSapling[Type] = true;
		TileID.Sets.CommonSapling[Type] = true;
		TileID.Sets.SwaysInWindBasic[Type] = true;

		DustType = ModContent.DustType<ResistantWoodDust>();

		AdjTiles = new int[] { TileID.Saplings };
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = fail ? 1 : 3;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		if (tile.HasUnactuatedTile)
		{
			if (j > Main.rockLayer)
			{
				if (WorldGen.genRand.NextBool(5))
				{
					AttemptToGrowResistantFromSapling(i, j);
				}
			}
			else
			{
				if (WorldGen.genRand.NextBool(20))
				{
					AttemptToGrowResistantFromSapling(i, j);
				}
			}
		}
	}

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
	{
		if (i % 2 == 1)
		{
			effects = SpriteEffects.FlipHorizontally;
		}
	}

	public static bool AttemptToGrowResistantFromSapling(int x, int y)
	{
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			return false;
		}
		if (!WorldGen.InWorld(x, y, 2))
		{
			return false;
		}
		Tile tile = Main.tile[x, y];
		if (tile == null || !tile.HasTile)
		{
			return false;
		}
		bool flag = GrowResistantTreeWithSettings(x, y, ResistantTree.Tree_Resistant);
		if (flag && WorldGen.PlayerLOS(x, y))
		{
			//GrowResistantTreeFXCheck(x, y);
		}
		return flag;
	}

	public static bool GrowResistantTreeWithSettings(int checkedX, int checkedY, GrowTreeSettings settings)
	{
		int i;

		for (i = checkedY; Main.tile[checkedX, i].TileType == settings.SaplingTileType; i--)
		{
		}

		/*if (Main.tile[checkedX - 1, i - 1].LiquidAmount != 0 || Main.tile[checkedX, i - 1].LiquidAmount != 0 || Main.tile[checkedX + 1, i - 1].LiquidAmount != 0)
		{
			return false;
		}*/

		Tile tile = Main.tile[checkedX, i];
		/*if (!tile.HasUnactuatedTile || tile.IsHalfBlock || tile.Slope != 0)
		{
			return false;
		}*/
		bool flag = settings.WallTest(Main.tile[checkedX, i + 1].WallType);
		/*if (!settings.GroundTest(tile.TileType) || !flag)
		{
			return false;
		}
		if ((!Main.tile[checkedX + 1, i].HasTile || !settings.GroundTest(Main.tile[checkedX + 1, i].TileType)) && (!Main.tile[checkedX - 1, i].HasTile || !settings.GroundTest(Main.tile[checkedX - 1, i].TileType)))
		{
			return false;
		}*/
		TileColorCache cache = Main.tile[checkedX, i].BlockColorAndCoating();
		if (Main.tenthAnniversaryWorld && !gen && (settings.TreeTileType == 596 || settings.TreeTileType == 616))
		{
			cache.Color = (byte)genRand.Next(1, 13);
		}
		int num = 2;
		int num2 = genRand.Next(settings.TreeHeightMin, settings.TreeHeightMax + 1);
		int num3 = num2 + settings.TreeTopPaddingNeeded;
		/*if (!EmptyTileCheck(checkedX - num, checkedX + num, i - num3, i - 1, 20))
		{
			return false;
		}*/
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
				Tile tile3 = Main.tile[checkedX + 1, j];
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
			Tile tile4 = Main.tile[checkedX - 1, j];
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
		if (Main.tile[checkedX + 1, i].HasUnactuatedTile && !Main.tile[checkedX + 1, i].IsHalfBlock && Main.tile[checkedX + 1, i].Slope == 0 && IsTileTypeFitForTree(Main.tile[checkedX + 1, i].TileType))
		{
			flag4 = true;
		}
		if (Main.tile[checkedX - 1, i].HasUnactuatedTile && !Main.tile[checkedX - 1, i].IsHalfBlock && Main.tile[checkedX - 1, i].Slope == 0 && IsTileTypeFitForTree(Main.tile[checkedX - 1, i].TileType))
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
			Tile HasTile1 = Main.tile[checkedX - 1, i + 1];
			HasTile1.HasTile = true;
			Main.tile[checkedX - 1, i + 1].TileType = settings.TreeTileType;
			Main.tile[checkedX - 1, i + 1].UseBlockColors(cache);
			num4 = genRand.Next(3);
			if (num4 == 0)
			{
				Main.tile[checkedX - 1, i + 1].TileFrameX = 22;
				Main.tile[checkedX - 1, i + 1].TileFrameY = 132;
			}
			if (num4 == 1)
			{
				Main.tile[checkedX - 1, i + 1].TileFrameX = 22;
				Main.tile[checkedX - 1, i + 1].TileFrameY = 154;
			}
			if (num4 == 2)
			{
				Main.tile[checkedX - 1, i + 1].TileFrameX = 22;
				Main.tile[checkedX - 1, i + 1].TileFrameY = 176;
			}
		}
		if (flag4)
		{
			Tile HasTile2 = Main.tile[checkedX + 1, i + 1];
			HasTile2.HasTile = true;
			Main.tile[checkedX + 1, i + 1].TileType = settings.TreeTileType;
			Main.tile[checkedX + 1, i + 1].UseBlockColors(cache);
			num4 = genRand.Next(3);
			if (num4 == 0)
			{
				Main.tile[checkedX + 1, i + 1].TileFrameX = 44;
				Main.tile[checkedX + 1, i + 1].TileFrameY = 132;
			}
			if (num4 == 1)
			{
				Main.tile[checkedX + 1, i + 1].TileFrameX = 44;
				Main.tile[checkedX + 1, i + 1].TileFrameY = 154;
			}
			if (num4 == 2)
			{
				Main.tile[checkedX + 1, i + 1].TileFrameX = 44;
				Main.tile[checkedX + 1, i + 1].TileFrameY = 176;
			}
		}
		num4 = genRand.Next(3);
		if (flag4 && flag5)
		{
			if (num4 == 0)
			{
				Main.tile[checkedX, i + 1].TileFrameX = 88;
				Main.tile[checkedX, i + 1].TileFrameY = 132;
			}
			if (num4 == 1)
			{
				Main.tile[checkedX, i + 1].TileFrameX = 88;
				Main.tile[checkedX, i + 1].TileFrameY = 154;
			}
			if (num4 == 2)
			{
				Main.tile[checkedX, i + 1].TileFrameX = 88;
				Main.tile[checkedX, i + 1].TileFrameY = 176;
			}
		}
		else if (flag4)
		{
			if (num4 == 0)
			{
				Main.tile[checkedX, i + 1].TileFrameX = 0;
				Main.tile[checkedX, i + 1].TileFrameY = 132;
			}
			if (num4 == 1)
			{
				Main.tile[checkedX, i + 1].TileFrameX = 0;
				Main.tile[checkedX, i + 1].TileFrameY = 154;
			}
			if (num4 == 2)
			{
				Main.tile[checkedX, i + 1].TileFrameX = 0;
				Main.tile[checkedX, i + 1].TileFrameY = 176;
			}
		}
		else if (flag5)
		{
			if (num4 == 0)
			{
				Main.tile[checkedX, i + 1].TileFrameX = 66;
				Main.tile[checkedX, i + 1].TileFrameY = 132;
			}
			if (num4 == 1)
			{
				Main.tile[checkedX, i + 1].TileFrameX = 66;
				Main.tile[checkedX, i + 1].TileFrameY = 154;
			}
			if (num4 == 2)
			{
				Main.tile[checkedX, i + 1].TileFrameX = 66;
				Main.tile[checkedX, i + 1].TileFrameY = 176;
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


	public static void GrowResistantTreeFXCheck(int x, int y)
	{
		int treeHeight = 1;
		for (int num = -1; num > -100; num--)
		{
			Tile tile = Main.tile[x, y + num];
			if (!tile.HasTile || !TileID.Sets.GetsCheckedForLeaves[tile.TileType])
			{
				break;
			}
			treeHeight++;
		}
		for (int i = 1; i < 5; i++)
		{
			Tile tile2 = Main.tile[x, y + i];
			if (tile2.HasTile && TileID.Sets.GetsCheckedForLeaves[tile2.TileType])
			{
				treeHeight++;
				continue;
			}
			break;
		}
		if (treeHeight > 0)
		{
			if (Main.netMode == NetmodeID.Server)
			{
				//NetMessage.SendData(MessageID.SpecialFX, -1, -1, null, 1, x, y, treeHeight, ModContent.GoreType<PetrifiedTreeLeaf>());
			}
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				//WorldGen.TreeGrowFX(x, y, treeHeight, ModContent.GoreType<PetrifiedTreeLeaf>());
			}
		}
	}
}
