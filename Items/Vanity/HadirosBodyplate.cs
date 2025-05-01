using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Body)]
public class HadirosBodyplate : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToVanity();
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.sellPrice(0, 5);
	}
}
