using Avalon.Common.Players;
using Avalon.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class XanthophyteTree : ModBuff
{
	public override void Update(Player player, ref int buffIndex)
    {
		player.velocity.X *= 0f;
		player.statDefense += 40;
		player.lifeRegen += 10;
		player.controlJump = false;
		player.controlDown = false;
		player.controlLeft = false;
		player.controlRight = false;
		player.controlUp = false;
		player.controlUseItem = false;
		player.controlUseTile = false;
		player.controlThrow = false;
		player.gravDir = 1f;
	}
}
