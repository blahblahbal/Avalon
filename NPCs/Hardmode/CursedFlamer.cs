using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Avalon.Items.Accessories.Hardmode;

namespace Avalon.NPCs.Hardmode;

public class CursedFlamer : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 3;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.CursedInferno] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        Data.Sets.NPC.Wicked[NPC.type] = true;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.CursedFlamer"))
        });
    }
    public override void SetDefaults()
    {
        NPC.npcSlots = 1;
        NPC.width = 40;
        NPC.height = 40;
        NPC.aiStyle = -1;
        NPC.timeLeft = 1750;
        AnimationType = NPCID.Corruptor;
        NPC.damage = 50;
        NPC.defense = 35;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.lifeMax = 210;
        NPC.scale = 1f;
        NPC.knockBackResist = 0.35f;
        NPC.noGravity = true;
        NPC.noTileCollide = false;
        NPC.value = 500;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.CursedFlamerBanner>();
        DrawOffsetY = (int)((82 / 2) - (NPC.height / 2));
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return Main.hardMode && spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.InPillarZone() && spawnInfo.SpawnTileY < (Main.maxTilesY - 200) ? 0.3f : 0f;
    }
    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.5f);
    }
    //public override void FindFrame(int frameHeight)
    //{
        //NPC.frameCounter++;
        //if (NPC.frameCounter < 6)
        //{
        //    NPC.frame.Y = 0;
        //}
        //if (NPC.frameCounter < 12)
        //{
        //    NPC.frame.Y = frameHeight;
        //}
        //if (NPC.frameCounter < 18)
        //{
        //    NPC.frame.Y = frameHeight * 2;
        //}
        //if (NPC.frameCounter < 24)
        //{
        //    NPC.frame.Y = frameHeight;
        //}
        

        //NPC.frameCounter++;
        //if (NPC.frameCounter >= 8.0)
        //{
        //    NPC.frame.Y = NPC.frame.Y + frameHeight;
        //    NPC.frameCounter = 0.0;
        //}
        //if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        //{
        //    NPC.frame.Y = 0;
        //}
    //}
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.CursedFlame, 3));
        //npcLoot.Add(ItemDropRule.StatusImmunityItem(ModContent.ItemType<GreekExtinguisher>(), 40));
    }
    public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
    {
        return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
    }
    public override void AI()
    {
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest(true);
        }
        var num1169 = 4.2f;
        var num1170 = 0.022f;
        var vector156 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
        var num1171 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
        var num1172 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;
        num1171 = (int)(num1171 / 8f) * 8;
        num1172 = (int)(num1172 / 8f) * 8;
        vector156.X = (int)(vector156.X / 8f) * 8;
        vector156.Y = (int)(vector156.Y / 8f) * 8;
        num1171 -= vector156.X;
        num1172 -= vector156.Y;
        var num1173 = (float)Math.Sqrt(num1171 * num1171 + num1172 * num1172);
        var num1174 = num1173;
        if (num1173 == 0f)
        {
            num1171 = NPC.velocity.X;
            num1172 = NPC.velocity.Y;
        }
        else
        {
            num1173 = num1169 / num1173;
            num1171 *= num1173;
            num1172 *= num1173;
        }
        NPC.ai[0] += 1f;
        if (NPC.ai[0] > 0f)
        {
            NPC.velocity.Y = NPC.velocity.Y + 0.023f;
        }
        else
        {
            NPC.velocity.Y = NPC.velocity.Y - 0.023f;
        }
        if (NPC.ai[0] < -100f || NPC.ai[0] > 100f)
        {
            NPC.velocity.X = NPC.velocity.X + 0.023f;
        }
        else
        {
            NPC.velocity.X = NPC.velocity.X - 0.023f;
        }
        if (NPC.ai[0] > 200f)
        {
            NPC.ai[0] = -200f;
        }
        if (num1174 < 150f)
        {
            NPC.velocity.X = NPC.velocity.X + num1171 * 0.007f;
            NPC.velocity.Y = NPC.velocity.Y + num1172 * 0.007f;
        }
        if (Main.player[NPC.target].dead)
        {
            num1171 = NPC.direction * num1169 / 2f;
            num1172 = -num1169 / 2f;
        }
        if (NPC.velocity.X < num1171)
        {
            NPC.velocity.X = NPC.velocity.X + num1170;
            if (NPC.velocity.X < 0f && num1171 > 0f)
            {
                NPC.velocity.X = NPC.velocity.X + num1170;
            }
        }
        else if (NPC.velocity.X > num1171)
        {
            NPC.velocity.X = NPC.velocity.X - num1170;
            if (NPC.velocity.X > 0f && num1171 < 0f)
            {
                NPC.velocity.X = NPC.velocity.X - num1170;
            }
        }
        if (NPC.velocity.Y < num1172)
        {
            NPC.velocity.Y = NPC.velocity.Y + num1170;
            if (NPC.velocity.Y < 0f && num1172 > 0f)
            {
                NPC.velocity.Y = NPC.velocity.Y + num1170;
            }
        }
        else if (NPC.velocity.Y > num1172)
        {
            NPC.velocity.Y = NPC.velocity.Y - num1170;
            if (NPC.velocity.Y > 0f && num1172 < 0f)
            {
                NPC.velocity.Y = NPC.velocity.Y - num1170;
            }
        }
        NPC.rotation = (float)Math.Atan2(num1172, num1171) - 1.57f;
        Vector2 correctedCenter = new Vector2(NPC.position.X + NPC.width / 2 - 8, NPC.position.Y + NPC.height / 2 - 8);
        Vector2 asdf = new Vector2(correctedCenter.X, NPC.position.Y + NPC.height * 1.26f - 6);
        Vector2 asdf2 = RotateAboutOrigin(asdf, correctedCenter, NPC.rotation);
        if (!Main.rand.NextBool(4))
        {
            int dusty = Dust.NewDust(asdf2 + Main.rand.NextVector2Circular(10f, 10f), 14, 12, DustID.CursedTorch, default, default, 100, new Color(), 2);
            Main.dust[dusty].noGravity = true;
            Main.dust[dusty].velocity.X += correctedCenter.DirectionTo(asdf2).X * Main.rand.NextFloat(2f);
            Main.dust[dusty].velocity.Y += correctedCenter.DirectionTo(asdf2).Y * Main.rand.NextFloat(2f);
        }
        if (Main.rand.NextBool(4))
        {
            int dusty2 = Dust.NewDust(asdf2 + Main.rand.NextVector2Circular(10f, 10f), 14, 12, DustID.CursedTorch, default, default, 100, new Color(), 1f);
            Main.dust[dusty2].velocity.X += correctedCenter.DirectionTo(asdf2).X * Main.rand.NextFloat(2f);
            Main.dust[dusty2].velocity.Y += correctedCenter.DirectionTo(asdf2).Y * Main.rand.NextFloat(2f);
        }
        var num1179 = 0.7f;
        if (NPC.collideX)
        {
            NPC.netUpdate = true;
            NPC.velocity.X = NPC.oldVelocity.X * -num1179;
            if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
            {
                NPC.velocity.X = 2f;
            }
            if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
            {
                NPC.velocity.X = -2f;
            }
        }
        if (NPC.collideY)
        {
            NPC.netUpdate = true;
            NPC.velocity.Y = NPC.oldVelocity.Y * -num1179;
            if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1.5)
            {
                NPC.velocity.Y = 2f;
            }
            if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1.5)
            {
                NPC.velocity.Y = -2f;
            }
        }
        else if (Main.rand.NextBool(20))
        {
            var num1182 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + NPC.height * 0.25f), NPC.width, (int)(NPC.height * 0.5f), DustID.CorruptGibs, NPC.velocity.X, 2f, 75, NPC.color, NPC.scale);
            var dust52 = Main.dust[num1182];
            dust52.velocity.X = dust52.velocity.X * 0.5f;
            var dust53 = Main.dust[num1182];
            dust53.velocity.Y = dust53.velocity.Y * 0.1f;
        }
        if (NPC.wet)
        {
            if (NPC.velocity.Y > 0f)
            {
                NPC.velocity.Y = NPC.velocity.Y * 0.95f;
            }
            NPC.velocity.Y = NPC.velocity.Y - 0.3f;
            if (NPC.velocity.Y < -2f)
            {
                NPC.velocity.Y = -2f;
            }
        }
        if (Main.netMode != NetmodeID.MultiplayerClient && !Main.player[NPC.target].dead)
        {
            if (NPC.justHit)
            {
                NPC.localAI[0] = 0f;
            }
            NPC.localAI[0] += 1f;
            if (NPC.localAI[0] == 180f)
            {
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    var mainproj = (float)Math.Atan2(NPC.Center.Y - (Main.player[NPC.target].Center.Y), NPC.Center.X - (Main.player[NPC.target].Center.X));
                    int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, -(float)Math.Cos(mainproj), -(float)Math.Sin(mainproj), ProjectileID.CursedFlameHostile, 50, 1f, NPC.target, 0f, 0f);
                    Main.projectile[p].velocity *= 7f;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, p, 0f, 0f, 0f, 0);
                    }
                }
                NPC.localAI[0] = 0f;
            }
        }
        if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
        {
            NPC.netUpdate = true;
            return;
        }
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0)
        {
            Rectangle R = new Rectangle((int)NPC.position.X, (int)(NPC.position.Y + ((NPC.height - NPC.width) / 2)), NPC.width, NPC.width);
            int C = 50;
            float vR = .4f;
            for (int i = 1; i <= C; i++)
            {
                int D = Dust.NewDust(NPC.position, R.Width, R.Height, DustID.CursedTorch, 0, 0, 100, new Color(), 2f);
                Main.dust[D].noGravity = true;
                Main.dust[D].velocity.X = vR * (Main.dust[D].position.X - (NPC.position.X + (NPC.width / 2)));
                Main.dust[D].velocity.Y = vR * (Main.dust[D].position.Y - (NPC.position.Y + (NPC.height / 2)));
            }
            for (int i2 = 1; i2 <= C; i2++)
            {
                int D2 = Dust.NewDust(NPC.position, R.Width, R.Height, DustID.CursedTorch, 0, 0, 100, new Color(), 2f);
                Main.dust[D2].noGravity = true;
                Main.dust[D2].velocity.X = vR * (Main.dust[D2].position.X - (NPC.position.X + (NPC.width / 2)));
                Main.dust[D2].velocity.Y = vR * (Main.dust[D2].position.Y - (NPC.position.Y + (NPC.height / 2)));
            }
            for (int i2 = 1; i2 <= C; i2++)
            {
                int D2 = Dust.NewDust(NPC.position, R.Width, R.Height, DustID.CorruptGibs, 0, 0, 100, new Color(), 2f);
                Main.dust[D2].noGravity = true;
                Main.dust[D2].velocity.X = vR * (Main.dust[D2].position.X - (NPC.position.X + (NPC.width / 2)));
                Main.dust[D2].velocity.Y = vR * (Main.dust[D2].position.Y - (NPC.position.Y + (NPC.height / 2)));
            }
            if (Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CursedFlamer1").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CursedFlamer1").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CursedFlamer2").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CursedFlamer3").Type);
            }
        }
    }
}
