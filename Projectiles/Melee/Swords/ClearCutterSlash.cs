using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Particles.OldParticleSystem;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class ClearCutterSlash : EnergySlashTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<ClearCutter>().DisplayName;
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TheHorsemansBlade}";
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.penetrate = 3;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Color[] Colors = { Color.LightSkyBlue, Color.Magenta, Color.White, Color.Magenta };
		Color Color1 = ClassExtensions.CycleThroughColors(Colors, 60) * 0.3f;
		Color1.A = 64;
		DrawSlash(Color1, Color1 * 0.8f, Color1 * 0.6f, Color.Lerp(Color1, Color.White, 0.5f), 0, 1.1f, 0, -0.2f, -0.3f, true);
		DrawSlash(Color1, Color1 * 0.3f, Color.Transparent, Color.Transparent, 0, 0.9f, 0, -0.2f, -0.3f, false);

		return false;
	}
	public override void AI()
	{
		float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
		Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
		Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
		int[] Dusts = { DustID.IceTorch, DustID.HallowedTorch, DustID.WhiteTorch };
		if (Main.rand.NextFloat() * 0.5f < Projectile.Opacity)
		{
			Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), Dusts[Main.rand.Next(3)], vector3 * 3f, 0, default, 1f);
			dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.5f;
			dust2.noGravity = true;
		}
		Projectile.ai[2] += 0.02f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ParticleOrchestraSettings settings = new ParticleOrchestraSettings();
		settings.PositionInWorld = target.Hitbox.ClosestPointInRect(Main.player[Projectile.owner].Center);
		Vector2 positionslansdglag = Main.rand.NextVector2FromRectangle(target.Hitbox);
		OldParticleSystemDeleteSoon.AddParticle(new CrystalSparkle(), positionslansdglag, Vector2.Zero, default);
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			Network.SyncParticles.SendPacket(ParticleType.CrystalSparkle, positionslansdglag, Vector2.Zero, default, Projectile.owner);
		}
		for (int i = 0; i < Main.rand.Next(1, 3); i++)
		{
			settings.MovementVector = Vector2.Zero;
			float rand = Main.rand.NextFloat(-0.5f, 0.5f);
			int Dist = Main.rand.Next(-1000, -600);
			if (Main.myPlayer == Projectile.owner)
			{
				settings.PositionInWorld = Main.MouseWorld + new Vector2(0, Dist).RotatedBy(rand);
				//ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.Excalibur, settings);
				OldParticleSystemDeleteSoon.AddParticle(new CrystalSparkle(), settings.PositionInWorld, Vector2.Zero, default, 1);
				Projectile P = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), settings.PositionInWorld, new Vector2(0, 30).RotatedBy(rand), ProjectileID.StarCloakStar, (int)(Projectile.damage * 0.4f), Projectile.knockBack, Projectile.owner, 0, target.position.Y);
			}
		}
	}
}

