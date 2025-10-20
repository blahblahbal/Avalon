using Terraria;
using Terraria.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using ReLogic.Utilities;
using Avalon.Tiles.Contagion.IckyAltar;

namespace Avalon.WorldGeneration.Passes
{
    internal class IckyAltars : GenPass
    {
        public IckyAltars() : base("Icky Altars", 20f)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            Main.tileSolid[484] = false;
            progress.Message = Lang.gen[26].Value;
            int num641 = (int)((double)(Main.maxTilesX * Main.maxTilesY) * 3.3E-06);
            if (WorldGen.remixWorldGen)
            {
                num641 *= 3;
            }
            for (int num642 = 0; num642 < num641; num642++)
            {
                progress.Set(num642 / num641);
                for (int num643 = 0; num643 < 10000; num643++)
                {
                    int num644 = WorldGen.genRand.Next(281, Main.maxTilesX - 3 - 280);
                    while (num644 > Main.maxTilesX * 0.45 && num644 < Main.maxTilesX * 0.55)
                    {
                        num644 = WorldGen.genRand.Next(281, Main.maxTilesX - 3 - 280);
                    }
                    int num645 = WorldGen.genRand.Next((int)(Main.worldSurface * 2.0 + Main.rockLayer) / 3, (int)(Main.rockLayer + (Main.maxTilesY - 350) * 2) / 3);
                    if (WorldGen.remixWorldGen)
                    {
                        num645 = WorldGen.genRand.Next(100, (int)(Main.maxTilesY * 0.9));
                    }
                    while (WorldGen.oceanDepths(num644, num645) || Vector2D.Distance(new Vector2D(num644, num645), GenVars.shimmerPosition) < WorldGen.shimmerSafetyDistance)
                    {
                        num644 = WorldGen.genRand.Next(281, Main.maxTilesX - 3 - 280);
                        while (num644 > Main.maxTilesX * 0.45 && num644 < Main.maxTilesX * 0.55)
                        {
                            num644 = WorldGen.genRand.Next(281, Main.maxTilesX - 3 - 280);
                        }
                        num645 = WorldGen.genRand.Next((int)(Main.worldSurface * 2.0 + Main.rockLayer) / 3, (int)(Main.rockLayer + (Main.maxTilesY - 350) * 2) / 3);
                        if (WorldGen.remixWorldGen)
                        {
                            num645 = WorldGen.genRand.Next(100, (int)(Main.maxTilesY * 0.9));
                        }
                    }
                    int style2 = 0;
                    //if (WorldGen.drunkWorldGen)
                    //{
                    //    style2 = ((!GenVars.crimsonLeft) ? ((num644 >= Main.maxTilesX / 2) ? 1 : 0) : ((num644 < Main.maxTilesX / 2) ? 1 : 0));
                    //}
                    if (!WorldGen.IsTileNearby(num644, num645, ModContent.TileType<IckyAltar>(), 3))
                    {
                        WorldGen.Place3x2(num644, num645, (ushort)ModContent.TileType<IckyAltar>(), style2);
                    }
                    if (Main.tile[num644, num645].TileType == ModContent.TileType<IckyAltar>())
                    {
                        break;
                    }
                }
            }
        }
    }
}
