using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvHeartsick : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().HeartsickElixir = true;
    }
}
