using Avalon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Ammo;

public class PhantasmalBullet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = 6;
		Projectile.height = 6;
		Projectile.aiStyle = 1;
		Projectile.friendly = true;
		AIType = ProjectileID.CursedBullet;
		Projectile.penetrate = 2;
		Projectile.scale = 1.2f;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 1200;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 60;
		Projectile.MaxUpdates = 2;
	}
	public override bool PreAI()
	{
		Lighting.AddLight(Projectile.position, 75 / 255f, 15 / 255f, 35 / 255f);
		return true;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		for (int i = 0; i < 15; i++)
		{
			int d = Dust.NewDust(Projectile.position, 8, 8, DustID.TheDestroyer);
			Main.dust[d].noGravity = true;
			Main.dust[d].velocity *= 1.5f;
			Main.dust[d].scale *= 0.7f;
		}
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		for (int i = 0; i < 15; i++)
		{
			int d = Dust.NewDust(Projectile.position, 8, 8, DustID.TheDestroyer);
			Main.dust[d].noGravity = true;
			Main.dust[d].velocity *= 1.5f;
			Main.dust[d].scale *= 0.7f;
		}
	}
	public override void OnKill(int timeLeft)
	{
		if (Projectile.penetrate == 1)
		{
			Projectile.maxPenetrate = -1;
			Projectile.penetrate = -1;

			int explosionArea = 60;
			Vector2 oldSize = Projectile.Size;
			Projectile.position = Projectile.Center;
			Projectile.Size += new Vector2(explosionArea);
			Projectile.Center = Projectile.position;

			Projectile.tileCollide = false;
			Projectile.velocity *= 0.01f;
			//Projectile.Damage();
			Projectile.scale = 0.01f;

			Projectile.position = Projectile.Center;
			Projectile.Size = new Vector2(10);
			Projectile.Center = Projectile.position;
		}

		SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/NPC_Killed_6") with { Volume = 0.5f, Pitch = -0.3f, PitchVariance = 0.2f }, Projectile.position);
		for (int i = 0; i < 6; i++)
		{
			Dust dust = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.TheDestroyer, 0, 0, 100, Color.Black, 0.8f);
			dust.noGravity = true;
			dust.velocity *= 1.5f;
			Dust dust2 = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.TheDestroyer, 0f, 0f, 100, Color.Black, 0.5f);
			dust2.noGravity = Main.rand.NextBool(3);
		}
		for (int i = 0; i < 6; i++)
		{
			int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TheDestroyer, 0f, 0f, 100, default(Color), 0.7f);
			Main.dust[dustIndex].noGravity = true;
			Main.dust[dustIndex].velocity *= 1.5f;
			Main.dust[dustIndex].fadeIn = Main.rand.NextFloat(0, 1.5f);
			int dustIndex2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TheDestroyer, 0f, 0f, 100, default(Color), 0.8f);
			Main.dust[dustIndex2].velocity *= 1.5f;
		}

		Projectile.position.X += Projectile.width / 2;
		Projectile.position.Y += Projectile.height / 2;
		Projectile.width = 80;
		Projectile.height = 80;
		Projectile.position.X -= Projectile.width / 2;
		Projectile.position.Y -= Projectile.height / 2;
		Projectile.active = false;
	}
	public bool CurveDirectionStart = true;
	public bool CurveDirection;
	public int maxSpeed = 15;
	public override void AI()
	{
		int num = 0;
		Projectile.localAI[num]++;
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		float x = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(Projectile.localAI[num] * ((float)Math.PI / Main.rand.Next(10, 15))).X;
		Vector2 value = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707963705062866);
		Projectile.position += value * x * Main.rand.NextFloat(1f, 1.5f);
		//Projectile.position += value * x * Main.rand.NextFloat(5f, 10.5f); //exaggerated sine wave to test trail
	}
	public override bool PreDraw(ref Color lightColor) // theft v3? (from enchanted SpectralBullet)
	{
		Rectangle dims = this.GetDims();
		Vector2 drawOrigin = new Vector2(Projectile.width, Projectile.height);
		for (int k = 0; k < Projectile.oldPos.Length; k++)
		{
			Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
			Color color = new Color(180, 180, 180, 50) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, new Rectangle(0, dims.Height * Projectile.frame, dims.Width, dims.Height), color, Projectile.rotation, drawOrigin, new Vector2(0.9f * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length), 0.9f) * Projectile.scale, SpriteEffects.None, 0);
		}
		return false;
	}
}
