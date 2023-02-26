using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs;

public class BloodCast : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.statManaMax2 += player.statLifeMax2;
    }
}
