using System;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent;
using Avalon.Items.Material;

namespace Avalon.NPCs.Bosses.PreHardmode;

public class KingSting : ModNPC
{
    // Misc vars
    int phase;
    int masterTimer;

    // Movement vars
    bool dontContinue;
    bool dontFlip;
    float acceleration;
    float maxVel;
    int facingType;
    Vector2 trueDestination;
    Vector2 destination;

    // Attack vars
    bool pickInit;
    int attackType;
    int lastAttack;
    int attackList;
    int attackTimer;
    int attackCounter;
    int goSide;

    // VFX
    bool afterImage;
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 8;

        NPCID.Sets.TrailCacheLength[NPC.type] = 4;
        NPCID.Sets.TrailingMode[NPC.type] = 3;
    }

    public override void SetDefaults()
    {
        NPC.npcSlots = 150f;
        NPC.height = 140;
        NPC.width = 88;
        NPC.knockBackResist = 0f;
        NPC.netAlways = true;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.timeLeft = 1000;
        NPC.value = 50000;
        NPC.aiStyle = -1;
        NPC.damage = 40;
        NPC.defense = 15;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.timeLeft = 1000;
        NPC.boss = true;
        NPC.lifeMax = 3400;
        NPC.scale = 1;
        Music = ExxoAvalonOrigins.MusicMod == null ? MusicID.Boss2 : MusicID.Boss4; // MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/KingSting");
        //NPC.BossBar = ModContent.GetInstance<BossBars.KingStingBossBar>();
        // Misc vars
        phase = 0;
        masterTimer = 0;

        // Movement vars
        dontContinue = false;
        dontFlip = false;
        facingType = 0;
        trueDestination = Vector2.Zero;
        destination = Vector2.Zero;

        // Projectile vars
        pickInit = false;
        acceleration = 0f;
        maxVel = 0f;
        attackType = 0;
        lastAttack = 0;
        attackList = 0;
        attackTimer = 0;
        attackCounter = 0;
        goSide = 0;

        //VFX
        afterImage = false;
    }
    //public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    //{
    //    NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * bossLifeScale);
    //    NPC.damage = (int)(NPC.damage * 0.65f);
    //}
    public override void AI()
    {
        if (phase == 0)
        {
            NPC.TargetClosest();
            phase = 1;
        }

        Player player = Main.player[NPC.target];
        if (player.dead || !player.active)
        {
            NPC.TargetClosest();
            if (player.dead || !player.active)
            {
                NPC.velocity.Y = NPC.velocity.Y - 0.04f;
                if (NPC.timeLeft > 10)
                {
                    NPC.timeLeft = 10;
                    return;
                }
            }
        }

        if (NPC.life <= NPC.lifeMax / 2)
            phase = 2;

        if (Main.expertMode)
        {
            if (phase == 2)
            {
                acceleration = 1.5f;
                maxVel = 8f;
            }
            else
            {
                acceleration = 0.9f;
                maxVel = 6f;
            }
        }
        else
        {
            if (phase == 2)
            {
                acceleration = 0.9f;
                maxVel = 6f;
            }
            else
            {
                acceleration = 0.6f;
                maxVel = 5f;
            }
        }

        float lOrR = (NPC.Center.X <= player.Center.X) ? 1 : (-1); // Based on pos
        float lOrR2 = (NPC.velocity.X <= 0f) ? 1 : (-1); // Based on vel
        // 1 is left -1 is right

        if (!dontFlip)
        {
            if (facingType != 3)
                NPC.direction = NPC.spriteDirection = (int)lOrR;
            else
                NPC.direction = NPC.spriteDirection = (int)-lOrR2;
        }

        switch (facingType)
        {
            case 1: // Face towards the player (capped angle)
                NPC.rotation = NPC.rotation.AngleLerp((player.Center - NPC.Center).ToRotation(), 0.1f) * -0.08f;
                break;
            case 2: // Face towards the player (uncapped)
                NPC.rotation = NPC.rotation.AngleLerp((player.Center - NPC.Center).ToRotation(), 0.1f);
                break;
            case 3: // Face towards velocity (capped angle)
                NPC.rotation = NPC.velocity.X * 0.02f;
                break;
        }

        if (!dontContinue && attackType != 0)
            masterTimer++;

        if (attackType == 0)
        {
            SelectAttack(player);
            NPC.velocity *= 0.98f;
            StandStillIfSlow(0.1f);
        }
        else if (attackType == 1)
        {
            attackTimer++;

            trueDestination = player.Center;
            if (NPC.velocity == Vector2.Zero)
                facingType = 1;
            else
                facingType = 3;

            if (Math.Abs(NPC.velocity.X) >= 6f || Math.Abs(NPC.velocity.Y) >= 6f)
                afterImage = true;
            else
                afterImage = false;

            if (attackTimer >= 90)
            {
                float rotation = (float)Math.Atan2(NPC.Center.Y - trueDestination.Y, NPC.Center.X - trueDestination.X);
                float speed = 15f + maxVel;
                NPC.velocity = new Vector2((float)Math.Cos(rotation) * speed, (float)Math.Sin(rotation) * speed) * -1f;
                attackCounter += 1;
                attackTimer = 0;
            }
            else
            {
                NPC.velocity *= 0.98f;
                StandStillIfSlow(0.1f);
            }

            if (attackCounter >= 3 && attackTimer >= 45)
            {
                pickInit = false;
                afterImage = false;
                attackType = 0;
            }
        }
        else if (attackType == 2)
        {
            attackTimer++;

            facingType = 1;
            trueDestination = new Vector2(player.Center.X, player.Center.Y - 300f);
            Vector2[] points =
            {
                new Vector2(trueDestination.X - 200, trueDestination.Y),
                new Vector2(trueDestination.X + 200, trueDestination.Y)
            };

            if (goSide == 0)
            {
                destination = points[0];

                if (Vector2.Distance(destination, NPC.Center) < 30)
                    goSide = 1;
            }
            else
            {
                destination = points[1];

                if (Vector2.Distance(destination, NPC.Center) < 30)
                    goSide = 0;
            }

            Vector2 desiredVelocity = NPC.DirectionTo(destination) * maxVel;
            FlyTo(desiredVelocity, acceleration);

            if (attackTimer >= 90)
            {
                SoundEngine.PlaySound(SoundID.Item17, NPC.position);
                Vector2 rotation = (player.Center - NPC.Center).SafeNormalize(-Vector2.UnitY);
                float speed = 8f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, rotation * speed, ProjectileID.Stinger, 20, 1.5f, Main.myPlayer);
                attackTimer = 0;
            }

            if (masterTimer >= 360)
            {
                pickInit = false;
                attackType = 0;
            }
        }
        else if (attackType == 3 || attackType == 4)
        {
            attackTimer++;

            trueDestination = new Vector2(player.Center.X, player.Center.Y - 400f);
            destination = trueDestination;
            Vector2 desiredVelocity = NPC.DirectionTo(destination) * maxVel;

            if (Vector2.Distance(destination, NPC.Center) < 100)
            {
                dontFlip = true;
                NPC.velocity *= 0.98f;
                StandStillIfSlow(0.2f);
            }
            else
            {
                FlyTo(desiredVelocity, acceleration);
                dontFlip = false;
            }

            if (attackType == 3)
            {
                if (attackTimer >= 60)
                {
                    Vector2 velocity = new Vector2(Main.rand.Next(-100, 101) * 0.03f, Main.rand.Next(-100, 101) * 0.03f);
                    int larvae = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Larvae>(), default, default, default, default, default, NPC.target);
                    Main.npc[larvae].velocity = velocity;
                    attackTimer = 0;
                }
            }
            else if (attackType == 4)
            {
                if (attackTimer >= 120)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 velocity = new Vector2(0f, Main.rand.NextFloat(-3f, -5f)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-35, 36)));
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Projectiles.Hostile.KingSting.ToxinBall>(), 25, 0.5f, NPC.target);
                    }
                    attackTimer = 0;
                }
            }

            if (masterTimer >= 360)
            {
                dontFlip = false;
                pickInit = false;
                attackType = 0;
            }
        }
    }
    public void FlyTo(Vector2 endpoint, float accel)
    {
        if (NPC.velocity.X < endpoint.X)
        {
            NPC.velocity.X += accel;
            if (NPC.velocity.X < 0f && endpoint.X > 0f)
            {
                NPC.velocity.X += accel;
            }
        }
        else if (NPC.velocity.X > endpoint.X)
        {
            NPC.velocity.X -= accel;
            if (NPC.velocity.X > 0f && endpoint.X < 0f)
            {
                NPC.velocity.X -= accel;
            }
        }
        if (NPC.velocity.Y < endpoint.Y)
        {
            NPC.velocity.Y += accel;
            if (NPC.velocity.Y < 0f && endpoint.Y > 0f)
            {
                NPC.velocity.Y += accel;
            }
        }
        else if (NPC.velocity.Y > endpoint.Y)
        {
            NPC.velocity.Y -= accel;
            if (NPC.velocity.Y > 0f && endpoint.Y < 0f)
            {
                NPC.velocity.Y -= accel;
            }
        }
    }
    public void StandStillIfSlow(float threshold)
    {
        if (Math.Abs(NPC.velocity.X) <= threshold)
            NPC.velocity.X = 0f;
        if (Math.Abs(NPC.velocity.Y) <= threshold)
            NPC.velocity.Y = 0f;
    }
    public void SelectAttack(Player player)
    {
        if (!pickInit)
        {
            int extraAttacks;
            if (Main.expertMode && phase == 2)
                extraAttacks = 2;
            else if (phase == 2)
                extraAttacks = 1;
            else
                extraAttacks = 0;

            attackList = Main.rand.Next(1, 3 + extraAttacks); // 1-2 in phase 1, 1-3 in phase 2 (1-4 in expert)

            pickInit = true;
        }

        if (attackList != lastAttack)
        {
            switch (attackList)
            {
                case 1: // Dashes at player
                    trueDestination = player.Center;
                    break;
                case 2: // Fire stingers
                    goSide = 0;
                    trueDestination = new Vector2(player.Center.X, player.Center.Y - 300f);
                    break;
                case 3: // Spawn Larvae
                    trueDestination = new Vector2(player.Center.X, player.Center.Y - 400f);
                    break;
                case 4: // Splash Venom
                    trueDestination = new Vector2(player.Center.X, player.Center.Y - 400f);
                    break;
            }

            //npc.TargetClosest();
            attackCounter = 0;
            masterTimer = 0;
            attackTimer = 0;
            lastAttack = attackList;
            attackType = attackList;
        }
        else
        {
            pickInit = false;
        }
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("KingStingHead").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("KingStingWing").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("KingStingWing").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("KingStingWing").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("KingStingWing").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("KingStingBody").Type, 1f);
        }
    }
    public override void OnKill()
    {
        ModContent.GetInstance<DownedBossSystem>().DownedKingSting = true;
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<WaspFiber>(), 1, 16, 27)); // wasp fiber
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Vanity.KingStingMask>(), 7));
        //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<KingStingTrophy>(), 10));
        npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossBags.KingStingBossBag>()));
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 v, Color lightColor) // Not flipping? Not sure why it's not
    {
        if (afterImage)
        {
            Vector2 drawOrigin = TextureAssets.Npc[NPC.type].Size() / new Vector2(2, (Main.npcFrameCount[NPC.type] * 2));

            SpriteEffects spriteEffect;
            if (NPC.spriteDirection == 1)
                spriteEffect = SpriteEffects.FlipHorizontally;
            else
                spriteEffect = SpriteEffects.None;

            for (int i = 0; i < NPC.oldPos.Length; i++)
            {
                Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(lightColor) * ((float)(NPC.oldPos.Length - i) / NPC.oldPos.Length);
                spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, NPC.frame, color, NPC.rotation, drawOrigin, NPC.scale, spriteEffect, 0f);
            }
        }
        return true;
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter++;
        if (NPC.frameCounter < 3.0)
        {
            NPC.frame.Y = 0;
        }
        else if (NPC.frameCounter < 6.0)
        {
            NPC.frame.Y = frameHeight;
        }
        else if (NPC.frameCounter < 9.0)
        {
            NPC.frame.Y = frameHeight * 2;
        }
        else if (NPC.frameCounter < 12.0)
        {
            NPC.frame.Y = frameHeight * 3;
        }
        else if (NPC.frameCounter < 15.0)
        {
            NPC.frame.Y = frameHeight * 4;
        }
        else if (NPC.frameCounter < 18.0)
        {
            NPC.frame.Y = frameHeight * 5;
        }
        else if (NPC.frameCounter < 21.0)
        {
            NPC.frame.Y = frameHeight * 6;
        }
        else if (NPC.frameCounter < 24.0)
        {
            NPC.frame.Y = frameHeight * 7;
        }
        else
        {
            NPC.frameCounter = 0.0;
        }
    }
}
