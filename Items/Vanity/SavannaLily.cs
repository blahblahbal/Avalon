using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
public class SavannaLily : ModItem
{
	public override void SetStaticDefaults()
	{
		ArmorIDs.Head.Sets.DrawFullHair[EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "SavannaLily", EquipType.Head)] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToVanity();
		Item.value = Item.sellPrice(copper: 20);
	}
}
