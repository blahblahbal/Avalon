using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class Coalesced : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Generic) -= 0.1f;
    }
}
