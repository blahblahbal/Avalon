using System;
using System.Collections.Generic;
using System.Linq;
using Avalon.Tiles.Contagion.SnotOrb;
using Avalon.Tiles.CrystalMines;
using Avalon.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.World.Biomes;
internal class CrystalMines
{
    private static readonly int[] blacklistedTiles = new int[] { 225, 41, 43, 44, 226, 203, 112, 25, 151, //ModContent.TileType<Tiles.TuhrtlBrick>(),
            ModContent.TileType<Tiles.OrangeBrick>(), ModContent.TileType<Tiles.PurpleBrick>(), ModContent.TileType<Tiles.CrackedOrangeBrick>(),
            ModContent.TileType<Tiles.CrackedPurpleBrick>(), TileID.WoodenSpikes, ModContent.TileType<Tiles.ImperviousBrick>(),
            ModContent.TileType<Tiles.VenomSpike>() };
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
        //ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>(),
        ModContent.WallType<Walls.OrangeBrickUnsafe>(),
        ModContent.WallType<Walls.OrangeTiledUnsafe>(),
        ModContent.WallType<Walls.OrangeSlabUnsafe>(),
        ModContent.WallType<Walls.PurpleBrickUnsafe>(),
        ModContent.WallType<Walls.PurpleSlabWallUnsafe>(),
        ModContent.WallType<Walls.PurpleTiledWallUnsafe>(),
        ModContent.WallType<Walls.ImperviousBrickWallUnsafe>()
    };

    public static bool PlaceNew(Point origin)
    {
        int width = WorldGen.genRand.Next(100, 120);
        int height = 90;// WorldGen.genRand.Next(70, 90);

        WorldGeneration.Utils.TileRunnerSpecial(origin.X - width / 4, origin.Y - height / 4, WorldGen.genRand.Next(60, 70), WorldGen.genRand.Next(60, 70), ModContent.TileType<CrystalStone>(), true);
        WorldGeneration.Utils.TileRunnerSpecial(origin.X + width / 4, origin.Y - height / 4, WorldGen.genRand.Next(60, 70), WorldGen.genRand.Next(60, 70), ModContent.TileType<CrystalStone>(), true);
        WorldGeneration.Utils.TileRunnerSpecial(origin.X - width / 4, origin.Y + height / 4, WorldGen.genRand.Next(60, 70), WorldGen.genRand.Next(60, 70), ModContent.TileType<CrystalStone>(), true);
        WorldGeneration.Utils.TileRunnerSpecial(origin.X + width / 4, origin.Y + height / 4, WorldGen.genRand.Next(60, 70), WorldGen.genRand.Next(60, 70), ModContent.TileType<CrystalStone>(), true);

		WorldGeneration.Utils.TileRunnerSpecial(origin.X, origin.Y + height / 2 - 5, WorldGen.genRand.Next(25, 35), WorldGen.genRand.Next(25, 35), ModContent.TileType<CrystalStone>(), true);
		WorldGeneration.Utils.TileRunnerSpecial(origin.X, origin.Y - height / 2 + 5, WorldGen.genRand.Next(25, 35), WorldGen.genRand.Next(25, 35), ModContent.TileType<CrystalStone>(), true);
		WorldGeneration.Utils.TileRunnerSpecial(origin.X + width / 2 - 5, origin.Y, WorldGen.genRand.Next(25, 35), WorldGen.genRand.Next(25, 35), ModContent.TileType<CrystalStone>(), true);
		WorldGeneration.Utils.TileRunnerSpecial(origin.X - width / 2 + 5, origin.Y, WorldGen.genRand.Next(25, 35), WorldGen.genRand.Next(25, 35), ModContent.TileType<CrystalStone>(), true);

		for (int i = origin.X - width / 2; i < origin.X + width / 2; i++)
        {
            for (int j = origin.Y - height / 2; j < origin.Y + height / 2; j++)
            {
                if (WorldGen.genRand.NextBool(200))
                {
                    WorldGeneration.Utils.TileRunnerSpecial(i, j, WorldGen.genRand.Next(10, 15), WorldGen.genRand.Next(10, 15), ModContent.TileType<CrystalStoneCrystals>(), true);
                }
            }
        }

		for (int i = origin.X - width / 4; i < origin.X + width / 4; i++)
		{
			for (int j = origin.Y - height / 4; j < origin.Y + height / 4; j++)
			{
				if (Main.tile[i, j].TileType != ModContent.TileType<CrystalStone>() && Main.tile[i, j].TileType != ModContent.TileType<CrystalStoneCrystals>())
				{
					Main.tile[i, j].TileType = (ushort)ModContent.TileType<CrystalStone>();
					WorldGen.SquareTileFrame(i, j);
				}
				Main.tile[i, j].WallType = (ushort)ModContent.WallType<CrystalStoneWall>();
				WorldGen.SquareWallFrame(i, j);
			}
		}
		int ystep = 11;
		int ypos = origin.Y - height / 2;
		int pillarMod = WorldGen.genRand.Next(10, 14);

		List<Point> leftSideCenters = new();
		List<Point> rightSideCenters = new();

		List<int> yCoords = new();

		for (int floor = 0; floor < 4; floor++)
		{
			for (int x = origin.X - width / 2 + 6; x < origin.X + width / 2 - 6; x++)
			{
				for (int y = ypos + ystep * (floor + 1); y < ypos + ystep * (floor + 2); y++)
				{
					if (y == ypos + ystep * (floor + 1) || y == ypos + ystep * (floor + 2) - 1)
					{
						if (y != ypos + ystep * (floor + 2) - 1)
						{
							if (!yCoords.Contains(y + 6))
							{
								yCoords.Add(y + 6);
							}
						}
						WorldGen.PlaceTile(x, y, ModContent.TileType<CrystalStoneCrystals>(), forced: true);
					}
					else
					{
						WorldGen.KillTile(x, y);
						if (x % pillarMod == 0)
						{
							WorldGen.PlaceTile(x, y, ModContent.TileType<CrystalColumn>());
						}
					}
				}
			}
			ypos += ystep;
			pillarMod = WorldGen.genRand.Next(10, 14);
		}

		for (int centers = 0; centers < 4; centers++)
		{
			leftSideCenters.Add(new Point(origin.X - width / 2 + 6, yCoords[centers]));
			rightSideCenters.Add(new Point(origin.X + width / 2 + 6, yCoords[centers]));
		}

		for (int q = 0; q < 4; q++)
		{
			int leftEndX = leftSideCenters[q].X - WorldGen.genRand.Next(15, 20);
			int leftEndY = leftSideCenters[q].Y + WorldGen.genRand.Next(-5, 6);

			int rightEndX = rightSideCenters[q].X - WorldGen.genRand.Next(15, 20);
			int rightEndY = rightSideCenters[q].Y + WorldGen.genRand.Next(-5, 6);

			BoreWavyTunnel(leftSideCenters[q].X, leftSideCenters[q].Y, leftEndX, leftEndY, 50, 4, 4);
			BoreWavyTunnel(rightSideCenters[q].X, rightSideCenters[q].Y, rightEndX, rightEndY, 50, 4, 4);
		}

		for (int i = origin.X - width / 2; i < origin.X + width / 2; i++)
		{
			for (int j = origin.Y - height / 2; j < origin.Y + height / 2; j++)
			{
				if (!Main.tile[i, j].HasTile && Main.tile[i, j + 1].HasTile && (Main.tile[i, j + 1].TileType == ModContent.TileType<CrystalStone>() ||
					Main.tile[i, j + 1].TileType == ModContent.TileType<CrystalStoneCrystals>()))
				{
					if (Main.tile[i, j].TileType != ModContent.TileType<ShatterShards>())
					{
						if (WorldGen.genRand.NextBool(8))
						{
							WorldGen.PlaceTile(i, j, ModContent.TileType<ShatterShards>(), style: WorldGen.genRand.Next(3));
						}
					}
				}
			}
		}

        WorldGeneration.Utils.SquareTileFrameArea(origin.X - width / 2, origin.Y - height / 2, width, height);
        return true;
    }

	public static void BoreWavyTunnel(int startX, int startY, int endX, int endY, int wavelength, float amplitude, int radius)
	{
		float length = Vector2.Distance(new Vector2(startX, startY), new Vector2(endX, endY));
		float direction = (float)Math.Atan2(endY - startY, endX - startX);
		float t = 0f;

		while (t <= 1f)
		{
			int x = (int)MathHelper.Lerp(startX, endX, t);
			int y = (int)MathHelper.Lerp(startY, endY, t) + (int)(Math.Sin(t * length / wavelength * MathHelper.TwoPi) * amplitude);

			// Place the desired tile or perform any other action
			BoreCircle(x, y, radius + WorldGen.genRand.Next(-2, 3));

			t += 1f / length;
		}
	}

	public static void BoreCircle(int x, int y, float r)
	{
		int num = (int)(x - r);
		int num2 = (int)(y - r);
		int num3 = (int)(x + r);
		int num4 = (int)(y + r);
		for (int i = num; i < num3 + 1; i++)
		{
			for (int j = num2; j < num4 + 1; j++)
			{
				if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) <= r)
				{
					Main.tile[i, j].Active(false);
				}
			}
		}
	}

	public static void CrystalsFloor(int x, int y, int mult, int multAdd)
    {
        //int mult = WorldGen.genRand.Next(2, 4);
        // multadd = WorldGen.genRand.Next(1, 6);
        int pstep = 0;

        for (int pyY = y; pyY <= y + mult + multAdd; pyY++)
        {
            for (int pyX = x - pstep + WorldGen.genRand.Next(1, 4); pyX <= x + pstep + WorldGen.genRand.Next(1, 4); pyX++)
            {
                WorldGen.PlaceTile(pyX, pyY, ModContent.TileType<CrystalStoneCrystals>(), forced: true);
            }
            pstep++;
        }
    }

    public static void CrystalsCeiling(int x, int y, int mult, int multAdd)
    {
        //int mult = WorldGen.genRand.Next(2, 4);
        // multadd = WorldGen.genRand.Next(1, 6);
        int pstep = 0;

        for (int pyY = y + mult + multAdd; pyY >= y; pyY--)
        {
            for (int pyX = x - pstep + WorldGen.genRand.Next(1, 4); pyX <= x + pstep + WorldGen.genRand.Next(1, 4); pyX++)
            {
                WorldGen.PlaceTile(pyX, pyY, ModContent.TileType<CrystalStoneCrystals>(), forced: true);
            }
            pstep++;
        }
    }
    public static bool Place(Point origin)
    {
        int width = WorldGen.genRand.Next(100, 150);
        int height = 90;// WorldGen.genRand.Next(70, 90);

        for (int i = origin.X; i < origin.X + width; i++)
        {
            for (int j = origin.Y; j < origin.Y + height; j++)
            {
                if (i == origin.X || i == origin.X + width - 1)
                {
                    if (j % 12 == 0)
                    {
                        WorldGeneration.Utils.TileRunnerSpecial(i, j, WorldGen.genRand.Next(35, 45), WorldGen.genRand.Next(35, 45), ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }
                if (j == origin.Y || j == origin.Y + height - 1)
                {
                    if (i % 12 == 0)
                    {
                        WorldGeneration.Utils.TileRunnerSpecial(i, j, WorldGen.genRand.Next(35, 45), WorldGen.genRand.Next(35, 45), ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }
            }
        }


        bool doCap1 = WorldGen.genRand.NextBool(3);
        bool doCap2 = WorldGen.genRand.NextBool(3);
        bool doCap3 = WorldGen.genRand.NextBool(3);
        bool doCap4 = WorldGen.genRand.NextBool(3);
        bool doCap5 = WorldGen.genRand.NextBool(3);
        bool doCap6 = WorldGen.genRand.NextBool(3);
        bool doCap7 = WorldGen.genRand.NextBool(3);
        bool doCap8 = WorldGen.genRand.NextBool(3);

        int tunnelHeight = 10;

        // horizontal tunnel 1
        int tunnel1YPos = WorldGen.genRand.Next(5, 11);

        // horizontal tunnel 2
        int tunnel2YPos = WorldGen.genRand.Next(20, 28);

        // horizontal tunnel 3
        int tunnel3YPos = WorldGen.genRand.Next(39, 51);

        // horizontal tunnel 4
        int tunnel4YPos = WorldGen.genRand.Next(60, 70);

        // horizontal tunnel 5
        int tunnel5YPos = WorldGen.genRand.Next(74, 79);

        //Main.NewText(tunnel3YPos);
        //Main.NewText(tunnel4YPos);

        // first vertical tunnel
        int vTunnel1Width = WorldGen.genRand.Next(6, 11);
        int vTunnel1PosStart = origin.X + WorldGen.genRand.Next(12, width - 24); //origin.X + WorldGen.genRand.Next(width / 3 - vTunnel1Width, width / 3 - vTunnel1Width + 5) + WorldGen.genRand.Next(-10, 10);
        int vTunnel1PosEnd = vTunnel1PosStart + vTunnel1Width; //WorldGen.genRand.Next(width / 3 + vTunnel1Width, width / 3 + vTunnel1Width + 5) + WorldGen.genRand.Next(-10, 10);

        // second vertical tunnel
        int vTunnel2Width = WorldGen.genRand.Next(6, 11);
        int vTunnel2PosStart = origin.X + WorldGen.genRand.Next(12, width - 24); //origin.X + WorldGen.genRand.Next(width / 3 * 2 - vTunnel2Width, width / 3 * 2 - vTunnel2Width + 5) + WorldGen.genRand.Next(-10, 10);
        int vTunnel2PosEnd = vTunnel2PosStart + vTunnel2Width; //origin.X + WorldGen.genRand.Next(width / 3 * 2 + vTunnel2Width, width / 3 * 2 + vTunnel2Width + 5) + WorldGen.genRand.Next(-10, 10);

        // third vertical tunnel
        int vTunnel3Width = WorldGen.genRand.Next(6, 11);
        int vTunnel3PosStart = origin.X + WorldGen.genRand.Next(12, width - 24); //origin.X + WorldGen.genRand.Next(width / 3 * 2 - vTunnel2Width, width / 3 * 2 - vTunnel2Width + 5) + WorldGen.genRand.Next(-10, 10);
        int vTunnel3PosEnd = vTunnel3PosStart + vTunnel3Width;

        // fourth vertical tunnel
        int vTunnel4Width = WorldGen.genRand.Next(6, 11);
        int vTunnel4PosStart = origin.X + WorldGen.genRand.Next(12, width - 24); //origin.X + WorldGen.genRand.Next(width / 3 * 2 - vTunnel2Width, width / 3 * 2 - vTunnel2Width + 5) + WorldGen.genRand.Next(-10, 10);
        int vTunnel4PosEnd = vTunnel4PosStart + vTunnel4Width;

        // caps
        int cap1X = origin.X + width - 7;
        int cap2X = origin.X + 7;

        // place crystal stone crystals
        //for (int i = origin.X - 10; i < origin.X + width + 10; i++)
        //{
        //    for (int j = origin.Y - 5; j < origin.Y + height + 5; j++)
        //    {
        //        if (WorldGen.genRand.NextBool(200))
        //        {
        //            WorldGeneration.Utils.TileRunnerSpecial(i, j, WorldGen.genRand.Next(5, 12), WorldGen.genRand.Next(15, 25), ModContent.TileType<Tiles.CrystalMines.CrystalStoneCrystals>());
        //        }
        //    }
        //}

        //WorldGen.stopDrops = true;
        for (int i = origin.X; i < origin.X + width; i++)
        {
            for (int j = origin.Y; j < origin.Y + height; j++)
            {
                Tile tile = Main.tile[i, j];
                Tile tileBelow = Main.tile[i, j + 1];
                Tile tileAbove = Main.tile[i, j - 1];
                if (i > origin.X && i < origin.X + width && j > origin.Y && j < origin.Y + height && !Main.wallDungeon[tile.WallType] &&
                    tile.WallType != WallID.LihzahrdBrickUnsafe)
                {
                    tile.WallType = (ushort)ModContent.WallType<Walls.CrystalStoneWall>();
                    WorldGen.SquareWallFrame(i, j);
                }
                tile.LiquidAmount = 0;
                //if (Main.tileFrameImportant[tile.TileType] && tileBelow.HasTile ||
                //    Main.tileFrameImportant[tileAbove.TileType] && tile.HasTile) //Main.tileFrameImportant[tile.TileType]
                //{
                //    continue;
                //}
                if (tile.TileType == TileID.Containers || tile.TileType == TileID.Containers2 ||
                    tile.TileType == TileID.ShadowOrbs || tile.TileType == TileID.DemonAltar ||
                    tile.TileType == ModContent.TileType<SnotOrb>() || tile.TileType == TileID.LihzahrdBrick || tile.TileType == TileID.BlueDungeonBrick ||
                    tile.TileType == TileID.PinkDungeonBrick || tile.TileType == TileID.GreenDungeonBrick || //tile.TileType == ModContent.TileType<Tiles.TuhrtlBrick>() ||
                    tile.TileType == TileID.Statues || tile.TileType == ModContent.TileType<Tiles.PurpleBrick>() || tile.TileType == ModContent.TileType<Tiles.OrangeBrick>() ||
                    tile.TileType == ModContent.TileType<Tiles.CrackedOrangeBrick>() || tile.TileType == ModContent.TileType<Tiles.CrackedPurpleBrick>() ||
                    tile.WallType == WallID.LihzahrdBrickUnsafe || Main.wallDungeon[tile.WallType] || tile.TileType == TileID.Painting2X3 || tile.TileType == TileID.Painting3X2 ||
                    tile.TileType == TileID.Painting3X3 || tile.TileType == TileID.Painting4X3 || tile.TileType == TileID.Painting6X4 || Main.tileContainer[tile.TileType])
                {
                    //if (tile.HasTile && (tileAbove.TileType == TileID.Containers || tileAbove.TileType == TileID.Containers2 || Main.tileContainer[tileAbove.TileType]))
                    //{
                    //    continue;
                    //}
                    if (!Main.tileSolid[tile.TileType] || Main.tileFrameImportant[tile.TileType] || Main.tileDungeon[tile.TileType] ||
                        //tile.TileType == ModContent.TileType<Tiles.TuhrtlBrick>() || 
                        tile.TileType == TileID.LihzahrdBrick ||
                        Main.wallDungeon[tile.WallType] || ((tile.TileType == TileID.Containers || tile.TileType == TileID.Containers2 || Main.tileContainer[tile.TileType]) && tileBelow.HasTile))
                    {
                        continue;
                    }
                    //else
                    //if (tile.TileType != TileID.Dirt)
                    //    tile.ResetToType(tile.TileType);
                    //WorldGen.noTileActions = false;
                    //continue;
                }
                else if (tile.TileType == TileID.Cobweb)
                {
                    //tile.TileType = (ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStoneCrystals>();
                    //WorldGen.KillTile(i, j, noItem: true);
                    //tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalStone>());
                }
                else
                {
                    if (tile.TileType != ModContent.TileType<Tiles.CrystalMines.CrystalStoneCrystals>())
                        tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                }
                if (tile.WallType != WallID.LihzahrdBrickUnsafe && //tile.WallType != ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>() &&
                    !Main.wallDungeon[tile.WallType] && tile.WallType != WallID.LihzahrdBrickUnsafe)
                {
                    if (i > origin.X && i < origin.X + width && j > origin.Y && j < origin.Y + height)
                    {
                        tile.WallType = (ushort)ModContent.WallType<Walls.CrystalStoneWall>();
                        WorldGen.SquareWallFrame(i, j);
                    }
                }

                // crystal stone crystals
                if (j == origin.Y + tunnel1YPos + tunnelHeight ||
                    j == origin.Y + tunnel2YPos + tunnelHeight ||
                    j == origin.Y + tunnel3YPos + tunnelHeight ||
                    j == origin.Y + tunnel4YPos + tunnelHeight ||
                    j == origin.Y + tunnel5YPos + tunnelHeight)
                {
                    int mult = WorldGen.genRand.Next(2, 4);
                    int multAdd = WorldGen.genRand.Next(1, 4);

                    if (i % 7 == 0)
                    {
                        // add the floor blobs
                        CrystalsFloor(i, j - multAdd + 1, mult, multAdd);

                        // add more crystals below the blob
                        WorldGeneration.Utils.TileRunnerSpecial(i, j + 1, WorldGen.genRand.Next(2, 8), WorldGen.genRand.Next(2, 8), ModContent.TileType<Tiles.CrystalMines.CrystalStoneCrystals>());
                    }
                }
                if (j == origin.Y + tunnel1YPos ||
                    j == origin.Y + tunnel2YPos ||
                    j == origin.Y + tunnel3YPos ||
                    j == origin.Y + tunnel4YPos ||
                    j == origin.Y + tunnel5YPos)
                {
                    int mult = WorldGen.genRand.Next(2, 4);
                    int multAdd = WorldGen.genRand.Next(1, 4);

                    if (i % 7 == 0)
                    {
                        // add the floor blobs
                        CrystalsCeiling(i, j - multAdd - 1, mult, multAdd);

                        // add more crystals below the blob
                        //WorldGeneration.Utils.TileRunnerSpecial(i, j + 1, WorldGen.genRand.Next(2, 8), WorldGen.genRand.Next(2, 8), ModContent.TileType<Tiles.CrystalMines.CrystalStoneCrystals>());
                    }
                }


                #region horizontal tunnels
                if (j >= origin.Y + tunnel1YPos && j <= origin.Y + tunnel1YPos + tunnelHeight ||
                    j >= origin.Y + tunnel2YPos && j <= origin.Y + tunnel2YPos + tunnelHeight ||
                    j >= origin.Y + tunnel3YPos && j <= origin.Y + tunnel3YPos + tunnelHeight ||
                    j >= origin.Y + tunnel4YPos && j <= origin.Y + tunnel4YPos + tunnelHeight ||
                    j >= origin.Y + tunnel5YPos && j <= origin.Y + tunnel5YPos + tunnelHeight)
                {
                    WorldGen.noTileActions = true;
                    if (!Main.tileDungeon[tile.TileType] && !Main.wallDungeon[tile.WallType] && tile.TileType != TileID.LihzahrdBrick && tile.TileType != ModContent.TileType<Tiles.CrystalMines.CrystalStoneCrystals>())
                    {
                        //WorldGen.stopDrops = true;
                        tile.HasTile = false;
                        //WorldGen.KillTile(i, j, noItem: true);
                        //WorldGen.stopDrops = false;
                    }
                    WorldGen.noTileActions = false;
                }
                #endregion horizontal tunnels
                #region vertical tunnels
                WorldGen.noTileActions = true;
                if (j >= origin.Y + tunnel1YPos + tunnelHeight && j <= origin.Y + tunnel2YPos + 1 &&
                    i >= vTunnel1PosStart && i <= vTunnel1PosEnd)
                {
                    if (j == origin.Y + tunnel2YPos + 1 && !Main.wallDungeon[tile.WallType] && tile.TileType != TileID.LihzahrdBrick)
                    {
                        tile.HasTile = false;
                    }
                    if (!Main.tileDungeon[tile.TileType] && !Main.wallDungeon[tile.WallType] && tile.TileType != TileID.LihzahrdBrick)
                    {
                        tile.HasTile = false;
                    }
                }
                if (j >= origin.Y + tunnel2YPos + tunnelHeight && j <= origin.Y + tunnel3YPos + 1 &&
                    i >= vTunnel2PosStart && i <= vTunnel2PosEnd)
                {
                    if (j == origin.Y + tunnel3YPos + 1 && !Main.wallDungeon[tile.WallType] && tile.TileType != TileID.LihzahrdBrick)
                    {
                        tile.HasTile = false;
                    }
                    if (!Main.tileDungeon[tile.TileType] && !Main.wallDungeon[tile.WallType] && tile.TileType != TileID.LihzahrdBrick)
                    {
                        tile.HasTile = false;
                    }
                }
                if (j >= origin.Y + tunnel3YPos + tunnelHeight && j <= origin.Y + tunnel4YPos + 1 &&
                    i >= vTunnel3PosStart && i <= vTunnel3PosEnd)
                {
                    if (j == origin.Y + tunnel3YPos + tunnelHeight && !Main.wallDungeon[tile.WallType] && tile.TileType != TileID.LihzahrdBrick)
                    {
                        tile.HasTile = false;
                    }
                    if (!Main.tileDungeon[tile.TileType] && !Main.wallDungeon[tile.WallType] && tile.TileType != TileID.LihzahrdBrick)
                    {
                        tile.HasTile = false;
                    }
                }
                if (j >= origin.Y + tunnel4YPos + tunnelHeight && j <= origin.Y + tunnel5YPos + 1 &&
                    i >= vTunnel4PosStart && i <= vTunnel4PosEnd)
                {
                    if (j == origin.Y + tunnel4YPos + tunnelHeight && !Main.wallDungeon[tile.WallType] && tile.TileType != TileID.LihzahrdBrick)
                    {
                        tile.HasTile = false;
                    }
                    if (!Main.tileDungeon[tile.TileType] && !Main.wallDungeon[tile.WallType] && tile.TileType != TileID.LihzahrdBrick)
                    {
                        tile.HasTile = false;
                    }
                }
                WorldGen.noTileActions = false;
                #endregion vertical tunnels
                #region caps
                // right side caps
                if (j >= origin.Y + tunnel1YPos && j <= origin.Y + tunnel1YPos + tunnelHeight &&
                    i >= cap1X && doCap1)
                {
                    if (!blacklistedTiles.Contains(tile.TileType) && !blacklistedWalls.Contains(tile.WallType))
                    {
                        tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }
                if (j >= origin.Y + tunnel2YPos && j <= origin.Y + tunnel2YPos + tunnelHeight &&
                    i >= cap1X && doCap4)
                {
                    if (!blacklistedTiles.Contains(tile.TileType) && !blacklistedWalls.Contains(tile.WallType))
                    {
                        tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }
                if (j >= origin.Y + tunnel3YPos && j <= origin.Y + tunnel3YPos + tunnelHeight &&
                    i >= cap1X && doCap5)
                {
                    if (!blacklistedTiles.Contains(tile.TileType) && !blacklistedWalls.Contains(tile.WallType))
                    {
                        tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }
                if (j >= origin.Y + tunnel4YPos && j <= origin.Y + tunnel4YPos + tunnelHeight &&
                    i >= cap1X && doCap7)
                {
                    if (!blacklistedTiles.Contains(tile.TileType) && !blacklistedWalls.Contains(tile.WallType))
                    {
                        tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }
                // left side caps
                if (j >= origin.Y + tunnel1YPos && j <= origin.Y + tunnel1YPos + tunnelHeight &&
                    i <= cap2X && doCap3)
                {
                    if (!blacklistedTiles.Contains(tile.TileType) && !blacklistedWalls.Contains(tile.WallType))
                    {
                        tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }

                if (j >= origin.Y + tunnel2YPos && j <= origin.Y + tunnel2YPos + tunnelHeight &&
                    i <= cap2X && doCap2)
                {
                    if (!blacklistedTiles.Contains(tile.TileType) && !blacklistedWalls.Contains(tile.WallType))
                    {
                        tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }
                if (j >= origin.Y + tunnel3YPos && j <= origin.Y + tunnel3YPos + tunnelHeight &&
                    i <= cap2X && doCap6)
                {
                    if (!blacklistedTiles.Contains(tile.TileType) && !blacklistedWalls.Contains(tile.WallType))
                    {
                        tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }
                if (j >= origin.Y + tunnel4YPos && j <= origin.Y + tunnel4YPos + tunnelHeight &&
                    i <= cap2X && doCap8)
                {
                    if (!blacklistedTiles.Contains(tile.TileType) && !blacklistedWalls.Contains(tile.WallType))
                    {
                        tile.ResetToType((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>());
                    }
                }
                #endregion caps
            }
        }
        int spacingFloor1 = WorldGen.genRand.Next(10, 14);
        int spacingFloor2 = WorldGen.genRand.Next(10, 14);
        int spacingFloor3 = WorldGen.genRand.Next(10, 14);
        int spacingFloor4 = WorldGen.genRand.Next(10, 14);
        int spacingFloor5 = WorldGen.genRand.Next(10, 14);
        for (int i = origin.X; i < origin.X + width; i++)
        {
            for (int j = origin.Y; j < origin.Y + height; j++)
            {
                #region pillars
                if (j >= origin.Y + tunnel1YPos && j <= origin.Y + tunnel1YPos + tunnelHeight)
                {
                    if (i % spacingFloor1 == 0 && !Main.tile[i, j].HasTile && Main.tile[i, origin.Y + tunnel1YPos - 1].HasTile)
                    {
                        if (!Main.wallDungeon[Main.tile[i, j].WallType] && !blacklistedWalls.Contains(Main.tile[i, j].WallType))
                            WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrystalMines.CrystalColumn>(), true);
                    }
                }
                if (j >= origin.Y + tunnel2YPos && j <= origin.Y + tunnel2YPos + tunnelHeight)
                {
                    if (i % spacingFloor2 == 0 && !Main.tile[i, j].HasTile && Main.tile[i, origin.Y + tunnel2YPos - 1].HasTile)
                    {
                        if (!Main.wallDungeon[Main.tile[i, j].WallType] && !blacklistedWalls.Contains(Main.tile[i, j].WallType))
                            WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrystalMines.CrystalColumn>(), true);
                    }
                }
                if (j >= origin.Y + tunnel3YPos && j <= origin.Y + tunnel3YPos + tunnelHeight)
                {
                    if (i % spacingFloor3 == 0 && !Main.tile[i, j].HasTile && Main.tile[i, origin.Y + tunnel3YPos - 1].HasTile)
                    {
                        if (!Main.wallDungeon[Main.tile[i, j].WallType] && !blacklistedWalls.Contains(Main.tile[i, j].WallType))
                            WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrystalMines.CrystalColumn>(), true);
                    }
                }
                if (j >= origin.Y + tunnel4YPos && j <= origin.Y + tunnel4YPos + tunnelHeight)
                {
                    if (i % spacingFloor4 == 0 && !Main.tile[i, j].HasTile && Main.tile[i, origin.Y + tunnel4YPos - 1].HasTile)
                    {
                        if (!Main.wallDungeon[Main.tile[i, j].WallType] && !blacklistedWalls.Contains(Main.tile[i, j].WallType))
                            WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrystalMines.CrystalColumn>(), true);
                    }
                }
                if (j >= origin.Y + tunnel5YPos && j <= origin.Y + tunnel5YPos + tunnelHeight)
                {
                    if (i % spacingFloor5 == 0 && !Main.tile[i, j].HasTile && Main.tile[i, origin.Y + tunnel5YPos - 1].HasTile)
                    {
                        if (!Main.wallDungeon[Main.tile[i, j].WallType] && !blacklistedWalls.Contains(Main.tile[i, j].WallType))
                            WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrystalMines.CrystalColumn>(), true);
                    }
                }
                if (!Main.tile[i, j].HasTile && Main.tile[i, j - 1].HasTile && Main.tile[i, j - 1].TileType == ModContent.TileType<Tiles.CrystalMines.CrystalColumn>())
                {
                    WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrystalMines.CrystalColumn>(), true);
                }
                #endregion pillars
                if (!Main.tile[i, j].HasTile && Main.tile[i, j + 1].HasTile && (Main.tile[i, j + 1].TileType == ModContent.TileType<Tiles.CrystalMines.CrystalStone>() ||
                    Main.tile[i, j + 1].TileType == ModContent.TileType<Tiles.CrystalMines.CrystalStoneCrystals>()))
                {
                    if (Main.tile[i, j].TileType != ModContent.TileType<Tiles.CrystalMines.ShatterShards>())
                    {
                        if (WorldGen.genRand.NextBool(8))
                        {
                            WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrystalMines.ShatterShards>(), style: WorldGen.genRand.Next(3));
                        }
                    }
                }
            }
        }
        WorldGeneration.Utils.SquareTileFrameArea(origin.X, origin.Y, width, height);
        //WorldGen.stopDrops = false;
        return true;
    }
}
