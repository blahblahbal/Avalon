using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;
public class MinersSword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(20, 5.5f, 23, useTurn: false, width: 32, height: 32);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
}