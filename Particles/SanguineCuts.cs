using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace Avalon.Particles
{
	public class SanguineCuts : Particle
	{
		public Vector2 Velocity;
		float Rotation;
		float SideDistance;

		public SanguineCuts(Vector2 Velocity)
		{
			this.Velocity = Velocity;
			SideDistance = Main.rand.Next(12, 24);
		}
		public override void Update()
		{
			if(Main.timeForVisualEffects % 4 == 0)
			{
				TimeInWorld += 1;
			}
			Position += Velocity;
			Velocity.Y += 0.08f;
			Rotation = Velocity.ToRotation() + MathHelper.PiOver2;
			if (TimeInWorld > 24)
				Active = false;
			SideDistance -= 0.1f;

			int num15 = Dust.NewDust(Position + Main.rand.NextVector2Circular(30,8), 0, 0, DustID.Blood, 0, 2, 140, default(Color), 1f);
			Main.dust[num15].noGravity = true;
			Main.dust[num15].velocity.X *= 0.1f;
		}
		public override void Draw(SpriteBatch spriteBatch, Vector2 screenpos)
		{
			Texture2D texture = TextureAssets.Extra[98].Value;
			Rectangle frame = texture.Frame();
			Vector2 frameOrigin = frame.Size() / 2f;
			Vector2 DrawPos = Position - screenpos;
			float Scale = 1;
			Color Color = default;
			for (int i = 0; i < 24; i++)
			{
				Color = Color.DarkRed * (1f - ((float)TimeInWorld / 24)) * (1 - (i / 12f)) * 0.5f;
				Scale -= 0.01f;
				spriteBatch.Draw(texture, DrawPos - Velocity * i, frame, Color, Rotation, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 2f) * Scale, SpriteEffects.None, 0);
				spriteBatch.Draw(texture, DrawPos - Velocity * i + new Vector2(SideDistance, 0), frame, Color, Rotation, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * Scale, SpriteEffects.None, 0);
				spriteBatch.Draw(texture, DrawPos - Velocity * i + new Vector2(-SideDistance, 0), frame, Color, Rotation, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * Scale, SpriteEffects.None, 0);
			}
			Scale = 1;
			Color = Color.Red * (1f - ((float)TimeInWorld / 24));
			//for (int i = 0; i < 2; i++)
			//{
				spriteBatch.Draw(texture, DrawPos, frame, Color, Rotation, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 2f) * Scale, SpriteEffects.None, 0);
				spriteBatch.Draw(texture, DrawPos + new Vector2(SideDistance, 0), frame, Color, Rotation, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * Scale, SpriteEffects.None, 0);
				spriteBatch.Draw(texture, DrawPos + new Vector2(-SideDistance, 0), frame, Color, Rotation, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * Scale, SpriteEffects.None, 0);
				Color = Color.Black * (1f - ((float)TimeInWorld / 64));
			//AI2 = 0.7f;
			//}
			Color = Color.Black * (1f - ((float)TimeInWorld / 64));
			Scale = 0.7f;
			spriteBatch.Draw(texture, DrawPos, frame, Color, Rotation, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 2f) * Scale, SpriteEffects.None, 0);
			spriteBatch.Draw(texture, DrawPos + new Vector2(SideDistance, 0), frame, Color, Rotation, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * Scale, SpriteEffects.None, 0);
			spriteBatch.Draw(texture, DrawPos + new Vector2(-SideDistance, 0), frame, Color, Rotation, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * Scale, SpriteEffects.None, 0);
		}
	}
}
