using Avalon.Items.Weapons.Magic.Tomes;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Tomes;

public class MagmafrostBoltProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<MagmafrostBolt>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<MagmafrostBolt>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 12;
		Projectile.aiStyle = -1;
		Projectile.tileCollide = true;
		Projectile.friendly = true;
		Projectile.alpha = 255;
		Projectile.light = 0.8f;
		Projectile.penetrate = 15;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.timeLeft = 2400 * 8;
		Projectile.ignoreWater = true;
		Projectile.extraUpdates = 8;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 10f)
		{
			Projectile.position += Projectile.velocity;
			Projectile.Kill();
		}
		else
		{
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
		}
		return false;
	}

	public override void AI()
	{
		if (Projectile.localAI[1] < 15f)
		{
			Projectile.localAI[1] += 1f;
		}
		Projectile.localAI[0]++;
		if (Projectile.localAI[1] >= 15f)
		{
			for (var loopCounter = 0; loopCounter < 2; loopCounter++)
			{
				int d = DustID.Torch;
				if (loopCounter == 0)
				{
					d = DustID.IceTorch;
				}
				Vector2 vel = Projectile.velocity / 1000;
				float x = vel.SafeNormalize(Vector2.Zero).RotatedBy(Projectile.localAI[0] * ((float)Math.PI / 50)).X;
				Vector2 value = vel.SafeNormalize(Vector2.Zero).RotatedBy(1.5707963705062866 * (loopCounter == 0 ? -1 : 1));
				var xVel = Projectile.velocity.X / 12f * loopCounter;
				var yVel = Projectile.velocity.Y / 12f * loopCounter;
				var posModifier = 4;
				var dust = Dust.NewDust(new Vector2(Projectile.position.X + posModifier, Projectile.position.Y + posModifier), Projectile.width - posModifier * 2, Projectile.height - posModifier * 2, d, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dust].noGravity = true;
				//Main.dust[dust].velocity *= -1f;
				//Main.dust[dust].velocity += Projectile.velocity * 0.1f;
				Main.dust[dust].position += (value * x * 6);
				Main.dust[dust].position -= new Vector2(xVel, yVel) * 2;
				Main.dust[dust].scale *= 1f;
			}
		}
		Projectile.rotation += 0.3f * Projectile.direction;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
			return;
		}
	}
}
