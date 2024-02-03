using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class BacteriaInfection : ModBuff
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Bacteria Infection");
        //Description.SetDefault("Yuck!");
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<AvalonGlobalNPCInstance>().BacterialInfection = true;
    }
}
