using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class ZincChainmail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(5);
		Item.value = Item.sellPrice(0, 0, 30);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 25).AddTile(TileID.Anvils).Register();
	}
}
