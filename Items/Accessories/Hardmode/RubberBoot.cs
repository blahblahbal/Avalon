using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class RubberBoot : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[BuffID.Electrified] = true;
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Electrified>()] = true;
	}
}
