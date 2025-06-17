using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvDupe : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
		player.GetModPlayer<AvalonPlayer>().AdvDupeLoot = true;
    }
}
