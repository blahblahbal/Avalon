using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Projectiles.Magic;

public class HallowedThorn : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 5;
	}
	public override void SetDefaults()
	{
		Projectile.width = 26;
		Projectile.height = 26;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.alpha = 255;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.light = 0.5f;
		Projectile.hide = true;
	}

	private readonly UnifiedRandom frameSeed = new();
	private void SetFrame()
	{
		UnifiedRandom currentFrameSeed = new(frameSeed.GetHashCode());

		if (Projectile.ai[1] > 2f)
		{
			if (currentFrameSeed.NextBool(10))
			{
				Projectile.frame = 0;
			}
			else
			{
				Projectile.frame = 5 - (int)((Projectile.ai[1] + 3) / 3);
			}
		}
		else if (Projectile.ai[1] == 2f)
		{
			Projectile.frame = 3;
		}
		else if (Projectile.ai[1] == 1f)
		{
			Projectile.frame = 4;
		}
	}

	public override void AI() // todo: have this draw with the end texture until it spawns the next segment
	{
		SetFrame();

		Lighting.AddLight(Projectile.position, 255 / 255f, 255 / 255f, 0);
		Vector2 oldCenter = Projectile.Center;
		Projectile.position -= Projectile.velocity;
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		if (Projectile.ai[0] == 0f)
		{
			Projectile.alpha -= 50;
			if (Projectile.alpha <= 0)
			{
				Projectile.alpha = 0;
				Projectile.ai[0] = 1f;
				if (Projectile.ai[1] == 0f)
				{
					Projectile.ai[1] += 1f;
					oldCenter += Projectile.velocity;
					Projectile.position += Projectile.velocity;
				}
				else
				{
					Projectile.hide = false;
				}

				if (Main.myPlayer == Projectile.owner)
				{
					int type = ModContent.ProjectileType<HallowedThorn>();
					if (Projectile.ai[1] >= 10f)
					{
						type = ModContent.ProjectileType<HallowedThornEnd>();
					}

					if ((int)Projectile.ai[1] % 3 == 0) // todo: if the ai[1] % 3 == 2, then do some custom drawing to connect the splits.
					{
						Vector2 newVel = AvalonGlobalProjectile.RotateAboutOrigin(Projectile.velocity, -MathHelper.ToRadians(22.5f) * Main.rand.NextFloat(0.33f, 1f));
						int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), oldCenter + newVel, newVel, type, Projectile.damage, Projectile.knockBack, Projectile.owner, ai1: Projectile.ai[1] + 1f);

						Vector2 newVel2 = AvalonGlobalProjectile.RotateAboutOrigin(Projectile.velocity, MathHelper.ToRadians(22.5f) * Main.rand.NextFloat(0.33f, 1f));
						int p2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), oldCenter + newVel2, newVel2, type, Projectile.damage, Projectile.knockBack, Projectile.owner, ai1: Projectile.ai[1] + 1f);
						return;
					}

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), oldCenter + Projectile.velocity, Projectile.velocity, type, Projectile.damage, Projectile.knockBack, Projectile.owner, ai1: Projectile.ai[1] + 1f);
				}
			}
		}
		else
		{
			Projectile.hide = false;
			if (Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
			{
				for (int i = 0; i < 3; i++)
				{
					if (Main.rand.NextBool((int)((Projectile.ai[1] + 3) / 3)))
					{
						Dust d = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 170, default, Main.rand.NextFloat(0.9f, 1.2f));
						d.velocity = Vector2.Lerp(d.velocity, Projectile.velocity * 0.025f, 0.667f);
					}
				}

				if (Main.rand.NextBool((int)((Projectile.ai[1] + 3) / 3)))
				{
					Dust d = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 170, default, Main.rand.NextFloat(0.8f, 1.1f));
					d.velocity = Vector2.Lerp(d.velocity, Vector2.Zero, 0.667f);
				}
			}

			Projectile.alpha += 5;
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
		}
	}
}
