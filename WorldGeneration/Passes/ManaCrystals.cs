using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes;

internal class ManaCrystals : GenPass
{
    public ManaCrystals(string name, double loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        for (int num381 = 0; num381 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-06); num381++)
        {
            bool flag27 = false;
            int num383 = 0;
            while (!flag27)
            {
                if (AvalonWorld.AddManaCrystal(WorldGen.genRand.Next(1, Main.maxTilesX), WorldGen.genRand.Next((int)(GenVars.worldSurfaceLow + 20.0), Main.maxTilesY - 300)))
                {
                    flag27 = true;
                }
                else
                {
                    num383++;
                    if (num383 >= 10000)
                    {
                        flag27 = true;
                    }
                }
            }
        }
    }
}
