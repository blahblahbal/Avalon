using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class HemorrhagingHalberd : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.damage = 48;
        Item.UseSound = SoundID.Item1;
        Item.noUseGraphic = true;
        Item.scale = 1f;
        Item.shootSpeed = 4f;
        Item.rare = ItemRarityID.LightRed;
        Item.noMelee = true;
        Item.useTime = 35;
        Item.useAnimation = 35;
        Item.knockBack = 4.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.HemorrhagingHalberd>();
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 3);
        Item.UseSound = SoundID.Item1;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float RotationAmount = 0.4f;
        Projectile.NewProjectile(source, position, velocity.RotatedBy(RotationAmount * -player.direction), type, damage / 2, knockback, player.whoAmI, 0, 0, RotationAmount * 2);
        return false;
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}
