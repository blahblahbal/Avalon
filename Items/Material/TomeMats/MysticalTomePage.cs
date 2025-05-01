using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

public class MysticalTomePage : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.CraftedTomeMats;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTomeMaterial();
		Item.rare = ItemRarityID.Blue;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.FallenStar)
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddRecipeGroup(RecipeGroupID.Wood, 3)
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
