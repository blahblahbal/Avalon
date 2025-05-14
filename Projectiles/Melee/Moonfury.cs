using Avalon.Common;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class Moonfury : FlailTemplate
{
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
		Projectile.width = 40; // The width of your projectile
		Projectile.height = 40; // The height of your projectile
		Projectile.localNPCHitCooldown = 40; // This facilitates custom hit cooldown logic
	}

	public override bool EmitDust(int dustType, int antecedent, int consequent, float fadeIn, bool noGravity, float scale, byte alpha)
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
