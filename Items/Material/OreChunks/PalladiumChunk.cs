using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class PalladiumChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 4, copper: 50);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.PalladiumBar)
			.AddIngredient(Type, 3)
			.AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ItemID.PalladiumBar)
			.Register();
	}
}
