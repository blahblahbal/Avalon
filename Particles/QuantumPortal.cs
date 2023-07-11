using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class QuantumPortal : Particle
    {
        int Frame;
        public override void Update()
        {
            TimeInWorld += 1;

            if(TimeInWorld <= 20)
            {
                AI1 += 0.05f;
            }
            if (TimeInWorld >= 30)
            {
                AI1 -= 0.03f;
            }
            if(TimeInWorld > 1 && AI1 < 0)
            {
                Active = false;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = TextureAssets.Extra[50].Value;
            int frameHeight = texture.Height;
            Rectangle frame = new Rectangle(0, frameHeight * Frame, texture.Width, frameHeight);
            Vector2 frameOrigin = new Vector2(texture.Width) / 2;
            Vector2 DrawPos = Position - Main.screenPosition;


            spriteBatch.Draw(texture, DrawPos, frame, Color.Lerp(new Color(255, 128, 255, 0), new Color(255, 128, 255, 0) * 0.4f, Main.masterColor), TimeInWorld * 0.1f, frameOrigin, AI1 * 1.2f, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, Color.Lerp(new Color(255, 64, 255), new Color(128, 64, 255), Main.masterColor), TimeInWorld * 0.11f, frameOrigin, AI1, SpriteEffects.None, 0);
        }
    }
}
