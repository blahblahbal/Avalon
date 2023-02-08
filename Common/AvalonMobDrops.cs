﻿using ExxoAvalonOrigins.Items.Material;
using ExxoAvalonOrigins.Items.Weapons.Magic.PreHardmode;
using ExxoAvalonOrigins.Items.Weapons.Melee.PreHardmode;
using ExxoAvalonOrigins.NPCs.PreHardmode;
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
        //Blood Barrage & Katana Drops
        if(npc.type is 489 or 490)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BloodBarrage>(), 200, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SanguineKatana>(), 200, 1, 1));
        }
        if (npc.type == ModContent.NPCType<FallenHero>() || npc.type == ModContent.NPCType<BloodshotEye>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BloodBarrage>(), 10, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SanguineKatana>(), 10, 1, 1));
        }
    }
}
