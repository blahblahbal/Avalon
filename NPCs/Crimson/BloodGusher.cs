using Avalon.Items.Banners;
using Avalon.Projectiles.Hostile;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Crimson;

public class BloodGusher : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 4;
		Data.Sets.NPCSets.Wicked[NPC.type] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			// Influences how the NPC looks in the Bestiary
			Position = new Vector2(8f, -6f),
			Rotation = MathHelper.PiOver4
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
		bestiaryEntry.Info.AddRange(
		[
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.BloodGusher")),
		]);

	public override void SetDefaults()
	{
		NPC.damage = 45;
		NPC.lifeMax = 260;
		NPC.defense = 45;
		NPC.width = NPC.height = 30;
		NPC.aiStyle = -1;
		NPC.value = 750;
		NPC.knockBackResist = 0.2f;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		Banner = NPC.type;
		BannerItem = ModContent.ItemType<BloodGusherBanner>();
		NPC.noGravity = true;
		NPC.scale = 1.1f;
		DrawOffsetY = 20;
	}
	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += Utils.Remap(NPC.ai[1], -30, 60, 0, 1);
		NPC.frame.Y = (int)((NPC.frameCounter / 8) % 2) * frameHeight;
		if (NPC.ai[1] < 0)
			NPC.frame.Y += frameHeight * 2;
	}
	private const int _attackTime = 120;
	public override void AI()
	{
		if (NPC.collideX)
		{
			NPC.netUpdate = true;
			NPC.velocity.X = NPC.oldVelocity.X * (-0.2f);
			if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
				NPC.velocity.X = 2f;

			if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
				NPC.velocity.X = -2f;
			if (Main.rand.NextBool(3))
				NPC.direction = Math.Sign(NPC.velocity.X);
		}
		if (NPC.collideY)
		{
			NPC.netUpdate = true;
			NPC.velocity.Y = NPC.oldVelocity.Y * (-0.2f);
			if (NPC.velocity.Y > 0f && (double)NPC.velocity.Y < 1.5)
				NPC.velocity.Y = 2f;

			if (NPC.velocity.Y < 0f && (double)NPC.velocity.Y > -1.5)
				NPC.velocity.Y = -2f;
			if (Main.rand.NextBool(3))
				NPC.directionY = Math.Sign(NPC.velocity.Y);
		}

		if (NPC.HasValidTarget && NPC.ai[0] < 100)
		{
			Player target = Main.player[NPC.target];

			// this is for the jittery turning
			Vector2 vector = NPC.Center;
			float num4 = Main.player[NPC.target].Center.X;
			float num5 = Main.player[NPC.target].Center.Y;
			num4 = (int)(num4 / 8f) * 8;
			num5 = (int)(num5 / 8f) * 8;
			vector.X = (int)(vector.X / 8f) * 8;
			vector.Y = (int)(vector.Y / 8f) * 8;
			num4 -= vector.X;
			num5 -= vector.Y;
			NPC.rotation = (float)Math.Atan2(num5, num4) - MathHelper.PiOver2;

			if (Main.rand.NextBool(15))
			{
				Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood);
				d.velocity *= 0.2f;
				d.scale = 0.8f;
			}

			NPC.direction = target.Center.X > NPC.Center.X ? 1 : -1;
			NPC.directionY = target.Center.Y > NPC.Center.Y ? 1 : -1;

			NPC.ai[1]++;
			if (NPC.justHit && NPC.ai[1] > _attackTime - 60)
			{
				NPC.ai[1] = _attackTime - 60;
			}
			if (NPC.ai[1] >= _attackTime - 30 && (target.Center.Distance(NPC.Center) > 300 || !Collision.CanHit(NPC, target)) && NPC.ai[1] < _attackTime)
			{
				NPC.ai[1] = _attackTime - 30;
				NPC.ai[0]++;
			}
			if (NPC.ai[1] < _attackTime)
			{

				NPC.SimpleFlyMovement(new Vector2(NPC.direction, NPC.directionY) * Utils.Remap(NPC.ai[1], -30, 60, 0, 3), 0.05f);
			}
			else
			{
				SoundEngine.PlaySound(SoundID.NPCDeath13);
				NPC.ai[1] = -30;
				Vector2 direction = NPC.Center.DirectionTo(target.Center);
				NPC.velocity = direction * -5;
				if (Main.netMode != NetmodeID.MultiplayerClient)
					for (int i = 0; i < 5; i++)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction.RotatedByRandom(0.6f) * Main.rand.NextFloat(3, 8), ModContent.ProjectileType<BloodgusherBlood>(), 15, 1);
					}
			}
		}
		else
		{
			NPC.TargetClosest(false);
			if (NPC.HasValidTarget)
			{
				if (Collision.CanHit(NPC, Main.player[NPC.target]))
				{
					NPC.ai[0] = 0;
				}
			}
			NPC.SimpleFlyMovement(new Vector2(NPC.direction, NPC.directionY) * 3, 0.05f);
			NPC.rotation = NPC.velocity.ToRotation() - MathHelper.PiOver2;
		}
	}
	public override void ModifyNPCLoot(NPCLoot loot)
	{
		loot.Add(ItemDropRule.Common(ItemID.Vertebrae, 3));
		loot.Add(ItemDropRule.Common(ItemID.MeatGrinder, 200));
		loot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsUp(), ItemID.PigPetItem, 500));
		loot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsNotUp(), ItemID.PigPetItem, 1500));
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		return spawnInfo.Player.ZoneCrimson && Main.hardMode ? 0.2f : 0;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life > 0)
		{
			for (int i = 0; i < hit.Damage * 0.3f; i++)
			{
				Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -2f, NPC.alpha, NPC.color, NPC.scale);
			}
			return;
		}
		for (int i = 0; i < 50; i++)
		{
			Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -2f, NPC.alpha, NPC.color, NPC.scale);
		}
		for (int i = 0; i < 2; i++)
		{
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>(Name + "Gore_" + i).Type, NPC.scale);
		}
	}
}
