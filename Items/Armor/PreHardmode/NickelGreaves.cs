using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class NickelGreaves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(3);
		Item.value = Item.sellPrice(0, 0, 10);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 20).AddTile(TileID.Anvils).Register();
	}
}
