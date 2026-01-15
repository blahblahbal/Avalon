using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class AeonExplosion : ModProjectile
{
	public override string Texture => ModContent.GetInstance<AeonStar>().Texture;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(128);
		Projectile.aiStyle = -1;
		Projectile.alpha = 0;
		Projectile.penetrate = -1;
		Projectile.scale = 1f;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 21;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 1;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = target.Center.X <= Projectile.Center.X ? -1 : 1;
	}
}