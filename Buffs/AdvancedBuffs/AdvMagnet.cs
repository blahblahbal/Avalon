using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvMagnet : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.treasureMagnet = true;
    }
}
