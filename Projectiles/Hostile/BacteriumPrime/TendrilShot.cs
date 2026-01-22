using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.BacteriumPrime;

public class TendrilShot : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = -1;
		Projectile.Size = new Vector2(16);
		Projectile.tileCollide = false;
		Projectile.hide = true;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1f, 1f, 1f, 0.5f);
	}
	public override void AI()
	{
		Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<SimpleColorableGlowyDust>(), Projectile.velocity * 0.2f + Main.rand.NextVector2Circular(1, 1));
		d.scale = 1.5f;
		d.noGravity = true;
		d.color = new Color(0.55f, 0.5f, 0.2f, 0.3f);

		Projectile.ai[1]++;

		if(Collision.SolidCollision(Projectile.position,Projectile.width,Projectile.height) || Main.tile[Projectile.Center.ToTileCoordinates()].WallType != 0)
		{
			Projectile.ai[1]++;
		}

		if (Projectile.ai[1] == 1)
		{
			SoundEngine.PlaySound(SoundID.Item112, Projectile.position);
		}
		Projectile.velocity = Utils.rotateTowards(Projectile.Center, Projectile.velocity, Main.player[(int)Projectile.ai[0]].Center, 0.06f);

		if (Projectile.ai[1] > 600)
		{
			Projectile.Kill();
		}
		else if (Projectile.ai[1] > 520)
		{
			Projectile.velocity *= 0.98f;
		}
	}
	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<SimpleColorableGlowyDust>(), Main.rand.NextVector2Circular(5, 5));
			d.scale = 1.5f;
			d.noGravity = true;
			d.color = new Color(0.55f, 0.5f, 0.2f, 0.3f);
		}
	}
}
