using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs;

public class Wisdom : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.statManaMax2 += 60;
        player.GetDamage(DamageClass.Magic) -= 0.08f;
    }
}
