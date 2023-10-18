using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class BaskingSpewer : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 4;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        Data.Sets.NPC.Wicked[NPC.type] = true;
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.SandShark);
        NPC.aiStyle = -1;
        NPC.damage = 67;
        NPC.defense = 23;
        NPC.lifeMax = 420;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 400f;
        NPC.npcSlots = 0.5f;
        NPC.noGravity = true;
        NPC.behindTiles = true;
        AIType = NPCID.SandsharkCorrupt;
        AnimationType = NPCID.SandsharkCorrupt;
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.ContagionDesert>().Type };
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].HasTile && spawnInfo.Player.InModBiome<Biomes.ContagionDesert>() && Main.hardMode ? 0.2f : 0f;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.BaskingSpewer"))
        });
    }
    public static bool SandTiles(int type)
    {
        return TileID.Sets.Conversion.Sand[type] || TileID.Sets.Conversion.Sandstone[type] || TileID.Sets.Conversion.HardenedSand[type];
    }
    public override void AI()
    {
        if (NPC.direction == 0)
        {
            NPC.TargetClosest();
        }
        bool flag11 = true;
        Point pt = NPC.Center.ToTileCoordinates();
        Tile tileSafely7 = Framing.GetTileSafely(pt);
        flag11 = tileSafely7.HasUnactuatedTile && SandTiles(tileSafely7.TileType);
        if (flag11)
        {
            NPC.noTileCollide = true;
        }
        flag11 |= NPC.wet;
        bool flag12 = false;
        NPC.TargetClosest(faceTarget: false);
        Vector2 vector233 = NPC.targetRect.Center.ToVector2();
        if (Main.player[NPC.target].velocity.Y > -0.1f && !Main.player[NPC.target].dead && NPC.Distance(vector233) > 150f)
        {
            flag12 = true;
        }
        if (NPC.localAI[0] == -1f && !flag11)
        {
            NPC.localAI[0] = 20f;
        }
        if (NPC.localAI[0] > 0f)
        {
            NPC.localAI[0]--;
        }
        if (flag11)
        {
            if (NPC.soundDelay == 0)
            {
                float num648 = NPC.Distance(vector233) / 40f;
                if (num648 < 10f)
                {
                    num648 = 10f;
                }
                if (num648 > 20f)
                {
                    num648 = 20f;
                }
                NPC.soundDelay = (int)num648;
                SoundEngine.PlaySound(SoundID.WormDig, NPC.Center);
            }
            float num649 = NPC.ai[1];
            bool flag14 = false;
            pt = (NPC.Center + new Vector2(0f, 24f)).ToTileCoordinates();
            tileSafely7 = Framing.GetTileSafely(pt.X, pt.Y);
            if (tileSafely7.HasUnactuatedTile && SandTiles(tileSafely7.TileType))
            {
                flag14 = true;
            }
            NPC.ai[1] = flag14.ToInt();
            if (NPC.ai[2] < 30f)
            {
                NPC.ai[2]++;
            }
            if (flag12)
            {
                NPC.TargetClosest();
                NPC.velocity.X += (float)NPC.direction * 0.15f;
                NPC.velocity.Y += (float)NPC.directionY * 0.15f;
                if (NPC.velocity.X > 5f)
                {
                    NPC.velocity.X = 5f;
                }
                if (NPC.velocity.X < -5f)
                {
                    NPC.velocity.X = -5f;
                }
                if (NPC.velocity.Y > 3f)
                {
                    NPC.velocity.Y = 3f;
                }
                if (NPC.velocity.Y < -3f)
                {
                    NPC.velocity.Y = -3f;
                }
                Vector2 center44 = NPC.Center;
                Vector2 val103 = NPC.velocity.SafeNormalize(Vector2.Zero);
                Vector2 val29 = NPC.Size;
                Vector2 vec4 = center44 + val103 * val29.Length() / 2f + NPC.velocity;
                pt = vec4.ToTileCoordinates();
                tileSafely7 = Framing.GetTileSafely(pt);
                bool flag15 = tileSafely7.HasUnactuatedTile && (TileID.Sets.Conversion.Sand[tileSafely7.TileType] || TileID.Sets.Conversion.Sandstone[tileSafely7.TileType] || TileID.Sets.Conversion.HardenedSand[tileSafely7.TileType]);
                Tile tileSafely0 = Framing.GetTileSafely(NPC.Center.ToTileCoordinates() + new Point(0, 3));
                bool flag1010 = tileSafely0.HasUnactuatedTile && (TileID.Sets.Conversion.Sand[tileSafely0.TileType] || TileID.Sets.Conversion.Sandstone[tileSafely0.TileType] || TileID.Sets.Conversion.HardenedSand[tileSafely0.TileType]);
                if (flag15 || flag1010)
                {
                    NPC.noTileCollide = true;
                }
                if (!flag15 && NPC.wet)
                {
                    flag15 = tileSafely7.LiquidAmount > 0;
                }
                int num650 = 400;
                if (Main.remixWorld)
                {
                    num650 = 700;
                }
                if (!flag15 && Math.Sign(NPC.velocity.X) == NPC.direction && NPC.Distance(vector233) < (float)num650 && (NPC.ai[2] >= 30f || NPC.ai[2] < 0f))
                {
                    if (NPC.localAI[0] == 0f)
                    {
                        //SoundEngine.PlaySound(14, NPC.Center, 542);
                        NPC.localAI[0] = -1f;
                    }
                    NPC.ai[2] = -30f;
                    Vector2 vector234 = NPC.DirectionTo(vector233 + new Vector2(0f, -80f));
                    NPC.velocity = vector234 * 12f;
                }
            }
            else
            {
                if (NPC.collideX)
                {
                    NPC.velocity.X *= -1f;
                    NPC.direction *= -1;
                    NPC.netUpdate = true;
                }
                if (NPC.collideY)
                {
                    NPC.netUpdate = true;
                    NPC.velocity.Y *= -1f;
                    NPC.directionY = Math.Sign(NPC.velocity.Y);
                    NPC.ai[0] = NPC.directionY;
                }
                float num651 = 6f;
                NPC.velocity.X += (float)NPC.direction * 0.1f;
                if (NPC.velocity.X < 0f - num651 || NPC.velocity.X > num651)
                {
                    NPC.velocity.X *= 0.95f;
                }
                if (flag14)
                {
                    NPC.ai[0] = -1f;
                }
                else
                {
                    NPC.ai[0] = 1f;
                }
                float num652 = 0.06f;
                float num653 = 0.01f;
                if (NPC.ai[0] == -1f)
                {
                    NPC.velocity.Y -= num653;
                    if (NPC.velocity.Y < 0f - num652)
                    {
                        NPC.ai[0] = 1f;
                    }
                }
                else
                {
                    NPC.velocity.Y += num653;
                    if (NPC.velocity.Y > num652)
                    {
                        NPC.ai[0] = -1f;
                    }
                }
                if (NPC.velocity.Y > 0.4f || NPC.velocity.Y < -0.4f)
                {
                    NPC.velocity.Y *= 0.95f;
                }
            }
        }
        else
        {
            if (NPC.velocity.Y == 0f)
            {
                if (flag12)
                {
                    NPC.TargetClosest();
                }
                float num654 = 1f;
                NPC.velocity.X += (float)NPC.direction * 0.1f;
                if (NPC.velocity.X < 0f - num654 || NPC.velocity.X > num654)
                {
                    NPC.velocity.X *= 0.95f;
                }
            }
            NPC.velocity.Y += 0.3f;
            if (NPC.velocity.Y > 10f)
            {
                NPC.velocity.Y = 10f;
            }
            NPC.ai[0] = 1f;
        }
        NPC.rotation = NPC.velocity.Y * (float)NPC.direction * 0.1f;
        if (NPC.rotation < -0.2f)
        {
            NPC.rotation = -0.2f;
        }
        if (NPC.rotation > 0.2f)
        {
            NPC.rotation = 0.2f;
        }
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            return;
        }
        else if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerHead").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerBody").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerFin").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerFin2").Type, NPC.scale);
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.SharkFin, 8));
        npcLoot.Add(ItemDropRule.Common(ItemID.Nachos, 33));
        npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 25));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), ItemID.KiteSandShark, 25));
    }
}
