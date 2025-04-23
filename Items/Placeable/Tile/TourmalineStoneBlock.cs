using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class TourmalineStoneBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.Tourmaline>());
		Item.sellPrice(silver: 1);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>())
			.AddIngredient(ItemID.StoneBlock)
			.AddTile(TileID.HeavyWorkBench)
			.AddCondition(Condition.InGraveyard)
			.SortAfterFirstRecipesOf(ItemID.TopazStoneBlock)
			.Register();
	}
}
