using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

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
			.AddIngredient(ModContent.ItemType<Placeable.Tile.Coughwood>(), 10)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
