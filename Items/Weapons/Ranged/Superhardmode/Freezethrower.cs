using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Superhardmode;

public class Freezethrower : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToFlamethrower(ModContent.ProjectileType<Projectiles.Ranged.Freezethrower>(), 70, 0.625f, 10.5f, 5, 30);
		Item.ArmorPenetration = 30;
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 20);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float f = 0.05f;
		if (Main.myPlayer == player.whoAmI)
		{
			Projectile.NewProjectile(source, position, Vector2.SmoothStep(velocity.RotatedBy(-f), velocity.RotatedBy(f), Main.masterColor), type, damage, knockback, player.whoAmI);
		}
		return false;
	}
	public override Vector2? HoldoutOffset() => new Vector2(-10, 0);
}
