using Avalon.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

internal class SilenceCandleBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[Type] = true;
		Main.debuff[Type] = true;
	}
}
public class SilenceCandleHook : ModHook
{
	protected override void Apply()
	{
		On_NPC.NewNPC += On_NPC_NewNPC;
	}

	private int On_NPC_NewNPC(On_NPC.orig_NewNPC orig, IEntitySource source, int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target)
	{
		NPC npc = new NPC();
		npc.SetDefaults(Type);
		for (int k = 0; k < 255; k++)
		{
			if (!Main.player[k].active || Main.player[k].dead)
				continue;

			if (Main.player[k].HasBuff(ModContent.BuffType<SilenceCandleBuff>()) && ((!NPCID.Sets.CountsAsCritter[Type] &&
				!npc.townNPC && !NPCID.Sets.TownCritter[Type] && !npc.boss && source is EntitySource_SpawnNPC) || Data.Sets.NPC.SilenceCandleStopSpawns[Type]))
			{
				return 200;
			}
		}

			
		return orig.Invoke(source, X, Y, Type, Start, ai0, ai1, ai2, ai3, Target);
	}
}
