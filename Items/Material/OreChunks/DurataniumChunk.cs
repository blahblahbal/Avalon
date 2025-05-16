using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.OreChunks;

public class DurataniumChunk : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 14);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 5);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ModContent.ItemType<Bars.DurataniumBar>())
			.AddIngredient(Type, 3)
			.AddTile(TileID.Furnaces)
			.SortAfterFirstRecipesOf(ModContent.ItemType<Bars.DurataniumBar>())
			.Register();
	}
}
