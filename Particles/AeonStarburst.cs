using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;

namespace Avalon.Particles
{
    public class AeonStarburst : Particle
    {
        public int BurstTime = 14;
        public override void Update()
        {
            if (TimeInWorld == 0)
                TimeInWorld += 3;
            TimeInWorld++;
            Lighting.AddLight(Position, 0, (30 - TimeInWorld) * 0.02f, (30 - TimeInWorld) * 0.03f);
			Position += Velocity;
			Velocity *= 0.95f;
            if (TimeInWorld > BurstTime)
                Active = false;
        }
		private static Asset<Texture2D> texture;
        public override void Draw(SpriteBatch spriteBatch)
        {
			if (texture == null)
				texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/SparklyAeon");

			Rectangle frame = texture.Frame();
            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 DrawPos = Position - Main.screenPosition;

            spriteBatch.Draw(texture.Value, DrawPos, frame, Color.Lerp(new Color(Color.R, Color.G, Color.B, 64), new Color(0, 0, 0, 0), (float)TimeInWorld / BurstTime) * 2,ai1, frameOrigin, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / BurstTime)) * ai2, SpriteEffects.None, 0);
            spriteBatch.Draw(texture.Value, DrawPos, frame, Color.Lerp(new Color(255, 255, 255, 64), new Color(0, 0, 0, 0), (float)TimeInWorld / BurstTime * 0.9f) * 2, ai1, frameOrigin, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / BurstTime)) * ai2 * 0.83f, SpriteEffects.None, 0);
        }
    }
}
