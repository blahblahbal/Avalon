using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class ColdCatcherBobber : ModProjectile
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.BobberWooden);
		DrawOriginOffsetY = -8; // Adjusts the draw position
	}
}
