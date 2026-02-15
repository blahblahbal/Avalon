using Avalon.Items.Weapons.Magic.Other;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Other;

public class MagicGrenadeBoom : ModProjectile
{
	public override string Texture => ModContent.GetInstance<MagicGrenade>().Texture;
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 128;
		Projectile.alpha = 255;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.friendly = true;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 1;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
		Projectile.position.X = Projectile.position.X + Projectile.width / 2;
		Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
		Projectile.width = 22;
		Projectile.height = 22;
		Projectile.position.X = Projectile.position.X - Projectile.width / 2;
		Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;

		for (int num369 = 0; num369 < 20; num369++)
		{
			int num370 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
			Main.dust[num370].velocity *= 1.4f;
		}
		for (int num371 = 0; num371 < 10; num371++)
		{
			int num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.HallowedWeapons, 0f, 0f, 100, default, 2.5f);
			Main.dust[num372].noGravity = true;
			Main.dust[num372].velocity *= 5f;
			num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.HallowedWeapons, 0f, 0f, 100, default, 1.5f);
			Main.dust[num372].velocity *= 3f;
		}
		int num373 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Main.gore[num373].velocity.X++;
		Main.gore[num373].velocity.Y++;
		num373 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Main.gore[num373].velocity.X--;
		Main.gore[num373].velocity.Y++;
		num373 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Main.gore[num373].velocity.X++;
		Main.gore[num373].velocity.Y--;
		num373 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Main.gore[num373].velocity.X--;
		Main.gore[num373].velocity.Y--;
	}
}

