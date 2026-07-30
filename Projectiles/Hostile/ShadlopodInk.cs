using Avalon.NPCs.Corruption;
using Terraria;
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
		Projectile.Size = new Microsoft.Xna.Framework.Vector2(8);
		Projectile.extraUpdates = 2;
		Projectile.light = 0;
		Projectile.tileCollide = false;
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
		else
		{
			Projectile.ai[1]++;
		}
		if (Projectile.ai[1] > 5)
		{
			// todo: offset the dust's spawn position by time so it looks like a sine wave (easier than doing it with projectile)
			Dust D = Dust.NewDustDirect(Projectile.Center - Projectile.Size / 2, 0, 0, DustID.Wraith, Projectile.velocity.X, Projectile.velocity.Y);
			D.noGravity = true;
			D.velocity *= 0.1f;
			D.scale *= 1.5f;
		}
	}
	public override void OnKill(int timeLeft)
	{
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Blackout, 60 * 5);
	}
}
