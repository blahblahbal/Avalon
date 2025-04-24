using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

public class AFlowerlessPlant : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(0, 1);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.02f;
		player.GetCritChance(DamageClass.Generic)++;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup(RecipeGroupID.Wood, 50)
			.AddIngredient(ItemID.FallenStar, 6)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
