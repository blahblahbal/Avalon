using Avalon.Biomes;
using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Avalon.DropConditions;
using Avalon.Items.Accessories;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Accessories.Vanity;
using Avalon.Items.Ammo;
using Avalon.Items.Armor.PreHardmode;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Items.Material.Shards;
using Avalon.Items.Material.TomeMats;
using Avalon.Items.Other;
using Avalon.Items.Placeable.Painting;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Potions.Other;
using Avalon.Items.Tokens;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Magic.PreHardmode;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.NPCs.Hardmode;
using Avalon.NPCs.PreHardmode;
using Avalon.NPCs.TownNPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonGlobalNPC : GlobalNPC
{
    public static float ModSpawnRate { get; set; } = 0.25f;
    public static int BleedTime = 60 * 7;

    private const int RareChance = 700;
    private const int UncommonChance = 50;
    private const int VeryRareChance = 1000;

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
        if (npc.type == NPCID.SkeletronHead && !NPC.downedBoss3)
        {
            AvalonWorld.GenerateSulphur();
        }
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
        if (shop.NpcType == NPCID.Pirate && NPC.downedPirates)
        {
            shop.Add(new Item(ModContent.ItemType<FalseTreasureMap>())
            {
                shopCustomPrice = Item.buyPrice(0, 4),
            });
        }
        if (shop.NpcType == NPCID.GoblinTinkerer && NPC.downedGoblins)
        {
            shop.Add(new Item(ModContent.ItemType<GoblinRetreatOrder>())
            {
                shopCustomPrice = Item.buyPrice(0, 4),
            });
        }
        if (shop.NpcType == NPCID.Dryad && ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion)
        {
            if (shop.TryGetEntry(ItemID.CorruptSeeds, out NPCShop.Entry entry))
            {
                shop.InsertBefore(entry, new Item(ModContent.ItemType<ContagionSeeds>()), Condition.BloodMoon);
                entry.Disable();
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
            int r = Main.rand.Next(7);
            if (r == 0) result += " was assimilated.";
            if (r == 1) result += "'s mechanisms were damaged beyond repair.";
            if (r == 2) result += " was crushed.";
            if (r == 3) result += " is '404 not found.'";
            if (r == 4) result += "'s implants were ripped out.";
            if (r == 5) result += " short-circuited.";
            if (r == 6) result += " malfunctioned.";
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
        if (npc.HasBuff<Bleeding>())
        {
            for (int i = 0; i < npc.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks; i++)
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

    public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
        if (npc.type is NPCID.BloodJelly or NPCID.Unicorn or NPCID.DarkMummy or NPCID.LightMummy && Main.rand.NextBool(9))
        {
            target.AddBuff(ModContent.BuffType<BrokenWeaponry>(), 60 * 7);
        }
        if (npc.type == NPCID.Mummy || npc.type == NPCID.FungoFish || npc.type == NPCID.Clinger) // || npc.type == ModContent.NPCType<GrossyFloat>())
        {
            if (Main.rand.NextBool(9))
            {
                target.AddBuff(ModContent.BuffType<Unloaded>(), 60 * 7);
            }
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

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        var notExpertCondition = new Conditions.NotExpert();
        var contagionCondition = new IsContagion();
        var corruptionCondition = new Conditions.IsCorruptionAndNotExpert();
        var crimsonNotExpert = new Combine(true, null, notExpertCondition, new CrimsonNotContagion());
        var contagionNotExpert = new Combine(true, null, notExpertCondition, contagionCondition);
        var corruptionNotContagion = new Combine(true, null, new Invert(contagionNotExpert), corruptionCondition);

        var hardModeCondition = new HardmodeOnly();
        var notFromStatueCondition = new Conditions.NotFromStatue();
        var preHardModeCondition = new Invert(hardModeCondition);
        //    var superHardModeCondition = new Superhardmode();
        //    var hardmodePreSuperHardmodeCondition =
        //        new Combine(true, null, hardModeCondition, new Invert(new Superhardmode()));

        switch (npc.type)
        {
            case NPCID.WallofFlesh:
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<FleshyTendril>(), 1, 13, 19));
                break;
            case NPCID.TheDestroyer:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SonicHat>(), 25, 1, 1));
                break;
            case NPCID.Retinazer:
            case NPCID.Spazmatism:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SonicShirt>(), 25, 1, 1));
                break;
            case NPCID.SkeletronPrime:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SonicShoes>(), 25, 1, 1));
                break;
            case NPCID.Duck or NPCID.Duck2 or NPCID.DuckWhite or NPCID.DuckWhite2:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Quack>(), VeryRareChance));
                break;
            case NPCID.EaterofSouls:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EvilOuroboros>(), RareChance));
                break;
            case NPCID.KingSlime:
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<BandofSlime>(), 3));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BirthofaMonster>(), 9));
                break;
            case NPCID.DialatedEye:
                npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 40));
                break;
            case NPCID.UndeadMiner:
                //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinersPickaxe>(), 30));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinersSword>(), 30));
                break;
            case NPCID.WyvernHead:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MysticalTotem>(), 2));
                break;
            case NPCID.Harpy:
                npcLoot.Add(ItemDropRule.ByCondition(hardModeCondition, ItemID.ShinyRedBalloon, 50));
                break;
            case NPCID.Vulture:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Beak>(), 3));
                break;
            case NPCID.QueenBee:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FightoftheBumblebee>(), 8));
                break;
            case NPCID.EyeofCthulhu:
                npcLoot.Add(ItemDropRule.ByCondition(
                    preHardModeCondition,
                    ItemID.BloodMoonStarter,
                    10, 1, 1, 3));

                //npcLoot.Add(ItemDropRule.ByCondition(
                //    hardmodePreSuperHardmodeCondition,
                //    ItemID.BloodMoonStarter,
                //    100, 1, 1, 15));

                //npcLoot.Add(ItemDropRule.ByCondition(
                //    superHardModeCondition,
                //    ItemID.BloodMoonStarter,
                //    100, 1, 1, 7));

                break;
        }
        if (npc.type is NPCID.Werewolf or NPCID.AnglerFish or NPCID.RustyArmoredBonesAxe or
            NPCID.RustyArmoredBonesFlail or NPCID.RustyArmoredBonesSword or NPCID.RustyArmoredBonesSwordNoArmor)
        {
            LeadingConditionRule AdhesiveBandage = new LeadingConditionRule(new CloverPotionActive());
            AdhesiveBandage.OnSuccess(ItemDropRule.StatusImmunityItem(885, 50), true);
            AdhesiveBandage.OnFailedConditions(ItemDropRule.StatusImmunityItem(885, 100));
            npcLoot.Add(AdhesiveBandage);
        }
        if (npc.type is NPCID.ArmoredSkeleton or NPCID.BlueArmoredBones or NPCID.BlueArmoredBonesMace or NPCID.BlueArmoredBonesNoPants or NPCID.BlueArmoredBonesSword)
        {
            LeadingConditionRule ArmorPolish = new LeadingConditionRule(new CloverPotionActive());
            ArmorPolish.OnSuccess(ItemDropRule.StatusImmunityItem(886, 50), true);
            ArmorPolish.OnFailedConditions(ItemDropRule.StatusImmunityItem(886, 100));
            npcLoot.Add(ArmorPolish);
        }
        if (npc.type is NPCID.ToxicSludge or NPCID.MossHornet or NPCID.Hornet or NPCID.HornetFatty or NPCID.HornetHoney or NPCID.HornetLeafy or NPCID.HornetSpikey or NPCID.HornetStingy)
        {
            LeadingConditionRule Bezoar = new LeadingConditionRule(new CloverPotionActive());
            Bezoar.OnSuccess(ItemDropRule.StatusImmunityItem(887, 50), true);
            Bezoar.OnFailedConditions(ItemDropRule.StatusImmunityItem(887, 100));
            npcLoot.Add(Bezoar);
        }
        if (npc.type is NPCID.CorruptSlime or NPCID.DarkMummy or NPCID.Crimslime or NPCID.BloodMummy)
        {
            LeadingConditionRule Blindfold = new LeadingConditionRule(new CloverPotionActive());
            Blindfold.OnSuccess(ItemDropRule.StatusImmunityItem(888, 50), true);
            Blindfold.OnFailedConditions(ItemDropRule.StatusImmunityItem(888, 100));
            npcLoot.Add(Blindfold);
        }
        if (npc.type is NPCID.Mummy or NPCID.Wraith or NPCID.Pixie)
        {
            LeadingConditionRule FastClock = new LeadingConditionRule(new CloverPotionActive());
            FastClock.OnSuccess(ItemDropRule.StatusImmunityItem(889, 50), true);
            FastClock.OnFailedConditions(ItemDropRule.StatusImmunityItem(889, 100));
            npcLoot.Add(FastClock);
        }
        if (npc.type is NPCID.GreenJellyfish or NPCID.Pixie or NPCID.DarkMummy or NPCID.BloodMummy)
        {
            LeadingConditionRule Megaphone = new LeadingConditionRule(new CloverPotionActive());
            Megaphone.OnSuccess(ItemDropRule.StatusImmunityItem(890, 50), true);
            Megaphone.OnFailedConditions(ItemDropRule.StatusImmunityItem(890, 100));
            npcLoot.Add(Megaphone);
        }
        if (npc.type is NPCID.CursedSkull or NPCID.CursedHammer or NPCID.EnchantedSword or NPCID.CrimsonAxe or NPCID.GiantCursedSkull)
        {
            LeadingConditionRule Nazar = new LeadingConditionRule(new CloverPotionActive());
            Nazar.OnSuccess(ItemDropRule.StatusImmunityItem(891, 50), true);
            Nazar.OnFailedConditions(ItemDropRule.StatusImmunityItem(891, 100));
            npcLoot.Add(Nazar);
        }
        if (npc.type is NPCID.Corruptor or NPCID.FloatyGross)
        {
            LeadingConditionRule Vitamins = new LeadingConditionRule(new CloverPotionActive());
            Vitamins.OnSuccess(ItemDropRule.StatusImmunityItem(892, 50), true);
            Vitamins.OnFailedConditions(ItemDropRule.StatusImmunityItem(892, 100));
            npcLoot.Add(Vitamins);
        }
        if (npc.type is NPCID.GiantBat or NPCID.Clown or NPCID.LightMummy)
        {
            LeadingConditionRule TrifoldMap = new LeadingConditionRule(new CloverPotionActive());
            TrifoldMap.OnSuccess(ItemDropRule.StatusImmunityItem(893, 50), true);
            TrifoldMap.OnFailedConditions(ItemDropRule.StatusImmunityItem(893, 100));
            npcLoot.Add(TrifoldMap);
        }
        if (npc.type is NPCID.BloodJelly or NPCID.Unicorn or NPCID.DarkMummy or NPCID.LightMummy or NPCID.BloodMummy)
        {
            LeadingConditionRule HiddenBlade = new LeadingConditionRule(new CloverPotionActive());
            HiddenBlade.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<HiddenBlade>(), 50), true);
            HiddenBlade.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<HiddenBlade>(), 100));
            npcLoot.Add(HiddenBlade);
        }
        if (npc.type == NPCID.Mummy || npc.type == NPCID.FungoFish || npc.type == NPCID.Clinger)
        {
            LeadingConditionRule AmmoMagazine = new LeadingConditionRule(new CloverPotionActive());
            AmmoMagazine.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<AmmoMagazine>(), 50), true);
            AmmoMagazine.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<AmmoMagazine>(), 100));
            npcLoot.Add(AmmoMagazine);
        }
        //greek extinguisher
        if (npc.type == NPCID.Clinger || npc.type == NPCID.Spazmatism || npc.type == ModContent.NPCType<CursedFlamer>())
        {
            LeadingConditionRule GreekExtinguisher = new LeadingConditionRule(new CloverPotionActive());
            GreekExtinguisher.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<GreekExtinguisher>(), 25), true);
            GreekExtinguisher.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<GreekExtinguisher>(), 50));
            npcLoot.Add(GreekExtinguisher);
        }

        //600 watt lightbulb
        if (npc.type == NPCID.RaggedCaster || npc.type == NPCID.RaggedCasterOpenCoat) // || npc.type == ModContent.NPCType<DarkMatterSlime>())
        {
            LeadingConditionRule SixHundredWattLightbulb = new LeadingConditionRule(new CloverPotionActive());
            SixHundredWattLightbulb.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<SixHundredWattLightbulb>(), 25), true);
            SixHundredWattLightbulb.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<SixHundredWattLightbulb>(), 50));
            npcLoot.Add(SixHundredWattLightbulb);
        }

        if (npc.type == NPCID.EyeofCthulhu)
        {
            // remove corruption loot
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.DemoniteOre);
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CorruptSeeds);
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.UnholyArrow);

            // remove crimson loot
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimtaneOre);
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimsonSeeds);

            // add corruption loot back
            LeadingConditionRule corruptionRule = new LeadingConditionRule(corruptionNotContagion);
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.DemoniteOre, 1, 30, 90));
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.CorruptSeeds, 1, 1, 3));
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.UnholyArrow, 1, 20, 50));
            npcLoot.Add(corruptionRule);

            // add crimson loot back
            LeadingConditionRule crimsonRule = new LeadingConditionRule(crimsonNotExpert);
            crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.CrimtaneOre, 1, 30, 90));
            crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.CrimsonSeeds, 1, 1, 3));
            crimsonRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BloodyArrow>(), 1, 20, 50));
            npcLoot.Add(crimsonRule);

            // add contagion loot
            LeadingConditionRule contagionRule = new LeadingConditionRule(contagionCondition);
            contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 1, 30, 90));
            contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ContagionSeeds>(), 1, 1, 3));
            npcLoot.Add(contagionRule);
        }

        #region mystical tome mats
        if (npc.type is NPCID.ManEater or NPCID.Snatcher or NPCID.AngryTrapper)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notFromStatueCondition, ModContent.ItemType<DewOrb>(), 25, 1, 1, 4));
        }

        if (npc.type is NPCID.GiantTortoise or NPCID.IceTortoise or NPCID.Vulture or NPCID.FlyingFish
            or NPCID.Unicorn)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementDust>(), 15));
        }

        if (npc.type is NPCID.Harpy or NPCID.CaveBat or NPCID.GiantBat or NPCID.JungleBat)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RubybeadHerb>(), 15));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MysticalClaw>(), 20));
        }

        if (npc.type is NPCID.Hornet or NPCID.BlackRecluse or NPCID.MossHornet or NPCID.HornetFatty
            or NPCID.HornetHoney
            or NPCID.HornetLeafy or NPCID.HornetSpikey or NPCID.HornetStingy or NPCID.JungleCreeper
            or NPCID.JungleCreeperWall or NPCID.BlackRecluseWall)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StrongVenom>(), 15));
        }

        if (npc.type is NPCID.Retinazer or NPCID.Spazmatism or NPCID.SkeletronPrime or NPCID.TheDestroyer)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScrollofTome>(), 8));
        }

        if (npc.type is NPCID.CorruptSlime or NPCID.Gastropod or NPCID.IlluminantSlime or NPCID.ToxicSludge or NPCID.Crimslime
            or NPCID.RainbowSlime or NPCID.FloatyGross)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notFromStatueCondition, ModContent.ItemType<DewofHerbs>(),
                25, 1, 1, 4));
        }
        if (npc.type is NPCID.ChaosElemental or NPCID.IceElemental or NPCID.IchorSticker or NPCID.Corruptor ||
            npc.type == ModContent.NPCType<Viris>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementDiamond>(), 6));
        }
        #endregion mystical tome mats

        if (npc.boss)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<StaminaCrystal>(), 4));
        }
    }
    public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    {
        var desertPostBeakCondition = new DesertPostBeakDrop();
        var contagionCondition = new ZoneContagion();

        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumHeadgear>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumPlateMail>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumGreaves>(), 150));

        

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.JungleKey);
        LeadingConditionRule JungleKeyRule = new LeadingConditionRule(new CloverPotionActive());
        JungleKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.JungleKey, 1250, 1, 1, new Conditions.JungleKeyCondition()), true);
        JungleKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.JungleKey, 2500, 1, 1, new Conditions.JungleKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CorruptionKey);
        LeadingConditionRule CorruptKeyRule = new LeadingConditionRule(new CloverPotionActive());
        CorruptKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.CorruptionKey, 1250, 1, 1, new Conditions.CorruptKeyCondition()), true);
        CorruptKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.CorruptionKey, 2500, 1, 1, new Conditions.CorruptKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.FrozenKey);
        LeadingConditionRule FrozenKeyRule = new LeadingConditionRule(new CloverPotionActive());
        FrozenKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.FrozenKey, 1250, 1, 1, new Conditions.FrozenKeyCondition()), true);
        FrozenKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.FrozenKey, 2500, 1, 1, new Conditions.FrozenKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimsonKey);
        LeadingConditionRule CrimsonKeyRule = new LeadingConditionRule(new CloverPotionActive());
        CrimsonKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.CrimsonKey, 1250, 1, 1, new Conditions.CrimsonKeyCondition()), true);
        CrimsonKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.CrimsonKey, 2500, 1, 1, new Conditions.CrimsonKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.HallowedKey);
        LeadingConditionRule HallowKeyRule = new LeadingConditionRule(new CloverPotionActive());
        HallowKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.HallowedKey, 1250, 1, 1, new Conditions.HallowKeyCondition()), true);
        HallowKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.HallowedKey, 2500, 1, 1, new Conditions.HallowKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.DungeonDesertKey);
        LeadingConditionRule DesertKeyRule = new LeadingConditionRule(new CloverPotionActive());
        DesertKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.DungeonDesertKey, 1250, 1, 1, new Conditions.DesertKeyCondition()), true);
        DesertKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.DungeonDesertKey, 2500, 1, 1, new Conditions.DesertKeyCondition()), true);

        globalLoot.Add(FrozenKeyRule);
        globalLoot.Add(JungleKeyRule);
        globalLoot.Add(CorruptKeyRule);
        globalLoot.Add(HallowKeyRule);
        globalLoot.Add(CrimsonKeyRule);
        globalLoot.Add(DesertKeyRule);

        if (ExxoAvalonOrigins.Tokens != null)
        {
            //globalLoot.Add(ItemDropRule.ByCondition(
            //    new PostPhantasmHellcastleTokenDrop(),
            //    ModContent.ItemType<HellcastleToken>(), 15));
            //globalLoot.Add(ItemDropRule.ByCondition(
            //    new SuperhardmodePreArmaTokenDrop(),
            //    ModContent.ItemType<SuperhardmodeToken>(), 15));
            //globalLoot.Add(ItemDropRule.ByCondition(new PostArmageddonTokenDrop(), ModContent.ItemType<DarkMatterToken>(),
            //    15));
            //globalLoot.Add(ItemDropRule.ByCondition(new PostMechastingTokenDrop(), ModContent.ItemType<MechastingToken>(),
            //    15));
            //globalLoot.Add(ItemDropRule.ByCondition(new ZoneTropicsToken(), ModContent.ItemType<TropicsToken>(), 15));
            globalLoot.Add(ItemDropRule.ByCondition(
                new UndergroundHardmodeContagionTokenDrop(contagionCondition),
                ModContent.ItemType<ContagionToken>(), 15));
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
