using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;

namespace Avalon.Projectiles.Ranged.Longbows;

public class LongbowHeld : LongbowTemplate
{
	public override Vector2 NotificationFlashOffset => new Vector2(8, 0);
	public override bool ArrowEffect(Projectile projectile, float Power, byte variant = 0)
	{
		if (Power > 0.8f)
		{
			projectile.extraUpdates++;
			return true;
		}
		return false;
	}
}
