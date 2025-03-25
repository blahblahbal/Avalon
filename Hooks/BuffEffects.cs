using Avalon.Buffs;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class BuffEffects : ModHook
{
	protected override void Apply()
	{
		//On_Projectile.FishingCheck_RollDropLevels += OnFishingCheckRollDropLevels;
		//CommonCode.DropItemForEachInteractingPlayerOnThePlayer += OnDropItemForEachInteractingPlayerOnThePlayer;
		On_Player.AddBuff += OnAddBuff;
		On_NPC.AddBuff += OnAddBuffNPC;
	}

	private static void OnAddBuff(On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet = true, bool foodHack = false)
	{
		if (self.GetModPlayer<AvalonPlayer>().Pathogen && Main.debuff[type])
		{
			timeToAdd *= 2;
		}
		if (self.GetModPlayer<AvalonPlayer>().ThePill && Main.debuff[type] && type != BuffID.PotionSickness)
		{
			timeToAdd = (int)(timeToAdd * 0.8f);
		}
		//for (int buffIndex = 0; buffIndex < Player.MaxBuffs; buffIndex++)
		//{
		//	if (self.buffType[buffIndex] == type)
		//	{
		//		// move this code for reckoning to its own modbuff when it's actually added, minus the stupid parts, copy how skyblessing does it

		//		/*if (type == ModContent.BuffType<Buffs.Reckoning>())
		//		{
		//			self.buffTime[j] += timeToAdd;
		//			if (self.GetModPlayer<ExxoPlayer>().reckoningLevel < 10)
		//			{
		//				self.GetModPlayer<ExxoPlayer>().reckoningLevel++;
		//			}
		//			else
		//				self.GetModPlayer<ExxoPlayer>().reckoningLevel = 10;
		//			if (self.buffTime[j] > 60 * 8)
		//			{
		//				self.buffTime[j] = 60 * 8;
		//				return;
		//			}
		//		}
		//		else if (self.buffTime[j] < timeToAdd)
		//		{
		//			self.buffTime[j] = timeToAdd;
		//		}*/
		//	}
		//}
		orig(self, type, timeToAdd, quiet, foodHack);
	}

	private static void OnAddBuffNPC(On_NPC.orig_AddBuff orig, NPC self, int type, int time, bool quiet = false)
	{
		if (ExxoAvalonOrigins.Achievements != null)
		{
			if (((self.HasBuff(BuffID.OnFire) || self.HasBuff(BuffID.OnFire3)) && self.HasBuff(BuffID.CursedInferno) && (self.HasBuff(BuffID.Frostburn) || self.HasBuff(BuffID.Frostburn2)) && type == BuffID.ShadowFlame) ||
			(self.HasBuff(BuffID.ShadowFlame) && self.HasBuff(BuffID.CursedInferno) && (self.HasBuff(BuffID.Frostburn) || self.HasBuff(BuffID.Frostburn2)) && (type == BuffID.OnFire || type == BuffID.OnFire3)) ||
			(self.HasBuff(BuffID.ShadowFlame) && self.HasBuff(BuffID.CursedInferno) && (self.HasBuff(BuffID.OnFire) || self.HasBuff(BuffID.OnFire3)) && (type == BuffID.Frostburn || type == BuffID.Frostburn2)) ||
			((self.HasBuff(BuffID.OnFire) || self.HasBuff(BuffID.OnFire3)) && self.HasBuff(BuffID.ShadowFlame) && (self.HasBuff(BuffID.Frostburn) || self.HasBuff(BuffID.Frostburn2)) && type == BuffID.CursedInferno))
			{
				ExxoAvalonOrigins.Achievements.Call("Event", "ItBurnsBurnsBurnsBurns");
			}
		}
		for (int j = 0; j < 5; j++)
		{
			if (self.buffType[j] == type)
			{
				if (type == ModContent.BuffType<Lacerated>())
				{
					self.buffTime[j] += time;
					if (self.GetGlobalNPC<AvalonGlobalNPCInstance>().LacerateStacks < 3)
					{
						self.GetGlobalNPC<AvalonGlobalNPCInstance>().LacerateStacks++;
					}
					if (self.buffTime[j] > AvalonGlobalNPC.BleedTime)
					{
						self.buffTime[j] = AvalonGlobalNPC.BleedTime;
						return;
					}
				}
				else if (self.buffTime[j] < time)
				{
					self.buffTime[j] = time;
				}
				return;
			}
			if (self.GetGlobalNPC<AvalonGlobalNPCInstance>().Pathogen && Main.debuff[self.buffType[j]])
			{
				if(self.buffTime[j] < time * 2)
					self.buffTime[j] = time * 2;
			}
	}
		orig(self, type, time, quiet);
	}

	//private static void OnFishingCheckRollDropLevels(
	//    On.Terraria.Projectile.orig_FishingCheck_RollDropLevels orig,
	//    Projectile self, int fishingLevel, out bool common,
	//    out bool uncommon, out bool rare, out bool veryrare,
	//    out bool legendary, out bool crate)
	//{
	//    orig(self, fishingLevel, out common, out uncommon, out rare, out veryrare, out legendary, out crate);
	//    if (!crate && Main.player[self.owner].HasBuff<AdvCrate>() && Main.rand.NextFloat() < AdvCrate.Chance)
	//    {
	//        crate = true;
	//    }
	//}

	//private static void OnDropItemForEachInteractingPlayerOnThePlayer(
	//    CommonCode.orig_DropItemForEachInteractingPlayerOnThePlayer orig, NPC npc, int itemId, UnifiedRandom rng,
	//    int chanceNumerator, int chanceDenominator, int stack, bool interactionRequired)
	//{
	//    if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<ExxoBuffPlayer>().Lucky)
	//    {
	//        chanceNumerator += (int)(chanceNumerator * AdvLuck.PercentIncrease);
	//    }

	//    orig(npc, itemId, rng, chanceNumerator, chanceDenominator, stack, interactionRequired);
	//}
}
