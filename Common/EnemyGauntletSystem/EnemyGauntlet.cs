using Avalon.Buffs;
using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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

	public bool FindSpawnLocations(List<Point> PointsToCheck,out List<Point> Points)
	{
		List<Point> validPoints = new();

		NPC n = ContentSamples.NpcsByNetId[Enemy];
		for(int i = 0; i < PointsToCheck.Count; i++)
		{
			int x = PointsToCheck[i].X;	
			int y = PointsToCheck[i].Y;
			switch (SpawnRule)
			{
				case GaunletSpawnRule.EmptySpace:
					if (!Main.tileSolid[Main.tile[x, y].TileType] || !Main.tile[x, y].HasTile)
					{
						validPoints.Add(new Point(x, y));
					}
					break;
				case GaunletSpawnRule.Grounded:
					y = PointsToCheck[i].Y + 1;
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
	public virtual Vector2 CircleRadius => new Vector2(15 * 16, 10 * 16);
	public virtual int TotalRounds => 5;
	public virtual EnemyGauntletMonsterType[] Monsters => [];


	public bool ShouldBeDisabled = false;
	public Rectangle Area;
	public int Round;
	public int BetweenRoundCountdown;
	public bool RoundIsInProgress;
	public Point PointOfInterest;
	public int WhoAmI;
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
		bool addedSuccessfully = false;
		for (int i = 0; i < EnemyGauntletSystem.Gauntlets.Length; i++)
		{
			if(EnemyGauntletSystem.Gauntlets[i] == null)
			{
				addedSuccessfully = true;
				this.WhoAmI = i;
				EnemyGauntletSystem.Gauntlets[i] = this;
				break;
			}
		}
		if (!addedSuccessfully)
		{
			Main.NewText("Wait for another enemy gauntlet to end before starting this one.", new Color(200,32,255));
		}
	}
	private void Search(Rectangle area, Point start, ref List<Point> validPoints)
	{
		if (start.X < area.Left || start.X > area.Right || start.Y < area.Top || start.Y > area.Bottom) return;
		if (validPoints.Contains(start)) return;
		if ((Main.tileSolid[Main.tile[start].TileType] || TileID.Sets.RoomNeeds.CountsAsDoor.Contains<int>(Main.tile[start].TileType)) && Main.tile[start].HasTile) return;
		validPoints.Add(start);

		Search(area, start + new Point(1, 0), ref validPoints);
		Search(area, start + new Point(-1, 0), ref validPoints);
		Search(area, start + new Point(0, 1), ref validPoints);
		Search(area, start + new Point(0, -1), ref validPoints);
	}
	private List<Point> FloodFillPointsInArea(Rectangle area, Point start)
	{
		List<Point> points = new List<Point>();
		Search(area, start, ref points);
		return points;
	}
	public virtual void Victory()
	{
		Main.NewText("Victory!");
	}
	private void StartRound()
	{
		int portalType = ModContent.NPCType<EnemyGauntletPortal>();
		Round++;
		if (Round > TotalRounds)
		{
			Victory();
			ShouldBeDisabled = true;
			return;
		}
		BetweenRoundCountdown = 40;
		Main.NewText("Started Round " + $"{Round}");
		List<Point> ValidPoints = FloodFillPointsInArea(Area, PointOfInterest);
		for (int i = 0; i < Monsters.Length; i++)
		{
			if (Round < Monsters[i].MinRound || Round > Monsters[i].MaxRound || Main.rand.NextFloat() > Monsters[i].Weight)
				continue;

			if(!Monsters[i].FindSpawnLocations(ValidPoints, out var Points))
				continue;



			int rand = Main.rand.Next(Monsters[i].MinAmount, Monsters[i].MaxAmount);
			for (int x = 0; x < rand; x++)
			{
				int r = Main.rand.Next(Points.Count);
				NPC n = NPC.NewNPCDirect(NPC.GetSource_None(), Points[r].ToWorldCoordinates(0, 0), portalType, 0, Monsters[i].Enemy);
				Points.RemoveAt(r);
				n.GetGlobalNPC<EnemyGauntletNPC>().AssignedGauntlet = WhoAmI;
			}
		}
	}
	public void Update()
	{
		RoundIsInProgress = false;
		foreach (NPC n in Main.ActiveNPCs) // Determine if any enemies are alive
		{
			if(n.GetGlobalNPC<EnemyGauntletNPC>().AssignedGauntlet == WhoAmI)
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
	public bool IsPlayerInside(Player player)
	{
		Vector2 center = PointOfInterest.ToWorldCoordinates(16, 16);
		Vector2 diff = player.Hitbox.ClosestPointInRect(center) - center;
		if ((diff / Vector2.Normalize(CircleRadius)).Length() < MathHelper.Max(CircleRadius.X, CircleRadius.Y))
		{
			return true;
		}
		return false;
	}
}
public class EnemyGauntletSystem : ModSystem
{
	public static EnemyGauntlet[] Gauntlets = new EnemyGauntlet[5];
	public override void PreUpdateInvasions()
	{
		for(int i = 0; i < Gauntlets.Length; i++)
		{
			if (Gauntlets[i] == null)
				continue;
			Gauntlets[i].Update();
			if (Gauntlets[i].ShouldBeDisabled)
			{
				foreach(Player p in Main.ActivePlayers)
				{
					EnemyGauntletPlayer egp = p.GetModPlayer<EnemyGauntletPlayer>();
					if (egp.AssignedGauntlet == Gauntlets[i].WhoAmI)
						egp.AssignedGauntlet = -1;
				}
				Gauntlets[i] = null;
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
	public int AssignedGauntlet = -1;

	public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
	{
		binaryWriter.Write((short)AssignedGauntlet);
	}
	public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
	{
		AssignedGauntlet = binaryReader.ReadInt16();
	}
}
public class EnemyGauntletPlayer : ModPlayer
{
	public int AssignedGauntlet = -1;
	public override void ResetEffects()
	{
		for(int i = 0; i < EnemyGauntletSystem.Gauntlets.Length; i++)
		{
			if (EnemyGauntletSystem.Gauntlets[i] != null && EnemyGauntletSystem.Gauntlets[i].IsPlayerInside(Player))
			{
				AssignedGauntlet = i;
				break;
			}
		}
		if (AssignedGauntlet > -1)
		{
			EnemyGauntlet gauntlet = EnemyGauntletSystem.Gauntlets[AssignedGauntlet];
			if (gauntlet == null)
			{
				AssignedGauntlet = -1;
				return;
			}
			if (gauntlet.IsPlayerInside(Player))
			{
				Player.AddBuff(BuffID.NoBuilding, 2, true);
			}
			else
			{
				Player.AddBuff(BuffID.WitheredWeapon, 2, true);
				Player.lifeRegen -= 50;
			}
		}
	}
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
