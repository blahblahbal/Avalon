using Terraria;
using Terraria.ID;

namespace Avalon.ModSupport.MLL.Projectiles;
public abstract class LiquidGrenadeProjBase : LiquidSpawningExplosionBase
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.IsARocketThatDealsDoubleDamageToPrimaryEnemy[Type] = true;
		ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true;

		ProjectileID.Sets.Explosive[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(800);
		AIType = 800;
		Projectile.timeLeft = 180;
	}

	public override void OnKill(int timeLeft)
	{
		LiquidExplosiveKill(Projectile);
	}
}
