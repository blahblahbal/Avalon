using Avalon.Common.Extensions;
using Avalon.Common.Interfaces;
using Avalon.Dusts;
using Avalon.Prefixes;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class RhodiumGreatsword : ModItem, IItemWithReleaseButtonMidSwingEffect
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.75f;
		ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSword(40, 5f, 35, crit: 6, useTurn: false);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
		Item.scale = 1.2f;
		Item.shootSpeed = 10;
		Item.noMelee = true;
		Item.shoot = ModContent.ProjectileType<RhodiumGreatswordSlash>();
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem); 
		Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax + 1, adjustedItemScale5 * 1.2f);
		return false;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.RhodiumBar>(), 14)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 3)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override float UseSpeedMultiplier(Player player)
	{
		return Utils.Remap(MathF.Pow(player.GetModPlayer<RhodiumGreatswordPlayer>().Power / (float)RhodiumGreatswordPlayer.maxPower,1.4f),0,1,0.7f,2.3f);
	}
	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		damage += Utils.Remap(MathF.Pow(player.GetModPlayer<RhodiumGreatswordPlayer>().Power / (float)RhodiumGreatswordPlayer.maxPower, 1.4f), 0, 1, -0.3f, 0f);
	}

	public void ReleaseButtonMidSwing(Player player)
	{
		RhodiumGreatswordPlayer rgp = player.GetModPlayer<RhodiumGreatswordPlayer>();
		player.itemTime = 0;
		player.itemAnimation = 0;
		int dType = ModContent.DustType<RhodiumDust>();
		for (int i = 0; i < 30; i++)
		{
			ClassExtensions.GetPointOnSwungItemPath(64, 64, 0.2f + 0.8f * Main.rand.NextFloat(), player.HeldItem.scale, out var location2, out var outwardDirection2, player);
			Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
			Dust d = Dust.NewDustPerfect(location2 + Main.rand.NextVector2Circular(12, 12), dType, (vector2 * Main.rand.NextFloat(4)) + Main.rand.NextVector2Circular(1, 1));
			d.noGravity = true;
			d.scale *= 1f;
		}
		if (Main.LocalPlayer == player)
		{
			foreach (Projectile p in Main.ActiveProjectiles)
			{
				if (p.owner == player.whoAmI && p.type == Item.shoot)
				{
					float power = rgp.Power / (float)RhodiumGreatswordPlayer.maxPower;
					int damage = (int)Math.Max(player.GetTotalDamage(Item.DamageType).ApplyTo(Item.damage * power * power * 6f), 1);
					Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, player.Center.DirectionTo(Main.MouseWorld) * Item.shootSpeed, ModContent.ProjectileType<RhodiumGreatswordBeam>(), damage, Item.knockBack, player.whoAmI, player.direction * player.gravDir, p.rotation, ai2: rgp.Power);
					p.Kill();
					break;
				}
			}
		}
		rgp.Power = 0;
	}
}
public class RhodiumGreatswordPlayer : ModPlayer
{
	public byte Power = 0;
	public static byte maxPower = 10;
	public override void ResetEffects()
	{
		if (Player.HeldItem.type != ModContent.ItemType<RhodiumGreatsword>())
		{
			Power = 0;
			return;
		}
		if (Power > maxPower) Power = maxPower;
	}
}
