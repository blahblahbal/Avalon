using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class SanguineCuts : Particle
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
            Position += Velocity;
            Velocity.Y += 0.08f;
            ai3 = Velocity.ToRotation() + MathHelper.PiOver2;
            if (TimeInWorld > 24)
                Active = false;
            ai1 -= 0.1f;

            int num15 = Dust.NewDust(Position + Main.rand.NextVector2Circular(30,8), 0, 0, DustID.Blood, 0, 2, 140, default(Color), 1f);
            Main.dust[num15].noGravity = true;
            Main.dust[num15].velocity.X *= 0.1f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = TextureAssets.Extra[98].Value;
            Rectangle frame = texture.Frame();
            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 DrawPos = Position - Main.screenPosition;
            ai2 = 1;

            for (int i = 0; i < 24; i++)
            {
                Color = Color.DarkRed * (1f - ((float)TimeInWorld / 24)) * (1 - (i / 12f)) * 0.5f;
                ai2 -= 0.01f;
                spriteBatch.Draw(texture, DrawPos - Velocity * i, frame, Color, ai3, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 2f) * ai2, SpriteEffects.None, 0);
                spriteBatch.Draw(texture, DrawPos - Velocity * i + new Vector2(ai1, 0), frame, Color, ai3, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * ai2, SpriteEffects.None, 0);
                spriteBatch.Draw(texture, DrawPos - Velocity * i + new Vector2(-ai1, 0), frame, Color, ai3, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * ai2, SpriteEffects.None, 0);
            }
            ai2 = 1;
            Color = Color.Red * (1f - ((float)TimeInWorld / 24));
            //for (int i = 0; i < 2; i++)
            //{
                spriteBatch.Draw(texture, DrawPos, frame, Color, ai3, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 2f) * ai2, SpriteEffects.None, 0);
                spriteBatch.Draw(texture, DrawPos + new Vector2(ai1, 0), frame, Color, ai3, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * ai2, SpriteEffects.None, 0);
                spriteBatch.Draw(texture, DrawPos + new Vector2(-ai1, 0), frame, Color, ai3, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * ai2, SpriteEffects.None, 0);
                Color = Color.Black * (1f - ((float)TimeInWorld / 64));
                //AI2 = 0.7f;
            //}
        }
        public override void PostDraw(SpriteBatch spriteBatch)
        {
            Texture2D texture = TextureAssets.Extra[98].Value;
            Rectangle frame = texture.Frame();
            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 DrawPos = Position - Main.screenPosition;
            Color = Color.Black * (1f - ((float)TimeInWorld / 64));
            ai2 = 0.7f;
            spriteBatch.Draw(texture, DrawPos, frame, Color, ai3, frameOrigin, new Vector2(1.4f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.10f) * 2f) * ai2, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos + new Vector2(ai1, 0), frame, Color, ai3, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * ai2, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos + new Vector2(-ai1, 0), frame, Color, ai3, frameOrigin, new Vector2(1f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.06f) * 1f) * ai2, SpriteEffects.None, 0);
        }
    }
}
