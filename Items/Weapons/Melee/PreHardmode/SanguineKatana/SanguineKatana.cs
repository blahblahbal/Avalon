using Avalon;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Particles;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.SanguineKatana;

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
public class SanguineKatanaSlash : EnergySlashTemplate
{
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TheHorsemansBlade}";
	public override bool PreDraw(ref Color lightColor)
	{
		DrawSlash(Color.Black * 0.2f, Color.Black * 0.2f, Color.Red * 0.2f, Color.Black * 0.5f, 512, 1f, MathHelper.PiOver4, -0.2f, 0, false);
		DrawSlash(new Color(255, 0, 0) * 0.6f, new Color(128, 0, 0) * 0.5f, new Color(0, 0, 0) * 0.5f, Color.Red * 0.2f, 512, 1f, 0, -0.2f, 0, true);
		return false;
	}
	public override void AI()
	{
		float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
		Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
		Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
		if (Main.rand.NextFloat() * 0.5f < Projectile.Opacity)
		{
			Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), DustID.Blood, vector3 * 3f, 100, default, 1f);
			dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.5f;
			dust2.noGravity = true;
		}
		if (Main.rand.NextFloat() * 0.5f < Projectile.Opacity)
		{
			Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), DustID.RedTorch, vector3 * 3f, 254, default, 1f);
			dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
			dust2.noGravity = true;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Projectile.penetrate < 90)
		{
			Player player = Main.player[Projectile.owner];
			if (target.type != NPCID.TargetDummy && !NPCID.Sets.CountsAsCritter[target.type])
			{
				int healAmount = Main.rand.Next(0, 3) + Main.rand.Next(1, 3) + 3;
				player.HealEffect(healAmount, true);
				player.statLife += healAmount;
				Projectile.penetrate = 100;
			}
		}
		Vector2 vector = Main.rand.NextVector2FromRectangle(target.Hitbox);
		SoundEngine.PlaySound(SoundID.NPCHit1, target.position);
		Vector2 pos = vector - new Vector2(0, 24);
		Vector2 vel = new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), 1);
		ParticleSystem.AddParticle(new SanguineCuts(), pos, vel, default);
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			Network.SyncParticles.SendPacket(ParticleType.SanguineCuts, pos, vel, default, Projectile.owner);
		}
	}
	public override void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawpos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
	{
		Texture2D value = TextureAssets.Extra[98].Value;
		Color color = shineColor * opacity;
		color.A = 0;
		Vector2 origin = value.Size() / 2f;
		Color color2 = drawColor * 0.5f;
		float num = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
		Vector2 vector = new Vector2(fatness.X * 0.5f, scale.X) * num;
		Vector2 vector2 = new Vector2(fatness.Y * 0.5f, scale.Y) * num;
		color *= num;
		color2 *= num;
		for (int i = 0; i < 3; i++)
		{
			Main.EntitySpriteDraw(value, drawpos, null, color, (float)Math.PI / 2f + rotation, origin, vector, dir);
			Main.EntitySpriteDraw(value, drawpos, null, color, 0f + rotation, origin, vector2, dir);
		}
	}
}
