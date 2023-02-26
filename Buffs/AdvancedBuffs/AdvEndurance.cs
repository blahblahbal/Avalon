using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvEndurance : ModBuff
{
    private const float PercentIncrease = 0.15f;

    public override void Update(Player player, ref int buffIndex)
    {
        player.endurance += PercentIncrease;
    }
}
