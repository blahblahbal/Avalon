using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.BacteriumPrime;

public class TendrilShot : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 5;
	}
	public override void SetDefaults()
	{
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = -1;
		Projectile.Size = new Vector2(32);
		Projectile.tileCollide = false;
		Projectile.Opacity = 0;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1, 0.9f, 0.4f, 0.6f) * 0.8f * MathHelper.Clamp(Projectile.Opacity * 2,0,1);
	}
	public override void AI()
	{
		Projectile.Opacity += 0.05f;
		Projectile.scale = Projectile.Opacity + Utils.PingPongFrom01To010((Projectile.timeLeft % 40) / 40f ) * 0.2f;
		if (Main.rand.NextBool(3))
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(16, 16) * Projectile.scale, ModContent.DustType<SimpleColorableGlowyDust>(), Projectile.velocity * 0.4f + Main.rand.NextVector2Circular(1, 1));
			d.scale = 1.5f;
			d.noGravity = true;
			d.noLightEmittence = true;
			d.color = new Color(0.55f, 0.5f, 0.2f, 0.3f);
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

		Projectile.frameCounter++;
		if(Projectile.frameCounter > 3)
		{
			Projectile.frameCounter = 0;
			Projectile.frame++;
			if(Projectile.frame > Main.projFrames[Type] - 1)
			{
				Projectile.frame = 0;
			}
		}

		if (Projectile.ai[1] == 0)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<SimpleColorableGlowyDust>(), Main.rand.NextVector2Circular(5, 5));
				d.scale = 1.5f;
				d.noGravity = true;
				d.color = new Color(0.55f, 0.5f, 0.2f, 0.3f);
			}
			SoundEngine.PlaySound(SoundID.Item112, Projectile.position);
			SoundEngine.PlaySound(SoundID.NPCDeath9, Projectile.position);
		}

		Projectile.ai[1]++;

		if(Collision.SolidCollision(Projectile.position,Projectile.width,Projectile.height) || Main.tile[Projectile.Center.ToTileCoordinates()].WallType != 0)
		{
			Projectile.ai[1]++;
		}

		Projectile.velocity = Utils.rotateTowards(Projectile.Center, Projectile.velocity, Main.player[(int)Projectile.ai[0]].Center, 0.06f);

		if (Projectile.ai[1] > 600)
		{
			Projectile.Kill();
		}
		else if (Projectile.ai[1] > 520)
		{
			Projectile.velocity *= 0.98f;
		}
	}
	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 25; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<SimpleColorableGlowyDust>(), Main.rand.NextVector2Circular(3, 3));
			d.scale = 1.5f;
			d.noGravity = true;
			d.fadeIn = Main.rand.NextFloat(2.3f);
			d.color = new Color(0.55f, 0.5f, 0.2f, 0.3f);
		}
	}
}
