using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvMagnet : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.treasureMagnet = true;
    }
}
