using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Tiles.GemTrees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Tiles.Contagion;
using Avalon.NPCs.Bosses.PreHardmode;
using Avalon.Projectiles.Melee;

namespace Avalon.Common;

internal class AvalonGlobalProjectile : GlobalProjectile
{
	public static void ModifyProjectileStats(Projectile p, int ownedCounts, int origDmg, int dmgMod, float origScale, float scaleMod)
	{
		p.damage = (int)Main.player[p.owner].GetDamage(DamageClass.Summon).ApplyTo(origDmg);
		p.damage += Main.player[p.owner].ownedProjectileCounts[ownedCounts] * dmgMod;
		p.scale = origScale;
		p.scale += scaleMod * Main.player[p.owner].ownedProjectileCounts[ownedCounts];
		if (Main.player[p.owner].ownedProjectileCounts[ownedCounts] > 7)
		{
			p.frame = 2;
			p.scale = origScale;
			if (Main.player[p.owner].ownedProjectileCounts[ownedCounts] < 10)
			{
				p.scale += scaleMod * (Main.player[p.owner].ownedProjectileCounts[ownedCounts] - 7);
			}
			else
			{
				p.scale += scaleMod * 2;
			}

		}
		else if (Main.player[p.owner].ownedProjectileCounts[ownedCounts] > 4)
		{
			p.frame = 1;
			p.scale = origScale;
			p.scale += scaleMod * (Main.player[p.owner].ownedProjectileCounts[ownedCounts] - 4);
		}
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
		if (projectile.type == ProjectileID.WorldGlobe && Main.player[projectile.owner].InModBiome<Biomes.Contagion>())
		{
			ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG++;
			if (ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG > 1)
			{
				ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG = 0;
			}
		}
		base.OnKill(projectile, timeLeft);
	}
	public override bool CanHitPlayer(Projectile projectile, Player target)
	{
		if (target.GetModPlayer<AvalonPlayer>().TrapImmune && (ProjectileID.Sets.IsAGravestone[projectile.type] ||
			Data.Sets.Projectile.TrapProjectiles[projectile.type]))
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
		if (projectile.aiStyle == 7)
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
		if ((projectile.type != 10 && projectile.type != 145 /* && projectile.type != ModContent.ProjectileType<Projectiles.LimeSolution>()*/) || projectile.owner != Main.myPlayer)
		{
			return;
		}
		int num = (int)(projectile.Center.X / 16f);
		int num2 = (int)(projectile.Center.Y / 16f);
		bool flag = projectile.type == 10;
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
		if (projectile.aiStyle == 6)
		{
			bool flag23 = projectile.type == 1019;
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
			if(Main.timeForVisualEffects % 2 == 0 && projectile.ai[1] != 0 && projectile.timeLeft > 3590)
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
		if (projectile.aiStyle == ProjAIStyleID.Spear || projectile.aiStyle == ProjAIStyleID.Drill || projectile.type == ModContent.ProjectileType<HallowedClaymore>() ||
			projectile.type == ModContent.ProjectileType<MarrowMasher>() || projectile.type == ModContent.ProjectileType<CraniumCrusher>() ||
			projectile.type == ModContent.ProjectileType<UrchinMace>() || projectile.type == ModContent.ProjectileType<WoodenClub>() ||
			((projectile.type == ModContent.ProjectileType<CaesiumMace>() || projectile.type == ModContent.ProjectileType<Sporalash>() ||
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
		ProjectileID.Sets.TrailCacheLength[44] = 6;
		ProjectileID.Sets.TrailingMode[44] = 2;
		ProjectileID.Sets.TrailCacheLength[45] = 6;
		ProjectileID.Sets.TrailingMode[45] = 2;
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
