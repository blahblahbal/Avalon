using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile.Ancient;

public class AncientPurpleBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ancient.AncientPurpleBrick>());
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Wall.AncientPurpleBrickWall>(), 4)
			.AddTile(TileID.WorkBenches)
			.DisableDecraft()
			.Register();
	}
}
