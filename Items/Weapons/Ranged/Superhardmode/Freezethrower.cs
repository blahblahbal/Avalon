using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Superhardmode;

public class Freezethrower : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 18;
        Item.UseSound = SoundID.Item34;
        Item.damage = 70;
        Item.autoReuse = true;
        Item.useAmmo = AmmoID.Gel;
        Item.shootSpeed = 10.5f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.useTime = 5;
        Item.knockBack = 0.625f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.Freezethrower>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 1000000;
        Item.useAnimation = 30;
        Item.ArmorPenetration = 30;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float f = 0.05f;
        if(Main.myPlayer == player.whoAmI)
        Projectile.NewProjectile(source, position, Vector2.SmoothStep(velocity.RotatedBy(-f), velocity.RotatedBy(f), Main.masterColor), type, damage, knockback, player.whoAmI);
        return false;
    }
    public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

    public override bool CanConsumeAmmo(Item ammo, Player player) =>
        player.itemAnimation >= player.HeldItem.useAnimation - 3;
}
