using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class CrystalSkullProj : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.MeleeNoSpeed;
		Projectile.width = 100;
		Projectile.height = 100;
		Projectile.penetrate = -1;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 position = Projectile.Center - Main.screenPosition;
		Texture2D value = TextureAssets.Projectile[Projectile.type].Value;
		Rectangle rectangle = value.Frame(1, 2);
		Rectangle value2 = value.Frame(1, 2, 0, 1);
		Vector2 origin = rectangle.Size() * new Vector2(0.03f, 0.5f);
		float num15 = 180f;
		float num16 = Projectile.ai[0] / ((float)Math.PI * 2f) + Projectile.localAI[0] / num15;
		float num17 = Utils.GetLerpValue(0f, 30f, Projectile.localAI[0], clamped: true) * Utils.GetLerpValue(num15, num15 - 30f, Projectile.localAI[0], clamped: true);
		Color color2 = Main.hslToRgb(num16 % 1f, 1f, 1f) * num17;
		float lerpValue = Utils.GetLerpValue(40f, 60f, Projectile.localAI[0], clamped: true);
		Vector2 vector2 = new Vector2(1f, MathHelper.Lerp(0.25f, 0.7f, lerpValue)) * Projectile.scale;
		Color value3 = Main.hslToRgb((num16 + 0.3f) % 1f, 1f, MathHelper.Lerp(0.3f, 0.66f, lerpValue)) * num17;

		value3 = Color.Lerp(value3, Color.White, 0.1f);
		value3.A /= 2;
		Main.spriteBatch.Draw(value, position, value2, value3, Projectile.rotation, origin, vector2 * 1.2f, SpriteEffects.None, 0f);
		Color value4 = Main.hslToRgb((num16 + 0.15f) % 1f, 1f, MathHelper.Lerp(0.3f, 0.5f, lerpValue)) * num17;

		value4 = Color.Lerp(value4, Color.White, 0.1f);
		value4.A /= 2;
		Main.spriteBatch.Draw(value, position, value2, value4, Projectile.rotation, origin, vector2 * 1.1f, SpriteEffects.None, 0f);
		Main.spriteBatch.Draw(value, position, rectangle, color2 * 0.5f, Projectile.rotation, origin, vector2, SpriteEffects.None, 0f);
		Main.spriteBatch.Draw(value, position, value2, color2 * lerpValue, Projectile.rotation, origin, vector2, SpriteEffects.None, 0f);
		return false;
	}
	public override void AI()
	{
		if (Projectile.localAI[0] == 0f)
			SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);

		Projectile.localAI[0] += 1f;
		float num = 180f;
		float num2 = (float)Math.PI / 9f;
		if (Projectile.localAI[0] >= num)
		{
			Projectile.Kill();
			return;
		}

		Projectile.alpha -= 15;
		if (Projectile.alpha < 0)
			Projectile.alpha = 0;

		Projectile.scale = Utils.GetLerpValue(0f, 20f, Projectile.localAI[0], clamped: true) * Utils.GetLerpValue(num, num - 60f, Projectile.localAI[0], clamped: true) * 0.2f;
		float lerpValue = Utils.GetLerpValue(10f, num, Projectile.localAI[0], clamped: true);
		Projectile.rotation = Projectile.ai[0] + lerpValue * num2;
		int num3 = (int)Projectile.ai[1];
		if (Main.player.IndexInRange(num3))
		{
			Player player = Main.player[num3];
			if (player.active)
				Projectile.Center = player.Center;

			Projectile.velocity = Vector2.Zero;
			Vector2 vector = Projectile.rotation.ToRotationVector2();
			Vector3 v3_ = Main.hslToRgb((Projectile.ai[0] / ((float)Math.PI * 2f) + Projectile.localAI[0] / num) % 1f, 1f, 0.85f).ToVector3() * Projectile.scale;
			float num4 = 800f * Projectile.scale;
			DelegateMethods.v3_1 = v3_;
			for (float num5 = 0f; num5 <= 1f; num5 += 1f / 12f)
			{
				Point point = (Projectile.Center + vector * num4 * num5).ToTileCoordinates();
				DelegateMethods.CastLightOpen(point.X, point.Y);
			}
		}
		else
		{
			Projectile.Kill();
		}
	}
}
