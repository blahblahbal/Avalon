using Avalon.Common.Extensions;
using Avalon.Projectiles.Ranged.Held;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class Longbone : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToLongbow(33, 2.3f, 30f, 83);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 50);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LongboneHeld>(), damage, knockback, player.whoAmI, type);
		return false;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.Bone, 24)
			.AddIngredient(ItemID.Cobweb, 25)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
