using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvObsidianSkin : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.lavaImmune = true;
    }
}
