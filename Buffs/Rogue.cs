using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs;

public class Rogue : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Ranged) -= 0.03f;
        player.ammoCost80 = true;
    }
}
