using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
public class TerrorPenguinsEarpiece : ModItem
{
	public override void SetStaticDefaults()
	{
		ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToVanity();
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.sellPrice(0, 5);
	}
}
