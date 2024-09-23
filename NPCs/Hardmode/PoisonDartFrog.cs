using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class PoisonDartFrog : ModNPC
{
    private const int AISlotFrame = 0;
    private const int AISlotInJump = 2;
    private const int AISlotTimer = 1;

    public float AIFrame
    {
        get => NPC.ai[AISlotFrame];
        set => NPC.ai[AISlotFrame] = value;
    }

    public float AITimer
    {
        get => NPC.ai[AISlotTimer];
        set => NPC.ai[AISlotTimer] = value;
    }

    public bool AIInJump
    {
        get => NPC.ai[AISlotInJump] == 1;
        set => NPC.ai[AISlotInJump] = value ? 1 : 0;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.PoisonDartFrog")),
        });
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 3;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.CursedInferno] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
		var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2(6f, 4f),
			PortraitPositionXOverride = 0,
			PortraitPositionYOverride = 0,
			Frame = 0
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
    }

    public override void SetDefaults()
	{
		NPC.damage = 65;
        NPC.scale = 1f;
        NPC.lifeMax = 310;
        NPC.defense = 24;
        NPC.width = 36;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.value = 1000f;
        NPC.timeLeft = 750;
        NPC.height = 36;
        NPC.knockBackResist = 0.2f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        AIInJump = true;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<PoisonDartFrogBanner>();
		SpawnModBiomes = [ModContent.GetInstance<Biomes.Tropics>().Type];
	}

    public override void AI()
    {
        AITimer++;
        NPC.TargetClosest();
        Player player = Main.player[NPC.target];
        NPC.spriteDirection = NPC.direction;

        if (AITimer == 1)
        {
            AIFrame = 0;
        }

        // Ready to jump
        if (AITimer >= 100)
        {
            int jumpType = Main.rand.Next(2);

            float speedY = jumpType == 0 ? -5f : -8f;
            float speedX = 12f;

            if (Vector2.Distance(NPC.position, Main.player[NPC.target].position) < 16 * 10)
            {
                speedY = jumpType == 0 ? -7f : -10f;
                speedX = 5f;
            }

            if (NPC.collideX)
            {
                speedY *= 2;
            }

            AIFrame = 1;
            NPC.velocity.Y = speedY;
            NPC.velocity.X += speedX * NPC.direction;
            AITimer = 0;
            AIInJump = true;
        }

        if (AIInJump && NPC.velocity.X == 0)
        {
            float speedX = 12f;
            if (Vector2.Distance(NPC.position, Main.player[NPC.target].position) < 16 * 10)
            {
                speedX = 5f;
            }

            NPC.velocity.X += speedX * NPC.direction;
        }

        if (NPC.collideY && NPC.velocity.Y >= 0 && Logic.Collision.TouchingTile(NPC.position, NPC.width, NPC.height))
        {
            NPC.velocity.X *= 0.7f;
            if (NPC.velocity.X > -0.5 && NPC.velocity.X < 0.5)
            {
                NPC.velocity.X = 0f;
                //AIFrame = 0;
            }

            AIInJump = false;
        }
    }

    //public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    //{
    //    NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * bossLifeScale);
    //    NPC.damage = (int)(NPC.damage * 0.8f);
    //}

    public override void FindFrame(int frameHeight)
    {
        if (NPC.collideY && NPC.velocity.Y >= 0 && Logic.Collision.TouchingTile(NPC.position, NPC.width, NPC.height))
        {
            NPC.frame.Y = 0;
        }
        else
        {
            if (AIFrame == 0)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else
            {
                NPC.frame.Y = frameHeight * 3;
            }
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneTropics && !spawnInfo.Player.InPillarZone() &&
        Main.hardMode ? 0.083f : 0f;

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("FrogGore1").Type);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("FrogGore2").Type);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("FrogGore2").Type);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("FrogGore3").Type);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("FrogGore3").Type);
        }
    }
}
