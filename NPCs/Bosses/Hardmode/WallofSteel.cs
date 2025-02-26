using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Weapons.Magic.Superhardmode;
using Avalon.Items.Weapons.Ranged.Superhardmode;
using Avalon.Projectiles.Hostile.WallOfSteel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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

namespace Avalon.NPCs.Bosses.Hardmode;

[AutoloadBossHead]
public class WallofSteel : ModNPC
{
	private static Asset<Texture2D> wosTexture;
	private static Asset<Texture2D> mechaHungryChainTexture;
	private int TopEyeHP;
	private int BottomEyeHP;
	private int TopEyeMaxHP;
	private int BottomEyeMaxHP;
	private int LaserSpreadTimer;
	private int MortarTimer;
	private float MortarAngleThingy;
	public override string Texture => "Avalon/NPCs/Bosses/Hardmode/WallofSteelWall";
	public override void Load()
	{
		wosTexture = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Assets/Textures/WallofSteel");
		mechaHungryChainTexture = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Assets/Textures/MechaHungryChain");
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 1;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.CursedInferno] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Ichor] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Venom] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
		var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{ // Influences how the NPC looks in the Bestiary
			CustomTexturePath = "Avalon/Assets/Bestiary/WallofSteel",
			Position = new Vector2(7f, 0f)
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
	}
	public override void SetDefaults()
	{
		NPC.width = 200;
		NPC.height = 580;
		NPC.boss = NPC.noTileCollide = NPC.noGravity = NPC.behindTiles = true;
		NPC.npcSlots = 100f;
		NPC.damage = 150;
		NPC.lifeMax = 92000;
		NPC.timeLeft = 750000;
		NPC.defense = 55;
		NPC.alpha = 255;
		NPC.aiStyle = -1;
		NPC.value = Item.buyPrice(0, 10);
		NPC.knockBackResist = 0;
		NPC.scale = 1f;
		NPC.HitSound = SoundID.NPCHit4;
		NPC.DeathSound = SoundID.NPCDeath14;
		Music = ExxoAvalonOrigins.MusicMod == null ? MusicID.Boss2 : MusicID.Boss2; // MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/WallofSteel");
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(
		[
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.WallOfSteel"))
		]);
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = ItemID.SuperHealingPotion;
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(TopEyeHP);
		writer.Write(TopEyeMaxHP);
		writer.Write(BottomEyeHP);
		writer.Write(BottomEyeMaxHP);
		writer.Write(LaserSpreadTimer);
		writer.Write(MortarTimer);
		writer.Write(MortarAngleThingy);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		TopEyeHP = reader.ReadByte();
		TopEyeMaxHP = reader.ReadByte();
		BottomEyeHP = reader.ReadByte();
		BottomEyeMaxHP = reader.ReadByte();
		LaserSpreadTimer = reader.ReadInt32();
		MortarTimer = reader.ReadInt32();
		MortarAngleThingy = reader.ReadSingle();
	}
	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//SpriteEffects effects = SpriteEffects.None;
		//if (NPC.spriteDirection == 1)
		//{
		//	effects = SpriteEffects.FlipHorizontally;
		//}

		//Vector2 start = NPC.position + new Vector2(100, 46);
		//Vector2 end = NPC.position + new Vector2(0, NPC.height);
		//start -= screenPos;
		////Texture2D TEX; // Mod.Assets.Request<Texture2D>("NPCs/Hardmode/VenusFlytrapVine").Value;
		////int linklength = TEX.Height;
		//Vector2 chain = new Vector2(0, 94);

		//int numlinks = 7;
		//Vector2[] links = new Vector2[numlinks];
		//for (int i = 0; i < numlinks; i++)
		//{
		//	links[i] = start + chain * i - (i == 6 ? new Vector2(0, 26) : Vector2.Zero);

		//	Texture2D TEX = WoSTextures[i].Value;

		//	//Main.spriteBatch.Draw(TEX, links[i], Lighting.GetColor((links[i] + screenPos).ToTileCoordinates()));
		//	Main.spriteBatch.Draw(TEX, links[i], new Rectangle(0, 0, TEX.Width, TEX.Height), Lighting.GetColor((links[i] + screenPos).ToTileCoordinates()), 0f, new Vector2(TEX.Width / 2, TEX.Height), 1f,
		//		effects, 1f);
		//}


		//float num66 = Main.NPCAddHeight(NPC);
		//var vector13 = new Vector2(TextureAssets.Npc[NPC.type].Width() / 2,
		//	TextureAssets.Npc[NPC.type].Height() / Main.npcFrameCount[NPC.type] / 2);
		//float glow = (float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.5f + 0.5f;
		//Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture).Value,
		//	new Vector2(NPC.position.X - screenPos.X + (NPC.width / 2) - (TextureAssets.Npc[NPC.type].Width() * NPC.scale / 2f) + (vector13.X * NPC.scale), NPC.position.Y - Main.screenPosition.Y + NPC.height - (TextureAssets.Npc[NPC.type].Height() * NPC.scale / Main.npcFrameCount[NPC.type]) + 4f + (vector13.Y * NPC.scale) + num66) + new Vector2(0, 2 * glow)
		//	, NPC.frame, default, NPC.rotation, vector13,
		//	NPC.scale, effects, 0f);
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.85f * bossAdjustment);
		NPC.damage = (int)(NPC.damage * 0.65f);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 v, Color drawColor)
	{
		if (AvalonWorld.WallOfSteel >= 0)
		{
			foreach (var player in Main.ActivePlayers)
			{
				if (player.gross && player.active && player.tongued && !player.dead)
				{
					float num = Main.npc[AvalonWorld.WallOfSteel].position.X + Main.npc[AvalonWorld.WallOfSteel].width / 2;
					float num2 = Main.npc[AvalonWorld.WallOfSteel].position.Y + Main.npc[AvalonWorld.WallOfSteel].height / 2;
					var vector = new Vector2(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
					float num3 = num - vector.X;
					float num4 = num2 - vector.Y;
					float rotation = (float)Math.Atan2(num4, num3) - 1.57f;
					bool flag = true;
					while (flag)
					{
						float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
						if (num5 < 40f)
						{
							flag = false;
						}
						else
						{
							num5 = TextureAssets.Chain38.Value.Height / num5;
							num3 *= num5;
							num4 *= num5;
							vector.X += num3;
							vector.Y += num4;
							num3 = num - vector.X;
							num4 = num2 - vector.Y;
							Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
							spriteBatch.Draw(TextureAssets.Chain8.Value, new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, TextureAssets.Chain8.Value.Width, TextureAssets.Chain8.Value.Height)), color, rotation, new Vector2(TextureAssets.Chain8.Value.Width * 0.5f, TextureAssets.Chain8.Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
						}
					}
				}
			}
			foreach (var npcMechaHungry in Main.ActiveNPCs)
			{
				if (npcMechaHungry.active && (npcMechaHungry.type == ModContent.NPCType<MechanicalHungry>() || npcMechaHungry.type == ModContent.NPCType<MechanicalHungry2>()))
				{
					float num6 = Main.npc[AvalonWorld.WallOfSteel].position.X + Main.npc[AvalonWorld.WallOfSteel].width / 2;
					float num7 = Main.npc[AvalonWorld.WallOfSteel].position.Y;
					float num8 = AvalonWorld.WallOfSteelB - AvalonWorld.WallOfSteelT;
					bool flag2 = false;
					if (npcMechaHungry.frameCounter > 7.0)
					{
						flag2 = true;
					}
					num7 = AvalonWorld.WallOfSteelT + num8 * npcMechaHungry.ai[0];
					var vector2 = new Vector2(npcMechaHungry.position.X + npcMechaHungry.width / 2, npcMechaHungry.position.Y + npcMechaHungry.height / 2);
					float num9 = num6 - vector2.X;
					float num10 = num7 - vector2.Y;
					float rotation2 = (float)Math.Atan2(num10, num9) - 1.57f;
					bool flag3 = true;
					while (flag3)
					{
						SpriteEffects effects = SpriteEffects.None;
						if (flag2)
						{
							effects = SpriteEffects.FlipHorizontally;
							flag2 = false;
						}
						else
						{
							flag2 = true;
						}
						int height = 28;
						float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
						if (num11 < 40f)
						{
							height = (int)num11 - 40 + 28;
							flag3 = false;
						}
						num11 = 28f / num11;
						num9 *= num11;
						num10 *= num11;
						vector2.X += num9;
						vector2.Y += num10;
						num9 = num6 - vector2.X;
						num10 = num7 - vector2.Y;
						Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
						Main.EntitySpriteDraw(mechaHungryChainTexture.Value, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, mechaHungryChainTexture.Value.Width, height)), color2, rotation2, new Vector2(mechaHungryChainTexture.Value.Width * 0.5f, mechaHungryChainTexture.Value.Height * 0.5f), 1f, effects, 0);
					}
				}
			}
			int heightOfPartToTile = 120;
			float num13 = AvalonWorld.WallOfSteelT;
			float screenYPos = AvalonWorld.WallOfSteelB;
			screenYPos = Main.screenPosition.Y + Main.screenHeight;
			float num15 = (int)((num13 - Main.screenPosition.Y) / heightOfPartToTile) + 1;
			num15 *= heightOfPartToTile;
			if (num15 > 0f)
			{
				num13 -= num15;
			}
			float num16 = num13;
			float wosPosX = Main.npc[AvalonWorld.WallOfSteel].position.X + 20;
			float num18 = screenYPos - num13;
			bool flag4 = true;
			SpriteEffects effects2 = SpriteEffects.None;
			if (Main.npc[AvalonWorld.WallOfSteel].spriteDirection == -1)
			{
				wosPosX += 50; // going to the left
			}
			if (Main.npc[AvalonWorld.WallOfSteel].spriteDirection == 1)
			{
				effects2 = SpriteEffects.FlipHorizontally;
			}
			if (Main.npc[AvalonWorld.WallOfSteel].direction > 0)
			{
				wosPosX -= 80f; // going to the right
			}
			int num19 = 0;
			if (!Main.gamePaused)
			{
				AvalonWorld.WallOfSteelF++;
			}
			if (AvalonWorld.WallOfSteelF > 12)
			{
				num19 = 240;
				if (AvalonWorld.WallOfSteelF > 17)
				{
					AvalonWorld.WallOfSteelF = 0;
				}
			}
			else if (AvalonWorld.WallOfSteelF > 6)
			{
				num19 = 120;
			}
			while (flag4)
			{
				num18 = screenYPos - num16;
				if (num18 > heightOfPartToTile)
				{
					num18 = heightOfPartToTile;
				}
				bool flag5 = true;
				int num20 = 0;
				while (flag5)
				{
					int x = (int)(wosPosX + wosTexture.Value.Width / 2) / 16;
					int y = (int)(num16 + num20) / 16;
					Main.spriteBatch.Draw(wosTexture.Value, new Vector2(wosPosX - Main.screenPosition.X, num16 + num20 - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, num19 + num20, wosTexture.Value.Width, 16)), Lighting.GetColor(x, y), 0f, default(Vector2), 1f, effects2, 0f);
					num20 += 16;
					if (num20 >= num18)
					{
						flag5 = false;
					}
				}
				num16 += heightOfPartToTile;
				if (num16 >= screenYPos)
				{
					flag4 = false;
				}
			}
		}

		SpriteEffects effects3 = SpriteEffects.None;
		if (NPC.spriteDirection == 1)
		{
			effects3 = SpriteEffects.FlipHorizontally;
		}

		Vector2 start = NPC.position + new Vector2(100, -16);
		int numVerticalSegments = (int)MathF.Ceiling(TextureAssets.Npc[Type].Value.Height / 16f);
		int numHorizontalSegments = (int)MathF.Ceiling(TextureAssets.Npc[Type].Value.Width / 16f);
		for (int i = 0; i < numVerticalSegments; i++)
		{
			for (int j = 0; j < numHorizontalSegments; j++)
			{
				Main.spriteBatch.Draw(
					TextureAssets.Npc[Type].Value,
					(start - Main.screenPosition).Floor() + new Vector2(NPC.direction == 1 ? TextureAssets.Npc[Type].Value.Width - j * 16 - 16 : j * 16, TextureAssets.Npc[Type].Value.Height + i * 16),
					new Rectangle(j * 16, i * 16, 16, 16),
					Lighting.GetColor((NPC.position + new Vector2(NPC.direction == 1 ? TextureAssets.Npc[Type].Value.Width - j * 16 - 16 : j * 16, i * 16 - 8)).ToTileCoordinates()),
					0f,
					new Vector2(TextureAssets.Npc[Type].Value.Width / 2, TextureAssets.Npc[Type].Value.Height),
					1f,
					effects3,
					1f);
			}
		}


		/*
		for (int i = 0; i < numSegments; i++)
		{
			Texture2D TEX = ModContent.Request<Texture2D>(Texture).Value;
			Main.spriteBatch.Draw(TEX, (start - Main.screenPosition).Floor() + new Vector2(0, TEX.Height + i * 16), new Rectangle(0, i * 16, TEX.Width, 16), Lighting.GetColor((start + new Vector2(0, i * 16)).ToTileCoordinates()), 0f, new Vector2(TEX.Width / 2, TEX.Height), 1f,
				effects, 1f);
		}
			*/
		return true;
	}

	public override void AI()
	{
		if (NPC.AnyNPCs(ModContent.NPCType<WallofSteelMouthEye>()) ||
			NPC.AnyNPCs(ModContent.NPCType<WallofSteelLaserEye>()))
		{
			NPC.dontTakeDamage = true;
		}
		else NPC.dontTakeDamage = false;
		bool expert = Main.expertMode;
		if (NPC.position.X < 160f || NPC.position.X > (Main.maxTilesX - 10) * 16)
		{
			NPC.active = false;
		}
		if (NPC.localAI[0] == 0f)
		{
			NPC.localAI[0] = 1f;
			AvalonWorld.WallOfSteelB = -1;
			AvalonWorld.WallOfSteelT = -1;
		}
		NPC.localAI[3] += 1f;
		if (NPC.localAI[3] >= 600 + Main.rand.Next(1000))
		{
			NPC.localAI[3] = -Main.rand.Next(200);
			//Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10);
		}
		AvalonWorld.WallOfSteel = NPC.whoAmI;
		int tilePosX = (int)(NPC.position.X / 16f);
		int tilePosXwidth = (int)((NPC.position.X + NPC.width) / 16f);
		int tilePosYCenter = (int)((NPC.position.Y + NPC.height / 2) / 16f);
		int num447 = tilePosYCenter + 7;
		num447 += 4;
		if (AvalonWorld.WallOfSteelB == -1)
		{
			AvalonWorld.WallOfSteelB = num447 * 16;
		}
		else if (AvalonWorld.WallOfSteelB > num447 * 16)
		{
			AvalonWorld.WallOfSteelB--;
			if (AvalonWorld.WallOfSteelB < num447 * 16)
			{
				AvalonWorld.WallOfSteelB = num447 * 16;
			}
		}
		else if (AvalonWorld.WallOfSteelB < num447 * 16)
		{
			AvalonWorld.WallOfSteelB++;
			if (AvalonWorld.WallOfSteelB > num447 * 16)
			{
				AvalonWorld.WallOfSteelB = num447 * 16;
			}
		}
		num447 = tilePosYCenter - 7;
		num447 -= 4;
		if (AvalonWorld.WallOfSteelT == -1)
		{
			AvalonWorld.WallOfSteelT = num447 * 16;
		}
		else if (AvalonWorld.WallOfSteelT > num447 * 16)
		{
			AvalonWorld.WallOfSteelT--;
			if (AvalonWorld.WallOfSteelT < num447 * 16)
			{
				AvalonWorld.WallOfSteelT = num447 * 16;
			}
		}
		else if (AvalonWorld.WallOfSteelT < num447 * 16)
		{
			AvalonWorld.WallOfSteelT++;
			if (AvalonWorld.WallOfSteelT > num447 * 16)
			{
				AvalonWorld.WallOfSteelT = num447 * 16;
			}
		}
		float num450 = (AvalonWorld.WallOfSteelB + AvalonWorld.WallOfSteelT) / 2 - NPC.height / 2;
		if (NPC.Center.Y > Main.player[NPC.target].Center.Y)
		{
			if (NPC.velocity.Y > 0)
			{
				NPC.velocity.Y *= 0.98f;
			}
			NPC.velocity.Y -= 0.02f;
			if (NPC.velocity.Y < -2.2f)
			{
				NPC.velocity.Y = -2.2f;
			}
		}
		if (NPC.Center.Y < Main.player[NPC.target].Center.Y)
		{
			if (NPC.velocity.Y < 0)
			{
				NPC.velocity.Y *= 0.98f;
			}
			NPC.velocity.Y += 0.02f;
			if (NPC.velocity.Y > 2.2f)
			{
				NPC.velocity.Y = 2.2f;
			}
		}
		//npc.velocity.Y = 0f;
		//npc.position.Y = num450;
		if (NPC.position.Y / 16 < Main.maxTilesY - 200)
		{
			NPC.position.Y = (Main.maxTilesY - 200) * 16;
		}

		if (Main.expertMode)
		{
			NPC.ai[2]--; // mouth eye respawn timer; do not use anywhere else
			NPC.ai[3]--; // laser eye respawn timer; do not use anywhere else
			if (NPC.ai[2] < 0) NPC.ai[2] = 0;
			if (NPC.ai[3] < 0) NPC.ai[3] = 0;

			if (NPC.ai[2] == 1)
			{
				int q = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<WallofSteelMouthEye>());
				Main.npc[q].life = Main.npc[q].lifeMax / 3;
			}
			if (NPC.ai[3] == 1)
			{
				int q = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<WallofSteelLaserEye>());
				Main.npc[q].life = Main.npc[q].lifeMax / 3;
			}
		}
		


		if (ClassExtensions.FindATypeOfNPC(ModContent.NPCType<WallofSteelMouthEye>()) == -1)
		{
			TopEyeHP = 0;
		}
		if (ClassExtensions.FindATypeOfNPC(ModContent.NPCType<WallofSteelLaserEye>()) == -1)
		{
			BottomEyeHP = 0;
		}

		int totalHP = TopEyeHP + BottomEyeHP + NPC.life;

		int totalMaxHP = TopEyeMaxHP + BottomEyeMaxHP + NPC.lifeMax;

		float speed = 2.5f;
		if (totalHP < totalMaxHP * 0.75)
		{
			speed += 0.25f;
		}
		if (totalHP < totalMaxHP * 0.5)
		{
			speed += 0.4f;
		}
		if (totalHP < totalMaxHP * 0.25)
		{
			speed += 0.5f;
		}
		if (totalHP < totalMaxHP * 0.1)
		{
			speed += 0.6f;
		}
		if (totalHP < totalMaxHP * 0.66 && Main.expertMode)
		{
			speed += 0.3f;
		}
		if (totalHP < totalMaxHP * 0.33 && Main.expertMode)
		{
			speed += 0.3f;
		}
		if (totalHP < totalMaxHP * 0.05 && Main.expertMode)
		{
			speed += 0.6f;
		}
		if (totalHP < totalMaxHP * 0.035 && Main.expertMode)
		{
			speed += 0.6f;
		}
		if (totalHP < totalMaxHP * 0.025 && Main.expertMode)
		{
			speed += 0.6f;
		}
		if (NPC.velocity.X == 0f)
		{
			NPC.TargetClosest(true);
			NPC.velocity.X = NPC.direction;
		}
		if (NPC.velocity.X < 0f)
		{
			NPC.velocity.X = -speed;
			NPC.direction = -1;
		}
		else
		{
			NPC.velocity.X = speed;
			NPC.direction = 1;
		}

		if (!NPC.AnyNPCs(ModContent.NPCType<WallofSteelMouthEye>()))
		{
			Vector2 offset = new(65, 115);
			if (NPC.direction == 1) offset = new(NPC.width - (65 + 16), 115);
			Dust d1 = Dust.NewDustDirect(NPC.position + offset, 1, 1, DustID.Smoke, 0f, 0f);
		}
		if (!NPC.AnyNPCs(ModContent.NPCType<WallofSteelLaserEye>()))
		{
			Vector2 offset = new(65, NPC.height - 115 - 16);
			if (NPC.direction == 1) offset = new(NPC.width - (65 + 16), NPC.height - 115 - 16);
			Dust d1 = Dust.NewDustDirect(NPC.position + offset, 1, 1, DustID.Smoke, 0f, 0f);
		}

		MortarTimer++;
		if (MortarTimer > 200)
		{
			int fire;
			float f = 0f;
			var laserPos = new Vector2(NPC.Center.X + NPC.DirectionTo(NPC.PlayerTarget().Center).X * 40f, NPC.Center.Y + NPC.DirectionTo(NPC.PlayerTarget().Center).Y * 40f);
			float rotation = (float)Math.Atan2(laserPos.Y - NPC.PlayerTarget().Center.Y, laserPos.X - NPC.PlayerTarget().Center.X);
			SoundEngine.PlaySound(SoundID.Item11, NPC.Center);
			if (MortarAngleThingy <= 0.3f)
			{
				Vector2 vel = NPC.Center.DirectionTo(NPC.Center + new Vector2((float)(Math.Cos(rotation + MortarAngleThingy) * 12f * -1), (float)(Math.Sin(rotation + MortarAngleThingy) * 20f * -1)));
				fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), laserPos.X + vel.X * 40f - 8, laserPos.Y + vel.Y * 40f - 8, vel.X * 20f, vel.Y * 20f, ModContent.ProjectileType<WoSMortar>(), Main.expertMode ? 70 : 55, 6f, Main.myPlayer);
				Main.projectile[fire].timeLeft = 600;
				if (Main.netMode != NetmodeID.SinglePlayer)
				{
					NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
				}

				NPC.rotation = NPC.Center.DirectionTo(Main.projectile[fire].Center).ToRotation() + (NPC.direction == -1 ? MathHelper.Pi : MathHelper.PiOver4 / 2);
				MortarAngleThingy += .1f;
			}
			MortarTimer = 0;
			MortarAngleThingy = 0f;
		}

		// if the eyes are dead
		if (!NPC.AnyNPCs(ModContent.NPCType<WallofSteelMouthEye>()) &&
			!NPC.AnyNPCs(ModContent.NPCType<WallofSteelLaserEye>()))
		{
			#region sweeping laser attack
			NPC.ai[1]++;

			if (NPC.ai[1] >= 360 && NPC.ai[1] < 540)
			{
				if (NPC.ai[1] == 360)
				{
					SoundEngine.PlaySound(
						new SoundStyle($"{nameof(Avalon)}/Sounds/Item/LaserCharge2") { Volume = 1.2f },
						NPC.position);
				}
				Vector2 center22 = NPC.Center;
				int num1260 = 0;
				for (int num1261 = 0; num1261 < 1 + num1260; num1261++)
				{
					float num1263 = 0.8f;
					if (num1261 % 2 == 1)
					{
						num1263 = 1.65f;
					}
					Vector2 vector215 = center22 + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * new Vector2(27, 59) / 2f;
					int num1264 = Dust.NewDust(vector215 - Vector2.One * 8f + (NPC.direction == 1 ? new Vector2(40, -10) : new Vector2(-36, -10)), 16, 16, DustID.RedTorch, NPC.velocity.X / 2f, NPC.velocity.Y / 2f);
					Main.dust[num1264].velocity = Vector2.Normalize(center22 - vector215) * 3.5f * (10f - num1260 * 2f) / 10f;
					Main.dust[num1264].noGravity = true;
					Main.dust[num1264].scale = num1263;
					Main.dust[num1264].customData = this;
				}
			}
			if (NPC.ai[1] == 540)
			{
				SoundEngine.PlaySound(
					new SoundStyle("Terraria/Sounds/Zombie_104") { Volume = 0.8f },
					NPC.Center);
				// fire laser
				Vector2 velocityOfProj = Main.player[NPC.target].Center - NPC.Center;
				velocityOfProj.Normalize();
				float num1275 = -1f;
				if (velocityOfProj.Y < 0f)
				{
					num1275 = 1f;
				}
				velocityOfProj = velocityOfProj.RotatedBy((double)((0f - num1275) * MathHelper.TwoPi));
				int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y + NPC.height / 3, velocityOfProj.X, velocityOfProj.Y, ModContent.ProjectileType<WoSBeegLaser>(), 95, 0f, Main.myPlayer, num1275 * MathHelper.TwoPi / 1080f, (float)NPC.whoAmI);
				NPC.localAI[1] += 0.05f;
				if (NPC.localAI[1] > 1f)
				{
					NPC.localAI[1] = 1f;
				}
				float num1277 = 1;
				if (num1277 < 0f)
				{
					num1277 *= -1f;
				}
				num1277 += MathHelper.Pi * -3;
				num1277 += MathHelper.TwoPi / 720f;
				NPC.localAI[0] = num1277;
				NPC.ai[1] = 0;
			}
			if (NPC.ai[1] % 100 == 0)
			{
				int fire;
				float f = -.1f;
				var fireballPos = new Vector2(NPC.Center.X + NPC.DirectionTo(NPC.PlayerTarget().Center).X * 40f, NPC.Center.Y + NPC.DirectionTo(NPC.PlayerTarget().Center).Y * 40f);
				float rotation = (float)Math.Atan2(fireballPos.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), fireballPos.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
				SoundEngine.PlaySound(SoundID.Item33, new Vector2((int)NPC.position.X, AvalonWorld.WallOfSteelT));
				while (f <= .1f)
				{
					fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), fireballPos.X, fireballPos.Y, (float)((Math.Cos(rotation + f) * 12f) * -1), (float)((Math.Sin(rotation + f) * 12f) * -1), ModContent.ProjectileType<WoSLaserSmall>(), Main.expertMode ? 70 : 55, 6f);
					Main.projectile[fire].timeLeft = 600;
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
					}
					fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), fireballPos.X, fireballPos.Y, (float)((Math.Cos(rotation - f) * 12f) * -1), (float)((Math.Sin(rotation - f) * 12f) * -1), ModContent.ProjectileType<WoSLaserSmall>(), Main.expertMode ? 70 : 55, 6f);
					Main.projectile[fire].timeLeft = 600;
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
					}
					f += .1f;
				}
			}
			#endregion
		}
		// if the eyes are not dead
		else
		{
			LaserSpreadTimer++;
			if (LaserSpreadTimer % 500 == 0)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient) // leeches
				{
					int num442 = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height / 2 + 20f), ModContent.NPCType<MechanicalLeechHead>(), 1);
					Main.npc[num442].velocity.X = NPC.direction * 8;
					SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);
				}
			}
			if (LaserSpreadTimer >= 1320 && LaserSpreadTimer < 1500)
			{
				if (LaserSpreadTimer == 1320)
				{
					SoundEngine.PlaySound(
						new SoundStyle($"{nameof(Avalon)}/Sounds/Item/LaserCharge2") { Volume = 1.2f },
						NPC.position);
				}
				Vector2 center22 = NPC.Center;
				int num1260 = 0;
				for (int num1261 = 0; num1261 < 1 + num1260; num1261++)
				{
					float num1263 = 0.8f;
					if (num1261 % 2 == 1)
					{
						num1263 = 1.65f;
					}
					Vector2 vector215 = center22 + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * new Vector2(27, 59) / 2f;
					int num1264 = Dust.NewDust(vector215 - Vector2.One * 8f + (NPC.direction == 1 ? new Vector2(40, -10) : new Vector2(-36, -10)), 16, 16, DustID.RedTorch, NPC.velocity.X / 2f, NPC.velocity.Y / 2f);
					Main.dust[num1264].velocity = Vector2.Normalize(center22 - vector215) * 3.5f * (10f - num1260 * 2f) / 10f;
					Main.dust[num1264].noGravity = true;
					Main.dust[num1264].scale = num1263;
					Main.dust[num1264].customData = this;
				}
			}
			if (LaserSpreadTimer == 1500)
			{
				SoundEngine.PlaySound(
					new SoundStyle("Terraria/Sounds/Zombie_104") { Volume = 0.8f },
					NPC.Center);
				// fire laser
				Vector2 velocityOfProj = Main.player[NPC.target].Center - NPC.Center;
				velocityOfProj.Normalize();
				float num1275 = -1f;
				if (velocityOfProj.Y < 0f)
				{
					num1275 = 1f;
				}
				velocityOfProj = velocityOfProj.RotatedBy((double)((0f - num1275) * MathHelper.TwoPi));
				int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y + NPC.width / 3, velocityOfProj.X, velocityOfProj.Y, ModContent.ProjectileType<WoSBeegLaser>(), 95, 0f, Main.myPlayer, num1275 * MathHelper.TwoPi / 1080f, (float)NPC.whoAmI);
				NPC.localAI[1] += 0.05f;
				if (NPC.localAI[1] > 1f)
				{
					NPC.localAI[1] = 1f;
				}
				float num1277 = 1;
				if (num1277 < 0f)
				{
					num1277 *= -1f;
				}
				num1277 += MathHelper.Pi * -3;
				num1277 += MathHelper.TwoPi / 720f;
				NPC.localAI[0] = num1277;
				LaserSpreadTimer = 0;
			}
		}
		{
			//NPC.ai[3]++;
			//if (NPC.ai[3] == 1)
			//{
			//    NPC.defense = 0;
			//    SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/LaserCharge"), NPC.Center);
			//}
			//if (NPC.ai[3] >= 60 && NPC.ai[3] <= 90)
			//{
			//    if (NPC.ai[3] == 60)
			//    {
			//        SoundEngine.PlaySound(SoundID.Item33, NPC.Center);
			//        NPC.ai[1] = (NPC.velocity.X < 0 ? NPC.position.X : NPC.position.X + NPC.width);
			//        NPC.ai[2] = NPC.Center.Y;
			//        NPC.localAI[1] = NPC.velocity.X;
			//        NPC.localAI[2] = NPC.velocity.Y;
			//    }
			//    int t = ModContent.ProjectileType<Projectiles.Hostile.WallofSteelLaser>(); // middle
			//    if (NPC.ai[3] == 60)
			//        t = ModContent.ProjectileType<Projectiles.Hostile.WallofSteelLaserStart>(); // start
			//    if (NPC.ai[3] == 90)
			//        t = ModContent.ProjectileType<Projectiles.Hostile.WallofSteelLaserEnd>(); // end
			//    if (NPC.ai[3] % 3 == 0)
			//    {
			//        int wide = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.ai[1], NPC.ai[2], NPC.localAI[1], NPC.localAI[2], t, 100, 4f);
			//        if (NPC.velocity.X > 0)
			//        {
			//            Main.projectile[wide].velocity = Vector2.Normalize(new Vector2(NPC.ai[1], NPC.ai[2]) - new Vector2(NPC.ai[1] - 100, NPC.ai[2])) * 20f;
			//        }
			//        else if (NPC.velocity.X < 0)
			//        {
			//            Main.projectile[wide].velocity = Vector2.Normalize(new Vector2(NPC.ai[1] - 100, NPC.ai[2]) - new Vector2(NPC.ai[1], NPC.ai[2])) * 20f;
			//        }

			//        Main.projectile[wide].tileCollide = false;
			//        if (Main.netMode != NetmodeID.SinglePlayer)
			//        {
			//            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, wide);
			//        }
			//    }
			//}
		}
		NPC.spriteDirection = NPC.direction;
		if (NPC.localAI[0] == 1f && Main.netMode != NetmodeID.MultiplayerClient)
		{
			NPC.localAI[0] = 2f;
			for (int num456 = 0; num456 < 11; num456++)
			{
				int hungry = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)num450, ModContent.NPCType<MechanicalHungry>(), NPC.whoAmI);
				Main.npc[hungry].ai[0] = num456 * 0.1f - 0.05f;
			}
			return;
		}
		NPC.velocity.X = 0;
		//NPC.velocity.Y = 0;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore1").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore2").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore3").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore3").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore6").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore6").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore7").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore8").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore9").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore10").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore11").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore12").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore13").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore14").Type, NPC.scale);
		}
	}
	public override void OnSpawn(IEntitySource source)
	{
		int topEye = NPC.NewNPC(source, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<WallofSteelMouthEye>());
		int bottomEye = NPC.NewNPC(source, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<WallofSteelLaserEye>());

		TopEyeMaxHP = Main.npc[topEye].lifeMax;
		TopEyeHP = Main.npc[topEye].life;
		BottomEyeMaxHP = Main.npc[bottomEye].lifeMax;
		BottomEyeHP = Main.npc[bottomEye].life;
	}
	public override void OnKill()
	{
		AvalonWorld.WallOfSteel = -1;
		/*if (!ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode)
		{
			if (!Main.expertMode)
				NPC.DropItemInstanced(NPC.position, new Vector2(NPC.width, NPC.height), ModContent.ItemType<Items.Consumables.MechanicalHeart>());
			Task.Run(() => ModContent.GetInstance<AvalonWorld>().InitiateSuperHardMode());
		}*/
		//if (Main.netMode != NetmodeID.MultiplayerClient)
		//{
		//	int num22 = (int)(NPC.position.X + (NPC.width / 2)) / 16;
		//	int num23 = (int)(NPC.position.Y + (NPC.height / 2)) / 16;
		//	int num24 = NPC.width / 4 / 16 + 1;
		//	for (int k = num22 - num24; k <= num22 + num24; k++)
		//	{
		//		for (int l = num23 - num24; l <= num23 + num24; l++)
		//		{
		//			if ((k == num22 - num24 || k == num22 + num24 || l == num23 - num24 || l == num23 + num24) && !Main.tile[k, l].HasTile)
		//			{
		//				Main.tile[k, l].TileType = (ushort)ModContent.TileType<Tiles.ImperviousBrick>();
		//				Main.tile[k, l].Active(true);
		//			}
		//			Main.tile[k, l].LiquidAmount = 0;
		//			if (Main.netMode == NetmodeID.Server)
		//			{
		//				NetMessage.SendTileSquare(-1, k, l, 1);
		//			}
		//			else
		//			{
		//				WorldGen.SquareTileFrame(k, l, true);
		//			}
		//		}
		//	}
		//}
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
		notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, [ModContent.ItemType<FleshBoiler>(), ModContent.ItemType<MagicCleaver>(), ModContent.ItemType<Items.Accessories.Superhardmode.BubbleBoost>()]));
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossBags.WallofSteelBossBag>()));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WallofSteelTrophy>(), 10));
		npcLoot.Add(notExpertRule);
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SoulofBlight>(), 1, 40, 56));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HellsteelPlate>(), 1, 20, 31));
	}
}
public class WallOfSteelHPHack : ModHook
{
	protected override void Apply()
	{
		On_Main.MouseTextHackZoom_string_string += On_Main_MouseTextHackZoom_string_string;
	}

	private void On_Main_MouseTextHackZoom_string_string(On_Main.orig_MouseTextHackZoom_string_string orig, Main self, string text, string buffTooltip)
	{
		if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().WOSRenderHPText)
		{
			if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().WOSLaserEyeIndex != -1)
			{
				text += ": " + Main.npc[Main.LocalPlayer.GetModPlayer<AvalonPlayer>().WOSLaserEyeIndex].life + "/" + Main.npc[Main.LocalPlayer.GetModPlayer<AvalonPlayer>().WOSLaserEyeIndex].lifeMax;
			}
			if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().WOSMouthEyeIndex != -1)
			{
				text += ": " + Main.npc[Main.LocalPlayer.GetModPlayer<AvalonPlayer>().WOSMouthEyeIndex].life + "/" + Main.npc[Main.LocalPlayer.GetModPlayer<AvalonPlayer>().WOSMouthEyeIndex].lifeMax;
			}
		}
		else
		{
			Main.LocalPlayer.GetModPlayer<AvalonPlayer>().WOSLaserEyeIndex = -1;
			Main.LocalPlayer.GetModPlayer<AvalonPlayer>().WOSMouthEyeIndex = -1;
		}
		orig.Invoke(self, text, buffTooltip);
	}
}
