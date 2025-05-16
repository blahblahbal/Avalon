using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class SilverChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.value = Item.sellPrice(copper: 75);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.SilverBar)
			.AddIngredient(Type, 4)
			.AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ItemID.SilverBar)
			.Register();
	}
}
