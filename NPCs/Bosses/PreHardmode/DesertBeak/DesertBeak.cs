using Avalon;
using Avalon.DropConditions;
using Avalon.Items.BossBags;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Placeable.Trophy.Relics;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Magic.PreHardmode.TomeoftheDistantPast;
using Avalon.Items.Weapons.Ranged.PreHardmode.EggCannon;
using Avalon.NPCs.Bosses.PreHardmode.DesertBeak.Projectiles;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode.DesertBeak;

[AutoloadBossHead]
public class DesertBeak : ModNPC
{
	public int leftWing = -1;
	public int rightWing = -1;
	public override void SetStaticDefaults()
	{
		NPCID.Sets.MPAllowedEnemies[Type] = true;

		Main.npcFrameCount[NPC.type] = 8;

		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2(30, 60),
			PortraitPositionXOverride = 0,
			Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.TrailCacheLength[Type] = 16;
		NPCID.Sets.TrailingMode[Type] = 7;
	}
	public override void SetDefaults()
	{
		NPC.TargetClosest();
		NPC.damage = 45;
		NPC.boss = true;
		NPC.noTileCollide = true;
		NPC.lifeMax = 4150;
		NPC.defense = 30;
		NPC.noGravity = true;
		NPC.width = 62;
		NPC.aiStyle = -1;
		NPC.npcSlots = 100f;
		NPC.value = 50000f;
		NPC.timeLeft = 22500;
		NPC.height = 78;
		NPC.knockBackResist = 0f;
		NPC.HitSound = new SoundStyle("Terraria/Sounds/NPC_Hit_28") { Pitch = -0.09f };
		NPC.DeathSound = new SoundStyle("Terraria/Sounds/NPC_Killed_31") { Pitch = -0.09f };
		Music = ExxoAvalonOrigins.MusicMod != null ? Main.swapMusic ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DesertBeakEnnway") : MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DesertBeak") : MusicID.Boss4;
		NPC.scale = 1f;
		phase = 0;
		FlapMultiplier = 1;
		//NPC.dontTakeDamage = true;
	}
	//public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	//{
	//    NPC.lifeMax = (int)(NPC.lifeMax * 0.66f * bossAdjustment);
	//    NPC.damage = (int)(NPC.damage * 0.58f);
	//}
	public override void OnKill()
	{
		Terraria.GameContent.Events.Sandstorm.StopSandstorm();
		if (!ModContent.GetInstance<SyncAvalonWorldData>().DownedDesertBeak)
		{
			NPC.SetEventFlagCleared(ref ModContent.GetInstance<SyncAvalonWorldData>().DownedDesertBeak, -1);
		}
		foreach (NPC npc in Main.ActiveNPCs)
		{
			if (npc.type == ModContent.NPCType<DesertBeakWingNPC>() && (npc.whoAmI == leftWing || npc.whoAmI == rightWing))
			{
				//Main.npc[i].active = false;
				npc.life = 0;
				npc.checkDead();
			}
		}
		//spawnedWings = false;
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertBeakTrophy>(), 10));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.SandBlock, 1, 22, 55));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertBeakMask>(), 7));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertFeather>(), 1, 9, 13));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TomeoftheDistantPast>(), 3));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EggCannon>(), 3));
		npcLoot.Add(ItemDropRule.ByCondition(new IridiumWorldDropAndNotExpert(), ModContent.ItemType<IridiumOre>(), 1, 15, 26));
		npcLoot.Add(ItemDropRule.ByCondition(new RhodiumWorldDropAndNotExpert(), ModContent.ItemType<RhodiumOre>(), 1, 15, 26));
		npcLoot.Add(ItemDropRule.ByCondition(new OsmiumWorldDropAndNotExpert(), ModContent.ItemType<OsmiumOre>(), 1, 15, 26));

		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DesertBeakBossBag>()));
		npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<DesertBeakRelic>()));
	}

	byte phase;
	byte phase1Feather = 0;
	byte phase1Egg = 1;
	byte phase2Transition = 2;
	byte phase2Circle = 3;
	byte phase2Tornados = 4;
	byte phase2Egg = 6;

	float FlapMultiplier = 1;
	bool Storming;
	bool Pulsing;
	int afterImageTimer;
	float pulseTimer = 0;
	float delay;
	bool enraged = false;

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			 BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.DesertBeak"))
		});
	}
	public override void OnSpawn(IEntitySource source)
	{
		//ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(NPC.target.ToString()), Color.White);
		while (leftWing == -1 || Main.npc[leftWing].type != ModContent.NPCType<DesertBeakWingNPC>() || !Main.npc[leftWing].active)
		{
			leftWing = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<DesertBeakWingNPC>(), ai1: NPC.whoAmI, ai2: 1);
			NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, leftWing);
			//ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("BRO L"), Color.Wheat);
		}
		while (rightWing == -1 || Main.npc[rightWing].type != ModContent.NPCType<DesertBeakWingNPC>() || !Main.npc[rightWing].active)
		{
			//ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("BRO R"), Color.Wheat);
			rightWing = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<DesertBeakWingNPC>(), ai1: NPC.whoAmI, ai2: 2);
			NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, rightWing);
		}
		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, NPC.whoAmI);
	}
	int JustSpawned = 10;
	public override void AI()
	{

		//if (!Main.npc[leftWing].active)
		//{
		//	Main.NewText("Left: " + leftWing + Main.npc[leftWing].active, Color.LightBlue);
		//}
		//if (!Main.npc[rightWing].active)
		//{
		//	Main.NewText("Right: " + rightWing + Main.npc[rightWing].active, Color.LightBlue);
		//}

		// this probably isn't foolproof, as you can tell by how hacky it is, this is just the only way to stop it from not syncing the npc in onspawn for no reason (idk how to properly sync it without fucking up the values on the server)
		// please if you can find a better way to do this, do it, I already tried specifically syncing it if the wing npc wasn't active on the multiplayer client, but I probably did it wrong cause it didn't work
		// turns out just setting NPC.netAlways in the wing seems to fix it (skeletron prime also does this for the hands)
		//if (JustSpawned > 0)
		//{
		//	//NPC.dontTakeDamage = true;
		//	//Main.npc[leftWing].dontTakeDamage = true;
		//	//Main.npc[rightWing].dontTakeDamage = true;
		//	if (Main.netMode == NetmodeID.Server)
		//	{
		//		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, NPC.whoAmI);
		//		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, leftWing);
		//		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, rightWing);
		//	}
		//	JustSpawned--;
		//}
		////if (JustSpawned == 5)
		////{
		////	Main.NewText(NPC.dontTakeDamage, Color.PaleVioletRed);
		////}
		//if (JustSpawned == 0)
		//{
		//	NPC.dontTakeDamage = false;
		//	Main.npc[leftWing].dontTakeDamage = false;
		//	Main.npc[rightWing].dontTakeDamage = false;
		//	if (Main.netMode == NetmodeID.Server)
		//	{
		//		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, NPC.whoAmI);
		//		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, leftWing);
		//		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, rightWing);
		//	}
		//	JustSpawned--;
		//	//Main.NewText(NPC.dontTakeDamage, Color.PaleVioletRed);
		//}
		//ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(NPC.target.ToString()), Color.Lime);
		//Main.NewText("[" + $"{NPC.ai[0]}" + "]" + "[" + $"{NPC.ai[1]}" + "]" + "[" + $"{NPC.ai[2]}" + "]" + " phase: " + $"{phase}", Main.DiscoColor);
		float enragedModifier = 1f;
		if (Main.player[NPC.target].ZoneDesert || Main.player[NPC.target].ZoneUndergroundDesert)
		{
			enraged = false;
		}
		else
		{
			enraged = true;
		}

		if (enraged)
		{
			enragedModifier = 1.7f;
		}

		if (pulseTimer < MathHelper.Pi && Pulsing)
		{
			pulseTimer += 0.08f;
		}
		else
		{
			pulseTimer = 0;
			Pulsing = false;
		}

		afterImageTimer--;
		DrawOffsetY = 50;

		NPC.spriteDirection = NPC.direction = NPC.velocity.X > 0 ? 1 : -1;
		NPC.rotation = NPC.velocity.X * 0.05f;

		if (Storming)
		{
			Terraria.GameContent.Events.Sandstorm.TimeLeft = 60;
		}

		if (!NPC.HasValidTarget)
		{
			NPC.TargetClosest(false);
			NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, NPC.whoAmI);
		}
		if (!NPC.HasValidTarget || !Main.dayTime)
		{
			phase = 255;
			NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, NPC.whoAmI);
		}
		if (phase == 255)
		{
			NPC.timeLeft = 0;
			NPC.velocity.Y -= 0.2f;
			NPC.alpha += 2;
			Main.NewText("phase");
			Main.npc[leftWing].timeLeft = 0;
			Main.npc[leftWing].life = 0;
			Main.npc[leftWing].checkDead();
			Main.npc[leftWing].active = false;
			Main.npc[rightWing].timeLeft = 0;
			Main.npc[rightWing].life = 0;
			Main.npc[rightWing].checkDead();
			Main.npc[rightWing].active = false;
			NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, leftWing);
			NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, rightWing);
		}
		else if (NPC.life > (int)(NPC.lifeMax * 0.6f))
		{
			PhaseOne(Main.player[NPC.target], enragedModifier);
		}
		else
		{
			PhaseTwo(Main.player[NPC.target], enragedModifier);
		}
		//NPC.velocity = Vector2.Zero;
		//Main.NewText(NPC.ai[1], Main.DiscoColor);
		if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height) && phase != phase2Tornados)
		{
			NPC.velocity.Y -= NPC.velocity.Length() * 0.2f;
		}
	}
	public void PhaseOne(Player Target, float modifier)
	{
		if (phase == phase1Feather)
		{
			const int DashDelay = 70;
			NPC.ai[0]++;
			if (NPC.ai[0] == DashDelay * 3 - 30) Pulsing = true;
			if (NPC.ai[0] < DashDelay * 3)
			{
				NPC.velocity += NPC.Center.DirectionTo(Target.Center + new Vector2(0, -50)) * 0.1f * modifier;
				NPC.velocity = NPC.velocity.LengthClamp(5);
			}
			else
			{

				if (NPC.ai[0] % DashDelay == 0)
				{
					if (NPC.ai[0] > DashDelay * 3.1f + 150)
					{
						SoundEngine.PlaySound(SoundID.Item7, NPC.position);
						int Feathers = Main.rand.Next(2, 3) * 2;
						for (int i = -Feathers / 2; i < Feathers / 2; i++)
						{
							if (Main.netMode != NetmodeID.MultiplayerClient)
								Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, NPC.Center.DirectionTo(Target.Center).RotatedBy((MathHelper.Pi / 5 / Feathers) * Main.rand.NextFloat(0.9f, 1.1f) * i) * Main.rand.NextFloat(8.8f, 9.1f), ModContent.ProjectileType<DesertBeakFeather>(), (int)(15 * modifier), 1, -1, 0, 0, Main.rand.Next(10));
						}
						NPC.ai[0] = -85;
						NPC.ai[1]++;
						if (NPC.ai[1] > 2)
						{
							NPC.ai[1] = 0;
							phase = phase1Egg;
							NPC.TargetClosest();
						}
					}
					afterImageTimer = 30;
					NPC.velocity = NPC.Center.DirectionTo(Target.Center + Main.rand.NextVector2Circular(30, 30)) * 12 * (NPC.ai[0] * 0.001f + 1);
					NPC.netUpdate = true;
				}
				NPC.velocity *= 0.98f;
			}
		}
		else if (phase == phase1Egg)
		{
			NPC.ai[0]++;
			NPC.ai[1]++;
			NPC.velocity += NPC.Center.DirectionTo(Target.Center + new Vector2(0, -100) + new Vector2(0, 300 + (float)Math.Sin(NPC.ai[0] * 0.02f) * 100).RotatedBy(NPC.ai[0] * 0.1f) * 0.5f) * 0.2f * modifier;

			NPC.velocity = NPC.velocity.LengthClamp(8);

			if (NPC.ai[1] > 200)
			{
				NPC.velocity *= 0.9f;
			}
			if (NPC.ai[1] == 260)
			{
				int howManyBirds = 0;
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (Main.npc[i].netID == ModContent.NPCType<DesertTalon>() && Main.npc[i].active)
						howManyBirds++;
				}
				int eggType = (howManyBirds <= 4) ? ModContent.ProjectileType<VultureEgg>() : ModContent.ProjectileType<ShrapnelEgg>();
				//Main.NewText(howManyBirds, Main.DiscoColor);

				NPC.ai[1] = Main.rand.Next(-60, 60);
				int dmg = 44;
				if (Main.masterMode) dmg = 38;
				if (Main.netMode != NetmodeID.MultiplayerClient)
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.NextFloat(-4, 4), 3), eggType, (int)(dmg * modifier), 1);
				NPC.netUpdate = true;
			}
			if (NPC.ai[0] > 1000)
			{
				NPC.ai[1] = 0;
				NPC.ai[0] = 0;
				phase = phase1Feather;
				NPC.TargetClosest();
				NPC.netUpdate = true;
			}
		}
	}
	public void PhaseTwo(Player Target, float modifier)
	{
		if (phase <= phase1Egg)
		{
			phase = phase2Transition;
			NPC.ai[0] = 0;
			NPC.ai[1] = 0;
			NPC.netUpdate = true;
		}
		else if (phase == phase2Transition)
		{
			NPC.ai[0]++;
			NPC.velocity *= 0.98f;

			if (NPC.ai[0] == 50)
			{
				Pulsing = true;
				SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.position);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Main.windSpeedCurrent = (Main.windSpeedCurrent >= 0) ? Main.windSpeedCurrent * 1.5f : 0.5f;
					Main.windSpeedCurrent = MathHelper.Clamp(Main.windSpeedCurrent, -2, 2);
					Main.windSpeedTarget = Main.windSpeedCurrent * 2;
					Terraria.GameContent.Events.Sandstorm.StartSandstorm();
					Storming = true;
				}
			}
			FlapMultiplier = 2;
			if (NPC.ai[0] > 100)
			{
				pulseTimer = 0;
				phase = phase2Circle;
				NPC.ai[0] = 0;
				NPC.ai[1] = -1;
				NPC.TargetClosest();
				FlapMultiplier = 1;
				NPC.defense = (int)(NPC.defense * 0.666f);
				NPC.netUpdate = true;
			}
		}
		else if (phase == phase2Circle)
		{
			NPC.ai[0]++;
			NPC.ai[2]++;
			float CircleDistance = (int)(Math.Floor(NPC.ai[0] * 0.001f)) % 2 == 0 ? 300 : 100;
			NPC.velocity += NPC.Center.DirectionTo(Target.Center + Vector2.One.RotatedBy(NPC.ai[0] * 0.02) * CircleDistance * NPC.ai[1]) * 0.3f * modifier;
			NPC.velocity = NPC.velocity.LengthClamp(10);

			if (CircleDistance > 100)
			{
				if (NPC.ai[2] == 100)
				{
					Pulsing = true;
				}
				else if (NPC.ai[2] > 130)
				{
					int Feathers = Main.rand.Next(7, 10);
					for (int i = 0; i < Feathers; i++)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
							Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 8).RotatedBy((MathHelper.TwoPi / Feathers) * Main.rand.NextFloat(0.9f, 1.1f) * i) * Main.rand.NextFloat(0.8f, 1.1f), ModContent.ProjectileType<DesertBeakFeather>(), (int)(15 * modifier), 1, -1, 0, 0, Main.rand.Next(10));
					}
					NPC.ai[2] = Main.rand.Next(-100, 0);
					NPC.netUpdate = true;
					NPC.ai[1] *= Main.rand.NextBool() ? 1 : -1;
				}
			}
			else
			{
				NPC.ai[2] = Main.rand.Next(-100, 0);
				NPC.ai[3]++;
			}
			if (NPC.ai[3] >= 4)
			{
				phase = phase2Tornados;
				NPC.ai[0] = 0;
				NPC.ai[3] = 1;
				NPC.ai[1] = Target.position.X + (Main.rand.NextBool() ? 1 : -1) * 50;
				NPC.ai[2] = Target.position.Y;
				NPC.TargetClosest();
				NPC.netUpdate = true;
			}
		}
		else if (phase == phase2Tornados)
		{
			NPC.ai[0]++;
			NPC.velocity += NPC.Center.DirectionTo(new Vector2(NPC.ai[1] + (float)Math.Sin(NPC.ai[0] * 0.1f) * 50, NPC.ai[2] + (float)Math.Sin(NPC.ai[0] * 0.05f) * 200)) * 1;
			NPC.velocity = NPC.velocity.LengthClamp(10);
			//Main.NewText($"{NPC.velocity.Y}" + " | " + $"{NPC.old.Y}", Main.DiscoColor);
			if ((NPC.velocity.Y >= 0 && NPC.position.DirectionFrom(NPC.oldPosition).Y < 0 || NPC.velocity.Y <= 0 && NPC.position.DirectionFrom(NPC.oldPosition).Y > 0) && Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (NPC.ai[0] > 60)
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(6 * (Target.Center.X > NPC.Center.X ? 1 : -1), NPC.Center.DirectionTo(Target.Center).Y * 2), ModContent.ProjectileType<DesertBeakSandstorm>(), 20, 1);
			}

			if (NPC.ai[0] >= 60 * 10)
			{
				phase = phase2Egg;
				NPC.ai[0] = 0;
				NPC.ai[1] = 0;
				NPC.ai[2] = 0;
				NPC.ai[3] = 0;
				NPC.TargetClosest();
				NPC.netUpdate = true;
			}
		}
		#region Grab
		//else if (phase == phase2Dash)
		//{
		//    NPC.ai[0]++;

		//    if (Target.getRect().Intersects(NPC.getRect()))
		//    {
		//        Target.velocity.X = NPC.velocity.X;
		//        phase = phase2Grab;
		//        NPC.ai[1] = 0;
		//        return;
		//    }
		//}
		//else if (phase == phase2Grab)
		//{
		//    int grabOffset = (int)NPC.Center.Y + 50;
		//    NPC.velocity.X *= 0.97f;
		//    NPC.ai[1]++;
		//    delay++;
		//    if (NPC.ai[1] < 70)
		//    {
		//        Target.velocity.X = NPC.velocity.X;
		//        NPC.height *= (int)1.5;
		//        if (delay == 30)
		//        {
		//            NPC.damage = 8;
		//            delay = 0;
		//        }
		//        NPC.velocity.Y = -12;
		//        Target.Center = new Vector2(NPC.Center.X, grabOffset);

		//    }
		//    else if (NPC.ai[1] >= 70 && NPC.ai[1] < 105)
		//    {
		//        Target.velocity.Y += 0.1f;
		//        NPC.velocity.Y += 0.1f;
		//        delay = 0;
		//    }
		//    if (NPC.ai[1] >= 150)
		//    {


		//        if (NPC.ai[1] < 250)
		//        {
		//            afterImageTimer = 30;
		//            NPC.velocity = NPC.Center.DirectionTo(Target.Center + Main.rand.NextVector2Circular(30, 30)) * 12 * (NPC.ai[1] * 0.001f + 1);
		//        }
		//        else if (NPC.ai[1] == 250)
		//        {
		//            NPC.ai[1] = -1;
		//            phase = phase2Egg;
		//            delay = 0;
		//        }
		//    }
		//}
		#endregion Grab
		else if (phase == phase2Egg)
		{
			NPC.ai[0]++;
			NPC.ai[1]++;
			NPC.velocity += NPC.Center.DirectionTo(Target.Center + new Vector2(0, -100) + new Vector2(0, 300 + (float)Math.Sin(NPC.ai[0] * 0.02f) * 100).RotatedBy(NPC.ai[0] * 0.1f) * 0.5f) * 0.2f * modifier;
			NPC.velocity = NPC.velocity.LengthClamp(8);

			if (NPC.ai[1] > 160 - 60)
			{
				NPC.velocity *= 0.9f;
			}
			if (NPC.ai[1] == 160)
			{
				int howManyBirds = 0;
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (Main.npc[i].netID == ModContent.NPCType<DesertTalon>() && Main.npc[i].active)
						howManyBirds++;
				}
				int eggType = (howManyBirds <= 5) ? ModContent.ProjectileType<VultureEgg>() : ModContent.ProjectileType<ShrapnelEgg>();
				//Main.NewText(howManyBirds, Main.DiscoColor);

				NPC.ai[1] = Main.rand.Next(-60, 60);
				int dmg = 44;
				if (Main.masterMode) dmg = 38;
				if (Main.netMode != NetmodeID.MultiplayerClient)
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.NextFloat(-4, 4), 3), eggType, (int)(dmg * modifier), 1);
				NPC.netUpdate = true;
			}
			if (NPC.ai[0] > 600)
			{
				NPC.ai[1] = -1;
				NPC.ai[0] = 0;
				phase = phase2Circle;
				NPC.TargetClosest();
				NPC.netUpdate = true;
			}
		}
	}
	public override void FindFrame(int frameHeight)
	{
		int frameAdd = 1;

		if (Main.netMode != NetmodeID.Server)
		{
			if (afterImageTimer <= 0)
				NPC.frameCounter += frameAdd + MathHelper.Clamp(-NPC.velocity.Y * 0.3f + Math.Abs(NPC.velocity.X * 0.1f), 0.5f, 1.7f) * FlapMultiplier;
			else
				NPC.frameCounter += frameAdd + MathHelper.Lerp(MathHelper.Clamp(-NPC.velocity.Y * 0.3f + Math.Abs(NPC.velocity.X * 0.1f), -0.3f, 2), MathHelper.Clamp(NPC.velocity.Length() * 0.3f, 0, 2), MathHelper.Clamp(afterImageTimer * 0.1f, 0, 1)) * FlapMultiplier;

			if (NPC.frameCounter > 5.0)
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = NPC.frame.Y + frameHeight;
			}
			if (NPC.frame.Y > frameHeight * 7)
			{
				NPC.frame.Y = 0;
			}
		}
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(phase);
		writer.Write(afterImageTimer);
		writer.Write(Storming);
		writer.Write(enraged);
		writer.Write(leftWing);
		writer.Write(rightWing);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		phase = reader.ReadByte();
		afterImageTimer = reader.ReadInt32();
		Storming = reader.ReadBoolean();
		enraged = reader.ReadBoolean();
		leftWing = reader.ReadInt32();
		rightWing = reader.ReadInt32();
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(-150, -40), NPC.velocity, Mod.Find<ModGore>("DesertBeakWing").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(40, -40), NPC.velocity, Mod.Find<ModGore>("DesertBeakWing2").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(-10, 0), NPC.velocity, Mod.Find<ModGore>("DesertBeakBody").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(-24, 10), NPC.velocity, Mod.Find<ModGore>("DesertBeakHead").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(-10, 60), NPC.velocity, Mod.Find<ModGore>("DesertBeakTalon").Type, 0.9f);
			//Main.NewText($"{NPC.position} | {NPC.velocity} Main");
		}
		Main.npc[leftWing].life -= hit.Damage;
		Main.npc[rightWing].life -= hit.Damage;
		//ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"L life: {Main.npc[leftWing].life} L rLife: {Main.npc[leftWing].realLife}"), Color.LightBlue);
		//ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"R life: {Main.npc[rightWing].life} R rLife: {Main.npc[rightWing].realLife}"), Color.LightBlue);
		//Main.NewText($"L life: {Main.npc[leftWing].life} L rLife: {Main.npc[leftWing].realLife}");
		//Main.NewText($"R life: {Main.npc[rightWing].life} R rLife: {Main.npc[rightWing].realLife}");
		if (Main.npc[leftWing].life <= 0)
		{
			Main.npc[leftWing].checkDead();
		}
		if (Main.npc[rightWing].life <= 0)
		{
			Main.npc[rightWing].checkDead();
		}
		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, leftWing);
		NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, rightWing);
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		int frameHeight = TextureAssets.Npc[Type].Value.Height / Main.npcFrameCount[NPC.type];
		Rectangle frame = NPC.frame;
		Vector2 drawPos = NPC.position + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition;

		SpriteEffects effect = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

		if (afterImageTimer > 0)
		{
			float alphaThingHackyWow = 0;
			for (int i = 8; i > 0; i--)
			{
				alphaThingHackyWow += 0.07f;
				Main.EntitySpriteDraw(TextureAssets.Npc[Type].Value, NPC.oldPos[i] + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition, frame, drawColor * alphaThingHackyWow * MathHelper.Clamp(afterImageTimer * 0.05f, 0, 1), NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, effect, 0);
			}
		}
		if (phase > phase1Egg)
		{
			float alphaThingHackyWow = 0;
			for (int i = 8; i > 0; i -= 2)
			{
				alphaThingHackyWow += 0.07f;
				Main.EntitySpriteDraw(TextureAssets.Npc[Type].Value, NPC.oldPos[i] + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition, frame, drawColor * alphaThingHackyWow, NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, effect, 0);
			}
		}
		if (pulseTimer is > 0 and < MathHelper.Pi)
		{
			for (int i = 4; i > 0; i--)
			{
				Main.EntitySpriteDraw(TextureAssets.Npc[Type].Value, drawPos + new Vector2(0, (float)Math.Sin(pulseTimer) * 8).RotatedBy(MathHelper.PiOver2 * i), frame, new Color(drawColor.R, drawColor.G, drawColor.B, 128) * (float)Math.Sin(pulseTimer) * 0.7f, NPC.oldRot[i], new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, effect, 0);
			}
		}
		return true;
	}
}

/// <summary>
/// Credit to Photonic0 on discord for this
/// </summary>
public class DesertBeakIFrames : GlobalNPC
{
	public override bool PreAI(NPC npc)
	{
		UpdateDesertBeakIFrames(npc);
		return base.PreAI(npc);
	}
	static bool IsNPCTypeDesertBeak(NPC npc) => npc.type == ModContent.NPCType<DesertBeak>() || npc.type == ModContent.NPCType<DesertBeakWingNPC>();
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
		//if (IsNPCTypeDesertBeak(npc))
		//{
		//    int iframes = GetAmountOfIframes(projectile);
		//    DesertBeakIFrame[projectile.whoAmI] = iframes;
		//    if (projectile.usesOwnerMeleeHitCD)
		//    {
		//        Player player = Main.player[projectile.owner];
		//        DesertBeakIFrame[Main.maxProjectiles + projectile.owner] = player.itemAnimation;
		//        player.SetMeleeHitCooldown(npc.whoAmI, player.itemAnimation);
		//    }
		//}

		if (IsNPCTypeDesertBeak(npc))
		{
			desertBeakIFrames[projectile.whoAmI] = GetAmountOfIframes(projectile);
		}
	}
	static int[] desertBeakIFrames = new int[Main.maxProjectiles + Main.maxPlayers];//ok so basically the first 1000 slots are for projs and the latter 255 slots are for players
	public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
	{
		if (IsNPCTypeDesertBeak(npc))
		{
			desertBeakIFrames[player.whoAmI + Main.maxProjectiles] = player.itemAnimation;
		}
	}
	public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
	{
		if (!IsNPCTypeDesertBeak(npc) || (desertBeakIFrames[player.whoAmI + Main.maxProjectiles] < 1))
			return null;
		return false;
	}
	/// <summary>
	/// THIS ASSUMES THAT YOU CHECK IF THE NPC IS A DESTROYER BEFOREHAND
	/// </summary>
	static bool IsDesertBeakImmuneToThis(Projectile projectile, NPC npc)
	{
		if (!projectile.friendly || projectile.DistanceSQ(npc.Center) > 40000)//checking distance for optimization
			return true;
		if (projectile.usesIDStaticNPCImmunity)
			if (desertBeakIFrames[projectile.whoAmI] < 1 && projectile.friendly)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					if (!npc.active || !IsNPCTypeDesertBeak(Main.npc[i]))
						continue;
					if (Projectile.perIDStaticNPCImmunity[projectile.type][i] > 0)
						return true;
				}
			}
		if (projectile.usesLocalNPCImmunity || projectile.usesOwnerMeleeHitCD || projectile.stopsDealingDamageAfterPenetrateHits)
			return desertBeakIFrames[projectile.whoAmI] > 1;
		for (int i = 0; i < Main.maxProjectiles; i++)//attempt at mimmicking global iframes
		{
			if (desertBeakIFrames[i] > 0)
				return true;
		}
		return false;
	}
	public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
	{
		if (!IsNPCTypeDesertBeak(npc) || !IsDesertBeakImmuneToThis(projectile, npc))
			return null;
		return false;
	}
	public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => IsNPCTypeDesertBeak(entity);
	/// <summary>
	/// CALL THIS ON PRE AI OF GLOBAL NPC
	/// </summary>
	static void UpdateDesertBeakIFrames(NPC npc)
	{
		if (npc.type == ModContent.NPCType<DesertBeak>())
		{
			for (int i = 0; i < desertBeakIFrames.Length; i++)
			{
				if (i < 1000 && !Main.projectile[i].active)
					desertBeakIFrames[i] = 0;
				desertBeakIFrames[i]--;
			}
		}
	}
}
