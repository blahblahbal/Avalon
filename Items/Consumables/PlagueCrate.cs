using Avalon.Items.Weapons.Magic.PreHardmode.Smogscreen;
using Avalon.Items.Weapons.Melee.Boomerangs;
using Avalon.Items.Weapons.Ranged.Guns;
using Avalon.Tiles.Furniture;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class PlagueCrate : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.IsFishingCrate[Type] = true;
        ItemID.Sets.IsFishingCrateHardmode[Type] = true;
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Crates>());
        Item.placeStyle = 1;
        Item.width = 12; //The hitbox dimensions are intentionally smaller so that it looks nicer when fished up on a bobber
        Item.height = 12;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 1);
    }

    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Crates;
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        int[] themedDrops = new int[] {
            ModContent.ItemType<Accessories.PreHardmode.NerveNumbNecklace>(),
            ModContent.ItemType<Blunderblight>(),
            ModContent.ItemType<Smogscreen>(),
            ModContent.ItemType<Pets.SepticCell>(),
            ModContent.ItemType<TetanusChakram>(),
        };
        itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, themedDrops));

        // Drop coins
        itemLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 4, 5, 12));

        // Drop pre-hm ores, with the addition of the Avalon ones
        IItemDropRule[] oreTypes = new IItemDropRule[] {
            ItemDropRule.Common(ItemID.CopperOre, 1, 20, 35),
            ItemDropRule.Common(ItemID.TinOre, 1, 20, 35),
            ItemDropRule.Common(ModContent.ItemType<Material.Ores.BronzeOre>(), 1, 20, 35),
            ItemDropRule.Common(ItemID.IronOre, 1, 20, 35),
            ItemDropRule.Common(ItemID.LeadOre, 1, 20, 35),
            ItemDropRule.Common(ModContent.ItemType<Material.Ores.NickelOre>(), 1, 20, 35),
            ItemDropRule.Common(ItemID.SilverOre, 1, 20, 35),
            ItemDropRule.Common(ItemID.TungstenOre, 1, 20, 35),
            ItemDropRule.Common(ModContent.ItemType<Material.Ores.ZincOre>(), 1, 20, 35),
            ItemDropRule.Common(ItemID.GoldOre, 1, 20, 35),
            ItemDropRule.Common(ItemID.PlatinumOre, 1, 20, 35),
            ItemDropRule.Common(ModContent.ItemType<Material.Ores.BismuthOre>(), 1, 20, 35),
        };
        itemLoot.Add(new OneFromRulesRule(14, oreTypes));

        // Drop HM ores, with the addition of the Avalon ones
        IItemDropRule[] HMOreTypes = new IItemDropRule[] {
            ItemDropRule.Common(ItemID.CobaltOre, 1, 20, 35),
            ItemDropRule.Common(ItemID.PalladiumOre, 1, 20, 35),
            ItemDropRule.Common(ModContent.ItemType<Material.Ores.DurataniumOre>(), 1, 20, 35),
            ItemDropRule.Common(ItemID.MythrilOre, 1, 20, 35),
            ItemDropRule.Common(ItemID.OrichalcumOre, 1, 20, 35),
            ItemDropRule.Common(ModContent.ItemType<Material.Ores.NaquadahOre>(), 1, 20, 35),
            ItemDropRule.Common(ItemID.AdamantiteOre, 1, 20, 35),
            ItemDropRule.Common(ItemID.TitaniumOre, 1, 20, 35),
            ItemDropRule.Common(ModContent.ItemType<Material.Ores.TroxiniumOre>(), 1, 20, 35),
        };
        itemLoot.Add(new OneFromRulesRule(14, HMOreTypes));

        // Drop pre-hm bars (except copper/tin), with the addition of the Avalon ones
        IItemDropRule[] oreBars = new IItemDropRule[] {
            ItemDropRule.Common(ItemID.IronBar, 1, 6, 16),
            ItemDropRule.Common(ItemID.LeadBar, 1, 6, 16),
            ItemDropRule.Common(ModContent.ItemType<Material.Bars.NickelBar>(), 1, 6, 16),
            ItemDropRule.Common(ItemID.SilverBar, 1, 6, 16),
            ItemDropRule.Common(ItemID.TungstenBar, 1, 6, 16),
            ItemDropRule.Common(ModContent.ItemType<Material.Bars.ZincBar>(), 1, 6, 16),
            ItemDropRule.Common(ItemID.GoldBar, 1, 6, 16),
            ItemDropRule.Common(ItemID.PlatinumBar, 1, 6, 16),
            ItemDropRule.Common(ModContent.ItemType<Material.Bars.BismuthBar>(), 1, 6, 16),
        };
        itemLoot.Add(new OneFromRulesRule(12, oreBars));

        // Drop pre-hm bars (except copper/tin), with the addition of the Avalon ones
        IItemDropRule[] HMOreBars = new IItemDropRule[] {
            ItemDropRule.Common(ItemID.CobaltBar, 1, 5, 16),
            ItemDropRule.Common(ItemID.PalladiumBar, 1, 5, 16),
            ItemDropRule.Common(ModContent.ItemType<Material.Bars.DurataniumBar>(), 1, 5, 16),
            ItemDropRule.Common(ItemID.MythrilBar, 1, 5, 16),
            ItemDropRule.Common(ItemID.OrichalcumBar, 1, 5, 16),
            ItemDropRule.Common(ModContent.ItemType<Material.Bars.NaquadahBar>(), 1, 5, 16),
            ItemDropRule.Common(ItemID.AdamantiteBar, 1, 5, 16),
            ItemDropRule.Common(ItemID.TitaniumBar, 1, 5, 16),
            ItemDropRule.Common(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 1, 5, 16),
        };
        itemLoot.Add(new OneFromRulesRule(6, HMOreBars));

        // Drop an "exploration utility" potion, with the addition of the Avalon ones
        IItemDropRule[] explorationPotions = new IItemDropRule[] {
            ItemDropRule.Common(ItemID.ObsidianSkinPotion, 1, 2, 4),
            ItemDropRule.Common(ItemID.SpelunkerPotion, 1, 2, 4),
            ItemDropRule.Common(ItemID.HunterPotion, 1, 2, 4),
            ItemDropRule.Common(ItemID.GravitationPotion, 1, 2, 4),
            ItemDropRule.Common(ItemID.MiningPotion, 1, 2, 4),
            ItemDropRule.Common(ItemID.HeartreachPotion, 1, 2, 4),
        };
        itemLoot.Add(new OneFromRulesRule(4, explorationPotions));

        // Drop (pre-hm) resource potion
        IItemDropRule[] resourcePotions = new IItemDropRule[] {
            ItemDropRule.Common(ItemID.HealingPotion, 1, 5, 17),
            ItemDropRule.Common(ItemID.ManaPotion, 1, 5, 17),
            ItemDropRule.Common(ModContent.ItemType<Potions.Other.StaminaPotion>(), 1, 5, 17),
        };
        itemLoot.Add(new OneFromRulesRule(2, resourcePotions));

        // Drop (high-end) bait
        IItemDropRule[] highendBait = new IItemDropRule[] {
            ItemDropRule.Common(ItemID.JourneymanBait, 1, 2, 6),
            ItemDropRule.Common(ItemID.MasterBait, 1, 2, 6),
        };
        itemLoot.Add(new OneFromRulesRule(2, highendBait));

        IItemDropRule[] oof1 = new IItemDropRule[] {
            ItemDropRule.Common(ItemID.SoulofNight, 1, 2, 5)
        };
        itemLoot.Add(new OneFromRulesRule(2, oof1));

        IItemDropRule[] oof2 = new IItemDropRule[] {
            ItemDropRule.Common(ModContent.ItemType<Material.Pathogen>(), 1, 2, 5)
        };
        itemLoot.Add(new OneFromRulesRule(2, oof2));
    }
}
