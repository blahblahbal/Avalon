using Terraria.GameContent.Bestiary;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Common.Players;
using Terraria.GameContent;
using Terraria.Localization;

namespace Avalon.NPCs.Hardmode;

public class EctoHand : ModNPC
{
    private int timer = 0;
    private bool spawn = false;
    private float PosX = 0f;
    private float PosY = 0f;
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 1;
        NPCID.Sets.SpecialSpawningRules.Add(ModContent.NPCType<EctoHand>(), 0);
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            // Influences how the NPC looks in the Bestiary
            Position = new Vector2(4f, -6f),
            Rotation = (float)Math.PI * 3f / 4f
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        Data.Sets.NPC.Undead[NPC.type] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
	}
    public override void SetDefaults()
    {
        NPC.damage = 155;
        NPC.lifeMax = 3600;
        NPC.defense = 55;
        NPC.width = 30;
        NPC.height = 30;
        NPC.aiStyle = -1;
        NPC.scale = 1.3f;
        NPC.value = 1000f;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit36;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.behindTiles = false;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.EctoHandBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Hellcastle>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.EctoHand"))
        });
    }
    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.5f);
    }

    public override Color? GetAlpha(Color drawColor)
    {
        return Color.White;
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 v, Color drawColor)
    {
        Vector2 start = NPC.Center;
        Vector2 end = new Vector2(NPC.ai[1], NPC.ai[2]);
        start -= Main.screenPosition;
        end -= Main.screenPosition;
        Texture2D TEX = Mod.Assets.Request<Texture2D>("NPCs/Hardmode/EctoArm1").Value;
        int linklength = TEX.Height;
        Vector2 chain = end - start;

        float length = (float)chain.Length();
        int numlinks = (int)Math.Ceiling(length / linklength);
        Vector2[] links = new Vector2[numlinks];
        float rotation = (float)Math.Atan2(chain.Y, chain.X);
        int chainVariant = 1;
        for (int i = 0; i < numlinks; i++)
        {
            links[i] = start + chain / numlinks * i;
            string TEXTURE;
            if (chainVariant % 3 == 0 || chainVariant % 4 == 0)
            {
                TEXTURE = "NPCs/Hardmode/EctoArm2";
            }
            else
            {
                TEXTURE = "NPCs/Hardmode/EctoArm1";
            }
            if (chainVariant % 2 == 0)
            {
                TEX = Mod.Assets.Request<Texture2D>(TEXTURE).Value;
            }
            else
            {
                TEX = Mod.Assets.Request<Texture2D>(TEXTURE + "B").Value;
            }
            Main.spriteBatch.Draw(TEX, links[i], new Rectangle(0, 0, TEX.Width, linklength), Color.White, rotation + 1.57f, new Vector2(TEX.Width / 2, TEX.Height), 1f,
                SpriteEffects.None, 1f);
            chainVariant++;
            if (chainVariant > 4)
            {
                chainVariant = 1;
            }
        }
        if (NPC.IsABestiaryIconDummy)
        {
            Texture2D bestiaryTex = Mod.Assets.Request<Texture2D>("NPCs/Hardmode/EctoHand_Bestiary").Value;
            Main.spriteBatch.Draw(bestiaryTex, NPC.Center - new Vector2(70f, -70f), new Rectangle(0, 0, bestiaryTex.Width, bestiaryTex.Height), Color.White, MathHelper.TwoPi / 8, new Vector2(bestiaryTex.Width / 2, bestiaryTex.Height), 1f, SpriteEffects.None, 1f);
            return false;
        }
        return true;
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneHellcastle && Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].WallType == (ushort)ModContent.WallType<Walls.ImperviousBrickWallUnsafe>())
        {
            return 2f;
        }
        return 0f;
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0)
        {
            for (int num34 = 0; num34 < 50; num34++)
            {
                int num35 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, NPC.velocity.X, NPC.velocity.Y, 0, default(Color), 1f);
                Main.dust[num35].velocity *= 2f;
                Main.dust[num35].noGravity = true;
                Main.dust[num35].scale = 1.4f;
            }
        }
    }
    public override void AI()
    {
        timer++;
        NPC.TargetClosest(true);
        int i = (int)(NPC.Center.X) / 16;
        int j = (int)(NPC.Center.Y) / 16;
        while (j < Main.maxTilesY - 10 && Main.tile[i, j] != null && (!WorldGen.SolidTile2(i, j) && Main.tile[i - 1, j] != null) && (!WorldGen.SolidTile2(i - 1, j) && Main.tile[i + 1, j] != null && !WorldGen.SolidTile2(i + 1, j)))
            j += 2;
        int num = j - 1;
        float worldY = num * 16;
        if (!spawn)
        {
            spawn = true;
            NPC.position.Y = worldY;
            PosX = Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f);
            PosY = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f);
            NPC.ai[1] = NPC.position.X + (float)(NPC.width / 2);
            NPC.ai[2] = NPC.position.Y + (float)(NPC.height / 2);
        }

        if (timer > 180)
        {
            timer = 0;
            PosX = Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f);
            PosY = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f);
        }
        else if (timer > 110 || NPC.Distance(new Vector2(NPC.ai[1], NPC.ai[2])) > 450)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * 0.5f) - Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f), NPC.position.Y + (NPC.height * 0.5f) - Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f));
            PosX = NPC.ai[1] - vector8.X * 1f;
            PosY = NPC.ai[2] - vector8.Y * 1f;
        }
        if (PosX < NPC.position.X)
        {
            if (NPC.velocity.X > -4) { NPC.velocity.X -= 0.25f; }
        }
        else if (PosX > NPC.Center.X)
        {
            if (NPC.velocity.X < 4) { NPC.velocity.X += 0.25f; }
        }
        if (PosY < NPC.position.Y)
        {
            if (NPC.velocity.Y > -4) NPC.velocity.Y -= 0.25f;
        }
        else if (PosY > NPC.Center.Y)
        {
            if (NPC.velocity.Y < 4) NPC.velocity.Y += 0.25f;
        }
        Vector2 vector6 = new Vector2(NPC.Center.X - NPC.ai[1], NPC.Center.Y - NPC.ai[1]);
        //NPC.rotation = ((float)Math.Atan2(Main.player[NPC.target].Center.Y - (double)NPC.Center.Y, Main.player[NPC.target].Center.X - (double)NPC.Center.X) + 3.14f) * 1f + ((float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X)) * 0.1f;

        //make the fucking wrist rotation look like a wrist JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO JEO
        NPC.rotation = NPC.Center.DirectionTo(Main.player[NPC.target].Center).ToRotation() + MathHelper.Pi;

        if (Main.rand.NextBool(15))
        {
            int num1225 = Dust.NewDust(NPC.Center + new Vector2(Main.rand.NextFloat(0, NPC.Center.Distance(new Vector2(NPC.ai[1], NPC.ai[2]))), 0).RotatedBy(NPC.Center.DirectionTo(new Vector2(NPC.ai[1], NPC.ai[2])).ToRotation()), (int)(NPC.width * 0.1f),
                (int)(NPC.height * 0.1f), DustID.HallowSpray, 0, 0, 150, NPC.color, 0.85f);
            Main.dust[num1225].noGravity = true;
            Main.dust[num1225].velocity *= 0.95f;
            int num1226 = Dust.NewDust(NPC.Center, NPC.width,
                (int)(NPC.height * 0.1f), DustID.HallowSpray, 0, 0, 150, NPC.color, 0.75f);
            Main.dust[num1226].noGravity = true;
            Main.dust[num1226].velocity *= 0.95f;
        }
        Lighting.AddLight(NPC.Center, 14f / 255f, 80f / 255f, 100f / 255f);
    }
}
