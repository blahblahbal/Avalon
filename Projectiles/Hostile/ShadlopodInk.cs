using Avalon.Common;
using Avalon.NPCs.Corruption;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class ShadlopodInk : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.BulletDeadeye);
		Projectile.hide = true;
		Projectile.aiStyle = -1;
		Projectile.Size = new Vector2(8);
		Projectile.extraUpdates = 2;
		Projectile.light = 0;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 300;
	}
	public override void AI()
	{
		//if (!Main.player[(int)Projectile.ai[0]].dead)
		//{
		//    float MaxVelocity = 0.05f;
		//    Projectile.velocity.X += MathHelper.Clamp(Main.player[(int)Projectile.ai[0]].position.X - Projectile.position.X, -MaxVelocity, MaxVelocity);
		//    //if (Main.expertMode)
		//    //{
		//    //    Projectile.velocity += Projectile.Center.DirectionTo(Main.player[(int)Projectile.ai[0]].Center) * new Vector2(0.2f,0.03f);
		//    //    if (Projectile.velocity.Y > 3)
		//    //        Projectile.velocity.Y = 3;
		//    //}
		//}
		Projectile.velocity.Y += Shadlopod.gravPx;

		if (Projectile.ai[1] > 15)
		{
			Projectile.tileCollide = true;
		}
		Projectile.ai[1]++;
		if (Projectile.ai[1] > 5)
		{
			// todo: offset the dust's spawn position by time so it looks like a sine wave (easier than doing it with projectile)
			Vector2 dustVel = Projectile.velocity.RotatedBy(Projectile.ai[1] / MathF.PI);
			Dust D = Dust.NewDustDirect(Projectile.Center - Projectile.Size / 2, 0, 0, DustID.Wraith, Projectile.velocity.X, Projectile.velocity.Y);
			D.noGravity = true;
			D.velocity = D.velocity * 0.1f + dustVel * 0.1f;
			D.scale *= 1.5f;
		}
	}
	public override void OnKill(int timeLeft)
	{
		// todo: kill the projectile after it's been alive for a certain time, with a splat of dust
		SoundEngine.PlaySound(SoundID.NPCDeath9, Projectile.position);
		for (int i = 0; i < 10; i++)
		{
			Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), 8, 8, DustID.Wraith, Projectile.oldVelocity.X * 0.15f, Projectile.oldVelocity.Y * 0.15f);
			dust.noGravity = false;
			dust.scale = 1f;
		}
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Blackout, 60 * 5);
	}
}
