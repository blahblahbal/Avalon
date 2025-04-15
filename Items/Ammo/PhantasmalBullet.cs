using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class PhantasmalBullet : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		Item.DefaultToBullet(18, ModContent.ProjectileType<Projectiles.Ranged.PhantasmalBullet>(), 11f, 6f);
		Item.rare = ModContent.RarityType<Rarities.QuibopsRarity>();
		Item.value = Item.sellPrice(0, 0, 2, 40);
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddIngredient(ModContent.ItemType<SpectralBullet>(), 70)
	//        .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 2)
	//        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
	//        .Register();
	//}
	//public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	//{
	//    Vector2 pos = player.Center + new Vector2(50, 0).RotatedBy(player.AngleTo(Main.MouseWorld));
	//    Projectile.NewProjectile(source, pos.X, pos.Y, velocity.X * 3, velocity.Y * 3, Type, damage, knockback);
	//    return false;
	//}
}
