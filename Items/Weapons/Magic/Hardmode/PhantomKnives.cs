using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class PhantomKnives : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<Projectiles.Magic.PhantomKnife>(), 51, 3.75f, 18, 15f, 16, true, noUseGraphic: true, width: 18, height: 20);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 30);
		Item.UseSound = SoundID.Item39;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
							   int type, int damage, float knockback)
	{
		int numberProjectiles = Main.rand.Next(4, 8);
		for (int i = 0; i < numberProjectiles; i++)
		{
			Vector2 perturbedSpeed = AvalonUtils.GetShootSpread(velocity, position, Type, MathHelper.ToRadians(20), random: true, maxRotUnsigned: Math.PI);
			Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
		}

		return false;
	}
}
