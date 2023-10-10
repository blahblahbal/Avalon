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
    public const string ShopName = "Shop";
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

        var drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0) {Velocity = 1f};

        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

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
                "The Librarian fled his homeland when mysterious otherwordly beings invaded. He seems to be much happier here.")
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
        if (!Main.dayTime && Main.hardMode && Main.rand.Next(5) == 0)
        {
            return "My home is fraught with Wraiths. I'm deathly afraid of them; please keep them away.";
        }

        if (NPC.AnyNPCs(NPCID.DyeTrader) && Main.rand.Next(6) == 0)
        {
            return "Wow, " + Main.npc[AvalonGlobalNPC.FindATypeOfNPC(NPCID.DyeTrader)].GivenName +
                   "'s services are free? Where I'm from, you have to pay an arm and a leg to dye clothes...";
        }

        switch (Main.rand.Next(11))
        {
            case 0:
                return
                    "I come from a distant land, one where there are many races. It doesn't appear that you are any of those races, however.";
            case 1:
                return
                    "This area is quite different from what I'm used to. People where I'm from spend loads to get virtual clothes.";
            case 2:
                return
                    "...okay. I've got your grade seventeen fifth cast weapon materials ready. *click* Sorry, did you need something?";
            case 3:
                return "How do you people deal with being two-dimensional?";
            case 4:
                return "買點東西，什麼都買。 Oops, sorry about that. I was just speaking to a friend of mine from home.";
            case 5:
                return
                    "Psionic Infusion, Spark, Chant of Chi, Tide Spirit, Sandburst Blast, Stone Smasher, macro. What? Just setting up my strategy for this nuke.";
            case 6:
                return "Pan Gu is the creator of my land. Who created yours? ... Redigit? Never heard of them.";
            case 7:
                return "You should consider the Tome Forge. It's my own invention!";
            case 8:
                return
                    "When the Changelings took over the Western Steppes, I fled to this land. I already like it better.";
            case 9:
                return "Wait, you get loot after defeating a boss more than once per day?!";
            case 10:
                return
                    "It's very strange to not see fairies flying around everyone. Why don't you buy one from me?"; // add genie pets
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
    //public override void SetupShop(Chest shop, ref int nextSlot)
    //{
    //    if (ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode)
    //    {
    //        shop.item[nextSlot].SetDefaults(ModContent.ItemType<ScrollofTome>());
    //        shop.item[nextSlot].value = Item.buyPrice(0, 12, 50);
    //        nextSlot++;
    //        if (ModContent.GetInstance<DownedBossSystem>().DownedDragonLord)
    //        {
    //            shop.item[nextSlot].SetDefaults(ModContent.ItemType<DragonOrb>());
    //            shop.item[nextSlot].value = Item.buyPrice(0, 30);
    //            nextSlot++;
    //        }
    //    }
    //}

    /*public override void AI()
    {
        var flag22 = Main.raining;
        if (!Main.dayTime)
        {
            flag22 = true;
        }
        if (Main.eclipse)
        {
            flag22 = true;
        }
        var num216 = (int)(NPC.position.X + NPC.width / 2) / 16;
        var num217 = (int)(NPC.position.Y + NPC.height + 1f) / 16;
        if (NPC.homeTileX == -1 && NPC.homeTileY == -1 && NPC.velocity.Y == 0f)
        {
            NPC.homeTileX = (int)NPC.Center.X / 16;
            NPC.homeTileY = (int)(NPC.position.Y + NPC.height + 4f) / 16;
        }
        var flag23 = false;
        NPC.directionY = -1;
        if (NPC.direction == 0)
        {
            NPC.direction = 1;
        }
        for (var num218 = 0; num218 < 255; num218++)
        {
            if (Main.player[num218].active && Main.player[num218].talkNPC == NPC.whoAmI)
            {
                flag23 = true;
                if (NPC.ai[0] != 0f)
                {
                    NPC.netUpdate = true;
                }
                NPC.ai[0] = 0f;
                NPC.ai[1] = 300f;
                NPC.ai[2] = 100f;
                if (Main.player[num218].position.X + Main.player[num218].width / 2 < NPC.position.X + NPC.width / 2)
                {
                    NPC.direction = -1;
                }
                else
                {
                    NPC.direction = 1;
                }
            }
        }
        if (NPC.ai[3] > 0f)
        {
            NPC.life = -1;
            NPC.HitEffect(0, 10.0);
            NPC.active = false;
        }
        var num219 = NPC.homeTileY;
        if (Main.netMode != NetmodeID.MultiplayerClient && NPC.homeTileY > 0)
        {
            while (!WorldGen.SolidTile(NPC.homeTileX, num219) && num219 < Main.maxTilesY - 20)
            {
                num219++;
            }
        }
        if (Main.tile[num216, num217] == null)
        {
            return;
        }
        if (Main.netMode != NetmodeID.MultiplayerClient && NPC.townNPC && (flag22 || Main.tileDungeon[Main.tile[num216, num217].TileType]) && (num216 != NPC.homeTileX || num217 != num219) && !NPC.homeless)
        {
            var flag24 = true;
            for (var num220 = 0; num220 < 2; num220++)
            {
                var rectangle3 = new Rectangle((int)(NPC.position.X + NPC.width / 2 - NPC.sWidth / 2 - NPC.safeRangeX), (int)(NPC.position.Y + NPC.height / 2 - NPC.sHeight / 2 - NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                if (num220 == 1)
                {
                    rectangle3 = new Rectangle(NPC.homeTileX * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, num219 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                }
                for (var num221 = 0; num221 < 255; num221++)
                {
                    if (Main.player[num221].active)
                    {
                        var rectangle4 = new Rectangle((int)Main.player[num221].position.X, (int)Main.player[num221].position.Y, Main.player[num221].width, Main.player[num221].height);
                        if (rectangle4.Intersects(rectangle3))
                        {
                            flag24 = false;
                            break;
                        }
                    }
                    if (!flag24)
                    {
                        break;
                    }
                }
            }
            if (flag24)
            {
                if (!Collision.SolidTiles(NPC.homeTileX - 1, NPC.homeTileX + 1, num219 - 3, num219 - 1))
                {
                    NPC.velocity.X = 0f;
                    NPC.velocity.Y = 0f;
                    NPC.position.X = NPC.homeTileX * 16 + 8 - NPC.width / 2;
                    NPC.position.Y = num219 * 16 - NPC.height - 0.1f;
                    NPC.netUpdate = true;
                }
                else
                {
                    NPC.homeless = true;
                    WorldGen.QuickFindHome(NPC.whoAmI);
                }
            }
        }
        if (NPC.ai[0] == 0f)
        {
            if (NPC.ai[2] > 0f)
            {
                NPC.ai[2] -= 1f;
            }
            if (flag22 && !flag23)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (num216 == NPC.homeTileX && num217 == num219)
                    {
                        if (NPC.velocity.X != 0f)
                        {
                            NPC.netUpdate = true;
                        }
                        if (NPC.velocity.X > 0.1)
                        {
                            NPC.velocity.X = NPC.velocity.X - 0.1f;
                        }
                        else if (NPC.velocity.X < -0.1)
                        {
                            NPC.velocity.X = NPC.velocity.X + 0.1f;
                        }
                        else
                        {
                            NPC.velocity.X = 0f;
                        }
                    }
                    else if (!flag23)
                    {
                        if (num216 > NPC.homeTileX)
                        {
                            NPC.direction = -1;
                        }
                        else
                        {
                            NPC.direction = 1;
                        }
                        NPC.ai[0] = 1f;
                        NPC.ai[1] = 200 + Main.rand.Next(200);
                        NPC.ai[2] = 0f;
                        NPC.netUpdate = true;
                    }
                }
            }
            else
            {
                if (NPC.velocity.X > 0.1)
                {
                    NPC.velocity.X = NPC.velocity.X - 0.1f;
                }
                else if (NPC.velocity.X < -0.1)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.1f;
                }
                else
                {
                    NPC.velocity.X = 0f;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (NPC.ai[1] > 0f)
                    {
                        NPC.ai[1] -= 1f;
                    }
                    if (NPC.ai[1] <= 0f)
                    {
                        NPC.ai[0] = 1f;
                        NPC.ai[1] = 200 + Main.rand.Next(200);
                        NPC.ai[2] = 0f;
                        NPC.netUpdate = true;
                    }
                }
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && (!flag22 || (num216 == NPC.homeTileX && num217 == num219)))
            {
                if (num216 < NPC.homeTileX - 25 || num216 > NPC.homeTileX + 25)
                {
                    if (NPC.ai[2] == 0f)
                    {
                        if (num216 < NPC.homeTileX - 50 && NPC.direction == -1)
                        {
                            NPC.direction = 1;
                            NPC.netUpdate = true;
                            return;
                        }
                        if (num216 > NPC.homeTileX + 50 && NPC.direction == 1)
                        {
                            NPC.direction = -1;
                            NPC.netUpdate = true;
                            return;
                        }
                    }
                }
                else if (Main.rand.Next(80) == 0 && NPC.ai[2] == 0f)
                {
                    NPC.ai[2] = 200f;
                    NPC.direction *= -1;
                    NPC.netUpdate = true;
                    return;
                }
            }
        }
        else if (NPC.ai[0] == 1f)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && flag22 && num216 == NPC.homeTileX && num217 == NPC.homeTileY)
            {
                NPC.ai[0] = 0f;
                NPC.ai[1] = 200 + Main.rand.Next(200);
                NPC.ai[2] = 60f;
                NPC.netUpdate = true;
                return;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && !NPC.homeless && !Main.tileDungeon[Main.tile[num216, num217].TileType] && (num216 < NPC.homeTileX - 35 || num216 > NPC.homeTileX + 35))
            {
                if (NPC.position.X < NPC.homeTileX * 16 && NPC.direction == -1)
                {
                    NPC.ai[1] -= 5f;
                }
                else if (NPC.position.X > NPC.homeTileX * 16 && NPC.direction == 1)
                {
                    NPC.ai[1] -= 5f;
                }
            }
            NPC.ai[1] -= 1f;
            if (NPC.ai[1] <= 0f)
            {
                NPC.ai[0] = 0f;
                NPC.ai[1] = 300 + Main.rand.Next(300);
                NPC.ai[1] -= Main.rand.Next(100);
                NPC.ai[2] = 60f;
                NPC.netUpdate = true;
            }
            if (NPC.closeDoor && ((NPC.position.X + NPC.width / 2) / 16f > NPC.doorX + 2 || (NPC.position.X + NPC.width / 2) / 16f < NPC.doorX - 2))
            {
                var flag25 = WorldGen.CloseDoor(NPC.doorX, NPC.doorY, false);
                if (flag25)
                {
                    NPC.closeDoor = false;
                    NetMessage.SendData(MessageID.ChangeDoor, -1, -1, NetworkText.FromLiteral(""), 1, NPC.doorX, NPC.doorY, NPC.direction, 0);
                }
                if ((NPC.position.X + NPC.width / 2) / 16f > NPC.doorX + 4 || (NPC.position.X + NPC.width / 2) / 16f < NPC.doorX - 4 || (NPC.position.Y + NPC.height / 2) / 16f > NPC.doorY + 4 || (NPC.position.Y + NPC.height / 2) / 16f < NPC.doorY - 4)
                {
                    NPC.closeDoor = false;
                }
            }
            var num222 = 1f;
            var num223 = 0.07f;
            if (NPC.velocity.X < -num222 || NPC.velocity.X > num222)
            {
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity *= 0.8f;
                }
            }
            else if (NPC.velocity.X < num222 && NPC.direction == 1)
            {
                NPC.velocity.X = NPC.velocity.X + num223;
                if (NPC.velocity.X > num222)
                {
                    NPC.velocity.X = num222;
                }
            }
            else if (NPC.velocity.X > -num222 && NPC.direction == -1)
            {
                NPC.velocity.X = NPC.velocity.X - num223;
                if (NPC.velocity.X > num222)
                {
                    NPC.velocity.X = num222;
                }
            }
            var flag26 = (NPC.homeTileY - 2) * 16 <= NPC.position.Y;
            if ((NPC.direction == 1 && NPC.position.Y + NPC.width / 2 > NPC.homeTileX * 16) || (NPC.direction == -1 && NPC.position.Y + NPC.width / 2 < NPC.homeTileX * 16))
            {
                flag26 = true;
            }
            if (NPC.velocity.Y >= 0f)
            {
                var num224 = 0;
                if (NPC.velocity.X < 0f)
                {
                    num224 = -1;
                }
                if (NPC.velocity.X > 0f)
                {
                    num224 = 1;
                }
                var vector22 = NPC.position;
                vector22.X += NPC.velocity.X;
                var num225 = (int)((vector22.X + NPC.width / 2 + (NPC.width / 2 + 1) * num224) / 16f);
                var num226 = (int)((vector22.Y + NPC.height - 1f) / 16f);
                if (num225 * 16 < vector22.X + NPC.width && num225 * 16 + 16 > vector22.X)
                {
                    //if (Main.tile[num225, num226] == null)
                    //{
                    //    Main.tile[num225, num226] = new Tile();
                    //}
                    //if (Main.tile[num225, num226 - 1] == null)
                    //{
                    //    Main.tile[num225, num226 - 1] = new Tile();
                    //}
                    //if (Main.tile[num225, num226 - 2] == null)
                    //{
                    //    Main.tile[num225, num226 - 2] = new Tile();
                    //}
                    //if (Main.tile[num225, num226 - 3] == null)
                    //{
                    //    Main.tile[num225, num226 - 3] = new Tile();
                    //}
                    //if (Main.tile[num225, num226 + 1] == null)
                    //{
                    //    Main.tile[num225, num226 + 1] = new Tile();
                    //}
                    if (((Main.tile[num225, num226].HasUnactuatedTile && !Main.tile[num225, num226].TopSlope && !Main.tile[num225, num226 - 1].TopSlope && ((Main.tileSolid[Main.tile[num225, num226].TileType] && !Main.tileSolidTop[Main.tile[num225, num226].TileType]) || (flag26 && Main.tileSolidTop[Main.tile[num225, num226].TileType] && Main.tile[num225, num226].TileFrameY == 0 && (!Main.tileSolid[Main.tile[num225, num226 - 1].TileType] || !Main.tile[num225, num226 - 1].HasUnactuatedTile) && Main.tile[num225, num226].TileType != 16 && Main.tile[num225, num226].TileType != 18 && Main.tile[num225, num226].TileType != 134 && Main.tile[num225, num226].TileType != 360))) || (Main.tile[num225, num226 - 1].IsHalfBlock && Main.tile[num225, num226 - 1].HasUnactuatedTile)) && (!Main.tile[num225, num226 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num225, num226 - 1].TileType] || Main.tileSolidTop[Main.tile[num225, num226 - 1].TileType] || (Main.tile[num225, num226 - 1].IsHalfBlock && (!Main.tile[num225, num226 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num225, num226 - 4].TileType] || Main.tileSolidTop[Main.tile[num225, num226 - 4].TileType]))) && (!Main.tile[num225, num226 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num225, num226 - 2].TileType] || Main.tileSolidTop[Main.tile[num225, num226 - 2].TileType]) && (!Main.tile[num225, num226 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num225, num226 - 3].TileType] || Main.tileSolidTop[Main.tile[num225, num226 - 3].TileType]) && (!Main.tile[num225 - num224, num226 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num225 - num224, num226 - 3].TileType] || Main.tileSolidTop[Main.tile[num225 - num224, num226 - 3].TileType]))
                    {
                        float num227 = num226 * 16;
                        if (Main.tile[num225, num226].IsHalfBlock)
                        {
                            num227 += 8f;
                        }
                        if (Main.tile[num225, num226 - 1].IsHalfBlock)
                        {
                            num227 -= 8f;
                        }
                        if (num227 < vector22.Y + NPC.height)
                        {
                            var num228 = vector22.Y + NPC.height - num227;
                            if (num228 <= 16.1)
                            {
                                NPC.gfxOffY += NPC.position.Y + NPC.height - num227;
                                NPC.position.Y = num227 - NPC.height;
                                if (num228 < 9f)
                                {
                                    NPC.stepSpeed = 1f;
                                }
                                else
                                {
                                    NPC.stepSpeed = 2f;
                                }
                            }
                        }
                    }
                }
            }
            if (NPC.velocity.Y == 0f)
            {
                if (NPC.position.X == NPC.ai[2])
                {
                    NPC.direction *= -1;
                }
                NPC.ai[2] = -1f;
                var num229 = (int)((NPC.position.X + NPC.width / 2 + 15 * NPC.direction) / 16f);
                var num230 = (int)((NPC.position.Y + NPC.height - 16f) / 16f);
                //if (Main.tile[num229, num230] == null)
                //{
                //    Main.tile[num229, num230] = new Tile();
                //}
                //if (Main.tile[num229, num230 - 1] == null)
                //{
                //    Main.tile[num229, num230 - 1] = new Tile();
                //}
                //if (Main.tile[num229, num230 - 2] == null)
                //{
                //    Main.tile[num229, num230 - 2] = new Tile();
                //}
                //if (Main.tile[num229, num230 - 3] == null)
                //{
                //    Main.tile[num229, num230 - 3] = new Tile();
                //}
                //if (Main.tile[num229, num230 + 1] == null)
                //{
                //    Main.tile[num229, num230 + 1] = new Tile();
                //}
                //if (Main.tile[num229 - NPC.direction, num230 + 1] == null)
                //{
                //    Main.tile[num229 - NPC.direction, num230 + 1] = new Tile();
                //}
                //if (Main.tile[num229 + NPC.direction, num230 - 1] == null)
                //{
                //    Main.tile[num229 + NPC.direction, num230 - 1] = new Tile();
                //}
                //if (Main.tile[num229 + NPC.direction, num230 + 1] == null)
                //{
                //    Main.tile[num229 + NPC.direction, num230 + 1] = new Tile();
                //}
                if (NPC.townNPC && Main.tile[num229, num230 - 2].HasUnactuatedTile && Main.tile[num229, num230 - 2].TileType == 10 && (Main.rand.Next(10) == 0 || flag22))
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        var flag27 = WorldGen.OpenDoor(num229, num230 - 2, NPC.direction);
                        if (flag27)
                        {
                            NPC.closeDoor = true;
                            NPC.doorX = num229;
                            NPC.doorY = num230 - 2;
                            NetMessage.SendData(MessageID.ChangeDoor, -1, -1, NetworkText.FromLiteral(""), 0, num229, num230 - 2, NPC.direction, 0);
                            NPC.netUpdate = true;
                            NPC.ai[1] += 80f;
                            return;
                        }
                        if (WorldGen.OpenDoor(num229, num230 - 2, -NPC.direction))
                        {
                            NPC.closeDoor = true;
                            NPC.doorX = num229;
                            NPC.doorY = num230 - 2;
                            NetMessage.SendData(MessageID.ChangeDoor, -1, -1, NetworkText.FromLiteral(""), 0, num229, num230 - 2, -NPC.direction, 0);
                            NPC.netUpdate = true;
                            NPC.ai[1] += 80f;
                            return;
                        }
                        NPC.direction *= -1;
                        NPC.netUpdate = true;
                        return;
                    }
                }
                else
                {
                    if ((NPC.velocity.X < 0f && NPC.spriteDirection == -1) || (NPC.velocity.X > 0f && NPC.spriteDirection == 1))
                    {
                        if (Main.tile[num229, num230 - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[num229, num230 - 2].TileType] && !Main.tileSolidTop[Main.tile[num229, num230 - 2].TileType])
                        {
                            if ((NPC.direction == 1 && !Collision.SolidTiles(num229 - 2, num229 - 1, num230 - 5, num230 - 1)) || (NPC.direction == -1 && !Collision.SolidTiles(num229 + 1, num229 + 2, num230 - 5, num230 - 1)))
                            {
                                if (!Collision.SolidTiles(num229, num229, num230 - 5, num230 - 3))
                                {
                                    NPC.velocity.Y = -6f;
                                    NPC.netUpdate = true;
                                }
                                else
                                {
                                    NPC.direction *= -1;
                                    NPC.netUpdate = true;
                                }
                            }
                            else
                            {
                                NPC.direction *= -1;
                                NPC.netUpdate = true;
                            }
                        }
                        else if (Main.tile[num229, num230 - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[num229, num230 - 1].TileType] && !Main.tileSolidTop[Main.tile[num229, num230 - 1].TileType])
                        {
                            if ((NPC.direction == 1 && !Collision.SolidTiles(num229 - 2, num229 - 1, num230 - 4, num230 - 1)) || (NPC.direction == -1 && !Collision.SolidTiles(num229 + 1, num229 + 2, num230 - 4, num230 - 1)))
                            {
                                if (!Collision.SolidTiles(num229, num229, num230 - 4, num230 - 2))
                                {
                                    NPC.velocity.Y = -5f;
                                    NPC.netUpdate = true;
                                }
                                else
                                {
                                    NPC.direction *= -1;
                                    NPC.netUpdate = true;
                                }
                            }
                            else
                            {
                                NPC.direction *= -1;
                                NPC.netUpdate = true;
                            }
                        }
                        else if (NPC.position.Y + NPC.height - num230 * 16 > 20f && Main.tile[num229, num230].HasUnactuatedTile && Main.tileSolid[Main.tile[num229, num230].TileType] && !Main.tile[num229, num230].TopSlope)
                        {
                            if ((NPC.direction == 1 && !Collision.SolidTiles(num229 - 2, num229, num230 - 3, num230 - 1)) || (NPC.direction == -1 && !Collision.SolidTiles(num229, num229 + 2, num230 - 3, num230 - 1)))
                            {
                                NPC.velocity.Y = -4.4f;
                                NPC.netUpdate = true;
                            }
                            else
                            {
                                NPC.direction *= -1;
                                NPC.netUpdate = true;
                            }
                        }
                        try
                        {
                            //if (Main.tile[num229, num230 + 1] == null)
                            //{
                            //    Main.tile[num229, num230 + 1] = new Tile();
                            //}
                            //if (Main.tile[num229 - NPC.direction, num230 + 1] == null)
                            //{
                            //    Main.tile[num229 - NPC.direction, num230 + 1] = new Tile();
                            //}
                            //if (Main.tile[num229, num230 + 2] == null)
                            //{
                            //    Main.tile[num229, num230 + 2] = new Tile();
                            //}
                            //if (Main.tile[num229 - NPC.direction, num230 + 2] == null)
                            //{
                            //    Main.tile[num229 - NPC.direction, num230 + 2] = new Tile();
                            //}
                            //if (Main.tile[num229, num230 + 3] == null)
                            //{
                            //    Main.tile[num229, num230 + 3] = new Tile();
                            //}
                            //if (Main.tile[num229 - NPC.direction, num230 + 3] == null)
                            //{
                            //    Main.tile[num229 - NPC.direction, num230 + 3] = new Tile();
                            //}
                            //if (Main.tile[num229, num230 + 4] == null)
                            //{
                            //    Main.tile[num229, num230 + 4] = new Tile();
                            //}
                            //if (Main.tile[num229 - NPC.direction, num230 + 4] == null)
                            //{
                            //    Main.tile[num229 - NPC.direction, num230 + 4] = new Tile();
                            //}
                            if (num216 >= NPC.homeTileX - 35 && num216 <= NPC.homeTileX + 35 && (!Main.tile[num229, num230 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num229, num230 + 1].TileType]) && (!Main.tile[num229 - NPC.direction, num230 + 1].HasTile || !Main.tileSolid[Main.tile[num229 - NPC.direction, num230 + 1].TileType]) && (!Main.tile[num229, num230 + 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num229, num230 + 2].TileType]) && (!Main.tile[num229 - NPC.direction, num230 + 2].HasTile || !Main.tileSolid[Main.tile[num229 - NPC.direction, num230 + 2].TileType]) && (!Main.tile[num229, num230 + 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num229, num230 + 3].TileType]) && (!Main.tile[num229 - NPC.direction, num230 + 3].HasTile || !Main.tileSolid[Main.tile[num229 - NPC.direction, num230 + 3].TileType]) && (!Main.tile[num229, num230 + 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num229, num230 + 4].TileType]) && (!Main.tile[num229 - NPC.direction, num230 + 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num229 - NPC.direction, num230 + 4].TileType]))
                            {
                                NPC.direction *= -1;
                                NPC.velocity.X = NPC.velocity.X * -1f;
                                NPC.netUpdate = true;
                            }
                        }
                        catch
                        {
                        }
                        if (NPC.velocity.Y < 0f)
                        {
                            NPC.ai[2] = NPC.position.X;
                        }
                    }
                    if (NPC.velocity.Y < 0f && NPC.wet)
                    {
                        NPC.velocity.Y = NPC.velocity.Y * 1.2f;
                    }
                }
            }
        }
    }*/
    public override void FindFrame(int frameHeight)
    {
        /*if (NPC.velocity.Y == 0f)
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
                if (NPC.frameCounter > 6.0)
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
        }*/
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
