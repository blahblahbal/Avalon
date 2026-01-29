using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles.OldParticleSystem
{
    public class SoulEdgeDot : LegacyParticleDeleteSoon
    {
        public override void Update()
        {
            TimeInWorld+= Main.rand.Next(-3,7);
            Position += Velocity;
            if (ai1 != -1)
            {
                Velocity += Position.DirectionTo(Main.player[(int)ai1].MountedCenter) * Velocity.Length() * 0.08f;
            }
            Velocity = Velocity.RotatedBy(ai2) * 0.94f;
            ai2 *= 0.98f;
            float scale = Velocity.Length() / 8f;
            if(scale <= 0.01f)
            {
                Active = false;
            }
            Lighting.AddLight(Position, new Vector3(0.15f, 0.2f, 0.2f) * scale * 6);
            if(TimeInWorld > 60 * 3)
            {
                Velocity *= 0.9f;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("Avalon/Assets/Textures/GlowCircle");
            Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 frameOrigin = new Vector2(texture.Width) / 2;
            Vector2 DrawPos = Position - Main.screenPosition;

            byte Sub = (byte)ai3;
            float scale = Velocity.Length() / 1f;
            if (scale > 3)
                scale = 3;
            float opacityMulti = MathHelper.Min(TimeInWorld / 40f, 1);
            spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * MathHelper.Min(scale * 0.4f, 1) * opacityMulti, Velocity.ToRotation() + MathHelper.PiOver2, frameOrigin, new Vector2(MathHelper.Min(scale,1),scale) * 0.8f * ai3, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, DrawPos, frame, new Color(0, 77, 128, 0) * MathHelper.Min(scale * 0.4f, 1) * opacityMulti, Velocity.ToRotation() + MathHelper.PiOver2, frameOrigin, new Vector2(MathHelper.Min(scale, 1), scale) * ai3, SpriteEffects.None, 0);
        }
    }
}
