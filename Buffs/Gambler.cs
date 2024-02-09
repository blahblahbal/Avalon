using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Gambler : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().Gambler = true;
        player.GetCritChance(DamageClass.Generic) += 30;
    }
}
