using Avalon.Common;
using Avalon.Core;
using Avalon.NPCs.Template;
using Avalon.Projectiles.Hostile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Underground;
public class Robot : CustomFighterAI
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 9;
	}
	public override void SetDefaults()
	{
		NPC.width = 24;
		NPC.height = 48;
		NPC.damage = 150;
		NPC.lifeMax = 5000;
		NPC.defense = 150;
		NPC.knockBackResist = 0;
		NPC.noTileCollide = false;
		NPC.noGravity = false;
		NPC.aiStyle = -1;
		NPC.value = Item.buyPrice(silver: 50);
		NPC.HitSound = SoundID.NPCHit4;
		NPC.DeathSound = SoundID.NPCDeath14;
		//Banner = NPC.type;
		//BannerItem = ModContent.ItemType<Items.Banners.RobotBanner>();
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(
		[
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Robot"))
		]);
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ConfusionTalisman>(), 8));
		//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ManaCompromise>(), 100));
	}
	public override float MaxMoveSpeed
	{
		get
		{
			float defaultSpeed = 2f;
			if (DefaultMovement)
			{
				return defaultSpeed;
			}
			else if (Stationary)
			{
				float amount = Utils.Remap(ProjectileTimer - ProjSpawnStartTime, 0, TimeUtils.SecondsToTicks(1) * 1.25f, 0, 1);
				amount = Easings.PowIn(amount, 1.75f);
				return Math.Abs(MathHelper.SmoothStep(NPC.velocity.X, 0, amount));
			}
			else if (SpawnEnergyBall || HoldingEnergyBall)
			{
				return 0.5f;
			}
			return defaultSpeed;
		}
	}
	public override float Acceleration => 0.3f;
	public override bool CanOpenDoors => DefaultMovement;
	public override int TimeBeforeTurningAround => TimeUtils.SecondsToTicks(5);


	public int ProjSpawnStartTime = TimeUtils.SecondsToTicks(8);
	public int ProjSpawnTime = TimeUtils.SecondsToTicks(10);
	public bool DefaultMovement => ProjectileTimer < ProjSpawnStartTime;
	public bool Stationary => !DefaultMovement && ProjectileTimer < ProjSpawnTime;
	public bool SpawnEnergyBall => ProjectileTimer == ProjSpawnTime;
	public bool HoldingEnergyBall => ProjectileTimer > ProjSpawnTime && ProjectileTimer < TimeUtils.SecondsToTicks(14);
	public bool ResetToDefault => ProjectileTimer >= TimeUtils.SecondsToTicks(14);
	private float ProjectileTimer;
	public override void SendExtraAI(BinaryWriter writer)
	{
		base.SendExtraAI(writer);
		writer.Write(ProjectileTimer);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.ReceiveExtraAI(reader);
		ProjectileTimer = reader.ReadSingle();
	}
	public Vector2 GetProjectileSpawnPos()
	{
		return NPC.Bottom - new Vector2(0, 20 * NPC.scale + NPC.height * NPC.scale - Main.NPCAddHeight(NPC) - NPC.gfxOffY);
	}
	public override void CustomBehavior()
	{
		ProjectileTimer++;
		if (Stationary)
		{
			float chargeUp = Utils.Remap(ProjectileTimer - ProjSpawnStartTime, 0, ProjSpawnTime - ProjSpawnStartTime, 0, 1);
			chargeUp = Easings.PowIn(chargeUp, 0.75f);
			if (Main.rand.NextBool((int)(chargeUp * 10), 40))
			{
				Vector2 size = ContentSamples.ProjectilesByType[ModContent.ProjectileType<RobotEnergyBall>()].Size * NPC.scale;
				Dust d = Dust.NewDustDirect(GetProjectileSpawnPos() - size / 2f + NPC.velocity, (int)size.X, (int)size.Y / 2, DustID.Electric);
				d.noGravity = !Main.rand.NextBool(4);
				d.scale = 0.75f;
			}
			NPC.direction = NPC.oldDirection;
			RunningModeTimer = 0;
		}
		else if (SpawnEnergyBall)
		{
			RunningMode = 0;
			Projectile.NewProjectile(NPC.GetSource_FromAI(), GetProjectileSpawnPos(), Vector2.Zero, ModContent.ProjectileType<RobotEnergyBall>(), 220, 0, ai1: NPC.whoAmI);
		}
		else if (ResetToDefault)
		{
			ProjectileTimer = 0;
		}

		Lighting.AddLight(NPC.Center, 0.12f, 0.105f, 0);
	}
	public override void Jump(float height)
	{
		if (DefaultMovement)
		{
			base.Jump(height);
		}
	}
	public override void FindFrame(int frameHeight)
	{
		if (NPC.velocity.Y == 0f)
		{
			if (NPC.direction == 1)
			{
				NPC.spriteDirection = 1;
			}
			if (NPC.direction == -1)
			{
				NPC.spriteDirection = -1;
			}
		}
		if (NPC.velocity.Y != 0f)
		{
			NPC.frameCounter = 0.0;
			NPC.frame.Y = frameHeight * 8;
		}
		else if (NPC.velocity.X == 0f || NPC.direction == -1 && NPC.velocity.X > 0f || NPC.direction == 1 && NPC.velocity.X < 0f)
		{
			NPC.frameCounter = 0.0;
			NPC.frame.Y = 0;
		}
		else
		{
			NPC.frameCounter += Math.Abs(NPC.velocity.X);
			double frameTime = 12.0;
			if (NPC.frameCounter < frameTime * (Main.npcFrameCount[NPC.type] - 1))
			{
				NPC.frame.Y = frameHeight * (int)(NPC.frameCounter / frameTime);
			}
			else
			{
				NPC.frameCounter = 0.0;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotFace").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotBody").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotBody").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotClaw").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotClaw").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotFoot").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotFoot").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotFoot").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotLimb").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotLimb").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotLimb").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotLimb").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RobotLimb").Type);

			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, GoreID.Smoke1);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, GoreID.Smoke2);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, GoreID.Smoke3);
		}
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		return spawnInfo.Player.ZoneRockLayerHeight && /*ModContent.GetInstance<AvalonWorld>().SuperHardmode*/ false ? 0.14f : 0;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Rectangle frame = NPC.frame;
		if (ProjectileTimer >= TimeUtils.SecondsToTicks(8) && ProjectileTimer < TimeUtils.SecondsToTicks(14))
		{
			frame.Y = NPC.frame.Height * 8;
		}
		else if (frame.Y == NPC.frame.Height * 8)
		{
			frame.Y = 0;
		}
		spriteBatch.Draw(AssetReferences.NPCs.Underground.Robot_Arms.Asset.Value, NPC.GetNPCDrawPos(screenPos), frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : default, 0f);
		spriteBatch.Draw(AssetReferences.NPCs.Underground.Robot_Glow.Asset.Value, NPC.GetNPCDrawPos(screenPos), frame, Color.White, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : default, 0f);
	}
}
