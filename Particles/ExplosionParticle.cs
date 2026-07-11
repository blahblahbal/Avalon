using Avalon.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Renderers;

namespace Avalon.Particles
{
    public class ExplosionParticle : BaseParticle
    {
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
			var texture = AssetReferences.Assets.Textures.FlameExplosion.Asset;
			var texture2 = AssetReferences.Assets.Textures.WhiteExplosion.Asset;
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
