using Avalon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode.KingSting;

public class Larvae : ModNPC
{
    int splitTimer;
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 1;
    }

    public override void SetDefaults()
    {
        NPC.damage = 0;
        NPC.lifeMax = 200;
        NPC.defense = 10;
        NPC.noGravity = false;
        NPC.width = 38;
        NPC.aiStyle = -1;
        NPC.noTileCollide = false;
        NPC.height = 18;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0.05f;

        splitTimer = 0;
    }

    public override void AI()
    {
        splitTimer++;

        NPC.velocity.X *= 0.8f;

        int threshold;

        if (Main.expertMode)
            threshold = 180;
        else
            threshold = 240;

        if (splitTimer >= threshold)
        {
            NPC.ai[0] = 1;
            NPC.NPCLoot();
            NPC.active = false;
        }
    }

    public override void OnKill()
    {
        float halfWidth = NPC.width / 2;
        float halfHeight = NPC.height / 2;
        Vector2 origin = NPC.Center + new Vector2(Main.rand.NextFloat(-halfWidth, halfWidth + 1f), Main.rand.NextFloat(-halfHeight, halfHeight + 1f));
        if (NPC.ai[0] == 1)
        {
            NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)origin.X, (int)origin.Y, NPCID.Hornet, default, default, default, default, default, NPC.target);
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)origin.X, (int)origin.Y, NPCID.Bee, default, default, default, default, default, NPC.target);
            }
        }
    }
}
