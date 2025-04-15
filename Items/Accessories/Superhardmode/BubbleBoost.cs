using Avalon.Common.Players;
using Avalon.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;

public class BubbleBoost : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ModContent.RarityType<BlueRarity>();
		Item.value = Item.sellPrice(0, 15);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().BubbleBoost = true;
		player.noFallDmg = true;
	}
}
