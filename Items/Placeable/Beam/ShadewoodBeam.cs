using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Beam;

public class ShadewoodBeam : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 50;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ShadewoodBeam>());
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type, 2)
			.AddIngredient(ItemID.Shadewood)
			.AddTile(TileID.Sawmill)
			.SortBeforeFirstRecipesOf(ItemID.ShadewoodDoor)
			.Register();
	}
}
