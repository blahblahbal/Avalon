using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Superhardmode;

[AutoloadEquip(EquipType.Body)]
public class BlahsHauberk : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(100);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(0, 40);
	}
	public override void UpdateEquip(Player player)
	{
		player.aggro += 1000;
		player.manaCost -= 0.3f;
		player.statManaMax2 += 800;
		player.maxMinions += 12;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return (head.type == ModContent.ItemType<BlahsHeadguard>() && body.type == ModContent.ItemType<BlahsHauberk>() && legs.type == ModContent.ItemType<BlahsCuisses>());
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "Shroomite stealth, Go Berserk, Rosebuds, Spectrum Speed, Attackers also take double full damage, and Spectre Heal";
		player.GetModPlayer<AvalonPlayer>().DoubleDamage = player.ghostHeal = player.shroomiteStealth = true;
		player.GetModPlayer<AvalonPlayer>().SpectrumSpeed = player.GetModPlayer<AvalonPlayer>().GoBerserk = true;
		player.GetModPlayer<AvalonPlayer>().RoseMagic = player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
	}
}
