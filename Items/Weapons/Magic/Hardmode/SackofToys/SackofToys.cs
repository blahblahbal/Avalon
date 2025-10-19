using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.SackofToys;

public class SackofToys : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponChanneled(ModContent.ProjectileType<SackofToysProj>(), 28, 1.5f, 11, 5f, 20);
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
					type = ModContent.ProjectileType<Lego>();
					break;
				case 1:
					type = ModContent.ProjectileType<Marble>();
					break;
			}
			Projectile.NewProjectile(source, position, velocity * 2, type, damage, knockback);
		}
		if (r == 1)
		{
			Projectile.NewProjectile(source, position, velocity * 3, ModContent.ProjectileType<Die>(), damage, knockback);
		}
		if (r == 2)
		{
			Projectile.NewProjectile(source, position, velocity * 1.5f, ModContent.ProjectileType<Vase>(), damage, knockback);
		}
		if (r == 3)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Doll>(), damage, knockback);
		}
		if (r == 4)
		{
			switch (Main.rand.Next(2))
			{
				case 0:
					type = ModContent.ProjectileType<Table>();
					break;
				case 1:
					type = ModContent.ProjectileType<RockingHorse>();
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
public class SackofToysProj : ModProjectile
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override LocalizedText DisplayName => ModContent.GetInstance<SackofToys>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
	}

	public override void AI()
	{
		if (Main.player[Projectile.owner].channel)
		{
			Projectile.timeLeft = 2;
		}
		Projectile.position = Main.player[Projectile.owner].position;
	}
}