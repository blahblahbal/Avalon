using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Weapons.Ranged.Superhardmode;

internal class Freezethrower : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

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

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        velocity = Vector2.SmoothStep(velocity.RotatedBy(-0.1f), velocity.RotatedBy(0.1f),Main.masterColor);
        position += new Vector2(6,0).RotatedBy(velocity.ToRotation());
    }
    public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

    public override bool CanConsumeAmmo(Item ammo, Player player) =>
        player.itemAnimation >= player.HeldItem.useAnimation - 3;
}
