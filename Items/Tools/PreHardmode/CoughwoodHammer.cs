using Avalon.Items.Armor.PreHardmode;
using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class CoughwoodHammer : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.ShadewoodHammer);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Coughwood>(), 8)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<CoughwoodGreaves>())
			.Register();
	}
}
