using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Heartsick : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().Heartsick = true;
    }
}
