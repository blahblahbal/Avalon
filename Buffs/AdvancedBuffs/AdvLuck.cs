using ExxoAvalonOrigins.Common;
using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvLuck : ModBuff
{
    public const float PercentIncrease = 0.10f;

    // TODO: OUTDATED DESCRIPTION
    public override void Update(Player player, ref int buffIndex)
    {
        player.enemySpawns = true;
        player.GetModPlayer<AvalonPlayer>().Lucky = true;
        player.GetModPlayer<AvalonPlayer>().AdvancedBattle = true;
    }
}
