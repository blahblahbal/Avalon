using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class CursedExplosionParticle : Particle
    {
		private static Asset<Texture2D> texture;
		private static Asset<Texture2D> texture2;
		public float Scale;
		public float Rotation;
		int Frame;
		public CursedExplosionParticle(float rotation, float scale)
		{
			Scale = scale;
			Rotation = rotation;
		}
		public override void Update()
		{
			if (TimeInWorld % 4 == 0)
			{
				Frame++;
			}
			if (Frame > 7)
			{
				Active = false;
			}
			Scale += 0.01f;
		}
		public override void Draw(SpriteBatch spriteBatch, Vector2 screenpos)
        {
			if (texture == null)
			{
				texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/CursedExplosion");
			}
			if (texture2 == null)
			{
				texture2 = ModContent.Request<Texture2D>("Avalon/Assets/Textures/WhiteExplosion");
			}
			int frameHeight = texture.Height() / 7;
			Rectangle frame = new Rectangle(0, frameHeight * Frame, texture.Width(), frameHeight);
			Vector2 frameOrigin = new Vector2(texture.Width()) / 2;
			Vector2 DrawPos = Position - screenpos;

			float muliply = ((32 - TimeInWorld) / 28);

            spriteBatch.Draw(texture.Value, DrawPos, frame, new Color(255,255,255,128), Rotation, frameOrigin, Scale, SpriteEffects.None, 0);

            for (int i = 0; i < 8; i++)
            {
                spriteBatch.Draw(texture2.Value, DrawPos + new Vector2(0, TimeInWorld * 0.2f).RotatedBy(i * MathHelper.PiOver4), frame, new Color(255, 255, 255, 0) * muliply, Rotation, frameOrigin, Scale, SpriteEffects.None, 0);
            }
        }
    }
}
