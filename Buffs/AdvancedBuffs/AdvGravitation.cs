using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvGravitation : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.gravControl = true;
    }
}
