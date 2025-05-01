using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class IridiumBar : ModItem
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
		Item.DefaultToBar(22);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 20);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Ores.IridiumOre>(), 4)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
