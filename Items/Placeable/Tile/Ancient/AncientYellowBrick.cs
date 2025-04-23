using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile.Ancient;

public class AncientYellowBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ancient.AncientYellowBrick>());
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Wall.AncientYellowBrickWall>(), 4)
			.AddTile(TileID.WorkBenches)
			.DisableDecraft()
			.Register();
	}
}
