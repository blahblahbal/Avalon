using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Avalon.Hooks
{
    internal class Extractinator : ModHook
    {
        protected override void Apply()
        {
            On_ItemTrader.CreateChlorophyteExtractinator += OnChlorophyteExtractinator;
        }
        public static ItemTrader CreateAvalonChlorophyteExtractinator()
        {
            ItemTrader itemTrader = new ItemTrader();
            itemTrader.AddOption_CyclicLoop(ItemID.CopperOre, ItemID.TinOre, ModContent.ItemType<Items.Material.Ores.BronzeOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.IronOre, ItemID.LeadOre, ModContent.ItemType<Items.Material.Ores.NickelOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.SilverOre, ItemID.TungstenOre, ModContent.ItemType<Items.Material.Ores.ZincOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.GoldOre, ItemID.PlatinumOre, ModContent.ItemType<Items.Material.Ores.BismuthOre>());
            itemTrader.AddOption_CyclicLoop(ModContent.ItemType<Items.Material.Ores.RhodiumOre>(), ModContent.ItemType<Items.Material.Ores.OsmiumOre>(), ModContent.ItemType<Items.Material.Ores.IridiumOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.DemoniteOre, ItemID.CrimtaneOre, ModContent.ItemType<Items.Material.Ores.BacciliteOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.CobaltOre, ItemID.PalladiumOre); //, ModContent.ItemType<Items.Material.Ores.DurataniumOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.MythrilOre, ItemID.OrichalcumOre); //, ModContent.ItemType<Items.Material.Ores.NaquadahOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.AdamantiteOre, ItemID.TitaniumOre); //, ModContent.ItemType<Items.Material.Ores.TroxiniumOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.PinkBrick, ModContent.ItemType<Items.Placeable.Tile.OrangeBrick>(), ModContent.ItemType<Items.Placeable.Tile.YellowBrick>(), ItemID.GreenBrick,
                ItemID.BlueBrick, ModContent.ItemType<Items.Placeable.Tile.PurpleBrick>());
            itemTrader.AddOption_CyclicLoop(ItemID.CopperBar, ItemID.TinBar, ModContent.ItemType<Items.Material.Bars.BronzeBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.IronBar, ItemID.LeadBar, ModContent.ItemType<Items.Material.Bars.NickelBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.SilverBar, ItemID.TungstenBar, ModContent.ItemType<Items.Material.Bars.ZincBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.GoldBar, ItemID.PlatinumBar, ModContent.ItemType<Items.Material.Bars.BismuthBar>());
            itemTrader.AddOption_CyclicLoop(ModContent.ItemType<Items.Material.Bars.RhodiumBar>(), ModContent.ItemType<Items.Material.Bars.OsmiumBar>(), ModContent.ItemType<Items.Material.Bars.IridiumBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.DemoniteBar, ItemID.CrimtaneBar, ModContent.ItemType<Items.Material.Bars.BacciliteBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.CobaltBar, ItemID.PalladiumBar); //, ModContent.ItemType<Items.Material.Bars.DurataniumBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.MythrilBar, ItemID.OrichalcumBar); //, ModContent.ItemType<Items.Material.Bars.NaquadahBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.AdamantiteBar, ItemID.TitaniumBar); //, ModContent.ItemType<Items.Material.Bars.TroxiniumBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.ShadowScale, ItemID.TissueSample, ModContent.ItemType<Items.Material.Booger>());
            itemTrader.AddOption_FromAny(ItemID.StoneBlock, ItemID.EbonstoneBlock, ItemID.CrimstoneBlock, ModContent.ItemType<Items.Placeable.Tile.ChunkstoneBlock>());
            itemTrader.AddOption_FromAny(ItemID.SandBlock, ItemID.EbonsandBlock, ItemID.CrimsandBlock, ItemID.PearlsandBlock, ModContent.ItemType<Items.Placeable.Tile.SnotsandBlock>());
            itemTrader.AddOption_FromAny(ItemID.IceBlock, ItemID.PurpleIceBlock, ItemID.RedIceBlock, ItemID.PinkIceBlock, ModContent.ItemType<Items.Placeable.Tile.YellowIceBlock>());
            itemTrader.AddOption_FromAny(ItemID.Sandstone, ItemID.CorruptSandstone, ItemID.CrimsonSandstone, ItemID.HallowSandstone, ModContent.ItemType<Items.Placeable.Tile.SnotsandstoneBlock>());
            itemTrader.AddOption_FromAny(ItemID.HardenedSand, ItemID.CorruptHardenedSand, ItemID.CrimsonHardenedSand, ItemID.HallowHardenedSand, ModContent.ItemType<Items.Placeable.Tile.HardenedSnotsandBlock>());
            return itemTrader;
        }
        private static ItemTrader OnChlorophyteExtractinator(On_ItemTrader.orig_CreateChlorophyteExtractinator orig)
        {
            ItemTrader itemTrader = new ItemTrader();
            itemTrader.AddOption_CyclicLoop(ItemID.CopperOre, ItemID.TinOre, ModContent.ItemType<Items.Material.Ores.BronzeOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.IronOre, ItemID.LeadOre, ModContent.ItemType<Items.Material.Ores.NickelOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.SilverOre, ItemID.TungstenOre, ModContent.ItemType<Items.Material.Ores.ZincOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.GoldOre, ItemID.PlatinumOre, ModContent.ItemType<Items.Material.Ores.BismuthOre>());
            itemTrader.AddOption_CyclicLoop(ModContent.ItemType<Items.Material.Ores.RhodiumOre>(), ModContent.ItemType<Items.Material.Ores.OsmiumOre>(), ModContent.ItemType<Items.Material.Ores.IridiumOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.DemoniteOre, ItemID.CrimtaneOre, ModContent.ItemType<Items.Material.Ores.BacciliteOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.CobaltOre, ItemID.PalladiumOre); //, ModContent.ItemType<Items.Material.Ores.DurataniumOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.MythrilOre, ItemID.OrichalcumOre); //, ModContent.ItemType<Items.Material.Ores.NaquadahOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.AdamantiteOre, ItemID.TitaniumOre); //, ModContent.ItemType<Items.Material.Ores.TroxiniumOre>());
            itemTrader.AddOption_CyclicLoop(ItemID.PinkBrick, ModContent.ItemType<Items.Placeable.Tile.OrangeBrick>(), ModContent.ItemType<Items.Placeable.Tile.YellowBrick>(), ItemID.GreenBrick,
                ItemID.BlueBrick, ModContent.ItemType<Items.Placeable.Tile.PurpleBrick>());
            itemTrader.AddOption_CyclicLoop(ItemID.CopperBar, ItemID.TinBar, ModContent.ItemType<Items.Material.Bars.BronzeBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.IronBar, ItemID.LeadBar, ModContent.ItemType<Items.Material.Bars.NickelBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.SilverBar, ItemID.TungstenBar, ModContent.ItemType<Items.Material.Bars.ZincBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.GoldBar, ItemID.PlatinumBar, ModContent.ItemType<Items.Material.Bars.BismuthBar>());
            itemTrader.AddOption_CyclicLoop(ModContent.ItemType<Items.Material.Bars.RhodiumBar>(), ModContent.ItemType<Items.Material.Bars.OsmiumBar>(), ModContent.ItemType<Items.Material.Bars.IridiumBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.DemoniteBar, ItemID.CrimtaneBar, ModContent.ItemType<Items.Material.Bars.BacciliteBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.CobaltBar, ItemID.PalladiumBar); //, ModContent.ItemType<Items.Material.Bars.DurataniumBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.MythrilBar, ItemID.OrichalcumBar); //, ModContent.ItemType<Items.Material.Bars.NaquadahBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.AdamantiteBar, ItemID.TitaniumBar); //, ModContent.ItemType<Items.Material.Bars.TroxiniumBar>());
            itemTrader.AddOption_CyclicLoop(ItemID.ShadowScale, ItemID.TissueSample, ModContent.ItemType<Items.Material.Booger>());
            itemTrader.AddOption_FromAny(ItemID.StoneBlock, ItemID.EbonstoneBlock, ItemID.CrimstoneBlock, ModContent.ItemType<Items.Placeable.Tile.ChunkstoneBlock>());
            itemTrader.AddOption_FromAny(ItemID.SandBlock, ItemID.EbonsandBlock, ItemID.CrimsandBlock, ItemID.PearlsandBlock, ModContent.ItemType<Items.Placeable.Tile.SnotsandBlock>());
            itemTrader.AddOption_FromAny(ItemID.IceBlock, ItemID.PurpleIceBlock, ItemID.RedIceBlock, ItemID.PinkIceBlock, ModContent.ItemType<Items.Placeable.Tile.YellowIceBlock>());
            itemTrader.AddOption_FromAny(ItemID.Sandstone, ItemID.CorruptSandstone, ItemID.CrimsonSandstone, ItemID.HallowSandstone, ModContent.ItemType<Items.Placeable.Tile.SnotsandstoneBlock>());
            itemTrader.AddOption_FromAny(ItemID.HardenedSand, ItemID.CorruptHardenedSand, ItemID.CrimsonHardenedSand, ItemID.HallowHardenedSand, ModContent.ItemType<Items.Placeable.Tile.HardenedSnotsandBlock>());
            return itemTrader;
        }
    }
}
