using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Items.Placeable.Tile;

namespace Avalon.Items.Placeable.Beam;

public class CoughwoodBeam : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 50;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CoughwoodBeam>());
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type, 2)
			.AddIngredient(ModContent.ItemType<Coughwood>())
			.AddTile(TileID.Sawmill)
			.SortAfterFirstRecipesOf(ItemID.ToiletShadewood)
			.Register();
	}
}
