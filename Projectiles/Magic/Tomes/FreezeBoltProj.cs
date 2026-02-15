using Avalon.Items.Weapons.Magic.Tomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Tomes;

public class FreezeBoltProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<FreezeBolt>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<FreezeBolt>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 12;
		Projectile.aiStyle = -1;
		Projectile.tileCollide = true;
		Projectile.friendly = true;
		Projectile.hide = true;
		Projectile.light = 0.9f;
		Projectile.penetrate = 12;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.timeLeft = 2400;
		Projectile.ignoreWater = true;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 7f)
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
		for (var num917 = 0; num917 < 5; num917++)
		{
			var num918 = Projectile.velocity.X / 3f * num917;
			var num919 = Projectile.velocity.Y / 3f * num917;
			var num920 = 4;
			var num921 = Dust.NewDust(new Vector2(Projectile.position.X + num920, Projectile.position.Y + num920), Projectile.width - num920 * 2, Projectile.height - num920 * 2, DustID.IceTorch, 0f, 0f, 100, default(Color), 1.2f);
			Main.dust[num921].noGravity = true;
			Main.dust[num921].velocity *= 0.1f;
			Main.dust[num921].velocity += Projectile.velocity * 0.1f;
			var dust105 = Main.dust[num921];
			dust105.position.X = dust105.position.X - num918;
			var dust106 = Main.dust[num921];
			dust106.position.Y = dust106.position.Y - num919;
		}
		if (Main.rand.NextBool(5))
		{
			var num922 = 4;
			var num923 = Dust.NewDust(new Vector2(Projectile.position.X + num922, Projectile.position.Y + num922), Projectile.width - num922 * 2, Projectile.height - num922 * 2, DustID.MagicMirror, 0f, 0f, 100, default(Color), 0.6f);
			Main.dust[num923].velocity *= 0.25f;
			Main.dust[num923].velocity += Projectile.velocity * 0.5f;
		}
		if (Projectile.ai[1] >= 20f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
		}
		Projectile.rotation += 0.3f * Projectile.direction;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
			return;
		}
	}
}
