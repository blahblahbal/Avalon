using Avalon.Common.Extensions;
using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Items.Weapons.Melee.Maces.WoodenClubs;

namespace Avalon.Items.Weapons.Ranged.Bows;

public class CoughwoodBow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBow(9, 0f, 7f, 27, 27, width: 12, height: 28);
		Item.value = Item.sellPrice(copper: 20);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Coughwood>(), 10)
			.AddTile(TileID.WorkBenches)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<CoughwoodClub>())
			.Register();
	}
}
