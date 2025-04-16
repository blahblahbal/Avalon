using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class CoughwoodGreaves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(2);
		Item.value = Item.sellPrice(0, 0, 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Placeable.Tile.Coughwood>(), 25)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
