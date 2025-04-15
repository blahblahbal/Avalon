using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class AncientTitaniumPlateMail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(7);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 2);
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.14f;
		player.GetAttackSpeed(DamageClass.Melee) += 0.14f;
	}
}
