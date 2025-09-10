using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class AcidproofFishingHook : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory(34, 30);
		Item.SetShopValues(ItemRarityColor.Lime7, Item.sellPrice(0, 2));
	}
	public override void UpdateEquip(Player player)
	{
		player.GetModPlayer<MLLPlayer>().accAcidFishing = true;
	}
}
