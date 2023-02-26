using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs;

public class Magnet : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.treasureMagnet = true;
    }
}
