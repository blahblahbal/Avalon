using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

class TomeoftheDistantPast : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 30;
        Item.autoReuse = true;
        Item.shootSpeed = 8f;
        Item.mana = 6;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.Bone1>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 27000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        switch (Main.rand.Next(4))
        {
            case 0:
                type = ModContent.ProjectileType<Projectiles.Magic.Bone1>();
                break;
            case 1:
                type = ModContent.ProjectileType<Projectiles.Magic.Bone2>();
                break;
            case 2:
                type = ModContent.ProjectileType<Projectiles.Magic.Bone3>();
                damage = (damage / 2) * 3;
                knockback *= 2;
                break;
            case 3:
                type = ModContent.ProjectileType<Projectiles.Magic.Bone4>();
                break;
        }
        velocity = velocity.RotatedByRandom(0.2f);
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(6, 2);
    }
}
