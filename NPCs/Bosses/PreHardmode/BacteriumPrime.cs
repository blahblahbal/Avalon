using System;
using System.IO;
using Avalon.Biomes;
using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Items.BossBags;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Items.Placeable.Trophy.Relics;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Vanity;
using Avalon.NPCs.Hardmode;
using Avalon.NPCs.PreHardmode;
using Avalon.Projectiles.Hostile.BacteriumPrime;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Avalon.Items.Pets;

namespace Avalon.NPCs.Bosses.PreHardmode;
public class ContagionLenient : ModBiome
{
    public override int Music => -1;
    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<BiomeTileCounts>().ContagionTiles >= 75;
    }
}

[AutoloadBossHead]
public class BacteriumPrime : ModNPC
{
    public static int secondStageHeadSlot = -1;
    public override void Load()
    {
        secondStageHeadSlot = Mod.AddBossHeadTexture(BossHeadTexture + "_2", -1);
    }
    public override void BossHeadSlot(ref int index)
    {
        if (NPC.ai[3] == 60)
        {
            index = secondStageHeadSlot;
        }
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.BacteriumPrime"))
        });
    }
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Bacterium Prime");

        // Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
        NPCID.Sets.MPAllowedEnemies[Type] = true;
        // Automatically group with other bosses
        NPCID.Sets.BossBestiaryPriority.Add(Type);

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            PortraitScale = 0.7f,
            PortraitPositionYOverride = -14,
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

        Main.npcFrameCount[NPC.type] = 8;
        NPCID.Sets.TrailCacheLength[NPC.type] = 12;
        NPCID.Sets.TrailingMode[NPC.type] = 7;

        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
    }
    public override void SetDefaults()
    {
        NPC.damage = 27;
        NPC.boss = true;
        NPC.noTileCollide = true;
        NPC.lifeMax = 3100;
        NPC.defense = 9;
        NPC.noGravity = true;
        NPC.width = 120;
        NPC.aiStyle = -1;
        NPC.npcSlots = 6f;
        NPC.value = 50000f;
        NPC.timeLeft = NPC.activeTime * 30;
        NPC.height = 120;
        NPC.HitSound = SoundID.NPCHit8;
        NPC.DeathSound = SoundID.NPCDeath21;
        NPC.knockBackResist = 0f;
        DrawOffsetY = 14;
        NPC.timeLeft = 200000;
        Music = ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/BacteriumPrime") : MusicID.Boss5;
        SpawnModBiomes = new int[] { ModContent.GetInstance<Contagion>().Type, ModContent.GetInstance<UndergroundContagion>().Type };
        //bossBag = ModContent.ItemType<Items.BossBags.BacteriumPrimeBossBag>();
    }
    public override void OnKill()
    {
        if (!NPC.downedBoss2)
        {
            NPC.SetEventFlagCleared(ref NPC.downedBoss2, -1);
        }
        if (!ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime)
        {
            NPC.SetEventFlagCleared(ref ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime, -1);
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacteriumPrimeTrophy>(), 10));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacteriumPrimeMask>(), 7));
        npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<BacteriumPrimeBossBag>()));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 1, 15, 41));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Booger>(), 1, 12, 17));
        npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<BacteriumPrimeRelic>()));
        npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<PetriDish>(), 4));
    }
    const float Phase2Health = 0.6f;
    const float Phase2part2Health = 0.3f;
    const float Phase2part3Health = 0.15f;
    float LorR = MathHelper.PiOver4;
    public override void AI()
    {
        int projDmg = 8;
        float speed;

        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].InModBiome(ModContent.GetInstance<ContagionLenient>()))
        {
            NPC.TargetClosest(false);
        }
        Player Target = Main.player[NPC.target];

        if (Target.dead || (!Main.player[NPC.target].InModBiome(ModContent.GetInstance<ContagionLenient>())))
            NPC.ai[3] = -2;
        if (NPC.ai[3] == -2)
        {
            NPC.noTileCollide = true;
            NPC.velocity.Y += 0.1f;
            NPC.velocity.X *= 0.99f;
            if (NPC.timeLeft > 60)
            {
                NPC.timeLeft = 60;
            }
        }

        #region Phase 1
        if (Main.expertMode)
            speed = 5;
        else
            speed = 4;

        if (NPC.ai[3] == 0)
        {
            if (NPC.noTileCollide || NPC.Center.Distance(Target.Center) > 40 * 16)
                NPC.velocity += NPC.Center.DirectionTo(Target.Center) * 0.13f;
            else
            {
                NPC.velocity += NPC.Center.DirectionTo(Target.Center) * 0.023f;
            }

            //Main.NewText(LastCollide.X + " " + LastCollide.Y);
            //if (NPC.collideY || NPC.collideX)
            //{
            //    if (Vector2.Distance(LastCollide, NPC.position) < 12000)
            //        NPC.noTileCollide = true;
            //    Main.NewText(Vector2.Distance(LastCollide, NPC.position));
            //}

            if (NPC.collideX)
            {
                NPC.velocity.X = NPC.oldVelocity.X * -1.2f;
            }
            if (NPC.collideY)
            {
                NPC.velocity.Y = NPC.oldVelocity.Y * -1.2f;
            }
            if(NPC.collideX || NPC.collideY)
            {
                if (Main.rand.NextBool(6))
                    NPC.velocity = new Vector2(Main.rand.NextFloat(speed / 2, speed), 0).RotatedBy(NPC.Center.DirectionTo(Target.Center).ToRotation());
                NPC.ai[1] += 100;
            }
            if (NPC.noTileCollide)
            {
                NPC.alpha = (int)(MathHelper.Lerp(128, NPC.alpha, 0.9f));
            }
            else
            {
                NPC.alpha = (int)(MathHelper.Lerp(0, NPC.alpha, 0.9f));
            }

            if (Collision.SolidCollision(NPC.position + new Vector2(20, 20), NPC.height - 40, NPC.width - 40) && NPC.ai[1] > 360)
            {
                NPC.ai[1] = -Main.rand.Next(-100, 100);
                NPC.noTileCollide = true;
            }
            NPC.ai[1]++;
            if (NPC.ai[1] > 360 && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                //NPC.ai[1] = 0;
                NPC.noTileCollide = false;
            }
            if (NPC.ai[1] > 1400)
            {
                NPC.ai[1] = -Main.rand.Next(-100, 100);
                NPC.noTileCollide = true;
            }

            if (Main.rand.NextBool(20) && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width * 0.6f, NPC.height * 0.6f), Main.rand.NextVector2Circular(1, 1), ModContent.ProjectileType<BacteriumGas>(), projDmg, 0, -1,1);
            }

            NPC.ai[0]++;
            if (NPC.ai[0] > 570)
            {
                float rotate = Main.rand.NextFloat(0, MathHelper.TwoPi);
                int d = Dust.NewDust(NPC.Center + new Vector2(0, 180).RotatedBy(rotate), 0, 0, DustID.CorruptGibs, 0, 0, 50, default, 2);
                Main.dust[d].velocity = new Vector2(0, -13).RotatedBy(rotate) + NPC.velocity;
                Main.dust[d].noGravity = true;
            }
            if (NPC.ai[0] == 630)
            {
                if (Main.rand.NextBool())
                {
                    LorR *= -1;
                }
                NPC.netUpdate = true;
                NPC.ai[0] = -60;
                NPC.velocity = NPC.Center.DirectionTo(Target.Center).RotatedBy(LorR) * MathHelper.Clamp((NPC.Center.Distance(Target.position) * 0.025f),10,60);
                SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.position);
            }
            if (NPC.ai[0] < -5)
            {
                NPC.ai[1] = -Main.rand.Next(50, 200);
                NPC.noTileCollide = true;
                speed = -NPC.ai[0];
                NPC.velocity = NPC.velocity.RotatedBy(LorR * Main.rand.NextFloat(-0.001f,-0.025f));
            }
            if (NPC.ai[0] % 4 == 0 && Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] < 60)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width * 0.6f, NPC.height * 0.6f), Main.rand.NextVector2Circular(2, 2), ModContent.ProjectileType<BacteriumGas>(), projDmg, 0, -1, 1);
            }

            //NPC.ai[0]++;
            //if (NPC.ai[0] > 120 && BactusCount < MaxBacCount)
            //{
            //    NPC.ai[0] = 0;
            //    NPC Summon = NPC.NewNPCDirect(NPC.GetSource_FromThis(),NPC.Center + Main.rand.NextVector2CircularEdge(1000,1000),ModContent.NPCType<BactusMinion>(),0,0,0,0,0,NPC.target);
            //    Summon.velocity = Summon.position.DirectionTo(Target.position) * 6;
            //    for(int i = 0; i < 20; i++)
            //    {
            //        Dust summonDust = Dust.NewDustPerfect(Summon.Center, DustID.CorruptGibs, Main.rand.NextVector2Circular(8, 8), 128, default, 1.5f);
            //        summonDust.noGravity = true;
            //    }
            //}
            NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -speed, speed);
            NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -speed, speed);
        }
        #endregion Phase 1

        if (NPC.life <= NPC.lifeMax * Phase2Health && NPC.ai[3] < 60 && NPC.ai[3] >= 0)
        {
            NPC.alpha = (int)(MathHelper.Lerp(64, NPC.alpha, 0.9f));
            NPC.ai[3]++;
            NPC.velocity *= 0.98f;

            float rotate = Main.rand.NextFloat(0, MathHelper.TwoPi);
            int d = Dust.NewDust(NPC.Center + new Vector2(0, 180).RotatedBy(rotate), 0, 0, DustID.CorruptGibs, 0, 0, 50, default, 2);
            Main.dust[d].velocity = new Vector2(0, -13).RotatedBy(rotate) + NPC.velocity;
            Main.dust[d].noGravity = true;
            if (Main.rand.NextBool(3) && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width * 0.6f, NPC.height * 0.6f), Main.rand.NextVector2Circular(3, 3), ModContent.ProjectileType<BacteriumGas>(), projDmg, 0, -1, 1);
            }
        }
        if (NPC.ai[3] == 59)
        {
            NPC.ai[0] = 0;
            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            //NPC.knockBackResist = 0f;
            NPC.TargetClosest(false);
            NPC.defense += 8;
            NPC.noTileCollide = true;
            //NPC.knockBackResist += 0.03f;
            SoundEngine.PlaySound(SoundID.ForceRoar);
        }

        #region Phase 2
        if (Main.expertMode)
            speed = 5.3f;
        else
            speed = 4.3f;
        if (NPC.life <= NPC.lifeMax * Phase2part2Health)
            speed += 1.3f;
        if (NPC.life <= NPC.lifeMax * Phase2part3Health && Main.expertMode)
            speed += 0.3f;

        if (NPC.ai[3] == 60)
        {
            if (!Collision.SolidCollision(NPC.position + new Vector2(NPC.width / 3, NPC.height / 3), NPC.width / 3, NPC.height / 3) || NPC.ai[1] > 199)
            {
                if (NPC.ai[1] < 190 && NPC.life < Phase2part2Health * NPC.lifeMax)
                    NPC.ai[1] += 4;
                if (NPC.ai[1] < 190 && NPC.life < Phase2part3Health * NPC.lifeMax)
                    NPC.ai[1] += 4;
                NPC.ai[1]++;
            }

            if (Main.rand.NextBool(30) && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width * 0.6f, NPC.height * 0.6f), Main.rand.NextVector2Circular(1, 1), ModContent.ProjectileType<BacteriumGas>(), projDmg, 0, -1,1);
            }

            int MushroomCount = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].type == ModContent.ProjectileType<MushroomWall>() || Main.projectile[i].type == ModContent.ProjectileType<SporeSeed>())
                {
                    MushroomCount++;
                }
            }

            if (NPC.ai[1] > 200)
            {
                if (NPC.ai[0] == 0)
                {
                    float rotate = Main.rand.NextFloat(0, MathHelper.TwoPi);
                    int d = Dust.NewDust(NPC.Center + new Vector2(0, 120).RotatedBy(rotate), 0, 0, DustID.PirateStaff, 0, 0, 50, default, 2);
                    Main.dust[d].velocity = new Vector2(0, -9).RotatedBy(rotate) + NPC.velocity;
                    Main.dust[d].noGravity = true;
                    //Main.dust[d].fadeIn = 1.2f;
                }
                else if(MushroomCount < 2 && NPC.position.Y < Main.worldSurface * 16 + 30 * 16 && Main.rand.NextBool())
                {
                    float rotate = Main.rand.NextFloat(0, MathHelper.TwoPi);
                    int d = Dust.NewDust(NPC.Center + new Vector2(0, 120).RotatedBy(rotate), 0, 0, ModContent.DustType<ContagionWeapons>(), 0, 0, 50, default, 2);
                    Main.dust[d].velocity = new Vector2(0, -9).RotatedBy(rotate) + NPC.velocity;
                    Main.dust[d].noGravity = true;
                }
                else
                {
                    float rotate = Main.rand.NextFloat(0, MathHelper.TwoPi);
                    int d = Dust.NewDust(NPC.Center + new Vector2(0, 120).RotatedBy(rotate), 0, 0, DustID.CorruptGibs, 0, 0, 50, default, 2);
                    Main.dust[d].velocity = new Vector2(0, -9).RotatedBy(rotate) + NPC.velocity;
                    Main.dust[d].noGravity = true;
                }
                NPC.velocity *= 0.94f;
            }
            if (NPC.ai[1] >= 240)
            {
                SoundEngine.PlaySound(SoundID.Item95, NPC.Center);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (NPC.ai[0] == 0)
                    {
                        for (int i = 0; i < Main.rand.Next(2, 4); i++)
                        {
                            Vector2 ShootDirection = NPC.Center.DirectionTo(Target.Center).RotatedByRandom(0.3f) * Main.rand.NextFloat(6, 3) + new Vector2(0, Main.rand.NextFloat(-4, 0));
                            int M = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, ShootDirection, ModContent.ProjectileType<CorrosiveMucus>(), (int)(NPC.damage * 0.3f), 0, -1);
                            //if(Main.expertMode)
                            //Main.projectile[M].damage = (int)(Main.projectile[M].damage * 0.5f);
                        }
                        //for (int i = 0; i < Main.rand.Next(3, 6); i++)
                        //{
                        //    Vector2 ShootDirection = NPC.Center.DirectionTo(Target.Center).RotatedByRandom(0.1f) * Main.rand.NextFloat(9, 8);
                        //    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, ShootDirection, ModContent.ProjectileType<BacteriumGas>(), (int)(NPC.damage * 0.2f), 0, -1,1);
                        //}
                    }
                    else if (MushroomCount < 2 && NPC.position.Y < Main.worldSurface * 16 + 30 * 16)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center,
                                new Vector2(Main.rand.NextFloat(4f, 7f), 0).RotatedBy(NPC.Center.DirectionTo(Target.Center).ToRotation()),
                                ModContent.ProjectileType<SporeSeed>(), projDmg, 0, 255, Target.Center.X);
                        }
                    }
                    else
                    {
                        int Balls = 3;
                        if (Main.expertMode)
                            Balls = Main.rand.Next(3, 5);
                        for (int i = 0; i < Balls; i++)
                        {
                            // Bouncy Ball
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int P = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(Main.rand.NextFloat(4f, 7f), 0).RotatedBy(NPC.Center.DirectionTo(Target.Center).ToRotation() + MathHelper.Pi / (Balls * 2) - (i * MathHelper.Pi / (Balls * 2))), ModContent.ProjectileType<BouncyBoogerBall>(), projDmg, 0, 255);
                                Main.projectile[P].timeLeft = Main.rand.Next(300, 400);
                                Main.projectile[P].ai[0] = Main.rand.Next(1, 3);
                                if (Main.expertMode)
                                {
                                    if (Main.rand.NextBool())
                                    {
                                        Main.projectile[P].velocity *= 1.5f;
                                        Main.projectile[P].timeLeft = (int)(Main.projectile[P].timeLeft * 1.5f);
                                    }
                                }
                            }
                        }
                    }
                }
                NPC.ai[0] = Main.rand.Next(3);
                if (NPC.life >= NPC.lifeMax * Phase2part2Health)
                    NPC.ai[1] = Main.rand.Next(-60, 60);
                else
                {
                    NPC.ai[1] = Main.rand.Next(-20, 80);
                }
            }

            //NPC.ai[0]++;
            //if (NPC.ai[0] > 20 && BactusCount < MaxBacCount)
            //{
            //    NPC.ai[0] = 0;
            //    NPC Summon = NPC.NewNPCDirect(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2CircularEdge(1000, 1000), ModContent.NPCType<BactusMinion>(), 0, 0, 0, 0, 0, NPC.target);
            //    Summon.velocity = Summon.position.DirectionTo(Target.position) * 6;
            //    for (int i = 0; i < 20; i++)
            //    {
            //        Dust summonDust = Dust.NewDustPerfect(Summon.Center, DustID.CorruptGibs, Main.rand.NextVector2Circular(8, 8), 128, default, 1.5f);
            //        summonDust.noGravity = true;
            //    }
            //}
            //NPC.velocity += NPC.Center.DirectionTo(Target.Center) * 0.26f;
            if (NPC.ai[1] < 200)
            {
                if (Target.Center != NPC.Center)
                {
                    NPC.velocity = Vector2.Lerp(NPC.Center.DirectionTo(Target.Center) * speed, NPC.velocity, 0.96f);
                    NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -speed, speed);
                    NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -5, 5);
                }
            }
        }
        #endregion Phase 2

        if (Main.rand.NextBool(5))
        {
            int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-3, 2), 128, default, Main.rand.NextFloat(1.2f, 1.7f));
            Main.dust[d].noGravity = true;
        }



        //if(NPC.alpha > 255)
        //{
        //    NPC.active = false;
        //}
        float maxRotate = 0.4f;
        if (NPC.ai[3] is < 1 or > 59)
        NPC.rotation = MathHelper.Clamp(NPC.velocity.X * 0.03f, -maxRotate, maxRotate);
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
        Rectangle frame = NPC.frame;
        Vector2 drawPos = NPC.position + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition;
        float alphaThingHackyWow = 0;
        for (int i = 11; i > 0; i -= 2)
        {
            alphaThingHackyWow += 0.03f;
            Main.EntitySpriteDraw(texture, NPC.oldPos[i] + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition, frame, new Color(drawColor.R / 2 * alphaThingHackyWow * (NPC.ai[3] / 120), drawColor.G, 0, 128) * alphaThingHackyWow * (NPC.ai[3] / 60), NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, SpriteEffects.None, 0);
        }
        
        if (NPC.ai[0] < 0 && NPC.ai[3] == 0)
        {
            alphaThingHackyWow = 0;
            for (int i = 11; i > 0; i -= 2)
            {
                alphaThingHackyWow += 0.07f * (NPC.ai[0] / -60);
                Main.EntitySpriteDraw(texture, NPC.oldPos[i] + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition, frame, new Color(drawColor.R * alphaThingHackyWow, drawColor.G * alphaThingHackyWow, drawColor.B * alphaThingHackyWow, 128 * alphaThingHackyWow) * alphaThingHackyWow, NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, SpriteEffects.None, 0);
            }
        }
        //Main.EntitySpriteDraw(texture, drawPos, frame, drawColor, NPC.rotation, new Vector2(NPC.frame.Width / 2,NPC.frame.Height / 2), NPC.scale,SpriteEffects.None,0);
        return true;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        if (Main.expertMode)
            target.AddBuff(BuffID.Blackout, 3 * 60);

        target.AddBuff(BuffID.Darkness, 5 * 60);
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter += 1.0;
        if (NPC.frameCounter > 6.0)
        {
            NPC.frameCounter = 0.0;
            NPC.frame.Y = NPC.frame.Y + frameHeight;
        }

        if (NPC.ai[3] <= 59f)
        {
            if (NPC.frame.Y > frameHeight * 3)
            {
                NPC.frame.Y = 0;
            }
        }
        else
        {
            if (NPC.frame.Y < frameHeight * 4)
            {
                NPC.frame.Y = frameHeight * 4;
            }

            if (NPC.frame.Y > frameHeight * 7)
            {
                NPC.frame.Y = frameHeight * 4;
            }
        }
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("BacteriumPrime1").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("BacteriumPrime2").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("BacteriumPrime3").Type, NPC.scale);
            for (int i = 0; i < 30; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-4, 2), 50, default, 2);
                Main.dust[d].velocity += NPC.velocity * Main.rand.NextFloat(0.6f, 1f);
                Main.dust[d].noGravity = true;
                Main.dust[d].fadeIn = 1.2f;
            }
        }
        for (int i = 0; i < 15; i++)
        {
            int d2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 1), 50, default, Main.rand.NextFloat(1, 1.5f));
            Main.dust[d2].velocity += NPC.velocity * Main.rand.NextFloat(0.2f, 0.8f);
            Main.dust[d2].noGravity = true;
        }
        if (NPC.life <= 0)
        {
            for(int i = 0; i < 8; i++)
            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width / 2, NPC.height / 2), Main.rand.NextVector2Circular(3, 3), ModContent.ProjectileType<BacteriumGas>(), NPC.damage / 3, 0, -1,1);
        }
    }

    public override bool? CanFallThroughPlatforms()
    {
        return true;
    }
}
