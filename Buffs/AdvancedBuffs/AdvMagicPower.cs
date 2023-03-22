using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvMagicPower : ModBuff
{
    private const float PercentIncrease = 0.3f;

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Magic) += PercentIncrease;
    }
}
