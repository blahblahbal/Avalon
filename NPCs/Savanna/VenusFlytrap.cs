using Avalon;
using Avalon.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Savanna;

public class VenusFlytrap : ModNPC
{
	public int leftHead = -1;
	public int rightHead = -1;
	private int timer = 0;
	private bool spawn = false;
	private float PosX = 0f;
	private float PosY = 0f;
	private static Asset<Texture2D>? bestiaryTexture0;
	private static Asset<Texture2D>? bestiaryTexture1;
	private static Asset<Texture2D>? bestiaryTexture2;
	internal static Asset<Texture2D> FlytrapVine;
	public override void SetStaticDefaults()
	{
		bestiaryTexture0 = ModContent.Request<Texture2D>(Texture + "_Bestiary0");
		bestiaryTexture1 = ModContent.Request<Texture2D>(Texture + "_Bestiary1");
		bestiaryTexture2 = ModContent.Request<Texture2D>(Texture + "_Bestiary2");
		FlytrapVine = ModContent.Request<Texture2D>(Texture + "Vine");

		NPCID.Sets.SpecialSpawningRules.Add(ModContent.NPCType<VenusFlytrap>(), 0);
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		Main.npcFrameCount[Type] = 3;

		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			// rotation doesn't work when a custom texture path is set, so instead we manually draw the bestiary icon in PreDraw
			//CustomTexturePath = Texture + "_Bestiary",
			Position = new Vector2(3f, 9f),
			PortraitPositionXOverride = 4f,
			PortraitPositionYOverride = -6f
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
	}

	public override void SetDefaults()
	{
		NPC.damage = 106;
		NPC.lifeMax = 550;
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
		Vector2 vector6 = new Vector2(NPC.Center.X - NPC.ai[1], NPC.Center.Y - NPC.ai[1]);
		//NPC.rotation = ((float)Math.Atan2(Main.player[NPC.target].Center.Y - (double)NPC.Center.Y, Main.player[NPC.target].Center.X - (double)NPC.Center.X) + 3.14f) * 1f + ((float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X)) * 0.1f;

		NPC.rotation = NPC.Center.DirectionTo(Main.player[NPC.target].Center).ToRotation() + MathHelper.Pi / 2;
		//NPC.velocity = Vector2.Zero;
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		return Main.hardMode && spawnInfo.Player.InModBiome<Biomes.UndergroundTropics>() && !spawnInfo.Player.InPillarZone() ? 0.3f : 0f;
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
		Main.npc[leftHead].life -= damageDone;
		Main.npc[rightHead].life -= damageDone;
		if (Main.npc[leftHead].life <= 0)
		{
			Main.npc[leftHead].checkDead();
		}
		if (Main.npc[rightHead].life <= 0)
		{
			Main.npc[rightHead].checkDead();
		}
	}
	public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
	{
		Main.npc[leftHead].life -= damageDone;
		Main.npc[rightHead].life -= damageDone;
		if (Main.npc[leftHead].life <= 0)
		{
			Main.npc[leftHead].checkDead();
		}
		if (Main.npc[rightHead].life <= 0)
		{
			Main.npc[rightHead].checkDead();
		}
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(leftHead);
		writer.Write(rightHead);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		leftHead = reader.ReadInt32();
		rightHead = reader.ReadInt32();
	}
	public override void OnSpawn(IEntitySource source)
	{
		NPC.frameCounter = NPC.whoAmI * Main.rand.Next(6, 13) % 26;
		leftHead = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 50, (int)NPC.position.Y, ModContent.NPCType<VenusFlytrapSideHead>(), ai3: NPC.whoAmI);
		rightHead = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 50, (int)NPC.position.Y, ModContent.NPCType<VenusFlytrapSideHead>(), ai3: NPC.whoAmI);

		Main.npc[leftHead].ai[0] = rightHead;
		Main.npc[rightHead].ai[0] = leftHead;

		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, leftHead);
		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, rightHead);
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.IsABestiaryIconDummy)
		{
			Main.spriteBatch.Draw(bestiaryTexture0.Value, NPC.Center - new Vector2(100f, -70f), new Rectangle(0, 0, bestiaryTexture0.Value.Width, bestiaryTexture0.Value.Height), Color.White, MathHelper.PiOver4 / 1.5f, new Vector2(bestiaryTexture0.Value.Width / 2, bestiaryTexture0.Value.Height), 1f, SpriteEffects.None, 1f);
			Main.spriteBatch.Draw(bestiaryTexture1.Value, NPC.Center - new Vector2(65f, -57f), new Rectangle(0, 0, bestiaryTexture1.Value.Width, bestiaryTexture1.Value.Height), Color.White, MathHelper.PiOver4, new Vector2(bestiaryTexture1.Value.Width / 2, bestiaryTexture1.Value.Height), 1f, SpriteEffects.None, 1f);
			Main.spriteBatch.Draw(bestiaryTexture2.Value, NPC.Center - new Vector2(70f, -85f), new Rectangle(0, 0, bestiaryTexture2.Value.Width, bestiaryTexture2.Value.Height), Color.White, MathHelper.PiOver2 / 1.5f, new Vector2(bestiaryTexture2.Value.Width / 2, bestiaryTexture2.Value.Height), 1f, SpriteEffects.None, 1f);
			return false;
		}
		Vector2 start = NPC.Center;
		Vector2 end = new Vector2(NPC.ai[1], NPC.ai[2]);
		start -= screenPos;
		end -= screenPos;
		int linklength = FlytrapVine.Height();
		Vector2 chain = end - start;

		float length = (float)chain.Length();
		int numlinks = (int)Math.Ceiling(length / linklength);
		Vector2[] links = new Vector2[numlinks];
		float rotation = (float)Math.Atan2(chain.Y, chain.X);
		for (int i = 0; i < numlinks; i++)
		{
			links[i] = start + chain / numlinks * i;
			Main.spriteBatch.Draw(FlytrapVine.Value, links[i], new Rectangle(0, 0, FlytrapVine.Width(), linklength), Lighting.GetColor((links[i] + screenPos).ToTileCoordinates()), rotation + 1.57f, new Vector2(FlytrapVine.Width() / 2, FlytrapVine.Height()), 1f,
				SpriteEffects.None, 1f);
		}
		return true;
	}
}
/// <summary>
/// Credit to Photonic0 on discord for this
/// </summary>
public class VenusFlytrapIFrames : GlobalNPC
{
	public override bool PreAI(NPC npc)
	{
		UpdateFlytrapIFrames(npc);
		return base.PreAI(npc);
	}
	static bool IsNPCTypeFlytrap(NPC npc) => npc.type == ModContent.NPCType<VenusFlytrap>() || npc.type == ModContent.NPCType<VenusFlytrapSideHead>();
	static int GetAmountOfIframes(Projectile projectile)
	{
		if (projectile.stopsDealingDamageAfterPenetrateHits)
			return int.MaxValue;
		if (projectile.usesOwnerMeleeHitCD)
			return Main.player[projectile.owner].itemAnimation;
		if (projectile.usesIDStaticNPCImmunity)
			return projectile.idStaticNPCHitCooldown;
		if (projectile.usesLocalNPCImmunity)
			return projectile.localNPCHitCooldown < 1 ? 10 : projectile.localNPCHitCooldown / projectile.MaxUpdates;
		return 10;
	}

	public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
	{
		//if (IsNPCTypeFlytrap(npc))
		//{
		//    int iframes = GetAmountOfIframes(projectile);
		//    FlytrapIFrame[projectile.whoAmI] = iframes;
		//    if (projectile.usesOwnerMeleeHitCD)
		//    {
		//        Player player = Main.player[projectile.owner];
		//        FlytrapIFrame[Main.maxProjectiles + projectile.owner] = player.itemAnimation;
		//        player.SetMeleeHitCooldown(npc.whoAmI, player.itemAnimation);
		//    }
		//}

		if (IsNPCTypeFlytrap(npc))
		{
			FlytrapIFrames[projectile.whoAmI] = GetAmountOfIframes(projectile);
		}
	}
	static int[] FlytrapIFrames = new int[Main.maxProjectiles + Main.maxPlayers];//ok so basically the first 1000 slots are for projs and the latter 255 slots are for players
	public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
	{
		if (IsNPCTypeFlytrap(npc))
		{
			FlytrapIFrames[player.whoAmI + Main.maxProjectiles] = player.itemAnimation;
		}
	}
	public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
	{
		if (!IsNPCTypeFlytrap(npc) || FlytrapIFrames[player.whoAmI + Main.maxProjectiles] < 1)
			return null;
		return false;
	}
	/// <summary>
	/// THIS ASSUMES THAT YOU CHECK IF THE NPC IS A DESTROYER BEFOREHAND
	/// </summary>
	static bool IsFlytrapImmuneToThis(Projectile projectile, NPC npc)
	{
		if (!projectile.friendly || projectile.DistanceSQ(npc.Center) > 40000)//checking distance for optimization
			return true;
		if (projectile.usesIDStaticNPCImmunity)
			if (FlytrapIFrames[projectile.whoAmI] < 1 && projectile.friendly)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					if (!npc.active || !IsNPCTypeFlytrap(Main.npc[i]))
						continue;
					if (Projectile.perIDStaticNPCImmunity[projectile.type][i] > 0)
						return true;
				}
			}
		if (projectile.usesLocalNPCImmunity || projectile.usesOwnerMeleeHitCD || projectile.stopsDealingDamageAfterPenetrateHits)
			return FlytrapIFrames[projectile.whoAmI] > 1;
		for (int i = 0; i < Main.maxProjectiles; i++)//attempt at mimmicking global iframes
		{
			if (FlytrapIFrames[i] > 0)
				return true;
		}
		return false;
	}
	public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
	{
		if (!IsNPCTypeFlytrap(npc) || !IsFlytrapImmuneToThis(projectile, npc))
			return null;
		return false;
	}
	public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => IsNPCTypeFlytrap(entity);
	/// <summary>
	/// CALL THIS ON PRE AI OF GLOBAL NPC
	/// </summary>
	static void UpdateFlytrapIFrames(NPC npc)
	{
		if (npc.type == ModContent.NPCType<VenusFlytrap>())
		{
			for (int i = 0; i < FlytrapIFrames.Length; i++)
			{
				if (i < 1000 && !Main.projectile[i].active)
					FlytrapIFrames[i] = 0;
				FlytrapIFrames[i]--;
			}
		}
	}
}
