using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class UndineTear : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.penetrate = 1;
		Projectile.width = 12;
		Projectile.height = 12;
		Projectile.aiStyle = 1;
		Projectile.hostile = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.scale = 0.7f;
	}
	public override Color? GetAlpha(Color drawColor)
	{
		return drawColor with { A = 128 } * Projectile.Opacity;
	}
	public override void AI()
	{
		Projectile.spriteDirection = Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Main.rand.NextBool(3))
		{
			Dust d = Dust.NewDustDirect(Projectile.position, 8, 8, DustID.DungeonWater);
			d.velocity += Projectile.velocity;
			d.velocity *= 0.4f;
			d.noGravity = true;
		}
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Wet, 120);
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath9 with { volume = 0.2f}, Projectile.position);
		for (int num237 = 0; num237 < 10; num237++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, 8, 8, DustID.DungeonWater, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f);
			d.noGravity = true;
			d.scale = 1f;
		}
	}
}
