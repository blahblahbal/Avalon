using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class Canister : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}
	public override void SetDefaults()
	{
		Item.DefaultToCanister(9, ModContent.ProjectileType<Projectiles.Ranged.FleshFire>());
		Item.value = Item.sellPrice(0, 0, 0, 2);
		Item.rare = ItemRarityID.Red;
	}
}
