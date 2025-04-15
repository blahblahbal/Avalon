using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class BismuthGreaves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(5);
		Item.value = Item.sellPrice(0, 0, 50);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 25).AddTile(TileID.Anvils).Register();
	}
}
