using Avalon.Common.Extensions;
using Avalon.Projectiles.Ranged.Ammo;
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
		Item.DefaultToCanister(9, ModContent.ProjectileType<CanisterFire>());
		Item.value = Item.sellPrice(0, 0, 0, 2);
		Item.rare = ItemRarityID.Red;
	}
}

