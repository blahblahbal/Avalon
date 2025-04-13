using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Buffs.Debuffs;

namespace Avalon.Projectiles.Ranged;

public class ShroomiteRocket : ModProjectile
{
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = dims.Width * 4 / 20;
		Projectile.height = dims.Height * 4 / 20 / Main.projFrames[Projectile.type];
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.alpha = 0;
		Projectile.scale = 1.2f;
		Projectile.timeLeft = 1200;
		Projectile.tileCollide = true;
		Projectile.DamageType = DamageClass.Ranged;
	}
	public override void OnKill(int timeLeft)
	{
		Projectile.maxPenetrate = -1;
		Projectile.penetrate = -1;

		int explosionArea = 75;
		Vector2 oldSize = Projectile.Size;
		Projectile.position = Projectile.Center;
		Projectile.Size += new Vector2(explosionArea);
		Projectile.Center = Projectile.position;

		Projectile.tileCollide = false;
		Projectile.velocity *= 0.01f;
		Projectile.Damage();
		Projectile.scale = 0.01f;

		Projectile.position = Projectile.Center;
		Projectile.Size = new Vector2(10);
		Projectile.Center = Projectile.position;

		SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

		for (int i = 0; i < 20; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (Projectile.ai[2] == 0) ? DustID.Torch : DustID.DesertTorch, 0, 0, 0, default, 2f);
			Main.dust[d].velocity = Main.rand.NextVector2Circular(6, 6);
			Main.dust[d].noGravity = true;
			Main.dust[d].fadeIn = 2.3f;
		}
		for (int i = 0; i < 20; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 0, default, 1.4f);
			Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-3, 0).RotatedBy(Projectile.velocity.ToRotation());
			Main.dust[d].noGravity = !Main.rand.NextBool(10);
		}
		for (int i = 0; i < 7; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0, 0, 0, default, 1.4f);
			//Main.dust[d].color = Color.Red;
			Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-5, 0).RotatedBy(Projectile.velocity.ToRotation());
			Main.dust[d].noGravity = Main.rand.NextBool(3);
		}
		for (int i = 0; i < 9; i++)
		{
			int g = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(10, 6) + new Vector2(-1, 0).RotatedBy(Projectile.velocity.ToRotation()), Main.rand.Next(61, 63), 0.8f);
			Main.gore[g].alpha = 128;
		}
		Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
		Projectile.width = 10;
		Projectile.height = 10;
		Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ShroomiteFullbright>(), 60 * 10);
	}
	public override void AI()
	{
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}
}
