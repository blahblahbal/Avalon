using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvRegeneration : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.lifeRegen += 3;
    }
}
