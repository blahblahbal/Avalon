using Avalon.Items.Weapons.Melee.Swords;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class SnotsabreShot : ModProjectile
{
	public override string Texture => ModContent.GetInstance<Snotsabre>().Texture;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(8);
		Projectile.aiStyle = -1;
		Projectile.tileCollide = true;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.timeLeft = 25;
		Projectile.penetrate = 3;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.ignoreWater = true;
		Projectile.extraUpdates = 1;
		Projectile.hide = true;
	}
	public override bool? CanHitNPC(NPC target)
	{
		if (target.whoAmI == Projectile.ai[0])
			return false;
		return base.CanHitNPC(target);
	}
	public override void AI()
	{
		Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Poisoned, Vector2.Zero, 128);
		d.noGravity = true;
		d.scale = 1.5f;
		Projectile.velocity.Y += 0.2f;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.Poisoned, 60 * 2);
	}
}
