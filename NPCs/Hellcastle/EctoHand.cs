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
using ReLogic.Content;
using Avalon;

namespace Avalon.NPCs.Hellcastle;

public class EctoHand : ModNPC
{
    private int timer = 0;
    private bool spawn = false;
    private float PosX = 0f;
    private float PosY = 0f;
	private static Asset<Texture2D> chain1;
	private static Asset<Texture2D> bestiaryTexture;

	public override void SetStaticDefaults()
	{
		chain1 = ModContent.Request<Texture2D>(Texture + "_Chain");
		bestiaryTexture = ModContent.Request<Texture2D>(Texture + "_Bestiary");

		Main.npcFrameCount[NPC.type] = 1;
        NPCID.Sets.SpecialSpawningRules.Add(ModContent.NPCType<EctoHand>(), 0);
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
			// rotation doesn't work when a custom texture path is set, so instead we manually draw the bestiary icon in PreDraw
			//CustomTexturePath = Texture + "_Bestiary",
			Position = new Vector2(4f, -6f),
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        Data.Sets.NPCSets.Undead[NPC.type] = true;
		NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
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
        NPC.lifeMax = (int)(NPC.lifeMax * 0.9f);
    }

    public override Color? GetAlpha(Color drawColor)
    {
        return Color.White;
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 v, Color drawColor)
    {
		if (NPC.IsABestiaryIconDummy)
		{
			Main.spriteBatch.Draw(bestiaryTexture.Value, NPC.Center - new Vector2(70f, -70f), new Rectangle(0, 0, bestiaryTexture.Value.Width, bestiaryTexture.Value.Height), Color.White, MathHelper.TwoPi / 8, new Vector2(bestiaryTexture.Value.Width / 2, bestiaryTexture.Value.Height), 1f, SpriteEffects.None, 1f);
			return false;
		}
		int divisions = 4;
		Vector2 start = NPC.Center;
		Vector2 end = new Vector2(NPC.ai[1], NPC.ai[2]);
		int iterations = (int)Math.Ceiling(start.Distance(end) / chain1.Height() * divisions);
		Vector2 direction = start.DirectionTo(end);
		float rotation = direction.ToRotation();
		for (int i = 0; i < iterations; i++)
		{
			Rectangle frame = chain1.Frame(1, divisions, 0, (i + 1) % divisions);
			Main.spriteBatch.Draw(chain1.Value, start + (direction * i * chain1.Height() / divisions) - Main.screenPosition, frame, Color.White, rotation + 1.57f, new Vector2(frame.Width / 2, frame.Height), 1f, SpriteEffects.None, 1f);
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
                int num35 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, NPC.velocity.X, NPC.velocity.Y, 0, default, 1f);
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
        int i = (int)NPC.Center.X / 16;
        int j = (int)NPC.Center.Y / 16;
        while (j < Main.maxTilesY - 10 && Main.tile[i, j] != null && !WorldGen.SolidTile2(i, j) && Main.tile[i - 1, j] != null && !WorldGen.SolidTile2(i - 1, j) && Main.tile[i + 1, j] != null && !WorldGen.SolidTile2(i + 1, j))
            j += 2;
        int num = j - 1;
        float worldY = num * 16;
        if (!spawn)
        {
            spawn = true;
            NPC.position.Y = worldY;
            PosX = Main.player[NPC.target].position.X + Main.player[NPC.target].width * 0.5f;
            PosY = Main.player[NPC.target].position.Y + Main.player[NPC.target].height * 0.5f;
            NPC.ai[1] = NPC.position.X + NPC.width / 2;
            NPC.ai[2] = NPC.position.Y + NPC.height / 2;
        }

        if (timer > 180)
        {
            timer = 0;
            PosX = Main.player[NPC.target].position.X + Main.player[NPC.target].width * 0.5f;
            PosY = Main.player[NPC.target].position.Y + Main.player[NPC.target].height * 0.5f;
        }
        else if (timer > 110 || NPC.Distance(new Vector2(NPC.ai[1], NPC.ai[2])) > 450)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + NPC.width * 0.5f - Main.player[NPC.target].position.X + Main.player[NPC.target].width * 0.5f, NPC.position.Y + NPC.height * 0.5f - Main.player[NPC.target].position.Y + Main.player[NPC.target].height * 0.5f);
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

		float armRotation = NPC.Center.DirectionFrom(new Vector2(NPC.ai[1], NPC.ai[2])).ToRotation();
		float targetRotation = NPC.Center.DirectionTo(Main.player[NPC.target].Center).ToRotation();
		if (NPC.HasValidTarget && Main.player[NPC.target].Hitbox.IntersectsConeFastInaccurate(NPC.Center, 600, armRotation, 1f))
		{
			NPC.rotation = Utils.AngleLerp(NPC.rotation, targetRotation + MathHelper.Pi, 0.1f);
		}
		else
		{
			NPC.rotation = Utils.AngleLerp(NPC.rotation, armRotation + MathHelper.Pi, 0.05f);
		}

		if (Main.LocalPlayer.HeldItem.type != 1)
			NPC.velocity = Vector2.Zero;

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
