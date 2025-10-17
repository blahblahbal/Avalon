using System;
using Avalon.Common;
using Avalon.Items.Banners;
using Avalon.Items.Vanity;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Melee.PreHardmode.MinersSword;

namespace Avalon.NPCs.PreHardmode;

public class FallenHero : ModNPC
{
    public override void SetStaticDefaults()
    {
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()

        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        Main.npcFrameCount[NPC.type] = 3;
        Data.Sets.NPCSets.Undead[NPC.type] = true;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.FallenHero")),
        });

    public override void SetDefaults()
    {
        NPC.damage = 30;
        NPC.lifeMax = 180;
        NPC.defense = 6;
        NPC.width = 18;
        NPC.aiStyle = 3;
        NPC.value = 10000f;
        NPC.height = 40;
        NPC.knockBackResist = 0.5f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath2;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<FallenHeroBanner>();
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
        }

        if (NPC.velocity.Y != 0f || NPC.direction == -1 && NPC.velocity.X > 0f ||
            NPC.direction == 1 && NPC.velocity.X < 0f)
        {
            NPC.frameCounter = 0.0;
            NPC.frame.Y = frameHeight * 2;
        }
        else if (NPC.velocity.X == 0f)
        {
            NPC.frameCounter = 0.0;
            NPC.frame.Y = 0;
        }
        else
        {
            NPC.frameCounter += Math.Abs(NPC.velocity.X);
            if (NPC.frameCounter < 8.0)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.frameCounter < 16.0)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.frameCounter < 24.0)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else if (NPC.frameCounter < 32.0)
            {
                NPC.frame.Y = frameHeight;
            }
            else
            {
                NPC.frameCounter = 0.0;
            }
        }
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.OneFromOptions(30, ModContent.ItemType<BloodstainedHelmet>(),
            ModContent.ItemType<BloodstainedChestplate>(), ModContent.ItemType<BloodstainedGreaves>()));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinersSword>(), 20));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RottenFlesh>(), 3));
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("FallenHeroGore1").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("FallenHeroGore2").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("FallenHeroGore2").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("FallenHeroGore3").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("FallenHeroGore3").Type, 1f);

            for (int i = 0; i < 30; i++)
            {
                Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood);
                d.velocity += NPC.velocity.RotatedByRandom(1);
                d.velocity *= 1.6f;
                d.scale = 2;
                d.noGravity = Main.rand.NextBool(3);
                d.fadeIn = Main.rand.NextFloat(1);
            }
        }
        for (int i = 0; i < hit.Damage; i++)
        {
            Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood);
            d.velocity += NPC.velocity.RotatedByRandom(1);
            d.noGravity = Main.rand.NextBool(3);
            d.fadeIn = Main.rand.NextFloat(1);
            d.velocity.X += hit.HitDirection;
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.ZoneOverworldHeight && !spawnInfo.Player.InPillarZone() && Main.bloodMoon
            ? 0.12f : 0f;
}
