using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class SpectralBullet : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		Item.DefaultToBullet(12, ModContent.ProjectileType<Projectiles.Ranged.SpectralBullet>(), 11f, 6f);
		Item.rare = ModContent.RarityType<Rarities.SapphirephlyRarity>();
		Item.value = Item.sellPrice(0, 0, 2, 40);
	}
	public override void AddRecipes()
	{
		CreateRecipe(70)
			.AddIngredient(ItemID.MusketBall, 70)
			.AddIngredient(ItemID.Ectoplasm, 2)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	//public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	//{
	//    Vector2 pos = player.Center + new Vector2(1000, 0).RotatedBy(player.AngleTo(Main.MouseWorld));
	//    Projectile.NewProjectile(source, pos.X, pos.Y, velocity.X * 3, velocity.Y * 3, Type, damage, knockback);
	//    return false;
	//}
}
