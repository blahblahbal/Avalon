using Terraria;
using Terraria.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Avalon.Tiles.Contagion;

namespace Avalon.WorldGeneration.Passes
{
    internal class AvalonStalac : GenPass
    {
        public AvalonStalac() : base("Avalon Stalac", 20f)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            for (int num19 = 20; num19 < Main.maxTilesX - 20; num19++)
            {
                for (int num22 = 5; num22 < Main.maxTilesY - 20; num22++)
                {
                    // contagion stalac
                    if (Main.tile[num19, num22 - 1].TileType == ModContent.TileType<Chunkstone>() && Main.tile[num19, num22 - 1].HasTile && WorldGen.genRand.NextBool(5))
                    {
                        if (!Main.tile[num19, num22].HasTile && !Main.tile[num19, num22 + 1].HasTile && Main.tile[num19, num22 - 1].Slope == SlopeType.Solid)
                        {
                            Utils.PlaceCustomTight(num19, num22, (ushort)ModContent.TileType<ContagionStalactgmites>());
                        }
                    }
                    if (Main.tile[num19, num22 + 1].TileType == ModContent.TileType<Chunkstone>() && Main.tile[num19, num22 + 1].HasTile && WorldGen.genRand.NextBool(5))
                    {
                        if (!Main.tile[num19, num22].HasTile && !Main.tile[num19, num22 - 1].HasTile && Main.tile[num19, num22 + 1].Slope == SlopeType.Solid)
                        {
                            Utils.PlaceCustomTight(num19, num22, (ushort)ModContent.TileType<ContagionStalactgmites>());
                        }
                    }
                    // blasted stone stalac
                    if (Main.tile[num19, num22 - 1].TileType == ModContent.TileType<Tiles.BlastedStone>() && Main.tile[num19, num22 - 1].HasTile && WorldGen.genRand.NextBool(5))
                    {
                        if (!Main.tile[num19, num22].HasTile && !Main.tile[num19, num22 + 1].HasTile && Main.tile[num19, num22 - 1].Slope == SlopeType.Solid)
                        {
                            Utils.PlaceCustomTight(num19, num22, (ushort)ModContent.TileType<Tiles.BlastedStalac>());
                        }
                    }
                    if (Main.tile[num19, num22 + 1].TileType == ModContent.TileType<Tiles.BlastedStone>() && Main.tile[num19, num22 + 1].HasTile && WorldGen.genRand.NextBool(5))
                    {
                        if (!Main.tile[num19, num22].HasTile && !Main.tile[num19, num22 - 1].HasTile && Main.tile[num19, num22 + 1].Slope == SlopeType.Solid)
                        {
                            Utils.PlaceCustomTight(num19, num22, (ushort)ModContent.TileType<Tiles.BlastedStalac>());
                        }
                    }
                }
            }
        }
    }
}
