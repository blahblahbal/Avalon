using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class GiantGravestone : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.GiantGravestone>());
		Item.width = 26;
		Item.height = 36;
		Item.rare = ItemRarityID.Blue;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup("Tombstones", 7)
			.AddTile(TileID.HeavyWorkBench)
			.Register();
	}
}
