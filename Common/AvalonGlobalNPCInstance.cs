using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonGlobalNPCInstance : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public bool AstigSpawned { get; set; }
    public int LacerateStacks { get; set; } = 1;
    public bool Lacerated { get; set; }
    public bool IsLaceratedHM { get; set; }
    public int BreathCd { get; set; } = 45;
    public bool DlBreath { get; set; }
    public bool Electrified { get; set; }
    public bool Frozen { get; set; }
    public bool InfernaSpawned { get; set; }
    public bool JugRunOnce { get; set; }
    public bool LavaWalk { get; set; }
    public bool Malaria { get; set; }
    public bool NecroticDrain { get; set; }
    public bool NoOneHitKill { get; set; }
    public int ORebirth { get; set; }
    public bool Silenced { get; set; }
    public int SlimeHitCounter { get; set; }
    public bool Slowed { get; set; }
    public int SpikeTimer { get; set; }
    public bool Virulent { get; set; }
    public bool Inferno { get; set; }
    public bool Pathogen { get; set; }
    public bool ShowStats { get; set; }
    public bool Wormed { get; set; }

    public override void ResetEffects(NPC npc)
    {
        NecroticDrain = false;
        Malaria = false;
        Electrified = false;
        Lacerated = false;
        Virulent = false;
        Inferno = false;
        Pathogen = false;
        Wormed = false;
        //BleedStacks = 1;
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        if (Pathogen)
        {
            drawColor.G = (byte)MathHelper.Clamp(drawColor.G - 76,0,255);
            drawColor.R = (byte)MathHelper.Clamp(drawColor.R - 25,0,255);
        }
    }
    public override void PostAI(NPC npc)
    {
        if (npc.HasBuff(BuffID.BrokenArmor))
        {
            npc.defense /= 2;
        }
        if (npc.HasBuff(BuffID.Slow))
        {
            npc.position += npc.velocity * -0.3f;
        }
        if (npc.HasBuff(BuffID.Chilled))
        {
            npc.position += npc.velocity * -0.35f;
        }
    }
    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        if(Pathogen && npc.ichor)
        {
            modifiers.ArmorPenetration += 7;
        }
        if (Pathogen && npc.betsysCurse)
        {
            modifiers.ArmorPenetration += 20;
        }
    }
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if(npc.type == NPCID.WallofFlesh && npc.life < 500)
        {
            npc.lifeRegen -= 100;
            damage = 50;
        }
        if (npc.HasBuff(BuffID.Electrified))
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= 8;
            if (npc.velocity != Vector2.Zero)
                npc.lifeRegen -= 40;
            if (damage < 4)
            {
                damage = 4;
            }
        }
        if (Wormed)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 16;
            if (damage < 4)
            {
                damage = 4;
            }
        }
        if (Malaria)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 6;
            if (damage < 3)
            {
                damage = 3;
            }
        }
        if (Inferno)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 40;
            if (damage < 6)
            {
                damage = 6;
            }
        }
        if (NecroticDrain)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 90;
            if (damage < 3)
            {
                damage = 3;
            }
        }
        if (Electrified)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            if (npc.velocity != Vector2.Zero)
                npc.lifeRegen -= 30;
            npc.lifeRegen -= 40;
            if (damage < 6)
            {
                damage = 6;
            }
        }
        if (Lacerated)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            int mult = 4;
            if (IsLaceratedHM)
            {
                mult = 6;
            }
            npc.lifeRegen -= mult * LacerateStacks * 2;
            if (damage < LacerateStacks * 2)
            {
                damage = LacerateStacks * 2;
            }
        }
        if (Virulent)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 100;
            if (damage < 10)
            {
                damage = 10;
            }
        }
        if (npc.lifeRegen < 0 && Pathogen)
        {
            damage = (int)(damage * 1.5f);
            npc.lifeRegen = (int)(npc.lifeRegen * 1.5f);
        }
    }
}
