using System;
using Avalon.Common;
using Avalon.Items.Banners;
using Avalon.Items.Material;
using Avalon.Projectiles.Hostile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Avalon.NPCs.PreHardmode;

public class BoneFish : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 6;
        Data.Sets.NPC.Undead[NPC.type] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
    }

    public override void SetDefaults()
    {
        NPC.damage = 40;
        NPC.lifeMax = 100;
        NPC.timeLeft = 750;
        NPC.defense = 15;
        NPC.width = 34;
        NPC.aiStyle = 16;
        NPC.value = 0;
        AnimationType = NPCID.Piranha;
        NPC.height = 22;
        NPC.knockBackResist = 0.4f;
        NPC.lavaImmune = true;
        NPC.noGravity = true;
        NPC.HitSound = SoundID.NPCHit2;
        NPC.DeathSound = SoundID.NPCDeath2;
        NPC.buffImmune[BuffID.Confused] = true;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<BoneFishBanner>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.BoneFish")),
        });

    //public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    //{
    //    NPC.lifeMax = (int)(NPC.lifeMax * 0.65f);
    //    NPC.damage = (int)(NPC.damage * 0.6f);
    //}
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("BoneFishHead").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("BoneFishTail").Type);
            }
        }
    }
}
