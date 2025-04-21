using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Pets;

public class Longevity : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToGenie();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().Longevity = true;
	}
}
