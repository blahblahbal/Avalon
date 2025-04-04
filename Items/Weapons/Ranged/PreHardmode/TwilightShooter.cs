using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class TwilightShooter : ModItem
{
	public enum AmmoType
	{
		None = 0,
		Arrow = 1,
		Bullet = 2,
		FallenStar = 3,
		DartSeed = 4,
		Rocket = 5,
		Gel = 6
	}
	public override void SetDefaults()
	{
		Item.width = 42;
		Item.height = 18;
		Item.UseSound = SoundID.Item34;
		Item.damage = 24;
		Item.autoReuse = false;
		Item.useAmmo = AmmoID.Bullet;
		Item.shootSpeed = 5f;
		Item.DamageType = DamageClass.Ranged;
		Item.noMelee = true;
		Item.rare = ItemRarityID.Orange;
		Item.useTime = 20;
		Item.knockBack = 0.625f;
		Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.RhotukaSpinnerScrambler>();
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.value = 1000000;
		Item.useAnimation = 20;
	}
	public override void HoldItem(Player player)
	{
		AmmoType ammo = Style(player);
		if (ammo == AmmoType.Arrow)
		{
			Item.UseSound = SoundID.Item5;
			Item.useAnimation = Item.useTime = 20;
			Item.shootSpeed = 8f;
		}
		if (ammo == AmmoType.Bullet)
		{
			Item.UseSound = SoundID.Item11;
			Item.useAnimation = Item.useTime = 9;
			Item.shootSpeed = 10f;
		}
		if (ammo == AmmoType.FallenStar)
		{
			Item.UseSound = SoundID.Item9;
			Item.useAnimation = Item.useTime = 15;
			Item.shoot = ProjectileID.StarCannonStar;
			Item.shootSpeed = 14f;
		}
		if (ammo == AmmoType.DartSeed)
		{
			Item.UseSound = SoundID.Item5;
			Item.useAnimation = Item.useTime = 13;
			Item.shootSpeed = 11f;
		}
	}
	private AmmoType Style(Player player)
	{
		bool flag = false;
		for (int j = 54; j < 58; j++)
		{
			if (player.inventory[j].stack > 0)
			{
				if (player.inventory[j].ammo == AmmoID.Arrow)
				{
					return AmmoType.Arrow;
				}
				else if (player.inventory[j].ammo == AmmoID.Bullet)
				{
					return AmmoType.Bullet;
				}
				else if (player.inventory[j].ammo == AmmoID.FallenStar)
				{
					return AmmoType.FallenStar;
				}
				else if (player.inventory[j].ammo == AmmoID.Dart)
				{
					return AmmoType.DartSeed;
				}
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			for (int k = 0; k < 54; k++)
			{
				if (player.inventory[k].stack > 0)
				{
					if (player.inventory[k].ammo == AmmoID.Arrow)
					{
						return AmmoType.Arrow;
					}
					else if (player.inventory[k].ammo == AmmoID.Bullet)
					{
						return AmmoType.Bullet;
					}
					else if (player.inventory[k].ammo == AmmoID.FallenStar)
					{
						return AmmoType.FallenStar;
					}
					else if (player.inventory[k].ammo == AmmoID.Dart)
					{
						return AmmoType.DartSeed;
					}
					break;
				}
			}
		}
		return 0;
	}
	public override Vector2? HoldoutOffset() => new Vector2(-10, 0);
}
