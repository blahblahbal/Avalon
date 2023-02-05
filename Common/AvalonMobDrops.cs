using ExxoAvalonOrigins.Items.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Common;

public class AvalonMobDrops : GlobalNPC
{
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if(npc.type == NPCID.Plantera)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<LifeDew>(), 1, 14, 20));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.ChlorophyteOre, 1, 60, 120));
        }
    }
}
