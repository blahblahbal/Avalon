using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Face)]
public class SurgicalMask : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Pathogen>()] = true;
	}
}
