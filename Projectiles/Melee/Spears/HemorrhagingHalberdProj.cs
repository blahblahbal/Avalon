using Avalon.Common.Templates;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Spears;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Spears;
public class HemorrhagingHalberdProj : SpearTemplate2
{
	public override LocalizedText DisplayName => ModContent.GetInstance<HemorrhagingHalberd>().DisplayName;
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
	}
	protected override float HoldoutRangeMax => 140;
	protected override float HoldoutRangeMin => 40;
	public override void PostAI()
	{
		Projectile.ai[1]++;
		Player player = Main.player[Projectile.owner];
		Projectile.position.Y += 10 * player.gravDir;
		Projectile.position.X += (Math.Abs(Projectile.velocity.Y - 1)) * 5 * player.direction;
		float duration = player.itemAnimationMax;

		Projectile.velocity = Projectile.velocity.RotatedBy(player.direction * (1 / duration) * Projectile.ai[2]);

		if (Main.rand.NextBool(3))
		{
			Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1.4f);
			d2.noGravity = true;
			d2.fadeIn = 1.5f;
			d2.velocity += Projectile.velocity * 3;
		}
		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<ContagionWeapons>(), 0, 0, 128);
		d.noGravity = true;
		d.velocity += Projectile.velocity * 3;
		if (Projectile.ai[1] % 4 == 0 && Main.myPlayer == Projectile.owner)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 3, ModContent.ProjectileType<PathogenSmoke>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
		}
	}
}
