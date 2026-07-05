using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Renderers;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class ColorExplosion : BaseParticle
    {
		private static Asset<Texture2D> texture;
		public Color Color;
		public float Scale;
		public float Rotation;
        int Frame;
		public ColorExplosion(Color color, float rotation, float scale, Vector2 position)
		{
			Color = color;
			Scale = scale;
			Rotation = rotation;
			Position = position;
		}
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
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
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spriteBatch)
		{
			if (texture == null)
			{
				texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/WhiteExplosion");
			}
			int frameHeight = texture.Height() / 7;
			Rectangle frame = new Rectangle(0, frameHeight * Frame, texture.Width(), frameHeight);
			Vector2 frameOrigin = new Vector2(texture.Width()) / 2;
			Vector2 DrawPos = Position + settings.AnchorPosition;

			float muliply = ((32 - TimeInWorld) / 28);

			for (int i = 0; i < 8; i++)
			{
				spriteBatch.Draw(texture.Value, DrawPos + new Vector2(0, TimeInWorld * 0.1f * Scale).RotatedBy(i * MathHelper.PiOver4), frame, Color * 0.3f, Rotation, frameOrigin, Scale, SpriteEffects.None, 0);
			}

			spriteBatch.Draw(texture.Value, DrawPos, frame, Color, Rotation, frameOrigin, Scale, SpriteEffects.None, 0);

			for (int i = 0; i < 8; i++)
			{
				spriteBatch.Draw(texture.Value, DrawPos + new Vector2(0, TimeInWorld * 0.2f).RotatedBy(i * MathHelper.PiOver4), frame, new Color(255, 255, 255, 0) * muliply, Rotation, frameOrigin, Scale, SpriteEffects.None, 0);
			}
		}
    }
}
