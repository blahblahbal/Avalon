using System;
using Avalon.Common;
using Avalon.Items.Banners;
using Avalon.Items.Material;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class IrateBones : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 15;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		Data.Sets.NPC.Undead[NPC.type] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
	}

    public override void SetDefaults()
    {
        NPC.damage = 35;
        NPC.lifeMax = 350;
        NPC.defense = 15;
        NPC.width = 18;
        NPC.aiStyle = 3;
		AIType = NPCID.AngryBones;
		AnimationType = NPCID.AngryBones;
		NPC.value = 1000f;
        NPC.height = 40;
        NPC.knockBackResist = 0.5f;
        NPC.HitSound = SoundID.NPCHit2;
        NPC.DeathSound = SoundID.NPCDeath2;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<IrateBonesBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.IrateBones")),
        });

    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.7f);
        NPC.damage = (int)(NPC.damage * 0.5f);
    }

	public override void PostAI()
	{
		if (NPC.velocity.Y == 0 && (NPC.velocity.X > 1f || NPC.velocity.X < 1f))
		{
			NPC.velocity.X = NPC.direction;
		}
	}

    public override float SpawnChance(NPCSpawnInfo spawnInfo) => Main.hardMode && spawnInfo.Player.ZoneDungeon
        ? 0.6f : 0f;

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            //Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
            //    Mod.Find<ModGore>("IrateBonesHelmet").Type);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 42);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 43);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 43);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 44);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 44);
        }
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.Common(ItemID.BoneWand, 250))
			.OnFailedRoll(ItemDropRule.Common(ItemID.TallyCounter, 100))
			.OnFailedRoll(ItemDropRule.Common(ItemID.GoldenKey, 65))
			.OnFailedRoll(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Bone, 1, 1, 3));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ItemID.Bone, 1, 2, 6));
	}
}
