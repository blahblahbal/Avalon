using Avalon;
using Avalon.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Ammo;

public class PathogenDartTrail : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NoLiquidDistortion[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.width = 36;
		Projectile.height = 36;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.alpha = 254;
		Projectile.friendly = true;
		Projectile.timeLeft = 720;
		Projectile.ignoreWater = true;
		Projectile.hostile = false;
		Projectile.scale = 0.5f;
		Projectile.extraUpdates = 1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		//Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
	}

	public override void AI()
	{
		Projectile.ai[1]++;
		if (Projectile.ai[2] > 1)
		{
			if (Projectile.ai[1] % 3 == 0)
			{
				Projectile.alpha += 1;
			}
			if (Projectile.ai[1] % 20 == 0)
			{
				Projectile.damage--;
			}
		}
		else
			Projectile.alpha -= 3;

		if (Projectile.alpha <= 100)
			Projectile.ai[2]++;

		if (Projectile.alpha == 255) Projectile.Kill();

		Projectile.velocity = Projectile.velocity.RotatedByRandom(0.1f) * 0.985f;
		Projectile.rotation += MathHelper.Clamp(Projectile.velocity.Length() * 0.1f, -0.3f, 0.3f);
		Projectile.scale *= 1.003f;
		Projectile.Resize((int)(40 * Projectile.scale), (int)(40 * Projectile.scale));

		//if (Main.rand.NextBool(3))
		//{
		//    int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Venom, 0, 0, (int)(Projectile.alpha * 1.4f), default, 0.5f);
		//    Main.dust[d].velocity *= 0.5f;
		//    Main.dust[d].fadeIn = 2f;
		//    Main.dust[d].noGravity = true;
		//}
	}
	public override bool? CanHitNPC(NPC target)
	{
		return Projectile.alpha < 220 && !target.friendly;
	}

	public override bool CanHitPvp(Player target)
	{
		return Projectile.alpha < 220;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Pathogen>(), 5 * 60);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Pathogen>(), 5 * 60);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		ClassExtensions.DrawGas(TextureAssets.Projectile[Type].Value, lightColor * 0.8f, Projectile, 3, 8);
		return false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity = oldVelocity * Main.rand.NextFloat(-0.2f, 0.2f);
		Projectile.tileCollide = false;
		return false;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
}
