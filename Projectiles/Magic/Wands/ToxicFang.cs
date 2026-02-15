using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Wands;
public class ToxicFang : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 16;
		Projectile.aiStyle = -1;
		Projectile.tileCollide = true;
		Projectile.friendly = true;
		Projectile.timeLeft = 90;
		Projectile.alpha = 100;
		Projectile.penetrate = 4;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 100);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Toxic>(), 60 * 4);
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		return true;
	}
	public override void AI()
	{
		Lighting.AddLight(Projectile.position, 150 / 255f, 0, 100 / 255f);
		Projectile.rotation = Projectile.velocity.ToRotation() + 90 / 57.2957795f;
		for (int num26 = 0; num26 < 2; num26++)
		{
			float x2 = Projectile.position.X - Projectile.velocity.X / 10f * num26;
			float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * num26;
			int num27 = Dust.NewDust(new Vector2(x2, y2), Projectile.width, Projectile.height, ModContent.DustType<Dusts.ToxinDust>(), 0f, 0f, 0, default, 1f);
			Main.dust[num27].alpha = Projectile.alpha;
			Main.dust[num27].velocity *= 0f;
			Main.dust[num27].noGravity = true;
		}
		if (Projectile.ai[1] >= 20f)
		{
			Projectile.velocity.Y += 0.2f;
		}
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
			return;
		}
	}
}
