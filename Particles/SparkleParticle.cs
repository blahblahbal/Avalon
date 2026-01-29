using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace Avalon.Particles;

public class SparkleParticle : Particle
{
	public float FadeInNormalizedTime = 0.05f;
	public float FadeOutNormalizedTime = 0.9f;
	public float TimeToLive = 60f;
	public Color ColorTint;
	public float Opacity;
	public float AdditiveAmount = 1f;
	public float FadeInEnd = 20f;
	public float FadeOutStart = 30f;
	public float FadeOutEnd = 45f;
	public bool DrawHorizontalAxis = true;
	public bool DrawVerticalAxis = true;

	public Vector2 AccelerationPerFrame;

	public Vector2 Velocity;

	public float Rotation;

	public float RotationVelocity;

	public float RotationAcceleration;

	public Vector2 Scale;

	public Vector2 ScaleVelocity;

	public Vector2 ScaleAcceleration;
	public override void Update()
	{
		Velocity += AccelerationPerFrame;
		Position += Velocity;
		RotationVelocity += RotationAcceleration;
		Rotation += RotationVelocity;
		ScaleVelocity += ScaleAcceleration;
		Scale += ScaleVelocity;

		Opacity = Utils.GetLerpValue(0f, FadeInNormalizedTime, TimeInWorld / TimeToLive, clamped: true) * Utils.GetLerpValue(1f, FadeOutNormalizedTime, TimeInWorld / TimeToLive, clamped: true);
		if (TimeInWorld >= TimeToLive)
		{
			Active = false;
		}
	}
	public override void Draw(SpriteBatch spriteBatch, Vector2 ScreenPos)
	{
		Color color = Color.White * Opacity * 0.9f;
		color.A /= 2;
		Texture2D value = TextureAssets.Extra[ExtrasID.SharpTears].Value;
		Color color2 = ColorTint * Opacity * 0.5f;
		color2.A = (byte)((float)(int)color2.A * (1f - AdditiveAmount));
		Vector2 origin = value.Size() / 2f;
		Color color3 = color * 0.5f;
		float t = TimeInWorld / TimeToLive * 60f;
		float num = Utils.GetLerpValue(0f, FadeInEnd, t, clamped: true) * Utils.GetLerpValue(FadeOutEnd, FadeOutStart, t, clamped: true);
		Vector2 vector = new Vector2(0.3f, 2f) * num * Scale;
		Vector2 vector2 = new Vector2(0.3f, 1f) * num * Scale;
		color2 *= num;
		color3 *= num;
		Vector2 position = Position - ScreenPos;
		SpriteEffects effects = SpriteEffects.None;
		if (DrawHorizontalAxis)
		{
			spriteBatch.Draw(value, position, null, color2, (float)Math.PI / 2f + Rotation, origin, vector, effects, 0f);
		}
		if (DrawVerticalAxis)
		{
			spriteBatch.Draw(value, position, null, color2, 0f + Rotation, origin, vector2, effects, 0f);
		}
		if (DrawHorizontalAxis)
		{
			spriteBatch.Draw(value, position, null, color3, (float)Math.PI / 2f + Rotation, origin, vector * 0.6f, effects, 0f);
		}
		if (DrawVerticalAxis)
		{
			spriteBatch.Draw(value, position, null, color3, 0f + Rotation, origin, vector2 * 0.6f, effects, 0f);
		}
	}
}
