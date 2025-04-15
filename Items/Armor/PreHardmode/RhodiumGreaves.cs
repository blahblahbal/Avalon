using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class RhodiumGreaves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(7);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.RhodiumBar>(), 17).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 5).AddTile(TileID.Anvils).Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Magic) += 0.14f;
	}
}
