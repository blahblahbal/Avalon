using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class ZincPickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(53, 6, 2f, 13, 20);
		Item.value = Item.sellPrice(0, 0, 12, 50);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 10)
			.AddRecipeGroup(RecipeGroupID.Wood, 4)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
