using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.WallOfSteel;

internal class WoSMortar : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bomb;

	public override void SetDefaults()
	{
		Projectile.width = 24;
		Projectile.height = 24;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.penetrate = -1;
		Projectile.aiStyle = 16;
		AIType = ProjectileID.Bomb;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = true;
		Projectile.timeLeft = 50;
		Projectile.alpha = 0;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Explode();
		Projectile.Kill();
		return true;
	}
	public void Explode()
	{
		SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);

		for (int i = 0; i < 2; i++)
		{
			int randomSize = Main.rand.Next(1, 4) / 2;
			int num161 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64));
			Gore gore30 = Main.gore[num161];
			Gore gore40 = gore30;
			gore40.velocity *= 0.3f;
			gore40.scale *= randomSize;
			Main.gore[num161].velocity.X += Main.rand.Next(-1, 2);
			Main.gore[num161].velocity.Y += Main.rand.Next(-1, 2);
		}
		int bomb = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.Grenade, 50, 3f);
		Main.projectile[bomb].timeLeft = 1;
		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, -5f), ModContent.ProjectileType<WoSGeyser>(), Projectile.damage / 3, 1f);
		for (int i = 0; i < 9; i++)
		{
			int rand = Main.rand.Next(-10, 11);
			Vector2 velocity = new Vector2(0, -5f).RotatedBy(MathHelper.ToRadians(rand));
			int cinder = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<WoSCinder>(), Projectile.damage / 4, 0.5f);

		}
	}
}
