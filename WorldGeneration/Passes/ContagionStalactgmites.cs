using Terraria;
using Terraria.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes
{
    internal class ContagionStalactgmites : GenPass
    {
        public ContagionStalactgmites() : base("Contagion Stalac", 20f)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            for (int num19 = 20; num19 < Main.maxTilesX - 20; num19++)
            {
                for (int num20 = (int)Main.worldSurface; num20 < Main.maxTilesY - 20; num20++)
                {
                    if (!WorldGen.oceanDepths(num19, num20) && !Main.tile[num19, num20].HasTile && WorldGen.genRand.NextBool(5))
                    {
                        if ((Main.tile[num19, num20 - 1].TileType == ModContent.TileType<Tiles.Contagion.Chunkstone>()) &&
                            !Main.tile[num19, num20].HasTile && !Main.tile[num19, num20 + 1].HasTile)
                        {
                            Tile t = Main.tile[num19, num20 - 1];
                            t.Slope = SlopeType.Solid;
                        }
                        if ((Main.tile[num19, num20 + 1].TileType == ModContent.TileType<Tiles.Contagion.Chunkstone>()) &&
                            !Main.tile[num19, num20].HasTile && !Main.tile[num19, num20 - 1].HasTile)
                        {
                            Tile t = Main.tile[num19, num20 + 1];
                            t.Slope = SlopeType.Solid;
                        }
                        Utils.PlaceContagionTight(num19, num20);
                    }
                }
            }
        }
    }
}
