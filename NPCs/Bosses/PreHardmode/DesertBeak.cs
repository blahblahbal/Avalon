using System;
using System.IO;
using System.Net.Http.Headers;
using Avalon.Common;
using Avalon.Items.BossBags;
using Avalon.Items.Material;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Magic.PreHardmode;
using Avalon.Projectiles.Hostile.DesertBeak;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode;

[AutoloadBossHead]
public class DesertBeak : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 8;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.TrailCacheLength[Type] = 16;
        NPCID.Sets.TrailingMode[Type] = 7;
    }
    public override void SetDefaults()
    {
        NPC.TargetClosest();
        NPC.damage = 65;
        NPC.boss = true;
        NPC.noTileCollide = true;
        NPC.lifeMax = 3650;
        NPC.defense = 30;
        NPC.noGravity = true;
        NPC.width = 64;
        NPC.aiStyle = -1;
        NPC.npcSlots = 100f;
        NPC.value = 50000f;
        NPC.timeLeft = 22500;
        NPC.height = 64;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit28;
        NPC.DeathSound = SoundID.NPCDeath31;
        Music = ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DesertBeak") : MusicID.Boss4;
        NPC.scale = 1f;
        phase = 0;
        FlapMultiplier = 1;
    }
    //public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    //{
    //    NPC.lifeMax = (int)(NPC.lifeMax * 0.66f * bossAdjustment);
    //    NPC.damage = (int)(NPC.damage * 0.58f);
    //}
    public override void OnKill()
    {
        Terraria.GameContent.Events.Sandstorm.StopSandstorm();
        if (!ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak)
        {
            NPC.SetEventFlagCleared(ref ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak, -1);
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.SandBlock, 1, 22, 55));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertBeakMask>(), 7));
        npcLoot.Add(
            ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertFeather>(), 1, 6, 10));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),
            AvalonWorld.RhodiumOre.GetRhodiumVariantItemOre(), 1, 15, 26));
        npcLoot.Add(
            ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TomeoftheDistantPast>(), 3));
        npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DesertBeakBossBag>()));
    }
    byte phase;
    byte phase1Feather = 0;
    byte phase1Egg = 1;
    byte phase2Transition = 2;
    byte phase2Circle = 3;
    byte phase2Dash = 4;
    byte phase2Grab = 5;
    byte phase2Egg = 6;
    float FlapMultiplier = 1;
    bool Storming;
    bool Pulsing;
    int afterImageTimer;
    float pulseTimer;
    float delay;
    public override void AI()
    {
        //Main.NewText("[" + $"{NPC.ai[0]}" + "]" + "[" + $"{NPC.ai[1]}" + "]" + "[" + $"{NPC.ai[2]}" + "]" + " phase: " + $"{phase}", Main.DiscoColor);

        if (pulseTimer < MathHelper.Pi && Pulsing)
        {
            pulseTimer += 0.08f;
        }
        else
        {
            pulseTimer = 0;
            Pulsing = false;
        }

        afterImageTimer--;
        DrawOffsetY = 50;

        NPC.spriteDirection = NPC.direction = NPC.velocity.X > 0 ? 1 : -1;
        NPC.rotation = NPC.velocity.X * 0.05f;

        if (Storming)
        {
            Terraria.GameContent.Events.Sandstorm.TimeLeft = 60;
        }

        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].InModBiome(ModContent.GetInstance<ContagionLenient>()))
        {
            NPC.TargetClosest(false);
        }
        Player Target = Main.player[NPC.target];
        if (Target.dead) 
        {
            phase = 255;
        }
        if(phase == 255)
        {
            NPC.timeLeft = 0;
            NPC.velocity.Y -= 0.2f;
            NPC.alpha += 2;
        }
        else if (NPC.life > (int)(NPC.lifeMax * 0.6f))
        {
            PhaseOne(Target);
        }
        else
        {
            PhaseTwo(Target);
        }
        //Main.NewText(NPC.ai[1], Main.DiscoColor);
    }
    public void PhaseOne(Player Target)
    {
        if (phase == phase1Feather)
        {
            NPC.ai[0]++;

            if (NPC.ai[0] < 150)
            {
                NPC.velocity += NPC.Center.DirectionTo(Target.Center + new Vector2(0,-50)) * 0.1f;
                NPC.velocity = NPC.velocity.LengthClamp(5);
            }
            else
            {
                if (NPC.ai[0] % 50 == 0)
                {
                    if (NPC.ai[0] > 50 * 3.1f + 150)
                    {
                        int Feathers = Main.rand.Next(10, 14);
                        for (int i = 0; i < Feathers; i++)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 8).RotatedBy((MathHelper.TwoPi / Feathers) * Main.rand.NextFloat(0.9f, 1.1f) * i) * Main.rand.NextFloat(0.8f, 1.1f), ModContent.ProjectileType<DesertBeakFeather>(), 15, 1, -1, 0, 0, Main.rand.Next(10));
                        }
                        NPC.ai[0] = -85;
                        NPC.ai[1]++;
                        if (NPC.ai[1] > 2)
                        {
                            NPC.ai[1] = 0;
                            phase = phase1Egg;
                            NPC.TargetClosest();
                            NPC.netUpdate = true;
                        }
                    }
                    afterImageTimer = 30;
                    NPC.velocity = NPC.Center.DirectionTo(Target.Center + Main.rand.NextVector2Circular(30, 30)) * 12 * (NPC.ai[0] * 0.001f + 1);
                }
                NPC.velocity *= 0.98f;
            }
        }
        else if(phase == phase1Egg)
        {
            NPC.ai[0]++;
            NPC.ai[1]++;
            NPC.velocity += NPC.Center.DirectionTo(Target.Center + new Vector2(0,-100) + new Vector2(0, 300 + (float)Math.Sin(NPC.ai[0] * 0.02f) * 100).RotatedBy(NPC.ai[0] * 0.1f) * 0.5f) * 0.2f;
            NPC.velocity = NPC.velocity.LengthClamp(8);

            if (NPC.ai[1] > 200)
            {
                NPC.velocity *= 0.9f;
            }
            if (NPC.ai[1] == 260)
            {
                NPC.ai[1] = Main.rand.Next(-60, 60);
                int dmg = 44;
                if (Main.masterMode) dmg = 38;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.NextFloat(-4,4), 3), ModContent.ProjectileType<VultureEgg>(), dmg, 1);
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] > 1000)
            {
                NPC.ai[1] = 0;
                NPC.ai[0] = 0;
                phase = phase1Feather;
                NPC.TargetClosest();
                NPC.netUpdate = true;
            }
        }
    }
    public void PhaseTwo(Player Target)
    {
        if (phase <= phase1Egg)
        {
            phase = phase2Transition;
            NPC.ai[0] = 0;
            NPC.ai[1] = 0;
            NPC.netUpdate = true;
        }
        else if (phase == phase2Transition)
        {
            NPC.ai[0]++;
            NPC.velocity *= 0.98f;

            if (NPC.ai[0] == 50)
            {
                Pulsing = true;
                SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.position);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Terraria.GameContent.Events.Sandstorm.StartSandstorm();
                    Storming = true;
                }
            }
            FlapMultiplier = 2;
            if (NPC.ai[0] > 100)
            {
                pulseTimer = 0;
                phase = phase2Circle;
                NPC.ai[0] = 0;
                NPC.ai[1] = -1;
                NPC.TargetClosest();
                FlapMultiplier = 1;
                NPC.defense = (int)(NPC.defense * 0.666f);
                NPC.netUpdate = true;
            }
        }
        else if (phase == phase2Circle)
        {
            NPC.ai[0]++;
            NPC.ai[2]++;
            float CircleDistance = (int)(Math.Floor(NPC.ai[0] * 0.001f)) % 2 == 0 ? 300 : 100;
            NPC.velocity += NPC.Center.DirectionTo(Target.Center + Vector2.One.RotatedBy(NPC.ai[0] * 0.02) * CircleDistance * NPC.ai[1]) * 0.3f;
            NPC.velocity = NPC.velocity.LengthClamp(10);

            if (CircleDistance > 100)
            {
                if (NPC.ai[2] == 100)
                {
                    Pulsing = true;
                }
                else if (NPC.ai[2] > 130)
                {
                    int Feathers = Main.rand.Next(7, 10);
                    for (int i = 0; i < Feathers; i++)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 8).RotatedBy((MathHelper.TwoPi / Feathers) * Main.rand.NextFloat(0.9f, 1.1f) * i) * Main.rand.NextFloat(0.8f, 1.1f), ModContent.ProjectileType<DesertBeakFeather>(), 15, 1, -1, 0, 0, Main.rand.Next(10));
                    }
                    NPC.ai[2] = Main.rand.Next(-100, 0);
                    NPC.netUpdate = true;
                    NPC.ai[1] *= Main.rand.NextBool() ? 1 : -1;
                }
            }
            else
            {
                NPC.ai[2] = Main.rand.Next(-100, 0);
                NPC.ai[3]++;
            }
            if (NPC.ai[3] >= 4)
            {
                phase = phase2Dash;
                NPC.ai[0] = 0;
                NPC.ai[3] = 0;
            }
        }
        else if (phase == phase2Dash)
        {
            NPC.ai[0]++;
            if (NPC.ai[0] % 90 == 0)
            {
                afterImageTimer = 30;
                NPC.velocity = NPC.Center.DirectionTo(Target.Center + Main.rand.NextVector2Circular(30, 30)) * 12 * (NPC.ai[0] * 0.001f + 1);
                NPC.velocity *= 0.98f;
                //bool withinRange = Vector2.Distance(new Vector2(NPC.Center.X, NPC.position.Y + NPC.height), new Vector2(Target.Center.X, Target.position.Y)) < 150;
                
                NPC.ai[0] = 0;
            }
            if (Target.getRect().Intersects(NPC.getRect()))
            {
                Target.velocity.X = NPC.velocity.X;
                phase = phase2Grab;
                NPC.ai[1] = 0;
                return;
            }

        }
        else if (phase == phase2Grab)
        {
            int grabOffset = (int)NPC.Center.Y + 50;
            NPC.velocity.X *= 0.97f;
            NPC.ai[1]++;
            delay++;
            if (NPC.ai[1] < 70)
            {
                Target.velocity.X = NPC.velocity.X;
                NPC.height *= (int)1.5;
                if (delay == 30)
                {
                    NPC.damage = 8;
                    delay = 0;
                }
                NPC.velocity.Y = -12;
                Target.Center = new Vector2(NPC.Center.X, grabOffset);

            }
            else if (NPC.ai[1] >= 70 && NPC.ai[1] < 105)
            {
                Target.velocity.Y += 0.1f;
                NPC.velocity.Y += 0.1f;
                delay = 0;
            }
            if (NPC.ai[1] >= 150)
            {
                
                
                if (NPC.ai[1] < 250)
                {
                    afterImageTimer = 30;
                    NPC.velocity = NPC.Center.DirectionTo(Target.Center + Main.rand.NextVector2Circular(30, 30)) * 12 * (NPC.ai[1] * 0.001f + 1);
                }
                else if (NPC.ai[1] == 250)
                {
                    NPC.ai[1] = -1;
                    phase = phase2Egg;
                    delay = 0;
                }
            }
        }

        else if (phase == phase2Egg)
        {
            NPC.ai[0]++;
            NPC.ai[1]++;
            NPC.velocity += NPC.Center.DirectionTo(Target.Center + new Vector2(0, -100) + new Vector2(0, 300 + (float)Math.Sin(NPC.ai[0] * 0.02f) * 100).RotatedBy(NPC.ai[0] * 0.1f) * 0.5f) * 0.2f;
            NPC.velocity = NPC.velocity.LengthClamp(8);

            if (NPC.ai[1] > 200)
            {
                NPC.velocity *= 0.9f;
            }
            if (NPC.ai[1] == 260)
            {
                NPC.ai[1] = Main.rand.Next(-60, 60);
                int dmg = 44;
                if (Main.masterMode) dmg = 38;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.NextFloat(-4, 4), 3), ModContent.ProjectileType<VultureEgg>(), dmg, 1);
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] > 500)
            {
                NPC.ai[1] = -1;
                NPC.ai[0] = 0;
                phase = phase2Circle;
                NPC.TargetClosest();
                NPC.netUpdate = true;
            }
        }
    }
    public override void FindFrame(int frameHeight)
    {
        if (afterImageTimer <= 0)
            NPC.frameCounter += 1.0 + MathHelper.Clamp(-NPC.velocity.Y * 0.3f + Math.Abs(NPC.velocity.X * 0.1f), -0.3f, 1.7f) * FlapMultiplier;
        else
            NPC.frameCounter += 1.0 + MathHelper.Lerp(MathHelper.Clamp(-NPC.velocity.Y * 0.3f + Math.Abs(NPC.velocity.X * 0.1f), -0.3f, 2), MathHelper.Clamp(NPC.velocity.Length() * 0.3f, 0, 2),MathHelper.Clamp(afterImageTimer * 0.1f,0,1)) * FlapMultiplier;

        if (NPC.frameCounter > 5.0)
        {
            NPC.frameCounter = 0.0;
            NPC.frame.Y = NPC.frame.Y + frameHeight;
        }
        if (NPC.frame.Y > frameHeight * 7)
        {
            NPC.frame.Y = 0;
        }
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(phase);
        writer.Write(afterImageTimer);
        writer.Write(Storming);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        phase = reader.ReadByte();
        afterImageTimer = reader.ReadInt16();
        Storming = reader.ReadBoolean();
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DesertBeakHead").Type,
                0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DesertBeakWing").Type,
                0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DesertBeakWing").Type,
                0.9f);
        }
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
        Rectangle frame = NPC.frame;
        Vector2 drawPos = NPC.position + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition;
        if (afterImageTimer > 0)
        {
            float alphaThingHackyWow = 0;
            for (int i = 8; i > 0; i--)
            {
                alphaThingHackyWow += 0.07f;
                Main.EntitySpriteDraw(texture, NPC.oldPos[i] + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition, frame, drawColor * alphaThingHackyWow * MathHelper.Clamp(afterImageTimer * 0.05f,0,1), NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, SpriteEffects.None, 0);
            }
        }
        if (phase > phase1Egg)
        {
            float alphaThingHackyWow = 0;
            for (int i = 8; i > 0; i-= 2)
            {
                alphaThingHackyWow += 0.07f;
                Main.EntitySpriteDraw(texture, NPC.oldPos[i] + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition, frame, drawColor * alphaThingHackyWow, NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, SpriteEffects.None, 0);
            }
        }
        if (pulseTimer is > 0 and < MathHelper.Pi)
        {
            for (int i = 4; i > 0; i--)
            {
                Main.EntitySpriteDraw(texture, drawPos + new Vector2(0, (float)Math.Sin(pulseTimer) * 8).RotatedBy(MathHelper.PiOver2 * i), frame, drawColor * (float)Math.Sin(pulseTimer) * 0.7f, NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, SpriteEffects.None, 0);
            }
        }
        return true;
    }
}
