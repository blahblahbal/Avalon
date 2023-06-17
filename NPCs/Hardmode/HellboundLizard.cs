using Terraria.GameContent.Bestiary;
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Avalon.NPCs.Template;
using Avalon.Common.Players;

namespace Avalon.NPCs.Hardmode;

public class HellboundLizard : CustomFighterAI
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 16;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new int[]
            {
                BuffID.Confused,
                BuffID.OnFire
            }
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }
    public override float MaxMoveSpeed { get; set; } = 3f;
    public override float Acceleration { get; set; } = 0.25f;
    public override float AirAcceleration { get; set; } = 0.15f;
    public override float MaxAirSpeed { get; set; } = 3.5f;
    public override float JumpRadius { get; set; } = 225;
    public override float MaxJumpHeight { get; set; } = 9f;
    public override void SetDefaults()
    {
        NPC.damage = 128;
        NPC.lifeMax = 3800;
        NPC.defense = 60;
        NPC.lavaImmune = true;
        NPC.width = 18;
        NPC.aiStyle = -1;
        NPC.value = 1000f;
        NPC.height = 40;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.HellboundLizardBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Hellcastle>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("Similar in appearance to Lihzahrds, they run about in the Hellcastle, seemingly without purpose.")
        });
    }
    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        //NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.85f);
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return lightColor;
    }
    public override void FindFrame(int frameHeight)
    {
        if (NPC.velocity.Y == 0f)
        {
            if (NPC.direction == 1)
            {
                NPC.spriteDirection = 1;
            }
            if (NPC.direction == -1)
            {
                NPC.spriteDirection = -1;
            }
            if (NPC.velocity.X == 0f)
            {
                NPC.frame.Y = 0;
                NPC.frameCounter = 0.0;
            }
            else
            {
                NPC.frameCounter += Math.Abs(NPC.velocity.X) * 2f;
                NPC.frameCounter += 1.0;
                if (NPC.frameCounter > 10.0)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y / frameHeight >= Main.npcFrameCount[NPC.type])
                {
                    NPC.frame.Y = frameHeight * 2;
                }
            }
        }
        else
        {
            NPC.frameCounter = 0.0;
            NPC.frame.Y = frameHeight;
        }
    }
    public override void CustomBehavior()
    {

    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneHellcastle && Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].WallType == (ushort)ModContent.WallType<Walls.ImperviousBrickWallUnsafe>())
        {
            return 3f;
        }
        return 0f;
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        for (int i = 0; i < 20; i++)
        {
            int num890 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 0f, 0f, 0, default(Color), 1f);
            Main.dust[num890].velocity *= 5f;
            Main.dust[num890].scale = 1.2f;
            Main.dust[num890].noGravity = true;
        }
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore1").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore2").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore2").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore3").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore3").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore4").Type, 1f);
        }
    }
}
