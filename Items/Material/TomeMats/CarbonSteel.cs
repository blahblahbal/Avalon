using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

public class CarbonSteel : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.CraftedTomeMats;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTomeMaterial();
		Item.rare = ItemRarityID.Green;
	}
	public override void AddRecipes()
	{
		CreateRecipe(10)
			.AddIngredient(ItemID.IronOre, 30)
			.AddTile(TileID.Hellforge)
			.Register();

		CreateRecipe(10)
			.AddIngredient(ItemID.LeadOre, 30)
			.AddTile(TileID.Hellforge)
			.Register();

		CreateRecipe(10)
			.AddIngredient(ModContent.ItemType<Ores.NickelOre>(), 30)
			.AddTile(TileID.Hellforge)
			.Register();
	}
}
