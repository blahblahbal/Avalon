using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class ShockwaveParticle : Particle
    {
		public float ai1;
		private static Asset<Texture2D> texture;

		public ShockwaveParticle(float ai1)
		{
			this.ai1 = ai1;
		}
		public override void Update()
        {
            if (TimeInWorld > 20)
                Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 screenpos)
        {
            if(texture == null) texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/Shockwave");
            Rectangle frame = texture.Frame();
            Vector2 DrawPos = Position - screenpos;

            spriteBatch.Draw(texture.Value, DrawPos, frame, new Color(255,128,64,64) * -((TimeInWorld / 20f) - 1), 0, frame.Size() / 2, new Vector2((ai1 / 128) + TimeInWorld / 10f, TimeInWorld / 10f + (ai1 / 256)), SpriteEffects.None, 0);
        }
    }
}
