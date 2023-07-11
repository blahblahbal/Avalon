using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class BloodCast : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.statManaMax2 += player.statLifeMax2;
        player.GetModPlayer<AvalonPlayer>().BloodCasting = true;
    }
}
