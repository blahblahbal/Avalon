using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class HallowedBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Bricks.HallowedBrick>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(5)
			.AddIngredient(ItemID.StoneBlock, 5)
			.AddIngredient(ModContent.ItemType<Material.Ores.HallowedOre>())
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
