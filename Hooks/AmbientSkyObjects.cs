using Avalon.Common;
using Avalon.Systems;
using Terraria.GameContent.Skies;
using Terraria.ModLoader;

namespace Avalon.Hooks;
public class AmbientSkyObjects : ModHook
{
	protected override void Apply()
	{
		On_AmbientSky.AnActiveSkyConflictsWithAmbience += On_AmbientSky_AnActiveSkyConflictsWithAmbience; ;
	}

	private bool On_AmbientSky_AnActiveSkyConflictsWithAmbience(On_AmbientSky.orig_AnActiveSkyConflictsWithAmbience orig, AmbientSky self)
	{
		if (ModContent.GetInstance<BiomeTileCounts>().DarkMatterMonolithNearby)
		{
			return true;
		}
		return orig(self);
	}
}