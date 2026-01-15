using Avalon.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Flails;

public class MoonfuryBlade : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 48;
		Projectile.height = 48;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.penetrate = 10;
		Projectile.alpha = 255;
		Projectile.friendly = true;
		Projectile.extraUpdates = 2;
		Projectile.timeLeft = 400;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 64) * Projectile.Opacity * ((float)Math.Sin(Projectile.ai[0] * 0.4f) * 0.25f + 0.75f);
	}
	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		Projectile.ai[0] += 0.2f;

		Projectile.scale = ((float)Math.Sin(Projectile.ai[0] * 0.4f) * 0.1f + 0.9f);

		Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha - 10, 0, 255);
		Projectile.velocity *= 0.99f;
		if (Projectile.timeLeft % 10 == 0)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.Shadowflame, Projectile.velocity, 128);
			d.fadeIn = 1.5f;
			d.noGravity = true;
			d.scale = 1.5f;
			Dust d2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.ShadowbeamStaff, Projectile.velocity, 128);
			d2.fadeIn = 1.5f;
			d2.noGravity = true;
			d2.scale = 1.5f;
		}
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 16;
		height = 16;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.ShadowFlame, TimeUtils.SecondsToTicks(2));
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.ShadowFlame, TimeUtils.SecondsToTicks(2));
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		for (int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.Shadowflame, Main.rand.NextVector2Circular(8, 8), 128);
			d.fadeIn = 1.5f;
			d.noGravity = true;
			d.scale = 1.5f;
			Dust d2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.ShadowbeamStaff, Main.rand.NextVector2Circular(8, 8), 128);
			d2.fadeIn = 1.5f;
			d2.noGravity = true;
			d2.scale = 1.5f;
		}
	}
}
