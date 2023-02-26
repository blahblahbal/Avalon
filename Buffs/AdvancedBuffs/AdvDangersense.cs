using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvDangersense : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.dangerSense = true;
    }
}
