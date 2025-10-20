using Avalon.Data.Sets;
using Avalon.Tiles.Contagion.ContagionGrasses;
using Avalon.Tiles.Contagion.SmallPlants;
using Avalon.Walls;
using Terraria;
using Terraria.ID;
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
            int grass = ModContent.TileType<ContagionShortGrass>();
            for (int num244 = 0; num244 < Main.maxTilesX; num244++)
            {
                progress.Set((double)num244 / Main.maxTilesX);
                for (int num245 = 1; num245 < Main.maxTilesY; num245++)
                {
                    if (Main.tile[num244, num245].TileType == ModContent.TileType<Ickgrass>() && Main.tile[num244, num245].HasUnactuatedTile)
                    {
                        if (!Main.tile[num244, num245 - 1].HasTile && !Main.tile[num244, num245].IsHalfBlock && Main.tile[num244, num245].Slope == SlopeType.Solid)
                        {
                            Terraria.Tile t = Main.tile[num244, num245 - 1];
                            t.TileType = (ushort)grass;
                            t.HasTile = true;
                            if (WorldGen.genRand.NextBool(50) || WorldGen.genRand.NextBool(40))
                            {
                                t.TileFrameX = 8 * 18;
                            }
                            else if (WorldGen.genRand.NextBool(35) || (Main.tile[num244, num245 - 1].WallType == ModContent.WallType<ContagionGrassWall>()))
                            {
                                int style = WorldGen.genRand.NextFromList(6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22);
                                t.TileFrameX = (short)(style * 18);
                            }
                            else
                            {
                                t.TileFrameX = (short)(WorldGen.genRand.Next(6) * 18);
                            }
                        }
                    }
                }
            }
        }
    }
}
