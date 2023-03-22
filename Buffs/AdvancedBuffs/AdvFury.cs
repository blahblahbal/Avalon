using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvFury : ModBuff
{
    private const int PercentIncrease = 30;

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.3f);
    }
}
