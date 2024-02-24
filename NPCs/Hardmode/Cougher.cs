using Terraria.GameContent.Bestiary;
using System;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Projectiles.Hostile;
using Terraria.Audio;
using Terraria.Localization;
using Avalon.Items.Armor.Hardmode;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Avalon.NPCs.Hardmode;

public class Cougher : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 3;
        Data.Sets.NPC.Wicked[NPC.type] = true;
    }
    static SoundStyle Cough = new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/CougherCough")
    {
        Volume = 0.8f,
        Pitch = 0f,
        PitchVariance = 0.1f,
        MaxInstances = 10,
    };
    static SoundStyle CoughSpecial = new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/CougherCoughSpecial")
    {
        Volume = 1f,
        Pitch = 0f,
        PitchVariance = 0f,
        MaxInstances = 10,
    };
    public override void OnSpawn(IEntitySource source)
    {
        NPC.ai[3] = Main.rand.Next(5);
    }
    public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
    {
        return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
    }
    public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (DateTime.Now.Month == 2 && DateTime.Now.Day == 14)
        {
            Rectangle frame = NPC.frame;
            Vector2 drawPos = NPC.position + new Vector2(18, 0);
            Vector2 drawPos2 = RotateAboutOrigin(drawPos, NPC.Center, NPC.rotation) - Main.screenPosition;
            var texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/Costumes/WhitePartyHat").Value;
            switch ((int)NPC.ai[3])
            {
                case 0:
                    texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/Costumes/BluePartyHat").Value;
                    break;
                case 1:
                    texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/Costumes/CyanPartyHat").Value;
                    break;
                case 2:
                    texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/Costumes/PinkPartyHat").Value;
                    break;
                case 3:
                    texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/Costumes/PurplePartyHat").Value;
                    break;
                case 4:
                    texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/Costumes/WhitePartyHat").Value;
                    break;
            }
            Main.spriteBatch.Draw(texture, drawPos2, frame, Color.White, NPC.rotation, new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, SpriteEffects.None, 0);
        }
    }
    public override void SetDefaults()
    {
        NPC.damage = 35;
        NPC.lifeMax = 230;
        NPC.defense = 12;
        NPC.noGravity = true;
        NPC.width = 28;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.value = 510f;
        NPC.height = 38;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/CougherDeath") { Pitch = 0.3f, Volume = 0.9f, PitchVariance = 0.5f, MaxInstances = 5};
        NPC.knockBackResist = 0.3f;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.CougherBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new ModBiomeBestiaryInfoElement(Mod, "Contagion", "Assets/Bestiary/ContagionIcon", "Assets/Bestiary/ContagionBG", null),
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Cougher"))
        });
    }
    //public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    //{
    //    NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
    //    NPC.damage = (int)(NPC.damage * 0.5f);
    //}
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YuckyBit>(), 2));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Pathogen>(), 2));
        // add back when the mask gets added back
        //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CougherMask>(), 150));
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return ((spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion || spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneUndergroundContagion) &&
            !spawnInfo.Player.InPillarZone() && Main.hardMode) ? 0.7f : 0f;
    }
    int Frame;
    public override void AI()
    {
        if (NPC.ai[2] < 200)
        {
            #region AI
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            float speed = 2;
            var vector17 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            var num149 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
            var num150 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;
            num149 = (int)(num149 / 8f) * 8;
            num150 = (int)(num150 / 8f) * 8;
            vector17.X = (int)(vector17.X / 8f) * 8;
            vector17.Y = (int)(vector17.Y / 8f) * 8;
            num149 -= vector17.X;
            num150 -= vector17.Y;
            var num151 = (float)Math.Sqrt(num149 * num149 + num150 * num150);
            var num152 = num151;
            if (num151 == 0f)
            {
                num149 = NPC.velocity.X;
                num150 = NPC.velocity.Y;
            }
            else
            {
                num151 = 4f / num151;
                num149 *= num151;
                num150 *= num151;
            }
            if (num152 > 100f)
            {
                NPC.ai[0] += 2f;
                if (NPC.ai[0] > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + (speed * 0.023f);
                }
                else
                {
                    NPC.velocity.Y = NPC.velocity.Y - (speed * 0.023f);
                }
                if (NPC.ai[0] < -100f || NPC.ai[0] > 100f)
                {
                    NPC.velocity.X = NPC.velocity.X + (speed * 0.023f);
                }
                else
                {
                    NPC.velocity.X = NPC.velocity.X - (speed * 0.023f);
                }
                if (NPC.ai[0] > 200f)
                {
                    NPC.ai[0] = -200f;
                }
            }
            if (num152 < 150f)
            {
                NPC.velocity.X = NPC.velocity.X + num149 * (speed * 0.007f);
                NPC.velocity.Y = NPC.velocity.Y + num150 * (speed * 0.007f);
            }
            if (Main.player[NPC.target].dead)
            {
                num149 = NPC.direction * 4f / 2f;
                num150 = -4f / 2f;
            }
            if (NPC.velocity.X < num149)
            {
                NPC.velocity.X = NPC.velocity.X + (speed * 0.02f);
                if (NPC.velocity.X < 0f && num149 > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + (speed * 0.02f);
                }
            }
            else if (NPC.velocity.X > num149)
            {
                NPC.velocity.X = NPC.velocity.X - (speed * 0.02f);
                if (NPC.velocity.X > 0f && num149 < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - (speed * 0.02f);
                }
            }
            if (NPC.velocity.Y < num150)
            {
                NPC.velocity.Y = NPC.velocity.Y + (speed * 0.02f);
                if (NPC.velocity.Y < 0f && num150 > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + (speed * 0.02f);
                }
            }
            else if (NPC.velocity.Y > num150)
            {
                NPC.velocity.Y = NPC.velocity.Y - (speed * 0.02f);
                if (NPC.velocity.Y > 0f && num150 < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - (speed * 0.02f);
                }
            }
            var num157 = 0.7f;
            if (NPC.collideX)
            {
                NPC.netUpdate = true;
                NPC.velocity.X = NPC.oldVelocity.X * -num157;
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
                NPC.velocity.Y = NPC.oldVelocity.Y * -num157;
                if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1.5)
                {
                    NPC.velocity.Y = 2f;
                }
                if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1.5)
                {
                    NPC.velocity.Y = -2f;
                }
            }
            if (Main.player[NPC.target].dead)
            {
                NPC.velocity.Y = NPC.velocity.Y - 0.02f * 2f;
                if (NPC.timeLeft > 10)
                {
                    NPC.timeLeft = 10;
                }
            }
            if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
            {
                NPC.netUpdate = true;
                return;
            }
            #endregion AI
        }
        Player TargetPlr = Main.player[NPC.target];
        if (TargetPlr.Center.Distance(NPC.Center) < 150 || NPC.ai[2] < 199 || NPC.ai[2] >= 200)
            NPC.ai[2]++;
        
        if (NPC.ai[2] > 200)
        {
            NPC.velocity *= 0.90f;
        }
        if (NPC.ai[2] > 220)
        {
            Frame = 1;
            NPC.frameCounter = 0;
        }

        if (NPC.ai[2] > 230)
        {
            Frame = 2;
            NPC.frameCounter = 0;
            NPC.velocity = NPC.Center.DirectionTo(TargetPlr.Center).RotatedByRandom(0.5f) * -4.5f;
            NPC.ai[2] = 0;
            if (Main.rand.NextBool(300))
                SoundEngine.PlaySound(CoughSpecial, NPC.Center);
            else
                SoundEngine.PlaySound(Cough, NPC.Center);
            for (int i = 0;i < 20; i++)
            {
                Vector2 ShootDirection = NPC.Center.DirectionTo(TargetPlr.Center).RotatedByRandom(0.5f) * Main.rand.NextFloat(6,3);
                int d = Dust.NewDust(NPC.position + new Vector2(0, NPC.height / 2), NPC.width, NPC.height / 3, DustID.CorruptGibs, ShootDirection.X, ShootDirection.Y, 128, default, Main.rand.NextFloat(1, 1.5f));
                Main.dust[d].noGravity = !Main.rand.NextBool(4);
                if (Main.dust[d].noGravity)
                    Main.dust[d].fadeIn = 1.5f;
            }
            for (int i = 0; i < 20; i++)
            {
                Vector2 ShootDirection = NPC.Center.DirectionTo(TargetPlr.Center).RotatedByRandom(0.5f) * Main.rand.NextFloat(6, 3);
                int d = Dust.NewDust(NPC.position + new Vector2(0, NPC.height / 2), NPC.width, NPC.height / 3, ModContent.DustType<PathogenDust>(), ShootDirection.X, ShootDirection.Y, 128, default, Main.rand.NextFloat(1, 1.5f));
                Main.dust[d].noLightEmittence = true;
                Main.dust[d].noGravity = !Main.rand.NextBool(4);
                if (Main.dust[d].noGravity)
                    Main.dust[d].fadeIn = 1.5f;
            }
            for (int i = 0; i < Main.rand.Next(3,6); i++)
            {
                Vector2 ShootDirection = NPC.Center.DirectionTo(TargetPlr.Center).RotatedByRandom(0.3f) * Main.rand.NextFloat(6, 3);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, ShootDirection, ModContent.ProjectileType<Cough>(), (int)(NPC.damage * 0.6f), 0, 255);
            }
        }

        if (Main.rand.NextBool(20))
        {
            Dust.NewDust(NPC.position + new Vector2(0,NPC.height/2), NPC.width, NPC.height / 3, ModContent.DustType<ContagionWaterSplash>(), Main.rand.NextFloat(0, 0), Main.rand.NextFloat(0, 2), 128, default, Main.rand.NextFloat(1f, 1.3f));
            //Main.dust[d].noGravity = true;
        }

        float maxRotate = 0.4f;
        NPC.rotation = MathHelper.Clamp((NPC.position.X - NPC.oldPosition.X) * 0.1f, -maxRotate, maxRotate);
    }

    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter += 1.0;
        //if (NPC.frameCounter >= 8.0)
        //{
        //    NPC.frame.Y = NPC.frame.Y + frameHeight;
        //    NPC.frameCounter = 0.0;
        //}
        if(Frame > 0 && NPC.frameCounter > 10.0)
        {
            Frame--;
            NPC.frameCounter = 0.0;
        }
        NPC.frame.Y = frameHeight * Frame;
        //if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        //{
        //    NPC.frame.Y = 0;
        //}
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        NPC.ai[0] = 0;
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            if (DateTime.Now.Month == 2 && DateTime.Now.Day == 14)
            {
                for (int num673 = 0; num673 < 10; num673++)
                {
                    int num674 = Main.rand.Next(139, 143);
                    int num675 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, num674, (0f - NPC.velocity.X) * 0.3f, (0f - NPC.velocity.Y) * 0.3f, 0, default(Color), 1.2f);
                    Main.dust[num675].velocity.X += Main.rand.Next(-50, 51) * 0.01f;
                    Main.dust[num675].velocity.Y += Main.rand.Next(-50, 51) * 0.01f;
                    Main.dust[num675].velocity.X *= 1f + Main.rand.Next(-50, 51) * 0.01f;
                    Main.dust[num675].velocity.Y *= 1f + Main.rand.Next(-50, 51) * 0.01f;
                    Main.dust[num675].velocity.X += Main.rand.Next(-50, 51) * 0.05f;
                    Main.dust[num675].velocity.Y += Main.rand.Next(-50, 51) * 0.05f;
                    Dust dust2 = Main.dust[num675];
                    dust2.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                }

                for (int num676 = 0; num676 < 5; num676++)
                {
                    int num677 = Main.rand.Next(276, 283);
                    int num678 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, -NPC.velocity * 0.3f, num677);
                    Main.gore[num678].velocity.X += Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num678].velocity.Y += Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num678].velocity.X *= 1f + Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num678].velocity.Y *= 1f + Main.rand.Next(-50, 51) * 0.01f;
                    Gore gore2 = Main.gore[num678];
                    gore2.scale *= 1f + Main.rand.Next(-20, 21) * 0.01f;
                    Main.gore[num678].velocity.X += Main.rand.Next(-50, 51) * 0.05f;
                    Main.gore[num678].velocity.Y += Main.rand.Next(-50, 51) * 0.05f;
                }
            }

            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("Cougher1").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("Cougher2").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("Cougher3").Type, NPC.scale);
            for (int i = 0; i < 30; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-5, 3), 50, default, 1.5f);
                Main.dust[d].velocity += NPC.velocity * Main.rand.NextFloat(0.6f, 1f);
                Main.dust[d].noGravity = !Main.rand.NextBool(3);
                Main.dust[d].fadeIn = 1.2f;
            }
        }
        for (int i = 0; i < 15; i++)
        {
            int d2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 1), 50, default, Main.rand.NextFloat(1, 1.5f));
            Main.dust[d2].velocity += NPC.velocity * Main.rand.NextFloat(0.2f, 0.8f);
            Main.dust[d2].noGravity = true;
        }
    }
}
