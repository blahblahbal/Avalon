using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvWarmth : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.resistCold = true;
    }
}
