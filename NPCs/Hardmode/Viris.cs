using System;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Avalon.Common;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.GameContent;
using ReLogic.Content;

namespace Avalon.NPCs.Hardmode;

public class Viris : ModNPC
{
	private static Asset<Texture2D> texture;
	public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 8;
        NPCID.Sets.TrailCacheLength[NPC.type] = 5;
        NPCID.Sets.TrailingMode[NPC.type] = 7;
        Data.Sets.NPCSets.Wicked[NPC.type] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        texture = TextureAssets.Npc[Type];
	}
    public override void SetDefaults()
    {
        NPC.damage = 40;
        NPC.lifeMax = 300;
        NPC.defense = 12;
        NPC.noGravity = true;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.value = 610f;
        NPC.Size = new Vector2(100, 120);
        NPC.scale = 1f;
        NPC.knockBackResist = 0.6f;
        NPC.HitSound = SoundID.NPCHit18;
        NPC.DeathSound = SoundID.NPCDeath21;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.VirisBanner>();
        NPC.noTileCollide = true;
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
        //DrawOffsetY = 10;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Viris"))
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.GetModPlayer<Common.Players.AvalonBiomePlayer>().ZoneUndergroundContagion && !spawnInfo.Player.ZoneDungeon && Main.hardMode
            ? 0.6f : 0f;
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YuckyBit>(), 2,2,4));

        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsNotUp(), 5091, 1500));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsUp(), 5091, 500));
    }
    public override void AI()
    {
        #region AI
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest(true);
        }
        Player Target = Main.player[NPC.target];

        float speed = 4;

        if (Target.Center.Distance(NPC.Center) < 250)
        {
            NPC.velocity += NPC.Center.DirectionTo(Target.Center) * 0.3f;
        }
        else
        {
            NPC.velocity += NPC.Center.DirectionTo(Target.Center + Main.rand.NextVector2CircularEdge(600, 600)) * 0.1f;
            NPC.netUpdate = true;
        }

        NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -speed, speed);
        NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -speed, speed);
        #endregion AI
        if (Main.rand.NextBool(20))
        {
            int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-3, 2), 128, default, Main.rand.NextFloat(1, 1.5f));
            Main.dust[d].noGravity = true;
        }

        float maxRotate = 0.4f;
        NPC.rotation = MathHelper.Clamp(NPC.velocity.X * 0.06f, -maxRotate, maxRotate);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        int frameHeight = texture.Value.Height / Main.npcFrameCount[NPC.type];
        Rectangle frame = NPC.frame;
        Vector2 drawPos = NPC.position + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition;
        float alphaThingHackyWow = 0;
        for (int i = 4; i > 0; i--)
        {
            alphaThingHackyWow += 0.1f;
            Main.EntitySpriteDraw(texture.Value, NPC.oldPos[i] + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition, frame, new Color(drawColor.R / 2, drawColor.G, 0,128) * alphaThingHackyWow, NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, SpriteEffects.None, 0);
        }
        //Main.EntitySpriteDraw(texture, drawPos, frame, drawColor, NPC.rotation, new Vector2(NPC.frame.Width / 2,NPC.frame.Height / 2), NPC.scale,SpriteEffects.None,0);
        return true;
    }

    public override void FindFrame(int frameHeight)
    {
        Player TargetPlayer = Main.player[NPC.target];
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
        if (NPC.life <= 0)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("Viris").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("Viris2").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("Viris3").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("Viris4").Type, NPC.scale);
                for (int i = 0; i < 30; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-4, 2), 50, default, 2);
                    Main.dust[d].velocity += NPC.velocity * Main.rand.NextFloat(0.6f, 1f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].fadeIn = 1.2f;
                    if(i % 6 == 0)
                    Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.2f) * Main.rand.NextFloat(-0.2f, 0.9f), GoreID.MaggotZombieMaggotPieces, NPC.scale);
                }
            }
            if (Main.netMode != 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    NPC N = NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, ModContent.NPCType<Viriling>(), 0);
                    N.velocity = Main.rand.NextVector2Circular(5, 5);
                    if (Main.netMode == 2 && NPC.whoAmI < 200)
                        NetMessage.SendData(MessageID.SyncNPC, number: N.whoAmI);
                }
                for (int i = 0; i < Main.rand.Next(3,14); i++)
                {
                    NPC N = NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, NPCID.Maggot, 0);
                    N.velocity = Main.rand.NextVector2Circular(5, 5);
                    N.dontTakeDamageFromHostiles= true;
                    if (Main.netMode == 2 && NPC.whoAmI < 200)
                        NetMessage.SendData(MessageID.SyncNPC, number: N.whoAmI);
                }
            }
        }
        for (int i = 0; i < 15; i++)
        {
            int d2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 1), 50, default, Main.rand.NextFloat(1, 1.5f));
            Main.dust[d2].velocity += NPC.velocity * Main.rand.NextFloat(0.2f, 0.8f);
            Main.dust[d2].noGravity = true;
        }
    }
}
