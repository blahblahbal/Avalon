using System;
using Avalon.Items.Material;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.Enums;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Avalon.NPCs.PreHardmode;

internal class Mosquito : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 6;
        Data.Sets.NPC.Toxic[NPC.type] = true;
    }

    public override void SetDefaults()
    {
        NPC.damage = 25;
        NPC.lifeMax = 51;
        NPC.defense = 12;
        NPC.noGravity = true;
        NPC.width = 58;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.height = 30;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 200;
        //AnimationType = NPCID.Hornet;
        NPC.knockBackResist = 0.5f;
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        NPC.ai[1] = 1;
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MosquitoProboscis>(), 2));
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return spawnInfo.Player.InModBiome<Biomes.UndergroundTropics>() && !spawnInfo.Player.InPillarZone() ? 0.5f : 0f;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Mosquito"))
        });
    }
    public override void FindFrame(int frameHeight)
    {
        if (NPC.ai[1] == 0)
        {
            NPC.frame.X = 0;
            NPC.frame.Width = 74;
        }
        if (NPC.ai[1] == 1)
        {
            NPC.frame.X = 74;
            NPC.frame.Width = 74;
        }
        NPC.frameCounter++;
        if (NPC.frameCounter < 2.0)
            NPC.frame.Y = 0 + (int)(150 * NPC.ai[3]);
        else if (NPC.frameCounter < 4)
            NPC.frame.Y = frameHeight + (int)(150 * NPC.ai[3]);
        else if (NPC.frameCounter < 6)
            NPC.frame.Y = frameHeight * 2 + (int)(150 * NPC.ai[3]);
        else if (NPC.frameCounter < 8)
            NPC.frame.Y = frameHeight + (int)(150 * NPC.ai[3]);
        else
            NPC.frameCounter = 0.0;
    }
    public override void AI()
    {
        if (NPC.ai[1] == 0)
        {
            #region AI
            NPC.spriteDirection = Math.Sign(NPC.PlayerTarget().Center.X - NPC.Center.X);

            if (Vector2.Distance(NPC.Center, NPC.PlayerTarget().Center) < 16 * 20 && Collision.CanHit(NPC, NPC.PlayerTarget()))
            {
                NPC.ai[3] = 1;
                NPC.velocity = NPC.Center.DirectionTo(NPC.PlayerTarget().Center) * 7f;
            }

            if (NPC.ai[3] == 0)
            {
                if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
                {
                    NPC.TargetClosest(true);
                }
                float speed = 2;
                var npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                var targetCenterX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
                var targetCenterY = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;
                targetCenterX = (int)(targetCenterX / 8f) * 8;
                targetCenterY = (int)(targetCenterY / 8f) * 8;
                npcCenter.X = (int)(npcCenter.X / 8f) * 8;
                npcCenter.Y = (int)(npcCenter.Y / 8f) * 8;
                targetCenterX -= npcCenter.X;
                targetCenterY -= npcCenter.Y;
                var num151 = (float)Math.Sqrt(targetCenterX * targetCenterX + targetCenterY * targetCenterY);
                var num152 = num151;
                if (num151 == 0f)
                {
                    targetCenterX = NPC.velocity.X;
                    targetCenterY = NPC.velocity.Y;
                }
                else
                {
                    num151 = 4f / num151;
                    targetCenterX *= num151;
                    targetCenterY *= num151;
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
                    NPC.velocity.X = NPC.velocity.X + targetCenterX * (speed * 0.007f);
                    NPC.velocity.Y = NPC.velocity.Y + targetCenterY * (speed * 0.007f);
                }
                if (Main.player[NPC.target].dead)
                {
                    targetCenterX = NPC.direction * 4f / 2f;
                    targetCenterY = -4f / 2f;
                }
                if (NPC.velocity.X < targetCenterX)
                {
                    NPC.velocity.X = NPC.velocity.X + (speed * 0.02f);
                    if (NPC.velocity.X < 0f && targetCenterX > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + (speed * 0.02f);
                    }
                }
                else if (NPC.velocity.X > targetCenterX)
                {
                    NPC.velocity.X = NPC.velocity.X - (speed * 0.02f);
                    if (NPC.velocity.X > 0f && targetCenterX < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - (speed * 0.02f);
                    }
                }
                if (NPC.velocity.Y < targetCenterY)
                {
                    NPC.velocity.Y = NPC.velocity.Y + (speed * 0.02f);
                    if (NPC.velocity.Y < 0f && targetCenterY > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + (speed * 0.02f);
                    }
                }
                else if (NPC.velocity.Y > targetCenterY)
                {
                    NPC.velocity.Y = NPC.velocity.Y - (speed * 0.02f);
                    if (NPC.velocity.Y > 0f && targetCenterY < 0f)
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
                //if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
                //{
                //    NPC.netUpdate = true;
                //    return;
                //}
            }
            #endregion AI
        }
        if (NPC.ai[1] == 1)
        {
            NPC.ai[2]++;
            if (NPC.ai[2] > 300)
            {
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                return;
            }
            float speed = 0.5f;
            NPC.spriteDirection = -Math.Sign(NPC.PlayerTarget().Center.X - NPC.Center.X);
            if (NPC.velocity.X < NPC.PlayerTarget().Center.X)
            {
                NPC.velocity.X = NPC.velocity.X - (speed * 0.02f);
                if (NPC.velocity.X < 0f && NPC.PlayerTarget().Center.X > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - (speed * 0.02f);
                }
            }
            else if (NPC.velocity.X > NPC.PlayerTarget().Center.X)
            {
                NPC.velocity.X = NPC.velocity.X + (speed * 0.02f);
                if (NPC.velocity.X > 0f && NPC.PlayerTarget().Center.X < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + (speed * 0.02f);
                }
            }
            if (NPC.velocity.Y < NPC.PlayerTarget().Center.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y - (speed * 0.02f);
                if (NPC.velocity.Y < 0f && NPC.PlayerTarget().Center.Y > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - (speed * 0.02f);
                }
            }
            else if (NPC.velocity.Y > NPC.PlayerTarget().Center.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y + (speed * 0.02f);
                if (NPC.velocity.Y > 0f && NPC.PlayerTarget().Center.Y < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + (speed * 0.02f);
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
        }

        float maxRotate = 0.4f;
        NPC.rotation = MathHelper.Clamp((NPC.position.X - NPC.oldPosition.X) * 0.1f, -maxRotate, maxRotate);
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("MosquitoHeadGore").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("MosquitoSacGore").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("MosquitoWingGore").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("MosquitoWingGore").Type, NPC.scale);
        }
    }
}
