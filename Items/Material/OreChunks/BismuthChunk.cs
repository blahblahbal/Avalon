using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class BismuthChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.value = Item.sellPrice(silver: 1, copper: 87);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ModContent.ItemType<Bars.BismuthBar>())
			.AddIngredient(Type, 4)
			.AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Bars.BismuthBar>())
			.Register();
	}
}
