using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvSonar : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.sonarPotion = true;
    }
}
