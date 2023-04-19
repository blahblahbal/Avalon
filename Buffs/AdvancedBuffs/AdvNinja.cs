using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvNinja : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().NinjaElixir = true;
    }
}
