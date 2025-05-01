using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class RhodiumBar : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.PrehardmodeBars;
	}
	public override void SetDefaults()
	{
		Item.DefaultToBar(3);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 28, 0);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Ores.RhodiumOre>(), 4)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
