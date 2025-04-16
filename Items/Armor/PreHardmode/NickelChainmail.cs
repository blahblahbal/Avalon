using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class NickelChainmail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(3);
		Item.value = Item.sellPrice(0, 0, 12);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 25).AddTile(TileID.Anvils).Register();
	}
}
