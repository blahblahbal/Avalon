using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class CurseofDelirium : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        AvalonPlayer modPlayer = player.GetModPlayer<AvalonPlayer>();

        if (modPlayer.DeliriumCount > 0)
        {
            player.confused = true;
            modPlayer.DeliriumCount--;
        }
        else if (Main.rand.NextBool((int)(600 / player.DebuffPotency)))
        {
            modPlayer.DeliriumCount = (int)(Main.rand.Next(240, 481) / player.DebuffPotency);
        }
    }
}
