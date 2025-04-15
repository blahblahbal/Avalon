using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class BismuthChainmail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(5);
		Item.value = Item.sellPrice(0, 0, 60);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 30)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
