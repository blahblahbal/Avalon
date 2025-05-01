using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class NickelHammer : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToHammer(45, 8, 5.5f, 17, 27);
		Item.value = Item.sellPrice(silver: 4);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 8)
			.AddRecipeGroup(RecipeGroupID.Wood, 3)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
