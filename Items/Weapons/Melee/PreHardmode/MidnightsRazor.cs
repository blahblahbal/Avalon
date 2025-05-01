using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class MidnightsRazor : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetDefaults()
	{
		Item.DefaultToBoomerang(ModContent.ProjectileType<Projectiles.Melee.MidnightsRazor>(), 40, 2.5f, 15, 25f, true);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 4);
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}
