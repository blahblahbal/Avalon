using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.DebuffPotency;

public class DebuffPotencyGlobalBuff : GlobalBuff
{
	//public override void Update(int type, NPC npc, ref int buffIndex)
	//{
	//	float efficiency = npc.GetGlobalNPC<DebuffPotencyNPC>().DebuffEfficiency;
	//	//switch (type)
	//	//{

	//	//}
	//	var anpc = npc.GetGlobalNPC<AvalonGlobalNPCInstance>();
	//	if(anpc.Speed < 1)
	//	{
	//		anpc.Speed *= 1f - (efficiency - 1f);
	//	}
	//}
	public override void Update(int type, Player player, ref int buffIndex)
	{
		float efficiency = player.GetModPlayer<DebuffPotencyPlayer>().DebuffPotency;
		float efficiencyDiff = efficiency - 1f;
		switch (type)
		{
			case BuffID.BrokenArmor:
			case BuffID.WitheredArmor:
				player.statDefense *= (1f - (efficiencyDiff * 0.5f));
				break;
			case BuffID.Ichor:
				player.statDefense -= (int)(15 * efficiencyDiff);
				break;
			case BuffID.WitheredWeapon:
				float reduce = (1f - (efficiencyDiff * 0.5f));
				player.meleeDamage *= reduce;
				player.rangedDamage *= reduce;
				player.magicDamage *= reduce;
				player.minionDamage *= reduce;
				//player.rangedMultDamage *= 0.5f; // what is this????
				break;
			case BuffID.Weak:
				player.meleeDamage -= 0.051f * efficiencyDiff;
				player.meleeSpeed -= 0.051f * efficiencyDiff;
				player.statDefense -= (int)(4 * efficiencyDiff);
				player.moveSpeed -= 0.1f * efficiencyDiff;
				break;
			case BuffID.Hunger:
				player.statDefense -= (int)(2 * efficiencyDiff);
				player.allCrit -= 2f * efficiencyDiff;
				player.allDamage -= 0.05f * efficiencyDiff;
				player.meleeSpeed -= 0.05f * efficiencyDiff;
				player.minionKB -= 0.5f * efficiencyDiff;
				player.pickSpeed += 0.05f * efficiencyDiff;
				break;
			case BuffID.Starving:
				player.statDefense -= (int)(4 * efficiencyDiff);
				player.allCrit -= 4f * efficiencyDiff;
				player.allDamage -= 0.1f * efficiencyDiff;
				player.meleeSpeed -= 0.1f * efficiencyDiff;
				player.minionKB -= 1f * efficiencyDiff;
				player.pickSpeed += 0.15f * efficiencyDiff;
				break;
			case BuffID.ManaSickness:
				player.manaSickReduction *= efficiency;
				break;
		}
	}
}
