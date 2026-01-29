using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles.OldParticleSystem
{
    public class ShockwaveParticle : LegacyParticleDeleteSoon
    {
        public override void Update()
        {
            TimeInWorld++;

            if (TimeInWorld > 20)
                Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("Avalon/Assets/Textures/Shockwave");
            Rectangle frame = texture.Frame();
            Vector2 DrawPos = Position - Main.screenPosition;

            spriteBatch.Draw(texture, DrawPos, frame, new Color(255,128,64,64) * -((TimeInWorld / 20f) - 1), 0, frame.Size() / 2, new Vector2((ai1 / 128) + TimeInWorld / 10f, TimeInWorld / 10f + (ai1 / 256)), SpriteEffects.None, 0);
        }
    }
}
