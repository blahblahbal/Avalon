using Avalon.ModSupport.MLL.Liquids;
using Avalon.ModSupport.MLL.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class BloodfallBlock : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<BloodfallTile>());
		Item.Size = new(12);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.Glass)
			.AddLiquid<Blood>()
			.AddTile(TileID.CrystalBall)
			.SortBeforeFirstRecipesOf(ItemID.SandFallBlock)
			.Register();
	}
}
