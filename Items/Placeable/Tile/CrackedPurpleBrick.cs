using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class CrackedPurpleBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Dungeon.CrackedPurpleBrick>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<PurpleBrick>())
			.AddTile(TileID.HeavyWorkBench)
			.Register();
	}
}
