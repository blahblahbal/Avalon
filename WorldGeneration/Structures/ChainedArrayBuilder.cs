using Avalon.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static log4net.Appender.ColoredConsoleAppender;

namespace Avalon.WorldGeneration.Structures;

class ChainedArrayBuilder
{
	//private enum GenDirections : byte
	//{
	//	Left,
	//	Right,
	//	Up,
	//	Down
	//}

	//public static int totalSegmentCount = 15;
	//public static int totalFloorCount = 6;
	//public static List<int[,]> floors = new List<int[,]>(totalSegmentCount * totalFloorCount);
	//public static int[,] sideWinderFloors = new int[totalSegmentCount, totalFloorCount];
	private enum sideWinderStates : byte
	{
		Default,
		RightBlocked,
		DefaultDownThrough,
		RightBlockedDownThrough
	}
	private const bool LeftConnection = true;
	private const bool RightConnection = true;
	private const bool UpConnection = true;
	private const bool DownConnection = true;
	//private enum sideWinderStates : byte
	//{
	//	Left,
	//	//Right,
	//	//Up,
	//	//Down,
	//	Left_Right,
	//	//Left_Up,
	//	Left_Down,
	//	//Right_Up,
	//	//Right_Down,
	//	//Up_Down,
	//	//Left_Right_Up,
	//	Left_Right_Down,
	//	//Left_Up_Down,
	//	//Right_Up_Down,
	//	//Left_Right_Up_Down
	//}
	//private enum fortressSpecialStates : byte
	//{
	//	Centre = 100,
	//	CentreRightBlocked,
	//	CentreDownThrough,
	//	CentreRightBlockedDownThrough
	//}

	public static void NewChainedStructure(int x, int y, int totalSegmentCount, int lowerHallsFloorCount, int middleHallsFloorCount, int upperHallsFloorCount, /*int totalFloorCount, */int wallChance, int maxWalls, int topFloorMaxWalls, int gapBetweenWalls, int initialGapBetweenWalls, int finalGapBetweenWalls)
	{
		int totalFloorCount = lowerHallsFloorCount + middleHallsFloorCount + upperHallsFloorCount;
		List<int[,]> floors = new List<int[,]>(totalSegmentCount * totalFloorCount);
		int[,] sideWinderFloors = new int[totalSegmentCount, totalFloorCount];
		ushort typeTile = 0;
		ushort typeWall = 0;

		int PosX = x;
		int PosY = y;
		// todo: make the bottom two floors (on large worlds) create random flip points where they go up/down
		// not sure if feasible on small/medium worlds since they likely won't have two entrance hall floors (not even sure if large worlds should)

		// okay for the note above, if I make this generate twice, once for before the central pillar, and once after, then I would be able to satisfy that condition
		// don't think I would need to do the upper floors separately either that way in order to achieve it? I'd just only modify the central pillar on the bottom halls
		for (int curFloor = totalFloorCount - 1; curFloor >= 0; curFloor--)
		{
			int previousWall = -1;
			int totalWalls = 0;
			if (curFloor == 0)
			{
				maxWalls = topFloorMaxWalls;
			}
			for (int curSegment = 0; curSegment < totalSegmentCount; curSegment++)
			{
				if (curFloor == totalFloorCount - 1)
				{
					if (curSegment == totalSegmentCount - 1)
					{
						sideWinderFloors[curSegment, curFloor] = (int)sideWinderStates.RightBlocked;
					}
					else
					{
						sideWinderFloors[curSegment, curFloor] = (int)sideWinderStates.Default;
					}
				}
				else if ((totalWalls < maxWalls && curSegment - previousWall > gapBetweenWalls - 1 && curSegment < totalSegmentCount - finalGapBetweenWalls && WorldGen.genRand.NextBool(wallChance)) ||
					curSegment == totalSegmentCount - 1 ||
					(totalWalls < maxWalls && curSegment < gapBetweenWalls && curSegment >= initialGapBetweenWalls - 1 && previousWall == -1 && WorldGen.genRand.NextBool(wallChance)))
				{
					sideWinderFloors[curSegment, curFloor] = (int)sideWinderStates.RightBlocked;
					int currentWall = previousWall == curSegment - 1 ? curSegment : WorldGen.genRand.Next(previousWall + 1, curSegment + 1);
					if (sideWinderFloors[currentWall, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)
					{
						sideWinderFloors[currentWall, curFloor] = (int)sideWinderStates.RightBlockedDownThrough;
					}
					else
					{
						sideWinderFloors[currentWall, curFloor] = (int)sideWinderStates.DefaultDownThrough;
					}
					previousWall = curSegment;
					totalWalls++;
				}
				else
				{
					sideWinderFloors[curSegment, curFloor] = (int)sideWinderStates.Default;
				}
			}
		}

		PosY -= SkyFortressArrays.Cells.Right.GetLength(0) - 1;

		floors.Clear();
		int leftCentre = Math.Min(totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1), totalSegmentCount / 2);
		int rightCentre = Math.Max(totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1), totalSegmentCount / 2);
		bool blockedCentreSide = WorldGen.genRand.NextBool(); // which side of the centre starts as possibly blocked downwards
		int hiddenRoomCount = 0; // counts how many hidden rooms exist

		List<(int, int, bool)> hiddenRoomLocations = ChooseHiddenRoomLocationsFromValid(totalSegmentCount, lowerHallsFloorCount, middleHallsFloorCount, upperHallsFloorCount, floors, sideWinderFloors, blockedCentreSide, leftCentre, rightCentre);

		Main.NewText("hidden rooms allocated: " + hiddenRoomLocations.Count); // it only happens very rarely, it'll be easier to make sure it never happens if I set the hidden rooms in an entirely different loop BEFORE setting regular/central, modifying the cell left of hidden rooms to blockright/blockrightdownthrough, and rightmost of hidden rooms to blockright
		if (hiddenRoomLocations.Count < 2)
		{
			Main.NewText("guh??");
		}
		else
		{
			Main.NewText("order before sort: " + hiddenRoomLocations[0] + " _ " + hiddenRoomLocations[1], Color.Gray);
			hiddenRoomLocations.Sort(CompareSegmentAndFloor);
			//List<(int, int, bool)> test = [(13, 2, true), (3, 2, false)];
			//Main.NewText("test unsorted: " + test[0] + " _ " + test[1]);
			//test.Sort(CompareSegmentAndFloor);
			//Main.NewText("test sorted: " + test[0] + " _ " + test[1]);
			Main.NewText("order after sort: " + hiddenRoomLocations[0] + " _ " + hiddenRoomLocations[1], Color.Gray);
		}

		// sets the edges of hidden rooms to be blocked off
		foreach (var location in hiddenRoomLocations)
		{
			if (location.Item1 > 0)
			{
				if (sideWinderFloors[location.Item1 - 1, location.Item2] == (int)sideWinderStates.Default)
				{
					sideWinderFloors[location.Item1 - 1, location.Item2] = (int)sideWinderStates.RightBlocked;
				}
				else if (sideWinderFloors[location.Item1 - 1, location.Item2] == (int)sideWinderStates.DefaultDownThrough)
				{
					sideWinderFloors[location.Item1 - 1, location.Item2] = (int)sideWinderStates.RightBlockedDownThrough;
				}
			}
			sideWinderFloors[location.Item1 + (location.Item3 == false ? 1 : 2), location.Item2] = (int)sideWinderStates.RightBlocked;
		}

		// loop for regular and central cells, as well as placing cells
		for (int curFloor = totalFloorCount - 1; curFloor >= 0; curFloor--)
		{
			blockedCentreSide = !blockedCentreSide; // flips for each floor
			int hiddenCellCountdown = 0;

			var selectedCell = SkyFortressArrays.Cells.LeftRightUpDown;
			for (int curSegment = 0; curSegment < totalSegmentCount; curSegment++)
			{
				//Main.NewText(curSegment + "_" + curFloor);
				bool flipped = false;
				// if the 3 wide room selected is placed after the 2 wide's position, the 2 wide will not gen
				// hiddenRoomCount can't be used as the index of hiddenRoomLocations
				if (hiddenRoomCount < hiddenRoomLocations.Count && hiddenCellCountdown == 0 && curSegment == hiddenRoomLocations[hiddenRoomCount].Item1 && curFloor == hiddenRoomLocations[hiddenRoomCount].Item2)
				{
					hiddenCellCountdown = 2 + (hiddenRoomLocations[hiddenRoomCount].Item3 == true ? 1 : 0);
					hiddenRoomCount++; // increment AFTER any checks involving this counter
				}
				if (hiddenCellCountdown > 0)
				{
					selectedCell = SkyFortressArrays.Cells.Filled;
					hiddenCellCountdown--;
					typeTile = (ushort)ModContent.TileType<Tiles.ImperviousBrick>();
				}
				else if (curSegment == totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1) || curSegment == totalSegmentCount / 2)
				{
					// todo; make this set flipped too instead of using different left/right cells
					selectedCell = FortressCentralCells(selectedCell, totalSegmentCount, totalFloorCount, floors, sideWinderFloors, curFloor, curSegment, blockedCentreSide);
					typeTile = (ushort)ModContent.TileType<Tiles.SkyBrick>();
				}
				else
				{
					var a = FortressRegularCells(totalSegmentCount, lowerHallsFloorCount, middleHallsFloorCount, upperHallsFloorCount, floors, sideWinderFloors, curFloor, curSegment, flipped, hiddenRoomLocations, leftCentre, rightCentre);
					selectedCell = a.outCell;
					flipped = a.flipped;
					typeTile = (ushort)ModContent.TileType<Tiles.SkyBrick>();
				}
				floors.Add(selectedCell);
				PlaceCell(PosX, PosY, totalSegmentCount, totalFloorCount, floors, curFloor, curSegment, typeTile, typeWall, flipped);

				PosX += floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(1);
			}
			for (int i = 0; i < totalSegmentCount; i++)
			{
				PosX -= floors[totalSegmentCount * (totalFloorCount - 1 - curFloor) + i].GetLength(1);
			}
			PosY -= floors[totalSegmentCount * (totalFloorCount - 1 - curFloor)].GetLength(0);
		}
		#region old loop
		//for (int curFloor = totalFloorCount - 1; curFloor >= 0; curFloor--)
		//{
		//	blockedCentreSide = !blockedCentreSide; // flips for each floor
		//	bool hiddenRoomValid = false; // true if current cell & next 2 cells is valid for placing a hidden room
		//	int hiddenCellCountdown = 0; // > 0 if placing hidden rooms, equal to remaining hidden room cells to place for each hidden room
		//	bool hiddenCellDelay = false; // delays the placement of a hidden room for the 2 wide rooms, so they don't leave gaps on the right side
		//								  //int hiddenRoomsOnCurrentFloor = 0;

		//	var selectedCell = CellArrays._structureTilesLeftRightTopBottom;
		//	for (int curSegment = 0; curSegment < totalSegmentCount; curSegment++)
		//	{
		//		bool flipped = false;
		//		var b = (false, 0, false);
		//		if (hiddenCellCountdown == 0 && !hiddenRoomValid && hiddenRoomCount < 2/* && (curFloor != 0 ? hiddenRoomsOnCurrentFloor == 0 : true)*/)
		//		{
		//			// I think I should be doing this in a different loop before the regular and central cells, so that the left/right sides of hiddem rooms aren't connected to
		//			b = HiddenRoomValidCheck(selectedCell, totalSegmentCount, totalFloorCount, floors, sideWinderFloors, curFloor, curSegment);
		//			hiddenRoomValid = b.Item1;
		//			hiddenCellCountdown = b.Item2;
		//			hiddenCellDelay = b.Item3;
		//		}
		//		if (hiddenCellCountdown == b.Item2 &&
		//			((curSegment == totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1) || curSegment == totalSegmentCount / 2) ||
		//			(curSegment + 1 == totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1) || curSegment + 1 == totalSegmentCount / 2) ||
		//			(curSegment + 2 == totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1) || curSegment + 2 == totalSegmentCount / 2)))
		//		{
		//			hiddenCellCountdown = 0;
		//		}
		//		if (curSegment == totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1) || curSegment == totalSegmentCount / 2)
		//		{
		//			// todo; make this set flipped too instead of using different left/right cells
		//			selectedCell = FortressCentralCells(selectedCell, totalSegmentCount, totalFloorCount, floors, sideWinderFloors, curFloor, curSegment, blockedCentreSide);
		//			hiddenCellCountdown = 0;
		//			if (hiddenRoomCount < 2 && hiddenRoomValid)
		//			{
		//				hiddenRoomValid = false;
		//				//hiddenRoomsOnCurrentFloor = 1;
		//			}
		//		}
		//		else if (hiddenRoomValid && hiddenCellCountdown > 0 && hiddenCellDelay == false)
		//		{
		//			//var a = FortressHiddenCells(selectedCell, totalSegmentCount, totalFloorCount, floors, sideWinderFloors, curFloor, curSegment, flipped);
		//			//selectedCell = a.outCell;
		//			//flipped = a.flipped;

		//			selectedCell = CellArrays._structureFilled;
		//			hiddenCellCountdown--;
		//			if (hiddenCellCountdown == 0)
		//			{
		//				hiddenRoomCount++;
		//			}
		//		}
		//		else
		//		{
		//			var a = FortressRegularCells(floors, sideWinderFloors, curFloor, curSegment, flipped);
		//			selectedCell = a.outCell;
		//			flipped = a.flipped;
		//			if (hiddenCellDelay == false)
		//			{
		//				hiddenCellCountdown = 0;
		//			}
		//		}
		//		//if (hiddenCellCountdown > 0 && hiddenCellDelay)
		//		//{
		//		//	hiddenCellDelay = false;
		//		//	hiddenCellCountdown--;
		//		//}
		//		hiddenCellDelay = false;
		//		floors.Add(selectedCell);
		//		PlaceCell(PosX, PosY, totalSegmentCount, totalFloorCount, floors, curFloor, curSegment, typeTile, typeWall, flipped);

		//		PosX += floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(1);
		//		if (curFloor == 0 && curSegment == totalSegmentCount - 1 && hiddenRoomCount != 2)
		//		{
		//			Main.NewText("less than two hidden rooms generated"); // it only happens very rarely, it'll be easier to make sure it never happens if I set the hidden rooms in an entirely different loop BEFORE setting regular/central, modifying the cell left of hidden rooms to blockright/blockrightdownthrough, and rightmost of hidden rooms to blockright
		//		}
		//	}
		//	for (int i = 0; i < totalSegmentCount; i++)
		//	{
		//		PosX -= floors[totalSegmentCount * (totalFloorCount - 1 - curFloor) + i].GetLength(1);
		//	}
		//	PosY -= floors[totalSegmentCount * (totalFloorCount - 1 - curFloor)].GetLength(0);
		//	Main.NewText(hiddenRoomValid);
		//}
		#endregion old loop
		if (hiddenRoomCount != hiddenRoomLocations.Count)
		{
			Main.NewText("less than allocated hidden rooms generated: " + hiddenRoomCount, Color.Red);
		}
		else if (hiddenRoomLocations.Count == 1)
		{
			Main.NewText("Not enough valid spaces for " + (hiddenRoomLocations[0].Item3 == true ? 2 : 3) + " wide", Color.Red);
		}
		else
		{
			Main.NewText("success", Color.Green);
		}
		//Console.WriteLine("\n\n" + String.Join(", ", sideWinderFloors.Cast<int>()));
	}

	public static void PlaceCell(int PosX, int PosY, int totalSegmentCount, int totalFloorCount, List<int[,]> floors, int curFloor, int curSegment, ushort typeTile, ushort typeWall, bool flipped)
	{
		typeWall = (ushort)ModContent.WallType<Walls.SkyBrickWallUnsafe>();
		//if (curSegment % 2 == 0)
		//{
		//	typeTile = (ushort)ModContent.TileType<Tiles.SkyBrick>();
		//	typeWall = (ushort)ModContent.WallType<Walls.SkyBrickWallUnsafe>();
		//}
		//else
		//{
		//	typeTile = (ushort)ModContent.TileType<Tiles.SkyBrick>();
		//	typeWall = (ushort)ModContent.WallType<Walls.SkyBrickWallUnsafe>();
		//}
		for (int confirmPlatforms = 0; confirmPlatforms < 3; confirmPlatforms++) //Increase the iterations on this outermost for loop if tabletop-objects are not properly spawning
		{
			for (int i = 0; i < floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(0); i++)
			{
				//var a = floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(1);
				//var b = flipped ? 0 : a - 1;
				//var c = flipped ? 1 : -1;
				//for (int j = b; flipped ? j < a : j >= 0; j += c)
				for (int j = floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(1) - 1; j >= 0; j--)
				{
					int k = PosX + (flipped ? floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(1) - 1 - j : j);
					//int k = PosX + j;
					int l = PosY + i;
					if (WorldGen.InWorld(k, l, 30))
					{
						Tile tile = Framing.GetTileSafely(k, l);
						switch (floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))][i, j])
						{
							case 0:
								tile.HasTile = false;
								tile.SkipLiquid = true;
								tile.LiquidAmount = 0;
								break;
							case 1:
								tile.HasTile = true;
								tile.TileType = typeTile;
								tile.Slope = 0;
								tile.IsHalfBlock = false;
								tile.SkipLiquid = true;
								tile.LiquidAmount = 0;
								break;
							case 2:
								tile.HasTile = true;
								tile.TileType = (ushort)ModContent.TileType<SkyBrickColumn>();
								tile.Slope = 0;
								tile.IsHalfBlock = false;
								tile.SkipLiquid = true;
								tile.LiquidAmount = 0;
								break;
						}
					}
				}
			}
		}
		var wallCell = SkyFortressArrays.Cells._structureWalls;
		if (curSegment == totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1) || curSegment == totalSegmentCount / 2)
		{
			wallCell = SkyFortressArrays.CentralCells._structureWallsCentral;
		}
		for (int i = 0; i < wallCell.GetLength(0); i++)
		{
			for (int j = wallCell.GetLength(1) - 1; j >= 0; j--)
			{
				int k = PosX + j;
				int l = PosY + i;
				if (WorldGen.InWorld(k, l, 30))
				{
					Tile tile = Framing.GetTileSafely(k, l);
					switch (wallCell[i, j])
					{
						case 0:
							tile.WallType = typeWall;
							break;
					}
				}
			}
		}

		Utils.SquareTileFrameArea(PosX, PosY, floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(1), floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(0));
		Utils.SquareWallFrameArea(PosX, PosY, floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(1), floors[curSegment + (totalSegmentCount * (totalFloorCount - 1 - curFloor))].GetLength(0));
	}

	public static List<(int x, int y, bool is3Width)> ChooseHiddenRoomLocationsFromValid(int totalSegmentCount, int lowerHallsFloorCount, int middleHallsFloorCount, int upperHallsFloorCount, /*int totalFloorCount, */List<int[,]> floors, int[,] sideWinderFloors, bool blockedCentreSide, int leftCentre, int rightCentre)
	{
		int totalFloorCount = lowerHallsFloorCount + middleHallsFloorCount + upperHallsFloorCount;
		List<(int, int)> validCoords2 = new List<(int, int)>(totalSegmentCount * totalFloorCount);
		List<(int, int)> validCoords3 = new List<(int, int)>(totalSegmentCount * totalFloorCount);
		List<(int, int, bool)> hiddenRoomLocations = new List<(int, int, bool)>();
		//bool has2WidthHiddenRoom = false;
		//bool has3WidthHiddenRoom = false;

		validCoords2.Clear();
		validCoords3.Clear();

		// loops for hidden rooms
		for (int curFloor = totalFloorCount - 1 - lowerHallsFloorCount; curFloor >= 0; curFloor--)
		{
			for (int curSegment = 0; curSegment < totalSegmentCount; curSegment++)
			{
				var a = HiddenRoomValidCheck(totalSegmentCount, lowerHallsFloorCount, middleHallsFloorCount, upperHallsFloorCount, floors, sideWinderFloors, curFloor, curSegment, blockedCentreSide, leftCentre, rightCentre);
				if (a.validFor2Width)
				{
					validCoords2.Add((curSegment, curFloor));
				}
				if (a.validFor3Width)
				{
					validCoords3.Add((curSegment, curFloor));
				}
			}
		}
		//var chosen3WidthHiddenRoom = (0, 0);
		if (validCoords3.Count != 0)
		{
			//has3WidthHiddenRoom = true;
			var chosenCoords3 = WorldGen.genRand.Next(validCoords3);
			hiddenRoomLocations.Add((chosenCoords3.Item1, chosenCoords3.Item2, true));
			// remove the cells from the valid spaces for 3 wide rooms to spawn in (unused, would be used if more 3 width rooms could spawn, which probably also wouldn't work atm idk)
			//validFor3WidthHiddenCoords.Remove(chosen3WidthHiddenRoom);
			//validFor3WidthHiddenCoords.Remove((chosen3WidthHiddenRoom.Item1 + 1, chosen3WidthHiddenRoom.Item2));
			//validFor3WidthHiddenCoords.Remove((chosen3WidthHiddenRoom.Item1 + 2, chosen3WidthHiddenRoom.Item2));

			// remove the cells from the valid spaces for 2 wide rooms to spawn in
			validCoords2.Remove((chosenCoords3.Item1 - 1, chosenCoords3.Item2));
			validCoords2.Remove(chosenCoords3);
			validCoords2.Remove((chosenCoords3.Item1 + 1, chosenCoords3.Item2));
			validCoords2.Remove((chosenCoords3.Item1 + 2, chosenCoords3.Item2));
		}
		else if (validCoords3.Count == 0)
		{
			if (validCoords2.Count == 1)
			{
				Main.NewText("oh.");
			}
			var chosenCoords2 = WorldGen.genRand.Next(validCoords2);
			hiddenRoomLocations.Add((chosenCoords2.Item1, chosenCoords2.Item2, false));
			// remove the cells from the valid spaces for 2 wide rooms to spawn in
			validCoords2.Remove((chosenCoords2.Item1 - 1, chosenCoords2.Item2));
			validCoords2.Remove(chosenCoords2);
			validCoords2.Remove((chosenCoords2.Item1 + 1, chosenCoords2.Item2));
		}
		//var chosen2WidthHiddenRoom = (0, 0);
		if (validCoords2.Count != 0)
		{
			var chosenCoords2 = WorldGen.genRand.Next(validCoords2);
			hiddenRoomLocations.Add((chosenCoords2.Item1, chosenCoords2.Item2, false));
		}
		else if (validCoords2.Count == 0)
		{
			Main.NewText("huh?");
		}
		return hiddenRoomLocations;
		//return new List<(int x, int y, bool is3Width)>();
	}

	public static int CompareSegmentAndFloor((int, int, bool) x, (int, int, bool) y)
	{
		// from what I can tell, different returns do this:
		// -1: x < y, pretty sure it leaves them in place
		//  0: x == y, unsure how it differs from above for int comparisons
		//  1: x > y, sorts x after y
		if (x.Item1 > y.Item1 && x.Item2 == y.Item2)
		{
			return 1;
		}
		if (x.Item2 < y.Item2)
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	public static (bool validFor2Width, bool validFor3Width) HiddenRoomValidCheck(/*int[,] selectedCell, */int totalSegmentCount, int lowerHallsFloorCount, int middleHallsFloorCount, int upperHallsFloorCount, /*int totalFloorCount, */List<int[,]> floors, int[,] sideWinderFloors, int curFloor, int curSegment, bool blockedCentreSide, int leftCentre, int rightCentre)
	{
		int totalFloorCount = lowerHallsFloorCount + middleHallsFloorCount + upperHallsFloorCount;
		// top floor isn't checking for valid spaces properly when it comes to halls open to the top from the central pillars, screenshot name: dotnet_thkNSNAI2Q.png, 2024-10
		// it properly assigned a space to a 2 wide room, but NOT a 3 wide room here, screenshot name: dotnet_exNsrQoYq7.png, 2024-10
		// another room with 0 valid 3 spots with potential space on top floor assigned to neither, had only 3 valid 2 spaces too: dotnet_GJpg6zoWcv.png
		// this one GENUINELY doesn't have space for 3 wide rooms: dotnet_8Qt8FAalgh.png

		// blockedCentreSide corresponding to the top floor will be the same as received here if an even amount of totalFloors, and opposite if an odd amount
		bool isLeftCentreTopFloorBlockedDownwards = (totalFloorCount % 2 == 0 ? blockedCentreSide == false : blockedCentreSide == true) && (sideWinderFloors[leftCentre, 0] == (int)sideWinderStates.RightBlocked);
		bool isRightCentreTopFloorBlockedDownwards = (totalFloorCount % 2 == 0 ? blockedCentreSide == true : blockedCentreSide == false) && (sideWinderFloors[leftCentre, 0] == (int)sideWinderStates.RightBlocked) && (sideWinderFloors[rightCentre, 0] == (int)sideWinderStates.Default);

		if (curSegment == leftCentre || curSegment == rightCentre || curSegment + 1 == leftCentre) // return if overlapping centre pillars
		{
			return (false, false);
		}
		bool validFor2Width = false;
		// special checks for the top floor in order to make sure that there will ALWAYS be two rooms genned, as there will always be unblocked spaces on the top floor
		// missing a bit of logic to make sure it doesn't block off any cells

		// for the left side, check all cells left for both downThrough and blockedRight
		// if downThrough is found first, it's true, if blockedRight is found first, it's false, if neither is found, it's false
		// then check all cells until leftCentre to the right, under the same conditions except that if neither is found, it's true
		// only valid if both left and right checks are true
		// vice versa for the right side
		if (curFloor == 0)
		{
			if (curSegment < leftCentre && isLeftCentreTopFloorBlockedDownwards == false)
			{
				if (curSegment < totalSegmentCount - 1 &&
				sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default)
				{
					bool leftCheck = false;
					bool rightCheck = false;
					for (int i = curSegment; i >= 0; i--)
					{
						if (sideWinderFloors[i, curFloor] == (int)sideWinderStates.DefaultDownThrough)
						{
							leftCheck = true;
							break;
						}
						if (sideWinderFloors[i, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)
						{
							leftCheck = false;
							break;
						}
						if (i == 0)
						{
							leftCheck = false;
						}
					}
					for (int i = curSegment; i < leftCentre; i++)
					{
						if (sideWinderFloors[i, curFloor] is (int)sideWinderStates.DefaultDownThrough or (int)sideWinderStates.RightBlockedDownThrough)
						{
							rightCheck = true;
							break;
						}
						if (sideWinderFloors[i, curFloor] == (int)sideWinderStates.RightBlocked)
						{
							rightCheck = false;
							break;
						}
						if (i == leftCentre - 1)
						{
							rightCheck = true;
						}
					}
					if (leftCheck == true && rightCheck == true)
					{
						validFor2Width = true;
					}
				}
				if (validFor2Width == true && curSegment + 2 != leftCentre && curSegment < totalSegmentCount - 2 &&
				sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.Default)
				{
					return (validFor2Width, true);
				}
			}
			else if (curSegment > rightCentre && isRightCentreTopFloorBlockedDownwards == false)
			{
				if (curSegment < totalSegmentCount - 1 &&
				sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default)
				{
					bool leftCheck = false;
					bool rightCheck = false;
					for (int i = curSegment; i < totalSegmentCount; i++)
					{
						if (sideWinderFloors[i, curFloor] is (int)sideWinderStates.DefaultDownThrough or (int)sideWinderStates.RightBlockedDownThrough)
						{
							rightCheck = true;
							break;
						}
						if (sideWinderFloors[i, curFloor] == (int)sideWinderStates.RightBlocked)
						{
							rightCheck = false;
							break;
						}
						if (i == totalSegmentCount - 1)
						{
							rightCheck = false;
						}
					}
					for (int i = curSegment; i >= rightCentre; i--)
					{
						if (sideWinderFloors[i, curFloor] == (int)sideWinderStates.DefaultDownThrough)
						{
							leftCheck = true;
							break;
						}
						if (sideWinderFloors[i, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)
						{
							leftCheck = false;
							break;
						}
						if (i == rightCentre)
						{
							leftCheck = true;
						}
					}
					if (leftCheck == true && rightCheck == true)
					{
						validFor2Width = true;
					}
				}
				if (validFor2Width == true && curSegment < totalSegmentCount - 2 &&
				(sideWinderFloors[curSegment + 2, curFloor] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked))
				{
					return (validFor2Width, true);
				}
			}
			//if (curSegment < leftCentre && isLeftCentreTopFloorBlockedDownwards == false)
			//{
			//	if (curSegment + 2 == leftCentre &&
			//	sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
			//	sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default)
			//	{
			//		validFor2Width = true;
			//	}
			//	if (curSegment + 3 == leftCentre &&
			//	sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.Default)
			//	{
			//		return (validFor2Width, true);
			//	}
			//}
			//else if (curSegment > rightCentre && isRightCentreTopFloorBlockedDownwards == false)
			//{
			//	if (curSegment < totalSegmentCount - 1 &&
			//	sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
			//	sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default)
			//	{
			//		validFor2Width = true;
			//	}
			//	if (validFor2Width == true && curSegment < totalSegmentCount - 2 &&
			//	(sideWinderFloors[curSegment + 2, curFloor] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked))
			//	{
			//		return (validFor2Width, true);
			//	}
			//}
		}
		// regular checks for other floors, excluding the bottom halls
		if (curFloor < totalFloorCount - lowerHallsFloorCount)
		{
			if ((curSegment == 0 &&
				sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default)
					||
				(curSegment > 0 && curSegment < totalSegmentCount - 1 &&
				(sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough) &&
				sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default)
					||
				(curSegment > 0 && curSegment < totalSegmentCount - 1 &&
				sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.RightBlocked))
			{
				if (curFloor == 0)
				{
					validFor2Width = true;
				}
				else if ((sideWinderFloors[curSegment, curFloor - 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked) &&
					(sideWinderFloors[curSegment + 1, curFloor - 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked))
				{
					validFor2Width = true;
				}
			}
		}
		if (curSegment + 2 == leftCentre) // return before validating 3 width if third cell overlapping centre pillars
		{
			return (validFor2Width, false);
		}
		if (curFloor < totalFloorCount - lowerHallsFloorCount)
		{
			if ((curSegment == 0 &&
				sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.Default)
					||
				(curSegment > 0 && curSegment < totalSegmentCount - 2 &&
				(sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough) &&
				sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.Default)
					||
				(curSegment > 0 && curSegment < totalSegmentCount - 2 &&
				sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default &&
				sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.RightBlocked))
			{
				if (curFloor == 0)
				{
					if (validFor2Width == false && sideWinderFloors[curSegment + 2, curFloor] != (int)sideWinderStates.RightBlocked)
					{
						Main.NewText("buh");
					}
					return (validFor2Width, true);
				}
				else if ((sideWinderFloors[curSegment, curFloor - 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked) &&
					(sideWinderFloors[curSegment + 1, curFloor - 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked) &&
					(sideWinderFloors[curSegment + 2, curFloor - 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked))
				{
					if (validFor2Width == false && sideWinderFloors[curSegment + 2, curFloor] != (int)sideWinderStates.RightBlocked)
					{
						Main.NewText("bug");
					}
					return (validFor2Width, true);
				}
			}
		}
		if (validFor2Width)
		{
			return (true, false);
		}
		return (false, false);
	}
	//public static (bool valid, int width, bool delay) HiddenRoomValidCheck(/*int[,] selectedCell, */int totalSegmentCount, int totalFloorCount, List<int[,]> floors, int[,] sideWinderFloors, int curFloor, int curSegment)
	//{
	//	bool roomSize = WorldGen.genRand.NextBool();
	//	if (curFloor < totalFloorCount - 2)
	//	{
	//		// todo: move the curSegment+2 checks to after, and use the result to determine which hidden room to place (randomise a bit probably too cause it'll be weighted towards 3 wide)
	//		// otherwise I THINK this works, haven't tested in game yet though since I haven't done the method for selecting the cells yet
	//		// separate the + 2 rightblocked check so I can do custom logic to make the 2 wide rooms work properly, doesn't really matter that it's checking for 3 available cells

	//		// okay so it actually does matter, look for dotnet_hIqULVShDv.png in Documents\ShareX\Screenshots\2024-10, it shows a layout with only a single valid position (left it in game too)
	//		// to fix, the top floor needs special checks I think
	//		// specifically, it needs to be able to place a hidden room between a side connected central pillar and a bottom connected hall piece
	//		// and to be able to place hidden rooms on the far left/right if two spaces are free
	//		if ((curSegment == 0 &&
	//			sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
	//			sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default &&
	//			sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.Default)
	//				||
	//			(curSegment > 0 && curSegment < totalSegmentCount - 2 &&
	//			(sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough) &&
	//			sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
	//			sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default &&
	//			sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.Default)
	//				||
	//			(curSegment > 0 && curSegment < totalSegmentCount - 2 &&
	//			sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default &&
	//			sideWinderFloors[curSegment + 1, curFloor] == (int)sideWinderStates.Default &&
	//			sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.RightBlocked))
	//		{
	//			if (curFloor == 0)
	//			{
	//				if (roomSize)
	//				{
	//					return (true, 3, false);
	//				}
	//				else if (sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.RightBlocked)
	//				{
	//					return (true, 2, true);
	//				}
	//				else
	//				{
	//					return (true, 2, false);
	//				}
	//			}
	//			else if ((sideWinderFloors[curSegment, curFloor - 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked) &&
	//				(sideWinderFloors[curSegment + 1, curFloor - 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked) &&
	//				(sideWinderFloors[curSegment + 2, curFloor - 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked))
	//			{
	//				if (roomSize)
	//				{
	//					return (true, 3, false);
	//				}
	//				else if (sideWinderFloors[curSegment + 2, curFloor] == (int)sideWinderStates.RightBlocked)
	//				{
	//					return (true, 2, true);
	//				}
	//				else
	//				{
	//					return (true, 2, false);
	//				}
	//			}
	//		}
	//	}
	//	return (false, 0, false);
	//}
	public static (int[,] outCell, bool flipped) FortressHiddenCells(int[,] selectedCell, int totalSegmentCount, int totalFloorCount, List<int[,]> floors, int[,] sideWinderFloors, int curFloor, int curSegment, bool flipped)
	{
		return (selectedCell, flipped);
	}
	//public static (int segmentsToOverwrite, bool flipped, bool hiddenCell) FortressHiddenCells(int[,] selectedCell, int totalSegmentCount, int totalFloorCount, List<int[,]> floors, int[,] sideWinderFloors, int curFloor, int curSegment, bool flipped, bool hiddenCell, int hiddenCellCountdown, bool hiddenCellLeftCheck, bool hiddenCellUpCheck)
	//{
	//	for (int i = 0; i < 3; i++)
	//	{
	//		// add possibility for it to spawn on top floor; as a fallback if not enough have spawned
	//		// not doing it here cause the logic is scuffed and I don't wanna have to modify it rn
	//		// actually, the logic seems a bit scuffed anyway? like, it doesn't seem to spawn the hidden rooms in some cases on the right blocked to the right
	//		if (curFloor < totalFloorCount - 1 - 1 && curFloor != 0) // skips bottom two floors and top floor
	//		{
	//			if (i == 0)
	//			{
	//				if (curSegment == 0)
	//				{
	//					hiddenCellLeftCheck = true;
	//				}
	//				else if (sideWinderFloors[curSegment - 1, curFloor] is not (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)
	//				{
	//					hiddenCellLeftCheck = false;
	//				}
	//				else
	//				{
	//					hiddenCellLeftCheck = true;
	//				}
	//			}
	//			if (i == 2)
	//			{
	//				if (hiddenCellLeftCheck == false && sideWinderFloors[curSegment, curFloor] is not (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)
	//				{
	//					hiddenCellCountdown = 0;
	//					break;
	//				}
	//			}
	//			if (curSegment + i == totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1) || curSegment + i == totalSegmentCount / 2)
	//			{
	//				hiddenCellCountdown = 0;
	//				break;
	//			}
	//			if (sideWinderFloors[curSegment + i, curFloor - 1] is (int)sideWinderStates.DownThrough or (int)sideWinderStates.RightBlockedDownThrough)
	//			{
	//				hiddenCellUpCheck = true;
	//			}
	//			if (hiddenCellUpCheck == true)
	//			{
	//				if (sideWinderFloors[curSegment + i, curFloor] is (int)sideWinderStates.DownThrough or (int)sideWinderStates.RightBlockedDownThrough)
	//				{
	//					hiddenCellUpCheck = false;
	//					hiddenCellCountdown = 0;
	//					break;
	//				}
	//			}
	//			if (curSegment == totalSegmentCount - 1 || curSegment == totalSegmentCount - 2)
	//			{
	//				hiddenCellCountdown = 0;
	//				break;
	//			}
	//			else if ((sideWinderFloors[curSegment + i, curFloor] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked) &&
	//				(sideWinderFloors[curSegment + i, curFloor - 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked) &&
	//				(sideWinderFloors[curSegment + i, curFloor + 1] is (int)sideWinderStates.Default or (int)sideWinderStates.RightBlocked))
	//			{
	//				hiddenCellCountdown++;
	//			}
	//			else
	//			{
	//				hiddenCellCountdown = 0;
	//				break;
	//			}
	//		}
	//		else if (curFloor == 0)
	//		{

	//		}
	//	}
	//	return (3, flipped, hiddenCell);
	//}

	public static (int[,] outCell, bool flipped) FortressRegularCells(int totalSegmentCount, int lowerHallsFloorCount, int middleHallsFloorCount, int upperHallsFloorCount, /*int totalFloorCount, */List<int[,]> floors, int[,] sideWinderFloors, int curFloor, int curSegment, bool flipped, List<(int, int, bool)> hiddenRoomLocations, int leftCentre, int rightCentre)
	{
		int totalFloorCount = lowerHallsFloorCount + middleHallsFloorCount + upperHallsFloorCount;
		// this version provides a bool to flip the given cell based on parameters instead of having to store flipped versions of cells; the regular version is commented out below

		(byte sidesOpen, bool? Up, bool? Down) connections = (0, null, null); // sidesOpen refers to the left/right sides, as it isn't necessary to define whether each are individually open

		if (sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default)
		{
			if (curSegment == 0 || (curSegment != 0 && (sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
			{
				connections = (1, null, false);
				flipped = true;
			}
			else
			{
				connections = (2, null, false);
			}
		}
		else if (sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.RightBlocked)
		{
			connections = (1, null, false);
		}
		else if (sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.DefaultDownThrough)
		{
			if (curSegment == 0 || (curSegment != 0 && (sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
			{
				connections = (1, null, true);
				flipped = true;
			}
			else
			{
				connections = (2, null, true);
			}
		}
		else if (sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.RightBlockedDownThrough)
		{
			if (curSegment == 0 || (curSegment != 0 && (sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
			{
				connections = (0, null, true);
				flipped = true;
			}
			else
			{
				connections = (1, null, true);
			}
		}
		if (curFloor != 0 && (sideWinderFloors[curSegment, curFloor - 1] is (int)sideWinderStates.DefaultDownThrough or (int)sideWinderStates.RightBlockedDownThrough))
		{
			connections.Up = true;
		}
		else
		{
			connections.Up = false;
		}

		//if (FortressRegularCellBuffer != null && FortressRegularCellBuffer.Count > 0)
		//{
		//	var temp = FortressRegularCellBuffer[0];
		//	FortressRegularCellBuffer.RemoveAt(0);
		//	return (WorldGen.genRand.Next(temp), flipped);
		//}
		//else
		//{
		//	FortressRegularCellBuffer = FortressRegularCellSelection(connections, totalSegmentCount, lowerHallsFloorCount, middleHallsFloorCount, upperHallsFloorCount, sideWinderFloors, curFloor, curSegment, flipped, hiddenRoomLocations, leftCentre, rightCentre);
		//	var temp = FortressRegularCellBuffer[0];
		//	FortressRegularCellBuffer.RemoveAt(0);
		//	return (WorldGen.genRand.Next(temp), flipped);
		//}
		var temp = FortressRegularCellSelection(connections, totalSegmentCount, lowerHallsFloorCount, middleHallsFloorCount, upperHallsFloorCount, sideWinderFloors, curFloor, curSegment, flipped, hiddenRoomLocations, leftCentre, rightCentre);
		return (WorldGen.genRand.Next(temp), flipped);
	}
	//public static int[,] FortressRegularCells(List<int[,]> floors, int[,] sideWinderFloors, int curFloor, int curSegment)
	//{
	//	IEnumerable<int[,]> tempList = new List<int[,]>();
	//	if (sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default)
	//	{
	//		tempList = CellConnections.RightConnections.Except(CellConnections.DownConnections);
	//		if (curSegment == 0 || (curSegment != 0 && (sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
	//		{
	//			tempList = tempList.Except(CellConnections.LeftConnections);
	//		}
	//		else
	//		{
	//			tempList = tempList.Intersect(CellConnections.LeftConnections);
	//		}
	//	}
	//	else if (sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.RightBlocked)
	//	{
	//		tempList = CellConnections.LeftConnections.Except(CellConnections.RightConnections).Except(CellConnections.DownConnections);
	//	}
	//	else if (sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.DownThrough)
	//	{
	//		tempList = CellConnections.RightConnections.Intersect(CellConnections.DownConnections);
	//		if (curSegment == 0 || (curSegment != 0 && (sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
	//		{
	//			tempList = tempList.Except(CellConnections.LeftConnections);
	//		}
	//		else
	//		{
	//			tempList = tempList.Intersect(CellConnections.LeftConnections);
	//		}
	//	}
	//	else if (sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.RightBlockedDownThrough)
	//	{
	//		tempList = CellConnections.DownConnections.Except(CellConnections.RightConnections);
	//		if (curSegment == 0 || (curSegment != 0 && (sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
	//		{
	//			tempList = tempList.Except(CellConnections.LeftConnections);
	//		}
	//		else
	//		{
	//			tempList = tempList.Intersect(CellConnections.LeftConnections);
	//		}
	//	}
	//	if (curFloor != 0 && (sideWinderFloors[curSegment, curFloor - 1] is (int)sideWinderStates.DownThrough or (int)sideWinderStates.RightBlockedDownThrough))
	//	{
	//		tempList = tempList.Intersect(CellConnections.UpConnections);
	//	}
	//	else
	//	{
	//		tempList = tempList.Except(CellConnections.UpConnections);
	//	}
	//	return WorldGen.genRand.Next(tempList.ToList());
	//}

	// this buffer is a little bit silly I think? need a better way to provide specific styles (book, statue, flower, etc), which will be carried to next cell when placing multi-cell rooms
	// as well as determining WHEN a multi-cell room should start
	// maybe just make this an (int segments, int style) tuple, with the first specifying number of cells to use this style, and the second being the style
	//public static List<List<int[,]>>? FortressRegularCellBuffer;
	public static (int segmentCount, int style) FortressRegularCellBuffer = (0, 0);

	public static List<int[,]> FortressRegularCellSelection((byte sidesOpen, bool? Up, bool? Down) connections, int totalSegmentCount, int lowerHallsFloorCount, int middleHallsFloorCount, int upperHallsFloorCount, /*int totalFloorCount, */int[,] sideWinderFloors, int curFloor, int curSegment, bool flipped, List<(int, int, bool)> hiddenRoomLocations, int leftCentre, int rightCentre)
	{
		int totalFloorCount = lowerHallsFloorCount + middleHallsFloorCount + upperHallsFloorCount;
		List<int[,]> tempList = [];
		if (connections.sidesOpen == 0)
		{
			if (connections.Up == false && connections.Down == false)
			{
				tempList = SkyFortressArrays.CellConnections.None; // doesn't ever choose this, just for completeness sake
			}
			else if (connections.Up == true && connections.Down == false)
			{
				tempList = SkyFortressArrays.CellConnections.Up;
			}
			else if (connections.Up == false && connections.Down == true)
			{
				tempList = SkyFortressArrays.CellConnections.Down;
			}
			else if (connections.Up == true && connections.Down == true)
			{
				tempList = SkyFortressArrays.CellConnections.UpDown;
			}
		}
		else if (connections.sidesOpen == 1)
		{
			if (connections.Up == false && connections.Down == false)
			{
				tempList = SkyFortressArrays.CellConnections.SingleSide;
			}
			else if (connections.Up == true && connections.Down == false)
			{
				tempList = SkyFortressArrays.CellConnections.SingleSideUp;
			}
			else if (connections.Up == false && connections.Down == true)
			{
				tempList = SkyFortressArrays.CellConnections.SingleSideDown;
			}
			else if (connections.Up == true && connections.Down == true)
			{
				tempList = SkyFortressArrays.CellConnections.SingleSideUpDown;
			}
		}
		else if (connections.sidesOpen == 2)
		{
			if (connections.Up == false && connections.Down == false)
			{
				if (curFloor >= totalFloorCount - lowerHallsFloorCount)
				{
					tempList = [SkyFortressArrays.Cells.LowerHall];
				}
				else
				{
					tempList = SkyFortressArrays.CellConnections.DualSide;
				}
			}
			else if (connections.Up == true && connections.Down == false)
			{
				tempList = SkyFortressArrays.CellConnections.DualSideUp;
			}
			else if (connections.Up == false && connections.Down == true)
			{
				tempList = SkyFortressArrays.CellConnections.DualSideDown;
			}
			else if (connections.Up == true && connections.Down == true)
			{
				tempList = SkyFortressArrays.CellConnections.DualSideUpDown;
			}
		}
		//List<List<int[,]>> tempListofLists = [tempList];
		return tempList;
	}

	public static int[,] FortressCentralCells(int[,] selectedCell, int totalSegmentCount, int totalFloorCount, List<int[,]> floors, int[,] sideWinderFloors, int curFloor, int curSegment, bool blockedCentreSide)
	{
		bool leftCentre = curSegment == Math.Min(totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1), totalSegmentCount / 2);
		bool rightCentre = curSegment == Math.Max(totalSegmentCount / 2 + (totalSegmentCount % 2 == 0 ? -1 : +1), totalSegmentCount / 2);

		if (curFloor == totalFloorCount - 1)
		{
			selectedCell = SkyFortressArrays.CentralCells._structureTilesCentralLeftRightTop;
		}
		else if (sideWinderFloors[curSegment, curFloor] is (int)sideWinderStates.Default or (int)sideWinderStates.DefaultDownThrough)
		{
			if (sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.Default or (int)sideWinderStates.DefaultDownThrough)
			{
				selectedCell = SkyFortressArrays.CentralCells._structureTilesCentralLeftRightTopBottom;
			}
			else // if cell left does not connect right
			{
				if (rightCentre && blockedCentreSide == true/* && (curFloor is 0 or 1)*/ && sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.Default)
				{
					if (curFloor == 0) // only do this for the top floor, otherwise it's possible for the top to be blocked off
					{
						selectedCell = SkyFortressArrays.CentralCells._structureTilesCentralRight;
					}
					else
					{
						selectedCell = SkyFortressArrays.CentralCells._structureTilesCentralRightTop;
					}
				}
				else
				{
					selectedCell = SkyFortressArrays.CentralCells._structureTilesCentralRightTopBottom;
				}
				//selectedCell = CentralCellArrays._structureTilesCentralRightTopBottom;
			}
		}
		else if (sideWinderFloors[curSegment, curFloor] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)
		{
			if (sideWinderFloors[curSegment - 1, curFloor] is (int)sideWinderStates.Default or (int)sideWinderStates.DefaultDownThrough)
			{
				if (leftCentre && blockedCentreSide == false/* && (curFloor is 0 or 1)*/ && sideWinderFloors[curSegment, curFloor] == (int)sideWinderStates.RightBlocked)
				{
					if (curFloor == 0) // only do this for the top floor, otherwise it's possible for the top to be blocked off
					{
						selectedCell = SkyFortressArrays.CentralCells._structureTilesCentralLeft;
					}
					else
					{
						selectedCell = SkyFortressArrays.CentralCells._structureTilesCentralLeftTop;
					}
				}
				else
				{
					selectedCell = SkyFortressArrays.CentralCells._structureTilesCentralLeftTopBottom;
				}
				//selectedCell = CentralCellArrays._structureTilesCentralLeftTopBottom;
			}
			else // if cell left does not connect right
			{
				selectedCell = SkyFortressArrays.CentralCells._structureTilesCentralTopBottom;
			}
		}
		return selectedCell;
	}
}

#region old version 2

//using Microsoft.Xna.Framework;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using Terraria;
//using Terraria.Chat;
//using Terraria.ID;
//using Terraria.Localization;
//using Terraria.ModLoader;
//using ThoriumMod.Core.EntitySources;

//namespace Avalon.WorldGeneration.Structures;

//class ChainedArrayBuilder
//{
//	private enum GenDirections : byte
//	{
//		Left,
//		Right,
//		Up,
//		Down
//	}
//	private static int[,] selectNextArray(int[,] previousCell, byte genDirection, int currentSegment, int totalSegments, int currentFloor, int totalFloors)
//	{
//		var tempCell = new List<int[,]>();
//		if (genDirection == (byte)GenDirections.Right)
//		{
//			#region attempt 1
//			// doesn't check all previous cells yet for existence of an up connection
//			//if (RightConnections.Contains(previousCell))
//			//{
//			//	if (UpConnections.Contains(previousCell))
//			//	{
//			//		return WorldGen.genRand.Next(LeftConnections.Except(DownConnections).Except(UpConnections).ToList());
//			//	}
//			//	else
//			//	{
//			//		var a = WorldGen.genRand.NextBool(3) ? WorldGen.genRand.Next(LeftConnections.Except(DownConnections).ToList()) : WorldGen.genRand.Next(LeftConnections.Except(DownConnections).Except(UpConnections).ToList());
//			//		return !RightConnections.Contains(a) ? CellArrays._structureTilesLeftTop : a;
//			//	}
//			//}
//			//else
//			//{
//			//	return WorldGen.genRand.Next(RightConnections.Except(LeftConnections).Except(DownConnections).ToList());
//			//}
//			#endregion attempt 1

//			#region attempt 2
//			//if (currentFloor == 0)
//			//{
//			//	if (totalSegmentCount / 2 == currentSegment) // central pillar
//			//	{
//			//		if (!RightConnections.Contains(previousCell))
//			//		{
//			//			return CellArrays._structureTilesCentralNoLeft;
//			//		}
//			//		else
//			//		{
//			//			return WorldGen.genRand.NextBool() ? CellArrays._structureTilesCentralNoRight : CellArrays._structureTilesCentral;
//			//		}
//			//	}
//			//	else if (totalSegmentCount - 1 == currentSegment) // right wall
//			//	{
//			//		if (!RightConnections.Contains(previousCell))
//			//		{
//			//			return CellArrays._structureTilesTop;
//			//		}
//			//		else
//			//		{
//			//			//return WorldGen.genRand.NextBool() ? CellArrays._structureTilesLeft : CellArrays._structureTilesLeftTop;
//			//			return CellArrays._structureTilesLeftTop; // temporary until I make all this properly check every existing cell beforehand for upwards/downwards connections
//			//		}
//			//	}
//			//	else
//			//	{
//			//		if (WorldGen.genRand.NextBool(totalSegmentCount / 2)) // upwards connections
//			//		{
//			//			if (!RightConnections.Contains(previousCell))
//			//			{
//			//				return WorldGen.genRand.NextBool(3)
//			//					?
//			//					WorldGen.genRand.Next(UpConnections.Except(DownConnections).Except(RightConnections).Except(LeftConnections).ToList())
//			//					:
//			//					WorldGen.genRand.Next(UpConnections.Except(DownConnections).Intersect(RightConnections).Except(LeftConnections).ToList());
//			//			}
//			//			else
//			//			{
//			//				return WorldGen.genRand.NextBool()
//			//					?
//			//					WorldGen.genRand.Next(LeftConnections.Except(DownConnections).Except(RightConnections).Intersect(UpConnections).ToList())
//			//					:
//			//					WorldGen.genRand.Next(LeftConnections.Except(DownConnections).Intersect(RightConnections).Intersect(UpConnections).ToList());
//			//			}
//			//		}
//			//		if (!RightConnections.Contains(previousCell)) // previous cell right wall
//			//		{
//			//			//return WorldGen.genRand.NextBool()
//			//			//	?
//			//			//	WorldGen.genRand.Next(UpConnections.Except(DownConnections).Except(RightConnections).Except(LeftConnections).ToList())
//			//			//	:
//			//			//	WorldGen.genRand.Next(UpConnections.Except(DownConnections).Intersect(RightConnections).Except(LeftConnections).ToList());

//			//			return WorldGen.genRand.Next(RightConnections.Except(DownConnections).Except(LeftConnections).ToList());
//			//		}
//			//		else
//			//		{
//			//			return CellArrays._structureTilesLeftRight;
//			//		}
//			//	}
//			//}
//			//else
//			//{
//			//	if (totalSegmentCount / 2 == currentSegment) // central pillar
//			//	{
//			//		if (!RightConnections.Contains(previousCell))
//			//		{
//			//			return CellArrays._structureTilesCentralNoLeft;
//			//		}
//			//		else
//			//		{
//			//			return WorldGen.genRand.NextBool() ? CellArrays._structureTilesCentralNoRight : CellArrays._structureTilesCentral;
//			//		}
//			//	}
//			//	else if (UpConnections.Contains(floors[currentSegment + (totalSegments * (currentFloor - 1))]))
//			//	//if (UpConnections.Contains(floors[1]))
//			//	{
//			//		Main.NewText("hi");
//			//		return CellArrays._structureTilesBottom;
//			//	}
//			//	else {
//			//		//return WorldGen.genRand.Next(RightConnections.Except(DownConnections).ToList());
//			//		return CellArrays._structureTilesRight;
//			//	}
//			//}
//			#endregion attempt 2

//			#region attempt 3
//			//		if (totalSegmentCount / 2 == currentSegment) // central pillar
//			//		{
//			//			if (!RightConnections.Contains(previousCell))
//			//			{
//			//				return CellArrays._structureTilesCentralNoLeft;
//			//			}
//			//			else
//			//			{
//			//				return WorldGen.genRand.NextBool() ? CellArrays._structureTilesCentralNoRight : CellArrays._structureTilesCentral;
//			//			}
//			//		}
//			//		else if (totalSegmentCount - 1 == currentSegment) // right wall
//			//		{
//			//			if (!RightConnections.Contains(previousCell))
//			//			{
//			//				return CellArrays._structureTilesTopBottom;
//			//			}
//			//			else
//			//			{
//			//				//return WorldGen.genRand.NextBool() ? CellArrays._structureTilesLeft : CellArrays._structureTilesLeftTop;
//			//				return CellArrays._structureTilesLeftTopBottom; // temporary until I make all this properly check every existing cell beforehand for upwards/downwards connections
//			//			}
//			//		}
//			//		else
//			//		{
//			//			if (WorldGen.genRand.NextBool(totalSegmentCount / 2)) // upwards connections
//			//			{
//			//				if (!RightConnections.Contains(previousCell))
//			//				{
//			//					if (WorldGen.genRand.NextBool(3))
//			//					{
//			//						tempCell = (UpConnections.Except(RightConnections).Except(LeftConnections).ToList());
//			//					}
//			//					else
//			//					{
//			//						tempCell = (UpConnections.Intersect(RightConnections).Except(LeftConnections).ToList());
//			//					}
//			//				}
//			//				else
//			//				{
//			//					if (WorldGen.genRand.NextBool())
//			//					{
//			//						tempCell = LeftConnections.Except(RightConnections).Intersect(UpConnections).ToList();
//			//					}
//			//					else
//			//					{
//			//						tempCell = LeftConnections.Intersect(RightConnections).Intersect(UpConnections).ToList();
//			//					}
//			//				}
//			//			}
//			//			else if (!RightConnections.Contains(previousCell)) // previous cell right wall
//			//			{
//			//				tempCell = (RightConnections.Except(LeftConnections).ToList());
//			//			}
//			//			else
//			//			{
//			//				tempCell = RightConnections.Intersect(LeftConnections).Except(UpConnections).ToList();
//			//			}
//			//		}
//			//	if (currentFloor == 0)
//			//	{
//			//		return WorldGen.genRand.Next(tempCell.Except(DownConnections).Except(SpecialCells).ToList());
//			//	}
//			//	else
//			//	{
//			//		if (UpConnections.Contains(floors[currentSegment + (totalSegments * (currentFloor - 1))]))
//			//		{
//			//			return WorldGen.genRand.Next(tempCell.Intersect(DownConnections).Except(SpecialCells).ToList());
//			//		}
//			//		else
//			//		{
//			//			return WorldGen.genRand.Next(tempCell.Except(DownConnections).Except(SpecialCells).ToList());
//			//		}
//			//	}
//			#endregion attempt 3
//		}
//		return CellArrays._structureTilesLeftRightTopBottom;
//	}
//	#region Connections
//	// these should be provided by the world structure calling this method, so that multiple of each connection type may be added arbitrarily
//	// create a helper method that takes multiple of these lists and filters it down to elements contained in both

//	// the world structure needs to be able to provide alternate lists for these in special cases; e.g. the sky fortress central pillars or hidden rooms
//	// this would mean providing the x/y cell counts for the structure, as well as the location and cell size of the special cells; e.g. hidden rooms take up 2-3 cells, central pillar only takes 1
//	// provide weights for each cell?
//	// provide cell density

//	// can't be modified with hot reload, make sure to restart game if for some reason you need to add more to these (or just modify it in another method)
//	public static List<int[,]> LeftConnections = new List<int[,]>()
//	{
//		CellArrays._structureTilesLeft,
//		CellArrays._structureTilesLeftRight,
//		CellArrays._structureTilesLeftTop,
//		CellArrays._structureTilesLeftBottom,
//		CellArrays._structureTilesLeftRightTop,
//		CellArrays._structureTilesLeftRightBottom,
//		CellArrays._structureTilesLeftTopBottom,
//		CellArrays._structureTilesLeftRightTopBottom,

//		CellArrays._structureTilesCentralLeft,
//		CellArrays._structureTilesCentralLeftRight,
//		CellArrays._structureTilesCentralLeftTop,
//		CellArrays._structureTilesCentralLeftBottom,
//		CellArrays._structureTilesCentralLeftRightTop,
//		CellArrays._structureTilesCentralLeftRightBottom,
//		CellArrays._structureTilesCentralLeftTopBottom,
//		CellArrays._structureTilesCentralLeftRightTopBottom
//	};
//	public static List<int[,]> RightConnections = new List<int[,]>()
//	{
//		CellArrays._structureTilesRight,
//		CellArrays._structureTilesRightTop,
//		CellArrays._structureTilesRightBottom,
//		CellArrays._structureTilesRightTopBottom,
//		CellArrays._structureTilesLeftRight,
//		CellArrays._structureTilesLeftRightTop,
//		CellArrays._structureTilesLeftRightBottom,
//		CellArrays._structureTilesLeftRightTopBottom,

//		CellArrays._structureTilesCentralRight,
//		CellArrays._structureTilesCentralRightTop,
//		CellArrays._structureTilesCentralRightBottom,
//		CellArrays._structureTilesCentralRightTopBottom,
//		CellArrays._structureTilesCentralLeftRight,
//		CellArrays._structureTilesCentralLeftRightTop,
//		CellArrays._structureTilesCentralLeftRightBottom,
//		CellArrays._structureTilesCentralLeftRightTopBottom
//	};
//	public static List<int[,]> UpConnections = new List<int[,]>()
//	{
//		CellArrays._structureTilesTop,
//		CellArrays._structureTilesLeftTop,
//		CellArrays._structureTilesRightTop,
//		CellArrays._structureTilesTopBottom,
//		CellArrays._structureTilesLeftRightTop,
//		CellArrays._structureTilesLeftTopBottom,
//		CellArrays._structureTilesRightTopBottom,
//		CellArrays._structureTilesLeftRightTopBottom,

//		CellArrays._structureTilesCentralTop,
//		CellArrays._structureTilesCentralLeftTop,
//		CellArrays._structureTilesCentralRightTop,
//		CellArrays._structureTilesCentralTopBottom,
//		CellArrays._structureTilesCentralLeftRightTop,
//		CellArrays._structureTilesCentralLeftTopBottom,
//		CellArrays._structureTilesCentralRightTopBottom,
//		CellArrays._structureTilesCentralLeftRightTopBottom
//	};
//	public static List<int[,]> DownConnections = new List<int[,]>()
//	{
//		CellArrays._structureTilesBottom,
//		CellArrays._structureTilesLeftBottom,
//		CellArrays._structureTilesRightBottom,
//		CellArrays._structureTilesTopBottom,
//		CellArrays._structureTilesLeftRightBottom,
//		CellArrays._structureTilesLeftTopBottom,
//		CellArrays._structureTilesRightTopBottom,
//		CellArrays._structureTilesLeftRightTopBottom,

//		CellArrays._structureTilesCentralBottom,
//		CellArrays._structureTilesCentralLeftBottom,
//		CellArrays._structureTilesCentralRightBottom,
//		CellArrays._structureTilesCentralTopBottom,
//		CellArrays._structureTilesCentralLeftRightBottom,
//		CellArrays._structureTilesCentralLeftTopBottom,
//		CellArrays._structureTilesCentralRightTopBottom,
//		CellArrays._structureTilesCentralLeftRightTopBottom
//	};
//	public static List<int[,]> SpecialCells = new List<int[,]>()
//	{
//		CellArrays._structureTilesCentralLeft,
//		CellArrays._structureTilesCentralRight,
//		CellArrays._structureTilesCentralTop,
//		CellArrays._structureTilesCentralBottom,
//		CellArrays._structureTilesCentralLeftRight,
//		CellArrays._structureTilesCentralLeftTop,
//		CellArrays._structureTilesCentralLeftBottom,
//		CellArrays._structureTilesCentralRightTop,
//		CellArrays._structureTilesCentralRightBottom,
//		CellArrays._structureTilesCentralTopBottom,
//		CellArrays._structureTilesCentralLeftRightTop,
//		CellArrays._structureTilesCentralLeftRightBottom,
//		CellArrays._structureTilesCentralLeftTopBottom,
//		CellArrays._structureTilesCentralRightTopBottom,
//		CellArrays._structureTilesCentralLeftRightTopBottom
//	};
//	#endregion Connections
//	public class CellArrays
//	{
//		#region regular cells
//		public static int[,] _structureWalls =
//			new int[,]
//			{
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0}
//			};
//		public static int[,] _structureTilesRight =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesTop =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesLeft =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1}
//				};
//		public static int[,] _structureTilesRightBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesLeftBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesRightTop =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesLeftRight =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesLeftTop =
//			new int[,] {
//				{1,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesRightTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesLeftRightTop =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesLeftTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesLeftRightBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesLeftRightTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{1,0,0,0,0,1}
//			};
//		#endregion regular cells


//		#region central pillars
//		public static int[,] _structureWallsCentral =
//			new int[,]
//			{
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0}
//			};
//		public static int[,] _structureTilesCentralLeft =
//			new int[,]
//			{
//				{1,1,1,1,1,0,1,1,1,1,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{1,1,1,1,1,0,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesCentralLeftRight =
//			new int[,]
//			{
//				{1,1,1,1,1,0,1,1,1,1,1},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{1,1,1,1,1,0,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesCentralLeftTop =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{1,1,1,1,1,0,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesCentralLeftBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,0,1,1,1,1,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesCentralLeftTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesCentralLeftRightTop =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{1,1,1,1,1,0,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesCentralLeftRightBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,0,1,1,1,1,1},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};

//		public static int[,] _structureTilesCentralRight =
//			new int[,]
//			{
//				{1,1,1,1,1,0,1,1,1,1,1},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,1,1,1,1,0,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesCentralRightTop =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,1,1,1,1,0,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesCentralRightTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesCentralRightBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,0,1,1,1,1,1},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesCentralTop =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,1,1,1,1,0,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesCentralTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesCentralBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,0,1,1,1,1,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};

//		public static int[,] _structureTilesCentralLeftRightTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};
//		#endregion central pillars
//	}

//	//public static int totalSegmentCount = 15;
//	//public static int totalFloorCount = 6;
//	//public static List<int[,]> floors = new List<int[,]>(totalSegmentCount * totalFloorCount);
//	//public static int[,] sideWinderFloors = new int[totalSegmentCount, totalFloorCount];
//	private enum sideWinderStates : byte
//	{
//		Default,
//		RightBlocked,
//		DownThrough,
//		RightBlockedDownThrough
//	}
//	private enum fortressSpecialStates : byte
//	{
//		Centre = 100,
//		CentreRightBlocked,
//		CentreDownThrough,
//		CentreRightBlockedDownThrough
//	}
//	public static void NewChainedStructure(int x, int y, int totalSegmentCount, int totalFloorCount, int wallChance, int maxWalls, int gapBetweenWalls)
//	{
//		List<int[,]> floors = new List<int[,]>(totalSegmentCount * totalFloorCount);
//		int[,] sideWinderFloors = new int[totalSegmentCount, totalFloorCount];
//		ushort typeTile = 0;
//		ushort typeWall = 0;

//		int PosX = x;
//		int PosY = y;
//		// todo: make the bottom two floors (on large worlds) create random flip points where they go up/down
//		// not sure if feasible on small/medium worlds since they likely won't have two entrance hall floors (not even sure if large worlds should)

//		// okay for the note above, if I make this generate twice, once for before the central pillar, and once after, then I would be able to satisfy that condition
//		// don't think I would need to do the upper floors separately either that way in order to achieve it? I'd just only modify the central pillar on the bottom halls

//		// using intersect and except fucking blows and is prone to failure, instead just add new sideWinderStates and set it ALL in here (make sideWinderStates a tuple)
//		// then I can just do the custom logic shit without worrying about intersect/except incorrectly masking arrays
//		for (int totalFloors = totalFloorCount - 1; totalFloors >= 0; totalFloors--)
//		{
//			int previousWall = -1;
//			int totalWalls = 0;
//			for (int totalSegments = 0; totalSegments < totalSegmentCount; totalSegments++)
//			{
//				if (totalFloors == totalFloorCount - 1)
//				{
//					if (totalSegments == totalSegmentCount - 1)
//					{
//						sideWinderFloors[totalSegments, totalFloors] = (int)sideWinderStates.RightBlocked;
//						//sideWinderFloors[totalSegments, totalFloors] = (int)sideWinderStates.Default; // temp just for testing to make it look nicer
//					}
//					else
//					{
//						sideWinderFloors[totalSegments, totalFloors] = (int)sideWinderStates.Default;
//					}
//				}
//				else if ((totalWalls < maxWalls && totalSegments - previousWall > gapBetweenWalls - 1 && /*WorldGen.genRand.NextBool((10 + maxWalls) - (totalSegments - previousWall)))*/WorldGen.genRand.NextBool(wallChance)) || totalSegments == totalSegmentCount - 1)
//				{
//					sideWinderFloors[totalSegments, totalFloors] = (int)sideWinderStates.RightBlocked;
//					int currentWall = previousWall == totalSegments - 1 ? totalSegments : WorldGen.genRand.Next(previousWall + 1, totalSegments + 1);
//					if (sideWinderFloors[currentWall, totalFloors] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)
//					{
//						sideWinderFloors[currentWall, totalFloors] = (int)sideWinderStates.RightBlockedDownThrough;
//					}
//					else
//					{
//						sideWinderFloors[currentWall, totalFloors] = (int)sideWinderStates.DownThrough;
//					}
//					//if (totalSegments == totalSegmentCount - 1 && sideWinderFloors[totalSegments, totalFloors] == (int)sideWinderStates.RightBlocked) // temp just for testing to make it look nicer
//					//{
//					//	sideWinderFloors[totalSegments, totalFloors] = (int)sideWinderStates.Default;
//					//}
//					previousWall = totalSegments;
//					totalWalls++;
//				}
//				else
//				{
//					sideWinderFloors[totalSegments, totalFloors] = (int)sideWinderStates.Default;
//				}
//			}
//		}

//		// stuff specific to sky fortress
//		//for (int i = 0; i < totalFloorCount; i++) // todo: replace the single centre cell with a double cell so that the left/right connections can both be random
//		//{
//		//	sideWinderFloors[totalSegmentCount / 2, i] += (int)fortressSpecialStates.Centre;
//		//}
//		PosY -= CellArrays._structureTilesRight.GetLength(0) - 1;

//		floors.Clear();
//		for (int totalFloors = totalFloorCount - 1; totalFloors >= 0; totalFloors--)
//		{
//			var selectedCell = CellArrays._structureTilesLeftRightTopBottom;
//			for (int totalSegments = 0; totalSegments < totalSegmentCount; totalSegments++)
//			{
//				IEnumerable<int[,]> tempList = new List<int[,]>();
//				//if (totalFloors == totalFloorCount - 1 && totalSegments == 0) // makes sure the entrance generates properly
//				//{
//				//	selectedCell = CellArrays._structureTilesLeftRight;
//				//}
//				/*else */if (sideWinderFloors[totalSegments, totalFloors] == (int)sideWinderStates.Default)
//				{
//					//selectedCell = CellArrays._structureTilesLeftRight;
//					tempList = RightConnections.Except(DownConnections);
//					if (totalSegments == 0 || (totalSegments != 0 && (sideWinderFloors[totalSegments - 1, totalFloors] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
//					{
//						tempList = tempList.Except(LeftConnections);
//					}
//					else
//					{
//						tempList = tempList.Intersect(LeftConnections);
//					}
//				}
//				else if (sideWinderFloors[totalSegments, totalFloors] == (int)sideWinderStates.RightBlocked)
//				{
//					//selectedCell = CellArrays._structureTilesLeft;
//					tempList = LeftConnections.Except(RightConnections).Except(DownConnections);
//				}
//				else if (sideWinderFloors[totalSegments, totalFloors] == (int)sideWinderStates.DownThrough)
//				{
//					//selectedCell = CellArrays._structureTilesLeftRightBottom;
//					tempList = RightConnections.Intersect(DownConnections);
//					if (totalSegments == 0 || (totalSegments != 0 && (sideWinderFloors[totalSegments - 1, totalFloors] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
//					{
//						tempList = tempList.Except(LeftConnections);
//					}
//					else
//					{
//						tempList = tempList.Intersect(LeftConnections);
//					}
//				}
//				else if (sideWinderFloors[totalSegments, totalFloors] == (int)sideWinderStates.RightBlockedDownThrough)
//				{
//					//selectedCell = CellArrays._structureTilesLeftBottom;
//					tempList = DownConnections.Except(RightConnections);
//					if (totalSegments == 0 || (totalSegments != 0 && (sideWinderFloors[totalSegments - 1, totalFloors] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
//					{
//						tempList = tempList.Except(LeftConnections);
//					}
//					else
//					{
//						tempList = tempList.Intersect(LeftConnections);
//					}
//				}
//				if (sideWinderFloors[totalSegments, totalFloors] < (int)fortressSpecialStates.Centre)
//				{
//					if (totalFloors != 0 && (sideWinderFloors[totalSegments, totalFloors - 1] is (int)sideWinderStates.DownThrough or (int)sideWinderStates.RightBlockedDownThrough))
//					{
//						tempList = tempList.Intersect(UpConnections);
//					}
//					else
//					{
//						tempList = tempList.Except(UpConnections);
//					}
//				}
//				else
//				{
//					////selectedCell = CellArrays._structureTilesCentral;
//					////var temp = (IEnumerable<int[,]>)SpecialCells;
//					//tempList = RightConnections;
//					//if (totalSegments == 0 || (totalSegments != 0 && (sideWinderFloors[totalSegments - 1, totalFloors] is (int)sideWinderStates.RightBlocked or (int)sideWinderStates.RightBlockedDownThrough)))
//					//{
//					//	tempList = tempList.Except(LeftConnections);
//					//}
//					//else
//					//{
//					//	tempList = tempList.Intersect(LeftConnections);
//					//}

//					//if (sideWinderFloors[totalSegments, totalFloors] == (int)fortressSpecialStates.Centre)
//					//{
//					//	tempList = SpecialCells.Intersect(LeftConnections).Intersect(RightConnections).Intersect(UpConnections).Intersect(DownConnections);
//					//}
//					//else if (sideWinderFloors[totalSegments, totalFloors] == (int)fortressSpecialStates.CentreRightBlocked)
//					//{
//					//	tempList = SpecialCells.Intersect(LeftConnections).Except(RightConnections).Intersect(UpConnections).Intersect(DownConnections);
//					//}
//					//else if (sideWinderFloors[totalSegments, totalFloors] == (int)fortressSpecialStates.CentreDownThrough)
//					//{
//					//	tempList = SpecialCells.Intersect(LeftConnections).Intersect(RightConnections).Intersect(UpConnections).Intersect(DownConnections);
//					//}
//					//else if (sideWinderFloors[totalSegments, totalFloors] == (int)fortressSpecialStates.CentreRightBlockedDownThrough)
//					//{
//					//	tempList = SpecialCells.Intersect(LeftConnections).Except(RightConnections).Intersect(UpConnections).Intersect(DownConnections);
//					//}
//				}
//				//if (totalSegments != totalSegmentCount / 2)
//				//{
//				//	tempList = tempList.Except(SpecialCells);
//				//}
//				tempList = tempList.Except(SpecialCells);
//				selectedCell = WorldGen.genRand.Next(tempList.ToList());
//				floors.Add(selectedCell);



//				if (totalSegments % 2 == 0)
//				{
//					typeTile = (ushort)ModContent.TileType<Tiles.SkyBrick>();
//					typeWall = (ushort)ModContent.WallType<Walls.SkyBrickWallUnsafe>();
//				}
//				else
//				{
//					typeTile = (ushort)ModContent.TileType<Tiles.SkyBrick>();
//					typeWall = (ushort)ModContent.WallType<Walls.SkyBrickWallUnsafe>();
//				}
//				for (int confirmPlatforms = 0; confirmPlatforms < 3; confirmPlatforms++) //Increase the iterations on this outermost for loop if tabletop-objects are not properly spawning
//				{
//					for (int i = 0; i < floors[totalSegments + (totalSegmentCount * (totalFloorCount - 1 - totalFloors))].GetLength(0); i++)
//					{
//						for (int j = floors[totalSegments + (totalSegmentCount * (totalFloorCount - 1 - totalFloors))].GetLength(1) - 1; j >= 0; j--)
//						{
//							int k = PosX + j;
//							int l = PosY + i;
//							if (WorldGen.InWorld(k, l, 30))
//							{
//								Tile tile = Framing.GetTileSafely(k, l);
//								switch (floors[totalSegments + (totalSegmentCount * (totalFloorCount - 1 - totalFloors))][i, j])
//								{
//									case 0:
//										tile.HasTile = false;
//										tile.SkipLiquid = true;
//										tile.LiquidAmount = 0;
//										break;
//									case 1:
//										tile.HasTile = true;
//										tile.TileType = typeTile;
//										tile.Slope = 0;
//										tile.IsHalfBlock = false;
//										tile.SkipLiquid = true;
//										tile.LiquidAmount = 0;
//										break;
//									}
//								}
//							}
//						}
//					}
//					var wallCell = CellArrays._structureWalls;
//					if (totalSegmentCount / 2 == totalSegments)
//					{
//						wallCell = CellArrays._structureWallsCentral;
//					}
//					for (int i = 0; i < wallCell.GetLength(0); i++)
//					{
//						for (int j = wallCell.GetLength(1) - 1; j >= 0; j--)
//						{
//							int k = PosX + j;
//							int l = PosY + i;
//							if (WorldGen.InWorld(k, l, 30))
//							{
//								Tile tile = Framing.GetTileSafely(k, l);
//								switch (wallCell[i, j])
//								{
//									case 0:
//										tile.WallType = typeWall;
//										break;
//								}
//							}
//						}
//					}

//					Utils.SquareTileFrameArea(PosX, PosY, floors[totalSegments + (totalSegmentCount * (totalFloorCount - 1 - totalFloors))].GetLength(1), floors[totalSegments + (totalSegmentCount * (totalFloorCount - 1 - totalFloors))].GetLength(0));
//					Utils.SquareWallFrameArea(PosX, PosY, floors[totalSegments + (totalSegmentCount * (totalFloorCount - 1 - totalFloors))].GetLength(1), floors[totalSegments + (totalSegmentCount * (totalFloorCount - 1 - totalFloors))].GetLength(0));

//				PosX += floors[totalSegments + (totalSegmentCount * (totalFloorCount - 1 - totalFloors))].GetLength(1);
//			}
//			for (int i = 0; i < totalSegmentCount; i++)
//			{
//				PosX -= floors[totalSegmentCount * (totalFloorCount - 1 - totalFloors) + i].GetLength(1);
//			}
//			PosY -= floors[totalSegmentCount * (totalFloorCount - 1 - totalFloors)].GetLength(0);
//		}
//		//string text = "hi";
//		//ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.White);
//		//Console.WriteLine(sideWinderFloors);
//		//Console.WriteLine("\n\n" + String.Join(", ", sideWinderFloors.Cast<int>()));

//		//			if (totalFloors != 0)
//		//			{
//		//				PosY -= floors[totalSegmentCount * (totalFloors - 1)].GetLength(0);
//		//				for (int i = 0; i < totalSegmentCount - 1; i++)
//		//				{
//		//					PosX -= floors[totalSegmentCount * (totalFloors - 1) + i].GetLength(1);
//		//				}
//		//			}
//		//			else
//		//			{
//		//				PosY -= CellArrays._structureTilesRight.GetLength(0) - 1;
//		//			}
//		//			for (int totalSegments = 0; totalSegments < totalSegmentCount; totalSegments++)
//		//			{
//		//				if (totalSegments != 0)
//		//				{
//		//					floors.Add(selectNextArray(floors[totalSegments - 1 + (totalSegmentCount * totalFloors)], (byte)GenDirections.Right, totalSegments, totalSegmentCount, totalFloors, totalFloorCount));
//		//					PosX += floors[totalSegments - 1 + (totalSegmentCount * totalFloors)].GetLength(1);
//		//				}

//		// test tile placement
//		//for (int i = 0; i < sideWinderFloors.GetLength(0); i++)
//		//{
//		//	for (int j = 0; j < sideWinderFloors.GetLength(1) * 2; j += 2)
//		//	{
//		//		int k = PosX + i;
//		//		int l = PosY + j;
//		//		if (WorldGen.InWorld(k, l, 30))
//		//		{
//		//			Tile tile = Framing.GetTileSafely(k, l);
//		//			Tile tile3 = Framing.GetTileSafely(k, l + 1);
//		//			switch (sideWinderFloors[i, j / 2])
//		//			{
//		//				case (int)sideWinderStates.Default:
//		//					tile.HasTile = true;
//		//					tile.TileType = typeTile;
//		//					tile.Slope = 0;
//		//					tile.IsHalfBlock = false;

//		//					tile3.HasTile = false;
//		//					break;

//		//				case (int)sideWinderStates.RightBlocked:
//		//					tile.HasTile = true;
//		//					tile.TileType = (ushort)ModContent.TileType<Tiles.ImperviousBrick>();
//		//					tile.Slope = 0;
//		//					tile.IsHalfBlock = false;

//		//					tile3.HasTile = false;
//		//					break;

//		//				case (int)sideWinderStates.DownThrough:
//		//					tile.HasTile = true;
//		//					tile.TileType = typeTile;
//		//					tile.Slope = 0;
//		//					tile.IsHalfBlock = false;

//		//					tile3.HasTile = true;
//		//					tile3.TileType = (ushort)ModContent.TileType<Tiles.ZirconGemsparkOff>();
//		//					tile3.Slope = 0;
//		//					tile3.IsHalfBlock = false;
//		//					break;

//		//				case (int)sideWinderStates.RightBlockedDownThrough:
//		//					tile.HasTile = true;
//		//					tile.TileType = (ushort)ModContent.TileType<Tiles.ImperviousBrick>();
//		//					tile.Slope = 0;
//		//					tile.IsHalfBlock = false;

//		//					tile3.HasTile = true;
//		//					tile3.TileType = (ushort)ModContent.TileType<Tiles.CoolGemsparkBlock>();
//		//					tile3.Slope = 0;
//		//					tile3.IsHalfBlock = false;
//		//					break;

//		//				case (int)fortressSpecialStates.Centre:
//		//					tile.HasTile = true;
//		//					tile.TileType = (ushort)ModContent.TileType<Tiles.SkyBrickColumn>();
//		//					tile.Slope = 0;
//		//					tile.IsHalfBlock = false;
//		//					if (j != sideWinderFloors.GetLength(1) * 2 - 2)
//		//					{
//		//						tile3.HasTile = true;
//		//						tile3.TileType = (ushort)ModContent.TileType<Tiles.SkyBrickColumn>();
//		//						tile3.Slope = 0;
//		//						tile3.IsHalfBlock = false;
//		//					}
//		//					break;
//		//			}
//		//		}
//		//	}
//		//}
//		//Utils.SquareTileFrameArea(PosX, PosY, sideWinderFloors.GetLength(0), sideWinderFloors.GetLength(1) * 2);
//		//Utils.SquareWallFrameArea(PosX, PosY, sideWinderFloors.GetLength(0), sideWinderFloors.GetLength(1) * 2);
//	}
//}
#endregion old version 2


// old version

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using ThoriumMod.Core.EntitySources;

//namespace Avalon.WorldGeneration.Structures;

//class ChainedArrayBuilder
//{
//	private enum GenDirections : byte
//	{
//		Left,
//		Right,
//		Up,
//		Down
//	}
//	private static int[,] selectNextArray(int[,] previousCell, byte genDirection, int currentSegment, int totalSegments, int currentFloor, int totalFloors)
//	{
//		var tempCell = new List<int[,]>();
//		if (genDirection == (byte)GenDirections.Right)
//		{
//			#region attempt 1
//			// doesn't check all previous cells yet for existence of an up connection
//			//if (RightConnections.Contains(previousCell))
//			//{
//			//	if (UpConnections.Contains(previousCell))
//			//	{
//			//		return WorldGen.genRand.Next(LeftConnections.Except(DownConnections).Except(UpConnections).ToList());
//			//	}
//			//	else
//			//	{
//			//		var a = WorldGen.genRand.NextBool(3) ? WorldGen.genRand.Next(LeftConnections.Except(DownConnections).ToList()) : WorldGen.genRand.Next(LeftConnections.Except(DownConnections).Except(UpConnections).ToList());
//			//		return !RightConnections.Contains(a) ? CellArrays._structureTilesLeftTop : a;
//			//	}
//			//}
//			//else
//			//{
//			//	return WorldGen.genRand.Next(RightConnections.Except(LeftConnections).Except(DownConnections).ToList());
//			//}
//			#endregion attempt 1

//			#region attempt 2
//			//if (currentFloor == 0)
//			//{
//			//	if (totalSegmentCount / 2 == currentSegment) // central pillar
//			//	{
//			//		if (!RightConnections.Contains(previousCell))
//			//		{
//			//			return CellArrays._structureTilesCentralNoLeft;
//			//		}
//			//		else
//			//		{
//			//			return WorldGen.genRand.NextBool() ? CellArrays._structureTilesCentralNoRight : CellArrays._structureTilesCentral;
//			//		}
//			//	}
//			//	else if (totalSegmentCount - 1 == currentSegment) // right wall
//			//	{
//			//		if (!RightConnections.Contains(previousCell))
//			//		{
//			//			return CellArrays._structureTilesTop;
//			//		}
//			//		else
//			//		{
//			//			//return WorldGen.genRand.NextBool() ? CellArrays._structureTilesLeft : CellArrays._structureTilesLeftTop;
//			//			return CellArrays._structureTilesLeftTop; // temporary until I make all this properly check every existing cell beforehand for upwards/downwards connections
//			//		}
//			//	}
//			//	else
//			//	{
//			//		if (WorldGen.genRand.NextBool(totalSegmentCount / 2)) // upwards connections
//			//		{
//			//			if (!RightConnections.Contains(previousCell))
//			//			{
//			//				return WorldGen.genRand.NextBool(3)
//			//					?
//			//					WorldGen.genRand.Next(UpConnections.Except(DownConnections).Except(RightConnections).Except(LeftConnections).ToList())
//			//					:
//			//					WorldGen.genRand.Next(UpConnections.Except(DownConnections).Intersect(RightConnections).Except(LeftConnections).ToList());
//			//			}
//			//			else
//			//			{
//			//				return WorldGen.genRand.NextBool()
//			//					?
//			//					WorldGen.genRand.Next(LeftConnections.Except(DownConnections).Except(RightConnections).Intersect(UpConnections).ToList())
//			//					:
//			//					WorldGen.genRand.Next(LeftConnections.Except(DownConnections).Intersect(RightConnections).Intersect(UpConnections).ToList());
//			//			}
//			//		}
//			//		if (!RightConnections.Contains(previousCell)) // previous cell right wall
//			//		{
//			//			//return WorldGen.genRand.NextBool()
//			//			//	?
//			//			//	WorldGen.genRand.Next(UpConnections.Except(DownConnections).Except(RightConnections).Except(LeftConnections).ToList())
//			//			//	:
//			//			//	WorldGen.genRand.Next(UpConnections.Except(DownConnections).Intersect(RightConnections).Except(LeftConnections).ToList());

//			//			return WorldGen.genRand.Next(RightConnections.Except(DownConnections).Except(LeftConnections).ToList());
//			//		}
//			//		else
//			//		{
//			//			return CellArrays._structureTilesLeftRight;
//			//		}
//			//	}
//			//}
//			//else
//			//{
//			//	if (totalSegmentCount / 2 == currentSegment) // central pillar
//			//	{
//			//		if (!RightConnections.Contains(previousCell))
//			//		{
//			//			return CellArrays._structureTilesCentralNoLeft;
//			//		}
//			//		else
//			//		{
//			//			return WorldGen.genRand.NextBool() ? CellArrays._structureTilesCentralNoRight : CellArrays._structureTilesCentral;
//			//		}
//			//	}
//			//	else if (UpConnections.Contains(floors[currentSegment + (totalSegments * (currentFloor - 1))]))
//			//	//if (UpConnections.Contains(floors[1]))
//			//	{
//			//		Main.NewText("hi");
//			//		return CellArrays._structureTilesBottom;
//			//	}
//			//	else {
//			//		//return WorldGen.genRand.Next(RightConnections.Except(DownConnections).ToList());
//			//		return CellArrays._structureTilesRight;
//			//	}
//			//}
//			#endregion attempt 2

//			#region attempt 3
//				if (totalSegmentCount / 2 == currentSegment) // central pillar
//				{
//					if (!RightConnections.Contains(previousCell))
//					{
//						return CellArrays._structureTilesCentralNoLeft;
//					}
//					else
//					{
//						return WorldGen.genRand.NextBool() ? CellArrays._structureTilesCentralNoRight : CellArrays._structureTilesCentral;
//					}
//				}
//				else if (totalSegmentCount - 1 == currentSegment) // right wall
//				{
//					if (!RightConnections.Contains(previousCell))
//					{
//						return CellArrays._structureTilesTopBottom;
//					}
//					else
//					{
//						//return WorldGen.genRand.NextBool() ? CellArrays._structureTilesLeft : CellArrays._structureTilesLeftTop;
//						return CellArrays._structureTilesLeftTopBottom; // temporary until I make all this properly check every existing cell beforehand for upwards/downwards connections
//					}
//				}
//				else
//				{
//					if (WorldGen.genRand.NextBool(totalSegmentCount / 2)) // upwards connections
//					{
//						if (!RightConnections.Contains(previousCell))
//						{
//							if (WorldGen.genRand.NextBool(3))
//							{
//								tempCell = (UpConnections.Except(RightConnections).Except(LeftConnections).ToList());
//							}
//							else
//							{
//								tempCell = (UpConnections.Intersect(RightConnections).Except(LeftConnections).ToList());
//							}
//						}
//						else
//						{
//							if (WorldGen.genRand.NextBool())
//							{
//								tempCell = LeftConnections.Except(RightConnections).Intersect(UpConnections).ToList();
//							}
//							else
//							{
//								tempCell = LeftConnections.Intersect(RightConnections).Intersect(UpConnections).ToList();
//							}
//						}
//					}
//					else if (!RightConnections.Contains(previousCell)) // previous cell right wall
//					{
//						tempCell = (RightConnections.Except(LeftConnections).ToList());
//					}
//					else
//					{
//						tempCell = RightConnections.Intersect(LeftConnections).Except(UpConnections).ToList();
//					}
//				}
//			if (currentFloor == 0)
//			{
//				return WorldGen.genRand.Next(tempCell.Except(DownConnections).Except(SpecialCells).ToList());
//			}
//			else
//			{
//				if (UpConnections.Contains(floors[currentSegment + (totalSegments * (currentFloor - 1))]))
//				{
//					return WorldGen.genRand.Next(tempCell.Intersect(DownConnections).Except(SpecialCells).ToList());
//				}
//				else
//				{
//					return WorldGen.genRand.Next(tempCell.Except(DownConnections).Except(SpecialCells).ToList());
//				}
//			}
//		}
//		#endregion attempt 3
//		return CellArrays._structureTilesLeftRightTopBottom;
//	}
//	#region Connections
//	// these should be provided by the world structure calling this method, so that multiple of each connection type may be added arbitrarily
//	// create a helper method that takes multiple of these lists and filters it down to elements contained in both

//	// the world structure needs to be able to provide alternate lists for these in special cases; e.g. the sky fortress central pillars or hidden rooms
//	// this would mean providing the x/y cell counts for the structure, as well as the location and cell size of the special cells; e.g. hidden rooms take up 2-3 cells, central pillar only takes 1
//	// provide weights for each cell?
//	// provide cell density

//	// can't be modified with hot reload, make sure to restart game if for some reason you need to add more to these (or just modify it in another method)
//	public static List<int[,]> LeftConnections = new List<int[,]>()
//	{
//		CellArrays._structureTilesLeft,
//		CellArrays._structureTilesLeftRight,
//		CellArrays._structureTilesLeftTop,
//		CellArrays._structureTilesLeftBottom,
//		CellArrays._structureTilesLeftRightTop,
//		CellArrays._structureTilesLeftRightBottom,
//		CellArrays._structureTilesLeftTopBottom,
//		CellArrays._structureTilesLeftRightTopBottom,
//		CellArrays._structureTilesCentral,
//		CellArrays._structureTilesCentralNoRight
//	};
//	public static List<int[,]> RightConnections = new List<int[,]>()
//	{
//		CellArrays._structureTilesRight,
//		CellArrays._structureTilesRightTop,
//		CellArrays._structureTilesRightBottom,
//		CellArrays._structureTilesRightTopBottom,
//		CellArrays._structureTilesLeftRight,
//		CellArrays._structureTilesLeftRightTop,
//		CellArrays._structureTilesLeftRightBottom,
//		CellArrays._structureTilesLeftRightTopBottom,
//		CellArrays._structureTilesCentral,
//		CellArrays._structureTilesCentralNoLeft
//	};
//	public static List<int[,]> UpConnections = new List<int[,]>()
//	{
//		CellArrays._structureTilesTop,
//		CellArrays._structureTilesLeftTop,
//		CellArrays._structureTilesRightTop,
//		CellArrays._structureTilesTopBottom,
//		CellArrays._structureTilesLeftRightTop,
//		CellArrays._structureTilesLeftTopBottom,
//		CellArrays._structureTilesRightTopBottom,
//		CellArrays._structureTilesLeftRightTopBottom,
//		CellArrays._structureTilesCentral,
//		CellArrays._structureTilesCentralNoLeft,
//		CellArrays._structureTilesCentralNoRight
//	};
//	public static List<int[,]> DownConnections = new List<int[,]>()
//	{
//		CellArrays._structureTilesBottom,
//		CellArrays._structureTilesLeftBottom,
//		CellArrays._structureTilesRightBottom,
//		CellArrays._structureTilesTopBottom,
//		CellArrays._structureTilesLeftRightBottom,
//		CellArrays._structureTilesLeftTopBottom,
//		CellArrays._structureTilesRightTopBottom,
//		CellArrays._structureTilesLeftRightTopBottom,
//		CellArrays._structureTilesCentral,
//		CellArrays._structureTilesCentralNoLeft,
//		CellArrays._structureTilesCentralNoRight
//	};
//	public static List<int[,]> SpecialCells = new List<int[,]>()
//	{
//		CellArrays._structureTilesCentral,
//		CellArrays._structureTilesCentralNoLeft,
//		CellArrays._structureTilesCentralNoRight
//	};
//	#endregion Connections
//	public class CellArrays
//	{
//		public static int[,] _structureWalls =
//			new int[,]
//			{
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0}
//			};
//		public static int[,] _structureTilesRight =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesTop =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesLeft =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1}
//				};
//		public static int[,] _structureTilesRightBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesLeftBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesRightTop =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesLeftRight =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesLeftTop =
//			new int[,] {
//				{1,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesRightTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,0},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesLeftRightTop =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{1,1,1,1,1,1}
//			};
//		public static int[,] _structureTilesLeftTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{0,0,0,0,0,1},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesLeftRightBottom =
//			new int[,]
//			{
//				{1,1,1,1,1,1},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesLeftRightTopBottom =
//			new int[,]
//			{
//				{1,0,0,0,0,1},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{0,0,0,0,0,0},
//				{1,0,0,0,0,1}
//			};
//		public static int[,] _structureWallsCentral =
//			new int[,]
//			{
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0},
//				{0,0,0,0,0,0,0,0,0,0,0}
//			};
//		public static int[,] _structureTilesCentral =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{0,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesCentralNoLeft =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,1,0,0,0,0,0},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};
//		public static int[,] _structureTilesCentralNoRight =
//			new int[,]
//			{
//				{1,0,0,0,0,0,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{0,0,0,0,0,1,0,0,0,0,1},
//				{1,0,0,0,0,0,0,0,0,0,1}
//			};
//	}

//	public static int totalSegmentCount = 15;
//	public static int totalFloorCount = 6;
//	public static List<int[,]> floors = new List<int[,]>(totalSegmentCount * totalFloorCount);
//	public static void NewChainedStructure(int x, int y)
//	{
//		floors.Clear();

//		ushort typeTile = 0;
//		ushort typeWall = 0;

//		int PosX = x;
//		int PosY = y;
//		for (int totalFloors = 0; totalFloors < totalFloorCount; totalFloors++)
//		{
//			if (totalFloors != 0)
//			{
//				PosY -= floors[totalSegmentCount * (totalFloors - 1)].GetLength(0);
//				for (int i = 0; i < totalSegmentCount - 1; i++)
//				{
//					PosX -= floors[totalSegmentCount * (totalFloors - 1) + i].GetLength(1);
//				}
//			}
//			else
//			{
//				PosY -= CellArrays._structureTilesRight.GetLength(0) - 1;
//			}
//			for (int totalSegments = 0; totalSegments < totalSegmentCount; totalSegments++)
//			{
//				if (totalSegments != 0)
//				{
//					floors.Add(selectNextArray(floors[totalSegments - 1 + (totalSegmentCount * totalFloors)], (byte)GenDirections.Right, totalSegments, totalSegmentCount, totalFloors, totalFloorCount));
//					PosX += floors[totalSegments - 1 + (totalSegmentCount * totalFloors)].GetLength(1);
//				}
//				else // add possibility for up conn for higher floors, also move this to selectNextArray
//				{
//					floors.Add(CellArrays._structureTilesRight);
//				}

//				if (totalSegments % 2 == 0)
//				{
//					typeTile = (ushort)ModContent.TileType<Tiles.SkyBrick>();
//					typeWall = (ushort)ModContent.WallType<Walls.SkyBrickWallUnsafe>();
//				}
//				else
//				{
//					typeTile = (ushort)ModContent.TileType<Tiles.SkyBrick>();
//					typeWall = (ushort)ModContent.WallType<Walls.SkyBrickWallUnsafe>();
//				}
//				for (int confirmPlatforms = 0; confirmPlatforms < 3; confirmPlatforms++) //Increase the iterations on this outermost for loop if tabletop-objects are not properly spawning
//				{
//					for (int i = 0; i < floors[totalSegments + (totalSegmentCount * totalFloors)].GetLength(0); i++)
//					{
//						for (int j = floors[totalSegments + (totalSegmentCount * totalFloors)].GetLength(1) - 1; j >= 0; j--)
//						{
//							int k = PosX + j;
//							int l = PosY + i;
//							if (WorldGen.InWorld(k, l, 30))
//							{
//								Tile tile = Framing.GetTileSafely(k, l);
//								switch (floors[totalSegments + (totalSegmentCount * totalFloors)][i, j])
//								{
//									case 0:
//										tile.HasTile = false;
//										break;
//									case 1:
//										tile.HasTile = true;
//										tile.TileType = typeTile;
//										tile.Slope = 0;
//										tile.IsHalfBlock = false;
//										break;
//								}
//							}
//						}
//					}
//				}
//				var wallCell = CellArrays._structureWalls;
//				if (totalSegmentCount / 2 == totalSegments)
//				{
//					wallCell = CellArrays._structureWallsCentral;
//				}
//				for (int i = 0; i < wallCell.GetLength(0); i++)
//				{
//					for (int j = wallCell.GetLength(1) - 1; j >= 0; j--)
//					{
//						int k = PosX + j;
//						int l = PosY + i;
//						if (WorldGen.InWorld(k, l, 30))
//						{
//							Tile tile = Framing.GetTileSafely(k, l);
//							switch (wallCell[i, j])
//							{
//								case 0:
//									tile.WallType = typeWall;
//									break;
//							}
//						}
//					}
//				}

//				Utils.SquareTileFrameArea(PosX, PosY, floors[totalSegments + (totalSegmentCount * totalFloors)].GetLength(1), floors[totalSegments + (totalSegmentCount * totalFloors)].GetLength(0));
//				Utils.SquareWallFrameArea(PosX, PosY, floors[totalSegments + (totalSegmentCount * totalFloors)].GetLength(1), floors[totalSegments + (totalSegmentCount * totalFloors)].GetLength(0));
//			}
//		}
//	}
//}
