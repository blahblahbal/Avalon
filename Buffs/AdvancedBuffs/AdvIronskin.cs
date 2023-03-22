using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvIronskin : ModBuff
{
    private const int DefenseIncrease = 12;
    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense += DefenseIncrease;
    }
}
