using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvTitan : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.kbBuff = true;
    }
}
