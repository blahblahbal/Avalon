using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class MidasShoe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 28;
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
        Item.value = 100000;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<GoldenShield>())
            .AddIngredient(ModContent.ItemType<RubberBoot>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Ichor] = true;
		player.buffImmune[BuffID.Electrified] = true;
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Electrified>()] = true;
    }
}
