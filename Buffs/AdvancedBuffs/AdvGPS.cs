using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvGPS : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
    }
}
