using Avalon.Tiles.Contagion;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes
{
    internal class ShortGrass : GenPass
    {
        public ShortGrass(string name, double loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            for (int num244 = 0; num244 < Main.maxTilesX; num244++)
            {
                progress.Set((double)num244 / Main.maxTilesX);
                for (int num245 = 1; num245 < Main.maxTilesY; num245++)
                {
                    if (Main.tile[num244, num245].TileType == ModContent.TileType<Ickgrass>() && Main.tile[num244, num245].HasUnactuatedTile)
                    {
                        if (!Main.tile[num244, num245 - 1].HasTile)
                        {
                            WorldGen.PlaceTile(num244, num245 - 1, ModContent.TileType<ContagionShortGrass>(), true, style: WorldGen.genRand.Next(23));
                        }
                    }
                }
            }
        }
    }
}
