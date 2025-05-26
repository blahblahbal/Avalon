using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class AncientSandstorm : ModProjectile
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
		Projectile.alpha = 128;
		Projectile.friendly = true;
		Projectile.timeLeft = 720;
		Projectile.ignoreWater = true;
		Projectile.hostile = false;
		Projectile.scale = 0.4f;
		Projectile.extraUpdates = 1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
		//Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
	}

	public bool CanSpawnChild { get => Convert.ToBoolean(Projectile.ai[0]); set => Projectile.ai[0] = value.ToInt(); }

	public override void AI()
	{
		Projectile.ai[1]++;
		if (Projectile.ai[2] > 1)
		{
			Projectile.alpha += 1;
			if (Projectile.ai[1] % 10 == 0)
			{
				Projectile.damage--;
			}
		}
		else
			Projectile.alpha -= 3;

		if (Projectile.alpha <= 100)
		{
			Projectile.ai[2]++;
		}

		if (Projectile.alpha == 255) Projectile.Kill();

		Projectile.velocity = Projectile.velocity.RotatedByRandom(CanSpawnChild ? 0.025f : 0.05f) * 0.985f;
		Projectile.rotation += MathHelper.Clamp(Projectile.velocity.Length() * 0.03f, -0.3f, 0.3f);
		Projectile.scale += Projectile.ai[0] == 0f ? 0.02f : 0.01f;
		Projectile.Resize((int)(32 * Projectile.scale), (int)(32 * Projectile.scale));

		int rand = (int)(250 / Utils.Remap(Projectile.velocity.Length(), 0f, 10f, 1.5f, 50f));
		if (Main.rand.NextBool(rand + ((255 - Projectile.alpha) / 15)))
		{
			float dustMagnitude = 3f;
			float dustX = Main.rand.NextFloat(-dustMagnitude, dustMagnitude);
			float dustY = Main.rand.NextFloat(-dustMagnitude, dustMagnitude);
			int dustAlpha = Math.Clamp((int)(Projectile.alpha * 1.4f), 0, 230);
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AncientSandDust>(), dustX, dustY, dustAlpha, default, 0.5f);
			dust.velocity *= 0.9f;
			dust.fadeIn = 2f;
			dust.noGravity = true;
		}
		if (Projectile.alpha < 150 && Projectile.alpha % 32 == 0 && CanSpawnChild)
		{
			Vector2 vel = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.TwoPi) * 3f * MathF.Pow(Projectile.alpha / 255f, 1.5f);
			vel += Projectile.velocity * 0.2f;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner);
		}
	}
	public override bool? CanHitNPC(NPC target)
	{
		return (Projectile.alpha < 220 || Projectile.ai[2] < 1) && !target.friendly;
	}

	public override bool CanHitPvp(Player target)
	{
		return Projectile.alpha < 220;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		ClassExtensions.DrawGas(Texture, lightColor * 0.8f, Projectile, 4, 6);
		return false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity = Projectile.oldVelocity * 0.7f;
		return false;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
}
