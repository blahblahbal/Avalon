using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Fury : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.2f);
    }
}
