using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvNightOwl : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.nightVision = true;
    }
}
