using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class BismuthBar : ModItem
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
		Item.DefaultToBar(21);
		Item.value = Item.sellPrice(0, 0, 15);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Ores.BismuthOre>(), 4)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
