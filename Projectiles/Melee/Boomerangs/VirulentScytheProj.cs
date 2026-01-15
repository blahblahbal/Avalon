using Avalon.Items.Weapons.Melee.Boomerangs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Boomerangs;
public class VirulentScytheProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<VirulentScythe>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<VirulentScythe>().DisplayName;

	public float seconds = 0.5f;
	public float returnSpeed = 2.5f;

	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(32);
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.ignoreWater = true;
		DrawOffsetX = -8;
		DrawOriginOffsetY = -8;
		Projectile.timeLeft = 2400;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 10;
	}

	public float timer;
	public float time;
	public bool willReturn;
	public bool runOnce;
	public float nextCloud;
	public override void AI()
	{
		Vector2 startPosition = Projectile.Center;
		Vector2 target = Main.player[Projectile.owner].Center;

		Projectile.spriteDirection = Projectile.direction;
		Projectile.rotation += 0.5f * Projectile.spriteDirection;

		timer++;
		if (willReturn)
		{
			Projectile.Center = Vector2.SmoothStep(startPosition, target, time);

			time += (returnSpeed / 100);
			time = MathHelper.Clamp(time, 0f, 1f);
			Projectile.velocity *= 0f;

			if (Main.player[Projectile.owner].getRect().Intersects(Projectile.getRect()))
			{
				Projectile.Kill();
			}
		}
		if (timer == (60f * seconds))
		{
			willReturn = true;
		}
		int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ContagionDust>(), Projectile.velocity.X * 0, Projectile.velocity.Y * 0, default, default, 1f);
		Main.dust[dust1].noGravity = true;
		Main.dust[dust1].alpha = 128;

		nextCloud++;
		int randomCloud = Main.rand.Next([ModContent.ProjectileType<VirulentCloud>(), ModContent.ProjectileType<VirulentCloudSmall>()]);

		if (nextCloud > 5 + Main.rand.Next(25))
		{
			Vector2 randomDir = new Vector2(Main.rand.NextFloat(-100f, 100f), Main.rand.NextFloat(-100f, 100f));
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(randomDir) * 1.5f, randomCloud, 0, 0, Main.player[Projectile.owner].whoAmI);
			nextCloud = 0;
		}
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = -oldVelocity.X;
		}
		if (Projectile.velocity.Y != oldVelocity.Y)
		{
			Projectile.velocity.Y = -oldVelocity.Y;
		}
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		return false;
	}
}
