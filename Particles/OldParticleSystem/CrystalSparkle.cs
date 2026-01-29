using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles.OldParticleSystem
{
    public class CrystalSparkle : LegacyParticleDeleteSoon
    { 
        public override void Update()
        {
            TimeInWorld += 1;
            if(Main.timeForVisualEffects % 4 == 0)
            {
                TimeInWorld += 1;
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

            if (TimeInWorld > 24)
                Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Color[] Colors = { Color.LightSkyBlue, Color.Magenta, Color.White, Color.Magenta };
            Color Color1 = Color.Lerp(ClassExtensions.CycleThroughColors(Colors, 60),Color.White,0.2f) * 0.7f;
            Color1.A = 0;

            Texture2D texture = TextureAssets.Extra[98].Value;
            Rectangle frame = texture.Frame();
            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 DrawPos = Position - Main.screenPosition;
            spriteBatch.Draw(texture, DrawPos, frame, Color1, 0f, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.20f) * 2f), SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, new Color(255,255,255,0) * 0.3f, 0f, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 1) * 0.20f) * 2f) * 0.7f, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, Color1, MathHelper.PiOver2, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.16f) * (ai1 + 1)), SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * 0.3f, MathHelper.PiOver2, frameOrigin, new Vector2(1, (float)Math.Sin((float)(TimeInWorld + 1) * 0.2f) * (ai1 + 1)) * 0.7f, SpriteEffects.None, 0);
        }
    }
}
