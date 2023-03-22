using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvManaRegeneration : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.manaRegenBuff = true;
    }
}
