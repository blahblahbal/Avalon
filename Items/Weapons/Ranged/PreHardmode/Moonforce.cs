using Avalon.Items.Material.Bars;
using Avalon.Projectiles.Ranged.Held;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode; 

public class Moonforce : ModItem
{
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(200, 200, 200, 128);
    }
    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 32;
        Item.scale = 1f;
        Item.shootSpeed = 24f;
        Item.useAmmo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.knockBack = 2.3f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0, 0, 50);

        Item.damage = 63;
        Item.useAnimation = 77;
        Item.useTime = 77;
        Item.channel = true;
        Item.noUseGraphic = true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<MoonforceHeld>(), damage, knockback, player.whoAmI, type);
        return false;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RhodiumLongbow>())
            .AddIngredient(ModContent.ItemType<Longbone>())
            .AddIngredient(ModContent.ItemType<Longbow>())
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<OsmiumLongbow>())
            .AddIngredient(ModContent.ItemType<Longbone>())
            .AddIngredient(ModContent.ItemType<Longbow>())
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<IridiumLongbow>())
            .AddIngredient(ModContent.ItemType<Longbone>())
            .AddIngredient(ModContent.ItemType<Longbow>())
            .AddTile(TileID.DemonAltar)
            .Register();
    }
}
