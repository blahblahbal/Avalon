using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class ArmorPenetration : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
		player.GetArmorPenetration(DamageClass.Generic) += 7;
    }
}
