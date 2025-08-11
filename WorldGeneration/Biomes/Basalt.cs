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
	private static readonly int[] blacklistedWalls =
	[
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
		WallID.Planked,
		WallID.GraniteUnsafe,
		WallID.MarbleUnsafe
	];

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
					if ((Main.tile[x, y].TileType == TileID.Ash || Main.tile[x, y].TileType == TileID.AshGrass ||
						Main.tile[x, y].TileType == TileID.MinecartTrack) && Main.tile[x, y].HasTile)
						continue;
					if (Main.tile[x, y].HasTile &&
						(Main.tile[x, y].TileType == TileID.Stone || Main.tile[x, y].TileType == TileID.Dirt || Main.tileMoss[Main.tile[x, y].TileType]))
					{
						WorldGen.PlaceTile(x, y, ModContent.TileType<BasaltBlock>(), mute: true, forced: true);
					}
					if (!blacklistedWalls.Contains(Main.tile[x, y].WallType))
						WorldGen.KillWall(x, y);
				}
			}
		}
	}
}
