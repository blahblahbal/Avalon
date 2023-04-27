using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class BrokenWeaponry : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().BrokenWeaponry = true;
    }
}
