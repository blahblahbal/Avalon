using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvLifeforce : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.lifeForce = true;
        player.statLifeMax2 += player.statLifeMax / 100 * 30;
    }
}
