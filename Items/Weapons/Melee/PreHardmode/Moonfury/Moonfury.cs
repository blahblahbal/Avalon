using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.PreHardmode.Sporalash;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.Moonfury;

public class Moonfury : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
	}
	public override void SetDefaults()
	{
		Item.DefaultToFlail(ModContent.ProjectileType<MoonfuryProj>(), 35, 6.75f, 42, 12f);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 4);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.BlueMoon)
			.AddIngredient(ItemID.Sunfury)
			.AddIngredient(ItemID.BallOHurt)
			.AddIngredient(ModContent.ItemType<Sporalash.Sporalash>())
			.AddTile(TileID.DemonAltar).Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.BlueMoon)
			.AddIngredient(ItemID.Sunfury)
			.AddIngredient(ItemID.TheMeatball)
			.AddIngredient(ModContent.ItemType<Sporalash.Sporalash>())
			.AddTile(TileID.DemonAltar).Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.BlueMoon)
			.AddIngredient(ItemID.Sunfury)
			.AddIngredient(ModContent.ItemType<TheCell.TheCell>())
			.AddIngredient(ModContent.ItemType<Sporalash.Sporalash>())
			.AddTile(TileID.DemonAltar).Register();
	}
}
public class MoonfuryProj : FlailTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<Moonfury>().DisplayName;
	public override int LaunchTimeLimit => 19;
	public override float LaunchSpeed => 18f;
	public override float MaxLaunchLength => 800f;
	public override float RetractAcceleration => 5f;
	public override float MaxRetractSpeed => 16f;
	public override float ForcedRetractAcceleration => 5f;
	public override float MaxForcedRetractSpeed => 16f;
	public override int DefaultHitCooldown => 10;
	public override int SpinHitCooldown => 20;
	public override int MovingHitCooldown => 10;
	public override int ExtraProjectile => ModContent.ProjectileType<MoonfuryBlade>();
	public override HeadRotationStyle CurrentRotationStyle => HeadRotationStyle.AlwaysFacePlayer;

	public override void SetStaticDefaults()
	{
		// These lines facilitate the trail drawing
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = Projectile.height = 40;
		Projectile.localNPCHitCooldown = 40; // This facilitates custom hit cooldown logic
	}

	public override bool EmitDust(int dustType, Vector2? posMod, Vector2? velMod, float velMaxRadians, float velMult, int antecedent, int consequent, float fadeIn, bool noGravity, float scale, byte alpha)
	{
		Player player = Main.player[Projectile.owner];

		if (Projectile.velocity.Length() > 3 || CurrentAIState == AIState.Spinning)
		{
			Dust dust = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.Shadowflame, Projectile.velocity.RotatedByRandom(0.1f) * 0.7f, 128);
			dust.fadeIn = 1.5f;
			dust.noGravity = true;
			dust.scale = 1.5f;
			Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.ShadowbeamStaff, Projectile.velocity.RotatedByRandom(0.1f), 128);
			dust2.fadeIn = 1.5f;
			dust2.noGravity = true;
			dust2.scale = 1.5f;
			if (CurrentAIState == AIState.Spinning)
			{
				dust.velocity = new Vector2(0, -3 * player.direction).RotatedBy(Projectile.Center.DirectionTo(player.Center).ToRotation()).RotatedByRandom(0.1f);
				dust2.velocity = new Vector2(0, -5 * player.direction).RotatedBy(Projectile.Center.DirectionTo(player.Center).ToRotation()).RotatedByRandom(0.1f);
			}
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ParticleOrchestraSettings particleOrchestraSettings = default;
		particleOrchestraSettings.PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
		ParticleOrchestraSettings settings = particleOrchestraSettings;
		base.OnHitNPC(target, hit, damageDone);
		ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge, settings, Projectile.owner);

		target.AddBuff(BuffID.ShadowFlame, TimeUtils.SecondsToTicks(2));
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (!info.PvP)
		{
			return;
		}
		ParticleOrchestraSettings particleOrchestraSettings = default;
		particleOrchestraSettings.PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
		ParticleOrchestraSettings settings = particleOrchestraSettings;
		base.OnHitPlayer(target, info);
		ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge, settings, Projectile.owner);

		target.AddBuff(BuffID.ShadowFlame, TimeUtils.SecondsToTicks(2));
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = Projectile.width - 28;
		height = Projectile.height - 28;
		return true;
	}

	public override void DrawTrail(Texture2D projectileTexture, Vector2 drawPos, Vector2 drawOrigin, Color color, float scale, int loopIteration, SpriteEffects spriteEffects)
	{
		color = new Color(55, 33, 75, 0) * ((float)(Projectile.oldPos.Length - loopIteration) / (float)Projectile.oldPos.Length);
		drawPos += Vector2.Normalize(Projectile.velocity) * 8;
		scale = (Projectile.scale + 0.1f) - loopIteration / (float)Projectile.oldPos.Length;
		scale *= 1.4f;
		base.DrawTrail(projectileTexture, drawPos, drawOrigin, color, scale, loopIteration, spriteEffects);
	}
}
public class MoonfuryBlade : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 48;
		Projectile.height = 48;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.penetrate = 10;
		Projectile.alpha = 255;
		Projectile.friendly = true;
		Projectile.extraUpdates = 2;
		Projectile.timeLeft = 400;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 64) * Projectile.Opacity * ((float)Math.Sin(Projectile.ai[0] * 0.4f) * 0.25f + 0.75f);
	}
	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		Projectile.ai[0] += 0.2f;

		Projectile.scale = ((float)Math.Sin(Projectile.ai[0] * 0.4f) * 0.1f + 0.9f);

		Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha - 10, 0, 255);
		Projectile.velocity *= 0.99f;
		if (Projectile.timeLeft % 10 == 0)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.Shadowflame, Projectile.velocity, 128);
			d.fadeIn = 1.5f;
			d.noGravity = true;
			d.scale = 1.5f;
			Dust d2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.ShadowbeamStaff, Projectile.velocity, 128);
			d2.fadeIn = 1.5f;
			d2.noGravity = true;
			d2.scale = 1.5f;
		}
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 16;
		height = 16;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.ShadowFlame, TimeUtils.SecondsToTicks(2));
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.ShadowFlame, TimeUtils.SecondsToTicks(2));
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		for (int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.Shadowflame, Main.rand.NextVector2Circular(8, 8), 128);
			d.fadeIn = 1.5f;
			d.noGravity = true;
			d.scale = 1.5f;
			Dust d2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(22, 22), DustID.ShadowbeamStaff, Main.rand.NextVector2Circular(8, 8), 128);
			d2.fadeIn = 1.5f;
			d2.noGravity = true;
			d2.scale = 1.5f;
		}
	}
}
