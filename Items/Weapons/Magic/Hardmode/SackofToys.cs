using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class SackofToys : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponChanneled(ModContent.ProjectileType<Projectiles.Magic.SackofToys.SackofToys>(), 28, 1.5f, 11, 5f, 20);
		Item.autoReuse = true;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 10);
		Item.useStyle = ItemUseStyleID.Shoot;
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
