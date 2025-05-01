using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class TetanusChakram : ModItem
{
	int ShootTimes;
	public override void SetDefaults()
	{
		Item.DefaultToBoomerang(ModContent.ProjectileType<Projectiles.Melee.TetanusChakram>(), 27, 2.5f, 15, 7.3f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1, 50);
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 2;
	}
}
