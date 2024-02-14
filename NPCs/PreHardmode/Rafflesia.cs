using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Avalon.Common.Players;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Avalon.NPCs.PreHardmode;

public class Rafflesia : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 4;
    }

    public override void SetDefaults()
    {
        NPC.damage = 31;
        NPC.lifeMax = 160;
        NPC.defense = 7;
        NPC.width = 54;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.value = 110f;
        NPC.height = 30;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0f;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.RafflesiaBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Tropics>().Type };
        //DrawOffsetY = 10;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Rafflesia")),
        });
    public override void ModifyNPCLoot(NPCLoot loot)
    {
        loot.Add(ItemDropRule.Common(ModContent.ItemType<Root>(), 2, 1, 2));
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneTropics || spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneUndergroundTropics)
        {
            if (Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY + 2].TileType == ModContent.TileType<Tiles.Tropics.TropicalGrass>())
                //&&
                //!Main.tile[spawnInfo.SpawnTileX + 1, spawnInfo.SpawnTileY].HasTile && !Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].HasTile &&
                //!Main.tile[spawnInfo.SpawnTileX - 1, spawnInfo.SpawnTileY].HasTile)
            {
                return 0.7f;
            }
        }
        return 0;
    }
    //public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    //{
    //    NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
    //    NPC.damage = (int)(NPC.damage * 0.65f);
    //}
    public override void AI()
    {
        if (Main.rand.NextBool(15))
        {
            int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.MosquitoDust>(), Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 0, Color.White);
            Main.dust[dust].noGravity = true;
        }
        if (Main.rand.NextBool(45))
        {
            int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.MosquitoDustImmortal>(), Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 0, Color.White);
            Main.dust[dust].noGravity = true;
        }

        NPC.ai[0]++;
        if (NPC.ai[0] >= 168) // 240
        {
            NPC.ai[1] = 1;

        }
        if (NPC.ai[1] == 1)
        {
            NPC.ai[2]++; // += 2
            int type = ModContent.NPCType<FlySmall>();
            if (Main.rand.NextBool(3))
                type = ModContent.NPCType<Fly>();
            if (NPC.ai[2] is 18 or 48 or 78)
            {
                int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.position.Y + 8, type, Target: NPC.target);
                Main.npc[n].velocity.Y = -2f;
            }
            if (NPC.ai[2] == 90) // 188
            {
                NPC.ai[2] = 0;
                NPC.ai[0] = 0;
                NPC.ai[1] = 0;
                return;
            }
        }
    }

    public override void FindFrame(int frameHeight)
    {
        int firstStart = 45;

        if (NPC.ai[1] == 0)
        {
            NPC.frameCounter++;
            // start, slower
            if (NPC.ai[0] < 96)
            {
                if (NPC.frameCounter < 12)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.frameCounter < 24)
                {
                    NPC.frame.Y = frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            // faster
            else
            {
                if (NPC.frameCounter < 6)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.frameCounter < 12)
                {
                    NPC.frame.Y = frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
        }
        else if (NPC.ai[1] == 1)
        {
            //if (NPC.ai[2] is < 40 or >= 60 and < 100 or >= 120 and < 160 ) // squish frame
            //{
            //    NPC.frame.Y = frameHeight;
            //}
            //else if (NPC.ai[2] is >= 40 and < 50 or >= 100 and < 110 or >= 160 and < 170)
            //{
            //    NPC.frame.Y = frameHeight * 2;
            //}
            //else if (NPC.ai[2] is >= 50 and < 60 or >= 110 and < 120 or >= 170 and < 180)
            //{
            //    NPC.frame.Y = frameHeight * 3;
            //}

            if (NPC.ai[2] is < 9 or >= 30 and < 39 or >= 60 and < 69) // squish frame
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.ai[2] is >= 9 and < 18 or >= 39 and < 48 or >= 69 and < 78)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else if (NPC.ai[2] is >= 18 and < 30 or >= 48 and < 60 or >= 78 and < 90)
            {
                NPC.frame.Y = frameHeight * 3;
            }
        }
    }
}
