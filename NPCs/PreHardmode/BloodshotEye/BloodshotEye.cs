using System;
using Avalon.Common;
using Avalon.Items.Banners;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.IO;
using Avalon;

namespace Avalon.NPCs.PreHardmode.BloodshotEye;

public class BloodshotEye : ModNPC
{
    int timer;
    public override void SetStaticDefaults()
    {
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()

        {
            // Influences how the NPC looks in the Bestiary
            PortraitPositionYOverride = -30,
            Position = new Vector2(6f, -10f)
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        Main.npcFrameCount[NPC.type] = 3;
        Data.Sets.NPCSets.Flyer[NPC.type] = true;
    }

    public override void SetDefaults()
    {
        NPC.damage = 25;
        NPC.lifeMax = 110;
        NPC.defense = 5;
        NPC.width = 48;
        NPC.aiStyle = 2;
        NPC.value = 150f;
        NPC.height = 34;
        NPC.knockBackResist = 0.4f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath6;
        NPC.buffImmune[BuffID.Confused] = true;
        AIType = NPCID.DemonEye;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<BloodshotEyeBanner>();
    }
    public override void AI()
    {
        base.AI();
        if (!NPC.HasPlayerTarget)
        {
            NPC.ai[2] = 0;
            NPC.ai[3] = 0;
            NPC.aiStyle = 2;
        }
        NPC.ai[2]++;
        if (NPC.ai[2] >= 320)
        {
            //NPC.aiStyle = -1;
            NPC.velocity *= 0.95f;
        }
        if (NPC.ai[2] is > 360 and < 400)
        {
            NPC.rotation = NPC.Center.AngleTo(Main.player[NPC.target].Center);
            NPC.ai[3] += 0.1f;
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 6), 0, default, Main.rand.NextFloat(1, 1.3f));
        }
        if (NPC.ai[2] == 400)
        {
            NPC.ai[2] = timer * 10;
            NPC.ai[3] = 0;
            NPC.aiStyle = 2;
            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(Main.rand.NextFloat(7, 9), 0).RotatedBy(NPC.Center.AngleTo(Main.player[NPC.target].Center)), ModContent.ProjectileType<BloodshotShot>(), 18, 0, 255);
            for (int i = 0; i < 2; i++)
            {
                int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(Main.rand.NextFloat(4, 7), 0).RotatedBy(NPC.Center.AngleTo(Main.player[NPC.target].Center)).RotateRandom(MathHelper.Pi / 32), ModContent.ProjectileType<BloodshotShot>(), 18, 0, 255);
                Main.projectile[p].Size = new Vector2(8);
            }
            SoundEngine.PlaySound(SoundID.Item17, NPC.Center);
            NPC.velocity = new Vector2(-5, 0).RotatedBy(NPC.rotation);
            if (timer < 30)
            {
                timer += 2;
            }
        }
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(timer);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        timer = reader.ReadInt32();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.BloodshotEye")),
        });

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(new CommonDrop(ModContent.ItemType<BloodshotLens>(), 5, 1, 1, 3));
        npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 33));
    }

    public override void FindFrame(int frameHeight)
    {
        if (NPC.velocity.X > 0f)
        {
            NPC.spriteDirection = 1;
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
        }

        if (NPC.velocity.X < 0f)
        {
            NPC.spriteDirection = -1;
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 3.14f;
        }

        NPC.frameCounter += 1.0;
        if (NPC.frameCounter >= 8.0)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0.0;
        }

        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0)
        {
            for (int i = 0; i < 35; i++)
            {
                Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 1), 0, default, Main.rand.NextFloat(1.7f, 2.3f));
                d.velocity += NPC.velocity * 1f;
            }
            if (Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("BloodshotEye1").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("BloodshotEye2").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("BloodshotEye3").Type);
            }
        }
        else
        {
            for (int i = 0; i < (int)hit.Damage / 3; i++)
            {
                Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 1), 0, default, Main.rand.NextFloat(1f, 1.2f));
                d.velocity += NPC.velocity * 1f;
            }
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (Main.bloodMoon && !spawnInfo.Player.InPillarZone() && spawnInfo.Player.ZoneOverworldHeight)
        {
            if (Main.bloodMoon && !NPC.downedBoss1 && !NPC.downedBoss2 && !NPC.downedBoss3)
            {
                return 0.08f;
            }
            else
            {
                return 0.15f;
            }
        }
        return 0f;
    }

}
