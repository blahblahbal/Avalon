using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class SapphireShardBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Magic) += 0.08f;
    }
}
