using Avalon.Common;
using Terraria.GameContent.Skies;

namespace Avalon.Hooks;
public class AmbientSkyObjects : ModHook
{
	protected override void Apply()
	{
		On_AmbientSky.AnActiveSkyConflictsWithAmbience += On_AmbientSky_AnActiveSkyConflictsWithAmbience; ;
	}

	private bool On_AmbientSky_AnActiveSkyConflictsWithAmbience(On_AmbientSky.orig_AnActiveSkyConflictsWithAmbience orig, AmbientSky self)
	{
		if (DarkMatterWorld.InArea)
		{
			return true;
		}
		return orig(self);
	}
}