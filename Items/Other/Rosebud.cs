using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class Rosebud : ModItem
{
	private static Asset<Texture2D>? glow;
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemIconPulse[Item.type] = true;
		ItemID.Sets.ItemNoGravity[Item.type] = true;
		glow = ModContent.Request<Texture2D>(Texture + "Glow");
	}

	public override void SetDefaults()
	{
		Item.Size = new Vector2(12);
	}

	public override bool CanPickup(Player player) => true;

	public override bool OnPickup(Player player)
	{
		int rand = Main.rand.Next(10, 16);
		if (player.statLife + rand > player.statLifeMax2)
		{
			player.statLife = player.statLifeMax2;
		}
		else
		{
			player.statLife += rand;
		}

		if (Main.myPlayer == player.whoAmI)
		{
			player.HealEffect(rand, true);
		}
		SoundEngine.PlaySound(SoundID.Grab, player.position);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		spriteBatch.Draw
		(
			glow.Value,
			new Vector2
			(
				Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
				Item.position.Y - Main.screenPosition.Y + Item.height - glow.Value.Height * 0.5f + 2f
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
