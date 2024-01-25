using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Avalon.Tiles.GemTrees;
using Terraria.ID;

namespace Avalon.WorldGeneration.Passes;

internal class GemStashes : GenPass
{
    public GemStashes() : base("Avalon Gem Stashes", 20f)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        for (int x = 20; x < Main.maxTilesX - 20; x++)
        {
            for (int y = (int)Main.rockLayer; y < Main.maxTilesY - 200; y++)
            {
                if (WorldGen.genRand.NextBool(90))
                {
                    if (Main.tile[x, y + 1].HasTile && Main.tile[x + 1, y + 1].HasTile &&
                        !Main.tile[x, y].HasTile && !Main.tile[x + 1, y].HasTile)
                    {
                        WorldGen.PlaceSmallPile(x, y, WorldGen.genRand.Next(3), 1, (ushort)ModContent.TileType<Tiles.GemStashes>());
                    }
                }
            }
        }
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
                if ((Main.tenthAnniversaryWorld || WorldGen.drunkWorldGen || WorldGen.genRand.NextBool(5)) && Main.tile[num19, num20 - 1].LiquidAmount == 0)
                {
                    int num21 = WorldGen.genRand.Next(3);
                    int treeTileType = 0;
                    switch (num21)
                    {
                        case 0:
                            treeTileType = ModContent.TileType<PeridotTree>();
                            break;
                        case 1:
                            treeTileType = ModContent.TileType<TourmalineTree>();
                            break;
                        case 2:
                            treeTileType = ModContent.TileType<ZirconTree>();
                            break;
                    }
                    TryGrowingAvalonGemTreeByType(treeTileType, num19, num20);
                }
            }
        }
    }

    public static bool TryGrowingAvalonGemTreeByType(int treeTileType, int checkedX, int checkedY)
    {
        bool result = false;
        if (treeTileType == ModContent.TileType<PeridotTree>())
        {
            result = PeridotSapling.GrowPeridotTreeWithSettings(checkedX, checkedY, PeridotSapling.GemTree_Peridot);
        }
        else if (treeTileType == ModContent.TileType<TourmalineTree>())
        {
            result = TourmalineSapling.GrowTourmalineTreeWithSettings(checkedX, checkedY, TourmalineSapling.GemTree_Tourmaline);
        }
        else if (treeTileType == ModContent.TileType<ZirconTree>())
        {
            result = ZirconSapling.GrowZirconTreeWithSettings(checkedX, checkedY, ZirconSapling.GemTree_Zircon);
        }
        return result;
    }
}
