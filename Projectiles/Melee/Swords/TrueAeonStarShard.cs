using Avalon.Core;
using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.ThrownItems;

namespace Avalon.Projectiles.Melee.Swords;

public class TrueAeonStarShard : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 14;
		Projectile.height = 14;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
	}
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Type] = 8;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		for (int i = 0; i < 8; i++)
		{
			float fade = (1f - i / 8f);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.oldPos[i] - Main.screenPosition + new Vector2(7), null, new Color(Math.Abs(MathF.Sin(Projectile.whoAmI)), fade, 1f, 0.5f) * Projectile.Opacity * fade, Projectile.oldRot[i], new Vector2(7, 9), Projectile.scale, SpriteEffects.None);
		}
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, new Color(1f, 1f, 1f, 0.5f) * Projectile.Opacity, Projectile.rotation, new Vector2(7, 9), Projectile.scale, SpriteEffects.None);
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, Color.Black * Projectile.Opacity, Projectile.rotation, new Vector2(7, 9), Projectile.scale * 0.7f, SpriteEffects.None);
		return false;
	}
	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		Projectile.ai[0]++;
		if (Projectile.ai[0] == 1)
		{
			Projectile.ai[1] = Projectile.FindTargetWithLineOfSight();
		}
		if (Projectile.ai[0] > 120)
			Projectile.ai[1] = -1;
		if (Projectile.ai[1] != -1)
		{
			if (Projectile.ai[0] > 30)
			{
				Projectile.velocity = Vector2.Lerp(Projectile.Center.DirectionTo(Main.npc[(int)Projectile.ai[1]].Center) * Projectile.ai[2], Projectile.velocity, 0.98f);
				if (!Main.npc[(int)Projectile.ai[1]].active)
					Projectile.ai[0] = 0;
			}
		}
		else
		{
			Projectile.alpha += 5;
			Projectile.velocity *= 0.98f;
			if (Projectile.alpha > 255)
			{
				Projectile.Opacity = 0;
				Projectile.Kill();
			}
		}
	}
	public override void OnKill(int timeLeft)
	{
		if (Projectile.Opacity == 0)
			return;
		var t = AssetReferences.Assets.Textures.InverseGlowRing.Asset;
		t.Wait();
		for (int i2 = 0; i2 < 2; i2++)
		{
			var ring = VanillaParticles.RequestFadingParticle();
			ring.SetBasicInfo(t, null, Vector2.Zero, Projectile.Center);
			int time = Main.rand.Next(15,30);
			ring.SetTypeInfo(time);
			ring.Scale = Vector2.Zero;
			ring.ScaleVelocity = Vector2.One.RotatedByRandom(1f) * 0.1f;
			ring.ScaleAcceleration = ring.ScaleVelocity / -time * 2;
			ring.FadeInNormalizedTime = 0.1f;
			ring.FadeOutNormalizedTime = 0.1f;
			ring.ColorTint = i2 == 0 ? new Color(Main.rand.Next(200), 100, 255, 0) : Color.Black * 0.5f;
			ring.Rotation = Main.rand.NextFloatDirection();
			ring.RotationVelocity = Main.rand.NextFloat(-0.1f, 0.1f);
			Main.ParticleSystem_World_OverPlayers.Add(ring);
		}
		Main.ParticleSystem_World_OverPlayers.Add(new AeonStarburst(Projectile.Center, Vector2.Zero, new Color(Main.rand.Next(200), 100, 255, 64), Color.Black, Main.rand.NextFloatDirection(), Main.rand.NextFloat(1,2), 24));
		int type = ModContent.DustType<SimpleColorableGlowyDust>();
		for (int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, type, 0, 0, 0, default, 1f);
			d.color = new Color(Main.rand.Next(200), 100, 255, 0) * Projectile.Opacity;
			d.noGravity = true;
			d.noLightEmittence = true;
			d.fadeIn = Main.rand.NextFloat(0.5f, 1.5f);
			d.velocity = Main.rand.NextVector2Circular(3, 3) + Projectile.velocity;
		}
	}
}


