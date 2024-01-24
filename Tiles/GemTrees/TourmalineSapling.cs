using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.WorldGen;

namespace Avalon.Tiles.GemTrees;

public class TourmalineSapling : ModTile
{
    public static GrowTreeSettings GemTree_Tourmaline = new GrowTreeSettings
    {
        GroundTest = GemTreeGroundTest,
        WallTest = GemTreeWallTest,
        TreeHeightMax = 12,
        TreeHeightMin = 7,
        TreeTileType = (ushort)ModContent.TileType<TourmalineTree>(),
        TreeTopPaddingNeeded = 4,
        SaplingTileType = (ushort)ModContent.TileType<TourmalineSapling>()
    };
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.CommonSapling[Type] = true;
        TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.SlowlyDiesInWater[Type] = true;
        TileID.Sets.DrawFlipMode[Type] = 1;
        TileID.Sets.SwaysInWindBasic[Type] = true;
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 2;
        TileObjectData.newTile.Origin = new Point16(0, 1);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.UsesCustomCanPlace = true;
        TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.AnchorValidTiles = new int[15] { 1, 25, 117, 203, 182, 180, 179, 381, 183, 181, 534, 536, 539, 625, 627 };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.DrawFlipHorizontal = true;
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.RandomStyleRange = 3;
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(128, 128, 128));
        DustType = ModContent.DustType<Dusts.TourmalineDust>();
        AdjTiles = new int[1] { TileID.Saplings };
    }

    public override bool CanDrop(int i, int j)
    {
        return false;
    }

    public override void RandomUpdate(int i, int j)
    {
        if (j > Main.rockLayer)
        {
            Tile tile = Main.tile[i, j];
            if (tile.HasUnactuatedTile)
            {
                for (int k = 0; k < (Main.maxTilesX * Main.maxTilesY); k++)
                {

                    if (WorldGen.genRand.Next(5) == 0)
                    {
                        AttemptToGrowTourmalineFromSapling(i, j, underground: true);
                    }
                }
            }
        }
    }

    public static bool AttemptToGrowTourmalineFromSapling(int x, int y, bool underground)
    {
        if (Main.netMode == 1)
        {
            return false;
        }
        if (!InWorld(x, y, 2))
        {
            return false;
        }
        Tile tile = Main.tile[x, y];
        if (tile == null || !tile.HasTile)
        {
            return false;
        }
        if (!underground)
        {
            return false;
        }
        bool flag = GrowTourmalineTreeWithSettings(x, y, GemTree_Tourmaline);
        if (flag && PlayerLOS(x, y))
        {
            GrowTourmalineTreeFXCheck(x, y);
        }
        return flag;
    }

    public static bool GrowTourmalineTreeWithSettings(int checkedX, int checkedY, GrowTreeSettings settings)
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
        if (genRand.Next(3) == 0)
        {
            flag4 = false;
        }
        if (genRand.Next(3) == 0)
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
        if (genRand.Next(13) != 0)
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
        if (Main.netMode == 2)
        {
            NetMessage.SendTileSquare(-1, checkedX - 1, i - num2, 3, num2);
        }
        return true;
    }

    public static void GrowTourmalineTreeFXCheck(int x, int y)
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
            if (Main.netMode == 2)
            {
                NetMessage.SendData(112, -1, -1, null, 1, x, y, treeHeight, ModContent.GoreType<TourmalineGemLeaves>());
            }
            if (Main.netMode == 0)
            {
                WorldGen.TreeGrowFX(x, y, treeHeight, ModContent.GoreType<TourmalineGemLeaves>());
            }
        }
    }

    public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
    {
        if (i % 2 == 0)
        {
            effects = SpriteEffects.FlipHorizontally;
        }
    }
}
