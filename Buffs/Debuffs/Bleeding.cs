using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class Bleeding : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<AvalonGlobalNPCInstance>().Bleeding = true;
        npc.GetGlobalNPC<AvalonGlobalNPCInstance>().IsBleedingHMBleed = false;
        if (npc.lifeRegen > 0)
        {
            npc.lifeRegen = 0;
        }

        int mult = 4;
        if (Main.hardMode)
        {
            mult = 6;
        }
        npc.lifeRegen -= mult * npc.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks;
        if (npc.buffTime[buffIndex] == 0)
        {
            npc.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks = 1;
        }
    }

    public override bool ReApply(NPC npc, int time, int buffIndex)
    {
        if (npc.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks < 3)
        {
            npc.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks++;
        }

        return false;
    }
}
