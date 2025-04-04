using Avalon.Common;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class AmmoHook : ModHook
{
	protected override void Apply()
	{
		On_Player.PickAmmo_Item_refInt32_refSingle_refBoolean_refInt32_refSingle_refInt32_bool += PickAmmo;
	}

	private void PickAmmo(On_Player.orig_PickAmmo_Item_refInt32_refSingle_refBoolean_refInt32_refSingle_refInt32_bool orig, Player self, Item sItem, ref int projToShoot, ref float speed, ref bool canShoot, ref int totalDamage, ref float KnockBack, out int usedAmmoItemId, bool dontConsume)
	{
		if (sItem.type == ModContent.ItemType<TwilightShooter>())
		{
			usedAmmoItemId = 0;
			Item item = ChooseAmmoType(self, sItem);
			canShoot = item != null;
			bool shootWithNoAmmo = false;

			if (!canShoot && !ItemLoader.NeedsAmmo(sItem, self))
			{
				item = ContentSamples.ItemsByType[sItem.useAmmo];
				if (item.ammo == sItem.useAmmo)
				{
					shootWithNoAmmo = (canShoot = true);
				}
			}
			if (!canShoot)
			{
				return;
			}
			usedAmmoItemId = item.type;
			StatModifier ammoDamage = self.GetTotalDamage(item.DamageType);
			if (AmmoID.Sets.IsArrow[item.ammo])
			{
				ammoDamage = ammoDamage.CombineWith(self.arrowDamage);
			}
			if (AmmoID.Sets.IsBullet[item.ammo])
			{
				ammoDamage = ammoDamage.CombineWith(self.bulletDamage);
			}
			ammoDamage.Base = 0f;
			ammoDamage.Flat = totalDamage;
			ref float Damage = ref ammoDamage.Flat;
			int pickedProjectileId = -1;

			if (sItem.useAmmo == AmmoID.Rocket)
			{
				projToShoot += item.shoot;
			}
			else if (item.shoot > ProjectileID.None)
			{
				projToShoot = item.shoot;
			}
			speed += item.shootSpeed;
			//if (self.magicQuiver && (sItem.useAmmo == AmmoID.Arrow || sItem.useAmmo == AmmoID.Stake))
			//{
			//	KnockBack *= 1.1f;
			//	speed *= 1.1f;
			//}
			//if ((sItem.useAmmo == AmmoID.Arrow || sItem.useAmmo == AmmoID.Stake) && self.archery && speed < 20f)
			//{
			//	speed *= 1.2f;
			//	if (speed > 20f)
			//	{
			//		speed = 20f;
			//	}
			//}
			KnockBack += item.knockBack;
			ItemLoader.PickAmmo(sItem, item, self, ref projToShoot, ref speed, ref ammoDamage, ref KnockBack);
			totalDamage = (int)(ammoDamage.ApplyTo(item.damage) + 5E-06f);
			if (!dontConsume && !shootWithNoAmmo && item.consumable && !self.IsAmmoFreeThisShot(sItem, item, projToShoot))
			{
				CombinedHooks.OnConsumeAmmo(self, sItem, item);
				item.stack--;
				if (item.stack <= 0)
				{
					item.active = false;
					item.TurnToAir();
				}
			}

		}
		else orig.Invoke(self, sItem, ref projToShoot, ref speed, ref canShoot, ref totalDamage, ref KnockBack, out usedAmmoItemId, dontConsume);
	}
	private Item ChooseAmmoType(Player player, Item weapon)
	{
		Item item = null;
		bool flag = false;
		for (int j = 54; j < 58; j++)
		{
			if (player.inventory[j].stack > 0 &&
				(player.inventory[j].ammo == AmmoID.Arrow || player.inventory[j].ammo == AmmoID.Bullet ||
				player.inventory[j].ammo == AmmoID.FallenStar || player.inventory[j].ammo == AmmoID.Dart))
			{
				item = player.inventory[j];
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			for (int k = 0; k < 54; k++)
			{
				if (player.inventory[k].stack > 0 &&
					(player.inventory[k].ammo == AmmoID.Arrow || player.inventory[k].ammo == AmmoID.Bullet ||
					player.inventory[k].ammo == AmmoID.FallenStar || player.inventory[k].ammo == AmmoID.Dart))
				{
					item = player.inventory[k];
					break;
				}
			}
		}
		return item;
	}
}
