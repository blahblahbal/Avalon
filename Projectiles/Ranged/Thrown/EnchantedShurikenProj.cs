using Avalon.Items.Weapons.Ranged.Thrown;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Thrown;

public class EnchantedShurikenProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<EnchantedShuriken>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<EnchantedShuriken>().DisplayName;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 3;
		Projectile.DamageType = DamageClass.Ranged;
	}
	public override void AI()
	{
		Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 20f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
			Projectile.velocity.X = Projectile.velocity.X * 0.99f;
		}
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		OnHitAnything();
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitAnything();
	}
	void OnHitAnything()
	{
		ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
		particleOrchestraSettings.PositionInWorld = Projectile.Center;
		particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(3, 3);
		ParticleOrchestraSettings settings = particleOrchestraSettings;
		ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);
		particleOrchestraSettings.PositionInWorld = Projectile.Center;
		particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(3, 3);
		settings = particleOrchestraSettings;
		ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, Projectile.owner);
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		for (int i = 0; i < 2; i++)
		{
			ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
			particleOrchestraSettings.PositionInWorld = Projectile.Center;
			particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(3, 3);
			ParticleOrchestraSettings settings = particleOrchestraSettings;
			ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);
			particleOrchestraSettings.PositionInWorld = Projectile.Center;
			particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(3, 3);
			settings = particleOrchestraSettings;
			ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, Projectile.owner);
		}
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 200);
	}
	public override bool PreDraw(ref Color lightColor) // theft
	{
		Vector2 drawOrigin = new Vector2(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
		for (int k = 0; k < Projectile.oldPos.Length; k++)
		{
			Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
			Color color = new Color(0, 128, 255, 0) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
			Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value, drawPos, new Rectangle(0, Projectile.height * Projectile.frame, Projectile.width, Projectile.height), color, Projectile.rotation, drawOrigin, Projectile.scale * 0.9f, SpriteEffects.None, 0);
		}
		return true;
	}
}
