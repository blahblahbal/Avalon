using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Projectiles.Magic;

public class HallowedThornEnd : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 3;
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
		Projectile.light = 0.1f;
	}

	private readonly UnifiedRandom frameSeed = new();
	private void SetFrame()
	{
		Projectile.frame = new UnifiedRandom(frameSeed.GetHashCode()).Next(Main.projFrames[Type]);
	}

	public override void AI()
	{
		SetFrame();

		Lighting.AddLight(Projectile.position, 100 / 255f, 100 / 255f, 0);
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
					Projectile.position += Projectile.velocity * 1f;
				}
			}
		}
		else
		{
			if (Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
			{
				for (int i = 0; i < 3; i++)
				{
					if (Main.rand.NextBool())
					{
						Dust d = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 170, default, Main.rand.NextFloat(1.1f, 1.2f));
						d.velocity = Vector2.Lerp(d.velocity, Projectile.velocity * 0.025f, 0.25f);
					}
				}
				if (Main.rand.NextBool())
				{
					Dust d = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 170, default, Main.rand.NextFloat(1f, 1.1f));
					d.velocity = Vector2.Lerp(d.velocity, Vector2.Zero, 0.25f);
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
