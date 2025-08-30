using Avalon.Common;
using Terraria;
using Terraria.ID;

namespace Avalon.Hooks;

internal class BiomeTorch : ModHook
{
	protected override void Apply()
	{
		On_Player.BiomeTorchHoldStyle += On_Player_BiomeTorchHoldStyle;
		On_Player.BiomeTorchPlaceStyle += On_Player_BiomeTorchPlaceStyle;
		On_Player.BiomeCampfirePlaceStyle += On_Player_BiomeCampfirePlaceStyle;
	}

	private int On_Player_BiomeCampfirePlaceStyle(On_Player.orig_BiomeCampfirePlaceStyle orig, Player self, ref int type, ref int style)
	{
		if (self.UsingBiomeTorches && self.InModBiome<Biomes.DungeonAltColors>())
		{
			style = 7;
		}
		return orig.Invoke(self, ref type, ref style);
	}

	private int On_Player_BiomeTorchPlaceStyle(On_Player.orig_BiomeTorchPlaceStyle orig, Player self, ref int type, ref int style)
	{
		if (self.UsingBiomeTorches && self.InModBiome<Biomes.DungeonAltColors>())
		{
			style = 13;
		}
		return orig.Invoke(self, ref type, ref style);
	}

	private int On_Player_BiomeTorchHoldStyle(On_Player.orig_BiomeTorchHoldStyle orig, Player self, int style)
	{
		if (self.UsingBiomeTorches && self.InModBiome<Biomes.DungeonAltColors>())
		{
			style = ItemID.BoneTorch;
		}
		return orig.Invoke(self, style);
	}
}
