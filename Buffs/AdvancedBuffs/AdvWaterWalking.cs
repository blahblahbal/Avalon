using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvWaterWalking : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.waterWalk = true;
    }
}
