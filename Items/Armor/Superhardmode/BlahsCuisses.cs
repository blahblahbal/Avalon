using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Superhardmode;

[AutoloadEquip(EquipType.Legs)]
public class BlahsCuisses : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(100);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(0, 40);
	}
	public override void UpdateEquip(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().OblivionKill = true;
		player.GetModPlayer<AvalonPlayer>().SplitProj = true;
		player.GetModPlayer<AvalonPlayer>().TeleportV = true;
	}
}
