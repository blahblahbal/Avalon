using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class NickelPickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(44, 6, 2f, 12, 19);
		Item.value = Item.sellPrice(silver: 5);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 10)
			.AddRecipeGroup(RecipeGroupID.Wood, 4)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
