using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvHunter : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.detectCreature = true;
    }
}
