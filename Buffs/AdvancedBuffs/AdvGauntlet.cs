using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvGauntlet : ModBuff
{
    private const int DefenseDecrease = 10;
    private const float DamageIncrease = 0.18f;

    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense -= DefenseDecrease;
        player.GetDamage(DamageClass.Melee) += DamageIncrease;
    }
}
