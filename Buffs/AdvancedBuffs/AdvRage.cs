using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvRage : ModBuff
{
    private const int PercentIncrease = 15;

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetCritChance<GenericDamageClass>() += PercentIncrease;
    }
}
