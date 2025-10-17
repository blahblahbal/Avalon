using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Shards;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.FeroziumIceSword;

public class FeroziumIceSword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<IcicleFerozium>(), 50, 6f, 15f, 20, 20, crit: 2, scale: 1.5f, width: 54, height: 54);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 7);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.AdamantiteBar, 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.TitaniumBar, 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<TroxiniumBar>(), 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class IcicleFerozium : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 12;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 2;
		Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 20f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
			Projectile.velocity.X = Projectile.velocity.X * 0.97f;
		}
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}
}