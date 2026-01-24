using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Ammo;

public class CanisterFireLinger : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames;
	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.alpha = 255;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 250 + Main.rand.Next(50, 100);
		Projectile.ignoreWater = false;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.usesIDStaticNPCImmunity = true;
		Projectile.idStaticNPCHitCooldown = 10;
	}
	public override void AI()
	{
		for (int i = 0; i < 1; i++)
		{
			int num421 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 3f);
			Main.dust[num421].velocity.X *= 3f;
			Main.dust[num421].velocity.Y *= 3.5f;
			Main.dust[num421].noGravity = true;
		}
	}
	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int size = 15;
		hitbox.X -= size;
		hitbox.Y -= size;
		hitbox.Width += size * 2;
		hitbox.Height += size * 2;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 240);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.OnFire, 240, false);
	}
}
