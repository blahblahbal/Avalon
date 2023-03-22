using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

// TODO: IMPLEMENT
public class StaminaDrain : ModBuff
{
    private int stacks = 1;

    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        if (stacks == 1)
        {
            tip += " 20%";
        }
        else if (stacks == 2)
        {
            tip += " 40%";
        }
        else if (stacks == 3)
        {
            tip += " 60%";
        }
        else if (stacks == 4)
        {
            tip += " 80%";
        }
        else if (stacks == 5)
        {
            tip += " 100%";
        }
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrain = true;
        stacks = player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks;
        if (player.buffTime[buffIndex] == 0)
        {
            player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks = 1;
        }
    }
}
