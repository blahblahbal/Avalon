using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvMining : ModBuff
{
    private const float PercentIncrease = 0.35f;
    public override void Update(Player player, ref int buffIndex)
    {
        player.pickSpeed -= PercentIncrease;
    }
}
