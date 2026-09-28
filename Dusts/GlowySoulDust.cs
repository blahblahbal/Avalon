using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts
{
	/// <summary>
	/// Set CustomData to a SoulType
	/// </summary>
    public class GlowySoulDust : ModDust
    {
		public enum SoulType : byte
		{
			Light,
			Night,
			Flight,
			Might,
			Sight,
			Fright,
			Blight,
			Ice,
			Delight,
			Humidity,
			Torture
		}
        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, Main.rand.Next(3) * 10, 10, 10);
        }
        public override bool Update(Dust dust)
        {
			if (dust.customData is SoulType sType)
			{
				dust.frame.X = 20 * (byte)dust.customData;


				Vector3 color = Vector3.One;

				switch (sType)
				{
					case SoulType.Light: color = new Vector3(1,0.3f,1); break;
					case SoulType.Night: color = new Vector3(0.5f, 0.3f, 1); break;
					case SoulType.Flight: color = new Vector3(0.5f, 0.9f, 1); break;
					case SoulType.Might: color = new Vector3(0, 0, 1); break;
					case SoulType.Sight: color = new Vector3(0.2f, 1f, 0.7f); break;
					case SoulType.Fright: color = new Vector3(1, 0.5f, 0); break;
					case SoulType.Blight: color = new Vector3(0.3f, 0.3f, 0.3f); break;
					case SoulType.Ice: color = new Vector3(0.5f, 0.8f, 1); break;
					case SoulType.Delight: color = new Vector3(0.2f, 0.1f, 1); break;
					case SoulType.Humidity: color = new Vector3(0.6f, 1, 0.5f); break;
					case SoulType.Torture: color = new Vector3(1, 0, 0); break;
				}

				Lighting.AddLight(dust.position, color * dust.scale * 0.25f * ((255 - dust.alpha) / 255f));
				if (dust.noGravity)
					dust.velocity = dust.velocity.RotatedByRandom(0.2f);
			}
			return true;
        }
		public override bool PreDraw(Dust dust)
		{
			Vector2 scale = Vector2.One;
			float length = dust.velocity.Length() / 1.5f;
			float opacity = (255 - dust.alpha) / 255f;
			if (length > 1)
			{
				scale.X = MathHelper.Clamp(length, 1, 3);
				dust.rotation = dust.velocity.ToRotation();
			}

			for (int i = 0; i < 4; i++)
			{
				Main.spriteBatch.Draw(Texture2D.Value, dust.position - Main.screenPosition + new Vector2(2 * dust.scale,0).RotatedBy(i * MathHelper.PiOver2 + dust.rotation), dust.frame, Color.White with { A = 0 } * opacity * dust.scale * 0.25f, dust.rotation, new Vector2(4), scale * dust.scale, SpriteEffects.None, 0);
			}
			Main.spriteBatch.Draw(Texture2D.Value, dust.position - Main.screenPosition, dust.frame, Color.White with { A = 128 } * opacity, dust.rotation, new Vector2(4), scale * dust.scale, SpriteEffects.None,0);

			float glowScale = Math.Min(MathF.Pow(dust.scale, 2),dust.scale) - 0.5f;
			if(glowScale > 0)
				Main.spriteBatch.Draw(Texture2D.Value, dust.position - Main.screenPosition, dust.frame with { X = dust.frame.X + 10}, Color.White with { A = 0 } * opacity, dust.rotation, new Vector2(4), scale * glowScale, SpriteEffects.None, 0);
			return false;
		}
    }
}
