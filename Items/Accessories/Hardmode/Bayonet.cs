using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class Bayonet : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 42;
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
        Item.value = 100000;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<HiddenBlade>())
            .AddIngredient(ModContent.ItemType<AmmoMagazine>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[ModContent.BuffType<Buffs.Debuffs.BrokenWeaponry>()] = true;
        player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Unloaded>()] = true;
    }
}
