using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class NickelChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.value = Item.sellPrice(copper: 62);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ModContent.ItemType<Bars.NickelBar>())
			.AddIngredient(Type, 3)
			.AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Bars.NickelBar>())
			.Register();
	}
}
