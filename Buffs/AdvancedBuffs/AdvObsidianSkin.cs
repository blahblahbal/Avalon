using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs.AdvancedBuffs;

public class AdvObsidianSkin : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.lavaImmune = true;
    }
}
