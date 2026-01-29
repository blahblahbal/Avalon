using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles.OldParticleSystem
{
    public class MoonforceParticle : LegacyParticleDeleteSoon
    { 
        public override void Update()
        {
            TimeInWorld += 1;
            if(Main.timeForVisualEffects % 4 == 0)
            {
                TimeInWorld += 1;
            }
            Position += Velocity;
            if (TimeInWorld > 24)
                Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Color Color1 = Color.Lerp(Color.Red, Color.Purple, Main.masterColor);
            Color1.A = 0;

            Texture2D texture = TextureAssets.Extra[98].Value;
            Rectangle frame = texture.Frame();
            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 DrawPos = Position - Main.screenPosition;
            float Rotation = Velocity.ToRotation();
            spriteBatch.Draw(texture, DrawPos, frame, Color1, Rotation, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.20f) * 2f) * ai1 * 1.2f, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, new Color(255,255,255,0) * 0.3f, Rotation, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 1) * 0.20f) * 2f) * ai1 * 0.8f, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, Color1, Rotation + MathHelper.PiOver2, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.16f)) * ai1, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * 0.3f, Rotation + MathHelper.PiOver2, frameOrigin, new Vector2(1, (float)Math.Sin((float)(TimeInWorld + 1) * 0.2f)) * ai1 * 0.8f, SpriteEffects.None, 0);
        }
    }
}
