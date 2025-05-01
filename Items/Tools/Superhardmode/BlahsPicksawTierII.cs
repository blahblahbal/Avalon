using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Superhardmode;

public class BlahsPicksawTierII : ModItem
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
		Item.DefaultToPickaxeAxe(700, 300, 55, 5.5f, 6, 6, 16, 1.15f);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(gold: 50);
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
