using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Spears;

public class CaesiumPike : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<CaesiumPikeProj>(), 120, 4.5f, 30, 4f, true, scale: 1.1f);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 20);
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}
