using Avalon.Common.Interfaces;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class RhodiumGreatswordBeam : ModProjectile, ISyncedOnHitEffect
{
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TheHorsemansBlade}";
	public override LocalizedText DisplayName => ModContent.GetInstance<RhodiumGreatsword>().DisplayName;

	public override void SetDefaults()
	{
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.aiStyle = -1;
		Projectile.width = Projectile.height = 32;
		Projectile.penetrate = 15;
		Projectile.stopsDealingDamageAfterPenetrateHits = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		Projectile.timeLeft = 120;
	}
	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		if (Projectile.localAI[0] == 0)
		{
			Projectile.scale = player.GetAdjustedItemScale(player.HeldItem) * 1.2f;
		}
		int width = (int)(Projectile.scale * 120);
		Projectile.Resize(width, width);
		Projectile.scale -= 0.015f;
		float pwr = Projectile.ai[2];
		Projectile.ai[1] += Utils.Remap(pwr,0,RhodiumGreatswordPlayer.maxPower,0.15f,0.35f) * Projectile.ai[0];
		Projectile.rotation = Projectile.ai[1];

		Projectile.localAI[0]++;
		float maxTime = 30 + (pwr * 5);
		Projectile.Opacity = Utils.Remap(Projectile.localAI[0] / maxTime,0,1,0.5f,1f);
		if (Projectile.localAI[0] > maxTime)
		{
			Projectile.Kill();
		}
		pwr /= RhodiumGreatswordPlayer.maxPower;

		Projectile.velocity *= Utils.Remap(pwr, 0, 1, 0.95f, 0.985f);

		pwr *= Utils.Remap(Projectile.Opacity, 0f, 0.6f, 0f, 1f) * Utils.Remap(Projectile.Opacity, 0.6f, 1f, 1f, 0f);
		int dType = ModContent.DustType<SimpleColorableGlowyDust>();
		Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2CircularEdge(Projectile.scale, Projectile.scale) * Main.rand.NextFloat(30, 80), dType, Projectile.velocity / 10f, 100);
		d.noGravity = true;
		d.velocity *= 2f;
		d.scale *= 0.7f;
		d.color = Color.Lerp(new Color(1f, 0.5f, 0.3f, 0f), new Color(1f, 0.3f, Main.rand.NextFloat(0.3f, 0.6f), 0f), Main.rand.NextFloat()) * (0.4f + pwr * 0.6f);
		d.fadeIn = Main.rand.NextFloat(1.3f);

		for (int j = 0; j < 2; j++)
		{
			Dust d2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2CircularEdge(Projectile.scale, Projectile.scale) * Main.rand.NextFloat(80,100), dType, Projectile.velocity / 10f, 100);
			d2.noGravity = true;
			d2.velocity *= 2f;
			d2.scale *= 0.9f;
			d2.color = new Color(0.8f, 0.2f, Main.rand.NextFloat(0.2f, 0.4f), 0f) * (0.2f + pwr * 0.6f);
		}

	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity = oldVelocity * 0.6f;
		return false;
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = height = 8;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		Vector2 point = Main.rand.NextVector2FromRectangle(target.Hitbox);
		for (int i = 0; i < 3; i++)
		{
			SparkleParticle p = new();
			p.ColorTint = new Color(1f, 0.3f, Main.rand.NextFloat(0.3f, 0.6f), 0f);
			p.HighlightColor = new Color(1f, 0.7f, 0.4f, 0f);
			p.FadeInEnd = Main.rand.NextFloat(4, 7);
			p.FadeOutStart = p.FadeInEnd + 5;
			p.FadeOutEnd = Main.rand.NextFloat(17, 21);
			p.Scale = new Vector2(2.5f, 1.25f);
			p.Rotation = Main.rand.NextFloat(-0.1f, 0.1f) + (i * MathHelper.TwoPi / 3f);
			p.DrawHorizontalAxis = false;
			ParticleSystem.NewParticle(p, point);
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		float percent = Projectile.ai[2] / (float)RhodiumGreatswordPlayer.maxPower;
		DrawSlash(new Color(0.5f, 0.05f, 0.3f, 1f) * (0.2f + percent * 0.8f), new Color(0.9f, 0.2f, 0.3f, 0.6f) * (0.2f + percent * 0.8f), new Color(1f, 0.2f, 0.2f, 0.8f) * (0.2f + percent * 0.8f), new Color(0.7f, 0.4f, 0.2f, 0f) * (0.4f + percent * 0.6f), 0, 1f, 0f, 0.1f + (Projectile.localAI[0] * 0.01f), -0.3f, true, true);
		return false;
	}
	private void DrawSlash(Color backColor, Color middleColor, Color frontColor, Color white, int sparkleAlpha, float VisualScaleMultiplier, float sparkleRotation, float splay, float rotationOffset, bool sparkle, bool fullBright = false)
	{
		splay *= Main.player[Projectile.owner].direction;
		rotationOffset *= Main.player[Projectile.owner].direction;
		Projectile proj = Projectile;
		float modifiedProjScale = proj.scale * VisualScaleMultiplier;
		Vector2 vector = proj.Center - Main.screenPosition;
		Asset<Texture2D> val = TextureAssets.Projectile[proj.type];
		Rectangle rectangle = val.Frame(1, 4);
		Vector2 origin = rectangle.Size() / 2f;
		float num = modifiedProjScale * 1.1f;
		SpriteEffects effects = ((!(proj.ai[0] >= 0f)) ? SpriteEffects.FlipVertically : SpriteEffects.None);
		float num2 = Projectile.Opacity;
		float num3 = Utils.Remap(num2, 0f, 0.6f, 0f, 1f) * Utils.Remap(num2, 0.6f, 1f, 1f, 0f);
		float num4 = 0.975f;
		float fromValue = 1;
		fromValue = Utils.Remap(fromValue, 0.2f, 1f, 0f, 1f);
		Color color = backColor;
		Color color2 = middleColor;
		Color color3 = frontColor;
		Color color4 = white * num3 * 0.5f;
		color4.A = (byte)((float)(int)color4.A * (1f - fromValue));
		Color color5 = color4 * fromValue * 0.5f;
		color5.G = (byte)((float)(int)color5.G * fromValue);
		color5.B = (byte)((float)(int)color5.R * (0.25f + fromValue * 0.75f));

		//Main.spriteBatch.Draw(val.Value, vector, rectangle, color * fromValue * num3, proj.rotation + proj.ai[0] * ((float)Math.PI / 4f) * -1f * (1f - num2), origin, num, effects, 0f);
		//Main.spriteBatch.Draw(val.Value, vector, rectangle, color5 * 0.15f, proj.rotation + proj.ai[0] * 0.01f, origin, num, effects, 0f);
		//Main.spriteBatch.Draw(val.Value, vector, rectangle, color3 * fromValue * num3 * 0.3f, proj.rotation, origin, num, effects, 0f);
		//Main.spriteBatch.Draw(val.Value, vector, rectangle, color2 * fromValue * num3 * 0.5f, proj.rotation, origin, num * num4, effects, 0f);

		Main.spriteBatch.Draw(val.Value, vector, rectangle, color * fromValue * num3, proj.rotation + rotationOffset, origin, num, effects, 0f);
		Main.spriteBatch.Draw(val.Value, vector, rectangle, color5 * 0.3f, proj.rotation + splay + rotationOffset, origin, num, effects, 0f);
		Main.spriteBatch.Draw(val.Value, vector, rectangle, color3 * fromValue * num3 * 0.3f, proj.rotation + (splay * 2) + rotationOffset, origin, num, effects, 0f);
		Main.spriteBatch.Draw(val.Value, vector, rectangle, color2 * fromValue * num3 * 0.5f, proj.rotation + (splay * 4) + rotationOffset, origin, num * num4, effects, 0f);


		Main.spriteBatch.Draw(val.Value, vector, val.Frame(1, 4, 0, 3), white * 0.6f * num3, proj.rotation + rotationOffset + proj.ai[0] * 0.01f, origin, num, effects, 0f);
		Main.spriteBatch.Draw(val.Value, vector, val.Frame(1, 4, 0, 3), white * 0.5f * num3, proj.rotation + rotationOffset + proj.ai[0] * -0.05f, origin, num * 0.8f, effects, 0f);
		Main.spriteBatch.Draw(val.Value, vector, val.Frame(1, 4, 0, 3), white * 0.4f * num3, proj.rotation + rotationOffset + proj.ai[0] * -0.1f, origin, num * 0.6f, effects, 0f);

		for (float num5 = 0f; num5 < 8f; num5 += 1f)
		{
			float num6 = proj.rotation + proj.ai[0] * num5 * ((float)Math.PI * -2f) * 0.025f + Utils.Remap(num2, 0f, 1f, 0f, (float)Math.PI / 4f) * proj.ai[0];
			Vector2 drawpos = vector + num6.ToRotationVector2() * ((float)val.Width() * 0.5f - 6f) * num;
			float num7 = num5 / 9f;
			if (sparkle)
			{
				DrawPrettyStarSparkle(proj.Opacity, SpriteEffects.None, drawpos, new Color(white.R, white.G, white.B, sparkleAlpha) * num3 * num7, white, num2, 0f, 0.5f, 0.5f, 1f, num6, new Vector2(0f, Utils.Remap(num2, 0f, 1f, 3f, 0f)) * num, Vector2.One * num);
			}
		}
		Vector2 drawpos2 = vector + (proj.rotation + Utils.Remap(num2, 0f, 1f, 0f, (float)Math.PI / 4f) * proj.ai[0]).ToRotationVector2() * ((float)val.Width() * 0.5f - 4f) * num;
		if (sparkle)
		{
			DrawPrettyStarSparkle(proj.Opacity, SpriteEffects.None, drawpos2, new Color(white.R, white.G, white.B, sparkleAlpha) * num3 * 0.5f, white, num2, 0f, 0.5f, 0.5f, 1f, sparkleRotation, new Vector2(Utils.Remap(num2, 0f, 1f, 4f, 1f)) * num, Vector2.One * num);
		}
	}
	private void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawpos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
	{
		Texture2D value = TextureAssets.Extra[98].Value;
		Color color = shineColor * opacity * 0.5f;
		color.A = 0;
		Vector2 origin = value.Size() / 2f;
		Color color2 = drawColor * 0.5f;
		float num = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
		Vector2 vector = new Vector2(fatness.X * 0.5f, scale.X) * num;
		Vector2 vector2 = new Vector2(fatness.Y * 0.5f, scale.Y) * num;
		color *= num;
		color2 *= num;
		Main.EntitySpriteDraw(value, drawpos, null, color, (float)Math.PI / 2f + rotation, origin, vector, dir);
		Main.EntitySpriteDraw(value, drawpos, null, color, 0f + rotation, origin, vector2, dir);
		Main.EntitySpriteDraw(value, drawpos, null, color2, (float)Math.PI / 2f + rotation, origin, vector * 0.6f, dir);
		Main.EntitySpriteDraw(value, drawpos, null, color2, 0f + rotation, origin, vector2 * 0.6f, dir);
	}
}
