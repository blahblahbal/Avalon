using Avalon.Common.Interfaces;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class SanguineKatanaSlash : EnergySlashTemplate, ISyncedOnHitEffect
{
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TheHorsemansBlade}";
	public override LocalizedText DisplayName => ModContent.GetInstance<SanguineKatana>().DisplayName;
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
	}
	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		Vector2 vector = Main.rand.NextVector2FromRectangle(target.Hitbox) - new Vector2(0, 24);
		Vector2 vel = new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), 1);
		ParticleSystem.NewParticle(new SanguineCuts(vel), vector);
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
