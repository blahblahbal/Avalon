using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class ZincHammer : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToHammer(49, 9, 5.5f, 18, 28);
		Item.value = Item.sellPrice(silver: 10);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 8)
			.AddRecipeGroup(RecipeGroupID.Wood, 3)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
