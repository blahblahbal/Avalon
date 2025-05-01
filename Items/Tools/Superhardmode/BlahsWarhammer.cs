using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Superhardmode;

public class BlahsWarhammer : ModItem
{
	private static Asset<Texture2D>? glow;
	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");
		if (!Main.dedServ)
		{
			Item.GetGlobalItem<ItemGlowmask>().glowTexture = glow;
		}
	}
	public override void SetDefaults()
	{
		Item.DefaultToHammer(250, 120, 20f, 9, 9, 6, 1.15f, width: 44, height: 48);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(gold: 50);
	}
	public override void HoldItem(Player player)
	{
		if (player.inventory[player.selectedItem].type == Item.type)
		{
			player.wallSpeed += 0.5f;
		}
	}
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		Rectangle dims = this.GetDims();
		Vector2 vector = dims.Size() / 2f;
		Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - dims.Height);
		Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
		spriteBatch.Draw(glow.Value, vector2, dims, new Color(250, 250, 250, 250), rotation, vector, scale, SpriteEffects.None, 0f);
	}
}
