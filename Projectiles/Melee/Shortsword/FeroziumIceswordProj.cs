using Avalon.Common.Interfaces;
using Avalon.Items.Weapons.Melee.Shortswords;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Shortsword;

public class FeroziumIceswordProj : ModProjectile, ISyncedOnHitEffect
{
	public override string Texture => ModContent.GetInstance<FeroziumIceSword>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<FeroziumIceSword>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.GoldShortswordStab);
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
		Projectile.timeLeft = 10;
	}
	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		return Collision.CheckAABBvLineCollision2(targetHitbox.TopLeft(), targetHitbox.Size(), Main.player[Projectile.owner].MountedCenter, Main.player[Projectile.owner].MountedCenter + Projectile.Center.DirectionTo(Main.player[Projectile.owner].MountedCenter) * -160 );
	}
	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4;
		Vector2 normalizedVel = Vector2.Normalize(Projectile.velocity);
		Projectile.position += normalizedVel * 16;
		Projectile.localAI[2]++;
		if (Projectile.localAI[2] == 1)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Snow, normalizedVel.RotatedByRandom(0.3f) * Main.rand.NextFloat(10,20));
				d.noGravity = true;
			}
			//for(int i = 0; i < 1; i++)
			//{
			//	var p = VanillaParticles.RequestPrettySparkleParticle();
			//	p.ColorTint = new Color(0,Main.rand.NextFloat(),1,0);
			//	p.LocalPosition = Projectile.Center;
			//	p.Scale = new Vector2(3, 1).RotatedByRandom(0.1f) * Main.rand.NextFloat(0.5f,0.8f);
			//	p.Rotation = MathHelper.PiOver2 + Main.rand.NextFloat(-0.2f, 0.2f);
			//	p.TimeToLive = Main.rand.Next(20, 30);
			//	p.Velocity = normalizedVel.RotatedByRandom(0.3f) * Main.rand.NextFloat(5, 20);
			//	p.AccelerationPerFrame = -p.Velocity / p.TimeToLive;
			//	Main.ParticleSystem_World_OverPlayers.Add(p);
			//}
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type];
		int originOffsetFromEnd = 10;
		DrawData d = new DrawData(tex.Value,Projectile.Center - Main.screenPosition,null,lightColor with { A = 200} * 0.85f, Projectile.rotation, tex.Size() / 2, Projectile.scale, SpriteEffects.None);

		float glowOpacity = MathF.Sin((Projectile.localAI[2] / 10f) * MathHelper.Pi);

		if(Projectile.direction * Main.player[Projectile.owner].gravDir == -1)
		{
			d.rotation += MathHelper.PiOver2;
			d.effect = SpriteEffects.FlipHorizontally;
		}
		Main.EntitySpriteDraw(d);
		var sparkleTex = TextureAssets.Extra[ExtrasID.ThePerfectGlow];
		Vector2 normalizedVel = Vector2.Normalize(Projectile.velocity);
		DrawData sparkle = new DrawData(sparkleTex.Value,Projectile.Center - Main.screenPosition + normalizedVel * 64, null, Color.DodgerBlue with { A = 0} * Projectile.Opacity * glowOpacity, Projectile.rotation + MathHelper.PiOver4, sparkleTex.Size() / 2, new Vector2(1,3 * glowOpacity), SpriteEffects.None);
		for (int i = 0; i < 2; i++)
		{
			Main.EntitySpriteDraw(sparkle);
			Main.EntitySpriteDraw(sparkle with { color = Color.White with { A = 0 } * Projectile.Opacity * 0.5f * glowOpacity * glowOpacity, scale = sparkle.scale * 0.8f });
			sparkle.rotation += MathHelper.PiOver2;
			sparkle.scale.Y /= 3;
		}
		return false;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Main.player[Projectile.owner].RefreshExtraJumps();
	}
	public bool SyncedOnHitNPC(Player player, NPC target, Rectangle attackHitbox, int damage, float knockback, bool crit, int hitDirection, Projectile? projectile)
	{
		SoundEngine.PlaySound(SoundID.Item28 with { pitch = 0.5f, pitchVariance = 0.5f, volume = 0.5f, MaxInstances = 10 }, Projectile.position);
		Vector2 closestPoint = target.Hitbox.ClosestPointInRect(attackHitbox.Center());
		for (int i = 0; i < 3; i++)
		{
			var p = VanillaParticles.RequestPrettySparkleParticle();
			p.ColorTint = new Color(0.25f,Main.rand.NextFloat(0.25f,1f),1,0);
			p.LocalPosition = closestPoint;
			p.Scale = new Vector2(3, Main.rand.NextFloat(1,2)).RotatedByRandom(0.1f) * Main.rand.NextFloat(0.6f, 1.2f);
			p.DrawHorizontalAxis = false;
			p.Rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + Main.rand.NextFloat(-0.3f,0.3f);
			p.TimeToLive = Main.rand.Next(10, 15);
			Main.ParticleSystem_World_OverPlayers.Add(p);
		}
		float iterations = 20;
		for(int i = 0; i < iterations; i++)
		{
			Vector2 vect = Main.rand.NextVector2CircularEdge(1, 1);
			Dust d = Dust.NewDustPerfect(player.Center + vect * 46, DustID.Cloud, vect * Main.rand.NextFloat(-5,-2));
			d.noGravity = true;
		}
		return true;
	}
}

