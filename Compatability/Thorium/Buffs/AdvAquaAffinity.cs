using Terraria;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Buffs;

public class AdvAquaAffinity : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.ignoreWater = true;
        if (player.wet)
        {
            player.moveSpeed += 0.15f;
            player.runAcceleration += 0.12f;
        }
    }
}
