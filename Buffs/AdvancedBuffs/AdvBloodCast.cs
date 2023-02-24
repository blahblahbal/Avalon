using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvBloodCast : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.statManaMax2 += player.statLifeMax2;
    }
}
