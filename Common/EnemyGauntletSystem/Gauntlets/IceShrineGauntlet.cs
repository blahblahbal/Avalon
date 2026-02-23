using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Avalon.Common.EnemyGauntletSystem.Gauntlets;

public class IceShrineGauntlet : EnemyGauntlet
{
	public override Vector2 CircleRadius => new Vector2(30,20) * 16;
	public override int TotalRounds => 3;
	public override EnemyGauntletMonsterType[] Monsters =>
		[
		new EnemyGauntletMonsterType(NPCID.ZombieEskimo, 1f, GaunletSpawnRule.Grounded, 1, 2, maxRound: 3),
		new EnemyGauntletMonsterType(NPCID.IceSlime, 1f, GaunletSpawnRule.Grounded, 2, 3, maxRound: 3),
		new EnemyGauntletMonsterType(NPCID.SpikedIceSlime, 0.3f, GaunletSpawnRule.Grounded, 1, 1, maxRound: 3),
		new EnemyGauntletMonsterType(NPCID.SpikedIceSlime, 0.3f, GaunletSpawnRule.Grounded, 1, 1, 2, maxRound: 3),
		new EnemyGauntletMonsterType(NPCID.IceElemental, 1f, GaunletSpawnRule.EmptySpace, 1, 1, 3),
		];

	public override void Victory()
	{
		Main.tile[PointOfInterest].TileFrameX -= 36;
		Main.tile[PointOfInterest + new Point(1,0)].TileFrameX -= 36;
		Main.tile[PointOfInterest + new Point(0, 1)].TileFrameX -= 36;
		Main.tile[PointOfInterest + new Point(1, 1)].TileFrameX -= 36;
	}
}
