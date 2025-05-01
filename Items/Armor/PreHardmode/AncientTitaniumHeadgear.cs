using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class AncientTitaniumHeadgear : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(7);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 2);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return (body.type == ModContent.ItemType<AncientTitaniumPlateMail>() || body.type == ModContent.ItemType<RhodiumPlateMail>()) &&
			(legs.type == ModContent.ItemType<AncientTitaniumGreaves>() || legs.type == ModContent.ItemType<RhodiumGreaves>());
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Rhodium");
		player.GetDamage(DamageClass.Generic) += 0.09f;
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 40;
		player.GetDamage(DamageClass.Ranged) += 0.14f;
	}
}
