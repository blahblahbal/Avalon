using Terraria;
using Terraria.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Avalon.Tiles.GemTrees;

namespace Avalon.WorldGeneration.Passes
{
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
                        int treeTileType = 0;
                        switch (t)
                        {
                            case 0:
                                treeTileType = ModContent.TileType<Tiles.GemTrees.TourmalineSapling>();
                                break;
                            case 1:
                                treeTileType = ModContent.TileType<Tiles.GemTrees.PeridotSapling>();
                                break;
                            case 2:
                                treeTileType = ModContent.TileType<Tiles.GemTrees.ZirconSapling>();
                                break;
                        }
                        if (!Main.tile[num19, num20].HasTile)
                        {
                            WorldGen.PlaceTile(num19, num20, treeTileType, true);
                            int q;
                            for (q = num20; TileID.Sets.CommonSapling[Main.tile[num19, q].TileType]; q++)
                            {
                            }
                            WorldGen.GrowTreeSettings g;
                            switch (t)
                            {
                                case 0:
                                    g = TourmalineSapling.GemTree_Tourmaline;
                                    break;
                                case 1:
                                    g = PeridotSapling.GemTree_Peridot;
                                    break;
                                default:
                                    g = ZirconSapling.GemTree_Zircon;
                                    break;
                            }
                            if (!WorldGen.GrowTreeWithSettings(num19, q, g) &&
                                (Main.tile[num19, num20].TileType == ModContent.TileType<TourmalineSapling>() ||
                                Main.tile[num19, num20].TileType == ModContent.TileType<PeridotSapling>() ||
                                Main.tile[num19, num20].TileType == ModContent.TileType<ZirconSapling>()))
                            {
                                WorldGen.KillTile(num19, num20);
                            }
                        }
                    }
                }
            }
        }
    }
}
