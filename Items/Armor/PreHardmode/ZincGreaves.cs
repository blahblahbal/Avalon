using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class ZincGreaves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(4);
		Item.value = Item.sellPrice(0, 0, 25);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 20).AddTile(TileID.Anvils).Register();
	}
}
