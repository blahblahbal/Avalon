using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles;

public class EnemyGauntletFlame : Particle
{
	private static Asset<Texture2D> texture;
	bool flip = false;
	public EnemyGauntletFlame()
	{
		this.flip = Main.rand.NextBool();
	}
	public override void Update()
	{
		if (TimeInWorld > 14 * 4)
			Active = false;
	}
	public override void Draw(SpriteBatch spriteBatch, Vector2 screenpos)
	{
		if (texture == null)
		{
			texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/EnemyGauntletFlame");
		}
		spriteBatch.Draw(texture.Value, Position - screenpos, texture.Frame(1, 14, 0, TimeInWorld / 4), new Color(1f, 1f, 1f, 0.4f), 0, new Vector2(53, 54), MathHelper.Clamp(TimeInWorld / 10f,0,1f), flip ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
	}
}
