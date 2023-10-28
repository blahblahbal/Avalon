using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class AeonStarburst : Particle
    {
        int BurstTime = 14;
        public override void Update()
        {
            if (TimeInWorld == 0)
                TimeInWorld += 3;
            TimeInWorld++;

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

            Lighting.AddLight(Position, 0, (30 - TimeInWorld) * 0.02f, (30 - TimeInWorld) * 0.03f);

            if (TimeInWorld > BurstTime)
                Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("Avalon/Assets/Textures/SparklyAeon");
            Rectangle frame = texture.Frame();
            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 DrawPos = Position - Main.screenPosition;

            spriteBatch.Draw(texture, DrawPos, frame, Color.Lerp(new Color(Color.R, Color.G, Color.B, 64), new Color(0, 0, 0, 0), (float)TimeInWorld / BurstTime) * 2,ai1, frameOrigin, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / BurstTime)) * ai2, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, Color.Lerp(new Color(255, 255, 255, 64), new Color(0, 0, 0, 0), (float)TimeInWorld / BurstTime * 0.9f) * 2, ai1, frameOrigin, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / BurstTime)) * ai2 * 0.83f, SpriteEffects.None, 0);


            //for (int i = 0; i < 5; i++)
            //{
            //    spriteBatch.Draw(texture, DrawPos, frame, Color.Lerp(new Color(255, 255, 128, 0), new Color(128, 255, 255, 0),(float)TimeInWorld / 20f) * 0.5f, i * 1.25664f, frameOrigin, new Vector2(1.9f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.2f) * 2f), SpriteEffects.None, 0);

            //    spriteBatch.Draw(texture, DrawPos, frame, new Color(255,255,255,0) * 0.2f, i * 1.25664f, frameOrigin, new Vector2(1.5f, (float)Math.Sin((float)(TimeInWorld + 3) * 0.2f) * 1.7f), SpriteEffects.None, 0);

            //}
        }
    }
}
