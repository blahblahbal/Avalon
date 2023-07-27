using Terraria;
using Terraria.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

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
                    if ((Main.tenthAnniversaryWorld || WorldGen.drunkWorldGen || WorldGen.genRand.NextBool(5)) && Main.tile[num19, num20 - 1].LiquidAmount == 0)
                    {
                        int num21 = WorldGen.genRand.Next(3);
                        int treeTileType = 0;
                        switch (num21)
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
                            if (!WorldGen.GrowTree(num19, num20) && (Main.tile[num19, num20].TileType == ModContent.TileType<Tiles.GemTrees.TourmalineSapling>() ||
                                Main.tile[num19, num20].TileType == ModContent.TileType<Tiles.GemTrees.PeridotSapling>() || Main.tile[num19, num20].TileType == ModContent.TileType<Tiles.GemTrees.ZirconSapling>()))
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
