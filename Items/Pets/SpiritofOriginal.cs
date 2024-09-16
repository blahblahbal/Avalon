using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Avalon.Common;
using Avalon.Common.Players;

namespace Avalon.Items.Pets;

internal class SpiritofOriginal : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.WispinaBottle);
		Item.shoot = 0;
		Item.buffType = 0;
		Item.value = Item.buyPrice(0, 20);
		Item.rare = ItemRarityID.Green;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Genie = true;
		Item.accessory = true;
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().SpiritOfOriginal = true;
	}
}
