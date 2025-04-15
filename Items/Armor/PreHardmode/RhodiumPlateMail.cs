using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class RhodiumPlateMail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(7);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.RhodiumBar>(), 20).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 6).AddTile(TileID.Anvils).Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.14f;
		player.GetAttackSpeed(DamageClass.Melee) += 0.14f;
	}
}
