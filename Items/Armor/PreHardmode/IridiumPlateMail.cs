using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class IridiumPlateMail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(9);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1, 40);
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.11f;
		player.GetAttackSpeed(DamageClass.Melee) += 0.11f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 20)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 6)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
