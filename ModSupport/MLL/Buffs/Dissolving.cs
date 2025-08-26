using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria;
using Avalon.Common;
using Avalon.ModSupport.MLL.Items;

namespace Avalon.ModSupport.MLL.Buffs;

public class Dissolving : ModBuff
{
	private int timer;
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
		timer++;
		if (timer % 10 == 0)
		{
			int amt = 4;
			if (player.GetModPlayer<AcidWadersPlayer>().AcidWalk) amt = 2;
			player.statLife -= amt;
			CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.LifeRegen, amt, dramatic: false, dot: true);
			if (player.statLife <= 0)
			{
				player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromKey($"Mods.Avalon.DeathText.Acid_{Main.rand.Next(5)}", $"{player.name}")), 10, 0);
			}
		}
		player.lifeRegenTime = 0;
		if (player.buffTime[buffIndex] == 0)
		{
			timer = 0;
		}
	}
}
