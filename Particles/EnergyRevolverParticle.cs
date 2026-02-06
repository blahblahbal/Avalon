using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles
{
    public class EnergyRevolverParticle : Particle
    {
		private static Asset<Texture2D> texture;
		public Vector2 Velocity;
		public Color Color;
		public float ai1;
		public float ai2;
		public float ai3;

		public EnergyRevolverParticle(Vector2 velocity, Color color, float ai1, float ai2, float ai3)
		{
			Velocity = velocity;
			Color = color;
			this.ai1 = ai1;
			this.ai2 = ai2;
			this.ai3 = ai3;
		}

		public override void Update()
        {
            if (TimeInWorld > ai3)
                Active = false;
            Position += Velocity;
            Velocity *= 0.95f;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 screenpos)
        {
			if(texture == null)
				texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/GlowCircle");
            Vector2 DrawPos = Position - screenpos;
            spriteBatch.Draw(texture.Value, DrawPos, null, Color.Lerp(new Color(Color.R, Color.G, Color.B, 0), new Color(0, 0, 0, 0), (float)TimeInWorld / ai3) * 2,ai1, texture.Size() / 2, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / ai3)) * ai2, SpriteEffects.None, 0);
            spriteBatch.Draw(texture.Value, DrawPos, null, Color.Lerp(new Color(255, 255, 255, 0), new Color(0, 0, 0, 0), (float)TimeInWorld / ai3) * 2, ai1, texture.Size() / 2, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / ai3)) * ai2 * 0.8f, SpriteEffects.None, 0);
        }
    }
}
