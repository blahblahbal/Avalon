using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvThorns : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.turtleArmor = true;
        player.thorns = 1f;
    }
}
