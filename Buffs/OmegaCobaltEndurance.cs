using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class OmegaCobaltEndurance : ModBuff
{
    private const int DefenseIncrease = 16;
    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense += DefenseIncrease;
    }
}
