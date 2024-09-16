using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class SpiritOfOriginal : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
		player.aggro -= 1000;
    }
}
