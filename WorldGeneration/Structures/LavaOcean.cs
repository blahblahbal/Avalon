using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Structures
{
	internal class LavaOcean
	{
		// old heartstone gen code, maybe could be useful
		public static void Generate2(int i, int j)
		{
			double num = (double)WorldGen.genRand.Next(50, 75);
			double num2 = num;
			float num3 = (float)WorldGen.genRand.Next(50, 75);
			if (WorldGen.genRand.NextBool(5))
			{
				num *= 1.5;
				num2 *= 1.5;
				num3 *= 1.2f;
			}
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j - num3 * 0.3f;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(-5, 6) * 0.1f;
			value2.Y = (float)WorldGen.genRand.Next(-10, -5) * 0.1f;
			while (num > 0.0 && num3 > 0f)
			{
				num -= (double)WorldGen.genRand.Next(3);
				num3 -= 1f;
				int num4 = (int)((double)value.X - num * 0.5);
				int num5 = (int)((double)value.X + num * 0.5);
				int num6 = (int)((double)value.Y - num * 0.5);
				int num7 = (int)((double)value.Y + num * 0.5);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				num2 = num * (double)WorldGen.genRand.Next(200, 400) * 0.01;
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						float num8 = Math.Abs((float)k - value.X);
						float num9 = Math.Abs(((float)l - value.Y) * 2.3f);
						double num10 = Math.Sqrt((double)(num8 * num8 + num9 * num9));
						if (num10 < num2 * 0.4)
						{
							if ((double)l < (double)value.Y + num2 * 0.02)
							{
								if (Main.tile[k, l].TileType != ModContent.TileType<Tiles.BlastedStone>())
								{
									Tile t2 = Main.tile[k, l];
									t2.HasTile = false;
								}
							}
							else
							{
								Tile t2 = Main.tile[k, l];
								t2.TileType = (ushort)ModContent.TileType<Tiles.BlastedStone>();
							}
							Tile t = Main.tile[k, l];
							t.LiquidAmount = 0;
							t.LiquidType = LiquidID.Water;
						}
					}
				}
				value += value2;
				value.X += value2.X;
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				value2.Y -= (float)WorldGen.genRand.Next(11) * 0.05f;
				if ((double)value2.X > -0.5 && (double)value2.X < 0.5)
				{
					if (value2.X < 0f)
					{
						value2.X = -0.5f;
					}
					else
					{
						value2.X = 0.5f;
					}
				}
				if (value2.X > 2f)
				{
					value2.X = 1f;
				}
				if (value2.X < -2f)
				{
					value2.X = -1f;
				}
				if (value2.Y > 1f)
				{
					value2.Y = 1f;
				}
				if (value2.Y < -1f)
				{
					value2.Y = -1f;
				}
				for (int m = 0; m < 2; m++)
				{
					int num11 = (int)value.X + WorldGen.genRand.Next(-20, 20);
					int num12 = (int)value.Y + WorldGen.genRand.Next(0, 20);
					while (!Main.tile[num11, num12].HasTile && Main.tile[num11, num12].TileType != ModContent.TileType<Tiles.BlastedStone>())
					{
						num11 = (int)value.X + WorldGen.genRand.Next(-10, 10);
						num12 = (int)value.Y + WorldGen.genRand.Next(0, 10);
					}
					int num13 = WorldGen.genRand.Next(7, 10);
					int num14 = WorldGen.genRand.Next(7, 10);
					WorldGen.TileRunner(num11, num12, (double)num13, num14, ModContent.TileType<Tiles.BlastedStone>(), true, 0f, 2f, true, true);
					if (WorldGen.genRand.NextBool(3))
					{
						WorldGen.TileRunner(num11, num12, (double)(num13 - 3), num14 - 3, -1, false, 0f, 2f, true, true);
					}
				}
			}
		}
		// old ice biome (1.1 avalon) gen code, maybe could be useful
		public static void MakeLavaLake(int x, int y)
		{
			int xsave = x;
			int ysave = y;
			ushort stone = (ushort)ModContent.TileType<Tiles.BlastedStone>();
			if (x < 30) x = 30;
			if (x > Main.maxTilesX - 30) x = Main.maxTilesX - 30;
			if (y < (int)Main.rockLayer) y = (int)Main.rockLayer;
			if (y > Main.maxTilesY - 200) y = Main.maxTilesY - 200;
			/*for (int stuff = 100; stuff < y; stuff++)
            {
                Main.tile[x, stuff].type = stone;
                WorldGen.SquareTileFrame(x, stuff);
            }*/
			int thing = 0;
			while (thing < 9)
			{
				GrowLava(x, y, stone, 3);
				GrowLava(x, y - 2, stone, 7);
				GrowLava(x + 1, y, stone, 3);
				GrowLava(x, y, stone, 4);
				GrowLava(x - 1, y, stone, 3);
				GrowLava(x, y, stone, 4);
				GrowLava(x + 3, y, stone, 2);
				GrowLava(x - 3, y, stone, 2);

				GrowLava(x + 2, y - 1, stone, 8);
				GrowLava(x - 1, y + 2, stone, 9);
				GrowLava(x + 1, y, stone, 8);
				GrowLava(x + 3, y - 3, stone, 6);
				GrowLava(x - 2, y, stone, 9);
				GrowLava(x - 4, y + 2, stone, 8);
				GrowLava(x - 3, y + 1, stone, 5);
				GrowLava(x - 3, y - 1, stone, 5);

				GrowLava(x + 5, y - 2, stone, 9);
				GrowLava(x - 4, y + 1, stone, 10);
				GrowLava(x + 2, y - 1, stone, 7);
				GrowLava(x + 3, y - 2, stone, 8);
				GrowLava(x, y + 3, stone, 9);
				GrowLava(x - 3, y + 2, stone, 8);
				GrowLava(x - 3, y + 2, stone, 6);
				GrowLava(x - 3, y - 2, stone, 6);

				switch (thing)
				{
					case 0:
						x += 6;
						y -= 6;
						break;
					case 1:
						x += 6;
						y += 6;
						break;
					case 2:
						x -= 6;
						y += 6;
						break;
					case 3:
						x -= 6;
						y -= 6;
						break;
					case 4:
						x = xsave + 18;
						y = ysave - 12;
						break;
					case 5:
						x += 6;
						y += 6;
						break;
					case 6:
						x -= 6;
						y += 6;
						break;
					case 7:
						x -= 6;
						y -= 6;
						break;
					default:
						break;
				}
				thing++;
			}
		}
		public static void GrowLava(int x, int y, ushort type, int rounds, bool lava = false)
		{
			int growth = WorldGen.genRand.Next(256);

			for (int i = 0; i < 9; i++)
			{
				int j = i % 3;
				int tgro = 1 << i;
				if ((tgro & growth) == tgro)
				{
					int tx = (x + j - 1);
					int ty = y + (i / 3) - 1;



					if (Main.tile[tx, ty].HasTile)
					{
						Tile t = Main.tile[tx, ty];
						t.HasTile = true;
						t.TileType = type;
						WorldGen.SquareTileFrame(tx, ty);
						if (rounds > 0)
						{
							GrowLava2(tx, ty, type, rounds - 1);
						}
					}
				}
			}
			WorldGen.SquareTileFrame(x, y);
		}
		public static void GrowLava2(int x, int y, ushort type, int rounds, bool lava = false)
		{
			int growth = WorldGen.genRand.Next(256);

			for (int i = 0; i < 9; i++)
			{
				int j = i % 3;
				int tgro = 1 << i;
				if ((tgro & growth) == tgro)
				{
					int tx = (x + j - 1);
					int ty = y + (i / 3) - 1;



					if (!Main.tile[tx, ty].HasTile)
					{
						Tile t = Main.tile[tx, ty];
						t.HasTile = true;
						t.TileType = type;
						WorldGen.SquareTileFrame(tx, ty);
						if (rounds > 0)
						{
							GrowLava2(tx, ty, type, rounds - 1);
						}
					}
				}
			}
			WorldGen.SquareTileFrame(x, y);
		}

		public static void Generate(int x, int y)
		{
			ushort tileStone = (ushort)ModContent.TileType<Tiles.BlastedStone>();

			int xRad = WorldGen.genRand.Next(50, 60);
			int yRad = WorldGen.genRand.Next(30, 40);
			int thickness = WorldGen.genRand.Next(12, 20);

			if (WorldGen.genRand.NextBool(3))
			{
				xRad += WorldGen.genRand.Next(5, 12);
			}

			//change the tiles
			MakeOval(x, y, xRad + 20, yRad + 20, tileStone, true);

			// add the air
			MakeOval(x, y, xRad, yRad, ushort.MaxValue);

			// add the lava
			MakeOval(x, y, xRad - thickness, yRad - thickness, ushort.MaxValue, lava: true);

			double num19 = 0.3;
			num19 *= 1.0 - WorldGen.genRand.NextDouble() * 0.1;

			int num24 = (int)(x - xRad * num19) - WorldGen.genRand.Next(-15, 1) - 5;
			int num2 = (int)(x + xRad * num19) + WorldGen.genRand.Next(0, 16);
			int m = num24;
			int num8 = 0;
			for (; m < num2; m += WorldGen.genRand.Next(12, 16))
			{
				int num9 = y - 3;
				while (!Main.tile[m, num9].HasTile)
				{
					num9--;
				}
				num9 -= 4;
				int num10 = WorldGen.genRand.Next(5, 10);
				int num11 = WorldGen.genRand.Next(5, 10);
				int n = m - num10;
				while (num10 > 0)
				{
					for (n = m - num10; n < m + num10; n++)
					{
						Tile t = Main.tile[n, num9];
						t.HasTile = true;
						t.TileType = tileStone;
					}
					num8++;
					if (WorldGen.genRand.Next(3) < num8)
					{
						num8 = 0;
						num10--;
						m += WorldGen.genRand.Next(-1, 2);
					}
					if (num11 <= 0)
					{
						num10--;
					}
					num11--;
					num9++;
				}
				n -= WorldGen.genRand.Next(1, 3);
				Tile t1 = Main.tile[n, num9 - 2];
				Tile t2 = Main.tile[n, num9 - 1];
				Tile t3 = Main.tile[n, num9];
				t1.HasTile = true;
				t1.TileType = tileStone;
				t2.HasTile = true;
				t2.TileType = tileStone;
				t3.HasTile = true;
				t3.TileType = tileStone;
				WorldGen.SquareTileFrame(n, num9 - 2);
				WorldGen.SquareTileFrame(n, num9 - 1);
				WorldGen.SquareTileFrame(n, num9);
				if (WorldGen.genRand.NextBool(2))
				{
					Tile t4 = Main.tile[n, num9 + 1];
					t4.HasTile = true;
					t4.TileType = tileStone;
					WorldGen.SquareTileFrame(n, num9 + 1);
					Utils.PlaceCustomTight(n, num9 + 2, (ushort)ModContent.TileType<Tiles.BlastedStalac>());
				}
				else
				{
					Utils.PlaceCustomTight(n, num9 + 1, (ushort)ModContent.TileType<Tiles.BlastedStalac>());
				}
			}
		}

		public static bool GenerateLavaOcean(int X, int Y)
		{
			ushort tileStone = (ushort)ModContent.TileType<Tiles.BlastedStone>();

			int num = 0;
			double num12 = 0.6;
			double num18 = 1.3;
			double num19 = 0.3;
			if (num == 0)
			{
				num12 = 0.55;
				num18 = 2.0;
			}
			num12 *= 1.05 - WorldGen.genRand.NextDouble() * 0.1;
			num18 *= 1.05 - WorldGen.genRand.NextDouble() * 0.1;
			num19 *= 1.0 - WorldGen.genRand.NextDouble() * 0.1;
			int biomeWidth = WorldGen.genRand.Next(125, 140); // 105, 125
			int num21 = (int)(biomeWidth * num19);
			int num22 = (int)(biomeWidth * num12);
			int num23 = WorldGen.genRand.Next(9, 13);
			int num24 = X - biomeWidth + (biomeWidth / 3);
			int num2 = X + biomeWidth - (biomeWidth / 3);
			int num3 = Y - biomeWidth;
			int num4 = Y + biomeWidth;
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num24; j <= num2; j++)
				{
					if (!WorldGen.InWorld(j, i, 50))
					{
						return false;
					}
					if (Main.tile[j, i].TileType == 203 || Main.tile[j, i].TileType == 25)
					{
						return false;
					}
				}
			}
			int num5 = Y;
			if (WorldGen.genRand.NextBool(4))
			{
				num5 = Y - WorldGen.genRand.Next(2);
			}
			int num6 = Y - num23;
			if (WorldGen.genRand.NextBool(4))
			{
				num6 = Y - num23 - WorldGen.genRand.Next(2);
			}
			for (int k = num3; k <= num4; k++)
			{
				for (int l = num24; l <= num2; l++)
				{
					Tile t0 = Main.tile[l, k];
					t0.LiquidAmount = 0;
					if (WorldGen.genRand.NextBool(4))
					{
						num5 = Y - WorldGen.genRand.Next(2);
					}
					if (WorldGen.genRand.NextBool(4))
					{
						num6 = Y - num23 + WorldGen.genRand.Next(2);
					}
					int num7 = ((k <= Y) ? ((int)Math.Sqrt(Math.Pow(Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2) + Math.Pow(Math.Abs(k - Y) * 1.8 * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2))) : ((int)Math.Sqrt(Math.Pow((double)Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2) + Math.Pow((double)Math.Abs(k - Y) * 1.8 * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2))));
					if (num7 < biomeWidth)
					{
						Tile t = Main.tile[l, k];
						t.Slope = SlopeType.Solid;
						t.IsHalfBlock = false;
						t.TileType = tileStone;
						if (l > num24 + 5 + WorldGen.genRand.Next(2) && l < num2 - 5 - WorldGen.genRand.Next(2))
						{
							t.HasTile = true;
						}
						if ((double)num7 < (double)biomeWidth * 0.9)
						{
							t.WallType = WallID.ObsidianBackUnsafe;
						}
						WorldGen.SquareTileFrame(l, k);
					}
					num7 = (int)Math.Sqrt(Math.Pow(Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.3) + Math.Pow((double)Math.Abs(k - Y) * num18 * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.3));
					if (k > num6 && k < num5)
					{
						Tile t = Main.tile[l, k];
						t.HasTile = false;
					}
					if (k < num5 && num7 < (int)(num22 * (1.0 + WorldGen.genRand.NextDouble() * 0.02)))
					{
						Tile t = Main.tile[l, k];
						t.HasTile = false;
					}
					num7 = (int)Math.Sqrt(Math.Pow(Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 1.85) + Math.Pow((Math.Abs(k - Y) * 2) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 1.85));
					if (k < Y - 1 || num7 >= (int)(num21 * (1.0 + WorldGen.genRand.NextDouble() * 0.025)))
					{
						continue;
					}
					if (k <= Y + 2 || num7 != num21 - 1 || !WorldGen.genRand.NextBool(2))
					{
						Tile t = Main.tile[l, k];
						t.HasTile = false;
					}
					if (k >= Y)
					{
						Tile t = Main.tile[l, k];
						if (k == Y)
						{
							t.LiquidAmount = 127;
						}
						else
						{
							t.LiquidAmount = byte.MaxValue;
						}
						t.LiquidType = LiquidID.Lava;
					}
				}
			}
			if (num == 0)
			{
				num24 = (int)(X - biomeWidth * num19) - WorldGen.genRand.Next(-15, 1) - 5;
				num2 = (int)(X + biomeWidth * num19) + WorldGen.genRand.Next(0, 16);
				int m = num24;
				int num8 = 0;
				for (; m < num2; m += WorldGen.genRand.Next(9, 14))
				{
					int num9 = Y - 3;
					while (!Main.tile[m, num9].HasTile)
					{
						num9--;
					}
					num9 -= 4;
					int num10 = WorldGen.genRand.Next(5, 10);
					int num11 = WorldGen.genRand.Next(5, 10);
					int n = m - num10;
					while (num10 > 0)
					{
						for (n = m - num10; n < m + num10; n++)
						{
							Tile t = Main.tile[n, num9];
							t.HasTile = true;
							t.TileType = tileStone;
						}
						num8++;
						if (WorldGen.genRand.Next(3) < num8)
						{
							num8 = 0;
							num10--;
							m += WorldGen.genRand.Next(-1, 2);
						}
						if (num11 <= 0)
						{
							num10--;
						}
						num11--;
						num9++;
					}
					n -= WorldGen.genRand.Next(1, 3);
					Tile t1 = Main.tile[n, num9 - 2];
					Tile t2 = Main.tile[n, num9 - 1];
					Tile t3 = Main.tile[n, num9];
					t1.HasTile = true;
					t1.TileType = tileStone;
					t2.HasTile = true;
					t2.TileType = tileStone;
					t3.HasTile = true;
					t3.TileType = tileStone;
					WorldGen.SquareTileFrame(n, num9 - 2);
					WorldGen.SquareTileFrame(n, num9 - 1);
					WorldGen.SquareTileFrame(n, num9);
					if (WorldGen.genRand.NextBool(2))
					{
						Tile t4 = Main.tile[n, num9 + 1];
						t4.HasTile = true;
						t4.TileType = tileStone;
						WorldGen.SquareTileFrame(n, num9 + 1);
						Utils.PlaceCustomTight(n, num9 + 2, (ushort)ModContent.TileType<Tiles.BlastedStalac>());
					}
					else
					{
						Utils.PlaceCustomTight(n, num9 + 1, (ushort)ModContent.TileType<Tiles.BlastedStalac>());
					}
				}
			}
			return true;
		}

		public static void MakeStalac(int x, int y, int length, int width, ushort centerType, ushort borderType, int direction = 1)
		{
			// Store the x and y in new vars
			int startX = x;
			int startY = y;

			// Define variables to determine how many tiles to travel in one direction before changing direction
			int howManyTimes = 0;
			int maxTimes = WorldGen.genRand.Next(4) + 2;
			int modifier = 1;

			// Change direction (left/right) of the spike before generating
			if (WorldGen.genRand.NextBool())
			{
				modifier *= -1;
			}

			// Initial assignment of last position
			Vector2 lastPos = new(x, y);

			// Loop until length
			for (int q = 1; q <= length; q++)
			{
				// Grab the distance between the start and the current position
				float distFromStart = Vector2.Distance(new Vector2(startX, startY), new Vector2(x, y));

				// If the distance is divisible by 4 and the width is less than 5, reduce the width
				//int w = width;
				//if ((int)distFromStart % 4 == 0 || w < 5)
				//{
				//    width--;
				//}

				// Put a circle between the last point and the current
				int betweenXPos = (int)(lastPos.X + x) / 2;
				int betweenYPos = (int)(lastPos.Y + y) / 2;
				Utils.MakeCircle(betweenXPos, betweenYPos, (int)((length - q)) * 2, borderType);

				// Make a square/circle of the tile
				Utils.MakeCircle(x, y, (int)((length - q)) * 2, borderType);

				// Assign the last position to the current position
				lastPos = new(x, y);

				// Make the spike go in the opposite direction after a certain amount of tiles
				howManyTimes++;
				if (howManyTimes % maxTimes == 0)
				{
					modifier *= -1;
					howManyTimes = 0;
					maxTimes = WorldGen.genRand.Next(4) + 2;
				}

				// Add to the x to make the spike turn/curve in a direction
				x += (WorldGen.genRand.Next(2) + 1) * modifier; // * (maxTimes / (WorldGen.genRand.Next(2) + 1));

				// Add a bit to the y coord to make it go up or down depending on the parameter
				//if (w > 3)
				//{
				//    y += (WorldGen.genRand.Next(3) + 2) * direction;
				//}
				//else
				{
					y += 1 * direction;
				}
			}
		}

		private static bool IsNearEllipseBorder(int x, int y, int centerX, int centerY, int radiusX, int radiusY, int borderThickness)
		{
			float dx = x - centerX;
			float dy = y - centerY;
			float ellipseValue = (dx * dx) / (radiusX * radiusX) + (dy * dy) / (radiusY * radiusY);

			// Check if the tile is within the specified border thickness from the ellipse's border
			return ellipseValue >= 1 - (borderThickness / (float)radiusX);
		}
		public static void MakeOval(int x, int y, int xRadius, int yRadius, int type, bool replaceTiles = false, bool lava = false)
		{
			int xmin = x - xRadius;
			int ymin = y - yRadius;
			int xmax = x + xRadius;
			int ymax = y + yRadius;
			for (int i = xmin; i < xmax + 1; i++)
			{
				for (int j = ymin; j < ymax + 1; j++)
				{
					if (Utils.IsInsideEllipse(i, j, new Vector2(x, y), xRadius, yRadius))
					{
						Tile q = Main.tile[i, j];
						q.WallType = 0;
						//q.LiquidAmount = 0;
						if (lava)
						{
							Tile t = Main.tile[i, j];
							t.LiquidAmount = 254;
							t.LiquidType = LiquidID.Lava;
							//WorldGen.SquareTileFrame(i, j);
							t.WallType = WallID.ObsidianBackUnsafe;
						}
						else if (type == 65535)
						{
							Tile t = Main.tile[i, j];
							t.HasTile = false;
							WorldGen.SquareTileFrame(i, j);
							t.WallType = WallID.ObsidianBackUnsafe;
						}
						else
						{
							if (replaceTiles && j < y + 10)
							{
								Tile tile = Main.tile[i, j];
								if (tile.HasTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType])
								{
									tile.TileType = (ushort)type;
									tile.Slope = SlopeType.Solid;
									tile.IsHalfBlock = false;
									WorldGen.SquareTileFrame(i, j);
								}
								else
								{
									tile.WallType = WallID.ObsidianBackUnsafe;
								}
							}
							else
							{
								Tile t = Main.tile[i, j];
								t.HasTile = true;
								t.Slope = SlopeType.Solid;
								t.IsHalfBlock = false;
								t.TileType = (ushort)type;
								WorldGen.SquareTileFrame(i, j);
							}
							WorldGen.SquareTileFrame(i, j);
						}
					}
				}
			}
			//for (int i = xmin - 10; i < xmax + 11; i++)
			//{
			//    for(int j = ymin - 10; j < ymax + 11; j++)
			//    {
			//        if (IsNearEllipseBorder(i, j, x, y, xRadius, yRadius, 1))
			//        {
			//            if (i % 5 == 0 || j % 5 == 0)
			//                WorldGen.TileRunner(i, j, WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(5, 9), type);
			//        }
			//    }
			//}
		}
	}
}
