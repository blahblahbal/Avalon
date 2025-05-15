using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class TroxiniumOre : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;

		ItemGlowmask.AddGlow(this, 0);
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.TroxiniumOre>());
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 0, 15);
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
	//public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	//{
	//    Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
	//    spriteBatch.Draw
	//    (
	//        texture,
	//        new Vector2
	//        (
	//            Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
	//            Item.position.Y - Main.screenPosition.Y + Item.height * 0.5f
	//        ),
	//        new Rectangle(0, 0, texture.Width, texture.Height),
	//        Color.White,
	//        rotation,
	//        texture.Size() * 0.5f,
	//        scale,
	//        SpriteEffects.None,
	//        0f
	//    );
	//}
}
