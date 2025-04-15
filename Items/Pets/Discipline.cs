using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Pets;

public class Discipline : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.WispinaBottle);
		Item.shoot = ProjectileID.None;
		Item.buffType = 0;
		Item.value = Item.buyPrice(0, 20);
		Item.rare = ItemRarityID.Green;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Genie = true;
		Item.accessory = true;
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().Discipline = true;
	}
}
