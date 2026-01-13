using Avalon;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Contagion;

public class Viriling : ModNPC
{
    public override void SetStaticDefaults()
    {
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		Main.npcFrameCount[NPC.type] = 4;
    }

    public override void SetDefaults()
    {
        NPC.width = 32;
        NPC.height = 40;
        NPC.gfxOffY = 6;
        NPC.damage = 23;
        NPC.lifeMax = 122;
        NPC.defense = 9;
        NPC.noGravity = true;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.value = 55f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0.3f;
        NPC.scale = 1f;
		if (NPC.IsABestiaryIconDummy)
		{
			NPC.alpha = 0;
		}
		else
		{
			NPC.alpha = 128;
		}
        NPC.noTileCollide = true;
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Viriling"))
        });
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Booger>(), 2));
        //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 2));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Pathogen>(), 2, 1, 1 + (!Main.rand.NextBool(3) ? 1 : 0)));
    }

    bool spawned;
    public override void AI()
    {
        if (!spawned)
        {
            spawned = true;
            int J = Main.rand.Next(0, 3);
            if (J == 1)
            {
                NPC.lifeMax = (int)(NPC.lifeMax * 0.9f);
                NPC.defense = (int)(NPC.defense * 0.8f);
                NPC.scale *= 0.9f;
                NPC.knockBackResist *= 1.2f;
                NPC.value *= 0.8f;
            }
            if (J == 2)
            {
                NPC.lifeMax = (int)(NPC.lifeMax * 1.2f);
                NPC.defense = (int)(NPC.defense * 1.1f);
                NPC.scale *= 1.15f;
                NPC.knockBackResist *= 0.9f;
                NPC.value *= 1.2f;
            }
            NPC.life = NPC.lifeMax;
            NPC.Size *= NPC.scale;
            NPC.netUpdate = true;
        }
        #region AI
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest(true);
        }
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
        }
        if (num152 < 150f)
        {
            NPC.velocity.X = NPC.velocity.X + num149 * 0.007f;
            NPC.velocity.Y = NPC.velocity.Y + num150 * 0.007f;
        }
        if (Main.player[NPC.target].dead)
        {
            num149 = NPC.direction * 4f / 2f;
            num150 = -4f / 2f;
        }
        if (NPC.velocity.X < num149)
        {
            NPC.velocity.X = NPC.velocity.X + 0.02f;
            if (NPC.velocity.X < 0f && num149 > 0f)
            {
                NPC.velocity.X = NPC.velocity.X + 0.02f;
            }
        }
        else if (NPC.velocity.X > num149)
        {
            NPC.velocity.X = NPC.velocity.X - 0.02f;
            if (NPC.velocity.X > 0f && num149 < 0f)
            {
                NPC.velocity.X = NPC.velocity.X - 0.02f;
            }
        }
        if (NPC.velocity.Y < num150)
        {
            NPC.velocity.Y = NPC.velocity.Y + 0.02f;
            if (NPC.velocity.Y < 0f && num150 > 0f)
            {
                NPC.velocity.Y = NPC.velocity.Y + 0.02f;
            }
        }
        else if (NPC.velocity.Y > num150)
        {
            NPC.velocity.Y = NPC.velocity.Y - 0.02f;
            if (NPC.velocity.Y > 0f && num150 < 0f)
            {
                NPC.velocity.Y = NPC.velocity.Y - 0.02f;
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
        if ((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f || NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f || NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f || NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f) && !NPC.justHit)
        {
            NPC.netUpdate = true;
            return;
        }
        #endregion AI
        //if (Main.expertMode)
        //{
        //    NPC.ai[2]++;
        //    if (NPC.ai[2] == 360)
        //    {
        //        NPC.noTileCollide = true;
        //    }
        //    if (NPC.ai[2] is > 380 and < 400)
        //    {
        //        if (Main.rand.NextBool(3))
        //        {
        //            int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-3, 2), 128, default, Main.rand.NextFloat(1, 1.5f));
        //            Main.dust[d].noGravity = true;
        //        }
        //        NPC.alpha += 5;
        //    }
        //}

        if ((!Collision.SolidCollision(NPC.position, NPC.width, NPC.height) || !NPC.noTileCollide) && NPC.alpha > 0 && Main.player[NPC.target].position.Distance(NPC.position) < 640)
        {
            NPC.noTileCollide = false;
            NPC.alpha--;
        }

        if (Main.rand.NextBool(20))
        {
            int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-3, 2), 128, default, Main.rand.NextFloat(1, 1.5f));
            Main.dust[d].noGravity = true;
        }

        float maxRotate = 0.4f;
        NPC.rotation = MathHelper.Clamp(NPC.velocity.X * 0.08f, -maxRotate, maxRotate);
    }

    public override void FindFrame(int frameHeight)
    {
        //Player TargetPlayer = Main.player[NPC.target];
        NPC.frameCounter += 1.0;
        if (NPC.frameCounter >= 8.0)
        {
            NPC.frame.Y = NPC.frame.Y + frameHeight;
            NPC.frameCounter = 0.0;
        }
        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }

        //int NPCFrame = 0;
        //float BacPos = NPC.Center.X;
        //float playerDist = Vector2.Distance(new Vector2(NPC.Center.X, 0), new Vector2(TargetPlayer.Center.X, 0));

        //int D = 120;

        //if (playerDist < D)
        //    NPCFrame = 0;
        //if (playerDist > D)
        //    NPCFrame = 1;
        //if (playerDist > D * 2)
        //    NPCFrame = 2;

        //if (TargetPlayer.Center.X < BacPos)
        //    NPCFrame += 4;

        //NPC.frame.Y = NPCFrame * frameHeight;
        //NPC.frameCounter--;
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("BactusMinion1").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("BactusMinion2").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("BactusMinion3").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("BactusMinion4").Type, NPC.scale);
            for (int i = 0; i < 10; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-5, 3), 50, default, 1);
                Main.dust[d].velocity += NPC.velocity * Main.rand.NextFloat(0.6f, 1f);
                Main.dust[d].noGravity = true;
                Main.dust[d].fadeIn = 1.2f;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            int d2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 1), 50, default, Main.rand.NextFloat(1, 1.5f));
            Main.dust[d2].velocity += NPC.velocity * Main.rand.NextFloat(0.2f, 0.8f);
            Main.dust[d2].noGravity = true;
        }
    }
}
