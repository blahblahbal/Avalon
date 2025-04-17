using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class EnergyCrystal : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.AegisCrystal);
	}
	public override bool CanUseItem(Player player)
	{
		return !player.GetModPlayer<AvalonStaminaPlayer>().EnergyCrystal;
	}
	public override bool? UseItem(Player player)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().EnergyCrystal = true;
		return true;
	}
}
