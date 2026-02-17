using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.EnemyGauntletSystem;

public enum GaunletSpawnRule
{
	EmptySpace,
	Grounded,
	InFloor
}
public struct EnemyGauntletMonsterType
{
	public EnemyGauntletMonsterType(int enemy, float weight, GaunletSpawnRule spawnRule, int minAmount = 3, int maxAmount = 6, int minRound = 0, int maxRound = 99)
	{
		Enemy = enemy;
		Weight = weight;
		SpawnRule = spawnRule;
		MaxAmount = maxAmount;
		MinAmount = minAmount;
		MinRound = minRound;
		MaxRound = maxRound;
	}

	/// <summary>
	/// the ID of the enemy this spawns
	/// </summary>
	public int Enemy { get; }
	/// <summary>
	/// How rare the enemy is. Use lower numbers for stronger enemies
	/// </summary>
	public float Weight { get; }
	/// <summary>
	/// The spawning rule for this enemy
	/// </summary>
	public GaunletSpawnRule SpawnRule { get; }
	/// <summary>
	/// Minimum round this enemy can spawn on
	/// </summary>
	public int MinRound { get; }
	/// <summary>
	/// Maximum round this enemy can spawn on
	/// </summary>
	public int MaxRound { get; }
	/// <summary>
	/// Max amount of this enemy
	/// </summary>
	public int MaxAmount { get; }
	/// <summary>
	/// minimum amount of this enemy
	/// </summary>
	public int MinAmount { get; }

	public bool FindSpawnLocation(Rectangle area, out List<Point> Points)
	{
		List<Point> validPoints = new();

		NPC n = ContentSamples.NpcsByNetId[Enemy];
		for(int x = area.Left; x < area.Right; x++)
		{
			for (int y = area.Top; y < area.Bottom; y++)
			{
				switch (SpawnRule)
				{
					case GaunletSpawnRule.EmptySpace:
						if (!Main.tileSolid[Main.tile[x, y].TileType] || !Main.tile[x, y].HasTile)
						{
							validPoints.Add(new Point(x, y));
						}
						break;
					case GaunletSpawnRule.Grounded:
						if (Main.tileSolid[Main.tile[x, y].TileType] && Main.tile[x, y].HasTile && (!Main.tileSolid[Main.tile[x, y - 1].TileType] || !Main.tile[x, y - 1].HasTile))
						{
							if (!Collision.SolidCollision(new Point(x, y).ToWorldCoordinates(-ContentSamples.NpcsByNetId[Enemy].width / 2, -ContentSamples.NpcsByNetId[Enemy].height), ContentSamples.NpcsByNetId[Enemy].width, ContentSamples.NpcsByNetId[Enemy].height))
								validPoints.Add(new Point(x, y));
						}
						break;
					case GaunletSpawnRule.InFloor:
						if (Main.tileSolid[Main.tile[x, y].TileType] && Main.tile[x, y].HasTile)
						{
							validPoints.Add(new Point(x, y));
						}
						break;
				}
			}
		}
		//if (!n.noTileCollide)
		//{
		//	for (int i = 0; i < validPoints.Count; i++)
		//	{
		//		if (Collision.SolidCollision(validPoints[i].ToWorldCoordinates()))
		//	}
		//}

		Points = validPoints;

		if(validPoints.Count > 0)
		{
			return true;
		}
		return false;
	}
}
public abstract class EnemyGauntlet : ModType
{
	public bool ShouldBeDisabled = false;
	public Rectangle Area;
	public virtual int TotalRounds => 5;
	public int Round;
	public int BetweenRoundCountdown;
	public bool RoundIsInProgress;
	public Point PointOfInterest;
	public virtual EnemyGauntletMonsterType[] Monsters => [];
	protected sealed override void Register()
	{
		ModTypeLookup<EnemyGauntlet>.Register(this);
	}
	public void Begin(Rectangle area, Point POI)
	{
		PointOfInterest = POI;
		Area = area;
		BetweenRoundCountdown = 60;
		Round = 0;
		EnemyGauntletSystem.ActiveGauntlets.Add(this);
		Main.NewText("Started Event");
	}
	public void StartRound()
	{
		int portalType = ModContent.NPCType<EnemyGauntletPortal>();
		Round++;
		if (Round > TotalRounds)
		{
			Main.NewText("Victory!");
			ShouldBeDisabled = true;
			return;
		}
		BetweenRoundCountdown = 40;
		Main.NewText("Started Round " + $"{Round}");
		for (int i = 0; i < Monsters.Length; i++)
		{
			if (Round < Monsters[i].MinRound || Round > Monsters[i].MaxRound || Main.rand.NextFloat() > Monsters[i].Weight)
				continue;

			if(!Monsters[i].FindSpawnLocation(Area, out var Points))
				continue;

			int rand = Main.rand.Next(Monsters[i].MinAmount, Monsters[i].MaxAmount);
			for (int x = 0; x < rand; x++)
			{
				int r = Main.rand.Next(Points.Count);
				NPC n = NPC.NewNPCDirect(NPC.GetSource_None(), Points[r].ToWorldCoordinates(0, 0), portalType, 0, Monsters[i].Enemy);
				Points.RemoveAt(r);
				n.GetGlobalNPC<EnemyGauntletNPC>().AssignedGauntlet = this;
				//NPC n = NPC.NewNPCDirect(NPC.GetSource_None(), Points[Main.rand.Next(Points.Count)].ToWorldCoordinates(0,0), Monsters[i].Enemy);
				//n.GetGlobalNPC<EnemyGauntletNPC>().AssignedGauntlet = this;
			}
		}
	}
	public void Update()
	{
		RoundIsInProgress = false;
		foreach (NPC n in Main.ActiveNPCs) // Determine if any enemies are alive
		{
			if(n.GetGlobalNPC<EnemyGauntletNPC>().AssignedGauntlet == this)
			{
				RoundIsInProgress = true;
				break;
			}
		}
		if (!RoundIsInProgress) // Coundown to start the next round
		{
			Main.NewText(BetweenRoundCountdown);
			BetweenRoundCountdown--;
			if (BetweenRoundCountdown <= 0)
			{
				StartRound();
			}
		}
	}
}
public class EnemyGauntletSystem : ModSystem
{
	public static List<EnemyGauntlet> ActiveGauntlets = new List<EnemyGauntlet>();
	public override void PreUpdateInvasions()
	{
		for(int i = 0; i < ActiveGauntlets.Count; i++)
		{
			ActiveGauntlets[i].Update();
			if (ActiveGauntlets[i].ShouldBeDisabled)
			{
				ActiveGauntlets.RemoveAt(i);
				i--;
			}
		}
	}
}

public class EnemyGauntletPortal : ModNPC
{
	public override string Texture => $"Terraria/Images/Extra_50";
	public override void SetDefaults()
	{
		NPC.width = NPC.height = 1;
		NPC.noTileCollide = true;
		NPC.hide = true;
		NPC.dontTakeDamage = true;
		NPC.noGravity = true;
		NPC.lifeMax = 99;
		NPC.HideStrikeDamage = true;
	}
	public override void AI()
	{
		NPC n = ContentSamples.NpcsByNetId[(int)NPC.ai[0]];
		if (NPC.ai[2] == 0)
		{
			ParticleSystem.NewParticle(new EnemyGauntletFlame(), NPC.Center + new Vector2(0, n.height / -2) + Main.rand.NextVector2Circular(6,6));
		}
		NPC.ai[2]++;
		if (NPC.ai[2] > 40)
		{
			int dust = ModContent.DustType<SimpleColorableGlowyDust>();
			for(int i = 0; i < 10; i++)
			{
				Dust d = Dust.NewDustPerfect(NPC.Center + new Vector2(0, n.height / -2), dust,Main.rand.NextVector2Circular(n.height,n.width) * 0.2f);
				d.noGravity = true;
				d.color = new Color(133, 102, 255,0);
				d.scale *= Main.rand.NextFloat(1f, 2f);
			}
			NPC.StrikeInstantKill();
		}
	}
	public override void OnKill()
	{
		NPC n = NPC.NewNPCDirect(NPC.GetSource_Misc(""), NPC.Bottom, (int)NPC.ai[0]);
		n.GetGlobalNPC<EnemyGauntletNPC>().AssignedGauntlet = NPC.GetGlobalNPC<EnemyGauntletNPC>().AssignedGauntlet;
		if (Main.netMode == NetmodeID.Server && n.whoAmI < 200)
		{
			NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n.whoAmI);
		}
	}
}
public class EnemyGauntletNPC : GlobalNPC
{
	public override bool InstancePerEntity => true;
	public EnemyGauntlet AssignedGauntlet = null;
}
public class TestGauntlet : EnemyGauntlet
{
	public override EnemyGauntletMonsterType[] Monsters => 
		[
		new EnemyGauntletMonsterType(NPCID.BlueSlime, 1f, GaunletSpawnRule.Grounded, maxRound: 3),
		new EnemyGauntletMonsterType(NPCID.Zombie, 1f, GaunletSpawnRule.Grounded),
		new EnemyGauntletMonsterType(NPCID.DemonEye, 1f, GaunletSpawnRule.Grounded),
		new EnemyGauntletMonsterType(NPCID.Skeleton, 0.3f, GaunletSpawnRule.Grounded, 2, 3, 2),
		new EnemyGauntletMonsterType(NPCID.Skeleton, 0.6f, GaunletSpawnRule.Grounded, 2, 3, 3),
		new EnemyGauntletMonsterType(NPCID.Skeleton, 1f, GaunletSpawnRule.Grounded, 2, 3, 4),
		];
}
