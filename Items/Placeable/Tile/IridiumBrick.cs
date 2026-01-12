using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class IridiumBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Bricks.IridiumBrick>());
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type, 5)
			.AddIngredient(ItemID.StoneBlock, 5)
			.AddIngredient(ModContent.ItemType<Material.Ores.IridiumOre>())
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
