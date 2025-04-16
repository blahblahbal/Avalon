using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Superhardmode;

[AutoloadEquip(EquipType.Head)]
public class BlahsHeadguard : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(100);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(0, 40);
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Generic) += 0.35f;
		player.GetCritChance(DamageClass.Generic) += 11;
	}
}
