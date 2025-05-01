using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Legs)]
public class PaladinGreaves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToVanity();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 1);
	}
}
