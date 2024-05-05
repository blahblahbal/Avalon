using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class AmberShardBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetAttackSpeed(DamageClass.Generic) += 0.15f;
    }
}
