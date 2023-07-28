using Terraria;
using Terraria.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Avalon.Tiles.GemTrees;
using Avalon.Common;

namespace Avalon.WorldGeneration.Passes
{
    public class GemTreeHook : ModHook
    {
        protected override void Apply()
        {
            On_WorldGen.IsTileTypeFitForTree += On_WorldGen_IsTileTypeFitForTree;
        }

        private bool On_WorldGen_IsTileTypeFitForTree(On_WorldGen.orig_IsTileTypeFitForTree orig, ushort type)
        {
            if (TileID.Sets.Stone[type] || Main.tileMoss[type]) return true;
            return orig(type);
        }
    }
    internal class GemTreePass : GenPass
    {
        public GemTreePass() : base("Avalon Gem Trees", 20f)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            for (int num19 = 20; num19 < Main.maxTilesX - 20; num19++)
            {
                for (int num20 = (int)Main.worldSurface; num20 < Main.maxTilesY - 20; num20++)
                {
                    if ((Main.tenthAnniversaryWorld || WorldGen.drunkWorldGen || WorldGen.genRand.NextBool(2)) && Main.tile[num19, num20 - 1].LiquidAmount == 0)
                    {
                        int t = WorldGen.genRand.Next(3);
                        int treeTileType;
                        WorldGen.GrowTreeSettings g;
                        switch (t)
                        {
                            case 0:
                                treeTileType = ModContent.TileType<TourmalineSapling>();
                                g = TourmalineSapling.GemTree_Tourmaline;
                                break;
                            case 1:
                                treeTileType = ModContent.TileType<PeridotSapling>();
                                g = PeridotSapling.GemTree_Peridot;
                                break;
                            default:
                                treeTileType = ModContent.TileType<ZirconSapling>();
                                g = ZirconSapling.GemTree_Zircon;
                                break;
                        }
                        if (!Main.tile[num19, num20].HasTile)
                        {
                            //Tile t1 = Framing.GetTileSafely(num19, num20);
                            //Tile t2 = Framing.GetTileSafely(num19, num20 - 1);

                            //short style = (short)WorldGen.genRand.Next(3);

                            //t1.HasTile = true;
                            //t2.HasTile = true;
                            //t1.TileType = (ushort)treeTileType;
                            //t2.TileType = (ushort)treeTileType;
                            //t1.TileFrameX = (short)(18 * style);
                            //t2.TileFrameX = (short)(18 * style);
                            //t1.TileFrameY = 16;
                            //Main.tile[num19, num20 - 1].TileType = (ushort)treeTileType;
                            //Main.tile[num19, num20].TileType = (ushort)treeTileType;
                            //Main.tile[num19, num20 - 1].Active(true);
                            //Main.tile[num19, num20].Active(true);
                            //Main.tile[num19, num20 - 1].TileFrameX = (short)(18 * style);
                            //Main.tile[num19, num20].TileFrameX = (short)(18 * style);
                            //Main.tile[num19, num20].TileFrameY = 16;
                            WorldGen.Place1x2(num19, num20, (ushort)treeTileType, 0);
                            //WorldGen.PlaceObject(num19, num20 - 1, treeTileType, true);
                            //WorldGen.PlaceTile(num19, num20 - 1, treeTileType, true);
                            //int q;
                            //for (q = num20; TileID.Sets.CommonSapling[Main.tile[num19, q].TileType]; q++)
                            //{
                            //}
                            if (!GrowGemTree(num19, num20, (ushort)treeTileType) &&
                                (Main.tile[num19, num20].TileType == ModContent.TileType<TourmalineSapling>() ||
                                Main.tile[num19, num20].TileType == ModContent.TileType<PeridotSapling>() ||
                                Main.tile[num19, num20].TileType == ModContent.TileType<ZirconSapling>()))
                            {
                                WorldGen.KillTile(num19, num20);
                            }
                            /*PlantLoader.Get<ModTree>(5, TileID.Stone).*/
                        }
                    }
                }
            }
        }

        public static bool GrowGemTree(int i, int y, ushort ID)
        {
            int j;
            for (j = y; Main.tile[i, j].TileType == ID; j++)
            {
            }
            //if ((Main.tile[i - 1, j - 1].LiquidAmount != 0 || Main.tile[i, j - 1].LiquidAmount != 0 || Main.tile[i + 1, j - 1].LiquidAmount != 0) && !WorldGen.notTheBees)
            //{
            //    return false;
            //}
            if (Main.tile[i, j].HasUnactuatedTile && !Main.tile[i, j].IsHalfBlock && Main.tile[i, j].Slope == SlopeType.Solid && WorldGen.IsTileTypeFitForTree(Main.tile[i, j].TileType) && ((Main.remixWorld && (double)j > Main.worldSurface) || Main.tile[i, j - 1].WallType == 0 || WorldGen.DefaultTreeWallTest(Main.tile[i, j - 1].WallType)) && ((Main.tile[i - 1, j].HasTile && WorldGen.IsTileTypeFitForTree(Main.tile[i - 1, j].TileType)) || (Main.tile[i + 1, j].HasTile && WorldGen.IsTileTypeFitForTree(Main.tile[i + 1, j].TileType))))
            {
                TileColorCache cache = Main.tile[i, j].BlockColorAndCoating();
                if (Main.tenthAnniversaryWorld && !WorldGen.gen)
                {
                    cache.Color = (byte)WorldGen.genRand.Next(1, 13);
                }
                int num = 2;
                int num2 = WorldGen.genRand.Next(5, 17);
                int num3 = num2 + 4;
                if (Main.tile[i, j].TileType == 60)
                {
                    num3 += 5;
                }
                bool flag = false;
                if (Main.tile[i, j].TileType == 70 && WorldGen.EmptyTileCheck(i - num, i + num, j - num3, j - 3, ID) && WorldGen.EmptyTileCheck(i - 1, i + 1, j - 2, j - 1, ID))
                {
                    flag = true;
                }
                if (WorldGen.EmptyTileCheck(i - num, i + num, j - num3, j - 1, ID))
                {
                    flag = true;
                }
                if (flag)
                {
                    bool flag2 = Main.remixWorld && (double)j < Main.worldSurface;
                    bool flag3 = false;
                    bool flag4 = false;
                    int num4;
                    for (int k = j - num2; k < j; k++)
                    {
                        Tile t = Main.tile[i, k];
                        t.TileFrameNumber = (byte)WorldGen.genRand.Next(3);
                        t.HasTile = true;
                        t.TileType = 5;
                        t.UseBlockColors(cache);
                        num4 = WorldGen.genRand.Next(3);
                        int num5 = WorldGen.genRand.Next(10);
                        if (k == j - 1 || k == j - num2)
                        {
                            num5 = 0;
                        }
                        while (((num5 == 5 || num5 == 7) && flag3) || ((num5 == 6 || num5 == 7) && flag4))
                        {
                            num5 = WorldGen.genRand.Next(10);
                        }
                        flag3 = false;
                        flag4 = false;
                        if (num5 == 5 || num5 == 7)
                        {
                            flag3 = true;
                        }
                        if (num5 == 6 || num5 == 7)
                        {
                            flag4 = true;
                        }
                        switch (num5)
                        {
                            case 1:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            case 2:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 0;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 22;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 44;
                                }
                                break;
                            case 3:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 44;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 44;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 44;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            case 4:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            case 5:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 88;
                                    Main.tile[i, k].TileFrameY = 0;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 88;
                                    Main.tile[i, k].TileFrameY = 22;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 88;
                                    Main.tile[i, k].TileFrameY = 44;
                                }
                                break;
                            case 6:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 66;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 66;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 66;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            case 7:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 110;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 110;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 110;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            default:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 0;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 22;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 44;
                                }
                                break;
                        }
                        if (num5 == 5 || num5 == 7)
                        {
                            Main.tile[i - 1, k].Active(true);
                            Main.tile[i - 1, k].TileType = 5;
                            Main.tile[i - 1, k].UseBlockColors(cache);
                            num4 = WorldGen.genRand.Next(3);
                            if (WorldGen.genRand.Next(3) < 2 && !flag2)
                            {
                                if (num4 == 0)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 44;
                                    Main.tile[i - 1, k].TileFrameY = 198;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 44;
                                    Main.tile[i - 1, k].TileFrameY = 220;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 44;
                                    Main.tile[i - 1, k].TileFrameY = 242;
                                }
                            }
                            else
                            {
                                if (num4 == 0)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 66;
                                    Main.tile[i - 1, k].TileFrameY = 0;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 66;
                                    Main.tile[i - 1, k].TileFrameY = 22;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 66;
                                    Main.tile[i - 1, k].TileFrameY = 44;
                                }
                            }
                        }
                        if (num5 != 6 && num5 != 7)
                        {
                            continue;
                        }
                        Main.tile[i + 1, k].Active(true);
                        Main.tile[i + 1, k].TileType = 5;
                        Main.tile[i + 1, k].UseBlockColors(cache);
                        num4 = WorldGen.genRand.Next(3);
                        if (WorldGen.genRand.Next(3) < 2 && !flag2)
                        {
                            if (num4 == 0)
                            {
                                Main.tile[i + 1, k].TileFrameX = 66;
                                Main.tile[i + 1, k].TileFrameY = 198;
                            }
                            if (num4 == 1)
                            {
                                Main.tile[i + 1, k].TileFrameX = 66;
                                Main.tile[i + 1, k].TileFrameY = 220;
                            }
                            if (num4 == 2)
                            {
                                Main.tile[i + 1, k].TileFrameX = 66;
                                Main.tile[i + 1, k].TileFrameY = 242;
                            }
                        }
                        else
                        {
                            if (num4 == 0)
                            {
                                Main.tile[i + 1, k].TileFrameX = 88;
                                Main.tile[i + 1, k].TileFrameY = 66;
                            }
                            if (num4 == 1)
                            {
                                Main.tile[i + 1, k].TileFrameX = 88;
                                Main.tile[i + 1, k].TileFrameY = 88;
                            }
                            if (num4 == 2)
                            {
                                Main.tile[i + 1, k].TileFrameX = 88;
                                Main.tile[i + 1, k].TileFrameY = 110;
                            }
                        }
                    }
                    int num6 = WorldGen.genRand.Next(3);
                    bool flag5 = false;
                    bool flag6 = false;
                    if (Main.tile[i - 1, j].HasUnactuatedTile && !Main.tile[i - 1, j].IsHalfBlock && Main.tile[i - 1, j].Slope == SlopeType.Solid && WorldGen.IsTileTypeFitForTree(Main.tile[i - 1, j].TileType))
                    {
                        flag5 = true;
                    }
                    if (Main.tile[i + 1, j].HasUnactuatedTile && !Main.tile[i + 1, j].IsHalfBlock && Main.tile[i + 1, j].Slope == SlopeType.Solid && WorldGen.IsTileTypeFitForTree(Main.tile[i + 1, j].TileType))
                    {
                        flag6 = true;
                    }
                    if (!flag5)
                    {
                        if (num6 == 0)
                        {
                            num6 = 2;
                        }
                        if (num6 == 1)
                        {
                            num6 = 3;
                        }
                    }
                    if (!flag6)
                    {
                        if (num6 == 0)
                        {
                            num6 = 1;
                        }
                        if (num6 == 2)
                        {
                            num6 = 3;
                        }
                    }
                    if (flag5 && !flag6)
                    {
                        num6 = 2;
                    }
                    if (flag6 && !flag5)
                    {
                        num6 = 1;
                    }
                    if (num6 == 0 || num6 == 1)
                    {
                        Main.tile[i + 1, j - 1].Active(true);
                        Main.tile[i + 1, j - 1].TileType = 5;
                        Main.tile[i + 1, j - 1].UseBlockColors(cache);
                        num4 = WorldGen.genRand.Next(3);
                        if (num4 == 0)
                        {
                            Main.tile[i + 1, j - 1].TileFrameX = 22;
                            Main.tile[i + 1, j - 1].TileFrameY = 132;
                        }
                        if (num4 == 1)
                        {
                            Main.tile[i + 1, j - 1].TileFrameX = 22;
                            Main.tile[i + 1, j - 1].TileFrameY = 154;
                        }
                        if (num4 == 2)
                        {
                            Main.tile[i + 1, j - 1].TileFrameX = 22;
                            Main.tile[i + 1, j - 1].TileFrameY = 176;
                        }
                    }
                    if (num6 == 0 || num6 == 2)
                    {
                        Main.tile[i - 1, j - 1].Active(true);
                        Main.tile[i - 1, j - 1].TileType = 5;
                        Main.tile[i - 1, j - 1].UseBlockColors(cache);
                        num4 = WorldGen.genRand.Next(3);
                        if (num4 == 0)
                        {
                            Main.tile[i - 1, j - 1].TileFrameX = 44;
                            Main.tile[i - 1, j - 1].TileFrameY = 132;
                        }
                        if (num4 == 1)
                        {
                            Main.tile[i - 1, j - 1].TileFrameX = 44;
                            Main.tile[i - 1, j - 1].TileFrameY = 154;
                        }
                        if (num4 == 2)
                        {
                            Main.tile[i - 1, j - 1].TileFrameX = 44;
                            Main.tile[i - 1, j - 1].TileFrameY = 176;
                        }
                    }
                    num4 = WorldGen.genRand.Next(3);
                    switch (num6)
                    {
                        case 0:
                            if (num4 == 0)
                            {
                                Main.tile[i, j - 1].TileFrameX = 88;
                                Main.tile[i, j - 1].TileFrameY = 132;
                            }
                            if (num4 == 1)
                            {
                                Main.tile[i, j - 1].TileFrameX = 88;
                                Main.tile[i, j - 1].TileFrameY = 154;
                            }
                            if (num4 == 2)
                            {
                                Main.tile[i, j - 1].TileFrameX = 88;
                                Main.tile[i, j - 1].TileFrameY = 176;
                            }
                            break;
                        case 1:
                            if (num4 == 0)
                            {
                                Main.tile[i, j - 1].TileFrameX = 0;
                                Main.tile[i, j - 1].TileFrameY = 132;
                            }
                            if (num4 == 1)
                            {
                                Main.tile[i, j - 1].TileFrameX = 0;
                                Main.tile[i, j - 1].TileFrameY = 154;
                            }
                            if (num4 == 2)
                            {
                                Main.tile[i, j - 1].TileFrameX = 0;
                                Main.tile[i, j - 1].TileFrameY = 176;
                            }
                            break;
                        case 2:
                            if (num4 == 0)
                            {
                                Main.tile[i, j - 1].TileFrameX = 66;
                                Main.tile[i, j - 1].TileFrameY = 132;
                            }
                            if (num4 == 1)
                            {
                                Main.tile[i, j - 1].TileFrameX = 66;
                                Main.tile[i, j - 1].TileFrameY = 154;
                            }
                            if (num4 == 2)
                            {
                                Main.tile[i, j - 1].TileFrameX = 66;
                                Main.tile[i, j - 1].TileFrameY = 176;
                            }
                            break;
                    }
                    if (WorldGen.genRand.Next(13) != 0 && !flag2)
                    {
                        num4 = WorldGen.genRand.Next(3);
                        if (num4 == 0)
                        {
                            Main.tile[i, j - num2].TileFrameX = 22;
                            Main.tile[i, j - num2].TileFrameY = 198;
                        }
                        if (num4 == 1)
                        {
                            Main.tile[i, j - num2].TileFrameX = 22;
                            Main.tile[i, j - num2].TileFrameY = 220;
                        }
                        if (num4 == 2)
                        {
                            Main.tile[i, j - num2].TileFrameX = 22;
                            Main.tile[i, j - num2].TileFrameY = 242;
                        }
                    }
                    else
                    {
                        num4 = WorldGen.genRand.Next(3);
                        if (num4 == 0)
                        {
                            Main.tile[i, j - num2].TileFrameX = 0;
                            Main.tile[i, j - num2].TileFrameY = 198;
                        }
                        if (num4 == 1)
                        {
                            Main.tile[i, j - num2].TileFrameX = 0;
                            Main.tile[i, j - num2].TileFrameY = 220;
                        }
                        if (num4 == 2)
                        {
                            Main.tile[i, j - num2].TileFrameX = 0;
                            Main.tile[i, j - num2].TileFrameY = 242;
                        }
                    }
                    WorldGen.RangeFrame(i - 2, j - num2 - 1, i + 2, j + 1);
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendTileSquare(-1, i - 1, j - num2, 3, num2);
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
