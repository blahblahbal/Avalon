using Avalon.Common.Players;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

internal class Sticky : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}
	public override void Update(Player player, ref int buffIndex)
	{
		player.moveSpeed /= 2.5f;
		if (player.velocity.Y == 0f && Math.Abs(player.velocity.X) > 1f)
		{
			player.velocity.X /= 2f;
			if (player.GetModPlayer<AvalonPlayer>().InertiaBoots)
			{
				player.velocity.X /= 1.5f;
			}
		}
			
	}
}
