using Avalon.Common.Extensions;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class PossessedFlamesaw : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<Projectiles.Melee.PossessedFlamesaw>(), 95, 9f, 20f, 15, 15, false, noMelee: true, width: 46, height: 16);
		Item.noUseGraphic = true;
		Item.rare = ModContent.RarityType<BlueRarity>();
		Item.value = Item.sellPrice(0, 40);
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
	public override bool? UseItem(Player player)
	{
		float velX = Main.mouseX + Main.screenPosition.X - player.Center.X;
		float velY = Main.mouseY + Main.screenPosition.Y - player.Center.Y;
		float num72 = (float)Math.Sqrt((double)(velX * velX + velY * velY));
		float num73 = num72;
		num72 = Item.shootSpeed / num72;
		velX *= num72;
		velY *= num72;
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.MountedCenter, new Vector2(velX, velY), ModContent.ProjectileType<Projectiles.Melee.PossessedFlamesawChop>(), (int)player.GetDamage(DamageClass.Melee).ApplyTo(Item.damage), Item.knockBack, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.MountedCenter, new Vector2(velX, velY), Item.shoot, (int)player.GetDamage(DamageClass.Melee).ApplyTo(Item.damage), Item.knockBack, player.whoAmI);
		}
		return true;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		return false;
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Melee.PossessedFlamesawChop>()] < 1;
	}
}
