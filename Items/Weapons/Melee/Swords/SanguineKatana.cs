using Avalon;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class SanguineKatana : ModItem
{
	public override bool MeleePrefix()
	{
		return true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<SanguineKatanaSlash>(), 22, 5f, 16f, 24, 24, useTurn: true);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
		{
			float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem);
			Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<SanguineKatanaSlash>(), (int)(damage * 1.25f), knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax * 1f, adjustedItemScale5 * 1.3f);
			NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
		}
		return false;
	}
	public override bool CanShoot(Player player)
	{
		if (!player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
		{
			Item.noMelee = false;
			Item.useTurn = true;
			return false;
		}
		else if (player.ItemAnimationJustStarted)
		{
			Item.noMelee = true;
			Item.useTurn = false;
			return true;
		}
		return false;
	}
	public override bool AltFunctionUse(Player player)
	{
		int healthSucked = 80;
		if (!player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
		{
			player.AddBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>(), 60 * 8);
			SoundEngine.PlaySound(SoundID.NPCDeath1, Main.LocalPlayer.position);

			CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, healthSucked, dramatic: false, dot: false);
			player.statLife -= healthSucked;
			if (player.statLife <= 0)
			{
				player.Hurt(PlayerDeathReason.ByCustomReason(NetworkText.FromKey($"Mods.Avalon.DeathText.{Name}_1", $"{player.name}")), healthSucked, 1, false, true, -1, false);
			}
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDustPerfect(player.MountedCenter, DustID.Blood, player.velocity * 0.85f + Main.rand.NextVector2Circular(2f, 1f).RotatedBy(i) + Main.rand.NextVector2Square(-3.5f, 3.5f), 100, default, 0.7f + Main.rand.NextFloat() * 0.6f);
				Dust.NewDustPerfect(player.MountedCenter, DustID.Blood, player.velocity * 0.85f + Main.rand.NextVector2Circular(0.5f, 0.5f).RotatedBy(i) + Main.rand.NextVector2Square(-1.5f, 1.5f), 100, default, 1f + Main.rand.NextFloat() * 0.6f);
			}
		}
		return !player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>());
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
		Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * player.direction * player.gravDir);
		Dust.NewDustPerfect(location2, DustID.Blood, vector2 * 2f, 100, default, 0.7f + Main.rand.NextFloat() * 0.6f);
	}
}
