using System;
using Avalon.Items.Material;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.Enums;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Avalon.NPCs.PreHardmode.Fly;

public class Fly : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 4;
    }

    public override void SetDefaults()
    {
        NPC.damage = 20;
        NPC.lifeMax = 20;
        NPC.defense = 5;
        NPC.noGravity = true;
        NPC.width = 16;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.height = 18;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        AnimationType = NPCID.Bee;
        NPC.knockBackResist = 0.01f;
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.UndergroundTropics>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Fly"))
        });
    }

    public override void AI()
    {
        if (NPC.target < 0 || NPC.target <= 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest();
        }
        NPCAimedTarget targetData = NPC.GetTargetData();
        bool flag = false;
        if (targetData.Type == NPCTargetType.Player)
        {
            flag = Main.player[NPC.target].dead;
        }
        float num = 6f;
        float num11 = 0.05f;
        Vector2 vector = NPC.Center;
        float num17 = targetData.Position.X + targetData.Width / 2;
        float num18 = targetData.Position.Y + targetData.Height / 2;
        num17 = (int)(num17 / 8f) * 8;
        num18 = (int)(num18 / 8f) * 8;
        vector.X = (int)(vector.X / 8f) * 8;
        vector.Y = (int)(vector.Y / 8f) * 8;
        num17 -= vector.X;
        num18 -= vector.Y;
        float num19 = (float)Math.Sqrt(num17 * num17 + num18 * num18);
        float num20 = num19;
        bool flag2 = false;
        if (num19 > 600f)
        {
            flag2 = true;
        }
        if (num19 == 0f)
        {
            num17 = NPC.velocity.X;
            num18 = NPC.velocity.Y;
        }
        else
        {
            num19 = num / num19;
            num17 *= num19;
            num18 *= num19;
        }
        if (NPC.velocity.X < num17)
        {
            NPC.velocity.X += num11;
            if (NPC.velocity.X < 0f && num17 > 0f)
            {
                NPC.velocity.X += num11;
            }
        }
        else if (NPC.velocity.X > num17)
        {
            NPC.velocity.X -= num11;
            if (NPC.velocity.X > 0f && num17 < 0f)
            {
                NPC.velocity.X -= num11;
            }
        }
        if (NPC.velocity.Y < num18)
        {
            NPC.velocity.Y += num11;
            if (NPC.velocity.Y < 0f && num18 > 0f)
            {
                NPC.velocity.Y += num11;
            }
        }
        else if (NPC.velocity.Y > num18)
        {
            NPC.velocity.Y -= num11;
            if (NPC.velocity.Y > 0f && num18 < 0f)
            {
                NPC.velocity.Y -= num11;
            }
        }
        NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) - 1.57f;
        if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
        {
            NPC.netUpdate = true;
        }
    }
    //public override float SpawnChance(NPCSpawnInfo spawnInfo)
    //{
    //    if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && spawnInfo.Player.ZoneOverworldHeight && !spawnInfo.Player.InPillarZone())
    //        return 1;
    //    return 0;
    //}
    //public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    //{
    //    NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
    //    NPC.damage = (int)(NPC.damage * 0.65f);
    //}

    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter++;
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

    //public override void HitEffect(int hitDirection, double damage)
    //{
    //    if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
    //    {
    //        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("Bactus").Type, 1f);
    //    }
    //}
}
