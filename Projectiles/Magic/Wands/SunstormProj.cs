using Avalon.Items.Weapons.Magic.Wands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Wands;

public class SunstormProj : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<Sunstorm>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.width = 14;
		Projectile.height = 1200;
		//Projectile.alpha = 50;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.friendly = true;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.light = 0.6f;
		Projectile.tileCollide = false;
	}

	public override void AI()
	{
		Projectile.ai[0]++;
		if (Projectile.ai[0] < 100)
		{
			if (Projectile.ai[1] == 0)
			{
				Projectile.rotation -= 0.002f;
				Projectile.velocity.X += 0.5f;
			}
			else if (Projectile.ai[1] == 1)
			{
				Projectile.rotation += 0.002f;
				Projectile.velocity.X -= 0.5f;
			}

		}
		else
		{
			Projectile.rotation = 0;
			Projectile.ai[0] = 0;
			Projectile.Kill();
		}
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 50);
	}
}
