using Terraria.GameContent.Bestiary;
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Avalon.NPCs.Hardmode;

public class Hallowor : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 4;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2(0f, 2f),
			Scale = 1f,
			PortraitScale = 1.2f,
			PortraitPositionYOverride = 10f
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
	}
    public override void SetDefaults()
	{
		NPC.npcSlots = 1;
        NPC.width = 40;
        NPC.height = 38;
        NPC.aiStyle = -1;
        NPC.timeLeft = 1750;
        AnimationType = 75;
        NPC.damage = 62;
        NPC.defense = 28;
        NPC.HitSound = SoundID.NPCHit5;
        NPC.DeathSound = SoundID.NPCDeath7;
        NPC.lifeMax = 530;
        NPC.scale = 1.2f;
        NPC.knockBackResist = 0.35f;
        NPC.noGravity = true;
        NPC.noTileCollide = false;
        NPC.value = 900;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.HalloworBanner>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Hallowor"))
        });
    }
    // uncomment when time to add
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return 0; // Main.hardMode && !spawnInfo.Player.InPillarZone() && spawnInfo.Player.ZoneHallow && spawnInfo.SpawnTileY < (Main.maxTilesY - 200) ? 0.3f : 0f;
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.PixieDust, 3));
        npcLoot.Add(ItemDropRule.StatusImmunityItem(ItemID.Megaphone, 50));
    }
    public override void AI()
    {
        //if (NPC.ai[3] == 0)
        //{
        //    int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Pixie);
        //    Main.npc[n].AddBuff(ModContent.BuffType<Buffs.PixieHalloworBuff>(), 60 * 15);
        //    Main.npc[n].velocity.X -= 0.5f;

        //    int n2 = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Pixie);
        //    Main.npc[n2].AddBuff(ModContent.BuffType<Buffs.PixieHalloworBuff>(), 60 * 15);
        //    Main.npc[n].velocity.Y -= 0.5f;

        //    int n3 = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Pixie);
        //    Main.npc[n3].AddBuff(ModContent.BuffType<Buffs.PixieHalloworBuff>(), 60 * 15);
        //    Main.npc[n].velocity.X += 0.5f;
        //    NPC.ai[3] = 1;
        //}
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest(true);
        }
        float num73 = 4.2f;
        float num74 = 0.022f;
        Vector2 vector11 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
        float num75 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
        float num76 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;
        num75 = (int)(num75 / 8f) * 8;
        num76 = (int)(num76 / 8f) * 8;
        vector11.X = (int)(vector11.X / 8f) * 8;
        vector11.Y = (int)(vector11.Y / 8f) * 8;
        num75 -= vector11.X;
        num76 -= vector11.Y;
        float num77 = (float)Math.Sqrt((double)(num75 * num75 + num76 * num76));
        float num78 = num77;
        if (num77 == 0f)
        {
            num75 = NPC.velocity.X;
            num76 = NPC.velocity.Y;
        }
        else
        {
            num77 = num73 / num77;
            num75 *= num77;
            num76 *= num77;
        }
        NPC.ai[0]++;
        if (NPC.ai[0] > 0f)
        {
            NPC.velocity.Y += 0.023f;
        }
        else
        {
            NPC.velocity.Y -= 0.023f;
        }
        if (NPC.ai[0] is < -100f or > 100f)
        {
            NPC.velocity.X += 0.023f;
        }
        else
        {
            NPC.velocity.X -= 0.023f;
        }
        if (NPC.ai[0] > 200f)
        {
            NPC.ai[0] = -200f;
        }
        NPC.velocity.X += num75 * 0.007f;
        NPC.velocity.Y += num76 * 0.007f;
        if (Main.player[NPC.target].dead)
        {
            num75 = NPC.direction * num73 / 2f;
            num76 = -num73 / 2f;
        }
        if (NPC.velocity.X < num75)
        {
            NPC.velocity.X += num74;
            if (NPC.velocity.X < 0f && num75 > 0f)
            {
                NPC.velocity.X += num74;
            }
        }
        else
        {
            if (NPC.velocity.X > num75)
            {
                NPC.velocity.X -= num74;
                if (NPC.velocity.X > 0f && num75 < 0f)
                {
                    NPC.velocity.X -= num74;
                }
            }
        }
        if (NPC.velocity.Y < num76)
        {
            NPC.velocity.Y += num74;
            if (NPC.velocity.Y < 0f && num76 > 0f)
            {
                NPC.velocity.Y += num74;
            }
        }
        else
        {
            if (NPC.velocity.Y > num76)
            {
                NPC.velocity.Y -= num74;
                if (NPC.velocity.Y > 0f && num76 < 0f)
                {
                    NPC.velocity.Y -= num74;
                }
            }
        }
        NPC.rotation = (float)Math.Atan2((double)num76, (double)num75) - 1.57f;
        float num83 = 0.7f;
        if (NPC.collideX)
        {
            NPC.netUpdate = true;
            NPC.velocity.X = NPC.oldVelocity.X * -num83;
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
            NPC.velocity.Y = NPC.oldVelocity.Y * -num83;
            if (NPC.velocity.Y is > 0f and < 1.5f)
            {
                NPC.velocity.Y = 2f;
            }
            if (NPC.velocity.Y is < 0f and > -1.5f)
            {
                NPC.velocity.Y = -2f;
            }
        }
        if (Main.rand.NextBool(20))
        {
            // Was originally vile dust but I changed it
            int num85 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + NPC.height * 0.25f), NPC.width, (int)(NPC.height * 0.5f), DustID.Pixie, NPC.velocity.X, 2f, 75, NPC.color, NPC.scale);
            Main.dust[num85].velocity.X *= 0.5f;
            Main.dust[num85].velocity.Y *= 0.1f;
        }
        if (NPC.wet)
        {
            if (NPC.velocity.Y > 0f)
            {
                NPC.velocity.Y *= 0.95f;
            }
            NPC.velocity.Y -= 0.3f;
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
            NPC.localAI[0]++;
            if (NPC.localAI[0] is 180f or 190 or 200)
            {
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + NPC.width / 2 + NPC.velocity.X), (int)(NPC.position.Y + NPC.height / 2 + NPC.velocity.Y), ModContent.NPCType<HallowSpit>(), 0);
				}
				if (NPC.localAI[0] == 200)
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
                int D = Dust.NewDust(NPC.position, R.Width, R.Height, DustID.Enchanted_Gold, 0, 0, 100, new Color(), 2f);
                Main.dust[D].noGravity = true;
                Main.dust[D].velocity.X = vR * (Main.dust[D].position.X - (NPC.position.X + (NPC.width / 2)));
                Main.dust[D].velocity.Y = vR * (Main.dust[D].position.Y - (NPC.position.Y + (NPC.height / 2)));
            }
            for (int i2 = 1; i2 <= C; i2++)
            {
                int D2 = Dust.NewDust(NPC.position, R.Width, R.Height, DustID.MagicMirror, 0, 0, 100, new Color(), 2f);
                Main.dust[D2].noGravity = true;
                Main.dust[D2].velocity.X = vR * (Main.dust[D2].position.X - (NPC.position.X + (NPC.width / 2)));
                Main.dust[D2].velocity.Y = vR * (Main.dust[D2].position.Y - (NPC.position.Y + (NPC.height / 2)));
            }
        }
    }
}
