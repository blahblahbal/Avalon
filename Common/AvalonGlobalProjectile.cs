using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Avalon.Items.Weapons.Melee.Hardmode.CaesiumMace;
using Avalon.Items.Weapons.Melee.Hardmode.CraniumCrusher;
using Avalon.Items.Weapons.Melee.Hardmode.HallowedClaymore;
using Avalon.Items.Weapons.Melee.PreHardmode.MarrowMasher;
using Avalon.Items.Weapons.Melee.PreHardmode.Sporalash;
using Avalon.Items.Weapons.Melee.PreHardmode.TheCell;
using Avalon.Items.Weapons.Melee.PreHardmode.UrchinMace;
using Avalon.Items.Weapons.Melee.PreHardmode.WoodenClubs;
using Avalon.Tiles.Contagion.BigPlants;
using Avalon.Tiles.GemTrees;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

internal class AvalonGlobalProjectile : GlobalProjectile
{
	private static List<int> _blacklistedTargets = new List<int>();
	public static void UpgradeableMinionCounterAI(Projectile projectile, Player owner, int buffType, ref bool playerMinionBool)
	{
		owner.AddBuff(buffType, 3600);
		projectile.position = owner.position;
		projectile.damage = 0;
		if (owner.dead)
		{
			playerMinionBool = false;
		}
		if (playerMinionBool)
		{
			projectile.timeLeft = 2;
		}

		List<int> blacklistedTargets = _blacklistedTargets;
		blacklistedTargets.Clear();
		AI_GetMyGroupIndexAndFillBlackList(projectile, blacklistedTargets, out int index, out int totalIndexesInGroup);

		Vector2 center2 = Projectile.AI_164_GetHomeLocation(owner, index, totalIndexesInGroup);
		projectile.Center = center2;
	}
	public static void AI_GetMyGroupIndexAndFillBlackList(Projectile projectile, List<int> blackListedTargets, out int index, out int totalIndexesInGroup)
	{
		index = 0;
		totalIndexesInGroup = 0;
		foreach (Projectile otherProj in Main.ActiveProjectiles)
		{
			if (otherProj.owner == projectile.owner && otherProj.type == projectile.type)
			{
				if (projectile.whoAmI > otherProj.whoAmI)
				{
					index++;
				}
				totalIndexesInGroup++;
			}
		}
	}
	public static void GetMinionTarget(Projectile minion, Player owner, out bool hasTarget, out NPC target, out float targetDist, float maxDist = 800f, bool ignoreTiles = false)
	{
		target = new NPC();
		targetDist = maxDist;
		hasTarget = false;

		if (owner.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[owner.MinionAttackTargetNPC];
			if (ignoreTiles || Collision.CanHitLine(minion.position, minion.width, minion.height, npc.position, npc.width, npc.height))
			{
				target = npc;
				targetDist = Vector2.Distance(minion.Center, target.Center);
				hasTarget = true;
			}
		}
		if (!owner.HasMinionAttackTargetNPC || !hasTarget)
		{
			foreach (var npc in Main.ActiveNPCs)
			{
				if (npc.CanBeChasedBy(minion.ModProjectile, false))
				{
					float distance = Vector2.Distance(npc.Center, minion.Center);
					if ((distance < targetDist || !hasTarget) && (ignoreTiles || Collision.CanHitLine(minion.position, minion.width, minion.height, npc.position, npc.width, npc.height)))
					{
						targetDist = distance;
						target = npc;
						hasTarget = true;
					}
				}
			}
		}
	}
	public static void AvoidOwnedMinions(Projectile minion)
	{
		float idleAccel = 0.05f;
		foreach (var otherProj in Main.ActiveProjectiles)
		{
			if (otherProj.whoAmI != minion.whoAmI && otherProj.owner == minion.owner && Math.Abs(minion.position.X - otherProj.position.X) + Math.Abs(minion.position.Y - otherProj.position.Y) < minion.width)
			{
				if (minion.position.X < otherProj.position.X)
				{
					minion.velocity.X -= idleAccel;
				}
				else
				{
					minion.velocity.X += idleAccel;
				}
				if (minion.position.Y < otherProj.position.Y)
				{
					minion.velocity.Y -= idleAccel;
				}
				else
				{
					minion.velocity.Y += idleAccel;
				}
			}
		}
	}
	public static void AvoidOtherGas(Projectile proj, Vector2? size = null, Vector2? otherSize = null, float strength = 1f)
	{
		if (size == null)
		{
			size = proj.Size;
		}
		Rectangle hitbox = new((int)(proj.Center.X - size.Value.X / 2), (int)(proj.Center.Y - size.Value.Y / 2), (int)size.Value.X, (int)size.Value.Y);
		foreach (Projectile otherProj in Main.ActiveProjectiles)
		{
			if (otherProj.whoAmI == proj.whoAmI || otherProj.type != proj.type)
			{
				continue;
			}
			if (otherSize == null)
			{
				otherSize = proj.Size;
			}
			Rectangle otherHitbox = new((int)(otherProj.Center.X - otherSize.Value.X / 2), (int)(otherProj.Center.Y - otherSize.Value.Y / 2), (int)otherSize.Value.X, (int)otherSize.Value.Y);
			if (!hitbox.Intersects(otherHitbox))
			{
				continue;
			}
			Vector2 dist = otherProj.Center - proj.Center;
			if (dist == Vector2.Zero)
			{
				if (otherProj.whoAmI < proj.whoAmI)
				{
					dist.X = -1f;
					dist.Y = 1f;
				}
				else
				{
					dist.X = 1f;
					dist.Y = -1f;
				}
			}
			Vector2 velMod = dist.SafeNormalize(Vector2.UnitX) * (0.005f * strength);
			proj.velocity = Vector2.Lerp(proj.velocity, proj.velocity - velMod, 0.6f);
			otherProj.velocity = Vector2.Lerp(otherProj.velocity, otherProj.velocity + velMod, 0.6f);
		}
	}
	public static bool GasAvoidTiles(Projectile proj, bool fastProj = false)
	{
		Vector2 velSafe = proj.velocity.SafeNormalize(Vector2.Zero);
		Vector2 pos = proj.Center + velSafe * 16f;
		bool hitTile = false;
		if (fastProj)
		{
			while (proj.velocity.Length() >= 1f && !Collision.CanHit(proj.Center, 0, 0, proj.Center + proj.velocity, 0, 0))
			{
				proj.velocity = Vector2.Lerp(proj.velocity, proj.velocity - velSafe * 1f, 0.5f);
				hitTile = true;
			}
		}
		if (Collision.IsWorldPointSolid(pos, treatPlatformsAsNonSolid: true))
		{
			proj.velocity = Vector2.Lerp(proj.velocity, proj.velocity - velSafe * 1f, 0.5f);
			hitTile = true;
		}
		return hitTile;
	}
	public override void OnSpawn(Projectile projectile, IEntitySource source)
	{
		if ((projectile.aiStyle == ProjAIStyleID.Spear || projectile.aiStyle == ProjAIStyleID.Flail ||
			projectile.aiStyle == ProjAIStyleID.ShortSword) && Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().RubberGloves)
		{
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 4;
		}
		if (source is EntitySource_Parent parent && parent.Entity is NPC npc && npc.HasBuff(BuffID.Cursed))
		{
			projectile.Kill();
		}
	}
	public static int FindClosest(Vector2 pos, float dist, bool hostile = true)
	{
		int closest = -1;
		float last = dist;
		for (int i = 0; i < Main.projectile.Length; i++)
		{
			Projectile p = Main.projectile[i];
			if (!p.active || (hostile ? !p.hostile : p.hostile))
			{
				continue;
			}

			if (!hostile && p.type == ProjectileID.Daybreak)
			{
				continue;
			}

			if (Vector2.Distance(pos, p.Center) < last)
			{
				last = Vector2.Distance(pos, p.Center);
				closest = i;
			}
		}

		return closest;
	}

	public override void OnKill(Projectile projectile, int timeLeft)
	{
		if (projectile.type == ProjectileID.WorldGlobe)
		{
			Player player = Main.LocalPlayer;
			if (Main.netMode != NetmodeID.MultiplayerClient && player.InModBiome<Biomes.Contagion>())
			{
				int rand = Main.rand.Next(AvalonWorld.contagionBGCount);
				if (rand == AvalonWorld.contagionBG)
					rand++;
				if (rand > AvalonWorld.contagionBGCount - 1)
					rand = 0;
				AvalonWorld.contagionBG = rand;
				if (!Main.gameMenu)
				{
					AvalonWorld.contagionBGFlash = 1f;
				}
				NetMessage.SendData(MessageID.WorldData);
			}
		}
		base.OnKill(projectile, timeLeft);
	}
	public override bool CanHitPlayer(Projectile projectile, Player target)
	{
		if (target.GetModPlayer<AvalonPlayer>().TrapImmune && (ProjectileID.Sets.IsAGravestone[projectile.type] ||
			Data.Sets.ProjectileSets.TrapProjectiles[projectile.type]))
		{
			return false;
		}
		return base.CanHitPlayer(projectile, target);
	}

	public override bool PreAI(Projectile projectile)
	{
		if (projectile.type == ProjectileID.TerraBlade2 && projectile.localAI[0] == 0)
		{
			projectile.localAI[0] = 1;
			SoundEngine.PlaySound(SoundID.Item8, projectile.position);
			return true;
		}
		if (projectile.aiStyle == ProjAIStyleID.Hook)
		{
			Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
			float xpos = mountedCenter.X - projectile.Center.X;
			float ypos = mountedCenter.Y - projectile.Center.Y;
			float distance = (float)Math.Sqrt(xpos * xpos + ypos * ypos);
			float distMod = 1f;
			// for some reason checking if NOT the hookbonus bool works; also the 14 is arbitrary but ends up working to be 50% boost for each hook
			if (!Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().HookBonus && projectile.ai[2] < 14)
			{
				distMod = 1.25f;
			}
			if (projectile.ai[0] == 1 && projectile.ai[2] < 14)
			{
				if ((distance > 300f * distMod && projectile.type == ProjectileID.Hook) || (distance > 400f * distMod && projectile.type == ProjectileID.IvyWhip) ||
					(distance > 440f * distMod && projectile.type == ProjectileID.DualHookBlue) || (distance > 440f * distMod && projectile.type == ProjectileID.DualHookRed) ||
					(distance > 375f * distMod && projectile.type == ProjectileID.Web) || (distance > 350f * distMod && projectile.type == ProjectileID.SkeletronHand) ||
					(distance > 500f * distMod && projectile.type == ProjectileID.BatHook) || (distance > 550f * distMod && projectile.type == ProjectileID.WoodHook) ||
					(distance > 400f * distMod && projectile.type == ProjectileID.CandyCaneHook) || (distance > 550f * distMod && projectile.type == ProjectileID.ChristmasHook) ||
					(distance > 400f * distMod && projectile.type == ProjectileID.FishHook) || (distance > 300f * distMod && projectile.type == ProjectileID.SlimeHook) ||
					(distance > 550f * distMod && projectile.type >= ProjectileID.LunarHookSolar && projectile.type <= ProjectileID.LunarHookStardust) ||
					(distance > 600f * distMod && projectile.type == ProjectileID.StaticHook) || (distance > 300f * distMod && projectile.type == ProjectileID.SquirrelHook) ||
					(distance > 500f * distMod && projectile.type == ProjectileID.QueenSlimeHook) ||
					(distance > 480f * distMod && projectile.type >= ProjectileID.TendonHook && projectile.type <= ProjectileID.WormHook) ||
					(distance > 500f * distMod && projectile.type == ProjectileID.AntiGravityHook))
				{
					projectile.ai[0] = 0;
					projectile.ai[2]++;
					if (projectile.ai[2] >= 14)
					{
						projectile.ai[0] = 1;
					}
				}
				else if (projectile.type >= ProjectileID.GemHookAmethyst && projectile.type <= ProjectileID.GemHookDiamond)
				{
					int num18 = 300 + (projectile.type - 230) * 30;
					num18 = (int)(num18 * distMod);
					if (distance > (float)num18)
					{
						projectile.ai[0] = 0;
						projectile.ai[2]++;
						if (projectile.ai[2] >= 14)
						{
							projectile.ai[0] = 1;
						}
					}
				}
				else if (projectile.type == ProjectileID.AmberHook)
				{
					int num19 = (int)(420 * distMod - 420);
					if (distance > num19)
					{
						projectile.ai[0] = 0;
						projectile.ai[2]++;
						if (projectile.ai[2] >= 14)
						{
							projectile.ai[0] = 1;
						}
					}
				}
			}
		}
		return base.PreAI(projectile);
	}
	public override void PostAI(Projectile projectile)
	{
		if ((projectile.type != ProjectileID.PurificationPowder && projectile.type != ProjectileID.PureSpray) || projectile.owner != Main.myPlayer)
		{
			return;
		}
		int num = (int)(projectile.Center.X / 16f);
		int num2 = (int)(projectile.Center.Y / 16f);
		bool flag = projectile.type == ProjectileID.PurificationPowder;
		for (int i = num - 1; i <= num + 1; i++)
		{
			for (int j = num2 - 1; j <= num2 + 1; j++)
			{
				if (projectile.type == ProjectileID.PureSpray || projectile.type == ProjectileID.PurificationPowder)
				{
					AvalonWorld.ConvertFromThings(i, j, 0, !flag);
				}
				//if (projectile.type == ModContent.ProjectileType<Projectiles.LimeSolution>())
				//{
				//    AvalonWorld.ConvertFromThings(i, j, 1, !flag);
				//}
				NetMessage.SendTileSquare(-1, i, j, 1, 1);
			}
		}
	}
	public override void AI(Projectile projectile)
	{
		#region fertilizer fix
		if (projectile.aiStyle == ProjAIStyleID.Powder)
		{
			bool flag23 = projectile.type == ProjectileID.Fertilizer;
			bool flag34 = Main.myPlayer == projectile.owner;
			if (flag23)
			{
				flag34 = Main.netMode != NetmodeID.MultiplayerClient;
			}
			if (flag34 && flag23)
			{
				int num988 = (int)(projectile.position.X / 16f) - 1;
				int num999 = (int)((projectile.position.X + projectile.width) / 16f) + 2;
				int num1010 = (int)(projectile.position.Y / 16f) - 1;
				int num1021 = (int)((projectile.position.Y + projectile.height) / 16f) + 2;
				if (num988 < 0)
				{
					num988 = 0;
				}
				if (num999 > Main.maxTilesX)
				{
					num999 = Main.maxTilesX;
				}
				if (num1010 < 0)
				{
					num1010 = 0;
				}
				if (num1021 > Main.maxTilesY)
				{
					num1021 = Main.maxTilesY;
				}
				Vector2 vector57 = default;
				for (int num1032 = num988; num1032 < num999; num1032++)
				{
					for (int num1043 = num1010; num1043 < num1021; num1043++)
					{
						vector57.X = num1032 * 16;
						vector57.Y = num1043 * 16;
						if (!(projectile.position.X + projectile.width > vector57.X) || !(projectile.position.X < vector57.X + 16f) || !(projectile.position.Y + projectile.height > vector57.Y) || !(projectile.position.Y < vector57.Y + 16f) || !Main.tile[num1032, num1043].HasTile)
						{
							continue;
						}
						Tile tile = Main.tile[num1032, num1043];
						if (tile.TileType == ModContent.TileType<ContagionSapling>())
						{
							//if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
							//{
							//    ContagionSapling.AttemptToGrowContagionTreeFromSapling(num1032, num1043);
							//}
							ContagionSapling.AttemptToGrowContagionTreeFromSapling(num1032, num1043);
						}

						if (tile.TileType == ModContent.TileType<TourmalineSapling>())
						{
							if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
							{
								TourmalineSapling.AttemptToGrowTourmalineFromSapling(num1032, num1043, underground: false);
							}
							TourmalineSapling.AttemptToGrowTourmalineFromSapling(num1032, num1043, num1043 > (int)Main.worldSurface - 1);
						}
						if (tile.TileType == ModContent.TileType<PeridotSapling>())
						{
							if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
							{
								PeridotSapling.AttemptToGrowPeridotFromSapling(num1032, num1043, underground: false);
							}
							PeridotSapling.AttemptToGrowPeridotFromSapling(num1032, num1043, num1043 > (int)Main.worldSurface - 1);
						}
						if (tile.TileType == ModContent.TileType<ZirconSapling>())
						{
							if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
							{
								ZirconSapling.AttemptToGrowZirconFromSapling(num1032, num1043, underground: false);
							}
							ZirconSapling.AttemptToGrowZirconFromSapling(num1032, num1043, num1043 > (int)Main.worldSurface - 1);
						}
					}
				}
			}
		}
		#endregion
		#region terra blade sound change
		if (projectile.type == ProjectileID.TerraBlade2)
		{
			if (projectile.localAI[0] == 1)
			{
				projectile.localAI[0] = 2;
				SoundEngine.PlaySound(SoundID.Item8, projectile.position);
			}
		}
		#endregion
		if (projectile.type == ProjectileID.PaladinsHammerFriendly)
		{
			if (Main.timeForVisualEffects % 2 == 0 && projectile.ai[1] != 0 && projectile.timeLeft > 3590)
			{
				ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
				particleOrchestraSettings.PositionInWorld = projectile.Center;
				particleOrchestraSettings.MovementVector = projectile.velocity * 1f;
				ParticleOrchestraSettings settings = particleOrchestraSettings;
				ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PaladinsHammer, settings, projectile.owner);
			}
		}
	}
	public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (projectile.aiStyle == ProjAIStyleID.Spear || projectile.aiStyle == ProjAIStyleID.Drill || projectile.type == ModContent.ProjectileType<HallowedClaymoreProj>() ||
			projectile.type == ModContent.ProjectileType<MarrowMasherProj>() || projectile.type == ModContent.ProjectileType<CraniumCrusherProj>() ||
			projectile.type == ModContent.ProjectileType<UrchinMaceProj>() || projectile.type == ModContent.ProjectileType<WoodenClubProj>() ||
			((projectile.type == ModContent.ProjectileType<CaesiumMaceProj>() || projectile.type == ModContent.ProjectileType<SporalashProj>() ||
			projectile.type == ModContent.ProjectileType<Cell>()) && projectile.ai[0] == 0) || (projectile.aiStyle == ProjAIStyleID.Flail && projectile.ai[0] == 0))
		{
			if (projectile.Owner().GetModPlayer<AvalonPlayer>().BloodyWhetstone)
			{
				if (!target.HasBuff<Lacerated>())
				{
					target.GetGlobalNPC<AvalonGlobalNPCInstance>().LacerateStacks = 1;
				}
				target.AddBuff(ModContent.BuffType<Lacerated>(), 120);
			}
			if (projectile.Owner().GetModPlayer<AvalonPlayer>().VampireTeeth)
			{
				if (target.boss)
				{
					projectile.Owner().VampireHeal(hit.Damage / 2, target.Center);
				}
				else
				{
					projectile.Owner().VampireHeal(hit.Damage, target.Center);
				}
			}
		}
		base.OnHitNPC(projectile, target, hit, damageDone);
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[ProjectileID.DemonSickle] = 6;
		ProjectileID.Sets.TrailingMode[ProjectileID.DemonSickle] = 2;
		ProjectileID.Sets.TrailCacheLength[ProjectileID.DemonScythe] = 6;
		ProjectileID.Sets.TrailingMode[ProjectileID.DemonScythe] = 2;
	}
	public static Vector2 RotateAboutOrigin(Vector2 point, float rotation)
	{
		if (rotation < 0f)
		{
			rotation += 12.566371f;
		}

		Vector2 value = point;
		if (value == Vector2.Zero)
		{
			return point;
		}

		float num = (float)Math.Atan2(value.Y, value.X);
		num += rotation;
		return value.Length() * new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
	}
}
