using Avalon.Biomes;
using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Accessories.Vanity;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Placeable.Wall;
using Avalon.NPCs.Hardmode;
using Avalon.NPCs.PreHardmode;
using Avalon.NPCs.TownNPCs;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonGlobalNPC : GlobalNPC
{
    public static float ModSpawnRate { get; set; } = 0.25f;
    public static int BleedTime = 60 * 7;
    public static int PhantasmBoss = -1;

    /// <summary>
    ///     Finds a type of NPC.
    /// </summary>
    /// <param name="type">The type of NPC to find.</param>
    /// <returns>The index of the found NPC in the Main.npc[] array.</returns>
    public static int FindATypeOfNPC(int type)
    {
        for (int i = 0; i < 200; i++)
        {
            if (type == Main.npc[i].type && Main.npc[i].active)
            {
                return i;
            }
        }

        return 0;
    }
    public override void OnKill(NPC npc)
    {
        if (npc.type == NPCID.Vulture && AvalonWorld.SpawnDesertBeak)
        {
            AvalonWorld.VultureKillCount++;
        }
        if (npc.type == NPCID.SkeletronHead && !NPC.downedBoss3)
        {
            AvalonWorld.GenerateSulphur();
        }
        if (npc.type is NPCID.TheDestroyer or NPCID.Retinazer or NPCID.Spazmatism or NPCID.SkeletronPrime)
        {
            if (ClassExtensions.DownedAllButOneMechBoss())
            {
                if ((npc.type == NPCID.Spazmatism && NPC.AnyNPCs(NPCID.Retinazer)) || (npc.type == NPCID.Retinazer && NPC.AnyNPCs(NPCID.Spazmatism)))
                {
                    return;
                }
                AvalonWorld.GenerateHallowedOre();
            }
        }
        if (npc.type == NPCID.MoonLordCore && !NPC.downedMoonlord)
        {
            if (Main.netMode == 0)
            {
                Main.NewText(Language.GetTextValue("Mods.Avalon.BossDefeatedBlurbs.MoonLord"), 50, 255, 130);
            }
            else if (Main.netMode == 2)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Avalon.BossDefeatedBlurbs.MoonLord"), new Color(50, 255, 130));
            }
        }

        // add back when hardmode/hidden temple releases
        //if (npc.type == NPCID.Golem && !NPC.downedGolemBoss)
        //{
        //    if (Main.netMode == NetmodeID.SinglePlayer)
        //    {
        //        Main.NewText(Language.GetTextValue("Mods.Avalon.BossDefeatedBlurbs.Golem"), 50, 255, 130);
        //    }
        //    else if (Main.netMode == NetmodeID.Server)
        //    {
        //        ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Avalon.BossDefeatedBlurbs.Golem"), new Color(50, 255, 130));
        //    }
        //}
    }

    /// <summary>
    /// A method that scrambles the stats of a target enemy.
    /// </summary>
    /// <param name="n">The NPC to scramble.</param>
    public static void ScrambleStats(NPC n)
    {
        float amount = (float)(Main.rand.NextFloat() + 0.5f + Main.rand.NextFloat() * 0.5f);
        //n.life = (int)(n.lifeMax * amount);
        n.defDefense *= (int)(n.defense * amount);
        n.defDamage *= (int)(n.damage * amount);
    }
    public override void ModifyShop(NPCShop shop)
    {
        Condition corruption = new Condition("Corruption", () => ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Corruption);
        Condition crimson = new Condition("Crimson", () => ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Crimson);
        Condition contagion = new Condition("Contagion", () => ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion);
        Condition notContagion = new Condition("Not Contagion", () => !contagion.IsMet());
        Condition downedBP = new Condition("BacteriumPrime", () => ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime);

        if (shop.NpcType == NPCID.Merchant)
        {
            shop.Add(new Item(ItemID.MusicBox)
            {
                shopCustomPrice = 350000,
            });
        }
        if (shop.NpcType == NPCID.PartyGirl)
        {
            shop.Add(new Item(ModContent.ItemType<AncientHeadphones>())
            {
                shopCustomPrice = Item.buyPrice(gold: 12),
            });
        }
        if (shop.NpcType == NPCID.Pirate)
        {
            shop.Add(new Item(ModContent.ItemType<FalseTreasureMap>())
            {
                shopCustomPrice = Item.buyPrice(0, 4),
            }, Condition.DownedPirates);
        }
        if (shop.NpcType == NPCID.GoblinTinkerer)
        {
            shop.Add(new Item(ModContent.ItemType<RocketinaBottle>())
            {
                shopCustomPrice = Item.buyPrice(0, 6),
            }, Condition.Hardmode);

            shop.Add(new Item(ModContent.ItemType<GoblinRetreatOrder>())
            {
                shopCustomPrice = Item.buyPrice(0, 4),
            }, Condition.DownedGoblinArmy);
        }
        if (shop.NpcType == NPCID.Dryad)
        {
            shop.InsertAfter(ItemID.CrimsonPlanterBox, new Item(ModContent.ItemType<BarfbushPlanterBox>())
            {
                shopCustomPrice = Item.buyPrice(silver: 1)
            }, downedBP);
            shop.InsertAfter(ItemID.CrimsonPlanterBox, new Item(ModContent.ItemType<SweetstemPlanterBox>())
            {
                shopCustomPrice = Item.buyPrice(silver: 1)
            }, Condition.DownedQueenBee);
            shop.InsertAfter(ItemID.FireBlossomPlanterBox, new Item(ModContent.ItemType<HolybirdPlanterBox>())
            {
                shopCustomPrice = Item.buyPrice(silver: 1)
            }, Condition.Hardmode);

            shop.InsertAfter(ItemID.CrimsonGrassEcho, new Item(ModContent.ItemType<ContagionGrassWall>())
            {
                shopCustomPrice = Item.buyPrice(silver: 2, copper: 50)
            }, Condition.BloodMoon, contagion);

            shop.InsertAfter(ItemID.CrimsonSeeds, new Item(ModContent.ItemType<ContagionSeeds>())
            {
                shopCustomPrice = Item.buyPrice(silver: 5)
            }, Condition.BloodMoon, contagion);

            shop.InsertAfter(ItemID.CrimsonSeeds, new Item(ModContent.ItemType<ContagionSeeds>())
            {
                shopCustomPrice = Item.buyPrice(silver: 5)
            }, corruption, Condition.InGraveyard, Condition.Hardmode);
            
            shop.InsertAfter(ItemID.CorruptSeeds, new Item(ModContent.ItemType<ContagionSeeds>())
            {
                shopCustomPrice = Item.buyPrice(silver: 5)
            }, crimson, Condition.InGraveyard, Condition.Hardmode);

            shop.InsertAfter(ItemID.ViciousPowder, new Item(ModContent.ItemType<VirulentPowder>())
            {
                shopCustomPrice = Item.buyPrice(silver: 1)
            }, Condition.BloodMoon, contagion);

            // the seeds here aren't always positioned correctly, but it works and I do NOT want to keep relaunching to figure out something as minor as positioning
            if (shop.TryGetEntry(ItemID.CorruptSeeds, out NPCShop.Entry entry))
            {
                entry.AddCondition(notContagion);
            }
            if (shop.TryGetEntry(ItemID.CrimsonSeeds, out NPCShop.Entry entry2))
            {
                entry2.AddCondition(notContagion);
            }
            shop.InsertAfter(ModContent.ItemType<ContagionGrassWall>(), new Item(ItemID.CorruptSeeds)
            {
                shopCustomPrice = Item.buyPrice(silver: 5)
            }, contagion, Condition.InGraveyard, Condition.Hardmode);


            if (shop.TryGetEntry(ItemID.CorruptGrassEcho, out NPCShop.Entry entry3))
            {
                entry3.AddCondition(notContagion);
            }
            if (shop.TryGetEntry(ItemID.CrimsonGrassEcho, out NPCShop.Entry entry4))
            {
                entry4.AddCondition(notContagion);
            }
            if (shop.TryGetEntry(ItemID.VilePowder, out NPCShop.Entry entry5))
            {
                entry5.AddCondition(notContagion);
            }
            if (shop.TryGetEntry(ItemID.ViciousPowder, out NPCShop.Entry entry6))
            {
                entry6.AddCondition(notContagion);
            }
        }
    }
    /// <summary>
    ///  A method to choose a random Town NPC death messages.
    /// </summary>
    /// <param name="type">The Town NPC's type.</param>
    /// <returns>The string containing the death message.</returns>
    public static string TownDeathMsg(int type)
    {
        string result = string.Empty;
        if (type == NPCID.Merchant)
        {
            int r = Main.rand.Next(5);
            if (r == 0) result += " tried to sell torches to a zombie.";
            if (r == 1) result += " made a grave error...";
            if (r == 2) result += " was slain...";
            if (r == 3) result += " was hanged with a bug net.";
            if (r == 4) result += " tried gold dust for the first time.";
        }
        else if (type == NPCID.Nurse)
        {
            int r = Main.rand.Next(5);
            if (r == 0) result += " couldn't heal herself in a timely manner.";
            if (r == 1) result += " was expected to make a full recovery.";
            if (r == 2) result += "'s face wasn't sewn on well enough.";
            if (r == 3) result += " encountered a complication.";
            if (r == 4) result += "'s surgical strike was in error.";
        }
        else if (type == NPCID.OldMan)
        {
            int r = Main.rand.Next(2);
            if (r == 0) result += " died of old age.";
            if (r == 1) result += " was slain...";
        }
        else if (type == NPCID.ArmsDealer)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += "'s gun jammed.";
            if (r == 1) result += " fired a meteor bullet...";
            if (r == 2) result += " ran out of ammo.";
            if (r == 3) result += " was slain...";
            if (r == 4) result += " shot himself.";
            if (r == 5)
            {
                if (!Main.dayTime) result += " was caught.";
                else result += " shot himself.";
            }
        }
        else if (type == NPCID.Dryad)
        {
            int r = Main.rand.Next(7);
            if (r == 0) result += "'s time was up...";
            if (r == 1) result += " choked on an herb.";
            if (r == 2) result += " turned into grass.";
            if (r == 3) result += " died.";
            if (r == 4) result += " was reduced to a fine paste...";
            if (r == 5) result += " was mauled by an unknown creature.";
            if (r == 6) result += " tripped on a corrupt vine.";
        }
        else if (type == NPCID.Guide)
        {
            int r = Main.rand.Next(7);
            if (r == 0) result += " let too many zombies in.";
            if (r == 1) result += " was reading a history book...";
            if (r == 2) result += " was slain...";
            if (r == 3) result += " got impaled...";
            if (r == 4) result += " was the victim of dark magic.";
            if (r == 5) result += " was voodoo'd.";
            if (r == 6) result += " opened a door.";
        }
        else if (type == NPCID.Demolitionist)
        {
            int r = Main.rand.Next(7);
            if (r == 0) result += " blew up.";
            if (r == 1) result += " threw a bomb at a mob, but it bounced back.";
            if (r == 2) result += "'s explosives were a little too sensitive...";
            if (r == 3) result += " made a dirty bomb.";
            if (r == 4) result += " was bombed.";
            if (r == 5) result += " went out with a bang.";
            if (r == 6) result += " became a true martyr.";
        }
        else if (type == NPCID.Clothier)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += " was unknowingly cursed...";
            if (r == 1) result += " died from unknown causes.";
            if (r == 2) result += " was unravelled...";
            if (r == 3) result += " was eviscerated...";
            if (r == 4) result += " went back to the dungeon.";
            if (r == 5) result += " stitched himself up.";
        }
        else if (type == NPCID.GoblinTinkerer)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += "'s contraption exploded.";
            if (r == 1) result += " tinkered with his life.";
            if (r == 2) result += " was crushed...";
            if (r == 3) result += " rocketed into the ceiling.";
            if (r == 4) result += " died from platinum poisoning.";
            if (r == 5) result += " was approached from the north.";
        }
        else if (type == NPCID.Wizard)
        {
            int r = Main.rand.Next(7);
            if (r == 0) result += " forgot how to live.";
            if (r == 1) result += " cast too many spells...";
            if (r == 2) result += " was crushed...";
            if (r == 3) result += " watched his innards become outards...";
            if (r == 4) result += " made himself 'disappear.'";
            if (r == 5) result += " became a frog.";
            if (r == 6) result += "'s low armor class failed him.";
        }
        else if (type == NPCID.SantaClaus)
        {
            int r = Main.rand.Next(2);
            if (r == 0) result += " had too much milk and cookies.";
            if (r == 1) result += " wasn't believed in.";
        }
        else if (type == NPCID.Mechanic)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += "'s spine cracked...";
            if (r == 1) result += "'s engine broke down.";
            if (r == 2) result += " ran out of wire...";
            if (r == 3) result += " died.";
            if (r == 4) result += " dropped her contacts...";
            if (r == 5) result += " was removed from " + Main.worldName + ".";
        }
        else if (type == NPCID.Truffle)
        {
            int r = Main.rand.Next(5);
            if (r == 0) result += " bit himself.";
            if (r == 1) result += " was eaten.";
            if (r == 2) result += " was slain...";
            if (r == 3) result += " is no longer a fun guy.";
            if (r == 4) result += " went on a trip.";
        }
        else if (type == NPCID.Steampunker)
        {
            int r = Main.rand.Next(5);
            if (r == 0) result += "'s time machine created a paradox...";
            if (r == 1) result += " slipped a cog.";
            if (r == 2) result += " died from unknown causes.";
            if (r == 3) result += " fell off an airship.";
            if (r == 4) result += " used her teleporter too fast.";
        }
        else if (type == NPCID.DyeTrader)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += " fell into his dye.";
            if (r == 1) result += " was killed.";
            if (r == 2) result += " died from unknown causes.";
            if (r == 3) result += " was decapitated.";
            if (r == 4) result += " has dyed.";
            if (r == 5) result += " was dyed a deep chartreuse.";
        }
        else if (type == NPCID.PartyGirl)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += " partied too hard.";
            if (r == 1) result += " inhaled too much confetti...";
            if (r == 2) result += " was crushed.";
            if (r == 3) result += " left the party.";
            if (r == 4) result += " was eaten.";
            if (r == 5) result += " was dissolved into the punch.";
        }
        else if (type == NPCID.Cyborg)
        {
            int r = Main.rand.Next(8);
            if (r == 0) result += " was assimilated.";
            if (r == 1) result += "'s mechanisms were damaged beyond repair.";
            if (r == 2) result += " was crushed.";
            if (r == 3) result += " is '404 not found.'";
            if (r == 4) result += "'s implants were ripped out.";
            if (r == 5) result += " short-circuited.";
            if (r == 6) result += " malfunctioned.";
            if (r == 7) result += " encountered a glitch in his systems.";
        }
        else if (type == NPCID.Painter)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += " inhaled paint fumes.";
            if (r == 1) result += " tried to paint himself.";
            if (r == 2) result += "'s body was mangled.";
            if (r == 3) result += "'s paint was ripped off the canvas...";
            if (r == 4) result += "'s paint cracked.";
            if (r == 5) result += " inhaled asbestos.";
        }
        else if (type == NPCID.WitchDoctor)
        {
            int r = Main.rand.Next(5);
            if (r == 0) result += "'s practice lead to his demise.";
            if (r == 1) result += " tried to embody life.";
            if (r == 2) result += "'s body was mangled.";
            if (r == 3) result += " was chopped up.";
            if (r == 4) result += " was eaten by an exotic insect.";
        }
        else if (type == NPCID.Pirate)
        {
            int r = Main.rand.Next(5);
            if (r == 0) result += " choked on his gold tooth.";
            if (r == 1) result += " was hit by a cannonball.";
            if (r == 2) result += " was eviscerated.";
            if (r == 3) result += "'s other eye was removed.";
            if (r == 4) result += " lost his peg leg.";
        }
        else if (type == NPCID.Stylist)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += " attempted to give a spider a trim.";
            if (r == 1) result += " was tied up.";
            if (r == 2) result += " was eviscerated.";
            if (r == 3) result += " got a little too snippy.";
            if (r == 4) result += " tried to give Cousin It a trim.";
            if (r == 5) result += " couldn't take the sight of spiders anymore.";
        }
        else if (type == NPCID.TravellingMerchant)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += " has departed...";
            if (r == 1) result += " had INTERPOL problems.";
            if (r == 2) result += " was vaporized.";
            if (r == 3) result += " did not need a monster there.";
            if (r == 4) result += " tried to sell an exotic pitcher plant...";
            if (r == 5) result += " failed to make any sales.";
        }
        else if (type == NPCID.Angler)
        {
            int r = Main.rand.Next(9);
            if (r == 0) result += " has left!";
            if (r == 1) result += " caught a fish!";
            if (r == 2) result += " fell into a ditch!";
            if (r == 3) result += " failed a test!";
            if (r == 4) result += " lost the game!";
            if (r == 5) result += " took off!";
            if (r == 6) result += " died.";
            if (r == 7) result += " found someone better to harass.";
            if (r == 8)
            {
                if (NPC.AnyNPCs(NPCID.Pirate))
                    result += " was thrown overboard by " + Main.npc[FindATypeOfNPC(NPCID.Pirate)].GivenName + ".";
                else result += " died.";
            }
        }
        else if (type == NPCID.TaxCollector)
        {
            int r = Main.rand.Next(5);
            if (r == 0) result += " went to the wrong house.";
            if (r == 1) result += " collected counterfeit money.";
            if (r == 2) result += " had a hole in his pocket.";
            if (r == 3) result += " saw the ghost of " + Main.worldName + "'s past.";
            if (r == 4) result += " overtaxed himself.";
        }
        else if (type == NPCID.DD2Bartender)
        {
            int r = Main.rand.Next(5);
            if (r == 0) result += " found himself on the wrong side of the bar.";
            if (r == 1) result += "'s sentries stopped working.";
            if (r == 2) result += " spontaneously combusted.";
            if (r == 3) result += " went through the wrong portal.";
            if (r == 4) result += " drank his feelings away.";
        }
        else if (type == NPCID.Princess)
        {
            int r = Main.rand.Next(4);
            if (r == 0) result += " was taken away by a dragon.";
            if (r == 1) result += " was chained up in a tower.";
            if (r == 2) result += " swallowed her scepter.";
            if (r == 3) result += " tripped on her dress.";
        }
        else if (type == NPCID.Golfer)
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += " didn't yell \"fore!\"";
            if (r == 1) result += " was hit in the head by a golf ball.";
            if (r == 2) result += " got a triple bogey.";
            if (r == 3) result += " went over par.";
            if (r == 4) result += " fell in a hole.";
            if (r == 5) result += "'s club hit back.";
        }
        else if (type == NPCID.BestiaryGirl)
        {
            int r = Main.rand.Next(3);
            if (r == 0) result += " got rabies.";
            if (r == 1) result += " was put down.";
            if (r == 2) result += " got too comfortable with an exotic beast.";
        }
        /*else if (type == ModContent.NPCType<Iceman>())
        {
            int r = Main.rand.Next(7);
            if (r == 0) result += " froze.";
            if (r == 1) result += " melted.";
            if (r == 2) result += " has died.";
            if (r == 3) result += "'s ice was cracked.";
            if (r == 4)
            {
                if (NPC.AnyNPCs(NPCID.ArmsDealer))
                    result += " was used to cool " + Main.npc[FindATypeOfNPC(NPCID.ArmsDealer)].GivenName + "'s drink.";
                else result += " fell into a crevasse.";
            }
            if (r == 5) result += " fell into a crevasse";
            if (r == 6) result += " slipped.";
        }*/
        else if (type == ModContent.NPCType<Librarian>())
        {
            int r = Main.rand.Next(6);
            if (r == 0) result += " was nuked by a full squad.";
            if (r == 1) result += " fell victim to toxic world chat.";
            if (r == 2) result += " couldn't afford grade eighteen.";
            if (r == 3) result += " was slain by a boss cone attack.";
            if (r == 4) result += " was no match for Kun Kun.";
            if (r == 5) result += "'s Visa card was declined.";
        }
        else result += " was slain...";

        return result;
    }
    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        if (source is EntitySource_Parent parent && parent.Entity is NPC npc2 && npc2.HasBuff(BuffID.Cursed))
        {
            npc.active = false;
        }
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        if (npc.HasBuff<Lacerated>())
        {
            for (int i = 0; i < npc.GetGlobalNPC<AvalonGlobalNPCInstance>().LacerateStacks; i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
            }
        }
    }
    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.InModBiome<ContagionCaveDesert>())
        {
            pool.Add(NPCID.DesertBeast, 0.3f);
            pool.Add(NPCID.DesertLamiaDark, 0.45f);
            pool.Add(NPCID.DesertDjinn, 0.45f);
            pool.Add(NPCID.DuneSplicerHead, 0.2f);
            pool.Add(NPCID.DesertScorpionWalk, 0.35f);
            pool.Add(NPCID.DesertBeast, 0.15f);
            pool.Add(NPCID.Antlion, 0.55f);
            pool.Add(NPCID.WalkingAntlion, 0.35f);
            pool.Add(NPCID.GiantWalkingAntlion, 0.05f);
            pool.Add(NPCID.GiantFlyingAntlion, 0.05f);
            pool.Add(NPCID.FlyingAntlion, 0.35f);
        }
        if (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneHellcastle)
        {
            pool.Clear();
            pool.Add(NPCID.Demon, 0.2f);
            pool.Add(NPCID.RedDevil, 0.2f);
            pool.Add(ModContent.NPCType<EctoHand>(), 0.3f);
            pool.Add(ModContent.NPCType<HellboundLizard>(), 1f);
            pool.Add(ModContent.NPCType<Gargoyle>(), 1f);
            //if (ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode)
            //{
            //    pool.Add(ModContent.NPCType<ArmoredHellTortoise>(), 1f);
            //}
        }
        if (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion && !spawnInfo.Player.InPillarZone()) //
        {
            pool.Clear();
            pool.Add(ModContent.NPCType<Bactus>(), 1f);
            pool.Add(ModContent.NPCType<PyrasiteHead>(), 0.1f);
            if (Main.hardMode)
            {
                pool.Add(ModContent.NPCType<Cougher>(), 0.8f);
                pool.Add(ModContent.NPCType<Ickslime>(), 0.7f);
                if (spawnInfo.Player.ZoneRockLayerHeight)
                {
                    pool.Add(ModContent.NPCType<Viris>(), 1f);
                    //pool.Add(ModContent.NPCType<GrossyFloat>(), 0.6f);
                }

                if (spawnInfo.Player.ZoneDesert)
                {
                    pool.Add(NPCID.DarkMummy, 0.3f);
                    //pool.Add(ModContent.NPCType<EvilVulture>(), 0.4f);
                }
            }
        }
    }
    public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
        if (player.InModBiome<Contagion>())
        {
            spawnRate = (int)(spawnRate * 0.65f);
            maxSpawns = (int)(maxSpawns * 1.3f);
        }
    }
    public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
        if (npc.type is NPCID.BloodJelly or NPCID.Unicorn or NPCID.DarkMummy or NPCID.LightMummy && Main.rand.NextBool(9))
        {
            target.AddBuff(ModContent.BuffType<BrokenWeaponry>(), 60 * 7);
        }
        if (npc.type == NPCID.Mummy || npc.type == NPCID.FungoFish || npc.type == NPCID.Clinger)
        {
            if (Main.rand.NextBool(9))
            {
                target.AddBuff(ModContent.BuffType<Unloaded>(), 60 * 7);
            }
        }
        if (npc.type is NPCID.AngryTrapper && Main.rand.NextBool(15))
        {
            target.AddBuff(ModContent.BuffType<CurseofDelirium>(), 60 * 60);
        }
        if (npc.type is NPCID.IlluminantBat or NPCID.IlluminantSlime && Main.rand.NextBool(6))
        {
            target.AddBuff(ModContent.BuffType<CurseofDelirium>(), 60 * 15);
        }
        if ((npc.type is NPCID.EnchantedSword or NPCID.CursedHammer or NPCID.CrimsonAxe || npc.type == ModContent.NPCType<InfectedPickaxe>()) && Main.rand.NextBool(5))
        {
            target.AddBuff(ModContent.BuffType<CurseofDelirium>(), 60 * 60);
        }
    }
    public static bool SpikeCollision2(Vector2 Position, int Width, int Height)
    {
        int LowX = (int)((Position.X - 2f) / 16f); // - Radius;
        int HighX = (int)((Position.X + (float)Width) / 16f); // + Radius;
        int LowY = (int)((Position.Y - 2f) / 16f); // - Radius;
        int HighY = (int)((Position.Y + (float)Height) / 16f); // + Radius;
        if (LowX < 0)
        {
            LowX = 0;
        }
        if (HighX > Main.maxTilesX)
        {
            HighX = Main.maxTilesX;
        }
        if (LowY < 0)
        {
            LowY = 0;
        }
        if (HighY > Main.maxTilesY)
        {
            HighY = Main.maxTilesY;
        }
        for (int i = LowX; i <= HighX; i++)
        {
            for (int j = LowY; j <= HighY; j++)
            {
                if (Main.tile[i, j] != null && Main.tile[i, j].HasTile && (Main.tile[i, j].TileType == ModContent.TileType<Tiles.DemonSpikescale>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.BloodiedSpike>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.NastySpike>()))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public override void PostAI(NPC npc)
    {
        npc.GetGlobalNPC<AvalonGlobalNPCInstance>().SpikeTimer++;
        if (npc.GetGlobalNPC<AvalonGlobalNPCInstance>().SpikeTimer >= 60)
        {
            if (!npc.townNPC && npc.lifeMax > 5 && !npc.dontTakeDamage && !npc.noTileCollide && SpikeCollision2(npc.position, npc.width, npc.height))
            {
                NPC.HitInfo hit = new NPC.HitInfo();
                hit.Damage = 30 + (int)(npc.defense / 2);
                hit.HitDirection = 0;
                hit.Knockback = 0;
                npc.StrikeNPC(hit);
                npc.GetGlobalNPC<AvalonGlobalNPCInstance>().SpikeTimer = 0;
            }
        }
    }
    public override bool CheckDead(NPC npc)
    {
        if (npc.townNPC && npc.life <= 0)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(npc.FullName + TownDeathMsg(npc.type), new Color(178, 0, 90));
                npc.life = 0;
                npc.active = false;
                npc.NPCLoot();
                SoundEngine.PlaySound(SoundID.NPCDeath1, npc.position);
            }
            else
            {
                ChatHelper.BroadcastChatMessage(
                    NetworkText.FromLiteral(npc.FullName + TownDeathMsg(npc.type)),
                    new Color(178, 0, 90));
                NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, npc.whoAmI, -1);
                int t = 0;
                int s = 1;
                switch (npc.type)
                {
                    case NPCID.Guide:
                        if (npc.GivenName == "Andrew")
                        {
                            t = ItemID.GreenCap;
                        }
                        break;
                    case NPCID.DyeTrader:
                        if (Main.rand.NextBool(8))
                        {
                            t = ItemID.DyeTradersScimitar;
                        }
                        break;
                    case NPCID.Painter:
                        if (Main.rand.NextBool(10))
                        {
                            t = ItemID.PainterPaintballGun;
                        }
                        break;
                    case NPCID.DD2Bartender:
                        if (Main.rand.NextBool(8))
                        {
                            t = ItemID.AleThrowingGlove;
                        }
                        break;
                    case NPCID.Stylist:
                        if (Main.rand.NextBool(8))
                        {
                            t = ItemID.StylistKilLaKillScissorsIWish;
                        }
                        break;
                    case NPCID.Clothier:
                        t = ItemID.RedHat;
                        break;
                    case NPCID.PartyGirl:
                        if (Main.rand.NextBool(4))
                        {
                            t = ItemID.PartyGirlGrenade;
                            s = Main.rand.Next(30, 61);
                        }
                        break;
                    case NPCID.TaxCollector:
                        if (Main.rand.NextBool(8))
                        {
                            t = 3351;
                        }
                        break;
                    case NPCID.TravellingMerchant:
                        t = ItemID.PeddlersHat;
                        break;
                    case NPCID.Princess:
                        t = ItemID.PrincessWeapon;
                        break;
                }
                if (t > 0)
                {
                    int a = Item.NewItem(npc.GetSource_Loot(), npc.position, 16, 16, t, s);
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a);
                }
                // Main.npc[npc.whoAmI].NPCLoot();
                SoundEngine.PlaySound(SoundID.NPCDeath1, npc.position);
            }
            return false;
        }

        return base.CheckDead(npc);
    }
    public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        if(npc.netID is 529 or 533)
        {
            bestiaryEntry.Info.Add(new ModBiomeBestiaryInfoElement(Mod, ModContent.GetInstance<ContagionCaveDesert>().DisplayName.Value, ModContent.GetInstance<ContagionCaveDesert>().BestiaryIcon, "Assets/Bestiary/ContagionBG", null));
        }
    }
    #region commented out incase i missed something
    //public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    //{
    //    var desertPostBeakCondition = new DesertPostBeakDrop();

    //    globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumHeadgear>(), 150));
    //    globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumPlateMail>(), 150));
    //    globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumGreaves>(), 150));
    //}
    //public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    //{
    //    var hardModeCondition = new HardmodeOnly();
    //    var notFromStatueCondition = new Conditions.NotFromStatue();
    //    var notExpertCondition = new Conditions.NotExpert();

    //    var preHardModeCondition = new Invert(hardModeCondition);
    //    var superHardModeCondition = new Superhardmode();
    //    var hardmodePreSuperHardmodeCondition =
    //        new Combine(true, null, hardModeCondition, new Invert(new Superhardmode()));

    //    int p = Player.FindClosest(npc.position, npc.width, npc.height);

    //    switch (npc.type)
    //    {

    //        case NPCID.BoneSerpentHead:
    //            npcLoot.Add(ItemDropRule.Common(ItemID.Sunfury, 20));
    //            break;
    //        case NPCID.GoblinThief:
    //            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoblinDagger>(), 100));
    //            break;
    //    }

    //    if (npc.type is NPCID.BloodZombie or NPCID.Drippler)
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SanguineKatana>(), 30));
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BloodBarrage>(), 30));
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Vanity.SanguineKabuto>(), 30));
    //    }

    //    #region shards
    //    if (Data.Sets.NPC.Toxic[npc.type])
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToxinShard>(), 8));
    //    }

    //    if (Data.Sets.NPC.Undead[npc.type])
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UndeadShard>(), 11));
    //    }

    //    if (Data.Sets.NPC.Fiery[npc.type])
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireShard>(), 8));
    //    }

    //    if (Data.Sets.NPC.Watery[npc.type])
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterShard>(), 8));
    //    }

    //    if (Data.Sets.NPC.Earthen[npc.type])
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthShard>(), 12));
    //    }

    //    if (Data.Sets.NPC.Flyer[npc.type])
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BreezeShard>(), 13));
    //    }

    //    if (Data.Sets.NPC.Frozen[npc.type])
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostShard>(), 10));
    //    }

    //    if (Data.Sets.NPC.Wicked[npc.type])
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CorruptShard>(), 9));
    //    }

    //    if (Data.Sets.NPC.Arcane[npc.type])
    //    {
    //        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcaneShard>(), 7));
    //    }


    //    #endregion shards


    //}
    #endregion commented out incase i missed something
}
