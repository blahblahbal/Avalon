using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs;

public class Wisdom : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.manaCost += 0.35f;
        player.GetDamage(DamageClass.Magic) += 0.25f;
    }
}
