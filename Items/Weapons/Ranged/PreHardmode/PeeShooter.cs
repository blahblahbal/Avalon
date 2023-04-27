using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

class PeeShooter : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item11;
        Item.damage = 8;
        Item.scale = 1f;
        Item.useAmmo = AmmoID.Bullet;
        Item.shootSpeed = 7f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTime = 13;
        Item.shoot = ProjectileID.Bullet;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 50000;
        Item.useAnimation = 13;
        Item.height = dims.Height;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(4, 0);
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.Bullet)
        {
            type = ModContent.ProjectileType<Projectiles.Ranged.PeeBullet>();
        }
    }
}
