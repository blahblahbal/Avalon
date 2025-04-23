using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.SoulCandles;

public class FlightCandle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.SoulCandles.FlightCandle>());
		Item.width = 8;
		Item.height = 18;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(silver: 10);
		Item.noWet = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ItemID.SoulofFlight, 5)
			.AddIngredient(ItemID.Candle)
			.AddTile(TileID.MythrilAnvil)
			.Register();

		CreateRecipe(2)
			.AddIngredient(ItemID.SoulofFlight, 5)
			.AddIngredient(ItemID.PlatinumCandle)
			.AddTile(TileID.MythrilAnvil)
			.Register();

		CreateRecipe(2)
			.AddIngredient(ItemID.SoulofFlight, 5)
			.AddIngredient(ModContent.ItemType<Furniture.BismuthCandle>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
