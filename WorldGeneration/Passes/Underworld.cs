using Avalon.Common;
using Avalon.Tiles;
using Avalon.Tiles.Ores;
using Avalon.WorldGeneration.Structures;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes;

internal class Underworld : GenPass
{
    public Underworld() : base("Avalon Underworld", 120f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
		#region caesium blastplains
		if (ModContent.GetInstance<AvalonClientConfig>().SuperhardmodeStuff)
		{
			progress.Message = "Generating Caesium Blastplains";
			
			int caesiumXPosLeft = Main.maxTilesX - (Main.maxTilesX / 5) - 15;
			int caesiumXPosRight = Main.maxTilesX - (Main.maxTilesX / 5);
			int caesiumMaxRight = Main.maxTilesX - 20;

			bool smallWorld = Main.maxTilesY < WorldGen.WorldSizeMediumY;
			bool mediumWorld = Main.maxTilesY is >= WorldGen.WorldSizeMediumY and < WorldGen.WorldSizeLargeY;

			int spikeLength = smallWorld ? 20 : mediumWorld ? 23 : 26;
			int spikeWidth = 13;

			if (Main.drunkWorld)
			{
				caesiumXPosLeft = Main.maxTilesX - (Main.maxTilesX / 3) - 15;
				caesiumXPosRight = Main.maxTilesX - (Main.maxTilesX / 3);
				caesiumMaxRight = Main.maxTilesX - (Main.maxTilesX / 5) + 50;
			}
			if (GenVars.dungeonSide < 0 && !Main.drunkWorld)
			{
				int caesiumLeftSidePosXLeft = Main.maxTilesX / 5;
				int caesiumLeftSidePosXRight = Main.maxTilesX / 5 + 20;
				int caesiumMaxLeft = 20;

				// make little blobs on the edge
				for (int q = caesiumLeftSidePosXLeft; q < caesiumLeftSidePosXRight; q++)
				{
					for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
					{
						if (q > caesiumLeftSidePosXLeft - 5)
						{
							if (WorldGen.genRand.NextBool(5) && Main.tile[q, z].HasTile &&
								Main.tile[q, z].TileType == TileID.Ash)
							{
								WorldGen.TileRunner(q, z, WorldGen.genRand.Next(12, 19), WorldGen.genRand.Next(12, 18),
									ModContent.TileType<BlastedStone>());
							}
						}

					}
				}

				// shave some stuff off the bottom layer of hell
				for (int q = caesiumMaxLeft; q < caesiumLeftSidePosXLeft; q++)
				{
					for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
					{
						if (q % 6 == 0 && z > Main.maxTilesY - 140 && z < Main.maxTilesY - 120)
						{
							Utils.TileRunnerSpecial(q, z, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(20, 30), -2);
						}
						if (z > Main.maxTilesY - 115 && z < Main.maxTilesY - 95)
						{
							Main.tile[q, z].LiquidAmount = 0;
						}
					}
				}
				// make the majority of the blastplains
				for (int q = caesiumMaxLeft; q < caesiumLeftSidePosXLeft; q++)
				{
					for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
					{
						if ((Main.tile[q, z].TileType == TileID.Ash || Main.tile[q, z].TileType == TileID.Hellstone || Main.tile[q, z].TileType == TileID.AshGrass) &&
							Main.tile[q, z].HasTile)
						{
							Main.tile[q, z].TileType = (ushort)ModContent.TileType<BlastedStone>();
						}
						if (Main.tile[q, z].TileType == TileID.AshVines || Main.tile[q, z].TileType == TileID.AshPlants || Main.tile[q, z].TileType == TileID.TreeAsh)
						{
							WorldGen.KillTile(q, z);
						}
						// up spikes
						if (z < Main.maxTilesY - 100 && z > Main.maxTilesY - 110)
						{
							if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
							{
								if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
								{
									if (q % WorldGen.genRand.Next(30, 45) == 0)
									{
										z = Utils.CaesiumTileCheck(q, z, -1);
										z += WorldGen.genRand.Next(12) + 4;
										MakeSpike(q, z, WorldGen.genRand.Next(spikeLength - 7, spikeLength), WorldGen.genRand.Next(spikeWidth - 5, spikeWidth), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), -1);
										//if (WorldGen.genRand.NextBool(3))
										//{
										//    MakeSpike(q, z, WorldGen.genRand.Next(20, 25), WorldGen.genRand.Next(6, 11), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), -1);
										//}
									}
								}
							}
							//if (WorldGen.genRand.NextBool(250))
							//{
							//	MakeSpike(q, z + WorldGen.genRand.Next(12, 90), WorldGen.genRand.Next(5, 14), WorldGen.genRand.Next(2, 7), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumOre>(), WorldGen.genRand.NextFromList(-1, 1));
							//}
						}
						// down spikes
						if (z < Main.maxTilesY - 185 && z > Main.maxTilesY - 195)
						{
							if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
							{
								if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
								{
									if (q % WorldGen.genRand.Next(10, 15) == 0)
									{
										z = Utils.CaesiumTileCheck(q, z, 1);
										MakeSpike(q, z, WorldGen.genRand.Next(spikeLength - 7, spikeLength), WorldGen.genRand.Next(spikeWidth - 5, spikeWidth), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), 1);
										//if (WorldGen.genRand.NextBool(3))
										//{
										//    MakeSpike(q, z, WorldGen.genRand.Next(20, 25), WorldGen.genRand.Next(6, 11), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), 1);
										//}
									}
								}
							}
							//if (WorldGen.genRand.NextBool(500))
							//{
							//	MakeSpike(q, z, WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(2, 5), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumOre>(), 1);
							//}
						}
						if (WorldGen.genRand.NextBool(50))
							Utils.OreRunner(q, z, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(5, 8), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>());
					}
				}

				for (int q = caesiumMaxLeft; q < caesiumLeftSidePosXLeft; q++)
				{
					for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
					{
						if (q % 100 < 33 && z > Main.maxTilesY - 175)
						{
							if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
							{
								if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
								{
									Main.tile[q, z].TileType = (ushort)ModContent.TileType<LaziteGrass>();
								}
							}
						}
					}
				}
			}
			else
			{
				// make little blobs on the edge
				for (int q = caesiumXPosLeft; q < caesiumXPosRight; q++)
				{
					for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
					{
						if (q > caesiumXPosLeft + 5)
						{
							if (WorldGen.genRand.NextBool(5) && Main.tile[q, z].HasTile &&
								Main.tile[q, z].TileType == TileID.Ash)
							{
								WorldGen.TileRunner(q, z, WorldGen.genRand.Next(12, 19), WorldGen.genRand.Next(12, 18),
									ModContent.TileType<BlastedStone>());
							}
						}

					}
				}

				// shave some stuff off the bottom layer of hell
				for (int q = caesiumXPosRight; q < caesiumMaxRight; q++)
				{
					for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
					{
						if (q % 6 == 0 && z > Main.maxTilesY - 140 && z < Main.maxTilesY - 120)
						{
							Utils.TileRunnerSpecial(q, z, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(20, 30), -2);
						}
						if (z > Main.maxTilesY - 115 && z < Main.maxTilesY - 95)
						{
							Main.tile[q, z].LiquidAmount = 0;
						}
					}
				}

				// make the majority of the blastplains
				for (int q = caesiumXPosRight; q < caesiumMaxRight; q++)
				{
					for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
					{
						if ((Main.tile[q, z].TileType == TileID.Ash || Main.tile[q, z].TileType == TileID.Hellstone || Main.tile[q, z].TileType == TileID.AshGrass) &&
							Main.tile[q, z].HasTile)
						{
							Main.tile[q, z].TileType = (ushort)ModContent.TileType<BlastedStone>();
						}
						if (Main.tile[q, z].TileType == TileID.AshVines || Main.tile[q, z].TileType == TileID.AshPlants)
						{
							WorldGen.KillTile(q, z);
						}
						// up spikes
						if (z < Main.maxTilesY - 100 && z > Main.maxTilesY - 110)
						{
							if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
							{
								if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
								{
									if (q % WorldGen.genRand.Next(30, 45) == 0)
									{
										z = Utils.CaesiumTileCheck(q, z, -1);
										z += WorldGen.genRand.Next(12) + 4;
										MakeSpike(q, z, WorldGen.genRand.Next(spikeLength - 7, spikeLength), WorldGen.genRand.Next(spikeWidth - 5, spikeWidth), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), -1);
										//if (WorldGen.genRand.NextBool(3))
										//{
										//    MakeSpike(q, z, WorldGen.genRand.Next(20, 25), WorldGen.genRand.Next(6, 11), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), -1);
										//}
									}
								}
							}
							//if (WorldGen.genRand.NextBool(250))
							//{
							//	MakeSpike(q, z + WorldGen.genRand.Next(12, 90), WorldGen.genRand.Next(5, 14), WorldGen.genRand.Next(2, 7), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumOre>(), WorldGen.genRand.NextFromList(-1, 1));
							//}
						}
						// down spikes
						if (z < Main.maxTilesY - 185 && z > Main.maxTilesY - 195)
						{
							if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
							{
								if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
								{
									if (q % WorldGen.genRand.Next(10, 15) == 0)
									{
										z = Utils.CaesiumTileCheck(q, z, 1);
										// 15, 22 / 8, 13
										MakeSpike(q, z, WorldGen.genRand.Next(spikeLength - 7, spikeLength), WorldGen.genRand.Next(spikeWidth - 5, spikeWidth), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), 1);
										//if (WorldGen.genRand.NextBool(3))
										//{
										//    MakeSpike(q, z, WorldGen.genRand.Next(20, 25), WorldGen.genRand.Next(6, 11), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>(), 1);
										//}
									}
								}
							}
							//if (WorldGen.genRand.NextBool(500))
							//{
							//	MakeSpike(q, z, WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(2, 5), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumOre>(), 1);
							//}
						}
						if (WorldGen.genRand.NextBool(50))
							Utils.OreRunner(q, z, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(5, 8), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>());
					}
				}

				for (int q = caesiumXPosLeft; q < caesiumXPosRight; q++)
				{
					for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
					{
						if (q % 100 < 33 && z > Main.maxTilesY - 175)
						{
							if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
								(Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
							{
								if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
								{
									Main.tile[q, z].TileType = (ushort)ModContent.TileType<LaziteGrass>();
								}
							}
						}
					}
				}
			}
		}
        #endregion

        progress.Message = "Generating Hellcastle and the Phantom Garden";
        int hellcastleOriginX = (Main.maxTilesX / 2) - 200;
        int ashenLeft = hellcastleOriginX - 125;
        int ashenRight = hellcastleOriginX + 525;


		//if (Main.drunkWorld)
		//{
		//    hellcastleOriginX = (Main.maxTilesX / 3) - 210;
		//    ashenLeft = (Main.maxTilesX / 3) - 450;
		//    ashenRight = (Main.maxTilesX / 3) + 500;
		//}

		#region hellcastle
		if (ModContent.GetInstance<AvalonClientConfig>().UnimplementedStructureGen)
		{
			Hellcastle.GenerateHellcastle(hellcastleOriginX, Main.maxTilesY - 330);
			for (int hbx = ashenLeft; hbx < ashenRight; hbx++)
			{
				for (int hby = Main.maxTilesY - 200; hby < Main.maxTilesY - 50; hby++)
				{
					//if (Main.tile[hbx, hby].HasTile &&
					//    (Main.tile[hbx, hby].TileType == TileID.ObsidianBrick ||
					//     Main.tile[hbx, hby].TileType == TileID.HellstoneBrick))
					//{
					//    Main.tile[hbx, hby].TileType = (ushort)ModContent.TileType<ImperviousBrick>();
					//    Tile t = Main.tile[hbx, hby];
					//    t.HasTile = true;
					//}
					//if (Main.tile[hbx, hby].WallType == WallID.ObsidianBrickUnsafe ||
					//     Main.tile[hbx, hby].WallType == WallID.HellstoneBrickUnsafe)
					//{
					//    Main.tile[hbx, hby].TileType = (ushort)ModContent.TileType<ImperviousBrick>();
					//    Tile t = Main.tile[hbx, hby];
					//    t.HasTile = true;
					//    WorldGen.KillWall(hbx, hby);
					//}
					if ((Main.tile[hbx, hby].HasTile && !Main.tile[hbx, hby - 1].HasTile) ||
						(Main.tile[hbx, hby].HasTile && !Main.tile[hbx, hby + 1].HasTile) ||
						(Main.tile[hbx, hby].HasTile && !Main.tile[hbx - 1, hby].HasTile) ||
						(Main.tile[hbx, hby].HasTile && !Main.tile[hbx + 1, hby].HasTile) ||
						(Main.tile[hbx, hby].HasTile && !Main.tile[hbx - 1, hby - 1].HasTile) ||
						(Main.tile[hbx, hby].HasTile && !Main.tile[hbx - 1, hby + 1].HasTile) ||
						(Main.tile[hbx, hby].HasTile && !Main.tile[hbx + 1, hby - 1].HasTile) ||
						(Main.tile[hbx, hby].HasTile && !Main.tile[hbx + 1, hby + 1].HasTile))
					{
						if (Main.tile[hbx, hby].TileType == TileID.Ash)
						{
							SlopeType s = Main.tile[hbx, hby].Slope;
							Tile t = Main.tile[hbx, hby];
							t.TileType = (ushort)ModContent.TileType<Ectograss>();
							t.Slope = s;
							if (WorldGen.genRand.NextBool(1))
							{
								WorldGen.GrowTree(hbx, hby - 1);
							}
						}
					}
					if (WorldGen.genRand.NextBool(100))
					{
						WorldGen.OreRunner(hbx, hby, 4, 4, (ushort)ModContent.TileType<BrimstoneBlock>());
					}
					if (WorldGen.genRand.NextBool(50))
					{
						WorldGen.OreRunner(hbx, hby, 2, 3, TileID.Hellstone);
					}
				}
			}
		}
		#endregion
	}

    public static void MakeSpike2(int x, int y, ushort type, int lengthMin, int lengthMax)
    {
        int xmin = (int)(x) - WorldGen.genRand.Next(-15, 1) - 5;
        int xmax = (int)(x) + WorldGen.genRand.Next(0, 16);
        int m = xmin;
        int num8 = 0;
        
        for (; m < xmax; m += WorldGen.genRand.Next(8, 12))
        {
            int ystart = y - 3;
            while (!Main.tile[m, ystart].HasTile)
            {
                ystart--;
            }
            ystart -= 4;
            int num10 = WorldGen.genRand.Next(4, 8);
            int num11 = WorldGen.genRand.Next(5, 10);

            int n = m - num10;
            while (num10 > 0)
            {
                for (n = m - num10; n < m + num10; n++)
                {
                    Tile t = Main.tile[n, ystart];
                    t.HasTile = true;
                    t.TileType = type;
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
                ystart++;
            }
            n -= WorldGen.genRand.Next(1, 3);
            Tile t0 = Main.tile[n, ystart - 3];
            Tile t1 = Main.tile[n, ystart - 2];
            Tile t2 = Main.tile[n, ystart - 1];
            Tile t3 = Main.tile[n, ystart];
            t0.HasTile = true;
            t0.TileType = type;
            t1.HasTile = true;
            t1.TileType = type;
            t2.HasTile = true;
            t2.TileType = type;
            t3.HasTile = true;
            t3.TileType = type;
            WorldGen.SquareTileFrame(n, ystart - 2);
            WorldGen.SquareTileFrame(n, ystart - 1);
            WorldGen.SquareTileFrame(n, ystart);
        }
    }

	/// <summary>
	/// Makes a spike at the given coordinates.
	/// </summary>
	/// <param name="x">The X coordinate.</param>
	/// <param name="y">The Y coordinate.</param>
	/// <param name="length">The height/tallness of the spike to generate.</param>
	/// <param name="width">The width/thickness of the spike to generate.</param>
	/// <param name="direction">The vertical direction of the spike; 1 is down, -1 is up.</param>
	public static void MakeSpike(int x, int y, int length, int width, ushort centerType, ushort borderType, int direction = 1)
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
			int w = width;
			if ((int)distFromStart % 4 == 0 || w < 5)
			{
				width--;
			}

			// Put a circle between the last point and the current
			int betweenXPos = (int)(lastPos.X + x) / 2;
			int betweenYPos = (int)(lastPos.Y + y) / 2;
			float radiusTemp = (length - q) * 0.4f;
			int radius = radiusTemp > 1f ? (int)radiusTemp : (int)MathF.Round(radiusTemp);
			float radiusNextTemp = MathHelper.Clamp(radiusTemp - 0.4f, 0, radiusTemp);
			int radiusNext = radiusNextTemp > 1f ? (int)radiusNextTemp : (int)MathF.Round(radiusNextTemp);
			//if (radius == 0) radius = 1;
			//Main.NewText($"{radius} {radiusTemp} {radiusNext}");
			if (radiusNext > 1)
			{
				WorldGeneration.Utils.MakeCircle2(betweenXPos, betweenYPos, radius, borderType, centerType, 0.5f);

				// Make a square/circle of the tile
				WorldGeneration.Utils.MakeCircle2(x, y, radius, borderType, centerType, 0.5f);
			}
			else
			{
				if ((direction == 1 || q != length) && (direction == 1 ? MathHelper.Max(betweenXPos, x) - MathHelper.Min(betweenXPos, x) != 0 : MathHelper.Max(betweenXPos, x) - MathHelper.Min(betweenXPos, x) == 0))
				{
					betweenXPos -= 1;
					//Main.NewText($"{betweenXPos - x} {x - betweenXPos}", Main.DiscoColor);
				}

				for (int i = 0; i < radius + 1 + (int)(radiusTemp - 0.4f); i++)
				{
					int betweenXPosMod = betweenXPos - i * modifier;
					if (direction == 1 && q == length)
					{
						Tile betweenPosUp = Framing.GetTileSafely(betweenXPosMod, betweenYPos - 1);
						if (betweenPosUp.TileType != borderType)
						{
							Tile betweenPosUpL = Framing.GetTileSafely(betweenXPosMod - 1, betweenYPos - 1);
							Tile betweenPosUpR = Framing.GetTileSafely(betweenXPosMod + 1, betweenYPos - 1);
							if (betweenPosUpL.TileType == borderType)
							{
								betweenXPosMod -= 1;
							}
							else if (betweenPosUpR.TileType == borderType)
							{
								betweenXPosMod += 1;
							}

						}
					}
					Tile t = Framing.GetTileSafely(betweenXPosMod, betweenYPos);
					t.HasTile = true;
					t.IsHalfBlock = false;
					t.Slope = SlopeType.Solid;
					t.TileType = borderType;
					Tile.SmoothSlope(betweenXPosMod, betweenYPos);
					WorldGen.SquareTileFrame(betweenXPosMod, betweenYPos);

					if (q != length)
					{
						int xMod = x - i * modifier;
						Tile t2 = Framing.GetTileSafely(xMod, y);
						t2.HasTile = true;
						t2.IsHalfBlock = false;
						t2.Slope = SlopeType.Solid;
						t2.TileType = borderType;
						Tile.SmoothSlope(xMod, y);
						WorldGen.SquareTileFrame(xMod, y);
						//Dust.QuickDust(new Point(xMod, y), Color.Blue);
						//Main.NewText(MathHelper.Max(betweenXPos, x) - MathHelper.Min(betweenXPos, x), Main.DiscoColor);
					}
					//Dust.QuickDust(new Point(betweenXPosMod, betweenYPos), Color.Red);
				}
			}

			// Assign the last position to the current position
			lastPos = new(x, y);

			// Make the spike go in the opposite direction after a certain amount of tiles
			howManyTimes++;
			if (howManyTimes % maxTimes == 0 && radiusNext > 3)
			{
				modifier *= -1;
				howManyTimes = 0;
				maxTimes = WorldGen.genRand.Next(4) + 2;
			}

			// Add to the x to make the spike turn/curve in a direction
			x += ((radiusNext > 1 ? WorldGen.genRand.Next(2) + 1 : y != startY + length * direction ? WorldGen.genRand.Next(2) : 0)) * modifier; // * (maxTimes / (WorldGen.genRand.Next(2) + 1));

			// Add a bit to the y coord to make it go up or down depending on the parameter
			if (w > 3)
			{
				y += ((radiusNext > 2 ? WorldGen.genRand.Next(3) + 2 : 1)) * direction;
			}
			else
			{
				y += 1 * direction;
			}
		}
	}
}
