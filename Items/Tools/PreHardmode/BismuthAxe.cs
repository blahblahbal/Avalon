using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class BismuthAxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAxe(65, 9, 4.5f, 17, 25);
		Item.value = Item.sellPrice(silver: 22);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 8).AddRecipeGroup(RecipeGroupID.Wood, 3).AddTile(TileID.Anvils).Register();
	}
}
