using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class LivingLightningDust : ModDust
{
	public override bool Update(Dust dust)
	{
		float lightModifier = dust.scale;
		if (lightModifier > 1f)
		{
			lightModifier = 1f;
		}
		if (!dust.noLight)
		{
			Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), lightModifier * 0.768f, lightModifier * 0.556f, lightModifier * 0.933f);
		}
		if (dust.noGravity)
		{
			dust.velocity *= 0.93f;
			if (dust.fadeIn == 0f)
			{
				dust.scale += 0.0025f;
			}
		}
		dust.velocity *= new Vector2(0.97f, 0.99f);
		dust.scale -= 0.03f;
		return true;
	}
	public override bool PreDraw(Dust dust)
	{
		Rectangle rectangle = new Rectangle((int)Main.screenPosition.X - 1000, (int)Main.screenPosition.Y - 1050, Main.screenWidth + 2000, Main.screenHeight + 2100);
		float velMagnitude = Math.Abs(dust.velocity.X) + Math.Abs(dust.velocity.Y);
		velMagnitude *= 0.3f;
		velMagnitude *= 10f;
		if (velMagnitude > 10f)
		{
			velMagnitude = 10f;
		}
		for (int i = 0; i < velMagnitude; i++)
		{
			Vector2 pos = dust.position - dust.velocity * i;
			float scale = dust.scale * (1f - i / 10f);
			Color color = Lighting.GetColor((int)(dust.position.X + 4.0) / 16, (int)(dust.position.Y + 4.0) / 16);
			color = dust.GetAlpha(color);
			Main.spriteBatch.Draw(Texture2D.Value, pos - Main.screenPosition, dust.frame, color, dust.rotation, new Vector2(4f, 4f), scale, SpriteEffects.None, 0f);
		}
		return true;
	}
}
