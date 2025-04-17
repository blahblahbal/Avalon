using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class NickelBar : ModItem
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
		Item.DefaultToBar(19);
		Item.value = Item.sellPrice(0, 0, 3, 75);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Ores.NickelOre>(), 3)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
