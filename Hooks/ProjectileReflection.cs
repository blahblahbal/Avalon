using Avalon.Data.Sets;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class ProjectileReflection : ILoadable
{
	public void Load(Mod mod)
	{
		On_Projectile.CanBeReflected += On_Projectile_CanBeReflected;
	}
	private bool On_Projectile_CanBeReflected(On_Projectile.orig_CanBeReflected orig, Projectile self)
	{
		if (ProjectileSets.CanBeReflected[self.type])
			return true;
		return orig(self);
	}
	public void Unload()
	{
	}
}
