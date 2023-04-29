using System;
using Avalon.Items.BossBags;
using Avalon.Items.Material.Ores;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Vanity;
using Avalon.NPCs.PreHardmode;
using Avalon.Projectiles.Hostile;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode;

[AutoloadBossHead]
public class BacteriumPrime : ModNPC
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Bacterium Prime");
        Main.npcFrameCount[NPC.type] = 8;
        NPCID.Sets.TrailCacheLength[NPC.type] = 12;
        NPCID.Sets.TrailingMode[NPC.type] = 7;
    }
    public override void SetDefaults()
    {
        NPC.damage = 31;
        NPC.boss = true;
        NPC.noTileCollide = true;
        NPC.lifeMax = 2700;
        NPC.defense = 16;
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
        //Music = Avalon.MusicMod != null ? MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/BacteriumPrime") : MusicID.Boss2;
        //bossBag = ModContent.ItemType<Items.BossBags.BacteriumPrimeBossBag>();
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacteriumPrimeTrophy>(), 10));
        //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacteriumPrimeMask>(), 10));
        //npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<BacteriumPrimeBossBag>()));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 1, 15, 41));
        if (!ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime)
        {
            ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime = true;
        }
    }
    Vector2 LastCollide;
    int BactusCount;
    int MaxBacCount = 8;
    public override void AI()
    {
        BactusCount = 0;
        for(int i = 0; i < Main.maxNPCs; i++)
        {
            if (Main.npc[i].type == ModContent.NPCType<BactusMinion>() && Main.npc[i].active)
            {
                BactusCount++;
            }
        }

        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest(true);
        }
        Player Target = Main.player[NPC.target];
        float speed = 5;
        #region Phase 1
        if (NPC.ai[3] == 0) {
            if (NPC.noTileCollide || NPC.Center.Distance(Target.Center) > 40 * 16)
                NPC.velocity += NPC.Center.DirectionTo(Target.Center + Main.rand.NextVector2CircularEdge(200, 200)) * 0.13f;
            else
            {
                NPC.velocity += NPC.Center.DirectionTo(Target.Center + Main.rand.NextVector2CircularEdge(200, 200)) * 0.023f;
            }

            NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -speed, speed);
            NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -speed, speed);

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
                LastCollide = NPC.position;
            }
            if (NPC.collideY)
            {
                LastCollide = NPC.position;
                NPC.velocity.Y = NPC.oldVelocity.Y * -1.2f;
            }
            if(NPC.collideX || NPC.collideY)
            {
                if (Main.rand.NextBool(6))
                    NPC.velocity = new Vector2(Main.rand.NextFloat(speed / 2, speed), 0).RotatedBy(NPC.Center.DirectionTo(Target.Center).ToRotation());
            }
            if (NPC.noTileCollide)
            {
                NPC.alpha = (int)(MathHelper.Lerp(128, NPC.alpha, 0.9f));
            }
            else
            {
                NPC.alpha = (int)(MathHelper.Lerp(0, NPC.alpha, 0.9f));
            }

            NPC.ai[1]++;
            if (NPC.ai[1] > 360 && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                //NPC.ai[1] = 0;
                NPC.noTileCollide = false;
            }
            if (NPC.ai[1] > 700 * 2 && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                NPC.ai[1] = -Main.rand.Next(-100, 0);
                NPC.noTileCollide = true;
            }

            if (Main.rand.NextBool(10))
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width / 2, NPC.height / 2), Main.rand.NextVector2Circular(1, 1), ModContent.ProjectileType<Cough>(), NPC.damage / 3, 0, -1,1);
            }

            NPC.ai[0]++;
            if (NPC.ai[0] > 120 && BactusCount < MaxBacCount)
            {
                NPC.ai[0] = 0;
                NPC Summon = NPC.NewNPCDirect(NPC.GetSource_FromThis(),NPC.Center + Main.rand.NextVector2CircularEdge(1000,1000),ModContent.NPCType<BactusMinion>(),0,0,0,0,0,NPC.target);
                Summon.velocity = Summon.position.DirectionTo(Target.position) * 6;
                for(int i = 0; i < 20; i++)
                {
                    Dust summonDust = Dust.NewDustPerfect(Summon.Center, DustID.CorruptGibs, Main.rand.NextVector2Circular(8, 8), 128, default, 1.5f);
                    summonDust.noGravity = true;
                }
            }
        }
        #endregion Phase 1

        if (NPC.life <= NPC.lifeMax * 0.55f && NPC.ai[3] < 60)
        {
            NPC.alpha = (int)(MathHelper.Lerp(64, NPC.alpha, 0.9f));
            NPC.ai[3]++;
            NPC.velocity *= 0.98f;

            float rotate = Main.rand.NextFloat(0, MathHelper.TwoPi);
            int d = Dust.NewDust(NPC.Center + new Vector2(0, 180).RotatedBy(rotate), 0, 0, DustID.CorruptGibs, 0, 0, 50, default, 2);
            Main.dust[d].velocity = new Vector2(0, -13).RotatedBy(rotate) + NPC.velocity;
            Main.dust[d].noGravity = true;
            if (Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width / 2, NPC.height / 2), Main.rand.NextVector2Circular(3, 3), ModContent.ProjectileType<Cough>(), NPC.damage / 3, 0, -1, 1);
            }
        }
        if (NPC.ai[3] == 59)
        {
            NPC.ai[0] = 0;
            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            //NPC.knockBackResist = 0f;
            NPC.defense -= 2;
            //speed += 1;
            NPC.noTileCollide = true;
            SoundEngine.PlaySound(SoundID.ForceRoar);
        }
        
        #region Phase 2
        if (NPC.ai[3] == 60)
        {
            if (!Collision.SolidCollision(NPC.position,NPC.width,NPC.height) || NPC.ai[1] > 199)
                NPC.ai[1]++;
            if (Main.rand.NextBool(30))
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width / 2, NPC.height / 2), Main.rand.NextVector2Circular(1, 1), ModContent.ProjectileType<Cough>(), NPC.damage / 3, 0, -1,1);
            }
            if (NPC.ai[1] > 200)
            {
                    float rotate = Main.rand.NextFloat(0, MathHelper.TwoPi);
                    int d = Dust.NewDust(NPC.Center + new Vector2(0,120).RotatedBy(rotate), 0,0, DustID.CorruptGibs,0,0, 50, default, 2);
                    Main.dust[d].velocity = new Vector2(0, -9).RotatedBy(rotate) + NPC.velocity;
                    Main.dust[d].noGravity = true;
                    //Main.dust[d].fadeIn = 1.2f;
                NPC.velocity *= 0.94f;
            }
            if (NPC.ai[1] == 240)
            {
                SoundEngine.PlaySound(SoundID.Item95, NPC.Center);
                for (int i = 0; i < Main.rand.Next(4, 7); i++)
                {
                    Vector2 ShootDirection = NPC.Center.DirectionTo(Target.Center).RotatedByRandom(0.3f) * Main.rand.NextFloat(6, 3) + new Vector2(0,Main.rand.NextFloat(-4,0));
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, ShootDirection, ModContent.ProjectileType<CorrosiveMucus>(), (int)(NPC.damage * 0.3f), 0, -1);
                }
                for (int i = 0; i < Main.rand.Next(2, 5); i++)
                {
                    Vector2 ShootDirection = NPC.Center.DirectionTo(Target.Center).RotatedByRandom(0.3f) * Main.rand.NextFloat(5, 2) + new Vector2(0, Main.rand.NextFloat(-4, 0));
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, ShootDirection, ModContent.ProjectileType<Cough>(), (int)(NPC.damage * 0.2f), 0, -1,1);
                }
                NPC.ai[1] = 0;
            }

            NPC.ai[0]++;
            if (NPC.ai[0] > 20 && BactusCount < MaxBacCount)
            {
                NPC.ai[0] = 0;
                NPC Summon = NPC.NewNPCDirect(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2CircularEdge(1000, 1000), ModContent.NPCType<BactusMinion>(), 0, 0, 0, 0, 0, NPC.target);
                Summon.velocity = Summon.position.DirectionTo(Target.position) * 6;
                for (int i = 0; i < 20; i++)
                {
                    Dust summonDust = Dust.NewDustPerfect(Summon.Center, DustID.CorruptGibs, Main.rand.NextVector2Circular(8, 8), 128, default, 1.5f);
                    summonDust.noGravity = true;
                }
            }
            //NPC.velocity += NPC.Center.DirectionTo(Target.Center) * 0.26f;
            if (NPC.ai[1] < 200)
            {
                NPC.velocity = Vector2.Lerp(NPC.Center.DirectionTo(Target.Center) * speed, NPC.velocity, 0.96f);
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -speed, speed);
                NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -5, 5);
            }
        }
        #endregion
        bool PlayerAlive = true;
        for(int i = 0; i < Main.maxPlayers; i++)
        {
            if (!Main.player[i].dead && Main.player[i].position.Distance(NPC.position) < 16000)
            {
                break;
                PlayerAlive = true;
            }
            else
            {
                PlayerAlive = false;
            }
        }
        if (!PlayerAlive)
            NPC.ai[3] = 61;
        if (NPC.ai[3] == 61)
        {
            NPC.velocity.Y -= 0.1f;
            NPC.velocity.X *= 0.99f;
            NPC.alpha++;
        }

        if (Main.rand.NextBool(5))
        {
            int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-3, 2), 128, default, Main.rand.NextFloat(1.2f, 1.7f));
            Main.dust[d].noGravity = true;
        }

       

        if(NPC.alpha > 255)
        {
            NPC.active = false;
        }
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
        for (int i = 11; i > 0; i-= 2)
        {
            alphaThingHackyWow += 0.05f;
            Main.EntitySpriteDraw(texture, NPC.oldPos[i] + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition, frame, new Color(drawColor.R / 2 * alphaThingHackyWow * (NPC.ai[3] / 120), drawColor.G, 0, 128) * alphaThingHackyWow * (NPC.ai[3] / 60), NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, SpriteEffects.None, 0);
        }
        //Main.EntitySpriteDraw(texture, drawPos, frame, drawColor, NPC.rotation, new Vector2(NPC.frame.Width / 2,NPC.frame.Height / 2), NPC.scale,SpriteEffects.None,0);
        return true;
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
            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width / 2, NPC.height / 2), Main.rand.NextVector2Circular(3, 3), ModContent.ProjectileType<Cough>(), NPC.damage / 3, 0, -1,1);
        }
    }

    public override bool? CanFallThroughPlatforms()
    {
        return true;
    }
}
