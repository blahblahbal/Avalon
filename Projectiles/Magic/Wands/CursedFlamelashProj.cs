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

public class CursedFlamelashProj : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<CursedFlamelash>().DisplayName;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		Main.projFrames[Type] = 6;
	}
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.MagicMissile);
		//Projectile.extraUpdates = 1;
		Projectile.penetrate = 3;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
			target.AddBuff(BuffID.CursedInferno, 160);
	}
	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
			target.AddBuff(BuffID.CursedInferno, 160);
	}
	public override void AI()
	{
		Projectile.scale = MathHelper.Lerp(1.5f, 1.2f, MathHelper.Clamp(Projectile.velocity.Length() * 0.1f, 0, 1));

		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CursedTorch);
		d.velocity += Projectile.velocity * Main.rand.NextFloat(0.2f);	

		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 3)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
		}
		if (Projectile.frame > 5)
		{
			Projectile.frame = 0;
		}
	}

	public override void OnKill(int timeLeft)
	{
		var p = new CursedExplosionParticle(Main.rand.NextFloat(0.9f, 1.2f), Main.rand.NextFloat(MathHelper.TwoPi),Projectile.Center);
		Main.ParticleSystem_World_OverPlayers.Add(p);
		SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
		//float decreaseBy = 0.05f;
		//for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type] / 2; i++)
		//{
		//	if (Projectile.oldPos[i].DirectionFrom(Projectile.oldPos[i + 1]).Length() > 0.6f)
		//	{
		//		for (int i2 = 0; i2 < Main.rand.Next(2, 4); i2++)
		//		{
		//			Dust d = Dust.NewDustPerfect(Projectile.oldPos[i], DustID.CursedTorch, Projectile.oldPos[i].DirectionFrom(Projectile.oldPos[i + 1]).RotateRandom(0.4f) * Main.rand.NextFloat(7, 9));
		//			d.noGravity = !Main.rand.NextBool(3);
		//			d.scale = (Main.rand.NextFloat(0.25f, 0.5f) * i2) - (decreaseBy * i);
		//			d.fadeIn = (Main.rand.NextFloat(0.75f, 1f) * i2) - (decreaseBy * i * 2);
		//			//d.noLight = true;
		//			if (!d.noGravity)
		//			{
		//				d.scale *= 0.5f;
		//			}
		//		}
		//	}
		//}

		for (int i = 0; i < 30; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CursedTorch);
			d.velocity = Main.rand.NextVector2Circular(5,5);
			d.noGravity = Main.rand.NextBool();
			d.scale *= 2;
		}
		if (Main.myPlayer == Projectile.owner)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CursedFlamelashExplosion>(), Projectile.damage * 2, Projectile.knockBack * 2, Projectile.owner);
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{

		Main.spriteBatch.End();
		AssetReferences.Projectiles.Magic.Wands.CursedFlamelashNoise.Asset.Wait();
		Main.graphics.GraphicsDevice.Textures[0] = AssetReferences.Projectiles.Magic.Wands.CursedFlamelashNoise.Asset.Value;
		AssetReferences.Projectiles.Magic.Wands.CursedFlamelashColors.Asset.Wait();
		Main.graphics.GraphicsDevice.Textures[1] = AssetReferences.Projectiles.Magic.Wands.CursedFlamelashColors.Asset.Value;

		var shader = AssetReferences.Effects.CursedFlamelash.CreateTrail();
		shader.Parameters.uTransformMatrix = Main.GameViewMatrix.NormalizedTransformationmatrix;
		shader.Parameters.uTime = Main.GlobalTimeWrappedHourly;
		shader.Apply();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, shader.Shader, Main.Transform);

		StripRenderer.DrawStripPadded(Projectile.oldPos, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

		//Thanks ballsfah
		int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Type];
		Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Value.Width, frameHeight);
		Vector2 drawPos = Projectile.Center - Main.screenPosition;

		float Rot = MathHelper.Lerp(0, Projectile.velocity.ToRotation() - MathHelper.PiOver2, MathHelper.Clamp(Projectile.velocity.Length() * 0.1f, 0, 1));

		//Main.EntitySpriteDraw(texture, drawPos, frame, Color.White, Rot, new Vector2(texture.Width, frameHeight) / 2, new Vector2(Projectile.scale,MathHelper.Clamp(Projectile.velocity.Length() * 0.2f,Projectile.scale,Projectile.scale * 2f)), SpriteEffects.None, 0);
		//The line above stretches the flame with speed
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, frame, new Color(230, 255, 0, 128), Rot, new Vector2(TextureAssets.Projectile[Type].Value.Width / 2, frameHeight * 0.6f), Projectile.scale, SpriteEffects.None, 0);

		return false;
	}
	private Color StripColors(float progressOnStrip)
	{
		return Color.White;
	}
	private float StripWidth(float progressOnStrip)
	{
		return (10 + (progressOnStrip * 100)) * Utils.Remap(Projectile.position.Distance(Projectile.oldPos[1]), 0, 12, 0, 1) * (progressOnStrip > 0.5f? MathF.Sin(progressOnStrip * MathHelper.Pi) : 1);
	}
}
public class CursedFlamelashExplosion : ModProjectile
{
	public override string Texture => ModContent.GetInstance<CursedFlamelash>().Texture;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(128);
		Projectile.aiStyle = -1;
		Projectile.hide = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 20;
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
			target.AddBuff(BuffID.CursedInferno, 160);
		modifiers.HitDirectionOverride = (target.Center.X <= Projectile.Center.X) ? -1 : 1;
	}
	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
			target.AddBuff(BuffID.CursedInferno, 160);
		modifiers.HitDirectionOverride = (target.Center.X <= Projectile.Center.X) ? -1 : 1;
	}
}
