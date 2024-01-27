using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode;

internal class DesertBeakWingNPC : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 1;
        NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            Hide = true // Hides this NPC from the bestiary
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, bestiaryData);
    }
    public override void SetDefaults()
    {
        NPC.TargetClosest();
        NPC.damage = 45;
        NPC.noTileCollide = true;
        NPC.lifeMax = 4650;
        NPC.defense = 40;
        NPC.noGravity = true;
        NPC.width = 77;
        NPC.aiStyle = -1;
        NPC.npcSlots = 100f;
        NPC.value = 50000f;
        NPC.timeLeft = 22500;
        NPC.height = 40;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit28;
        NPC.DeathSound = SoundID.NPCDeath31;
        NPC.scale = 1f;
    }
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        return false;
    }
    public override void AI()
    {
        if (NPC.ai[3] == 1)
        {
            NPC.position.X = Main.npc[NPC.realLife].Center.X - 77 - 31;
        }
        else if (NPC.ai[3] == 2)
        {
            NPC.position.X = Main.npc[NPC.realLife].Center.X + 31;
        }
        if (Main.npc[NPC.realLife].frame.Y == 0 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 31;
        }
        if (Main.npc[NPC.realLife].frame.Y == 1 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 26;
        }
        if (Main.npc[NPC.realLife].frame.Y == 2 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 12;
        }
        if (Main.npc[NPC.realLife].frame.Y == 3 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 7;
        }
        if (Main.npc[NPC.realLife].frame.Y == 4 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 22;
            if (NPC.position.X > Main.npc[NPC.realLife].position.X)
            {
                NPC.position.X -= 15;
            }
            if (NPC.position.X < Main.npc[NPC.realLife].position.X)
            {
                NPC.position.X += 18;
            }
        }
        if (Main.npc[NPC.realLife].frame.Y == 5 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 31;
            if (NPC.position.X > Main.npc[NPC.realLife].position.X)
            {
                NPC.position.X -= 30;
            }
            if (NPC.position.X < Main.npc[NPC.realLife].position.X)
            {
                NPC.position.X += 30;
            }
        }
        if (Main.npc[NPC.realLife].frame.Y == 6 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 34;
            if (NPC.position.X > Main.npc[NPC.realLife].position.X)
            {
                NPC.position.X -= 13;
            }
            if (NPC.position.X < Main.npc[NPC.realLife].position.X)
            {
                NPC.position.X += 13;
            }
        }
        if (Main.npc[NPC.realLife].frame.Y == 7 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 29;
            if (NPC.position.X > Main.npc[NPC.realLife].position.X)
            {
                NPC.position.X -= 5;
            }
            if (NPC.position.X < Main.npc[NPC.realLife].position.X)
            {
                NPC.position.X += 5;
            }
        }
        // npcPosY + lengthDiffBetHorizandDiagDist
        float heightDiff = Main.npc[NPC.realLife].Center.Y - NPC.Center.Y;
        float widthDiff = Main.npc[NPC.realLife].Center.X - NPC.Center.X;
        if (NPC.position.X < Main.npc[NPC.realLife].position.X)
        {
            NPC.position.Y -= heightDiff - widthDiff * (-Main.npc[NPC.realLife].velocity.X * 0.05f);
        }
        if (NPC.position.X > Main.npc[NPC.realLife].position.X)
        {
            NPC.position.Y += -heightDiff - widthDiff * (Main.npc[NPC.realLife].velocity.X * 0.05f);
        }
        if (NPC.life <= 0)
        {
            NPC.life = 0;
            NPC.active = false;
        }
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (Main.npc[NPC.realLife].life <= 0 || NPC.life <= 0)
        {
            NPC.life = 0;
            NPC.active = false;
        }
    }
}
