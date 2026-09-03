using Avalon.Common;
using Avalon.Common.Interfaces;
using Avalon.Core;
using Avalon.Data.Sets;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Swords;
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

public class IridiumGreatswordSwing : ModProjectile, ISyncedOnHitEffect
{
	public override void SetStaticDefaults()
	{
		ProjectileSets.TrueMeleeProjectiles[Type] = true;
	}
	public override string Texture => ModContent.GetInstance<IridiumGreatsword>().Texture;
	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.penetrate = -1;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		Projectile.hide = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
	}
	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (Main.player[Projectile.owner].channel || Projectile.ai[2] > 0)
			return false;
		return Utils.IntersectsConeFastInaccurate(targetHitbox, Main.player[Projectile.owner].MountedCenter, 110 * Projectile.scale, Projectile.rotation - MathHelper.PiOver4, 0.4f); //Collision.CheckAABBvLineCollision2(targetHitbox.TopLeft(), targetHitbox.Size(), Main.player[Projectile.owner].MountedCenter, Main.player[Projectile.owner].MountedCenter + (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2() * 110 * Projectile.scale);
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = Main.player[Projectile.owner].direction;
		modifiers.SourceDamage *= Utils.Remap(Projectile.ai[0], 0, 1, 0.25f, 3);
	}
	public override void AI()
	{
		float chargeTime = 60;
		Player player = Main.player[Projectile.owner];
		if (player.dead || !player.active || player.CCed)
		{
			Projectile.Kill();
			return;
		}
		Projectile.scale = player.GetAdjustedItemScale(player.HeldItem);
		player.heldProj = Projectile.whoAmI;
		player.SetDummyItemTime(2);
		if (player.channel || Projectile.ai[0] < 0.3f)
		{
			Projectile.ai[0] += 1 / chargeTime;
			Projectile.ai[1] += 1 / chargeTime;
			if (Projectile.ai[1] > 1)
			{
				Projectile.localAI[2]++;
				if (Projectile.localAI[2] == 1)
				{
					SoundEngine.PlaySound(Sounds.Item.IridiumSwordCharge.Asset with { pitchVariance = 0.2f }, Projectile.position);
					//SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/deadcells_flint_charge") { pitchVariance = 0.2f }, Projectile.position);
					for (int i = 0; i < 25; i++)
					{
						Vector2 speed = (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2() * 5;
						Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(1, -1).RotatedBy(Projectile.rotation) * Main.rand.NextFloat(0, 70 * Projectile.scale), ModContent.DustType<SimpleColorableGlowyDust>());
						d.noGravity = true;
						d.color = Color.SandyBrown with { A = 0 } * Main.rand.NextFloat();
						d.scale += Main.rand.NextFloat(0.5f);
						d.velocity *= 2;
						d.velocity += speed;
					}
				}
				Projectile.ai[0] = 1;
				Projectile.ai[1] = 1;
			}
			if (Main.myPlayer == Projectile.owner)
			{
				Projectile.spriteDirection = Main.MouseWorld.X < player.Center.X ? -1 : 1;
				player.direction = Projectile.spriteDirection;
			}
		}
		else
		{
			// collision visualizer
			//if (Main.timeForVisualEffects % 2 == 0)
			//{
			//	for (int x = -20; x < 20; x += 2)
			//	{
			//		for (int y = -20; y < 20; y += 2)
			//		{
			//			Dust d = Dust.QuickDust(player.MountedCenter - new Vector2(x, y) * 8, Color.Transparent);
			//			if (Colliding(Projectile.Hitbox, new Rectangle((int)d.position.X, (int)d.position.Y, 2, 2)) == true)
			//				d.color = Color.Green;
			//		}
			//	}
			//}

			if (Projectile.localAI[2] > 0)
				Projectile.localAI[2]++;
			if (Projectile.ai[2] <= 0)
			{
				if (Projectile.localAI[0] == 0)
				{
					SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing, Projectile.position);
					Projectile.localAI[0]++;
				}

				Projectile.ai[1] -= 0.075f;

				if (Projectile.ai[1] < 0.1f * Projectile.ai[0])
				{
					Vector2 vect = new Vector2(1, -1).RotatedBy(Projectile.rotation);
					if (AvalonUtils.SolidCollisionWithFunctionalTopSurfaceDetection(Projectile.Center + vect * 64, 16, 16) || AvalonUtils.SolidCollisionWithFunctionalTopSurfaceDetection(Projectile.Center + vect * 32, 16, 16))
					{
						SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, Projectile.position);
						Projectile.ai[2] = 1;

						Vector2 pos = IridiumGreatswordBoomLarge.FindSpotForSpike(Projectile.Bottom + new Vector2(Projectile.spriteDirection * 80 * Projectile.scale, 0));
						if (pos != Vector2.Zero && Main.myPlayer == Projectile.owner)
						{
							int type = 0;
							switch ((int)(Projectile.ai[0] * 2))
							{
								case 0:
									type = ModContent.ProjectileType<IridiumGreatswordBoomSmall>();
									break;
								case 1:
									type = ModContent.ProjectileType<IridiumGreatswordBoomMedium>();
									break;
								case 2:
									type = ModContent.ProjectileType<IridiumGreatswordBoomLarge>();
									break;
							}
							pos.Y -= ContentSamples.ProjectilesByType[type].height / 2 * Projectile.scale;
							Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos, Vector2.Zero, type, (int)(Projectile.damage * Utils.Remap(Projectile.ai[0], 0, 1, 0.05f, 3)), Projectile.knockBack * 0.75f, Projectile.owner, Projectile.spriteDirection);
						}
						Projectile.damage = 0;
					}
				}

				if (Projectile.ai[1] < -0.3f * Projectile.ai[0])
					Projectile.Kill();
				for (int i = 0; i < 2; i++)
				{
					Vector2 speed = (Projectile.rotation + MathHelper.PiOver4).ToRotationVector2() * 5 * player.direction;
					Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(1, -1).RotatedBy(Projectile.rotation) * Main.rand.NextFloat(0, 70 * Projectile.scale), ModContent.DustType<SimpleColorableGlowyDust>());
					d.noGravity = true;
					d.color = Main.rand.NextBool(3) ? Color.SandyBrown with { A = 0 } : new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0.5f);
					d.velocity += speed;

					Dust d2 = Dust.NewDustPerfect(Projectile.Center + new Vector2(1, -1).RotatedBy(Projectile.rotation) * Main.rand.NextFloat(0, 64 * Projectile.scale), DustID.Wraith);
					d2.noGravity = true;
					d2.velocity += speed;
				}
			}
			else
			{
				Projectile.ai[2]++;
				Projectile.ai[1] += MathF.Sin(Projectile.ai[2] / 20f * MathHelper.Pi * 1.5f) * 0.01f;
				if (Projectile.ai[2] >= 20 || player.controlUseItem)
				{
					Projectile.Kill();
				}
			}
		}

		player.direction = Projectile.spriteDirection;
		Projectile.rotation = Utils.Remap(MathF.Sin(Projectile.ai[1] * MathHelper.PiOver2), 0, 1, MathHelper.PiOver4, -2, false);
		if (player.direction == -1)
		{
			Projectile.rotation = (Projectile.rotation * -1) - MathHelper.PiOver2;
		}

		player.bodyFrame.Y = 56 * (int)MathF.Round(Utils.Remap(MathF.Sin(Projectile.ai[1] * MathHelper.PiOver2), 0, 1, 4, 1));
		Projectile.Center = ((Vector2)player.HandPosition).Floor();
		Projectile.position -= Projectile.velocity;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var glowTex = AssetReferences.Projectiles.Melee.Swords.IridiumGreatswordGlow.Asset.Value;
		Player player = Main.player[Projectile.owner];
		float glow = 0;
		if (Projectile.ai[1] > 0)
			glow = Utils.Remap(Projectile.ai[1], 0, 0.6f * Projectile.ai[0], Projectile.ai[0], 0);
		else
			glow = Utils.Remap(Projectile.ai[1], -0.3f * Projectile.ai[0], 0, 0, Projectile.ai[0]);
		bool swinging = (!player.channel && Projectile.ai[0] > 0.3f && Projectile.ai[2] == 0);
		if (swinging)
		{
			var slashTex = TextureAssets.Projectile[ProjectileID.TheHorsemansBlade];
			if (!slashTex.IsLoaded)
				Main.instance.LoadProjectile(ProjectileID.TheHorsemansBlade);

			DrawData slash = new(slashTex.Value, Projectile.Center - Main.screenPosition + new Vector2(16, -16).RotatedBy(Projectile.rotation), null, default, Projectile.rotation + MathHelper.PiOver4 * 3, new Vector2(slashTex.Width() / 2, (slashTex.Height() / 8) - slashTex.Height() / 8), Projectile.scale, SpriteEffects.FlipHorizontally);
			if (Projectile.spriteDirection == -1)
			{
				slash.rotation -= MathHelper.Pi;
				slash.effect = SpriteEffects.None;
			}
			Color[] colors = [new Color(54, 84, 68), new Color(132, 150, 111), new Color(164, 209, 148), new Color(245, 255, 206)];
			for (int i = 0; i < 4; i++)
			{
				Main.EntitySpriteDraw(slash with {color = colors[i] with { A = 200 } * glow, sourceRect = slashTex.Frame(1, 8, 0, (i * 2) + 1) });
			}
		}
		var tex = TextureAssets.Projectile[Type];
		Vector2 origin = new Vector2(0, tex.Height());
		DrawData d = new(tex.Value, Projectile.Center - Main.screenPosition, tex.Frame(), lightColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None);
		Main.EntitySpriteDraw(d);
		float glowPercent = MathF.Sin(Utils.Remap(Projectile.localAI[2], 0, 30, MathHelper.PiOver2, MathHelper.Pi));
		if (Projectile.localAI[2] > 0 && glowPercent > 0)
		{
			Main.EntitySpriteDraw(d with { texture = glowTex, color = Color.SandyBrown with { A = 0 } * glowPercent });
			glowPercent = MathF.Sin(Utils.Remap(Projectile.localAI[2], 0, 15, MathHelper.PiOver2, MathHelper.Pi));
			Main.EntitySpriteDraw(d with { texture = glowTex, color = Color.SandyBrown with { A = 0 } * glowPercent * 2, scale = d.scale * (1f + Projectile.localAI[2] * 0.03f) });
		}
		if (Projectile.ai[0] == 1 && Projectile.ai[2] == 0)
		{
			var d2 = d with { texture = glowTex, sourceRect = new Rectangle(0,76,74,74), color = Color.SandyBrown with { A = 0 } * (float)(0.3f + Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.1f) };
			Main.EntitySpriteDraw(d with { texture = glowTex, color = Color.White * 0.5f });
			for (int i = 0; i < 4; i++)
			{
				Main.EntitySpriteDraw(d2 with { position = d2.position + new Vector2(0, 2 * Projectile.scale).RotatedBy(i * MathHelper.PiOver2 + Projectile.rotation) });
			}
		}
		if (swinging)
		{
			var sparkleTex = TextureAssets.Extra[ExtrasID.ThePerfectGlow];
			DrawData sparkle = new(sparkleTex.Value, Projectile.Center - Main.screenPosition + new Vector2(70, -70).RotatedBy(Projectile.rotation) * Projectile.scale, null, Color.SandyBrown with { A = 64 } * Projectile.ai[0], 0, sparkleTex.Size() / 2, new Vector2(1, 1.5f * glow) * Projectile.scale, SpriteEffects.None);

			for (int i = 0; i < 2; i++)
			{
				Main.EntitySpriteDraw(sparkle with { rotation = i * MathHelper.PiOver2 });
				Main.EntitySpriteDraw(sparkle with { color = Color.DarkGray with { A = 0 } * Projectile.ai[0] * glow, scale = sparkle.scale * new Vector2(0.5f, 0.75f), rotation = i * MathHelper.PiOver2 });
			}
			for (int i = 0; i < 8; i++)
			{
				float rotation = i * -0.15f * Projectile.spriteDirection;
				Main.EntitySpriteDraw(sparkle with { position = Vector2.Lerp(sparkle.position.RotatedBy(rotation, Projectile.Center - Main.screenPosition), Projectile.Center - Main.screenPosition, i * 0.01f), rotation = Projectile.rotation + rotation - MathHelper.PiOver4, scale = new Vector2(glow, 1), color = sparkle.color * (1f - i * 0.1f) });
			}
		}
		return false;
	}

	public bool SyncedOnHitNPC(Player player, NPC target, Rectangle attackHitbox, int damage, float knockback, bool crit, int hitDirection, Projectile? projectile)
	{
		Vector2 pos = target.Hitbox.ClosestPointInRect(player.Center);
		int type = ModContent.DustType<SimpleColorableGlowyDust>();
		for (int i = 0; i < 3; i++)
		{
			var p = VanillaParticles.RequestPrettySparkleParticle();
			p.ColorTint = Color.SandyBrown;
			p.FadeInEnd = Main.rand.NextFloat(2, 5);
			p.FadeOutStart = p.FadeInEnd;
			p.FadeOutEnd = Main.rand.NextFloat(13, 28);
			p.Scale = new Vector2(4, 2).RotatedByRandom(0.3f);
			p.Rotation = Projectile.rotation - MathHelper.PiOver4 + Main.rand.NextFloat(-0.2f, 0.2f);
			p.Velocity = (p.Rotation + MathHelper.PiOver2).ToRotationVector2() * Main.rand.NextFloat(2, 5) * player.direction;
			p.DrawHorizontalAxis = false;
			p.LocalPosition = pos + Main.rand.NextVector2Circular(8, 8);
			Main.ParticleSystem_World_OverPlayers.Add(p);
			for (int x = 0; x < 5; x++)
			{
				Dust d = Dust.NewDustPerfect(p.LocalPosition, type, p.Velocity.RotatedByRandom(0.4f) * Main.rand.NextFloat(2));
				d.noGravity = true;
				d.color = p.ColorTint with { A = 0 };
			}
		}

		return true;
	}
}