using Avalon.Buffs.Debuffs;
using Avalon.Common.Interfaces;
using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Tomes;

public class DevilScythe : ModProjectile, ISyncedOnHitEffect
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 32;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.width = 48;
		Projectile.height = 48;
		Projectile.alpha = 512;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 8;
		Projectile.tileCollide = true;
		Projectile.scale = 0.9f;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.extraUpdates = 1;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 60;
		Projectile.alpha = 255;
	}
	public override void OnKill(int timeLeft)
	{
		int type = ModContent.DustType<SimpleColorableGlowyDust>();
		for (int i = 1; i < Projectile.oldPos.Length; i++)
		{
			float percent = i / (float)Projectile.oldPos.Length;
			Dust d = Dust.NewDustDirect(Projectile.oldPos[i], Projectile.width, Projectile.height, type);
			d.color = Color.Lerp(Color.Red, Color.Purple, percent * 2) with { A = 0 } * Projectile.Opacity * MathF.Pow(1f - percent,2);
			d.velocity = Projectile.oldPos[i].DirectionTo(Projectile.oldPos[i - 1]) * Projectile.oldPos[i].Distance(Projectile.oldPos[i - 1]) * 0.5f;
			d.scale *= 1.3f;
			d.fadeIn = Main.rand.NextFloat(2);
			d.noLight = true;
			d.noGravity = true;
		}
		Projectile.oldVelocity *= 0.5f;
		var sound = new SoundStyle("Terraria/Sounds/Custom/meteor_shower_", [1, 2, 3]) with{ MaxInstances = 30 };
		SoundEngine.PlaySound(sound, Projectile.position);
		SoundEngine.PlaySound(sound with { Pitch = -2.3f, Volume = 0.8f}, Projectile.position);
		for (int dustAmount = 0; dustAmount < 15; dustAmount++)
		{
			Dust d = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DesertTorch, 0f, 0f, 0, default(Color), 1f);
			d.noGravity = true;
			d.velocity *= 3;
			d.noLightEmittence = true;
			d.fadeIn = Main.rand.NextFloat(1, 1.5f);
		}
		for (int dustAmount = 0; dustAmount < 15; dustAmount++)
		{
			Dust d = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BoneTorch, 0f, 0f, 0, default(Color), 1f);
			d.noGravity = true;
			d.velocity *= 3;
			d.noLightEmittence = true;
			d.fadeIn = 1.4f;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<InfernalJudgement>(), 60 * Main.rand.Next(3,15));
	}
	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		Vector2 pos = Main.rand.NextVector2FromRectangle(target.Hitbox);
		for(int i = 0; i < 5; i++)
		{
			var p = VanillaParticles.RequestPrettySparkleParticle();

			p.Velocity = new Vector2(Main.rand.NextFloat(2,3) * (Main.rand.NextBool() ? -1 : 1),Main.rand.NextFloat(-0.3f,0.3f)).RotatedBy(Projectile.velocity.ToRotation()) * Main.rand.NextFloat(1f,1.2f);
			p.Rotation = p.Velocity.ToRotation() + MathHelper.PiOver2;

			p.ColorTint = Color.Lerp(new Color(1f, 0.3f, 0f, 0f), new Color(0.4f, 0.5f, 1f, 0f),MathF.Pow(Main.rand.NextFloat(),5));
			p.Scale = new Vector2(3, 1f);
			p.DrawHorizontalAxis = false;
			p.FadeInEnd = 3;
			p.FadeOutStart = p.FadeInEnd;
			p.FadeOutEnd = 20;
			p.LocalPosition = pos;
			Main.ParticleSystem_World_OverPlayers.Add(p);
		}
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
		for (int i = 0; i < 5; i++)
		{
			var p = VanillaParticles.RequestPrettySparkleParticle();

			p.Velocity = new Vector2(Main.rand.NextFloat(2, 3) * (Main.rand.NextBool() ? -1 : 1), Main.rand.NextFloat(-0.3f, 0.3f)).RotatedBy(Projectile.oldVelocity.ToRotation()) * Main.rand.NextFloat(1f, 1.2f);
			p.Rotation = p.Velocity.ToRotation() + MathHelper.PiOver2;

			p.ColorTint = Color.Lerp(new Color(1f, 0.3f, 0f, 0f), new Color(0.4f, 0.5f, 1f, 0f), MathF.Pow(Main.rand.NextFloat(), 5));
			p.Scale = new Vector2(3, 1f);
			p.DrawHorizontalAxis = false;
			p.FadeInEnd = 3;
			p.FadeOutStart = p.FadeInEnd;
			p.FadeOutEnd = 20;
			p.LocalPosition = Projectile.Center + Vector2.Normalize(oldVelocity) * 10;
			Main.ParticleSystem_World_OverPlayers.Add(p);
		}

		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = -oldVelocity.X;
		}
		if (Projectile.velocity.Y != oldVelocity.Y)
		{
			Projectile.velocity.Y = -oldVelocity.Y;
		}
		Projectile.penetrate--;
		return false;
	}
	public override void AI()
	{
		if (Projectile.Opacity == 0)
			Projectile.localAI[0] = Projectile.direction;
		Projectile.Opacity += 0.02f;
		Projectile.localAI[0] = MathHelper.Clamp(Projectile.localAI[0] + Projectile.direction * 0.05f, -1, 1);
		Projectile.rotation += Projectile.localAI[0] * 0.025f * Projectile.velocity.Length();
		Projectile.ai[0] += 1f;
		Projectile.velocity += Vector2.Normalize(Projectile.velocity) * 0.06f;
		if (Main.rand.NextBool(3))
		{
			Dust d2 = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DesertTorch, 0f, 0f, 0, default(Color), Projectile.Opacity);
			d2.noGravity = true;
			d2.velocity = Projectile.velocity * 0.5f;
			d2.noLightEmittence = Main.rand.NextBool();
		}
		if (Main.rand.NextBool(9))
		{
			Dust d = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BoneTorch, 0f, 0f, 0, default(Color), Projectile.Opacity * 0.8f);
			d.noGravity = true;
			d.velocity = Projectile.velocity * 0.5f;
			d.noLightEmittence = Main.rand.NextBool();
		}
		//int target = Projectile.FindTargetWithLineOfSight(500);
		//if(target != -1)
		//{
		//	NPC npc = Main.npc[target];
		//	Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(npc.Center) * Projectile.velocity.Length(), Utils.Remap(Projectile.ai[0],0,300,0,0.2f));
		//}
		if (Projectile.velocity.Length() >= 16)
		{
			Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 16;
		}
		//if (Projectile.velocity.Length() < Projectile.oldVelocity.Length())
		//{
		//	Projectile.velocity = Vector2.Normalize(Projectile.velocity) * Projectile.oldVelocity.Length();
		//}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type].Value;
		var effect = Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
		var frame = tex.Frame(1, 2, 0, 1);
		Vector2 origin = frame.Size() / 2;
		for (int i = Projectile.oldPos.Length - 1; i >= 0; i--)
		{
			float percent = i / (float)Projectile.oldPos.Length;
			Main.EntitySpriteDraw(tex, Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2, frame, Color.Lerp(Color.Red,Color.Purple,percent * 2) with { A = 64 } * Projectile.Opacity * 0.2f * (1f - percent), Projectile.oldRot[i], origin, (Projectile.scale + (0.5f - percent)) * Projectile.Opacity, effect);
		}
		frame.Y -= frame.Height;
		Main.EntitySpriteDraw(tex,Projectile.Center - Main.screenPosition, frame, Color.White * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale * Projectile.Opacity, effect);

		Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, frame, Color.White with { A = 0 } * Projectile.Opacity * Main.masterColor, Projectile.rotation, origin, Projectile.scale * Projectile.Opacity, effect);

		Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, frame, Color.White with { A = 0 } * Projectile.Opacity * 0.3f, Projectile.rotation, origin, Projectile.scale * 1.5f * Projectile.Opacity, effect);
		Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, frame, Color.White with { A = 0 } * Projectile.Opacity * 0.1f, Projectile.rotation, origin, Projectile.scale * 2.5f * Projectile.Opacity, effect);
		return false;
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = Projectile.width - 36;
		height = Projectile.height - 36;
		return true;
	}
}
