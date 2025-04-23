using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class HeartstoneBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.HeartstoneBrick>());
		Item.rare = ItemRarityID.Green;
	}
	public override void AddRecipes()
	{
		CreateRecipe(5)
			.AddIngredient(ItemID.StoneBlock, 5)
			.AddIngredient(ModContent.ItemType<Material.Ores.Heartstone>())
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
