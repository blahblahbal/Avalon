using Avalon.Common;
using Avalon.Data.Sets;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Swords;
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

public class IridiumGreatswordSwing : ModProjectile
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
		return Collision.CheckAABBvLineCollision2(targetHitbox.TopLeft(), targetHitbox.Size(), Main.player[Projectile.owner].MountedCenter, Main.player[Projectile.owner].MountedCenter + (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2() * 96 * Projectile.scale);
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = Main.player[Projectile.owner].direction;
		modifiers.SourceDamage *= Utils.Remap(Projectile.ai[0],0,1,0.25f,3);
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
					SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/deadcells_flint_charge") { pitchVariance = 0.2f}, Projectile.position);
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
							switch((int)(Projectile.ai[0] * 2))
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

				Vector2 speed = (Projectile.rotation + MathHelper.PiOver4).ToRotationVector2() * 5 * player.direction;
				Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(1,-1).RotatedBy(Projectile.rotation) * Main.rand.NextFloat(0,64 * Projectile.scale), ModContent.DustType<SimpleColorableGlowyDust>());
				d.noGravity = true;
				d.color = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0.5f);
				d.velocity += speed;

				Dust d2 = Dust.NewDustPerfect(Projectile.Center + new Vector2(1, -1).RotatedBy(Projectile.rotation) * Main.rand.NextFloat(0, 64 * Projectile.scale), DustID.Wraith);
				d2.noGravity = true;
				d2.velocity += speed;
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
		Projectile.rotation = Utils.Remap(MathF.Sin(Projectile.ai[1] * MathHelper.PiOver2), 0, 1, MathHelper.PiOver4, -2,false);
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
		var tex = TextureAssets.Projectile[Type];
		Vector2 origin = new Vector2(0, tex.Height());
		DrawData d = new(tex.Value,Projectile.Center - Main.screenPosition,null,lightColor,Projectile.rotation,origin,Projectile.scale,SpriteEffects.None);
		Main.EntitySpriteDraw(d);
		float glowPercent = MathF.Sin(Utils.Remap(Projectile.localAI[2], 0, 30, MathHelper.PiOver2, MathHelper.Pi));
		if (Projectile.localAI[2] > 0 && glowPercent > 0)
		{
			Main.EntitySpriteDraw(d with { color = Color.SlateBlue with { A = 0} * glowPercent });
			glowPercent = MathF.Sin(Utils.Remap(Projectile.localAI[2], 0, 15, MathHelper.PiOver2, MathHelper.Pi));
			Main.EntitySpriteDraw(d with { color = Color.SlateBlue with { A = 0 } * glowPercent, scale = d.scale * (1f + Projectile.localAI[2] * 0.03f)});
		}
		return false;
	}
}