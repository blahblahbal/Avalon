using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Boomerangs;

public class VirulentCloud : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(44, 42);
		Projectile.scale = 1.15f;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.ignoreWater = true;
		Projectile.timeLeft = 9000;
		Projectile.alpha = 150;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		Projectile.tileCollide = false;
	}
	public float Timer;
	public override void AI()
	{
		Projectile.spriteDirection = Projectile.direction;
		Projectile.rotation += (Projectile.velocity.X + Projectile.velocity.Y) * 0.1f * Projectile.spriteDirection;
		Projectile.velocity *= 0.980f;
		if (!Main.rand.NextBool(3))
		{
			Projectile.alpha++;
		}
		if (Projectile.alpha >= 255)
		{
			Projectile.Kill();
		}
		Timer++;
		if (Timer <= 40)
		{
			Projectile.scale *= 1.007f;
		}
		if (Timer >= 40)
		{
			Projectile.scale *= 0.993f;
		}
		if (Timer == 80)
		{
			Timer = 0;
			Projectile.scale = 1.15f;
		}

		foreach (NPC n in Main.npc)
		{
			if (n.active && n.life > 0 && n.lifeMax > 5 && !n.dontTakeDamage && !n.townNPC)
			{
				if (n.getRect().Intersects(Projectile.getRect()))
				{
					n.AddBuff(ModContent.BuffType<Buffs.Debuffs.Pathogen>(), 60 * 8);
				}
			}
		}
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
}
public class VirulentCloudSmall : VirulentCloud;

