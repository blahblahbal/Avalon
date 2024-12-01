using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Projectiles.Enemy;

namespace Avalon.Compatability.Thorium.Projectiles.Hostile;

public class PathogenSickle : ModProjectile
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("ThoriumMod");
	}
	public bool Spawned { get => Projectile.ai[0] == 1f; set => Projectile.ai[0] = (value ? 1f : 0f); }
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Type] = 6;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.width = 48;
		Projectile.height = 48;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 300;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 0) * 0.75f;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = Projectile.width / 2;
		height = Projectile.height / 2;
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Pathogen>(), 300, true, false);
	}

	public override void AI()
	{
		if (!Spawned)
		{
			Spawned = true;
			SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);
		}
		Projectile.rotation += Projectile.direction * 0.8f;
		Projectile.ai[1] += 1f;
		if (Projectile.ai[1] >= 30f)
		{
			if (Projectile.ai[1] < 100f)
			{
				Projectile.velocity *= 1.06f;
			}
			else
			{
				Projectile.ai[1] = 200f;
			}
		}
		for (int i = 0; i < 2; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PathogenDust>(), 0f, 0f, 0, default, 1f);
			Main.dust[d].noGravity = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 30; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PathogenDust>(), Projectile.velocity.X, Projectile.velocity.Y, 0, default, 1f);
			Main.dust[d].noGravity = true;
			Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PathogenDust>(), Projectile.velocity.X, Projectile.velocity.Y, 0, default, 1f);
		}
	}
}
