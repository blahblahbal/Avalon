using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvIronskin : ModBuff
{
    private const int DefenseIncrease = 12;
    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense += DefenseIncrease;
    }
}
