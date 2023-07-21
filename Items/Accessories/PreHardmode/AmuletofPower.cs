using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Neck)]
class AmuletofPower : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 90);
        Item.height = dims.Height;
        Item.defense = 3;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += 0.07f;
        player.GetCritChance(DamageClass.Generic) += 5;
        player.statManaMax2 += 40;
        player.statLifeMax2 += 40;
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.05f);
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<AmethystAmulet>())
            .AddIngredient(ModContent.ItemType<TopazAmulet>())
            .AddIngredient(ModContent.ItemType<SapphireAmulet>())
            .AddIngredient(ModContent.ItemType<EmeraldAmulet>())
            .AddIngredient(ModContent.ItemType<RubyAmulet>())
            .AddIngredient(ModContent.ItemType<DiamondAmulet>())
            .AddIngredient(ModContent.ItemType<TourmalineAmulet>())
            .AddIngredient(ModContent.ItemType<PeridotAmulet>())
            .AddIngredient(ModContent.ItemType<ZirconAmulet>())
            .AddTile(TileID.Anvils)
            .Register();
    }
}
