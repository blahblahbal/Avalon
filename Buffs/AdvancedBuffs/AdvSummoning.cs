using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvSummoning : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.maxMinions += 2;
    }
}
