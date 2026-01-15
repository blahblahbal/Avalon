using Avalon.Common.Extensions;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords.WoodSwords;

public class CoughwoodSword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(11, 5.5f, 18, false, width: 24, height: 28);
		Item.value = Item.sellPrice(copper: 20);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Coughwood>(), 7)
			.AddTile(TileID.WorkBenches)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<CoughwoodHammer>())
			.Register();
	}
}
