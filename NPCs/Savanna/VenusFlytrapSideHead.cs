using Avalon;
using Avalon.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Savanna;

public class VenusFlytrapSideHead : ModNPC
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.SpecialSpawningRules.Add(ModContent.NPCType<VenusFlytrapSideHead>(), 0);
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		Main.npcFrameCount[Type] = 3;
		var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Hide = true
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
	}
	public override void SetDefaults()
	{
		NPC.damage = 106;
		NPC.lifeMax = 5500;
		NPC.defense = 27;
		NPC.noGravity = true;
		NPC.width = 44;
		NPC.aiStyle = -1;
		NPC.npcSlots = 1f;
		NPC.height = 44;
		NPC.HitSound = SoundID.NPCHit32;
		NPC.DeathSound = SoundID.NPCDeath35;
		NPC.value = 200;
		NPC.knockBackResist = 0f;
		NPC.noTileCollide = true;
		SpawnModBiomes = [ModContent.GetInstance<Biomes.UndergroundTropics>().Type];
		DrawOffsetY = 2f;
	}
	public override void OnSpawn(IEntitySource source)
	{
		NPC.frameCounter = NPC.whoAmI * Main.rand.Next(6, 13) % 26;
	}
	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter == 27.0)
		{
			NPC.frameCounter = 0.0;
		}

		if (NPC.frameCounter < 9.0)
		{
			NPC.frame.Y = 0;
		}
		else if (NPC.frameCounter < 18.0)
		{
			NPC.frame.Y = frameHeight;
		}
		else if (NPC.frameCounter < 27.0)
		{
			NPC.frame.Y = frameHeight * 2;
		}
	}
	public int MainHead
	{
		get => (int)NPC.ai[3];
		set => NPC.ai[3] = value;
	}

	private int timer = 0;
	private bool spawn = false;
	private float PosX = 0f;
	private float PosY = 0f;

	public override void AI()
	{
		Vector2 middleHeadPos = Main.npc[MainHead].position;
		if (Vector2.Distance(middleHeadPos, NPC.position) < 44)
		{
			NPC.velocity = Vector2.Normalize(NPC.position - middleHeadPos) * 3f;
		}
		Vector2 otherHeadPos = Main.npc[(int)NPC.ai[0]].position;
		if (Vector2.Distance(otherHeadPos, NPC.position) < 44)
		{
			NPC.velocity = Vector2.Normalize(NPC.position - otherHeadPos) * 3f;
		}
		//if (Main.player[Main.npc[MainHead].target].dead || Main.npc[MainHead].target < 0 || Main.npc[MainHead].target == 255)
		//{
		//	NPC.timeLeft = 0;
		//	NPC.checkDead();
		//	NPC.life = 0;
		//	NPC.active = false;
		//}

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
			PosX = Main.player[Main.npc[MainHead].target].position.X + Main.player[Main.npc[MainHead].target].width * 0.5f;
			PosY = Main.player[Main.npc[MainHead].target].position.Y + Main.player[Main.npc[MainHead].target].height * 0.5f;
			NPC.ai[1] = Main.npc[MainHead].position.X + NPC.width / 2;
			NPC.ai[2] = Main.npc[MainHead].position.Y + NPC.height / 2;
		}

		if (timer > 180)
		{
			timer = 0;
			PosX = Main.player[Main.npc[MainHead].target].position.X + Main.player[Main.npc[MainHead].target].width * 0.5f;
			PosY = Main.player[Main.npc[MainHead].target].position.Y + Main.player[Main.npc[MainHead].target].height * 0.5f;
		}
		else if (timer > 110 || NPC.Distance(new Vector2(NPC.ai[1], NPC.ai[2])) > 450)
		{
			Vector2 vector8 = new Vector2(NPC.position.X + NPC.width * 0.5f - Main.player[Main.npc[MainHead].target].position.X + Main.player[Main.npc[MainHead].target].width * 0.5f, NPC.position.Y + NPC.height * 0.5f - Main.player[NPC.target].position.Y + Main.player[NPC.target].height * 0.5f);
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

		NPC.rotation = NPC.Center.DirectionTo(Main.player[Main.npc[MainHead].target].Center).ToRotation() + MathHelper.Pi / 2;
		//NPC.velocity = Vector2.Zero;
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(ModContent.BuffType<Sticky>(), 60 * 5);
		}
	}
	public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
	{
		Main.npc[MainHead].life -= damageDone;
		if (Main.npc[MainHead].life <= 0)
		{
			Main.npc[MainHead].life = 0;
			Main.npc[MainHead].checkDead();
			Main.npc[MainHead].HitEffect();
		}
	}
	public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
	{
		Main.npc[MainHead].life -= damageDone;
		if (Main.npc[MainHead].life <= 0)
		{
			Main.npc[MainHead].life = 0;
			Main.npc[MainHead].checkDead();
			Main.npc[MainHead].HitEffect();
		}
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Vector2 start = NPC.Center;
		Vector2 end = new Vector2(NPC.ai[1], NPC.ai[2]);
		start -= screenPos;
		end -= screenPos;
		Texture2D TEX = VenusFlytrap.FlytrapVine.Value;
		int linklength = TEX.Height;
		Vector2 chain = end - start;

		float length = (float)chain.Length();
		int numlinks = (int)Math.Ceiling(length / linklength);
		Vector2[] links = new Vector2[numlinks];
		float rotation = (float)Math.Atan2(chain.Y, chain.X);
		for (int i = 0; i < numlinks; i++)
		{
			links[i] = start + chain / numlinks * i;
			Main.spriteBatch.Draw(TEX, links[i], new Rectangle(0, 0, TEX.Width, linklength), Lighting.GetColor((links[i] + screenPos).ToTileCoordinates()), rotation + 1.57f, new Vector2(TEX.Width / 2, TEX.Height), 1f,
				SpriteEffects.None, 1f);
		}
		//if (NPC.IsABestiaryIconDummy)
		//{
		//	Texture2D bestiaryTex = Mod.Assets.Request<Texture2D>("NPCs/Hardmode/EctoHand_Bestiary").Value;
		//	Main.spriteBatch.Draw(bestiaryTex, NPC.Center - new Vector2(70f, -70f), new Rectangle(0, 0, bestiaryTex.Width, bestiaryTex.Height), Color.White, MathHelper.TwoPi / 8, new Vector2(bestiaryTex.Width / 2, bestiaryTex.Height), 1f, SpriteEffects.None, 1f);
		//	return false;
		//}
		return true;
	}
}
