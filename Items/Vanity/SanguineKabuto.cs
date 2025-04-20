using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class SanguineKabuto : ModItem
	{
		public override void SetDefaults()
		{
			Item.value = Item.sellPrice(0, 1, 20);
			Item.rare = ItemRarityID.Orange;
			Item.vanity = true;
		}
	}
}
