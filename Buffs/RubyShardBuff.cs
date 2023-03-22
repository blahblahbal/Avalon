using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class RubyShardBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetArmorPenetration(DamageClass.Generic) += 5;
    }
}
