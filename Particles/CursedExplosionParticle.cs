using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class CursedExplosionParticle : Particle
    {
        int Frame;
        public override void Update()
        {
            TimeInWorld += 1;
            if(TimeInWorld % 4 == 0)
            {
                Frame++;
            }
            if(Frame > 7)
            {
                Active = false;
            }
            ai2 += 0.01f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("Avalon/Assets/Textures/CursedExplosion");
            Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>("Avalon/Assets/Textures/WhiteExplosion");
            int frameHeight = texture.Height / 7;
            Rectangle frame = new Rectangle(0, frameHeight * Frame, texture.Width, frameHeight);
            Vector2 frameOrigin = new Vector2(texture.Width) / 2;
            Vector2 DrawPos = Position - Main.screenPosition;

            float muliply = ((32 - TimeInWorld) / 28);

            spriteBatch.Draw(texture, DrawPos, frame, new Color(255,255,255,128), ai1, frameOrigin, ai2, SpriteEffects.None, 0);

            for (int i = 0; i < 8; i++)
            {
                spriteBatch.Draw(texture2, DrawPos + new Vector2(0, TimeInWorld * 0.2f).RotatedBy(i * MathHelper.PiOver4), frame, new Color(255, 255, 255, 0) * muliply, ai1, frameOrigin, ai2, SpriteEffects.None, 0);
            }
        }
    }
}
