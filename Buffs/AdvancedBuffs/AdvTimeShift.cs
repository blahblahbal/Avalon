using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvTimeShift : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        Main.time--;
    }
}
