using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Bows;

public class HellrazerStake : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 8;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.arrow = true;
		Projectile.extraUpdates = 2;
		Projectile.aiStyle = ProjAIStyleID.Arrow;
	}
	public override void AI()
	{
		if (Main.rand.NextBool())
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DesertTorch);
			d.velocity += Projectile.velocity * Main.rand.NextFloat();
			d.scale += Main.rand.NextFloat();
			d.noGravity = true;
		}

		Projectile.ai[0]--;
		Projectile.localAI[2]++;
		Projectile.rotation -= MathHelper.PiOver2;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		for (int i = 0; i < 5; i++)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + (Projectile.velocity.RotatedByRandom(0.2f * i) * Main.rand.NextFloat(i * 2, i * 3.5f)), Vector2.Zero, ModContent.ProjectileType<HellrazerStakeExplosion>(), (int)(Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner, Main.rand.Next(i * 3, i * 7), Main.rand.Next(1, 4));
		}
	}
	public override void OnKill(int timeLeft)
	{
		for(int i = 0; i < 40; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DesertTorch);
			d.velocity += Projectile.velocity * Main.rand.NextFloat();
			d.scale += Main.rand.NextFloat();
			d.noGravity = true;
		}
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type];
		float iterations = Math.Min((int)(Projectile.localAI[2] / 2),15);
		for(float i = iterations; i > 0; i--)
		{
			float percent = i / iterations;
			Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition - Projectile.velocity * i, null, (Color.Lerp(Color.OrangeRed,Color.White, MathF.Pow(1f - percent,3)) * (1f - percent) * 0.75f) with { A = 32 }, Projectile.rotation, tex.Size() / 2, Projectile.scale - percent * 0.5f, SpriteEffects.None);
		}
		Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, null, Color.OrangeRed with { A = 0}, Projectile.rotation, tex.Size() / 2, Projectile.scale * 1.3f, SpriteEffects.None);
		Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() / 2, Projectile.scale,SpriteEffects.None);
		return false;
	}
}
