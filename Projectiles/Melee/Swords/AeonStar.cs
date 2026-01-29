using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class AeonStar : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 24;
		Projectile.height = 24;
		Projectile.aiStyle = -1;
		Projectile.alpha = 0;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 25;
		Projectile.tileCollide = false;
		DrawOriginOffsetY = 2;
		DrawOffsetX = 4;
		Projectile.extraUpdates = 1;
	}
	Vector2 LastStarPos;
	public override void OnSpawn(IEntitySource source)
	{
		int J = Projectile.ai[0] != -255 ? Main.projectile[(int)Projectile.ai[0]].whoAmI : Projectile.whoAmI;
		LastStarPos = Main.projectile[J].Center;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Projectile.type];
		Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Value.Width, frameHeight);
		Vector2 frameOrigin = frame.Size() / 2f;
		Color color = Color.Lerp(new Color(255, 255, 255, 0), new Color(128, 128, 128, 64), Projectile.ai[1] * 0.03f);
		for (int i = 0; i < 6; i++)
		{
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.position + frameOrigin - Main.screenPosition + new Vector2(0, (float)Math.Sin(Main.GlobalTimeWrappedHourly * MathHelper.TwoPi / 12f) * 4).RotatedBy(i * MathHelper.PiOver2 + Main.timeForVisualEffects * 0.03f), frame, color * 0.2f, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None);
		}
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.position + frameOrigin - Main.screenPosition, frame, color, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None);
		return false;
	}
	public override void AI()
	{
		float Seed = Projectile.ai[2];
		Projectile lastStar = Projectile.ai[0] != -255 ? Main.projectile[(int)Projectile.ai[0]] : Projectile;
		float distanceToStar = Projectile.Center.Distance(lastStar.Center);
		if (!lastStar.active)
		{
			lastStar = Projectile;
		}
		Projectile.ai[1]--;
		if (Projectile.ai[1] == 100 && lastStar.ai[2] == Seed && lastStar.whoAmI != Projectile.whoAmI == true)
		{
			SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);
		}
		if (lastStar.ai[2] == Seed && lastStar.whoAmI != Projectile.whoAmI && Projectile.ai[1] < 100)
		{
			for (int i = 0; i < distanceToStar; i += 6)
			{
				int D = Dust.NewDust(Projectile.Center + new Vector2(i, 0).RotatedBy(Projectile.Center.AngleTo(lastStar.Center)), 0, 0, DustID.UnusedWhiteBluePurple, 0, 0, 0, default, 1);
				Main.dust[D].noGravity = true;
				Main.dust[D].velocity *= 0;
			}
		}
		Projectile.velocity *= 0.95f;
		Projectile.rotation += Projectile.velocity.Length() / 30;
		Projectile.rotation += 0.007f;


		if (Projectile.ai[1] > 30)
		{
			LastStarPos = lastStar.Center;
		}

		if (Projectile.ai[1] < 10)
		{
			int D = Dust.NewDust(Vector2.Lerp(Projectile.Center, LastStarPos, Projectile.ai[1] / 10), 0, 0, DustID.UnusedWhiteBluePurple, 0, 0, 0, default, 2);
			Main.dust[D].color = new Color(255, 255, 255, 0);
			Main.dust[D].noGravity = true;
			Main.dust[D].velocity *= 0;
			Main.dust[D].noLightEmittence = true;
		}
		if (Projectile.ai[1] < 0)
		{
			Projectile.Kill();
		}
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item110, Projectile.Center);

		for (int i = 0; i < 30; i++)
		{
			int D = Dust.NewDust(Projectile.Center, 0, 0, DustID.GoldCoin, 0, 0, 0, default, 3);
			Main.dust[D].color = new Color(255, 255, 255, 0);
			Main.dust[D].noGravity = true;
			Main.dust[D].noLightEmittence = true;
			Main.dust[D].fadeIn = Main.rand.NextFloat(0.5f, 1.5f);
			Main.dust[D].velocity = new Vector2(Main.rand.NextFloat(3, 8), 0).RotatedBy(MathHelper.Pi / 15 * i);
		}

		if (Main.myPlayer == Projectile.owner)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AeonExplosion>(), Projectile.damage * 5, Projectile.knockBack * 2, Projectile.owner);
		}

		Projectile lastStar = Projectile.ai[0] != -255 ? Main.projectile[(int)Projectile.ai[0]] : Projectile;
		if (!lastStar.active)
		{
			lastStar = Projectile;
		}
		ParticleSystem.NewParticle(new AeonStarburst(Vector2.Zero, Color.Yellow, Projectile.rotation, 2), Projectile.Center);
		if (lastStar == Projectile)
		{
			ParticleSystem.NewParticle(new AeonStarburst(Vector2.Zero, Color.Red, Projectile.rotation + MathHelper.Pi, 3), Projectile.Center);
		}
	}
}
