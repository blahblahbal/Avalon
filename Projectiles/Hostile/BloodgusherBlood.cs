using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class BloodgusherBlood : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.penetrate = 1;
		Projectile.width = 12;
		Projectile.height = 12;
		Projectile.aiStyle = 1;
		Projectile.hostile = true;
	}
	public override void AI()
	{
		Projectile.spriteDirection = Projectile.direction;
		Dust d = Dust.NewDustDirect(Projectile.position, 8, 8, DustID.Blood, Scale: 1.4f);
		d.velocity += Projectile.velocity;
		d.velocity *= 0.4f;
		d.noGravity = true;
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath9 with { volume = 0.5f}, Projectile.position);
		for (int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, 8, 8, DustID.Blood, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f);
			d.noGravity = true;
			d.scale = 2;
			d.velocity *= 2;
		}
	}
}
