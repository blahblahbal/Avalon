using System.Collections.Generic;
using Avalon.Common;
using Avalon.Items.Accessories.Info;
using Avalon.Items.Material;
using Avalon.Items.Material.TomeMats;
using Avalon.Items.Placeable.Crafting;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.TownNPCs;

[AutoloadHead]
public class Librarian : ModNPC
{
    private static int ShimmerHeadIndex;
    private static Profiles.StackedNPCProfile NPCProfile;
    public override void Load()
    {
        // Adds our Shimmer Head to the NPCHeadLoader.
        ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, Texture + "_Shimmer_Head");
    }
    public string ShopName = Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Shop");
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 25;
        NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
        NPCID.Sets.AttackFrameCount[NPC.type] = 4;
        NPCID.Sets.DangerDetectRange[NPC.type] = 600;
        NPCID.Sets.AttackType[NPC.type] = 1;
        NPCID.Sets.AttackTime[NPC.type] = 50;
        NPCID.Sets.AttackAverageChance[NPC.type] = 10;
        NPCID.Sets.ShimmerTownTransform[NPC.type] = true;
        NPCID.Sets.ShimmerTownTransform[Type] = true;


        NPC.Happiness
            .SetBiomeAffection<ForestBiome>(AffectionLevel.Love)
            .SetBiomeAffection<JungleBiome>(AffectionLevel.Like)
            .SetBiomeAffection<HallowBiome>(AffectionLevel.Dislike)
            .SetNPCAffection(NPCID.DyeTrader, AffectionLevel.Love)
            .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
            .SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Hate);

        NPCProfile = new Profiles.StackedNPCProfile(
                new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture)),
                new Profiles.DefaultNPCProfile(Texture + "_Shimmer", ShimmerHeadIndex)
            );

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()

        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }
    public override ITownNPCProfile TownNPCProfile()
    {
        return NPCProfile;
    }

    public override void SetDefaults()
    {
        NPC.damage = 10;
        NPC.lifeMax = 250;
        NPC.townNPC = true;
        NPC.defense = 15;
        NPC.friendly = true;
        NPC.width = 18;
        NPC.aiStyle = 7;
        NPC.scale = 1f;
        NPC.height = 40;
        NPC.knockBackResist = 0.5f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        AnimationType = 22;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface, new FlavorTextBestiaryInfoElement(
                "Mods.Avalon.NPCs.Librarian.Bestiary")
        });
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            for (int numDust = 0; (double)numDust < NPC.life / (double)NPC.lifeMax * 100.0; numDust++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f);
            }
            return;
        }
        // Create gore when the NPC is killed.
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            // Retrieve the gore types. This NPC has shimmer and party variants for head, arm, and leg gore. (12 total gores)
            string variant = "";
            if (NPC.IsShimmerVariant) variant += "_Shimmer";
            int hatGore = NPC.GetPartyHatGore();
            int headGore = Mod.Find<ModGore>($"{Name}_Head{variant}").Type;
            int armGore = Mod.Find<ModGore>($"{Name}_Arm{variant}").Type;
            int legGore = Mod.Find<ModGore>($"{Name}_Leg{variant}").Type;

            // Spawn the gores. The positions of the arms and legs are lowered for a more natural look.
            if (hatGore > 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, hatGore);
            }
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, headGore, 1f);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
            for (int numDust = 0; numDust < 50; numDust++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * (float)hit.HitDirection, -2.5f);
            }
        }
    }

    // can't really localize these, since they're meant to be references to names of people I play PWI with
    public override List<string> SetNPCNameList()
    {
        return new List<string>
        {
            "Juisefuss",
            "Rob",
            "Nasard",
            "Helafrin",
            "Heleghir",
            "Kevin",
            "Baysh",
            "Mars",
            "Dato",
            "Callumn",
            "Alkaido",
            "Harry",
            "Flish",
            "Ryugrei",
            "Krayvuss",
            "Halo"
        };
    }

    public override string GetChat()
    {
        if (!Main.dayTime && Main.hardMode && Main.rand.NextBool(5))
        {
            return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Second");
        }

        if (NPC.AnyNPCs(NPCID.DyeTrader) && Main.rand.NextBool(6))
        {
            return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Second", Main.npc[AvalonGlobalNPC.FindATypeOfNPC(NPCID.DyeTrader)].GivenName);
        }

        switch (Main.rand.Next(11))
        {
            case 0:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Third");
            case 1:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Fourth");
            case 2:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Fifth");
            case 3:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Sixth");
            case 4:
                return "買點東西，什麼都買。 " + Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Seventh");
            case 5:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Eighth");
            case 6:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Ninth");
            case 7:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Tenth");
            case 8:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Eleventh");
            case 9:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Twelfth");
            case 10:
                return Language.GetTextValue("Mods.Avalon.NPCs.Librarian.Dialogue.Thirteenth"); // add genie pets
        }

        return "";
    }

    public override void SetChatButtons(ref string button, ref string button2)
    {
        button = Language.GetTextValue("LegacyInterface.28");
    }

    public override bool CanTownNPCSpawn(int numTownNPCs)
    {
        return NPC.downedBoss1;
    }

    public override void OnChatButtonClicked(bool firstButton, ref string shop)
    {
        if (firstButton)
        {
            shop = ShopName;
        }
    }
    public override void AddShops()
    {
        var shop = new NPCShop(Type, "Shop");
        // always sell this
        shop.Add(new Item(ModContent.ItemType<TomeForge>())
        {
            shopCustomPrice = 75000
        });
        shop.Add(new Item(ItemID.Book));
        // in hardmode
        shop.Add(new Item(ModContent.ItemType<MysticalTomePage>())
        {
            shopCustomPrice = 20000
        }, Condition.Hardmode);
        // downed eye
        shop.Add(new Item(ModContent.ItemType<MysticalClaw>())
        {
            shopCustomPrice = 25000
        }, Condition.DownedEyeOfCthulhu);
        shop.Add(new Item(ModContent.ItemType<RubybeadHerb>())
        {
            shopCustomPrice = 25000
        }, Condition.DownedEyeOfCthulhu);
        // in jungle
        shop.Add(new Item(ModContent.ItemType<StrongVenom>())
        {
            shopCustomPrice = 25000
        }, Condition.InJungle);
        // downed skeletron
        shop.Add(new Item(ModContent.ItemType<ElementDust>())
        {
            shopCustomPrice = 25000
        }, Condition.DownedSkeletron);
        shop.Add(new Item(ModContent.ItemType<DewOrb>())
        {
            shopCustomPrice = 25000
        }, Condition.DownedSkeletron);

        // craftable tome mats
        shop.Add(new Item(ModContent.ItemType<CarbonSteel>())
        {
            shopCustomPrice = 2000
        }, Condition.Hardmode);
        shop.Add(new Item(ModContent.ItemType<FineLumber>())
        {
            shopCustomPrice = 2000
        }, Condition.Hardmode);
        shop.Add(new Item(ModContent.ItemType<Gravel>())
        {
            shopCustomPrice = 2000
        }, Condition.Hardmode);
        shop.Add(new Item(ModContent.ItemType<Sandstone>())
        {
            shopCustomPrice = 2000
        }, Condition.Hardmode);

        // downed all 3 mech bosses and hardmode
        shop.Add(new Item(ModContent.ItemType<MysticalTotem>())
        {
            shopCustomPrice = 70000
        }, Condition.DownedMechBossAll, Condition.Hardmode);

        // downed plantera
        shop.Add(new Item(ModContent.ItemType<ElementDiamond>())
        {
            shopCustomPrice = 35000
        }, Condition.DownedPlantera, Condition.Hardmode);
        shop.Add(new Item(ModContent.ItemType<DewofHerbs>())
        {
            shopCustomPrice = 35000
        }, Condition.DownedPlantera, Condition.Hardmode);

        shop.Add(new Item(ModContent.ItemType<EyeoftheGods>())
        {
            shopCustomPrice = 100000
        }, Condition.DownedCultist, Condition.Hardmode);

        shop.Register();
    }

    public override void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
        damage = 22;
        knockback = 2f;
    }

    public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
    {
        cooldown = 30;
        randExtraCooldown = 30;
    }

    public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
    {
        projType = ProjectileID.WaterBolt;
        attackDelay = 1;
    }

    public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection,
                                                ref float randomOffset)
    {
        multiplier = 12f;
        randomOffset = 2f;
    }
}
