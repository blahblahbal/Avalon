using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs;

public class Gauntlet : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense -= 6;
        player.GetDamage(DamageClass.Melee) += 0.12f;
    }
}
