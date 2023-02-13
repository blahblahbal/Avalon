using ExxoAvalonOrigins.Items.Accessories;
using ExxoAvalonOrigins.Items.Accessories.PreHardmode;
using ExxoAvalonOrigins.Items.Material;
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
        if(npc.type == NPCID.AngryBones || npc.type >= NPCID.AngryBonesBig && npc.type <= NPCID.AngryBonesBigHelmet)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlackWhetstone>(), 100));
        }
    }
    public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    {
        globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(),ModContent.ItemType<BloodyWhetstone>(), 160));
        globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<BloodBarrage>(), 160));
        globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<SanguineKatana>(), 160));
    }
}
