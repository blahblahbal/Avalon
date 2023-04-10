using Avalon.Buffs;
using Avalon.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common; 

public class ExxoEquipEffects : ModPlayer
{
    public bool BloodyWhetstone;
    public override void ResetEffects()
    {
        BloodyWhetstone = false;
    }
    public override void OnHitNPC(NPC npc, NPC.HitInfo hit, int damage)
    {
        if (hit.DamageType == DamageClass.Melee && BloodyWhetstone)
        {
            if (!npc.HasBuff<Bleeding>())
            {
                npc.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks = 1;
            }

            npc.AddBuff(ModContent.BuffType<Bleeding>(), 120);
        }
    }
}
