using Avalon.Projectiles.Ranged.Held;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class Longbow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToLongbow(44, 2.3f, 24f, 55);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 54);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LongbowHeld>(), damage, knockback, player.whoAmI, type);
		return false;
	}
}
