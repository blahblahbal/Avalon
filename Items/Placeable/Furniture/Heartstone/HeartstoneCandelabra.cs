using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Heartstone;

public class HeartstoneCandelabra : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Heartstone.HeartstoneCandelabra>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 3);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.Ores.Heartstone>(), 5)
			.AddIngredient(ItemID.Torch, 3)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
