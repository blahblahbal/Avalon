using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvSwiftness : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.moveSpeed += 0.5f;
    }
}
