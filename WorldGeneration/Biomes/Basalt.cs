using Avalon.Tiles;
using Avalon.Tiles.Savanna;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.WorldGeneration.Biomes;

internal class Basalt
{
	private static readonly int[] blacklistedTiles = new int[] { 225, 41, 43, 44, 226, 203, 112, 25, 151, ModContent.TileType<TuhrtlBrick>(),
			ModContent.TileType<OrangeBrick>(), ModContent.TileType<PurpleBrick>(), ModContent.TileType<CrackedOrangeBrick>(), TileID.Dirt, TileID.Stone,
			ModContent.TileType<CrackedPurpleBrick>(), TileID.WoodenSpikes, ModContent.TileType<ImperviousBrick>(), ModContent.TileType<SavannaGrass>(),
			ModContent.TileType<VenomSpike>(), TileID.Mud, TileID.JungleGrass, TileID.MushroomGrass, ModContent.TileType<Loam>(),
			TileID.Silt };
	private static readonly int[] blacklistedWalls = new int[]
	{
		WallID.BlueDungeonSlabUnsafe,
		WallID.BlueDungeonTileUnsafe,
		WallID.BlueDungeonUnsafe,
		WallID.GreenDungeonSlabUnsafe,
		WallID.GreenDungeonTileUnsafe,
		WallID.GreenDungeonUnsafe,
		WallID.PinkDungeonSlabUnsafe,
		WallID.PinkDungeonTileUnsafe,
		WallID.PinkDungeonUnsafe,
		WallID.LihzahrdBrickUnsafe,
		ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>(),
		ModContent.WallType<Walls.OrangeBrickUnsafe>(),
		ModContent.WallType<Walls.OrangeTiledUnsafe>(),
		ModContent.WallType<Walls.OrangeSlabUnsafe>(),
		ModContent.WallType<Walls.PurpleBrickUnsafe>(),
		ModContent.WallType<Walls.PurpleSlabWallUnsafe>(),
		ModContent.WallType<Walls.PurpleTiledWallUnsafe>(),
		ModContent.WallType<Walls.ImperviousBrickWallUnsafe>(),
		WallID.HiveUnsafe,
		ModContent.WallType<Walls.NestWall>(),
	};

	public static void PlaceBasalt()
	{
		//if (GenSystem.CaesiumSide == 1)
		{
			Point highestPoint = new Point(20, WorldGen.genRand.Next(Main.maxTilesY - 400, Main.maxTilesY - 385));
			Point lowestPoint = new Point((Main.maxTilesX / 5), Main.maxTilesY - 200);
			float amplitude = 10f;
			int waveCount = 4;

			Vector2 path = lowestPoint.ToVector2() - highestPoint.ToVector2();
			float length = path.Length();
			Vector2 direction = Vector2.Normalize(path);
			Vector2 normal = new Vector2(-direction.Y, direction.X);
			int resolution = Math.Abs(lowestPoint.X - highestPoint.X);

			// Precompute sine wave heights at each x position
			int[] sineWaveHeights = new int[Main.maxTilesX];
			for (int x = 0; x < Main.maxTilesX; x++)
			{
				sineWaveHeights[x] = Main.maxTilesY; // Default to bottom of world
			}

			for (int waveX = 0; waveX <= resolution; waveX++)
			{
				float t = (float)waveX / resolution;
				float pointOnLine = highestPoint.Y + direction.Y * (t * length);
				float sineValue = (float)Math.Sin(t * waveCount * MathHelper.TwoPi);
				float offset = normal.Y * (sineValue * amplitude);
				float wavePoint = pointOnLine + offset;

				int heightMod = (waveX % 6) + WorldGen.genRand.Next(4) + 1;
				int waveY = (int)wavePoint + WorldGen.genRand.Next(-(heightMod / 3), (heightMod / 3) + 1);

				if (waveX >= 0 && waveX < Main.maxTilesX)
				{
					int curPosX = highestPoint.X;
					if (lowestPoint.X < highestPoint.X)
					{
						curPosX -= waveX;
					}
					else
					{
						curPosX += waveX;
					}
					if (waveY < sineWaveHeights[curPosX])
					{
						sineWaveHeights[curPosX] = waveY;
					}
				}
			}

			// Fill everything below sine wave with terrain
			for (int x = 0; x < Main.maxTilesX; x++)
			{
				int waveY = sineWaveHeights[x];
				for (int y = waveY; y < Main.maxTilesY - 180; y++)
				{
					if (Main.tile[x, y].TileType == TileID.Ash && Main.tile[x, y].HasTile) continue;
					if (Main.tile[x, y].HasTile && (blacklistedTiles.Contains(Main.tile[x, y].TileType) || TileID.Sets.Ore[Main.tile[x, y].TileType]))
					{
						WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Basalt>(), mute: true, forced: true);
					}
					if (!blacklistedWalls.Contains(Main.tile[x, y].WallType))
						WorldGen.KillWall(x, y);
				}
			}

			// Fill everything below sine wave with terrain
			for (int x = 0; x < Main.maxTilesX; x++)
			{
				int waveY = sineWaveHeights[x];
				for (int y = waveY; y < Main.maxTilesY - 180; y++)
				{
					if (Main.tile[x, y].TileType == TileID.Ash && Main.tile[x, y].HasTile) continue;
					if (Main.tile[x, y].HasTile && (blacklistedTiles.Contains(Main.tile[x, y].TileType) || TileID.Sets.Ore[Main.tile[x, y].TileType]))
					{
						WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Basalt>(), mute: true, forced: true);
					}
					if (!blacklistedWalls.Contains(Main.tile[x, y].WallType))
						WorldGen.KillWall(x, y);
				}
			}
			/*
			int numCaves = 150; // Total cave "worms"
			for (int i = 0; i < numCaves; i++)
			{
				// Start somewhere below the sine wave
				int startX = WorldGen.genRand.Next(Main.maxTilesX - (Main.maxTilesX / 5), Main.maxTilesX - 20);
				int startY = sineWaveHeights[startX] + WorldGen.genRand.Next(20, 120);

				CarveTunnel(startX, startY, WorldGen.genRand);
			}*/

			// Add scattered ovaloid caves
			int numOvaloidCaves = 80;
			for (int i = 0; i < numOvaloidCaves; i++)
			{
				int centerX = WorldGen.genRand.Next(Main.maxTilesX / 4, 3 * Main.maxTilesX / 4);
				int centerY = WorldGen.genRand.Next(Main.maxTilesY / 3, (int)(Main.maxTilesY * 0.85f));
				int radiusX = WorldGen.genRand.Next(6, 12);
				int radiusY = WorldGen.genRand.Next(4, 8);

				CarveOvaloidCave(centerX, centerY, radiusX, radiusY, WorldGen.genRand);

				// Optional: Lava pool in some of them
				if (centerY > Main.maxTilesY * 0.6 && WorldGen.genRand.NextFloat() < 0.25f)
				{
					PlaceLavaPool(centerX, centerY + radiusY / 2, WorldGen.genRand);
				}
			}
		}
	}

	private static void CarveOvaloidCave(int centerX, int centerY, int radiusX, int radiusY, UnifiedRandom rand)
	{
		for (int dx = -radiusX; dx <= radiusX; dx++)
		{
			for (int dy = -radiusY; dy <= radiusY; dy++)
			{
				int x = centerX + dx;
				int y = centerY + dy;

				if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY) continue;

				// Ellipse equation: (x^2 / a^2) + (y^2 / b^2) <= 1
				float normX = dx / (float)radiusX;
				float normY = dy / (float)radiusY;

				float ellipseVal = normX * normX + normY * normY;

				if (ellipseVal <= 1f + rand.NextFloat(-0.15f, 0.1f)) // Add some fuzziness
				{
					WorldGen.KillTile(x, y, noItem: true);
				}
			}
		}
	}
	private static void PlaceLavaPool(int x, int y, UnifiedRandom rand)
	{
		int width = rand.Next(3, 7);
		int height = rand.Next(2, 4);

		for (int i = -width / 2; i <= width / 2; i++)
		{
			for (int j = 0; j < height; j++)
			{
				int lx = x + i;
				int ly = y + j;

				if (lx < 0 || lx >= Main.maxTilesX || ly < 0 || ly >= Main.maxTilesY) continue;

				Tile tile = Main.tile[lx, ly];
				tile.LiquidType = LiquidID.Lava;
				tile.LiquidAmount = 255;
				tile.HasTile = false; // Remove any tile in lava space
			}
		}
	}
}
