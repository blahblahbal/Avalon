using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class TinChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.value = Item.sellPrice(copper: 37);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.TinBar)
			.AddIngredient(Type, 3)
			.AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ItemID.TinBar)
			.Register();
	}
}
