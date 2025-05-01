using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class ZincAxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAxe(55, 8, 4.5f, 17, 26);
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
