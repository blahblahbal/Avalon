using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.PreHardmode;

public class InfestedAmberSlime : ModNPC
{
    public override void OnKill()
    {
        int num = Main.rand.Next(2, 5);
        for (int i = 0; i < num; i++)
        {
            int n = NPC.NewNPC(NPC.GetSource_Loot(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Fly>());
            Main.npc[n].velocity = new Vector2(Main.rand.NextFloat(-3, 4), Main.rand.NextFloat(-2, 3));
        }
        // change to smaller mosquito
        NPC.NewNPC(NPC.GetSource_Loot(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MosquitoSmall>());
    }
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 2;
        Data.Sets.NPCSets.Toxic[NPC.type] = true;
    }

    public override void SetDefaults()
    {
        NPC.damage = 29;
        NPC.lifeMax = 70;
        NPC.defense = 6;
        NPC.width = 32;
        NPC.aiStyle = 1;
        NPC.value = 1000f;
        NPC.knockBackResist = 0.1f;
        NPC.height = 22;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.alpha = 50;
		//Banner = NPC.type;
		//BannerItem = ModContent.ItemType<InfestedAmberSlimeBanner>();
		SpawnModBiomes = [ModContent.GetInstance<Biomes.UndergroundTropics>().Type];
	}

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            //BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.InfestedAmberSlime")),
        });
    public override void FindFrame(int frameHeight)
    {
        int num2 = 0;
        if (NPC.aiAction == 0)
        {
            if (NPC.velocity.Y < 0f)
            {
                num2 = 2;
            }
            else if (NPC.velocity.Y > 0f)
            {
                num2 = 3;
            }
            else if (NPC.velocity.X != 0f)
            {
                num2 = 1;
            }
            else
            {
                num2 = 0;
            }
        }
        else if (NPC.aiAction == 1)
        {
            num2 = 4;
        }

        NPC.frameCounter += 1.0;
        if (num2 > 0)
        {
            NPC.frameCounter += 1.0;
        }

        if (num2 == 4)
        {
            NPC.frameCounter += 1.0;
        }

        if (NPC.frameCounter >= 8.0)
        {
            NPC.frame.Y = NPC.frame.Y + frameHeight;
            NPC.frameCounter = 0.0;
        }

        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return spawnInfo.Player.InModBiome<Biomes.UndergroundTropics>() && !spawnInfo.Player.ZoneDungeon ? 0.4f : 0f;
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0)
        {
            for (int i = 0; i < 30; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, Main.rand.Next(100, 200), default, Main.rand.NextFloat(1, 1.5f));
                Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1.5f, 5) * hit.HitDirection, Main.rand.NextFloat(-1, -5));
            }
        }
    }
}
