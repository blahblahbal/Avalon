using Avalon.Tiles.Contagion.ContagionChest;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.TrappedChests;

public class TrappedContagionChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 0);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<ContagionChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.Register();
	}
}
