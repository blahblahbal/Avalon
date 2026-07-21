using Avalon.Dusts;
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
	SoundStyle _chime = new SoundStyle("Avalon/Sounds/Item/WindChime", 5) { pitchVariance = 0.15f, pitch = 0.2f, MaxInstances = 16, volume = 0.5f };
	private const int _deathDelayAfterLasers = 40;
	private const int _spawnLaserTimeStart = 40;
	private const int _deathInterval = 5;
	private Vector2[] StarPositions = new Vector2[5];
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
		Projectile.noEnchantmentVisuals = true;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type].Value;
		DrawData d = new DrawData(tex,Vector2.Zero,new Rectangle(0, 0, tex.Width, tex.Height / 2),Color.White with { A = 200},0,tex.Size() / new Vector2(2,4), Projectile.scale, SpriteEffects.None);
		var seed = (ulong)Projectile.ai[0];
		Rectangle glowFrame = new Rectangle(0, tex.Height / 2, tex.Width, tex.Height / 2);
		for (int i = 0; i < StarPositions.Length; i++)
		{
			Vector2 starPos = StarPositions[i] - Main.screenPosition + Projectile.Size / 2;
			float starRotation = (Projectile.position.X - StarPositions[i].X) * -0.05f;
			//float glowRadius = (int)((Main.timeForVisualEffects + Utils.RandomInt(ref seed,60)) % 60) / 60f;
			float laserWidth = MathF.Sin((float)(Main.timeForVisualEffects * 0.15f) + Utils.RandomInt(ref seed, 60));
			int timeTillDeath = (int)Projectile.ai[2] - (_deathDelayAfterLasers + _spawnLaserTimeStart + _deathInterval * i);
			float deathAnimationPercent = Utils.Remap(timeTillDeath, 0, -20, 1, 0);
			if (timeTillDeath <= 0)
			{
				//for (int i2 = 0; i2 < 4; i2++)
				//{
				//	Main.EntitySpriteDraw(d with
				//	{
				//		position = starPos + new Vector2(glowRadius * 4).RotatedBy(i2 * MathHelper.PiOver2 + Main.timeForVisualEffects * 0.04f),
				//		color = Color.White with { A = 0 } * (1f - glowRadius) * glowRadius,
				//		rotation = starRotation
				//	});
				//}
				if (i != 0 && Projectile.ai[2] > _spawnLaserTimeStart + _deathInterval * i)
				{
					Vector2 nextStarPos = StarPositions[i - 1] - Main.screenPosition + Projectile.Size / 2;
					var laserData = new DrawData(TextureAssets.Extra[ExtrasID.ThePerfectGlow].Value, starPos, new Rectangle(0, 40, 72, 1), Color.Lerp(Color.DodgerBlue,Color.Red, MathF.Pow(deathAnimationPercent,3) * 2) with { A = 128 }, starPos.DirectionTo(nextStarPos).ToRotation() + MathHelper.PiOver2, new Vector2(72 / 2, 1), new Vector2(0.5f + laserWidth * 0.2f, starPos.Distance(nextStarPos)), SpriteEffects.None);
					Main.EntitySpriteDraw(laserData);
					Main.EntitySpriteDraw(laserData with { color = new Color(1f,1f,0.5f,0), scale = new Vector2(laserData.scale.X * 0.5f, laserData.scale.Y) });
					//Utils.DrawLine(Main.spriteBatch, starPos + Main.screenPosition, StarPositions[i + 1] + Projectile.Size / 2, Color.Blue with { A = 0 }, Color.Purple with { A = 0}, 4);
				}
				Main.EntitySpriteDraw(d with { position = starPos, rotation = starRotation });

				if (deathAnimationPercent > 0)
				{
					for (int i2 = 0; i2 < 3; i2++)
					{
						Main.EntitySpriteDraw(d with
						{
							position = starPos + new Vector2((1f - MathF.Pow(deathAnimationPercent, 3)) * 10).RotatedBy(i2 * MathHelper.TwoPi / 3 + Main.timeForVisualEffects * 0.1f),
							color = Color.Lerp(Main.DiscoColor,Color.White,deathAnimationPercent) with { A = 0 } * deathAnimationPercent * deathAnimationPercent,
							rotation = starRotation,
							sourceRect = glowFrame
						});
					}
				}

				//if (deathAnimationPercent > 0)
				//{
				//	Color yellow = Color.Lerp(new Color(1f, 1, 0.5f, 0), Color.White with { A = 0 }, MathF.Pow(deathAnimationPercent, 2.5f)) * MathF.Pow(deathAnimationPercent,1);
				//	Color red = Color.Lerp(new Color(1f, 0.45f, 0.2f, 0), Color.Gray with { A = 0 }, MathF.Pow(deathAnimationPercent, 2.5f)) * MathF.Pow(deathAnimationPercent,1);
				//	for (int i2 = 0; i2 < 3; i2++)
				//	{
				//		Main.EntitySpriteDraw(d with
				//		{
				//			position = starPos + new Vector2((1f - MathF.Pow(deathAnimationPercent,3)) * 10).RotatedBy(i2 * MathHelper.TwoPi / 3 + Main.timeForVisualEffects * 0.1f),
				//			color = yellow,
				//			rotation = starRotation,
				//			scale = Vector2.One * deathAnimationPercent,
				//			sourceRect = glowFrame
				//		});
				//		Main.EntitySpriteDraw(d with
				//		{
				//			position = starPos + new Vector2((1f - MathF.Pow(deathAnimationPercent, 3)) * 20).RotatedBy(i2 * MathHelper.TwoPi / 3 + MathHelper.TwoPi / 6 + Main.timeForVisualEffects * 0.1f),
				//			color = red,
				//			rotation = starRotation,
				//			scale = Vector2.One * deathAnimationPercent,
				//			effect = SpriteEffects.FlipVertically,
				//			sourceRect = glowFrame
				//		});
				//	}
				//}
			}
		}
		return false;
	}
	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		for(int i = 0; i < StarPositions.Length; i++)
		{
			if (Projectile.ai[2] > _spawnLaserTimeStart + _deathDelayAfterLasers + _deathInterval)
				continue;
			if (targetHitbox.Intersects(new Rectangle((int)StarPositions[i].X, (int)StarPositions[i].Y, Projectile.width, Projectile.height)))
				return true;
		}
		return false;
	}
	public override void AI()
	{
		int dustType = ModContent.DustType<SimpleColorableGlowyDust>();
		if (Projectile.ai[2] == 0)
		{
			SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
			for(int i = 0;i < 15; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, dustType);
				d.color = Color.Lerp(new Color(0.5f, 0.65f, 1f, 0.25f), new Color(0.6f, 1f, 1f, 0.25f), Main.rand.NextFloat());
				d.noGravity = true;
				d.noLightEmittence = true;
				d.velocity = Projectile.velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat(4);
				d.scale += Main.rand.NextFloat();
			}
		}
		Projectile.position -= Projectile.velocity;
		float speed = Utils.Remap(Projectile.ai[1], 0, 30, 2, 0);
		Projectile.position += Main.player[Projectile.owner].velocity * speed / 2;
		Projectile.ai[2]++;
		Projectile.ai[1] += speed;
		var seed = (ulong)Projectile.ai[0];
		for(int i = 0; i < StarPositions.Length; i++)
		{
			StarPositions[i] = Projectile.position + Projectile.velocity.RotatedBy(Utils.RandomFloat(ref seed) * 0.75f - 0.375f) * Projectile.ai[1] * (Utils.RandomFloat(ref seed) + 0.25f);
			Projectile.EmitEnchantmentVisualsAt(StarPositions[i], Projectile.width, Projectile.height);
			Vector2 starCenter = StarPositions[i] + Projectile.Size / 2;
			float starRotation = (Projectile.position.X - StarPositions[i].X) * -0.05f;
			if (Projectile.ai[2] == _spawnLaserTimeStart + _deathInterval * i)
			{
				SoundEngine.PlaySound(_chime, StarPositions[i]);
			}
			if (Projectile.ai[2] == _deathDelayAfterLasers + _spawnLaserTimeStart + _deathInterval * i)
			{
				SoundEngine.PlaySound(SoundID.Item110 with { pitchVariance = 0.6f, MaxInstances = 10}, StarPositions[i]);
				for (int i2 = 0; i2 < 30; i2++)
				{
					Dust d = Dust.NewDustDirect(StarPositions[i], Projectile.width, Projectile.height, dustType);
					d.color = Color.Lerp(new Color(1f, 1f, 0.8f, 0.25f), new Color(1f, 0.3f, 0.1f, 0.25f), Main.rand.NextFloat());
					d.noGravity = true;
					d.noLightEmittence = true;
					d.fadeIn = Main.rand.NextFloat(0.5f, 1.5f);
					d.velocity = new Vector2(Main.rand.NextFloat(3, 8), 0).RotatedBy(MathHelper.Pi / 15 * i2);
				}
				Main.ParticleSystem_World_OverPlayers.Add(new AeonStarburst(starCenter, Vector2.Zero, Color.Yellow with { A = 64}, starRotation, 2));
				Main.ParticleSystem_World_OverPlayers.Add(new AeonStarburst(starCenter, Vector2.Zero, Color.Red with { A = 64 }, starRotation + MathHelper.Pi, 3));

				if(i != 0)
				{
					for(int i2 = 0; i2 < 60; i2++)
					{
						Dust d = Dust.NewDustPerfect(Vector2.Lerp(StarPositions[i], StarPositions[i - 1], Main.rand.NextFloat()) + Projectile.Size / 2, dustType);
						d.color = Color.OrangeRed with { A = 0 };
						d.noGravity = true;
						d.noLightEmittence = true;
						d.velocity = Main.rand.NextVector2Circular(1, 1) + StarPositions[i].DirectionTo(StarPositions[i - 1]) * -3;
						d.scale = 0.4f;
						d.fadeIn = Main.rand.NextFloat(1);
					}
				}

				if (Main.myPlayer == Projectile.owner)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), starCenter, Vector2.Zero, ModContent.ProjectileType<AeonExplosion>(), Projectile.damage * 5, Projectile.knockBack * 2, Projectile.owner);
				}
			}
		}
		if (Projectile.ai[2] > _spawnLaserTimeStart + _deathDelayAfterLasers + _deathInterval * StarPositions.Length)
			Projectile.Kill();
		//if (Projectile.ai[2] > _firstDeathTime)
		//{

		//}
	}
}
