using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class Starstorm : ModItem
{
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 128);
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ProjectileID.StarWrath, 28, 6f, 30f, 20, 15);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 7);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int starCount = (int)(5 - MathHelper.Clamp(player.Center.Distance(Main.MouseWorld) * 0.01f, 1, 4));
		for (int i = 0; i < starCount; i++)
		{
			Vector2 StarSpawnPosition = new Vector2(Main.MouseWorld.X + Main.rand.Next(-60, 60), player.position.Y - 500);
			int P = Projectile.NewProjectile(source, StarSpawnPosition, StarSpawnPosition.DirectionTo(Main.MouseWorld) * velocity.Length() * Main.rand.NextFloat(0.7f, 1.1f), type, (int)((damage * (1f - starCount * 0.1f)) * 1.25f), knockback / 2, player.whoAmI, ai1: Main.MouseWorld.Y);
		}
		return false;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.FallenStar, 25)
			.AddIngredient(ItemID.MeteoriteBar, 25)
			.AddIngredient(ItemID.SoulofLight, 5)
			.AddIngredient(ItemID.SoulofSight, 5)
			.AddIngredient(ItemID.LightShard, 1)
			.AddTile(TileID.MythrilAnvil).Register();
	}
}
