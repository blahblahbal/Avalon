using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Avalon;

namespace Avalon.Particles.OldParticleSystem
{
	public class StarTorch : LegacyParticleDeleteSoon
	{
		public override void Update()
		{
			if (Main.timeForVisualEffects % 4 == 0)
			{
				TimeInWorld++;
			}

			//if(TimeInWorld == 1)
			//{
			//    for (int i = 0; i < 5; i++)
			//    {
			//        for (int k = 0; k < 3; k++)
			//        {
			//            Dust d = Dust.NewDustPerfect(Position, DustID.LifeDrain, new Vector2(0, Main.rand.NextFloat(-3, -4)).RotatedBy(i * 1.25664f).RotatedByRandom(0.4f));
			//            d.noGravity = true;
			//        }
			//    }
			//}
			Position += (Velocity + new Vector2(0, Main.rand.NextFloat(0.65f, 0.80f))) * 1.2f;

			if (TimeInWorld > 35)
				Active = false;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/StarTorch").Value;
			Rectangle frame = texture.Frame();
			Vector2 frameOrigin = frame.Size() / 2f;
			Vector2 DrawPos = Position - Main.screenPosition;
			//spriteBatch.Draw(texture, DrawPos, frame, Color, 0f, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 1.5f) * Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * 0.3f, 0f, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 1) * 0.20f) * 1f) * 0.7f * Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(texture, DrawPos, frame, Color, MathHelper.PiOver2, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.16f) * (ai1 + 1)) * Scale, SpriteEffects.None, 0);
			//spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * 0.3f, MathHelper.PiOver2, frameOrigin, new Vector2(1, (float)Math.Sin((float)(TimeInWorld + 1) * 0.2f) * (ai1 + 1)) * 0.7f * Scale, SpriteEffects.None, 0);
			spriteBatch.Draw(texture, DrawPos, frame, Color, ai2 * TimeInWorld, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 1.5f) * Scale, SpriteEffects.None, 0);
		}
	}
}
