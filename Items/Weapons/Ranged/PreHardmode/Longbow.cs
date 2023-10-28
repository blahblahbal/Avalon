using Avalon.Projectiles.Ranged.Held;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

class Longbow : ModItem
{
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

        Item.damage = 50;
        Item.useAnimation = 55;
        Item.useTime = 55;
        Item.channel = true;
        Item.noUseGraphic = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LongbowHeld>(), damage, knockback, player.whoAmI, type);
        return false;
    }
}
