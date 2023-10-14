using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Avalon.Common.Players;
using Avalon.Common;

namespace Avalon.Buffs.Debuffs;

public class Wormed : ModBuff
{
    private int timer;
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<AvalonGlobalNPCInstance>().Wormed = true;
    }
}
