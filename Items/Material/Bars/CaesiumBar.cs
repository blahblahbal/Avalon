using Avalon.Items.Material.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class CaesiumBar : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.HardmodeBars;
	}
	public override void SetDefaults()
	{
		Item.DefaultToBar(0);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 1, 5);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<CaesiumOre>(), 8)
			.AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();
	}
}
