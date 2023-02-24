using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvFeatherfall : ModBuff
{
    // TODO: IMPLEMENT

    public override void Update(Player player, ref int buffIndex)
    {
        player.slowFall = true;
    }
}
