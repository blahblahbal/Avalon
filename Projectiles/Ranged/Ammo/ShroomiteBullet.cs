using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Buffs.Debuffs;

namespace Avalon.Projectiles.Ranged.Ammo;

public class ShroomiteBullet : ModProjectile
{
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = dims.Width * 4 / 20;
		Projectile.height = dims.Height * 4 / 20 / Main.projFrames[Projectile.type];
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.alpha = 0;
		Projectile.scale = 1.2f;
		Projectile.timeLeft = 1200;
		Projectile.tileCollide = true;
		Projectile.DamageType = DamageClass.Ranged;
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ShroomiteFullbright>(), 60 * 10);
	}
	public override void AI()
	{
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}
}
