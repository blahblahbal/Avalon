using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Fledgling : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
		player.wingTimeMax = 26;
		player.wingsLogic = 46;
    }
}
