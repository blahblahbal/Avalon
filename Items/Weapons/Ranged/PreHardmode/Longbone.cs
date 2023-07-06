using Avalon.Projectiles.Ranged.Held;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

class Longbone : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 32;
        Item.scale = 1f;
        Item.shootSpeed = 30f;
        Item.useAmmo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.knockBack = 2.3f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0, 0, 50);

        Item.damage = 33;
        Item.useAnimation = 83;
        Item.useTime = 83;
        Item.channel = true;
        Item.noUseGraphic = true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LongboneHeld>(), damage, knockback, player.whoAmI, type);
        return false;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Bone, 24)
            .AddIngredient(ItemID.Cobweb,25)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
