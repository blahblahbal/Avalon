using System;
using Avalon.Items.Material;
using Avalon.Items.Potions;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Avalon.Items.Vanity;
using System.IO;
using Terraria.DataStructures;

namespace Avalon.NPCs.Bosses.Hardmode;

public class Mechasting : ModNPC
{
    public bool SecondPhase;

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 8;
    }

    public override void SetDefaults()
    {
        NPC.aiStyle = -1;
        NPC.npcSlots = 175;
        NPC.height = 174;
        NPC.width = 88;
        NPC.knockBackResist = 0f;
        NPC.netAlways = true;
        NPC.noTileCollide = true;
        NPC.value = 50000;
        NPC.timeLeft = 22500;
        NPC.damage = 52;
        NPC.defense = 15;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        NPC.boss = true;
        NPC.lifeMax = 31000;
        NPC.scale = 1.2f;
        //NPC.BossBar = ModContent.GetInstance<BossBars.MechastingBossBar>();
        Music = ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Mechasting") : MusicID.Boss5;
        //bossBag = ModContent.ItemType<Items.BossBags.MechastingBossBag>();

    }
    public override void BossLoot(ref string name, ref int potionType)
    {
        potionType = ItemID.GreaterHealingPotion;
    }
    //public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    //{
    //    NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
    //    NPC.damage = (int)(NPC.damage * 0.65f);
    //}
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        SecondPhase = reader.ReadBoolean();
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(SecondPhase);
    }
	public override void AI()
    {
		//if (NPC.ai[3] == 0)
		//{
		//	for (int i = 0; i < 10; i++)
		//	{
		//		NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<StingerProbe>(), NPC.target, NPC.whoAmI);
		//	}
		//}
		NPC.TargetClosest(true);
        if (NPC.PlayerTarget().dead || Main.dayTime)
        {
            NPC.velocity.Y = NPC.velocity.Y - 0.04f;
            if (NPC.timeLeft > 10)
            {
                NPC.timeLeft = 10;
                return;
            }
        }
        if (NPC.life < NPC.lifeMax * 0.3f)
        {
            SecondPhase = true;
        }
        if (!SecondPhase)
        {
			//NPC.ai[3] = 2;
            // ai phase 1
            if (NPC.ai[3] == 0)
            {
                NPC.ai[0]++; // ai phase counter
                if (NPC.PlayerTarget().position.X < NPC.position.X)
                {
                    if (NPC.velocity.X > -8) NPC.velocity.X -= 0.22f;
                }
                if (NPC.PlayerTarget().position.X > NPC.position.X)
                {
                    if (NPC.velocity.X < 8) NPC.velocity.X += 0.22f;
                }
                if (NPC.PlayerTarget().position.Y < NPC.position.Y + 300)
                {
                    if (NPC.velocity.Y < 0)
                    {
                        if (NPC.velocity.Y > -4) NPC.velocity.Y -= 0.8f;
                    }
                    else NPC.velocity.Y -= 0.6f;
                    if (NPC.velocity.Y < -4) NPC.velocity.Y = -4;
                }
                if (NPC.PlayerTarget().position.Y > NPC.position.Y + 300)
                {
                    if (NPC.velocity.Y > 0)
                    {
                        if (NPC.velocity.Y < 4) NPC.velocity.Y += 0.8f;
                    }
                    else NPC.velocity.Y += 0.6f;
                    if (NPC.velocity.Y > 4) NPC.velocity.Y = 4;
                }
                NPC.ai[2]++;
                // fire laser
                if (NPC.ai[2] > 15)
                {
                    float Speed = 9f;
                    Vector2 vector8 = new Vector2(NPC.Center.X, NPC.position.Y + NPC.height - 10);
                    int damage = 35;
                    SoundEngine.PlaySound(SoundID.Item33, NPC.position);
                    Vector2 offset = new Vector2(NPC.Center.X + Main.rand.Next(2) * NPC.direction, NPC.Center.Y + Main.rand.Next(2, 5));
                    float rotation = (float)Math.Atan2(NPC.Center.Y - offset.Y, NPC.Center.X - offset.X);
                    int num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)(Math.Sin(rotation) * Speed * -1), ProjectileID.DeathLaser, damage, 0f, 0);
                    //Main.projectile[num54].notReflect = true;
                    NPC.ai[2] = 0;
                }
                if (NPC.ai[0] > 540)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[3] = 1;
                    NPC.ai[2] = 0;
                }
            }
            // ai phase 2
            if (NPC.ai[3] == 1)
            {
                NPC.ai[2]++; // ai phase counter
                NPC.ai[1]++; // movement and stinger probe counter
                             // normal movement
                if (NPC.ai[1] < 300)
                {
                    if (NPC.PlayerTarget().position.X < NPC.position.X)
                    {
                        if (NPC.velocity.X > -8) NPC.velocity.X -= 0.22f;
                    }
                    if (NPC.PlayerTarget().position.X > NPC.position.X)
                    {
                        if (NPC.velocity.X < 8) NPC.velocity.X += 0.22f;
                    }
                    if (NPC.PlayerTarget().position.Y < NPC.position.Y + 300)
                    {
                        if (NPC.velocity.Y < 0)
                        {
                            if (NPC.velocity.Y > -4) NPC.velocity.Y -= 0.8f;
                        }
                        else NPC.velocity.Y -= 0.6f;
                        if (NPC.velocity.Y < -4) NPC.velocity.Y = -4;
                    }
                    if (NPC.PlayerTarget().position.Y > NPC.position.Y + 300)
                    {
                        if (NPC.velocity.Y > 0)
                        {
                            if (NPC.velocity.Y < 4) NPC.velocity.Y += 0.8f;
                        }
                        else NPC.velocity.Y += 0.6f;
                        if (NPC.velocity.Y > 4) NPC.velocity.Y = 4;
                    }
                }
                NPC.ai[0]++; // stinger counter
                             // fire a spread of stingers at the closest player
                if (NPC.ai[0] >= 90)
                {
                    float speed = 12f;
                    Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height / 2));
                    int damage = 30;
                    int type = ModContent.ProjectileType<Projectiles.Hostile.Mechasting.Mechastinger>();
                    float rotation = (float)Math.Atan2(NPC.Center.Y - NPC.PlayerTarget().Center.Y, NPC.Center.X - NPC.PlayerTarget().Center.X);
                    int num54;
                    float f = 0f;
                    if (NPC.ai[0] >= 150)
                    {
                        while (f <= .2f)
                        {
                            num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)(Math.Cos(rotation + f) * speed * -1), (float)(Math.Sin(rotation + f) * speed * -1), type, damage, 0f, NPC.target);
                            Main.projectile[num54].timeLeft = 600;
                            Main.projectile[num54].tileCollide = false;
                            //Main.projectile[num54].notReflect = true;
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, num54);
                            }
                            num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)(Math.Cos(rotation - f) * speed * -1), (float)(Math.Sin(rotation - f) * speed * -1), type, damage, 0f, NPC.target);
                            Main.projectile[num54].timeLeft = 600;
                            Main.projectile[num54].tileCollide = false;
                            //Main.projectile[num54].notReflect = true;
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, num54);
                            }
                            f += .04f;
                        }
                        NPC.ai[0] = 0;
                    }
                }
                // dash at the player
                if (NPC.ai[1] >= 300)
                {
                    NPC.velocity.X *= 0.98f;
                    NPC.velocity.Y *= 0.98f;
                    Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height / 2));
                    if ((NPC.velocity.X < 2f) && (NPC.velocity.X > -2f) && (NPC.velocity.Y < 2f) && (NPC.velocity.Y > -2f))
                    {
                        float rotation = (float)Math.Atan2(vector8.Y - (NPC.PlayerTarget().position.Y + (NPC.PlayerTarget().height * 0.5f)), vector8.X - (NPC.PlayerTarget().position.X + (NPC.PlayerTarget().width * 0.5f)));
                        NPC.velocity.X = (float)(Math.Cos(rotation) * 25) * -1;
                        NPC.velocity.Y = (float)(Math.Sin(rotation) * 25) * -1;
                    }
                    NPC.ai[1] = 0;
                }
                // spawn stinger probes
                if (NPC.ai[1] % 70 == 0)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<StingerProbe>(), NPC.target);
                }
                if (NPC.ai[2] > 700)
                {
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 2;
                }
            }
            // ai phase 3
            if (NPC.ai[3] == 2)
            {
                // fire rockets
                NPC.velocity *= 0f;
                NPC.noGravity = true;
                NPC.ai[1]++; // rocket counter
                if (NPC.ai[1] > 90 && NPC.ai[1] < 360 && NPC.ai[1] % 25 == 0)
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - NPC.PlayerTarget().Center.Y, NPC.Center.X - NPC.PlayerTarget().Center.X);
                    float f = 0f; // degrees; 3.6f is a full 360 degrees
                    float speed = 3.5f; // velocity of the projectile to be fired
                    int p;
                    while (f < 0.2f) // less than 20 degrees
                    {
                        int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HomingRocket>());
                        Main.npc[n].velocity = new((float)(Math.Cos(rotation + f) * speed * -1), (float)(Math.Sin(rotation + f) * speed * -1));
                        //Main.projectile[p].notReflect = true;
                        //Main.projectile[p].bombPlayer = true;
                        //if (Main.netMode != NetmodeID.SinglePlayer)
                        //{
                        //    NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, n);
                        //}

                        //n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HomingRocket>());
                        //Main.npc[n].velocity = new((float)(Math.Cos(rotation - f) * speed * -1), (float)(Math.Sin(rotation - f) * speed * -1));
                        ////Main.projectile[p].notReflect = true;
                        ////Main.projectile[p].bombPlayer = true;
                        //if (Main.netMode != NetmodeID.SinglePlayer)
                        //{
                        //    NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, n);
                        //}
                        f += .2f;
                    }
                }
                if (NPC.ai[1] == 360)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[1] = 0;
                    NPC.ai[3] = 3;
                }
            }
            // ai phase 4
            if (NPC.ai[3] == 3)
            {
                NPC.ai[2]++; // ai phase counter
                if (NPC.PlayerTarget().position.X < NPC.position.X)
                {
                    if (NPC.velocity.X > -8) NPC.velocity.X -= 0.22f;
                }
                if (NPC.PlayerTarget().position.X > NPC.position.X)
                {
                    if (NPC.velocity.X < 8) NPC.velocity.X += 0.22f;
                }
                if (NPC.PlayerTarget().position.Y < NPC.position.Y + 300)
                {
                    if (NPC.velocity.Y < 0)
                    {
                        if (NPC.velocity.Y > -4) NPC.velocity.Y -= 0.8f;
                    }
                    else NPC.velocity.Y -= 0.6f;
                    if (NPC.velocity.Y < -4) NPC.velocity.Y = -4;
                }
                if (NPC.PlayerTarget().position.Y > NPC.position.Y + 300)
                {
                    if (NPC.velocity.Y > 0)
                    {
                        if (NPC.velocity.Y < 4) NPC.velocity.Y += 0.8f;
                    }
                    else NPC.velocity.Y += 0.6f;
                    if (NPC.velocity.Y > 4) NPC.velocity.Y = 4;
                }
                NPC.ai[1]++; // electric bolt counter
                if (NPC.ai[1] >= 240 && NPC.ai[1] <= 300)
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - NPC.PlayerTarget().Center.Y, NPC.Center.X - NPC.PlayerTarget().Center.X);
                    float f = 0f; // degrees; 3.6f is a full 360 degrees
                    float speed = 9f; // the velocity of the projectile to be shot
                    if (Main.expertMode)
                    {
                        speed = 15f;
                    }
                    int p;
                    #region electric bolt attack
                    if (NPC.ai[1] % 60 == 0)
                    {
                        float increment = Main.expertMode ? 0.225f : 0.45f;
                        while (f <= 3.6f)
                        {
                            // above the boss
                            p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(rotation + f) * speed * -1), (float)(Math.Sin(rotation + f) * speed * -1), ModContent.ProjectileType<Projectiles.Hostile.Mechasting.ElectricBolt>(), 25, 0f, NPC.target);
                            Main.projectile[p].timeLeft = 600;
                            Main.projectile[p].friendly = false;
                            //Main.projectile[p].notReflect = true;
                            Main.projectile[p].hostile = true;
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, p);
                            }
                            // below the boss
                            p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(rotation - f) * speed * -1), (float)(Math.Sin(rotation - f) * speed * -1), ModContent.ProjectileType<Projectiles.Hostile.Mechasting.ElectricBolt>(), 25, 0f, NPC.target);
                            Main.projectile[p].timeLeft = 600;
                            Main.projectile[p].friendly = false;
                            //Main.projectile[p].notReflect = true;
                            Main.projectile[p].hostile = true;
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, p);
                            }
                            f += increment;
                        }
                    }
                    #endregion
                    if (NPC.ai[1] == 300)
                    {
                        NPC.ai[1] = 0;
                    }
                }
                if (NPC.ai[2] > 900)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                }
            }
        }
        else
        {
            // phase swap at low hp
            if (NPC.ai[3] < 5)
            {
                NPC.ai[0] = 0;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 5;
                return;
            }

            // ai phase 5, low hp
            if (NPC.ai[3] == 5)
            {
                NPC.ai[1]++; // movement counter
                NPC.ai[0]++; // phase change counter
                // normal movement
                if (NPC.ai[1] < 120)
                {
                    if (NPC.PlayerTarget().position.X < NPC.position.X)
                    {
                        if (NPC.velocity.X > -8) NPC.velocity.X -= 0.4f;
                    }
                    if (NPC.PlayerTarget().position.X > NPC.position.X)
                    {
                        if (NPC.velocity.X < 8) NPC.velocity.X += 0.4f;
                    }
                    if (NPC.PlayerTarget().position.Y < NPC.position.Y + 300)
                    {
                        if (NPC.velocity.Y < 0)
                        {
                            if (NPC.velocity.Y > -4) NPC.velocity.Y -= 0.8f;
                        }
                        else NPC.velocity.Y -= 0.6f;
                        if (NPC.velocity.Y < -4) NPC.velocity.Y = -4;
                    }
                    if (NPC.PlayerTarget().position.Y > NPC.position.Y + 300)
                    {
                        if (NPC.velocity.Y > 0)
                        {
                            if (NPC.velocity.Y < 4) NPC.velocity.Y += 0.8f;
                        }
                        else NPC.velocity.Y += 0.6f;
                        if (NPC.velocity.Y > 4) NPC.velocity.Y = 4;
                    }
                }
                if (NPC.ai[1] >= 120)
                {
                    Vector2 directionToPlayer = NPC.PlayerTarget().Center - NPC.Center;
                    directionToPlayer.Normalize();
                    Vector2 targetPosition = NPC.PlayerTarget().Center + directionToPlayer * 200;
                    Vector2 dashDirection = targetPosition - NPC.Center;
                    dashDirection.Normalize();
                    NPC.velocity = dashDirection * 40f;
                    if (NPC.ai[1] == 130) NPC.ai[1] = 0;
                }
                if (NPC.ai[0] == 130 * 4)
                {
                    NPC.ai[3] = 6;
                    NPC.ai[0] = 0;
                }
            }

            // ai phase 6, low hp phase 2
            if (NPC.ai[3] == 6)
            {
                // movement
                if (NPC.PlayerTarget().position.X < NPC.position.X)
                {
                    if (NPC.velocity.X > -8) NPC.velocity.X -= 0.6f;
                }
                if (NPC.PlayerTarget().position.X > NPC.position.X)
                {
                    if (NPC.velocity.X < 8) NPC.velocity.X += 0.6f;
                }
                if (NPC.PlayerTarget().position.Y < NPC.position.Y + 300)
                {
                    if (NPC.velocity.Y < 0)
                    {
                        if (NPC.velocity.Y > -4) NPC.velocity.Y -= 0.8f;
                    }
                    else NPC.velocity.Y -= 0.6f;
                    if (NPC.velocity.Y < -4) NPC.velocity.Y = -4;
                }
                if (NPC.PlayerTarget().position.Y > NPC.position.Y + 300)
                {
                    if (NPC.velocity.Y > 0)
                    {
                        if (NPC.velocity.Y < 4) NPC.velocity.Y += 0.8f;
                    }
                    else NPC.velocity.Y += 0.6f;
                    if (NPC.velocity.Y > 4) NPC.velocity.Y = 4;
                }

                NPC.ai[0]++; // phase counter
                NPC.ai[2]++;
                // fire homing rockets
                if (NPC.ai[2] > 45)
                {
                    float Speed = 3.5f;
                    Vector2 vector8 = new Vector2(NPC.Center.X, NPC.position.Y + NPC.height - 10);
                    int damage = 35;
                    SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                    Vector2 offset = new Vector2(NPC.Center.X + Main.rand.Next(2) * NPC.direction, NPC.Center.Y + Main.rand.Next(2, 5));
                    float rotation = (float)Math.Atan2(NPC.Center.Y - offset.Y, NPC.Center.X - offset.X);
                    float speed = 9f; // velocity of the projectile to be fired

                    int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.position.Y + NPC.height - 10, ModContent.NPCType<HomingRocket>());
                    Main.npc[n].velocity = new((float)(Math.Cos(rotation) * speed * -1), (float)(Math.Sin(rotation) * speed * -1));
                    NPC.ai[2] = 0;
                }

                if (NPC.ai[0] >= 45 * 8)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[3] = 5;
                }
            }
        }
    }
    public override void OnKill()
    {
        if (!ModContent.GetInstance<DownedBossSystem>().DownedMechasting)
        {
            NPC.SetEventFlagCleared(ref ModContent.GetInstance<DownedBossSystem>().DownedMechasting, -1);
            //ModContent.GetInstance<AvalonWorld>().GenerateCrystalMines(); // CHANGE LATER TO OBLIVION
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
        notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SoulofDelight>(), 1, 20, 40));
        notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MechastingMask>(), 7));
        //notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<Items.Accessories.StingerPack>(), ModContent.ItemType<Items.Weapons.Magic.Mechazapinator>(), ModContent.ItemType<Items.Weapons.Ranged.HeatSeeker>() }));
        //npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossBags.MechastingBossBag>()));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.Trophy.MechastingTrophy>(), 10));
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.spriteDirection = NPC.direction;
        NPC.rotation = NPC.velocity.X * 0.03f;
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
