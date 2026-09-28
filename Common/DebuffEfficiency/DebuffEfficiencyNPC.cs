using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.DebuffEfficiency;

public class DebuffEfficiencyNPC : GlobalNPC
{
	public override bool InstancePerEntity => true;
	/// <summary>
	/// Changes to this need to happen before buffs are updated, use IBuffThatNeedsToUpdatePlayerEarly if on a buff.
	/// </summary>
	public float DebuffEfficiency = 1;
	public override void ResetEffects(NPC npc)
	{
		DebuffEfficiency = 1;
	}
	public override void UpdateLifeRegen(NPC npc, ref int damage) // this will have to be updated when tmod updates because they have a way to properly do this now.
	{
		if (npc.lifeRegen < 0)
		{
			npc.lifeRegen = (int)(npc.lifeRegen * DebuffEfficiency);
		}
	}
	public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
	{
		if (npc.HasBuff(BuffID.BrokenArmor))
		{
			modifiers.ArmorPenetration += 20 * (1f - DebuffEfficiency);
		}
		if (npc.ichor)
		{
			modifiers.ArmorPenetration += 15 * (1f - DebuffEfficiency);
		}
		if (npc.onFire2)
		{
			modifiers.Knockback += 0.1f * (1f - DebuffEfficiency);
		}
	}
	public override bool PreKill(NPC npc)
	{
		if (npc.midas)
		{
			npc.value = (int)(npc.value * (1f + (Main.rand.NextFloat(0.3f, 0.5f) * (1f - DebuffEfficiency))));
		}
		return base.PreKill(npc);
	}
}
