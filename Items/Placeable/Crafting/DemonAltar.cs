using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class DemonAltar : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.EvilAltarsPlaced>(), 0);
		Item.width = 24;
		Item.height = 14;
		Item.rare = ItemRarityID.Blue;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.EbonstoneBlock, 50)
			.AddIngredient(ItemID.RottenChunk, 10)
			.AddIngredient(ItemID.Deathweed, 5)
			.AddIngredient(ItemID.SoulofNight, 20)
			.AddIngredient(ItemID.ShadowScale, 20)
			.AddIngredient(ItemID.DemoniteBar, 25)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
