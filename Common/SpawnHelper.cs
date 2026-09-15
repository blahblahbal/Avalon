using Avalon.Biomes;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

public class SpawnHelper
{
	public static bool Evil(ref NPCSpawnInfo spawnInfo)
	{
		return Contagion(ref spawnInfo) || Crimson(ref spawnInfo) || Corruption(ref spawnInfo);
	}
	public static bool Contagion(ref NPCSpawnInfo spawnInfo)
	{
		return !spawnInfo.Player.InPillarZone() && ((spawnInfo.Player.InModBiome<Contagion>() && spawnInfo.SpawnTileType == ModContent.TileType<BacciliteOre>())
			|| spawnInfo.SpawnTileType == ModContent.TileType<Ickgrass>()
			|| spawnInfo.SpawnTileType == ModContent.TileType<ContagionJungleGrass>()
			|| spawnInfo.SpawnTileType == ModContent.TileType<Chunkstone>()
			|| spawnInfo.SpawnTileType == ModContent.TileType<Snotsand>()
			|| spawnInfo.SpawnTileType == ModContent.TileType<YellowIce>());
	}
	public static bool Crimson(ref NPCSpawnInfo spawnInfo)
	{
		return !spawnInfo.Player.InPillarZone() && ((spawnInfo.Player.ZoneCrimson && spawnInfo.SpawnTileType == TileID.Crimtane)
			|| spawnInfo.SpawnTileType == TileID.CrimsonGrass
			|| spawnInfo.SpawnTileType == TileID.CrimsonJungleGrass
			|| spawnInfo.SpawnTileType == TileID.Crimstone
			|| spawnInfo.SpawnTileType == TileID.Crimsand
			|| spawnInfo.SpawnTileType == TileID.FleshIce);
	}
	public static bool Corruption(ref NPCSpawnInfo spawnInfo)
	{
		return !spawnInfo.Player.InPillarZone() && ((spawnInfo.Player.ZoneCorrupt && spawnInfo.SpawnTileType == TileID.Demonite)
			|| spawnInfo.SpawnTileType == TileID.CorruptGrass
			|| spawnInfo.SpawnTileType == TileID.CorruptJungleGrass
			|| spawnInfo.SpawnTileType == TileID.Ebonstone
			|| spawnInfo.SpawnTileType == TileID.Ebonsand
			|| spawnInfo.SpawnTileType == TileID.CorruptIce);
	}
	public static bool Hallow(ref NPCSpawnInfo spawnInfo)
	{
		return !spawnInfo.Player.InPillarZone() && Main.hardMode &&
			(spawnInfo.SpawnTileType == TileID.HallowedGrass
			|| spawnInfo.SpawnTileType == TileID.HallowedIce
			|| spawnInfo.SpawnTileType == TileID.Pearlstone
			|| spawnInfo.SpawnTileType == TileID.Pearlsand);
	}
	public static bool Underground(ref NPCSpawnInfo spawnInfo)
	{
		return spawnInfo.SpawnTileY > Main.worldSurface && !spawnInfo.Player.ZoneDungeon && !spawnInfo.Player.InPillarZone();
	}
	public static bool RockLayer(ref NPCSpawnInfo spawnInfo)
	{
		return spawnInfo.SpawnTileY > Main.rockLayer && !spawnInfo.Player.ZoneDungeon && !spawnInfo.Player.InPillarZone();
	}
	public static bool NoWorms(ref NPCSpawnInfo spawnInfo)
	{
		return spawnInfo.SpawnTileY > Main.worldSurface;
	}
}
