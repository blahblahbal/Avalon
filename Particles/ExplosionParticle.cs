using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Renderers;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class ExplosionParticle : BaseParticle
    {
		private static Asset<Texture2D> texture;
		private static Asset<Texture2D> texture2;
		public float Scale;
		public float Rotation;
		int Frame;
		public ExplosionParticle(Vector2 position, float rotation, float scale)
		{
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
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			if (texture == null)
			{
				texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/FlameExplosion");
			}
			if (texture2 == null)
			{
				texture2 = ModContent.Request<Texture2D>("Avalon/Assets/Textures/WhiteExplosion");
			}
			int frameHeight = texture.Height() / 7;
			Rectangle frame = new Rectangle(0, frameHeight * Frame, texture.Width(), frameHeight);
			Vector2 frameOrigin = new Vector2(texture.Width()) / 2;
			Vector2 DrawPos = Position + settings.AnchorPosition;

			float muliply = ((32 - TimeInWorld) / 28);

			for (int i = 0; i < 8; i++)
			{
				spritebatch.Draw(texture.Value, DrawPos + new Vector2(0, TimeInWorld * 0.1f).RotatedBy(i * MathHelper.PiOver4), frame, Color.White * 0.3f, Rotation, frameOrigin, Scale, SpriteEffects.None, 0);
			}

			spritebatch.Draw(texture.Value, DrawPos, frame, new Color(255, 255, 255, 128), Rotation, frameOrigin, Scale, SpriteEffects.None, 0);

			for (int i = 0; i < 8; i++)
			{
				spritebatch.Draw(texture2.Value, DrawPos + new Vector2(0, TimeInWorld * 0.2f).RotatedBy(i * MathHelper.PiOver4), frame, new Color(255, 255, 255, 0) * muliply, Rotation, frameOrigin, Scale, SpriteEffects.None, 0);
			}
		}
    }
}
