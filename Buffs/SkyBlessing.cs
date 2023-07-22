using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

// TODO: IMPLEMENT
public class SkyBlessing : ModBuff
{
    private int stacks = 1;

    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        if (stacks < 10)
        {
            tip += (stacks * 4) + "%";
        }
        else tip += "45%";
    }

    public override void Update(Player player, ref int buffIndex)
    {
        //player.GetModPlayer<AvalonPlayer>().SkyBlessing = true;
        stacks = player.GetModPlayer<AvalonPlayer>().SkyStacks;
        if (player.buffTime[buffIndex] == 1)
        {
            player.GetModPlayer<AvalonPlayer>().SkyStacks = 1;
            stacks = 1;
        }
    }
}
