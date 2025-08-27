using Terraria.ModLoader;
using Terraria;
using Avalon.Common;
using Avalon.Common.Players;

namespace Avalon.ModSupport.MLL.Buffs;

public class Dissolving : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}
	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.GetGlobalNPC<AvalonGlobalNPCInstance>().Dissolving = true;
	}
	public override void Update(Player player, ref int buffIndex)
	{
		if (player.lifeRegen > 0)
		{
			player.lifeRegen = 0;
		}
		player.lifeRegenTime = 0;
		player.GetModPlayer<AvalonPlayer>().Dissolving = true;
	}
}
