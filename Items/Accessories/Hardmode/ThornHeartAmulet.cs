using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class ThornHeartAmulet : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Chain)
            .AddIngredient(ItemID.LifeCrystal)
            .AddIngredient(ItemID.Stinger, 6)
            .AddIngredient(ItemID.SoulofNight, 8)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        float dmg = 2 * (float)Math.Floor((player.statLifeMax2 - (double)player.statLife) / player.statLifeMax2 * 10) / 50;
        if (dmg < 0) dmg = 0;
        player.GetDamage(DamageClass.Generic) += dmg;
    }
}
