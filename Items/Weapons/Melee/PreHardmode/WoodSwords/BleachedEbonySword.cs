using Avalon.Items.Tools.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.WoodSwords;

public class BleachedEbonySword : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.RichMahoganySword);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Placeable.Tile.BleachedEbony>(), 7)
			.AddTile(TileID.WorkBenches)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<BleachedEbonyHammer>())
			.Register();
	}
}
