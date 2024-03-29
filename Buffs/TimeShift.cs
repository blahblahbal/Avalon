using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class TimeShift : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().TimeSlowCounter++;
        if (player.GetModPlayer<AvalonPlayer>().TimeSlowCounter % 2 == 0)
        {
            Main.time--;
        }
        if (player.buffTime[buffIndex] == 0)
        {
            player.GetModPlayer<AvalonPlayer>().TimeSlowCounter = 0;
        }
    }
}
