using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Common;

namespace Avalon.NPCs;

public class Rift : ModNPC
{
    bool[,] copperDone = new bool[21, 21];
    bool[,] ironDone = new bool[21, 21];
    bool[,] silverDone = new bool[21, 21];
    bool[,] goldDone = new bool[21, 21];
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 1;
    }

    public override void SetDefaults()
    {
        NPC.width = NPC.height = 20;
        NPC.noTileCollide = NPC.noGravity = true;
        NPC.npcSlots = 0f;
        NPC.damage = 0;
        NPC.lifeMax = 100;
        NPC.dontTakeDamage = true;
        NPC.alpha = 255;
        NPC.defense = 0;
        NPC.aiStyle = -1;
        NPC.value = 0;
        NPC.knockBackResist = 0f;
        NPC.scale = 1f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath39;
    }

    public override void AI()
    {
        NPC.velocity *= 0f;
        NPC.ai[0]++;
        if (NPC.ai[1] == 0)
        {
            if (NPC.ai[0] % 60 == 0)
            {
                Player p = Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)];
                if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Corruption)
                {
                    if (Main.rand.NextBool(2)) // crimson mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Crimslime);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Herpling);
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IchorSticker);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.FloatyGross);
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(3);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Crimera);
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.FaceMonster);
                            if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.BloodCrawler);
                        }
                    }
                    else // contagion mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Hardmode.Ickslime>());
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Hardmode.Cougher>());
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Hardmode.Viris>());
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(2);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<PreHardmode.Bactus>());
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<PreHardmode.PyrasiteHead>());
                            //if (t == 2) NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.BloodCrawler);
                        }
                    }
                }
                else if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion) // contagion world
                {
                    if (Main.rand.NextBool(2)) // crimson mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Crimslime);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Herpling);
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IchorSticker);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.FloatyGross);
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(3);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Crimera);
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.FaceMonster);
                            if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.BloodCrawler);
                        }
                    }
                    else // corruption mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(3);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.CorruptSlime);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Slimer);
                                if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Corruptor);
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                int t = Main.rand.Next(3);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.SeekerHead);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.CorruptSlime);
                                if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Corruptor);
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(2);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.EaterofSouls);
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DevourerHead);
                            //if (t == 2) NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.BloodCrawler);
                        }
                    }
                }
                else if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Crimson) // crimson
                {
                    if (Main.rand.NextBool(2)) // corruption mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(3);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.CorruptSlime);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Slimer);
                                if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Corruptor);
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                int t = Main.rand.Next(3);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.SeekerHead);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.CorruptSlime);
                                if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Corruptor);
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(2);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.EaterofSouls);
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DevourerHead);
                            //if (t == 2) NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.BloodCrawler);
                        }
                    }
                    else // contagion mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Hardmode.Ickslime>());
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Hardmode.Cougher>());
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Hardmode.Viris>());
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(2);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<PreHardmode.Bactus>());
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<PreHardmode.PyrasiteHead>());
                        }
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
                    Main.dust[num893].velocity *= 2f;
                    Main.dust[num893].scale = 0.9f;
                    Main.dust[num893].noGravity = true;
                    Main.dust[num893].fadeIn = 3f;
                }
                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
            }
        }
        else if (NPC.ai[1] == 1)
        {
            Point tile = NPC.Center.ToTileCoordinates();

            #region copper
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Copper)
            {
                ClassExtensions.RiftReplace(tile, TileID.Copper, TileID.Tin);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tin)
            {
                ClassExtensions.RiftReplace(tile, TileID.Tin, ModContent.TileType<Tiles.Ores.BronzeOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BronzeOre>())
            {
                ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.BronzeOre>(), TileID.Copper);
            }
            #endregion
            #region iron
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Iron)
            {
                ClassExtensions.RiftReplace(tile, TileID.Iron, TileID.Lead);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Lead)
            {
                ClassExtensions.RiftReplace(tile, TileID.Lead, ModContent.TileType<Tiles.Ores.NickelOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.NickelOre>())
            {
                ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.NickelOre>(), TileID.Iron);
            }
            #endregion
            #region silver
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Silver)
            {
                ClassExtensions.RiftReplace(tile, TileID.Silver, TileID.Tungsten);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tungsten)
            {
                ClassExtensions.RiftReplace(tile, TileID.Tungsten, ModContent.TileType<Tiles.Ores.ZincOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.ZincOre>())
            {
                ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.ZincOre>(), TileID.Silver);
            }
            #endregion
            #region gold
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Gold)
            {
                ClassExtensions.RiftReplace(tile, TileID.Gold, TileID.Platinum);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Platinum)
            {
                ClassExtensions.RiftReplace(tile, TileID.Platinum, ModContent.TileType<Tiles.Ores.BismuthOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BismuthOre>())
            {
                ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.BismuthOre>(), TileID.Gold);
            }
            #endregion
            #region rhodium
            if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.RhodiumOre>())
            {
                ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.RhodiumOre>(), ModContent.TileType<Tiles.Ores.OsmiumOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.OsmiumOre>())
            {
                ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.OsmiumOre>(), ModContent.TileType<Tiles.Ores.IridiumOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.IridiumOre>())
            {
                ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.IridiumOre>(), ModContent.TileType<Tiles.Ores.RhodiumOre>());
            }
            #endregion
            #region evil
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Demonite)
            {
                ClassExtensions.RiftReplace(tile, TileID.Demonite, TileID.Crimtane);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Crimtane)
            {
                ClassExtensions.RiftReplace(tile, TileID.Crimtane, ModContent.TileType<Tiles.Ores.BacciliteOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BacciliteOre>())
            {
                ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.BacciliteOre>(), TileID.Demonite);
            }
            #endregion

            #region cobalt (do later)
            //if (Main.tile[tile.X, tile.Y].TileType == TileID.Cobalt)
            //{
            //    ClassExtensions.RiftReplace(tile, TileID.Cobalt, TileID.Palladium);
            //}
            //else if (Main.tile[tile.X, tile.Y].TileType == TileID.Palladium)
            //{
            //    ClassExtensions.RiftReplace(tile, TileID.Palladium, TileID.Cobalt); //ClassExtensions.RiftReplace(tile, TileID.Palladium, ModContent.TileType<Tiles.Ores.DurataniumOre>());
            //}
            //else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.DurataniumOre>())
            //{
            //    ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.DurataniumOre>(), TileID.Cobalt);
            //}
            #endregion
            NPC.ai[1] = 4;
        }
        if (NPC.ai[0] >= 200) NPC.active = false;
    }
}
