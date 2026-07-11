using Avalon;
using Avalon.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Renderers;

namespace Avalon.Particles
{
	public class StarTorch : BaseParticle
	{
		float ai2;
		float Scale;
		Vector2 Velocity;

		public StarTorch(Vector2 position, float ai2, float scale, Vector2 velocity)
		{
			Position = position;
			this.ai2 = ai2;
			Scale = scale;
			Velocity = velocity;
		}
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			Position += (Velocity + new Vector2(0, Main.rand.NextFloat(0.65f, 0.80f))) * 0.3f;

			if (TimeInWorld > 140)
				Active = false;
		}
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			var texture = AssetReferences.Assets.Textures.StarTorch.Asset;
			Vector2 DrawPos = Position + settings.AnchorPosition;
			//spriteBatch.Draw(texture, DrawPos, frame, Color, 0f, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 1.5f) * Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * 0.3f, 0f, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 1) * 0.20f) * 1f) * 0.7f * Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(texture, DrawPos, frame, Color, MathHelper.PiOver2, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.16f) * (ai1 + 1)) * Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * 0.3f, MathHelper.PiOver2, frameOrigin, new Vector2(1, (float)Math.Sin((float)(TimeInWorld + 1) * 0.2f) * (ai1 + 1)) * 0.7f * Scale, SpriteEffects.None, 0);
			spritebatch.Draw(texture.Value, DrawPos, null, new Color(1f, 1f, 1f, 0.5f), ai2 * TimeInWorld / 4f, texture.Size() / 2, new Vector2(1.4f, (float)Math.Sin(TimeInWorld * 0.025f) * 1.5f) * Scale, SpriteEffects.None, 0);
		}
	}
}
