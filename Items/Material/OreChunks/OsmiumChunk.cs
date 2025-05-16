using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class OsmiumChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 4, copper: 50);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ModContent.ItemType<Bars.OsmiumBar>())
			.AddIngredient(Type, 4)
			.AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Bars.OsmiumBar>())
			.Register();
	}
}
