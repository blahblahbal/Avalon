using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;
using Terraria.ID;

namespace Avalon.Particles
{
    public class QuantumPortal : BaseParticle
    {
		public float ai1;
		float direction = 0;
		SpriteEffects effect = 0;
		public QuantumPortal(Vector2 position, float ai1 = 0)
		{
			this.ai1 = ai1;
			Position = position;
			direction = Main.rand.NextFloat(0.5f, 1.5f) * (Main.rand.NextBool() ? 1 : -1);
			effect = Main.rand.NextBool() ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
		}
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			if (TimeInWorld <= 20)
			{
				ai1 += 0.05f;
			}
			if (TimeInWorld >= 30)
			{
				ai1 -= 0.03f;
			}
			if (TimeInWorld > 1 && ai1 < 0)
			{
				Active = false;
			}
		}
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Texture2D texture = TextureAssets.Extra[ExtrasID.VortexBlack].Value;
			Vector2 DrawPos = Position + settings.AnchorPosition;
			float multiply = MathHelper.Clamp(ai1 * ai1 * ai1 * 3, 0, 1);
			for (int i = 0; i < 4; i++)
			{
				spritebatch.Draw(texture, DrawPos, null, Color.Lerp(new Color(255, 0, 0, 64), new Color(128, 0, 255, 64), i * 0.25f) * multiply, (TimeInWorld * 0.05f * direction) + i, texture.Size() / 2, ai1 * (1.1f + (i * 0.1f)), effect, 0);
			}
			spritebatch.Draw(texture, DrawPos, null, Color.Lerp(new Color(255, 64, 255), new Color(128, 64, 255), Main.masterColor) * multiply, TimeInWorld * 0.1f * direction, texture.Size() / 2, ai1, effect, 0);
		}
    }
}
