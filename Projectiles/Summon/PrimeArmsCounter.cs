using Avalon.Common;
using Avalon.Common.Players;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;
public class PrimeArmsCounter : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		Main.projPet[Projectile.type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.netImportant = true;
		Projectile.width = 10;
		Projectile.height = 10;
		Projectile.penetrate = -1;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.friendly = true;
		Projectile.minion = true;
		Projectile.minionSlots = 1f;
		Projectile.timeLeft = 60;
		Projectile.aiStyle = -1;
		Projectile.hide = true;
	}
	private static List<int> _blacklistedTargets = new List<int>();
	public override void AI()
	{
		Player owner = Main.player[Projectile.owner];
		AvalonGlobalProjectile.UpgradeableMinionCounterAI(Projectile, owner, ModContent.BuffType<Buffs.Minions.PrimeArms>(), ref owner.GetModPlayer<AvalonPlayer>().PrimeMinion);
	}
	public static void ModifyPrimeMinionStats(Projectile projectile, Player owner)
	{
		int damageMod = 3;
		float origScale = 1f;
		float scaleMod = 0.1f;

		int ownedCounts = owner.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()];

		projectile.originalDamage = owner.GetModPlayer<AvalonPlayer>().highestPrimeCounterOriginalDamage;
		projectile.originalDamage += Math.Max(0, ownedCounts - 1) * damageMod;
		projectile.originalDamage = (int)owner.GetTotalDamage(DamageClass.Summon).ApplyTo(projectile.originalDamage);

		projectile.knockBack = owner.GetModPlayer<AvalonPlayer>().primeMinionKnockback;

		projectile.scale = origScale;
		projectile.scale += scaleMod * ownedCounts;

		if (ownedCounts > 7)
		{
			projectile.frame = 2;
			projectile.scale = origScale;
			if (ownedCounts < 10)
			{
				projectile.scale += scaleMod * (ownedCounts - 7);
			}
			else
			{
				projectile.scale += scaleMod * 2;
			}
		}
		else if (ownedCounts > 4)
		{
			projectile.frame = 1;
			projectile.scale = origScale;
			projectile.scale += scaleMod * (ownedCounts - 4);
		}
	}
}
public class UpgradeableMinionHook : ModHook
{
	protected override void Apply()
	{
		On_Player.UpdateProjectileCaches += On_Player_UpdateProjectileCaches;
		On_Player.ResetProjectileCaches += On_Player_ResetProjectileCaches;
	}

	private void On_Player_UpdateProjectileCaches(On_Player.orig_UpdateProjectileCaches orig, Player self, int i)
	{
		orig(self, i);
		foreach (var proj in Main.ActiveProjectiles)
		{
			if (proj.owner != i)
			{
				continue;
			}
			if (proj.type == ModContent.ProjectileType<PrimeArmsCounter>())
			{
				int originalDamage = proj.originalDamage;
				if (self.GetModPlayer<AvalonPlayer>().highestPrimeCounterOriginalDamage < originalDamage)
				{
					self.GetModPlayer<AvalonPlayer>().highestPrimeCounterOriginalDamage = originalDamage;
				}
				self.GetModPlayer<AvalonPlayer>().primeMinionKnockback = proj.knockBack;
				break;
			}
		}
	}
	private void On_Player_ResetProjectileCaches(On_Player.orig_ResetProjectileCaches orig, Player self)
	{
		self.GetModPlayer<AvalonPlayer>().highestPrimeCounterOriginalDamage = 0;
		self.GetModPlayer<AvalonPlayer>().primeMinionKnockback = 0f;
		orig(self);
	}
}
