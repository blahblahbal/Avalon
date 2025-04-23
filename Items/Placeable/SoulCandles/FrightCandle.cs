using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.SoulCandles;

public class FrightCandle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.SoulCandles.FrightCandle>());
		Item.width = 8;
		Item.height = 18;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(silver: 10);
		Item.noWet = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddIngredient(ItemID.Candle)
			.AddTile(TileID.MythrilAnvil)
			.Register();

		CreateRecipe(2)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddIngredient(ItemID.PlatinumCandle)
			.AddTile(TileID.MythrilAnvil)
			.Register();

		CreateRecipe(2)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddIngredient(ModContent.ItemType<Furniture.BismuthCandle>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
