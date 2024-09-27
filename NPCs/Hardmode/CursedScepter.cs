using Avalon.Common;
using Avalon.Items.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class CursedScepter : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 6;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2(-9f, 18f),
			PortraitPositionXOverride = -4f,
			PortraitPositionYOverride = -8f,
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
	}

    public override void SetDefaults()
	{
		NPC.damage = 61;
        NPC.lifeMax = 226;
        NPC.defense = 18;
        NPC.lavaImmune = true;
        NPC.Size = new Vector2(32, 32);
        NPC.aiStyle = 23;
        NPC.value = 1000f;
        NPC.scale = 1.1f;
        NPC.knockBackResist = 0.3f;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath6;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<CursedScepterBanner>();
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        if (Main.rand.NextBool(4))
        {
            target.AddBuff(BuffID.Cursed, 60 * 5);
        }
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.CursedScepter")),
        });

    public override Color? GetAlpha(Color drawColor)
    {
        return Color.White;
    }

    public override void FindFrame(int frameHeight)
    {
        if (NPC.ai[0] == 2f)
        {
            NPC.frameCounter = 0.0;
            NPC.frame.Y = 0;
        }
        else
        {
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter >= 4.0)
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                if (NPC.frame.Y / frameHeight >= Main.npcFrameCount[NPC.type])
                {
                    NPC.frame.Y = 0;
                }
            }
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) => Main.hardMode && spawnInfo.Player.ZoneDungeon
        ? 0.1f : 0f;

    public override void ModifyNPCLoot(NPCLoot npcLoot) => npcLoot.Add(ItemDropRule.Common(ItemID.Nazar, 75));
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            int num161 = 0;
            while ((double)num161 < hit.Damage / (double)NPC.lifeMax * 50.0)
            {
                int num162 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 0, default, 1.5f);
                Main.dust[num162].noGravity = true;
                num161++;
            }
        }
        else if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            for (int num163 = 0; num163 < 20; num163++)
            {
                int num164 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 0, default, 1.5f);
                Main.dust[num164].velocity *= 2f;
                Main.dust[num164].noGravity = true;
            }
            int num165 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y + NPC.height / 2 - 10f), new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), 61, NPC.scale);
            Main.gore[num165].velocity *= 0.5f;
            num165 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y + NPC.height / 2 - 10f), new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), 61, NPC.scale);
            Main.gore[num165].velocity *= 0.5f;
            num165 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y + NPC.height / 2 - 10f), new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), 61, NPC.scale);
            Main.gore[num165].velocity *= 0.5f;
        }
    }
}
