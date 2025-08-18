using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.Furniture;
using Avalon.Tiles.Savanna;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Systems;

public class BiomeTileCounts : ModSystem
{
	public const int DarkMatterTilesHardLimit = 250000;

	public int WorldDarkMatterTiles;
	public int ContagionTiles { get; private set; }
	public int SavannaTiles { get; private set; }
	public int HellCastleTiles { get; private set; }
	public int DarkTiles { get; private set; }
	public int CaesiumTiles { get; private set; }
	public int SkyFortressTiles { get; private set; }
	public int CrystalTiles { get; private set; }
	public int DungeonAltTiles { get; private set; }
	public int DarkMonolithTiles { get; private set; }
	public int ContagionDesertTiles { get; private set; }

	public int AshenOvergrowthTiles { get; private set; }

	public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
	{
		HellCastleTiles = tileCounts[ModContent.TileType<ImperviousBrick>()];

		ContagionTiles = tileCounts[ModContent.TileType<Chunkstone>()] +
						 tileCounts[ModContent.TileType<HardenedSnotsand>()] +
						 tileCounts[ModContent.TileType<Snotsandstone>()] +
						 tileCounts[ModContent.TileType<Ickgrass>()] +
						 tileCounts[ModContent.TileType<ContagionJungleGrass>()] +
						 tileCounts[ModContent.TileType<Snotsand>()] +
						 tileCounts[ModContent.TileType<YellowIce>()];
		DarkMonolithTiles = tileCounts[ModContent.TileType<Tiles.DarkMatter.DarkMatterMonolith>()];
		AshenOvergrowthTiles = tileCounts[TileID.AshGrass] +
							   tileCounts[TileID.AshPlants];

		Main.SceneMetrics.GraveyardTileCount += tileCounts[ModContent.TileType<GiantGravestone>()] * 7;

		Main.SceneMetrics.JungleTileCount += tileCounts[ModContent.TileType<GreenIce>()];
		Main.SceneMetrics.SnowTileCount += tileCounts[ModContent.TileType<GreenIce>()];
		Main.SceneMetrics.SandTileCount += tileCounts[ModContent.TileType<Snotsand>()] + tileCounts[ModContent.TileType<HardenedSnotsand>()] + tileCounts[ModContent.TileType<Snotsandstone>()];

		ContagionDesertTiles = tileCounts[ModContent.TileType<Snotsand>()] + tileCounts[ModContent.TileType<HardenedSnotsand>()] + tileCounts[ModContent.TileType<Snotsandstone>()];


		SavannaTiles = tileCounts[ModContent.TileType<SavannaStone>()] +
					   tileCounts[ModContent.TileType<TuhrtlBrick>()] +
					   tileCounts[ModContent.TileType<Loam>()] +
					   tileCounts[ModContent.TileType<SavannaGrass>()];
		/*
        DarkTiles = tileCounts[ModContent.TileType<DarkMatter>()] +
                    tileCounts[ModContent.TileType<DarkMatterSand>()] +
                    tileCounts[ModContent.TileType<BlackIce>()] +
                    tileCounts[ModContent.TileType<DarkMatterSoil>()] +
                    tileCounts[ModContent.TileType<HardenedDarkSand>()] +
                    tileCounts[ModContent.TileType<Darksandstone>()] +
                    tileCounts[ModContent.TileType<DarkMatterGrass>()];*/
		DungeonAltTiles = tileCounts[ModContent.TileType<OrangeBrick>()] +
			tileCounts[ModContent.TileType<PurpleBrick>()] +
			tileCounts[ModContent.TileType<YellowBrick>()] +
			tileCounts[ModContent.TileType<CrackedYellowBrick>()] +
			tileCounts[ModContent.TileType<CrackedOrangeBrick>()] +
			tileCounts[ModContent.TileType<CrackedPurpleBrick>()];
		CaesiumTiles = tileCounts[ModContent.TileType<BlastedStone>()];
		SkyFortressTiles = tileCounts[ModContent.TileType<SkyBrick>()];
		//CrystalTiles = tileCounts[ModContent.TileType<CrystalStone>()];
		//DarkMonolithTiles = tileCounts[ModContent.TileType<DarkMatterMonolith>()];
		//Main.LocalPlayer.GetModPlayer<ExxoBiomePlayer>().UpdateZones(this);
	}

	public bool BasaltObeliskNearby;
	public bool DarkMatterMonolithNearby;
	public bool DelightCandleNearby;
	public bool FlightCandleNearby;
	public bool FrightCandleNearby;
	public bool IceCandleNearby;
	public bool LightCandleNearby;
	public bool MightCandleNearby;
	public bool NightCandleNearby;
	public bool SightCandleNearby;
	public override void ResetNearbyTileEffects()
	{
		BasaltObeliskNearby = false;
		DarkMatterMonolithNearby = false;
		DelightCandleNearby = false;
		FlightCandleNearby = false;
		FrightCandleNearby = false;
		IceCandleNearby = false;
		LightCandleNearby = false;
		MightCandleNearby = false;
		NightCandleNearby = false;
		SightCandleNearby = false;
	}
	public static bool NearbyEffectsRectangle(int i, int j, int xRad, int yRad)
	{
		Point pos = Main.LocalPlayer.Center.ToTileCoordinates();
		return (i + xRad >= pos.X && i - xRad <= pos.X && j + yRad >= pos.Y && j - yRad <= pos.Y);
	}

	public override void SaveWorldData(TagCompound tag)
	{
		tag["WorldDarkMatterTiles"] = WorldDarkMatterTiles;
	}

	public override void LoadWorldData(TagCompound tag)
	{
		if (tag.ContainsKey("WorldDarkMatterTiles"))
		{
			WorldDarkMatterTiles = tag.Get<int>("WorldDarkMatterTiles");
		}
	}
}
