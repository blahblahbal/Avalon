using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class FallenStarBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 50;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.FallenStarTile>());
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(silver: 5);
	}

	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ItemID.FallenStar).Register();
		Recipe.Create(ItemID.FallenStar).AddIngredient(this).Register();
	}
}
