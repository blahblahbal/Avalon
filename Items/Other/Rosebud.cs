using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class Rosebud : LifePickupItem
{
	private static Asset<Texture2D>? glow;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		ItemID.Sets.ItemIconPulse[Item.type] = true;
		ItemID.Sets.ItemNoGravity[Item.type] = true;
		glow = ModContent.Request<Texture2D>(Texture + "Glow");
	}
	public override float HealAmount => Main.rand.NextFloat(10, 15);
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		spriteBatch.Draw
		(
			glow.Value,
			new Vector2
			(
				Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
				Item.position.Y - Main.screenPosition.Y + Item.height - glow.Value.Height * 0.5f
			),
			new Rectangle(0, 0, glow.Value.Width, glow.Value.Height),
			Color.White,
			rotation,
			glow.Value.Size() * 0.5f,
			scale,
			SpriteEffects.None,
			0f
		);
	}
}
