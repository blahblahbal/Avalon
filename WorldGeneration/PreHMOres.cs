using ExxoAvalonOrigins.Tiles.Ores;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ExxoAvalonOrigins.WorldGeneration // Write original code challenged FAILED
{
    public class OsmiumPass : GenPass
    {
        public OsmiumPass(string name, float loadWeight) : base(name, loadWeight)
        {
        }
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            for (int roi = 0; roi < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); roi++)
            {
                WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY), WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(4, 7), ModContent.TileType<OsmiumOre>());
            }
        }
    }
}
