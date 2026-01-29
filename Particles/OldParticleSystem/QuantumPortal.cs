using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles.OldParticleSystem
{
    public class QuantumPortal : LegacyParticleDeleteSoon
    {
        int Frame;
        public override void Update()
        {
            TimeInWorld += 1;

            if(TimeInWorld <= 20)
            {
                ai1 += 0.05f;
            }
            if (TimeInWorld >= 30)
            {
                ai1 -= 0.03f;
            }
            if(TimeInWorld > 1 && ai1 < 0)
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

            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(texture, DrawPos, frame, Color.Lerp(new Color(255, 0, 0, 64), new Color(128, 0, 255, 64), i * 0.25f), TimeInWorld * 0.1f + i * 0.5f, frameOrigin, ai1 * (1.1f + (i * 0.1f)), SpriteEffects.None, 0);
            }
            spriteBatch.Draw(texture, DrawPos, frame, Color.Lerp(new Color(255, 64, 255), new Color(128, 64, 255), Main.masterColor), TimeInWorld * 0.1f, frameOrigin, ai1, SpriteEffects.None, 0);
        }
    }
}
