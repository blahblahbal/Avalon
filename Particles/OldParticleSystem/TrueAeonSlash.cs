using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles.OldParticleSystem
{
    public class TrueAeonSlash : LegacyParticleDeleteSoon
    {
		public Vector2 Offset = Vector2.Zero;
		public Player Owner = null;
		public float Scale = 1f;
		public float RotationAmount = 0f;
		public int Direction = 1;
		public float MaxTime = 60;
		public Vector2 AccumulatedDistance = Vector2.Zero;
		public TrueAeonSlash(Vector2 offset, Player owner, float scale, float rotationAmount, int direction, float maxTime, Color color)
		{
			Offset = offset;
			Owner = owner;
			Scale = scale;
			RotationAmount = rotationAmount;
			Direction = direction;
			MaxTime = maxTime;
			Color = color;
		}
		public override void Update()
        {
			TimeInWorld++;

			Position = Owner.Center + AccumulatedDistance;

			AccumulatedDistance -= Owner.velocity * TimeInWorld / MaxTime;

            if (TimeInWorld > MaxTime)
                Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = TextureAssets.Extra[98].Value;
			float percent = TimeInWorld / MaxTime;
			float sin = MathF.Sin(percent * MathHelper.Pi);
			int trailLength = Math.Min(TimeInWorld, 10);
			for(int i = 1; i < trailLength; i++)
			{
				Vector2 pos = Position + Offset.RotatedBy(RotationAmount * ((TimeInWorld - i) / MaxTime) * Direction);
				spriteBatch.Draw(tex, pos - Main.screenPosition, null, Color * sin * 0.1f * (1f - i / trailLength), pos.DirectionTo(Owner.Center).ToRotation(), tex.Size() / 2, new Vector2(sin,1f) * Scale, SpriteEffects.None, 0);
			}
			for(int i = 0; i < 2; i++)
			{
				spriteBatch.Draw(tex, Position + Offset.RotatedBy(RotationAmount * percent * Direction) - Main.screenPosition, null, Color * sin, i * MathHelper.PiOver2, tex.Size() / 2, Scale * sin, SpriteEffects.None, 0);
				spriteBatch.Draw(tex, Position + Offset.RotatedBy(RotationAmount * percent * Direction) - Main.screenPosition, null, new Color(1f,1f,1f,0f) * sin, i * MathHelper.PiOver2, tex.Size() / 2, new Vector2(0.5f,0.8f) * Scale * sin, SpriteEffects.None, 0);
			}
        }
    }
}
