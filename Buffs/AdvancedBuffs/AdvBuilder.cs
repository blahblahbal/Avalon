using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvBuilder : ModBuff
{
    private const float SpeedIncrease = 0.3f;
    private const int BlockRangeIncrease = 2;

    public override void Update(Player player, ref int buffIndex)
    {
        player.tileSpeed += SpeedIncrease;
        player.wallSpeed += SpeedIncrease;
        player.blockRange += BlockRangeIncrease;
    }
}
