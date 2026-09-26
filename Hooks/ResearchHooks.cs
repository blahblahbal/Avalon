using Avalon.Common;
using Avalon.Items.Tools.Hardmode;
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;
public class ResearchHooks : ModHook
{
	protected override void Apply()
	{
		On_ContentSamples.FillResearchItemOverrides += On_ContentSamples_FillResearchItemOverrides;
	}

	private void On_ContentSamples_FillResearchItemOverrides(On_ContentSamples.orig_FillResearchItemOverrides orig)
	{
		orig.Invoke();
		// Shadow Phone
		ContentSamples.AddItemResearchOverride(ModContent.ItemType<ShadowPhoneDummy>(),
			ModContent.ItemType<ShadowPhoneSpawn>(),
			ModContent.ItemType<ShadowPhoneDungeon>(),
			ModContent.ItemType<ShadowPhoneHell>(),
			ModContent.ItemType<ShadowPhoneHome>(),
			ModContent.ItemType<ShadowPhoneJungleTropics>(),
			ModContent.ItemType<ShadowPhoneOcean>(),
			ModContent.ItemType<ShadowPhoneRandom>(),
			ModContent.ItemType<ShadowPhoneSurface>());
	}
}
