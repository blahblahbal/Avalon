using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class CaesiumBoostingStance : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoTimeDisplay[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.endurance += 0.2f;
        player.accRunSpeed *= 0.3f;
        player.maxRunSpeed *= 0.3f;
        player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
    }
}
