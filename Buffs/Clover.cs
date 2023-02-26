using ExxoAvalonOrigins.Common;
using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs;

public class Clover : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().Lucky = true;
        player.enemySpawns = true;
        player.GetModPlayer<AvalonPlayer>().AdvancedBattle = true;
    }
}
