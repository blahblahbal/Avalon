using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class OsmiumBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.OsmiumBrick>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(5).AddIngredient(ItemID.StoneBlock, 5).AddIngredient(ModContent.ItemType<Material.Ores.OsmiumOre>()).AddTile(TileID.Furnaces).Register();
	}
}
