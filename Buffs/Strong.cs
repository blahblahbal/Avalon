using ExxoAvalonOrigins.Common;
using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Buffs;

public class Strong : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Generic) += 0.1f;
        player.GetCritChance<GenericDamageClass>() += 1;
        player.statDefense += 5;
        player.lifeRegen++;
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.05f);
    }
}
