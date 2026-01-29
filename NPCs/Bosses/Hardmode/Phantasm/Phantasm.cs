using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Avalon.Dusts;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Weapons.Magic.Hardmode.PhantomKnives;
using Avalon.Particles;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Avalon.Particles.ParticleSystem;

namespace Avalon.NPCs.Bosses.Hardmode.Phantasm;

[AutoloadBossHead]
public partial class Phantasm : ModNPC
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.MPAllowedEnemies[Type] = true;
		Main.npcFrameCount[NPC.type] = 16;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
		NPCID.Sets.TrailCacheLength[NPC.type] = 4;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2(0, -16f),
			PortraitPositionYOverride = 0,
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
	}
	public byte phase;
	public Player target
	{ get { return Main.player[NPC.target]; } }
	public override void SetDefaults()
	{
		phase = 200;
		NPC.Size = new Vector2(66);
		NPC.boss = NPC.noTileCollide = NPC.noGravity = true;
		NPC.npcSlots = 100f;
		NPC.damage = 40;
		NPC.lifeMax = 220000;
		NPC.defense = 25;
		NPC.aiStyle = -1;
		NPC.value = 100000f;
		NPC.knockBackResist = 0f;
		NPC.scale = 1.5f;
		if(!NPC.IsABestiaryIconDummy)
			NPC.alpha = 255;
		SoundStyle sound = SoundID.NPCHit54;
		sound.Pitch += 0.1f;
		NPC.HitSound = sound;
		NPC.DeathSound = SoundID.NPCDeath39;
		Music = ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Phantasm") : MusicID.EmpressOfLight;
		SpawnModBiomes = [ModContent.GetInstance<Biomes.Hellcastle>().Type];
	}
	public Vector2 eyePos;
	public override void OnSpawn(IEntitySource source)
	{
		Transition();
		eyePos = NPC.Center;
	}
	public override void BossLoot(ref int potionType)
	{
		potionType = ItemID.SuperHealingPotion;
	}
	public override void OnKill()
	{
		AvalonGlobalNPC.PhantasmBoss = -1;
		if (!ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)
		{
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText("The spirits are stirring in the depths!", new Color(50, 255, 130));
			}
			else if (Main.netMode == NetmodeID.Server)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The spirits are stirring in the depths!"), new Color(50, 255, 130));
			}
			NPC.SetEventFlagCleared(ref ModContent.GetInstance<DownedBossSystem>().DownedPhantasm, -1);
		}
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(phase);
		writer.WriteVector2(eyePos);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		phase = reader.ReadByte();
		eyePos = reader.ReadVector2();
	}
	public override void AI()
	{
		
		Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit);
		d.noGravity = true;
		d.scale = Main.rand.NextFloat(1);

		if(phase > 3 && Main.rand.NextBool(3) && phase != 200)
			AddParticle(new SoulEmbers(), new Vector2(Main.rand.Next(-2000, 2000) + Main.LocalPlayer.position.X, Main.LocalPlayer.position.Y + 600), new Vector2(Main.rand.NextFloat(-5, 5), -1), default);
		if (phase > 8 && Main.rand.NextBool(3) && phase != 200)
			AddParticle(new HellEmbers(), new Vector2(Main.rand.Next(-2000, 2000) + Main.LocalPlayer.position.X, Main.LocalPlayer.position.Y + 600), new Vector2(Main.rand.NextFloat(-5, 5), -1), default);

		foreach(NPC orb in Main.ActiveNPCs)
		{
			if(orb.type == ModContent.NPCType<PhantasmHealthOrbs>())
			{
				NPC.dontTakeDamage = true;
				break;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
		}
		if (NPC.justHit || NPC.lifeRegen < 0)
		{
			Vector2 vector = NPC.Center;
			NPC.scale = Utils.Remap(NPC.life, 0, NPC.lifeMax, Main.expertMode ? 0.7f : 1f, 1.6f);
			NPC.Size = new Vector2(66 * NPC.scale);
			NPC.Center = vector;
		}

		if (!NPC.HasValidTarget)
		{
			NPC.TargetClosest();
			if (!NPC.HasValidTarget)
			{
				NPC.alpha += 4;
				if (NPC.alpha > 255)
				{
					NPC.active = false;
				}
				return;
			}
		}
		switch (phase)
		{
			case 200:
				if (NPC.ai[0] == 0)
				{
					SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/PhantasmJumpscareScary2024") { Volume = 0.8f, }, NPC.Center);
					//SoundEngine.PlaySound(SoundID.ScaryScream, vector);
					for (int x = 0; x < 35; x++)
					{
						Dust d2 = Dust.NewDustPerfect(NPC.Center, DustID.DungeonSpirit);
						d2.noGravity = true;
						d2.velocity = Main.rand.NextVector2Circular(30, 30);
						d2.scale = 2;
					}
					NPC.velocity.Y = 10;
				}
				NPC.velocity *= 0.96f;
				NPC.ai[0]++;
				NPC.alpha = (int)(NPC.alpha * 0.9f);

				if (NPC.ai[0] > 120)
				{
					Transition();
					NPC.alpha = 0;
					phase = 0; 
					NPC.TargetClosest();
				}
				break;
			case 0:
				Phase0_Dash();
				break;
			case 1:
				Phase1_Swords();
				break;
			case 2:
				Phase2_Hands();
				break;
			case 3:
				Phase3_Transition();
				break;
			case 4:
				Phase4_Dash2();
				break;
			case 5:
				Phase5_Hands2();
				break;
			case 6:
				Phase6_Swords2();
				break;
			case 7:
				Phase7_SawHandsScary();
				break;
			case 8:
				Phase8_Transition2();
				break;
			case 9:
				Phase9_Dash3();
				break;
			case 10:
				Phase10_ZoinksItsTheGayBlades();
				break;
			case 11:
				Phase11_SawHandsScary2();
				break;
			case 12:
				Phase12_Hands3();
					break;
			case 13:
				Phase13_PhantasmalDeathrayLikeMoonlordOMQCantBelieveTheySTOLEFROMAVALONOnceAgainTrulyPatheticRelogicWhyWouldYouDoThatLikeSeriouslyGuysWhatTheHellWhyWouldYouDoThatStealingIsWrongDontDoItGuysWTFGenuinelyCannotBelieveThisWouldHappenManWhatTheHellITRUSTEDYOURELOGICBUTyouGoAndDOTHISWHATTHEHELLISeriouslyCannotBelieveThatTheyWouldDoThatLikeReallyWhatTheHellGuysLikeComeOnWTFDudesAndDudettesAndNoneOfTheAbovesTheRenameBoxIsSoLongThatItGoesOffTheScreenWhenYouTryToRenameThisMethodWhichIsKindaFunnyIThinkInMyOpinionIDKThoughItsJustMySenseofHumorNotYoursYouAreAllowedToDisagreeAllYouWantIWontReallyCareAllThatMuchButIMIghtMentallySendNeedlesToYourMindOrSomethingLikeThatIfYouTellMeYouDisagree();
				break;
		}
	}
	public static void ApplyShadowCurse(Player p)
	{
		p.AddBuff(ModContent.BuffType<ShadowCurse>(), 60 * 15);
	}
	private void Transition()
	{
		NPC.netUpdate = true;
		if (NPC.life <= NPC.lifeMax * 0.65f && phase < 4)
		{
			phase = 3;
			NPC.ai[0] = -60;
			NPC.ai[1] = 0;
			NPC.ai[2] = 0;
		}
		else if (NPC.life <= NPC.lifeMax * 0.45f && phase < 9)
		{
			phase = 8;
			NPC.ai[0] = -60;
			NPC.ai[1] = 0;
			NPC.ai[2] = 0;
		}
		NPC.direction = Main.rand.NextBool() ? 1 : -1;
		return;
		CombatText.NewText(NPC.Hitbox, new Color(1f,1f,1f,0f), "Phantasm!", true);
		Main.NewText("<Phantasm> Phantasm!", Color.Cyan);
		SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/Phantasm") { Volume = 0.8f, PitchVariance = 0.2f }, NPC.position);
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			if(SoundEngine.TryGetActiveSound(beamSound, out ActiveSound sound) && sound != null && sound.IsPlaying)
			{
				sound.Stop();
			}

			for (int i = 0; i < 20; i++)
			{
				PrettySparkleParticle s = VanillaParticlePools.PoolPrettySparkle.RequestParticle();
				s.LocalPosition = NPC.Center;
				s.Velocity = Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(6f, 12f);
				s.Rotation = s.Velocity.ToRotation();
				s.Scale = new Vector2(6f, 2f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(3, 10);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(20, 40);
				s.AdditiveAmount = 1f;
				s.ColorTint = new Color(1f, 0f, 0.2f);
				Main.ParticleSystem_World_OverPlayers.Add(s);
			}
			for (int i = 0; i < 20; i++)
			{
				PrettySparkleParticle s = VanillaParticlePools.PoolPrettySparkle.RequestParticle();
				s.LocalPosition = NPC.Center;
				s.Velocity = Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(12f, 24f);
				s.Rotation = s.Velocity.ToRotation();
				s.Scale = new Vector2(3f, 1f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(3, 5);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(10, 30);
				s.AdditiveAmount = 1f;
				s.ColorTint = new Color(Main.rand.NextFloat(0.2f, 0.7f), 0.9f, 1f);
				Main.ParticleSystem_World_OverPlayers.Add(s);
			}
			for (int i = 0; i < 40; i++)
			{
				int num890 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default, 1f);
				Main.dust[num890].velocity *= Main.rand.NextFloat(10f);
				Main.dust[num890].scale = 1.5f;
				Main.dust[num890].noGravity = true;
				Main.dust[num890].fadeIn = 2f;
			}

			//for (int i = 0; i < 40; i++)
			//{
			//	int num890 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default(Color), 1f);
			//	Main.dust[num890].velocity *= 5f;
			//	Main.dust[num890].scale = 1.5f;
			//	Main.dust[num890].noGravity = true;
			//	Main.dust[num890].fadeIn = 2f;
			//}
			//for (int i = 0; i < 20; i++)
			//{
			//	int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default(Color), 1f);
			//	Main.dust[num893].velocity *= 2f;
			//	Main.dust[num893].scale = 1.5f;
			//	Main.dust[num893].noGravity = true;
			//	Main.dust[num893].fadeIn = 3f;
			//}
			//for (int i = 0; i < 40; i++)
			//{
			//	int num892 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SpectreStaff, 0f, 0f, 0, default(Color), 1f);
			//	Main.dust[num892].velocity *= 5f;
			//	Main.dust[num892].scale = 1.5f;
			//	Main.dust[num892].noGravity = true;
			//	Main.dust[num892].fadeIn = 2f;
			//}
			//for (int i = 0; i < 40; i++)
			//{
			//	int num891 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default, 1f);
			//	Main.dust[num891].velocity *= 10f;
			//	Main.dust[num891].scale = 1.5f;
			//	Main.dust[num891].noGravity = true;
			//	Main.dust[num891].fadeIn = 1.5f;
			//}

			//for (int i = 0; i < 25; i++)
			//{
			//	Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Main.rand.NextVector2Circular(12,12), ModContent.ProjectileType<Phantom>(), 5, 1, -1, target.whoAmI);
			//}
		}
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
		notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, [ModContent.ItemType<PhantomKnives>(), ModContent.ItemType<Items.Accessories.Hardmode.EtherealHeart>(), ModContent.ItemType<Items.Accessories.Hardmode.VampireTeeth>()]));
		notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GhostintheMachine>(), 1, 3, 6));
		npcLoot.Add(notExpertRule);
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PhantasmTrophy>(), 10));
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossBags.PhantasmBossBag>()));
	}
	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter++;
		NPC.frame.Y = frameHeight * (int)((NPC.frameCounter / 4) % 4);
		if (phase > 3 && phase != 200)
			NPC.frame.Y += 416;
		if (phase > 8 && phase != 200)
			NPC.frame.Y += 416;
		if (NPC.dontTakeDamage)
			NPC.frame.Y += 416;
	}
	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		position -= new Vector2(0, -25f);
		scale = 1.5f;
		return true;
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Asset<Texture2D> texture = TextureAssets.Npc[Type];
		Vector2 origin = new Vector2(33, 64);
		for (int i = 0; i < NPC.oldPos.Length; i++)
		{
			Vector2 drawPosOld = NPC.oldPos[i] - screenPos + NPC.Size / 2;
			Main.EntitySpriteDraw(texture.Value, drawPosOld, NPC.frame, new Color(255, 125, 255, 225) * (1 - (i * 0.25f)) * 0.2f * NPC.Opacity, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0);
		}
		Color color = (!NPC.dontTakeDamage?  Color.White : new Color(Color.Purple.R,Color.Purple.G,Color.Purple.B,Main.masterColor));
		Main.EntitySpriteDraw(texture.Value, NPC.Center - screenPos, NPC.frame, color * 0.3f * NPC.Opacity, NPC.rotation, origin, NPC.scale * 1.1f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, NPC.Center - screenPos, NPC.frame, color * 0.15f * NPC.Opacity, NPC.rotation, origin, NPC.scale * 1.2f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, NPC.Center - screenPos, NPC.frame, color * NPC.Opacity, NPC.rotation, origin, new Vector2(NPC.scale, NPC.scale), SpriteEffects.None, 0);
		return false;
	}
}
