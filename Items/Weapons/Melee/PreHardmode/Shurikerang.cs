using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class Shurikerang : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBoomerang(ModContent.ProjectileType<Projectiles.Melee.Shurikerang>(), 14, 3f, 20, 12f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(silver: 60);
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 3;
	}
}
