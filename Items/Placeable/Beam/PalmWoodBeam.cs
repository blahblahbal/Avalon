using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Beam;

public class PalmWoodBeam : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 50;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.PalmWoodBeam>());
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type, 2)
			.AddIngredient(ItemID.PalmWood)
			.AddTile(TileID.Sawmill)
			.SortBeforeFirstRecipesOf(ItemID.PalmWoodDoor)
			.Register();
	}
}
