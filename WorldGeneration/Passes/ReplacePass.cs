using Avalon.Common;
using Avalon.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes;

internal class ReplacePass : GenPass
{
    public ReplacePass(string name, float loadWeight) : base(name, loadWeight)
    {
    }
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        for (int i = 10; i < Main.maxTilesX - 10; i++)
        {
            for (int j = 150; j < Main.maxTilesY - 150; j++)
            {
                // copper tier
                if (Main.tile[i, j].TileType == TileID.Copper || Main.tile[i, j].TileType == TileID.Tin || Main.tile[i, j].TileType == ModContent.TileType<BronzeOre>())
                {
                    if (WorldGen.SavedOreTiers.Copper == TileID.Copper && Main.tile[i, j].TileType != TileID.Copper)
                    {
                        Main.tile[i, j].TileType = TileID.Copper;
                    }
                    else if (WorldGen.SavedOreTiers.Copper == TileID.Tin && Main.tile[i, j].TileType != TileID.Tin)
                    {
                        Main.tile[i, j].TileType = TileID.Tin;
                    }
                    else if (WorldGen.SavedOreTiers.Copper == ModContent.TileType<BronzeOre>() && Main.tile[i, j].TileType != ModContent.TileType<BronzeOre>())
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<BronzeOre>();
                    }
                }
                // iron tier
                if (Main.tile[i, j].TileType == TileID.Iron || Main.tile[i, j].TileType == TileID.Lead || Main.tile[i, j].TileType == ModContent.TileType<NickelOre>())
                {
                    if (WorldGen.SavedOreTiers.Iron == TileID.Iron && Main.tile[i, j].TileType != TileID.Iron)
                    {
                        Main.tile[i, j].TileType = TileID.Iron;
                    }
                    else if (WorldGen.SavedOreTiers.Iron == TileID.Lead && Main.tile[i, j].TileType != TileID.Lead)
                    {
                        Main.tile[i, j].TileType = TileID.Lead;
                    }
                    else if (WorldGen.SavedOreTiers.Iron == ModContent.TileType<NickelOre>() && Main.tile[i, j].TileType != ModContent.TileType<NickelOre>())
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<NickelOre>();
                    }
                }
                // silver tier
                if (Main.tile[i, j].TileType == TileID.Silver || Main.tile[i, j].TileType == TileID.Tungsten || Main.tile[i, j].TileType == ModContent.TileType<ZincOre>())
                {
                    if (WorldGen.SavedOreTiers.Silver == TileID.Silver && Main.tile[i, j].TileType != TileID.Silver)
                    {
                        Main.tile[i, j].TileType = TileID.Silver;
                    }
                    else if (WorldGen.SavedOreTiers.Silver == TileID.Tungsten && Main.tile[i, j].TileType != TileID.Tungsten)
                    {
                        Main.tile[i, j].TileType = TileID.Tungsten;
                    }
                    else if (WorldGen.SavedOreTiers.Silver == ModContent.TileType<ZincOre>() && Main.tile[i, j].TileType != ModContent.TileType<ZincOre>())
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<ZincOre>();
                    }
                }
                // gold tier
                if (Main.tile[i, j].TileType == TileID.Gold || Main.tile[i, j].TileType == TileID.Platinum || Main.tile[i, j].TileType == ModContent.TileType<BismuthOre>())
                {
                    if (WorldGen.SavedOreTiers.Gold == TileID.Gold && Main.tile[i, j].TileType != TileID.Gold)
                    {
                        Main.tile[i, j].TileType = TileID.Gold;
                    }
                    else if (WorldGen.SavedOreTiers.Gold == TileID.Platinum && Main.tile[i, j].TileType != TileID.Platinum)
                    {
                        Main.tile[i, j].TileType = TileID.Platinum;
                    }
                    else if (WorldGen.SavedOreTiers.Gold == ModContent.TileType<BismuthOre>() && Main.tile[i, j].TileType != ModContent.TileType<BismuthOre>())
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<BismuthOre>();
                    }
                }
            }
        }
    }
}
