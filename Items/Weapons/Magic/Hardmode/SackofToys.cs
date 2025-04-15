using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Avalon.Items.Material;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class SackofToys : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 28;
        Item.autoReuse = true;
        Item.scale = 1f;
        Item.shootSpeed = 5f;
        Item.mana = 11;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.knockBack = 1.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.SackofToys.SackofToys>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 500000;
        Item.channel = true;
        Item.noUseGraphic = true;
        Item.useAnimation = 20;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int r = Main.rand.Next(5);
        if (r == 0)
        {
            switch (Main.rand.Next(2))
            {
                case 0:
                    type = ModContent.ProjectileType<Projectiles.Magic.SackofToys.Lego>();
                    break;
                case 1:
                    type = ModContent.ProjectileType<Projectiles.Magic.SackofToys.Marble>();
                    break;
            }
            Projectile.NewProjectile(source, position, velocity * 2, type, damage, knockback);
        }
        if (r == 1)
        {
            Projectile.NewProjectile(source, position, velocity * 3, ModContent.ProjectileType<Projectiles.Magic.SackofToys.Die>(), damage, knockback);
        }
        if (r == 2)
        {
            Projectile.NewProjectile(source, position, velocity * 1.5f, ModContent.ProjectileType<Projectiles.Magic.SackofToys.Vase>(), damage, knockback);
        }
        if (r == 3)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Magic.SackofToys.Doll>(), damage, knockback);
        }
        if (r == 4)
        {
            switch (Main.rand.Next(2))
            {
                case 0:
                    type = ModContent.ProjectileType<Projectiles.Magic.SackofToys.Table>();
                    break;
                case 1:
                    type = ModContent.ProjectileType<Projectiles.Magic.SackofToys.RockingHorse>();
                    break;
            }
            Projectile.NewProjectile(source, position, velocity * 0.4f, type, damage, knockback);
        }
        if (r == 5)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileID.Grenade, damage, knockback);
        }
        return true;
    }
}
