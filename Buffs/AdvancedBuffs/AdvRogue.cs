using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvRogue : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.ammoCost75 = true;
        player.GetDamage(DamageClass.Ranged) -= 0.05f;
    }
}
