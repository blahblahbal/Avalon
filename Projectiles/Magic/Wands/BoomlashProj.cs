using Avalon.Common;
using Avalon.Core;
using Avalon.Items.Weapons.Magic.Wands;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Wands;

public class BoomlashProj : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<Boomlash>().DisplayName;
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 1;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 29;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
	}
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.Flamelash);
		Projectile.DamageType = DamageClass.Magic;
		DrawOriginOffsetY = -6;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
	}
	public override void AI()
	{
		if (Projectile.position.Distance(Projectile.oldPosition) > 1f)
		{
			if (Main.rand.NextBool(3))
			{
				for (int i = 0; i < 2; i++)
				{
					Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(-10, 0).RotatedBy(Projectile.rotation), DustID.DesertTorch, Projectile.velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat(0.3f));
					d.noGravity = true;
					d.scale = 1.5f;
				}
			}
			if (Main.rand.NextBool(6))
			{
				int dusty = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
				Main.dust[dusty].noGravity = true;
				Main.dust[dusty].scale = 1f;
				Main.dust[dusty].alpha = 128;
			}
		}
		if (Projectile.velocity != Vector2.Zero)
		{
			Projectile.rotation = Projectile.velocity.ToRotation();
		}
	}
	public override void OnKill(int timeLeft)
	{
		int rand = Main.rand.Next(5, 15);
		if (!TextureAssets.Gore[GoreID.Smoke1].IsLoaded)
			Main.instance.LoadGore(GoreID.Smoke1);
		if (!TextureAssets.Gore[GoreID.Smoke2].IsLoaded)
			Main.instance.LoadGore(GoreID.Smoke2);
		if (!TextureAssets.Gore[GoreID.Smoke3].IsLoaded)
			Main.instance.LoadGore(GoreID.Smoke3);
		for (int i = 0; i < rand; i++)
		{
			var p2 = VanillaParticles.RequestFadingParticle();
			var tex2 = TextureAssets.Gore[Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1)];
			int time = Main.rand.Next(60, 120);
			p2.SetBasicInfo(tex2, null, Main.rand.NextVector2Circular(1, 1), Projectile.Center);
			p2.SetTypeInfo(time);
			p2.ColorTint = new Color(0.3f, 0.2f, Main.rand.NextFloat(0.1f, 0.2f)) * Main.rand.NextFloat(0.5f, 1);
			p2.FadeInNormalizedTime = 0.1f;
			p2.FadeOutNormalizedTime = 0.1f;
			p2.Scale = Vector2.One * Main.rand.NextFloat(1f, 1.5f);
			p2.Rotation = Main.rand.NextFloatDirection();
			p2.RotationVelocity = Main.rand.NextFloat(-0.05f, 0.05f);
			p2.AccelerationPerFrame.Y = -Main.rand.NextFloat(0.01f, 0.025f);
			Main.ParticleSystem_World_OverPlayers.Add(p2);
		}

		for (int i = 0; i < 30; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DesertTorch);
			d.velocity += Main.rand.NextVector2Circular(8, 8);
			d.noGravity = true;
			d.fadeIn = Main.rand.NextFloat(3);
		}

		var tex = AssetReferences.Assets.Textures.FireballExplosion.Asset;
		tex.Wait();
		var p = AnimatedParticle.Request();
		p.SetBasicInfo(tex, 5, Vector2.Zero, Projectile.Center);
		p.SetTypeInfo(Main.rand.Next(10, 20));
		p.ColorTint = Color.White with { A = 128 };
		p.Scale = Vector2.One * 2;
		p.Rotation = Main.rand.NextFloatDirection();
		p.RotationVelocity = Main.rand.NextFloat(-0.05f, 0.05f);
		p.ScaleVelocity = Vector2.One * Main.rand.NextFloat(-0.02f, 0.02f);
		Main.ParticleSystem_World_OverPlayers.Add(p);

		if (Main.myPlayer == Projectile.owner)
		{
			for (int i = 2; i < 7; i++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2CircularEdge(1,1) * Main.rand.NextFloat(5,8), ModContent.ProjectileType<BoomlashSeed>(), (int)(Projectile.damage * 0.85f), Projectile.knockBack, Projectile.owner, i * 3,Main.rand.Next(1,4));
			}
		}
		SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Rectangle frame2 = TextureAssets.Projectile[Type].Frame();
		Vector2 frameOrigin2 = frame2.Size() / 2f;
		Color col2 = Color.Lerp(Color.White, Color.Black, Main.masterColor);
		Vector2 stretchscale = new Vector2(Projectile.scale * 1.4f + (Main.masterColor / 2));

		Main.spriteBatch.End();
		StripRenderer.SetTexturesForTrail(TextureAssets.MagicPixel, TextureAssets.Extra[ExtrasID.RainbowRodTrailShape], TextureAssets.Extra[ExtrasID.RainbowRodTrailErosion]);
		StripRenderer.BeginSpriteBatchForBasicTrail(0.75f, -3, Projectile.Opacity * 2);
		StripRenderer.DrawStripPadded(Projectile.oldPos, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, frame2, new Color(255, 0, 0, 0), Projectile.rotation - MathHelper.PiOver2, frameOrigin2, stretchscale * 0.8f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, frame2, col2 * Projectile.Opacity, Projectile.rotation - MathHelper.PiOver2, frameOrigin2, Projectile.scale, SpriteEffects.None, 0);

		return false;
	}
	private Color StripColors(float progressOnStrip)
	{
		return Color.Lerp(Color.Lerp(new Color(255, 100, 0, 0), new Color(230, 32, 0, 0), (float)(Math.Sin((Main.timeForVisualEffects * 0.4f) + progressOnStrip * -5) * 0.5f + 0.5f)), new Color(32, 32, 32, 255), progressOnStrip) * (1f - MathF.Pow(progressOnStrip,5));
	}
	private float StripWidth(float progressOnStrip)
	{
		return MathHelper.Lerp(15, Utils.Remap(Projectile.velocity.Length(),0,6,15,32), progressOnStrip);
	}
}