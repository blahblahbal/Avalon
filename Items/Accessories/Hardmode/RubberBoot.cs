using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class RubberBoot : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 28;
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
        Item.value = 100000;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
		player.buffImmune[BuffID.Electrified] = true;
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Electrified>()] = true;
	}
}
