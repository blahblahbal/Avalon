using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class CobaltEndurance : ModBuff
{
    private const int DefenseIncrease = 8;
    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense += DefenseIncrease;
    }
}
