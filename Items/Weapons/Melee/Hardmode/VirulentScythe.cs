using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class VirulentScythe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBoomerang(ModContent.ProjectileType<Projectiles.Melee.VirulentScythe>(), 50, 2.5f, 18, 15f, true, width: 34, height: 36);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 20);
	}
}
