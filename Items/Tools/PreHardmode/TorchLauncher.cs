using Avalon.Projectiles.Tools;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

internal class TorchLauncher : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToRangedWeapon(24, 14, ModContent.ProjectileType<Torch>(), ItemID.Torch, 1, 0f, 8f, 16, 16);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 78);
	}
	private int ammoItem;
	public override void OnConsumeAmmo(Item ammo, Player player)
	{
		ammoItem = ammo.type;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai2: ammoItem);
		return false;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Torch, 50)
			.AddIngredient(ItemID.IronBar, 10)
			.AddIngredient(ItemID.Wood, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
