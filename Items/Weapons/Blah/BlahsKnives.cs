using Avalon.Common.Extensions;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah;

public class BlahsKnives : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<Projectiles.Magic.BlahKnife>(), 95, 3.75f, 14, 15f, 14, true, noUseGraphic: true, width: 18, height: 20);
		Item.rare = ModContent.RarityType<BlahRarity>();
		Item.value = Item.sellPrice(0, 50);
		Item.UseSound = SoundID.Item39;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
							   int type, int damage, float knockback)
	{
		int numberProjectiles = Main.rand.Next(4, 8); // AvalonGlobalProjectile.HowManyProjectiles(4, 8);
		for (int i = 0; i < numberProjectiles; i++)
		{
			Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
			Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage,
				knockback, player.whoAmI);
		}

		return false;
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddIngredient(ModContent.ItemType<Magic.PhantomKnives>())
	//        .AddIngredient(ModContent.ItemType<Melee.KnivesoftheCorruptor>())
	//        .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 40)
	//        .AddIngredient(ModContent.ItemType<Placeable.Bar.SuperhardmodeBar>(), 35)
	//        .AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 40)
	//        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
	//        .Register();
	//}
}
