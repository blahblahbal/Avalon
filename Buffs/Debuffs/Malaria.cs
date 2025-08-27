using Avalon.Common.Players;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Avalon.Buffs.Debuffs;

public class Malaria : ModBuff
{
	private int timer;
	// do something different than just "dot" with this debuff
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.lifeRegen > 0)
		{
			player.lifeRegen = 0;
		}
		
		player.lifeRegenTime = 0;
		if (player.buffTime[buffIndex] == 0)
		{
			timer = 0;
		}
		player.GetModPlayer<AvalonPlayer>().Malaria = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.GetGlobalNPC<AvalonGlobalNPCInstance>().Malaria = true;
	}
}
