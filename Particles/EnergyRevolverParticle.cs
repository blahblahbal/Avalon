using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class EnergyRevolverParticle : Particle
    {
        public override void Update()
        {
            if (TimeInWorld == 0)
                TimeInWorld += 3;
            TimeInWorld++;

            if (TimeInWorld > ai3)
                Active = false;
            Position += Velocity;
            Velocity *= 0.95f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("Avalon/Assets/Textures/GlowCircle");
            Rectangle frame = texture.Frame();
            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 DrawPos = Position - Main.screenPosition;

            spriteBatch.Draw(texture, DrawPos, frame, Color.Lerp(new Color(Color.R, Color.G, Color.B, 0), new Color(0, 0, 0, 0), (float)TimeInWorld / ai3) * 2,ai1, frameOrigin, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / ai3)) * ai2, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, Color.Lerp(new Color(255, 255, 255, 0), new Color(0, 0, 0, 0), (float)TimeInWorld / ai3) * 2, ai1, frameOrigin, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / ai3)) * ai2 * 0.8f, SpriteEffects.None, 0);
        }
    }
}
