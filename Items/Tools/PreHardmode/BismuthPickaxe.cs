using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class BismuthPickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(59, 6, 2f, 14, 18);
		Item.value = Item.sellPrice(silver: 28);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 10).AddRecipeGroup(RecipeGroupID.Wood, 4).AddTile(TileID.Anvils).Register();
	}
}
