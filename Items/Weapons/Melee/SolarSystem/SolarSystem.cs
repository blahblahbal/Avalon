using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.SolarSystem;

public class SolarSystem : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToYoyo(ModContent.ProjectileType<Sun>(), 200, 8f, 12f, width: 46, height: 36);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 19, 98);
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<Sun>()] == 0;
	}
	public override void HoldItemFrame(Player player)
	{
		if (player.channel)
		{
			player.bodyFrame.Y = player.bodyFrame.Height * 3;
		}
	}
}
