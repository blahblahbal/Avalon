using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;

namespace Avalon.Particles
{
    public class TrueAeonSlash : BaseParticle
    {
		public Vector2 Offset = Vector2.Zero;
		public Player Owner = null;
		public float Scale = 1f;
		public float RotationAmount = 0f;
		public int Direction = 1;
		public float MaxTime = 60;
		public Vector2 AccumulatedDistance = Vector2.Zero;
		public Color Color;
		public TrueAeonSlash(Vector2 offset, Player owner, float scale, float rotationAmount, int direction, float maxTime, Color color)
		{
			Offset = offset;
			Owner = owner;
			Scale = scale;
			RotationAmount = rotationAmount;
			Direction = direction;
			MaxTime = maxTime;
			Color = color;
		}
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			Position = Owner.Center + AccumulatedDistance;

			AccumulatedDistance -= Owner.velocity * TimeInWorld / MaxTime;

			if (TimeInWorld > MaxTime)
				Active = false;
		}
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Texture2D tex = TextureAssets.Extra[98].Value;
			float percent = TimeInWorld / MaxTime;
			float sin = MathF.Sin(percent * MathHelper.Pi);
			int trailLength = Math.Min(TimeInWorld, 10);
			for (int i = 1; i < trailLength; i++)
			{
				Vector2 pos = Position + Offset.RotatedBy(RotationAmount * ((TimeInWorld - i) / MaxTime) * Direction);
				float trailPercent = 1f - ((float)i / trailLength);
				spritebatch.Draw(tex, pos + settings.AnchorPosition, null, Color * sin * 0.3f * trailPercent, pos.DirectionTo(Position).ToRotation(), tex.Size() / 2, new Vector2(sin * trailPercent * 1.2f, 1f) * Scale, SpriteEffects.None, 0);
			}
			for (int i = 0; i < 2; i++)
			{
				spritebatch.Draw(tex, Position + Offset.RotatedBy(RotationAmount * percent * Direction) + settings.AnchorPosition, null, Color with { A = 128 } * sin, i * MathHelper.PiOver2, tex.Size() / 2, Scale * sin, SpriteEffects.None, 0);
				spritebatch.Draw(tex, Position + Offset.RotatedBy(RotationAmount * percent * Direction) + settings.AnchorPosition, null, new Color(1f, 1f, 1f, 0f) * sin, i * MathHelper.PiOver2, tex.Size() / 2, new Vector2(0.25f, 0.8f) * Scale * sin, SpriteEffects.None, 0);
			}
		}
    }
}
