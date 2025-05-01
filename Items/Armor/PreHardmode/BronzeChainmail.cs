using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class BronzeChainmail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(2);
		Item.value = Item.sellPrice(0, 0, 6);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 20).AddTile(TileID.Anvils).Register();
	}
}
