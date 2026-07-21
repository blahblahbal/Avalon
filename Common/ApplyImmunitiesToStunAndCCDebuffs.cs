using Avalon.Data.Sets;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

public class ApplyImmunitiesToStunAndCCDebuffs : ModSystem
{
	public override void PostSetupContent()
	{
		List<int> debuffs = new List<int>();
		for (int i = 0; i < BuffSets.CCOrSlowDebuffThatCannotGoOnBossesOrNPCsThatWouldCauseSignificantJank.Length; i++)
		{
			if (BuffSets.CCOrSlowDebuffThatCannotGoOnBossesOrNPCsThatWouldCauseSignificantJank[i])
				debuffs.Add(i);
		}
		for (int i = 0; i < NPCSets.StunOrSlowResistant.Length; i++)
		{
			if (NPCSets.StunOrSlowResistant[i] || ContentSamples.NpcsByNetId[i].boss)
			{
				foreach (var i2 in debuffs)
					NPCID.Sets.SpecificDebuffImmunity[i][i2] = true;
			}
		}
	}
}
