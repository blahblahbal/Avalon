using Avalon;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Boomerangs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Boomerangs;

public class TetanusChakram : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBoomerang(ModContent.ProjectileType<TetanusChakramProj>(), 27, 2.5f, 15, 7.3f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1, 50);
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 2;
	}
}
