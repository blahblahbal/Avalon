using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Avalon;
using ReLogic.Content;

namespace Avalon.Particles
{
	public class StarTorch : Particle
	{
		private static Asset<Texture2D> texture;
		float ai2;
		float Scale;
		Vector2 Velocity;

		public StarTorch(float ai2, float scale, Vector2 velocity)
		{
			this.ai2 = ai2;
			Scale = scale;
			Velocity = velocity;
		}

		public override void Update()
		{
			Position += (Velocity + new Vector2(0, Main.rand.NextFloat(0.65f, 0.80f))) * 0.3f;

			if (TimeInWorld > 140)
				Active = false;
		}
		public override void Draw(SpriteBatch spriteBatch, Vector2 screenpos)
		{
			if (texture == null)
				texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/StarTorch");
			Vector2 DrawPos = Position - screenpos;
			//spriteBatch.Draw(texture, DrawPos, frame, Color, 0f, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 1.5f) * Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * 0.3f, 0f, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 1) * 0.20f) * 1f) * 0.7f * Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(texture, DrawPos, frame, Color, MathHelper.PiOver2, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.16f) * (ai1 + 1)) * Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * 0.3f, MathHelper.PiOver2, frameOrigin, new Vector2(1, (float)Math.Sin((float)(TimeInWorld + 1) * 0.2f) * (ai1 + 1)) * 0.7f * Scale, SpriteEffects.None, 0);
			spriteBatch.Draw(texture.Value, DrawPos, null, new Color(1f,1f,1f,0.5f), ai2 * TimeInWorld / 4f, texture.Size() / 2, new Vector2(1.4f, (float)Math.Sin(TimeInWorld * 0.025f) * 1.5f) * Scale, SpriteEffects.None, 0);
		}
	}
}
