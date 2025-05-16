using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class DemoniteChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(silver: 5);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.DemoniteBar)
			.AddIngredient(Type, 3)
			.AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ItemID.DemoniteBar)
			.Register();
	}
}
