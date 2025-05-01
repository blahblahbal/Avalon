using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class HemorrhagingHalberd : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<Projectiles.Melee.HemorrhagingHalberd>(), 48, 4.5f, 35, 4f, true);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 3);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float RotationAmount = 0.4f;
		Projectile.NewProjectile(source, position, velocity.RotatedBy(RotationAmount * -player.direction), type, damage / 2, knockback, player.whoAmI, 0, 0, RotationAmount * 2);
		return false;
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}
