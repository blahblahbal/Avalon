using Avalon.Common;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Savanna;

public class AmberSlime : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 2;
        Data.Sets.NPCSets.Toxic[NPC.type] = true;
    }

    public override void SetDefaults()
    {
        NPC.damage = 19;
        NPC.lifeMax = 62;
        NPC.defense = 5;
        NPC.width = 32;
        NPC.aiStyle = 1;
        NPC.value = 1000f;
        NPC.knockBackResist = 0.1f;
        NPC.height = 22;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
		//Banner = NPC.type;
		//BannerItem = ModContent.ItemType<AmberSlimeBanner>();
		SpawnModBiomes = [ModContent.GetInstance<Biomes.Savanna>().Type];
	}

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            //BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.AmberSlime")),
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
        return (spawnInfo.Player.InModBiome<Biomes.Savanna>() || spawnInfo.Player.InModBiome<Biomes.UndergroundTropics>()) && !spawnInfo.Player.ZoneDungeon ? 0.5f : 0f;
    }
}
public class TropicalSlimeGrassy : AmberSlime
{
}

public class TropicalSlimeShroomy : AmberSlime
{
}
