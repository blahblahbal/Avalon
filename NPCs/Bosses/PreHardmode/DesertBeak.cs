using System;
using System.IO;
using Avalon.Common;
using Avalon.Items.BossBags;
using Avalon.Items.Material;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Magic.PreHardmode;
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
        Main.npcFrameCount[NPC.type] = 3;
        NPCID.Sets.DebuffImmunitySets[Type] = new NPCDebuffImmunityData { SpecificallyImmuneTo = new[] { /*ModContent.BuffType<Frozen>(),*/ BuffID.Confused } };
    }
    private enum PhaseOne
    {
        Spawn,
        Grab,
        Dive,
        Minion_and_Projectile
    }
    private enum PhaseTwo
    {
        ProjectileCircle,
        SandStormProjectile,
        TripleShotProjectileTwo
    }
    PhaseOne modePartOne = PhaseOne.Spawn;
    PhaseTwo modeSwitchTwo;
    public float ModeTimer
    {
        get => NPC.ai[0];
        set => NPC.ai[0] = value;
    }
    public float DiveTimer
    {
        get => NPC.ai[1];
        set => NPC.ai[1] = value;
    }
    public override void SetDefaults()
    {
        NPC.TargetClosest();
        Player player = Main.player[NPC.target];

        NPC.damage = 65;
        NPC.boss = true;
        NPC.noTileCollide = true;
        NPC.lifeMax = 3650;
        NPC.defense = 30;
        NPC.noGravity = true;
        NPC.width = 130;
        NPC.aiStyle = -1;
        NPC.npcSlots = 100f;
        NPC.value = 50000f;
        NPC.timeLeft = 22500;
        NPC.height = 78;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit28;
        NPC.DeathSound = SoundID.NPCDeath31;
        Music = ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DesertBeak") : MusicID.Boss4;
        NPC.Center = player.Center + new Vector2(300, -600);
        NPC.scale = 1.3f;
        DiveTimer = 0;

    }

    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.66f * bossAdjustment);
        NPC.damage = (int)(NPC.damage * 0.58f);
    }

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
    int winddir, grabOffset, divecount, divemax, eggcount, eggtotal;
    float posoffset, heightoffset, dashVelocity, delay, maxTime, diveDelay, acceleration, playerMovementCounter, oldPlayerPos;
    bool istotheRight, isLastDive, eggDrop, afterImage, verticalDive;
    bool genDiveMax = true;
    Vector2 direction;
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(heightoffset);
        writer.Write(posoffset);
        writer.Write(dashVelocity);
        writer.Write(divemax);
        writer.Write(isLastDive);
        writer.Write(eggcount);
        writer.Write(eggtotal);
        writer.Write(eggDrop);
        writer.Write(afterImage);

    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        heightoffset = reader.ReadInt32();
        posoffset = reader.ReadInt32();
        dashVelocity = reader.ReadInt32();
        divemax = reader.ReadInt32();
        isLastDive = reader.ReadBoolean();
        eggcount = reader.ReadInt32();
        eggtotal = reader.ReadInt32();
        eggDrop = reader.ReadBoolean();
        afterImage = reader.ReadBoolean();
    }

    public override void AI()
    {

        afterImage = true;

        Player player = Main.player[NPC.target];
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
        {
            NPC.TargetClosest();
        }
        if (Main.player[NPC.target].dead)
        {
            Terraria.GameContent.Events.Sandstorm.StopSandstorm();
            NPC.velocity.Y -= 0.04f;
            if (NPC.timeLeft > 10)
            {
                NPC.timeLeft = 10;
                return;
            }
        }
        istotheRight = NPC.Center.X > player.Center.X;
        NPC.dontTakeDamage = NPC.alpha > 200;
        if (NPC.life >= (int)NPC.lifeMax * 0.45f && !Main.player[NPC.target].dead)
        {

            if (modePartOne == PhaseOne.Spawn)
            {
                NPC.TargetClosest();
                NPC.spriteDirection = NPC.direction;
                //NPC.rotation = NPC.velocity.X * 0.1f;
                NPC.velocity = NPC.DirectionTo(player.Center + new Vector2(0, -300));
                ModeTimer++;
                if (ModeTimer > 100)
                {
                    NPC.alpha += 20;
                }
                if (ModeTimer >= 150)
                {
                    ModeTimer = 0;
                    DiveTimer = 0;
                    eggcount = 1;
                    modePartOne = PhaseOne.Dive;
                    NPC.netUpdate = true;
                    SoundEngine.PlaySound(SoundID.NPCHit28, NPC.position);
                    NPC.velocity = new Vector2(0, 0);
                    NPC.Center = player.Center + new Vector2(-500, -500);
                    //dive = Main.rand.Next(3);

                }
            }
            if (modePartOne == PhaseOne.Dive)
            {

                NPC.TargetClosest(false);
                NPC.damage = 0;
                NPC.alpha = 0;
                if (genDiveMax) divemax = Main.rand.Next(4, 7);//Boolean so it only gens a divemax once per Dive;
                                                               //Checks if the amount of dives has reached the randomly generated maximum amount, isLastDive is used throughout the funtion to determine if egg projectiles will be spawned
                                                               //Splits the time variable into equal parts depending on the amount of projectiles and returns true at equidistant points;
                if (DiveTimer == 0)//This is where all of the pre-dive calculations are set
                {
                    eggtotal = Main.rand.Next(3, 6);
                    posoffset = -1500 + Main.rand.Next(-100, 100);
                    dashVelocity = 10;//Base velocity
                    if (istotheRight == true)
                    {
                        posoffset *= -1;//Checks if the NPC is to the right of the player and changes the Velocity and Horizontal offset accordingly.
                        dashVelocity *= -1;
                    }
                    heightoffset = player.Center.Y - (600 - Main.rand.NextFloat(-20, 40));//Randomly generates heights for the swoops, changing the values to be more in the postive range inscreases the general depth.
                    isLastDive = divecount == divemax;
                    if (isLastDive)
                    {
                        heightoffset += 100;//Ofsetting because the egg-dropping dive is more linear than the normal one
                        posoffset = posoffset * 0.5f;//NPC doesnt show up on screen if posoffset isnt halfed during the egg dropping swoop 
                        dashVelocity += 5;
                        maxTime = 270;
                        diveDelay = 0f;
                        eggcount = 1;
                    }
                    else
                    {
                        diveDelay = (float)Main.rand.Next(0, 100);
                        maxTime = 150 + diveDelay;
                        acceleration = Main.rand.NextFloat(0.15f, 0.19f);
                        oldPlayerPos = player.Center.X;
                    }
                    if (playerMovementCounter >= 480)
                    {
                        verticalDive = true;
                        oldPlayerPos = 0f;
                        playerMovementCounter = 0;
                    }
                    Vector2 neededstartpos = new Vector2(player.Center.X + posoffset, heightoffset);//Uses the offsets to generate a needed position so that horizontal position of minimum == player horizontal position
                    if (!verticalDive) NPC.Center = neededstartpos;//Need to somehow implement a velocity so the NPC smoothly travels to the point instead of teleporting
                    else
                    {
                        Main.NewText("Desert Beak easily spots " + player.name + " due to them not moving!");
                        float dist = (float)Math.Tan(MathHelper.ToRadians(45)) * 500;
                        NPC.Center = player.Center + new Vector2(istotheRight ? dist : -dist, -500);
                        direction = NPC.DirectionTo(player.Center);
                    }
                    NPC.netUpdate = true;

                }
                eggDrop = isLastDive && DiveTimer == (Math.Floor(((double)(maxTime * 0.5f) / eggtotal) * eggcount));

                if (DiveTimer >= 0 && DiveTimer <= maxTime)
                {
                    NPC.spriteDirection = NPC.velocity.X < 0 ? -1 : 1;//Manually setting the Sprite direction using the horizontal velocity instead of the direction due to the wonkiness of the shape
                    if (!isLastDive && !verticalDive)
                    {
                        if (DiveTimer < maxTime / 2)
                        {
                            dashVelocity += NPC.velocity.X <= 0 ? acceleration * -1 : acceleration; //Acelleration for first half of the arc, switches depending on direction;
                        }
                        else if (DiveTimer >= maxTime / 2)
                        {
                            dashVelocity += NPC.velocity.X <= 0 ? acceleration : acceleration * -1;//Same as that^ except its switched around;
                        }
                    }
                    if (verticalDive)
                    {
                        NPC.damage = 75;
                        NPC.velocity = direction * 20;
                    }
                    if (eggDrop == true)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 5).RotatedByRandom(5), ModContent.ProjectileType<Projectiles.Hostile.VultureEgg>(), 30, 0, player.whoAmI);//Generate projectile
                        eggcount++;
                    }
                    if (oldPlayerPos == player.Center.X)
                    {
                        playerMovementCounter++;
                    }
                    else
                    {
                        playerMovementCounter = 0;
                    }
                    oldPlayerPos = player.Center.X;
                    int heightmodifier = isLastDive ? 2 : 10;//Both heightmodifier and periodmodifier are modifiers for the lower cosine function depending on which type of swoop is done;The higher the height modifer the taller the waves
                    double periodmodifier = isLastDive ? MathHelper.PiOver2 * 3 : 1;//The Higher the periodmodifier value the more narrower the wavelengths
                    if (!verticalDive) NPC.velocity = new Vector2(dashVelocity, (float)Math.Cos(MathHelper.ToRadians(DiveTimer * (float)periodmodifier)) * heightmodifier);// Time is used as the X value for the cosine function as it simulates degrees.

                    if (NPC.velocity.X > 0) NPC.rotation = NPC.velocity.ToRotation();
                    else NPC.rotation = NPC.velocity.ToRotation() + (float)Math.PI;

                    if (Vector2.Distance(NPC.Center, player.Center) < 50 && !verticalDive)
                    {//Homemade hill-billy collision function used to detect if the two npcs touch each other.
                        player.velocity.X = NPC.velocity.X;
                        modePartOne = PhaseOne.Grab;
                        verticalDive = false;
                    }
                    DiveTimer++;
                    NPC.netUpdate = true;
                }
                else if (DiveTimer >= maxTime)
                {
                    verticalDive = false;
                    DiveTimer = 0;
                    genDiveMax = false;
                    divecount++;
                    if (isLastDive == true)
                    {
                        genDiveMax = true;
                        divecount = 0;
                        modePartOne = PhaseOne.Minion_and_Projectile;
                    }

                }
            }
            if (modePartOne == PhaseOne.Grab)
            {
                grabOffset = (int)NPC.Center.Y + 50;
                NPC.velocity.X *= 0.97f;//increasing the value increases the time taken for the npc to slow down, 0.97 seems to be the sweet spot
                ModeTimer++;
                delay++;
                if (ModeTimer < 100)
                {
                    player.velocity.X = NPC.velocity.X;
                    NPC.height *= (int)1.5;
                    if (delay == 30)
                    {
                        NPC.damage = 8;
                        delay = 0;
                    }
                    NPC.velocity.Y = -10;
                    player.Center = new Vector2(NPC.Center.X, grabOffset);

                }
                else if (ModeTimer >= 100 && ModeTimer < 125)
                {
                    player.velocity.Y += 0.1f;
                    NPC.velocity.Y += 0.1f;
                    delay = 0;
                }
                if (ModeTimer >= 225)
                {
                    ModeTimer = 0;
                    istotheRight = true;
                    DiveTimer = 0;
                    modePartOne = PhaseOne.Dive;
                    isLastDive = false;
                    delay = 0;

                }
                NPC.netUpdate = true;
            }

            if (modePartOne == PhaseOne.Minion_and_Projectile)
            {//UNFINISHED
                NPC.TargetClosest();
                NPC.spriteDirection = NPC.direction;
                if (NPC.velocity.X < 0) NPC.rotation = NPC.velocity.ToRotation() + (float)Math.PI;
                else NPC.rotation = NPC.velocity.ToRotation();
                Vector2 perturbed = Vector2.Distance(NPC.Center, player.Center) >= 150 ? Vector2.One * 6 : Vector2.One;
                direction = NPC.DirectionTo(player.Center);
                NPC.velocity = Vector2.Distance(NPC.Center, player.Center) >= 300 ? direction * 12 : perturbed * direction;

                /*ModeTimer +=2;
                if(ModeTimer < 200)
                {
                NPC.velocity = NPC.DirectionTo(player.Center + new Vector2(300, -300))*10;
                }*/




            }

        }
        else //if (NPC.life < (int)NPC.lifeMax * 0.45f && !Main.player[NPC.target].dead) (placeholder)
        {

            NPC.alpha = 250;
            modePartOne = PhaseOne.Dive;//placeholder so boss doesnt despawn
            /*NPC.damage = 0;
            NPC.TargetClosest();
            //When at this stage the boss whips up strong winds pushing the player in whatever direction the wind blows
            Terraria.GameContent.Events.Sandstorm.StartSandstorm();
            if (!transformed)
            {
                Main.NewText("The wind is growing stronger and is kicking up a lot of sand.", Color.SandyBrown);
                transformed = true;
                mode = 0;
                NPC.velocity = new Vector2(0, 0);
                divetimer = 0;
                modetimer = 0;
                
            }
            //Main.NewText(mode);
            switch (mode)
            {
                case 0:

                    target = player.Center + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-250, -150));
                    posStored = target;
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = NPC.velocity.X * 0.1f;
                    NPC.velocity *= 0f;
                    no_teleport = 1;
                    mode = 3;
                    //Main.NewText(no_teleport);
                    //if (no_teleport >= 57)
                    //{
                    //    mode = 1;
                    //}
                    return;
                case 3:
                    no_teleport++;
                    NPC.velocity *= 0f;
                    NPC.velocity = NPC.DirectionTo(target) * 8;
                    if (no_teleport >= 48)
                    {
                        mode = 1;
                    }
                    return;
                case 4:


                    NPC.velocity *= 0.95f;
                    break;
                case 1:
                    // Quickly dashes to a random location above the player and fires a spread of 3 feathers
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = 0;
                    //NPC.velocity *= 0f;
                    if (no_teleport <= 12)
                    {
                        NPC.velocity = Vector2.Zero;
                        //NPC.velocity *= 0f;
                        no_teleport = 0;
                        //no_teleport++;
                        if (teleports == ((Main.expertMode || Main.masterMode) ? 4 : 9))
                        {
                            SoundEngine.PlaySound(SoundID.NPCHit28, NPC.position);

                            Vector2 targetPosition = Main.player[NPC.target].position;
                            Vector2 position = NPC.Center;
                            Vector2 target = Main.player[NPC.target].Center;
                            Vector2 direction = targetPosition - position;
                            position += Vector2.Normalize(direction) * 2f;
                            Vector2 perturbedSpeed = direction * 0.2f;

                            int NumProjectiles = (Main.expertMode || Main.masterMode) ? 30 : 20;
                            float rotation = MathHelper.ToRadians(180);

                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                Vector2 newVelocity = perturbedSpeed.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (NumProjectiles - 1f)));

                                int dmg = 30;
                                if (Main.expertMode) dmg = 22;
                                if (Main.masterMode) dmg = 16;
                                Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position, newVelocity * 0.1f, ModContent.ProjectileType<Projectiles.Hostile.DesertBeakFeather>(), dmg, 0, player.whoAmI);
                            }
                        }
                        else
                        {
                            SoundEngine.PlaySound(SoundID.Item64, NPC.position);

                            Vector2 targetPosition = Main.player[NPC.target].position;
                            Vector2 position = NPC.Center;
                            Vector2 target = Main.player[NPC.target].Center;
                            Vector2 direction = targetPosition - position;
                            position += Vector2.Normalize(direction) * 2f;
                            Vector2 perturbedSpeed = direction * 0.27f;

                            const int NumProjectiles = 3;
                            float rotation = MathHelper.ToRadians(Main.rand.Next(15, 35));

                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                Vector2 newVelocity = perturbedSpeed.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (NumProjectiles - 1f)));

                                int dmg = 30;
                                if (Main.expertMode) dmg = 22;
                                if (Main.masterMode) dmg = 16;
                                Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position, newVelocity * 0.1f, ModContent.ProjectileType<Projectiles.Hostile.DesertBeakFeather>(), dmg, 0, player.whoAmI);
                            }
                        }
                        teleports++;

                        if(teleports == ((Main.expertMode || Main.masterMode) ? 5 : 10))
                        {
                            mode = 2;
                            no_teleport = 0;
                            //return;
                        }
                        else
                        {
                            mode = 0;
                            //return;
                        }
                    }
                    else
                    {
                        if (Vector2.Distance(target, NPC.position) > 0)
                        {
                            //NPC.velocity = NPC.DirectionTo(target) * 25;
                            afterImage = true;
                        }
                        //NPC.velocity *= 0f;
                        no_teleport--;
                    }
                    break;
                case 2:
                    NPC.spriteDirection = NPC.direction;
                    if (NPC.Center.X > player.Center.X)
                    {
                        NPC.velocity = NPC.DirectionTo(player.Center + new Vector2(250, 0)) * 3;
                    }
                    else
                    {
                        NPC.velocity = NPC.DirectionTo(player.Center - new Vector2(250, 0)) * 3;
                    }

                    NPC.alpha = 0;
                    modetimer++;
                    divetimer++;

                    if (divetimer >= (Main.expertMode || Main.masterMode ? 110 : 150))
                    {
                        float speed = (Main.expertMode || Main.masterMode ? 14f : 11f);
                        if (NPC.Center.X > player.Center.X)
                        {
                            Vector2 position = NPC.Center;
                            const int NumProjectiles = 1;

                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                Projectile p = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position, new Vector2(-speed, 0), ModContent.ProjectileType<Projectiles.Hostile.DesertBeakSandstorm>(), 30, 10, player.whoAmI);
                            }
                        }
                        else
                        {
                            Vector2 position = NPC.Center;
                            const int NumProjectiles = 1;

                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                Projectile p = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position, new Vector2(speed, 0), ModContent.ProjectileType<Projectiles.Hostile.DesertBeakSandstorm>(), 30, 10, player.whoAmI);
                            }
                        }

                        SoundEngine.PlaySound(SoundID.Item82, NPC.position);

                        divetimer = 0;
                    }

                    if (modetimer >= (Main.expertMode || Main.masterMode ? 450 : 600))
                    {
                        modetimer = 0;
                        mode = 0;
                        divetimer = 0;
                        teleports = 0;
                        no_teleport = 0;
                    }
                    break;
            }*/
        }
    }

    public override void FindFrame(int frameHeight)
    {
        if (NPC.velocity == Vector2.Zero || modePartOne == PhaseOne.Dive)
        {
            NPC.frame.Y = 0;
            NPC.frameCounter = 0.0;
        }
        else
        {
            NPC.frameCounter++;
            if (NPC.frameCounter < 10.0)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.frameCounter >= 10 && NPC.frameCounter < 12)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else
            {
                if (NPC.frameCounter >= 20.0)
                {
                    NPC.frameCounter = 0.0;
                }
            }

        }
    }

    public override void HitEffect(int hitDirection, double damage)
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


}
