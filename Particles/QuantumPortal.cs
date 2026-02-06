using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace Avalon.Particles
{
    public class QuantumPortal : Particle
    {
		public float ai1;
		public QuantumPortal(float ai1 = 0)
		{
			this.ai1 = ai1;
		}

		public override void Update()
        {
            if(TimeInWorld <= 20)
            {
                ai1 += 0.05f;
            }
            if (TimeInWorld >= 30)
            {
                ai1 -= 0.03f;
            }
            if(TimeInWorld > 1 && ai1 < 0)
            {
                Active = false;
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 screenpos)
        {
            Texture2D texture = TextureAssets.Extra[50].Value;
            Vector2 DrawPos = Position - screenpos;
			float multiply = MathHelper.Clamp(ai1 * ai1 * ai1 * 3, 0, 1);
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(texture, DrawPos, null, Color.Lerp(new Color(255, 0, 0, 64), new Color(128, 0, 255, 64), i * 0.25f) * multiply, TimeInWorld * 0.1f + i * 0.5f, texture.Size() / 2, ai1 * (1.1f + (i * 0.1f)), SpriteEffects.None, 0);
            }
            spriteBatch.Draw(texture, DrawPos, null, Color.Lerp(new Color(255, 64, 255), new Color(128, 64, 255), Main.masterColor) * multiply, TimeInWorld * 0.1f, texture.Size() / 2, ai1, SpriteEffects.None, 0);
        }
    }
}
