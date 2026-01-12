using Avalon.Common.Extensions;
using Avalon.Tiles.Furniture.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class CaesiumChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(silver: 10, copper: 50);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ModContent.ItemType<Bars.CaesiumBar>())
			.AddIngredient(Type, 8)
			.AddTile(ModContent.TileType<CaesiumForge>())
			.SortAfterFirstRecipesOf(ModContent.ItemType<Bars.CaesiumBar>())
			.Register();
	}
}
