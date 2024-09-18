using Avalon.Common;
using Avalon.Tiles;
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
                if (ModContent.GetInstance<AvalonWorld>().WorldJungle == Enums.WorldJungle.Tropics)
                {
                    if (Main.tile[i, j].TileType == TileID.Platforms && Main.tile[i, j].TileFrameY / 18 == 2)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyPlatform>();
                        Main.tile[i, j].TileFrameY -= 18 * 2;
                    }
                    if (Main.tile[i, j].TileType == TileID.ClosedDoor && Main.tile[i, j].TileFrameY / 54 == 2)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyDoorClosed>();
                        Main.tile[i, j].TileFrameY -= 54 * 2;
                    }
                    if (Main.tile[i, j].TileType == TileID.Tables && Main.tile[i, j].TileFrameX / 54 == 2)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyTable>();
                        Main.tile[i, j].TileFrameX -= 54 * 2;
                    }
                    if (Main.tile[i, j].TileType == TileID.WorkBenches && Main.tile[i, j].TileFrameX / 36 == 2)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyWorkBench>();
                        Main.tile[i, j].TileFrameX -= 36 * 2;
                    }
                    if (Main.tile[i, j].TileType == TileID.Pianos && Main.tile[i, j].TileFrameX / 54 == 2)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyPiano>();
                        Main.tile[i, j].TileFrameX -= 54 * 2;
                    }
                    if (Main.tile[i, j].TileType == TileID.Bookcases && Main.tile[i, j].TileFrameX / 54 == 12)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyBookcase>();
                        Main.tile[i, j].TileFrameX -= 54 * 12;
                    }
                    if (Main.tile[i, j].TileType == TileID.Chairs && Main.tile[i, j].TileFrameY / 40 == 3)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyChair>();
                        Main.tile[i, j].TileFrameY -= 40 * 3;
                    }
                    if (Main.tile[i, j].TileType == TileID.Containers && Main.tile[i, j].TileFrameX / 36 == 8)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyChest>();
                        Main.tile[i, j].TileFrameX -= 8 * 36;
                    }
                }
                

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
				// poison spike unsmoothing
				if (Main.tile[i, j].TileType == ModContent.TileType<PoisonSpike>())
				{
					Tile aklsd = Main.tile[i, j];
					aklsd.Slope = SlopeType.Solid;
					aklsd.IsHalfBlock = false;
				}
            }
        }
    }
}
