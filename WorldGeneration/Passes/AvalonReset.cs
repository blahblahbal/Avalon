using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes; 

public class AvalonReset : GenPass
{
    public AvalonReset(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        int copper = WorldGen.genRand.Next(3);
        switch (copper)
        {
            case 0:
                WorldGen.SavedOreTiers.Copper = TileID.Copper;
                GenVars.copper = TileID.Copper;
                GenVars.copperBar = ItemID.CopperBar;
                break;
            case 1:
                WorldGen.SavedOreTiers.Copper = TileID.Tin;
                GenVars.copper = TileID.Tin;
                GenVars.copperBar = ItemID.TinBar;
                break;
            case 2:
                WorldGen.SavedOreTiers.Copper = ModContent.TileType<Tiles.Ores.BronzeOre>();
                GenVars.copper = ModContent.TileType<Tiles.Ores.BronzeOre>();
                GenVars.copperBar = ModContent.ItemType<Items.Material.Bars.BronzeBar>();
                break;
        }
        int iron = WorldGen.genRand.Next(3);
        switch (iron)
        {
            case 0:
                WorldGen.SavedOreTiers.Iron = TileID.Iron;
                GenVars.iron = TileID.Iron;
                GenVars.ironBar = ItemID.IronBar;
                break;
            case 1:
                WorldGen.SavedOreTiers.Iron = TileID.Lead;
                GenVars.iron = TileID.Lead;
                GenVars.ironBar = ItemID.LeadBar;
                break;
            case 2:
                WorldGen.SavedOreTiers.Iron = ModContent.TileType<Tiles.Ores.NickelOre>();
                GenVars.iron = ModContent.TileType<Tiles.Ores.NickelOre>();
                GenVars.ironBar = ModContent.ItemType<Items.Material.Bars.NickelBar>();
                break;
        }
        int silver = WorldGen.genRand.Next(3);
        switch (silver)
        {
            case 0:
                WorldGen.SavedOreTiers.Silver = TileID.Silver;
                GenVars.silver = TileID.Silver;
                GenVars.silverBar = ItemID.SilverBar;
                break;
            case 1:
                WorldGen.SavedOreTiers.Silver = TileID.Tungsten;
                GenVars.silver = TileID.Tungsten;
                GenVars.silverBar = ItemID.TungstenBar;
                break;
            case 2:
                WorldGen.SavedOreTiers.Silver = ModContent.TileType<Tiles.Ores.ZincOre>();
                GenVars.silver = ModContent.TileType<Tiles.Ores.ZincOre>();
                GenVars.silverBar = ModContent.ItemType<Items.Material.Bars.ZincBar>();
                break;
        }
        int gold = WorldGen.genRand.Next(3);
        switch (gold)
        {
            case 0:
                WorldGen.SavedOreTiers.Gold = TileID.Gold;
                GenVars.gold = TileID.Gold;
                GenVars.goldBar = ItemID.GoldBar;
                break;
            case 1:
                WorldGen.SavedOreTiers.Gold = TileID.Platinum;
                GenVars.gold = TileID.Platinum;
                GenVars.goldBar = ItemID.PlatinumBar;
                break;
            case 2:
                WorldGen.SavedOreTiers.Gold = ModContent.TileType<Tiles.Ores.BismuthOre>();
                GenVars.gold = ModContent.TileType<Tiles.Ores.BismuthOre>();
                GenVars.goldBar = ModContent.ItemType<Items.Material.Bars.BismuthBar>();
                break;
        }
        int rhodium = WorldGen.genRand.Next(3);
        switch (rhodium)
        {
            case 0:
                AvalonWorld.RhodiumOre = AvalonWorld.RhodiumVariant.Rhodium;
                break;
            case 1:
                AvalonWorld.RhodiumOre = AvalonWorld.RhodiumVariant.Osmium;
                break;
            case 2:
                AvalonWorld.RhodiumOre = AvalonWorld.RhodiumVariant.Iridium;
                break;
        }
    }
}
