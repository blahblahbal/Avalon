using Avalon.Items.Material.Bars;
using Avalon.Projectiles.Ranged.Held;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode;

public class StasisRifle : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 32;
        Item.scale = 1f;
        Item.shootSpeed = 24f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.knockBack = 2.3f;
        Item.shoot = ModContent.ProjectileType<StasisRifleHeld>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0, 7, 0);

        Item.damage = 82;
        Item.useAnimation = 84;
        Item.useTime = 84;
        Item.reuseDelay = 40;
        Item.channel = true;
        Item.noUseGraphic = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.AdamantiteBar, 20)
            .AddIngredient(ItemID.FrostCore, 2)
            .AddIngredient(ItemID.IceBlock, 20)
            .AddIngredient(ItemID.Shotgun, 20)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
