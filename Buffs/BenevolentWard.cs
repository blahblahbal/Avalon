using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class BenevolentWard : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().Ward = true;
        if (player.buffTime[buffIndex] == 1)
        {
            player.AddBuff(ModContent.BuffType<WardCurse>(), 20 * 60);
        }
    }
}
