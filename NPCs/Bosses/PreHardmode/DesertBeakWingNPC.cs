using Microsoft.Xna.Framework;
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
        NPC.lifeMax = 3650;
        NPC.defense = 30;
        NPC.noGravity = true;
        NPC.width = 62;
        NPC.aiStyle = -1;
        NPC.npcSlots = 100f;
        NPC.value = 50000f;
        NPC.timeLeft = 22500;
        NPC.height = 45;
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
            NPC.position.X = Main.npc[NPC.realLife].Center.X - 62 - 31;
        }
        else if (NPC.ai[3] == 2)
        {
            NPC.position.X = Main.npc[NPC.realLife].Center.X + 31;
        }
        if (Main.npc[NPC.realLife].frame.Y == 0 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 39 - 15;
        }
        if (Main.npc[NPC.realLife].frame.Y == 1 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 39 - 3;
        }
        if (Main.npc[NPC.realLife].frame.Y == 2 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 9;
        }
        if (Main.npc[NPC.realLife].frame.Y == 3 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y + 2;
        }
        if (Main.npc[NPC.realLife].frame.Y == 4 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 39 + 17;
        }
        if (Main.npc[NPC.realLife].frame.Y == 5 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 39 + 7;
        }
        if (Main.npc[NPC.realLife].frame.Y == 6 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 39 + 3;
        }
        if (Main.npc[NPC.realLife].frame.Y == 7 * 178)
        {
            NPC.position.Y = Main.npc[NPC.realLife].Center.Y - 39 - 5;
        }
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (Main.npc[NPC.realLife].life <= 0)
        {
            NPC.life = 0;
            NPC.active = false;
        }
    }
}
