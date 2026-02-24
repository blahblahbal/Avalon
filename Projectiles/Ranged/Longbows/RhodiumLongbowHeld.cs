using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace Avalon.Projectiles.Ranged.Longbows;
public class RhodiumLongbowHeld : LongbowTemplate
{
	public override void SetDefaults()
	{
		base.SetDefaults();
		DrawOffsetX = -16;
		DrawOriginOffsetY = -25;
	}
	public override bool ArrowEffect(Projectile projectile, float Power, byte variant = 0)
	{
		if (Power == 1)
		{
			SoundEngine.PlaySound(SoundID.Item110);
			if (projectile.penetrate > 0)
				projectile.penetrate += 2;
			projectile.ai[0] = -100;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
			projectile.extraUpdates++;

			for (int i = 0; i < 20; i++)
			{
				Dust d = Dust.NewDustDirect(projectile.Center, 0, 0, DustID.Snow);
				d.noGravity = true;
				d.velocity = projectile.velocity.RotatedByRandom(0.1f) * i * 0.1f;
				d.alpha = 25;
				d.color = new Color(200, 200, 200);
			}
			return true;
		}
		return false;
	}
}
public class OsmiumLongbowHeld : RhodiumLongbowHeld { }
public class IridiumLongbowHeld : RhodiumLongbowHeld { }
