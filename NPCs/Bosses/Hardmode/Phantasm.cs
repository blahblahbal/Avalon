using System;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Trophy;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Linq;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Avalon.Common.Players;
using Avalon.Common;
using ReLogic.Content;
using Terraria.GameContent;
using Avalon.Projectiles.Hostile.Phantasm;
using System.IO;
using Avalon.Dusts;
using Avalon.Particles;
using static Avalon.Particles.ParticleSystem;
using Terraria.DataStructures;

namespace Avalon.NPCs.Bosses.Hardmode;

[AutoloadBossHead]
public partial class Phantasm : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 12;
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
		phase = 0;
		NPC.Size = new Vector2(66);
		NPC.boss = NPC.noTileCollide = NPC.noGravity = true;
		NPC.npcSlots = 100f;
		NPC.damage = 105;
		NPC.lifeMax = 100000;
		NPC.defense = 20;
		NPC.aiStyle = -1;
		NPC.value = 100000f;
		NPC.knockBackResist = 0f;
		NPC.scale = 1.5f;
		SoundStyle sound = SoundID.NPCHit54;
		sound.Pitch += 0.1f;
		NPC.HitSound = sound;
		NPC.DeathSound = SoundID.NPCDeath39;
		Music = ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Phantasm") : MusicID.EmpressOfLight;
		SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Hellcastle>().Type };
	}
	public Vector2 eyePos;
	public override void OnSpawn(IEntitySource source)
	{
		playPhantasmSound();
		eyePos = NPC.Center;
	}
	public override void BossLoot(ref string name, ref int potionType)
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

		if(phase > 3 && Main.rand.NextBool(3))
			AddParticle(new SoulEmbers(), new Vector2(Main.rand.Next(-2000, 2000) + Main.LocalPlayer.position.X, Main.LocalPlayer.position.Y + 600), new Vector2(Main.rand.NextFloat(-5, 5), -1), default);
		if (phase > 8 && Main.rand.NextBool(3))
			AddParticle(new HellEmbers(), new Vector2(Main.rand.Next(-2000, 2000) + Main.LocalPlayer.position.X, Main.LocalPlayer.position.Y + 600), new Vector2(Main.rand.NextFloat(-5, 5), -1), default);

		//Main.NewText(phase);
		if (!NPC.HasValidTarget)
		{
			NPC.TargetClosest();
		}
		Vector2 vector = NPC.Center;
		NPC.scale = 1.2f + (0.3f * (NPC.life / (float)NPC.lifeMax));
		NPC.Size = new Vector2(66 * NPC.scale);
		NPC.Center = vector;


		if (NPC.life <= NPC.lifeMax * 0.7f && phase < 3)
		{
			phase = 3;
			NPC.ai[0] = -60;
			NPC.ai[1] = 0;
			NPC.ai[2] = 0;
			NPC.netUpdate = true;
		}
		else if (NPC.life <= NPC.lifeMax * 0.3f && phase < 8 && phase != 3)
		{
			phase = 8;
			NPC.ai[0] = -60;
			NPC.ai[1] = 0;
			NPC.ai[2] = 0;
			NPC.netUpdate = true;
		}

		switch (phase)
		{
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
				Phase11_PhantasmalDeathrayLikeMoonlordOMQCantBelieveTheySTOLEFROMAVALONOnceAgainTrulyPatheticRelogicWhyWouldYouDoThatLikeSeriouslyGuysWhatTheHellWhyWouldYouDoThatStealingIsWrongDontDoItGuysWTFGenuinelyCannotBelieveThisWouldHappenManWhatTheHellITRUSTEDYOURELOGICBUTyouGoAndDOTHISWHATTHEHELL();
					break;
		}
	}
	private void playPhantasmSound()
	{
		return;
		CombatText.NewText(NPC.Hitbox, new Color(1f,1f,1f,0f), "Phantasm!", true);
		Main.NewText("<Phantasm> Phantasm!", Color.Cyan);
		SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/Phantasm") { Volume = 0.8f, PitchVariance = 0.2f }, NPC.position);
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			for (int i = 0; i < 40; i++)
			{
				int num890 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default(Color), 1f);
				Main.dust[num890].velocity *= 5f;
				Main.dust[num890].scale = 1.5f;
				Main.dust[num890].noGravity = true;
				Main.dust[num890].fadeIn = 2f;
			}
			for (int i = 0; i < 20; i++)
			{
				int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default(Color), 1f);
				Main.dust[num893].velocity *= 2f;
				Main.dust[num893].scale = 1.5f;
				Main.dust[num893].noGravity = true;
				Main.dust[num893].fadeIn = 3f;
			}
			for (int i = 0; i < 40; i++)
			{
				int num892 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SpectreStaff, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num892].velocity *= 5f;
				Main.dust[num892].scale = 1.5f;
				Main.dust[num892].noGravity = true;
				Main.dust[num892].fadeIn = 2f;
			}
			for (int i = 0; i < 40; i++)
			{
				int num891 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default(Color), 1f);
				Main.dust[num891].velocity *= 10f;
				Main.dust[num891].scale = 1.5f;
				Main.dust[num891].noGravity = true;
				Main.dust[num891].fadeIn = 1.5f;
			}

			for (int i = 0; i < 25; i++)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Main.rand.NextVector2Circular(12,12), ModContent.ProjectileType<Phantom>(), 5, 1, -1, target.whoAmI);
			}
		}
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
		notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<Items.Weapons.Magic.Hardmode.PhantomKnives>(), ModContent.ItemType<Items.Accessories.Hardmode.EtherealHeart>(), ModContent.ItemType<Items.Accessories.Hardmode.VampireTeeth>() }));
		notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GhostintheMachine>(), 1, 3, 6));
		npcLoot.Add(notExpertRule);
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PhantasmTrophy>(), 10));
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossBags.PhantasmBossBag>()));
	}
	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter++;
		NPC.frame.Y = frameHeight * (int)((NPC.frameCounter / 4) % 4);
		if (phase > 3)
			NPC.frame.Y += 416;
		if (phase > 8)
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
		int frameHeight = texture.Value.Height / Main.npcFrameCount[NPC.type];
		Rectangle sourceRectangle = new Rectangle(0, NPC.frame.Y, texture.Value.Width, frameHeight);
		Vector2 frameOrigin = sourceRectangle.Size() / 2f;
		Vector2 offset = new Vector2(NPC.width / 2 - frameOrigin.X, NPC.height - sourceRectangle.Height);

		Vector2 drawPos = NPC.position - Main.screenPosition + frameOrigin + offset;

		for (int i = 0; i < NPC.oldPos.Length; i++)
		{
			Vector2 drawPosOld = NPC.oldPos[i] - Main.screenPosition + frameOrigin + offset;
			Main.EntitySpriteDraw(texture.Value, drawPosOld + (NPC.IsABestiaryIconDummy ? Main.screenPosition : Vector2.Zero), sourceRectangle, new Color(255, 125, 255, 225) * (1 - (i * 0.25f)) * 0.2f, NPC.rotation, frameOrigin, NPC.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture.Value, drawPos + (NPC.IsABestiaryIconDummy ? Main.screenPosition : Vector2.Zero), sourceRectangle, new Color(255, 255, 255, 225) * 0.3f, NPC.rotation, frameOrigin, NPC.scale * 1.1f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos + (NPC.IsABestiaryIconDummy ? Main.screenPosition : Vector2.Zero), sourceRectangle, new Color(255, 255, 255, 225) * 0.15f, NPC.rotation, frameOrigin, NPC.scale * 1.2f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos + (NPC.IsABestiaryIconDummy ? Main.screenPosition : Vector2.Zero), sourceRectangle, new Color(255, 255, 255, 225), NPC.rotation, frameOrigin, new Vector2(NPC.scale, NPC.scale), SpriteEffects.None, 0);
		return false;
	}
}
