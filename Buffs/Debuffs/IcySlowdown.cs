using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class IcySlowdown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        //npc.GetGlobalNPC<AvalonGlobalNPCInstance>().slowed = true;
        npc.velocity.X = npc.direction == 1 ? 0.7f : -0.7f;
    }
}
