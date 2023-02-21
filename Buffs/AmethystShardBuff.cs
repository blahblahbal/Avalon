using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs;

public class AmethystShardBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense += 6;
    }
}
