using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvInvincibility : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.immune = true;
    }
}
