using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class AttackSpeedDown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
		BuffID.Sets.LongerExpertDebuff[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
		player.GetAttackSpeed(DamageClass.Generic) -= 0.3f;
		if (player.GetModPlayer<AvalonPlayer>().Pathogen)
			player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
    }
}
