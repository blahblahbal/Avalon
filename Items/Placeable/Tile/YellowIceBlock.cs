using Avalon.Items.Placeable.Furniture;
using Avalon.Tiles.Contagion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class YellowIceBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<YellowIce>());
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.IceTorch, 3)
			.AddIngredient(ItemID.Torch, 3)
			.AddIngredient(this)
			.Register();

		Recipe.Create(ModContent.ItemType<ContagionTorch>(), 3)
			.AddIngredient(ItemID.Torch, 3)
			.AddIngredient(this)
			.Register();
	}
}
