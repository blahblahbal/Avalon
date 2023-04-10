using Terraria.GameContent.Bestiary;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Common.Players;

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
            new FlavorTextBestiaryInfoElement("They reach out, as if to touch their lost life. Or maybe they just want to give you a high-five?")
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
        Vector2 vector7 = new Vector2(NPC.Center.X, NPC.Center.Y);
        float num29 = NPC.ai[1] - vector7.X;
        float num30 = NPC.ai[2] - vector7.Y;

        float rotation7 = (float)Math.Atan2((double)num30, (double)num29) - 1.57f;
        bool flag8 = true;
        while (flag8)
        {
            float num31 = (float)Math.Sqrt((double)(num29 * num29 + num30 * num30));
            if (num31 < 16f)
            {
                flag8 = false;
            }
            else
            {
                num31 = 16f / num31;
                num29 *= num31;
                num30 *= num31;
                vector7.X += num29;
                vector7.Y += num30;
                num29 = NPC.ai[1] - vector7.X;
                num30 = NPC.ai[2] - vector7.Y;

                Color color7 = Lighting.GetColor((int)vector7.X / 16, (int)(vector7.Y / 16f));
                Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("NPCs/Hardmode/EctoArm").Value, new Vector2(vector7.X - v.X, vector7.Y - v.Y), new Rectangle?(new Rectangle(0, 0, Mod.Assets.Request<Texture2D>("NPCs/Hardmode/EctoArm").Value.Width, Mod.Assets.Request<Texture2D>("NPCs/Hardmode/EctoArm").Value.Height)), Color.White, rotation7, new Vector2(Mod.Assets.Request<Texture2D>("NPCs/Hardmode/EctoArm").Value.Width * 0.5f, Mod.Assets.Request<Texture2D>("NPCs/Hardmode/EctoArm").Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
            }
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
        NPC.rotation = ((float)Math.Atan2(Main.player[NPC.target].Center.Y - (double)NPC.Center.Y, Main.player[NPC.target].Center.X - (double)NPC.Center.X) + 3.14f) * 1f + ((float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X)) * 0.1f;
    }
}
