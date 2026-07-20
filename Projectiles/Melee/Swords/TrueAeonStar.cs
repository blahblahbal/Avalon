using Avalon.Core;
using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class TrueAeonStar : ModProjectile
{
	SoundStyle _chime = new SoundStyle("Avalon/Sounds/Item/ReverseWindChime", 5) { pitchVariance = 0.15f, pitch = 0.2f, MaxInstances = 16, volume = 0.75f };
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
		DrawData d = new DrawData(tex, Vector2.Zero, new Rectangle(0, 0, tex.Width, tex.Height / 2), Color.White with { A = 64 }, 0, tex.Size() / new Vector2(2, 4), Projectile.scale, SpriteEffects.None);
		var seed = (ulong)Projectile.ai[0];
		Rectangle glowFrame = new Rectangle(0, tex.Height / 2, tex.Width, tex.Height / 2);
		for (int i = 0; i < StarPositions.Length; i++)
		{
			Vector2 starPos = StarPositions[i] - Main.screenPosition + Projectile.Size / 2;
			float starRotation = (Projectile.position.X - StarPositions[i].X) * -0.05f;
			float laserWidth = MathF.Sin((float)(Main.timeForVisualEffects * 0.3f) + Utils.RandomInt(ref seed, 60));
			int timeTillDeath = (int)Projectile.ai[2] - (_deathDelayAfterLasers + _spawnLaserTimeStart + _deathInterval * i);
			float deathAnimationPercent = Utils.Remap(timeTillDeath, 0, -30, 1, 0);
			if (timeTillDeath <= 0)
			{
				if (i != 0 && Projectile.ai[2] > _spawnLaserTimeStart + _deathInterval * i)
				{
					Vector2 nextStarPos = StarPositions[i - 1] - Main.screenPosition + Projectile.Size / 2;
					var laserData = new DrawData(TextureAssets.Extra[ExtrasID.ThePerfectGlow].Value, starPos, new Rectangle(0, 40, 72, 1), Color.Lerp(Color.DodgerBlue, Color.Magenta, MathF.Pow(deathAnimationPercent, 3) * 2) with { A = 0 }, starPos.DirectionTo(nextStarPos).ToRotation() + MathHelper.PiOver2, new Vector2(72 / 2, 1), new Vector2(0.5f + laserWidth * 0.1f + MathF.Pow(deathAnimationPercent,3), starPos.Distance(nextStarPos)), SpriteEffects.None);
					Main.EntitySpriteDraw(laserData);
					Main.EntitySpriteDraw(laserData with { color = Color.Lerp(Color.White with { A= 0}, Color.Black,MathF.Pow(deathAnimationPercent,3)), scale = new Vector2(laserData.scale.X * 0.5f * deathAnimationPercent, laserData.scale.Y) });
				}
				Main.EntitySpriteDraw(d with { position = starPos, rotation = starRotation });

				if (deathAnimationPercent > 0)
				{
					for (int i2 = 0; i2 < 3; i2++)
					{
						Main.EntitySpriteDraw(d with
						{
							position = starPos + new Vector2((1f - MathF.Pow(deathAnimationPercent, 2)) * 15).RotatedBy(i2 * MathHelper.TwoPi / 3 + Main.timeForVisualEffects * 0.03f),
							color = Color.Lerp(Color.Magenta, Color.Cyan, deathAnimationPercent) with { A = 64 } * deathAnimationPercent * deathAnimationPercent,
							rotation = starRotation,
							scale = Vector2.One * deathAnimationPercent * 1.25f,
							sourceRect = glowFrame
						});
						Main.EntitySpriteDraw(d with
						{
							position = starPos + new Vector2((1f - MathF.Pow(deathAnimationPercent, 2)) * 15).RotatedBy(i2 * MathHelper.TwoPi / 3 + Main.timeForVisualEffects * 0.03f),
							color = Color.Black * deathAnimationPercent * deathAnimationPercent,
							rotation = starRotation,
							scale = Vector2.One * deathAnimationPercent,
							sourceRect = glowFrame
						});
						Main.EntitySpriteDraw(d with
						{
							position = starPos + new Vector2((1f - MathF.Pow(deathAnimationPercent, 3)) * 30).RotatedBy(MathHelper.TwoPi / 6 + i2 * MathHelper.TwoPi / 3 + Main.timeForVisualEffects * -0.03f),
							color = Color.Lerp(Color.DodgerBlue, Color.Lavender, deathAnimationPercent) with { A = 64 } * deathAnimationPercent * deathAnimationPercent,
							rotation = -starRotation,
							scale = Vector2.One * deathAnimationPercent * 1.25f,
							sourceRect = glowFrame
						});
						Main.EntitySpriteDraw(d with
						{
							position = starPos + new Vector2((1f - MathF.Pow(deathAnimationPercent, 3)) * 30).RotatedBy(MathHelper.TwoPi / 6 + i2 * MathHelper.TwoPi / 3 + Main.timeForVisualEffects * -0.03f),
							color = Color.Black * deathAnimationPercent * deathAnimationPercent,
							rotation = -starRotation,
							scale = Vector2.One * deathAnimationPercent,
							sourceRect = glowFrame
						});
					}
				}
			}
		}
		for (int i = 0; i < StarPositions.Length; i++)
		{
			Vector2 starPos = StarPositions[i] - Main.screenPosition + Projectile.Size / 2;
			float starRotation = (Projectile.position.X - StarPositions[i].X) * -0.05f;
			int timeTillDeath = (int)Projectile.ai[2] - (_deathDelayAfterLasers + _spawnLaserTimeStart + _deathInterval * i);
			float deathAnimationPercent = Utils.Remap(timeTillDeath, -10, -30, 1, 0);
			if(deathAnimationPercent > 0 && timeTillDeath <= 0)
			{
				Main.EntitySpriteDraw(d with { position = starPos, rotation = starRotation, sourceRect = glowFrame, color = Color.Lerp(Color.DodgerBlue with { A = 128},Color.Black * 0.5f,deathAnimationPercent * deathAnimationPercent) * deathAnimationPercent * 2, scale = Vector2.One * MathHelper.SmoothStep(0,1, deathAnimationPercent) });
			}
		}
		return false;
	}
	private static int _deathDelayAfterLasers = 20;
	private static int _spawnLaserTimeStart = 40;
	private static int _deathInterval = 4;

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		for (int i = 0; i < StarPositions.Length; i++)
		{
			if (Projectile.ai[2] > _spawnLaserTimeStart + _deathDelayAfterLasers + _deathInterval)
				continue;
			if (targetHitbox.Intersects(new Rectangle((int)StarPositions[i].X, (int)StarPositions[i].Y, Projectile.width, Projectile.height)))
				return true;
		}
		return false;
	}
	SlotId[] SoundSlots = new SlotId[6];
	
	public override void AI()
	{
		int dustType = ModContent.DustType<SimpleColorableGlowyDust>();
		if (Projectile.ai[2] == 0)
		{
			SoundEngine.PlaySound(SoundID.Item117 with { Volume = 0.7f}, Projectile.position);
			for (int i = 0; i < 35; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, dustType);
				d.color = Main.rand.NextBool(3) ? d.color = new Color(Main.rand.Next(200), 100, 255, 0) : Color.Black;
				d.noGravity = true;
				d.noLightEmittence = true;
				d.velocity = Projectile.velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat(4);
				d.scale += Main.rand.NextFloat();
			}
		}
		Projectile.position -= Projectile.velocity;
		float speed = MathHelper.SmoothStep(4, 0, Projectile.ai[1] / 50f); //Utils.Remap(Projectile.ai[1], 0, 50, 4, 0f);
		Projectile.ai[2]++;
		Projectile.ai[1] += speed;
		var seed = (ulong)Projectile.ai[0];
		for (int i = 0; i < StarPositions.Length; i++)
		{
			StarPositions[i] = Projectile.position + Projectile.velocity.RotatedBy(Utils.RandomFloat(ref seed) - 0.5f) * Projectile.ai[1] * (Utils.RandomFloat(ref seed) * 0.75f + 0.5f);
			Projectile.EmitEnchantmentVisualsAt(StarPositions[i], Projectile.width, Projectile.height);
			Vector2 starCenter = StarPositions[i] + Projectile.Size / 2;
			float starRotation = (Projectile.position.X - StarPositions[i].X) * -0.05f;
			if (Projectile.ai[2] == _spawnLaserTimeStart + _deathInterval * i)
			{
				SoundSlots[i] = SoundEngine.PlaySound(_chime, StarPositions[i]);
				//SoundEngine.PlaySound(_chime with { pitch = -1.4f, volume = 0.05f}, StarPositions[i]);
			}
			if (Projectile.ai[2] == _deathDelayAfterLasers + _spawnLaserTimeStart + _deathInterval * i)
			{
				if (SoundEngine.TryGetActiveSound(SoundSlots[i],out var sound))
				{
					sound.Stop();
				}
				SoundEngine.PlaySound(SoundID.Item14 with { pitchVariance = 2, MaxInstances = 10 }, StarPositions[i]);

				for (int i2 = 0; i2 < 30; i2++)
				{
					Dust d = Dust.NewDustDirect(starCenter, 0, 0, dustType, 0, 0, 0, default, 1);
					d.color = new Color(Main.rand.Next(200), 100, 255, 0);
					d.noGravity = true;
					d.fadeIn = Main.rand.NextFloat(0.5f, 1.5f);
					d.velocity = new Vector2(Main.rand.NextFloat(3, 8), 0).RotatedBy(MathHelper.Pi / 15 * i2);
				}
				if (Main.myPlayer == Projectile.owner)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), starCenter, Vector2.Zero, ModContent.ProjectileType<AeonExplosion>(), Projectile.damage * 7, Projectile.knockBack * 2, Projectile.owner);
					for (int i2 = 0; i2 < 2; i2++)
					{
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), starCenter, Main.rand.NextVector2CircularEdge(4, 4) * Main.rand.NextFloat(0.8f, 1f), ModContent.ProjectileType<TrueAeonStarShard>(), Projectile.damage * 4, Projectile.knockBack, Projectile.owner, ai1: -1, ai2: Main.rand.NextFloat(14, 24));
					}
				}
				Main.ParticleSystem_World_OverPlayers.Add(new AeonStarburst(starCenter, Vector2.Zero, new Color(Main.rand.Next(200), 100, 255, 64), Color.Black, starRotation, 3, 24));
				Main.ParticleSystem_World_OverPlayers.Add(new AeonStarburst(starCenter, Vector2.Zero, new Color(Main.rand.Next(200), 100, 255, 64), Color.Black, starRotation + MathHelper.Pi, 3, 18));

				var t = AssetReferences.Assets.Textures.InverseGlowRing.Asset;
				t.Wait();
				for (int i2 = 0; i2 < 2; i2++)
				{
					var ring = VanillaParticles.RequestFadingParticle();
					ring.SetBasicInfo(t, null, Vector2.Zero, starCenter);
					int time = Main.rand.Next(10,20);
					ring.SetTypeInfo(time);
					ring.Scale = Vector2.One * 0.15f;
					ring.ScaleVelocity = Vector2.One.RotatedByRandom(0.5f) * 0.15f;
					ring.ScaleAcceleration = ring.ScaleVelocity / -time;
					ring.FadeInNormalizedTime = 0.1f;
					ring.FadeOutNormalizedTime = 0.1f;
					ring.ColorTint = i2 == 0? new Color(Main.rand.Next(200), 100, 255, 0) : Color.Black;
					ring.Rotation = Main.rand.NextFloatDirection();
					ring.RotationVelocity = Main.rand.NextFloat(-0.1f, 0.1f);
					Main.ParticleSystem_World_OverPlayers.Add(ring);
				}

				float iterations = 7;
				float spin = Main.rand.NextFloatDirection();
				for (int i2 = 0; i2 < iterations; i2++)
				{
					var p = VanillaParticles.RequestPrettySparkleParticle();
					p.TimeToLive = Main.rand.Next(20, 50);
					p.FadeInEnd = p.FadeOutStart = 10;
					p.Velocity = Vector2.UnitY.RotatedBy((i2 / iterations * MathHelper.TwoPi) + spin + Main.rand.NextFloat(MathHelper.Pi / -iterations, MathHelper.Pi / iterations)) * Main.rand.NextFloat(5, 8);
					p.LocalPosition = starCenter + p.Velocity * 3;
					p.Scale = new Vector2(3, Main.rand.NextFloat(1, 2.5f));
					p.DrawHorizontalAxis = false;
					p.AccelerationPerFrame = -p.Velocity / p.TimeToLive;
					p.ColorTint = new Color(Main.rand.Next(200), 100, 255, 0);
					p.Rotation = p.Velocity.ToRotation() - MathHelper.PiOver2;
					Main.ParticleSystem_World_OverPlayers.Add(p);
				}

				//for (int i2 = 0; i2 < 3; i2++)
				//{
				//	Vector2 velocity = Main.rand.NextVector2CircularEdge(8, 8) * Main.rand.NextFloat(0.6f, 1.2f);
				//	Main.ParticleSystem_World_OverPlayers.Add(new AeonStarburst(starCenter, velocity, new Color(Main.rand.Next(200), 100, 255, 0), velocity.ToRotation() + MathHelper.PiOver2, 1,24));
				//}

				if (i != 0)
				{
					for (int i2 = 0; i2 < 60; i2++)
					{
						Dust d = Dust.NewDustPerfect(Vector2.Lerp(StarPositions[i], StarPositions[i - 1], Main.rand.NextFloat()) + Projectile.Size / 2, dustType);
						d.color = new Color(Main.rand.Next(200), 100, 255, 32);
						d.noGravity = true;
						d.noLightEmittence = true;
						d.velocity = Main.rand.NextVector2Circular(3, 3) + StarPositions[i].DirectionTo(StarPositions[i - 1]) * -3;
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
	}
	public Vector2[] StarPositions = new Vector2[6];
}
