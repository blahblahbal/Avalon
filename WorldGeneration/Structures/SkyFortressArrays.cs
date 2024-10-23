using System.Collections.Generic;
using static log4net.Appender.ColoredConsoleAppender;

namespace Avalon.WorldGeneration.Structures;

class SkyFortressArrays
{
	// tbh, it might be more convenient to strictly define the cells being generated instead of using include/exclude?
	// since there needs to be some cases where cells are really composites of several cells
	public class CellConnections
	{
		// these should be provided by the world structure calling this method, so that multiple of each connection type may be added arbitrarily
		// create a helper method that takes multiple of these lists and filters it down to elements contained in both

		// the world structure needs to be able to provide alternate lists for these in special cases; e.g. the sky fortress central pillars or hidden rooms
		// this would mean providing the x/y cell counts for the structure, as well as the location and cell size of the special cells; e.g. hidden rooms take up 2-3 cells, central pillar only takes 1
		// provide weights for each cell?
		// provide cell density

		// can't be modified with hot reload, make sure to restart game if for some reason you need to add more to these (or just modify it in another method)
		public static List<int[,]> LeftConnections = new List<int[,]>()
		{
			Cells._structureTilesLeft,
			Cells._structureTilesLeftRight,
			Cells._structureTilesLeftTop,
			Cells._structureTilesLeftBottom,
			Cells._structureTilesLeftRightTop,
			Cells._structureTilesLeftRightBottom,
			Cells._structureTilesLeftTopBottom,
			Cells._structureTilesLeftRightTopBottom,

			//CentralCellArrays._structureTilesCentralLeft,
			//CentralCellArrays._structureTilesCentralLeftRight,
			//CentralCellArrays._structureTilesCentralLeftTop,
			//CentralCellArrays._structureTilesCentralLeftBottom,
			//CentralCellArrays._structureTilesCentralLeftRightTop,
			//CentralCellArrays._structureTilesCentralLeftRightBottom,
			//CentralCellArrays._structureTilesCentralLeftTopBottom,
			//CentralCellArrays._structureTilesCentralLeftRightTopBottom
		};
		public static List<int[,]> RightConnections = new List<int[,]>()
		{
			Cells._structureTilesRight,
			Cells._structureTilesRightTop,
			Cells._structureTilesRightBottom,
			Cells._structureTilesRightTopBottom,
			Cells._structureTilesLeftRight,
			Cells._structureTilesLeftRightTop,
			Cells._structureTilesLeftRightBottom,
			Cells._structureTilesLeftRightTopBottom,

			//CentralCellArrays._structureTilesCentralRight,
			//CentralCellArrays._structureTilesCentralRightTop,
			//CentralCellArrays._structureTilesCentralRightBottom,
			//CentralCellArrays._structureTilesCentralRightTopBottom,
			//CentralCellArrays._structureTilesCentralLeftRight,
			//CentralCellArrays._structureTilesCentralLeftRightTop,
			//CentralCellArrays._structureTilesCentralLeftRightBottom,
			//CentralCellArrays._structureTilesCentralLeftRightTopBottom
		};
		public static List<int[,]> UpConnections = new List<int[,]>()
		{
			Cells._structureTilesTop,
			Cells._structureTilesLeftTop,
			Cells._structureTilesRightTop,
			Cells._structureTilesTopBottom,
			Cells._structureTilesLeftRightTop,
			Cells._structureTilesLeftTopBottom,
			Cells._structureTilesRightTopBottom,
			Cells._structureTilesLeftRightTopBottom,

			//CentralCellArrays._structureTilesCentralTop,
			//CentralCellArrays._structureTilesCentralLeftTop,
			//CentralCellArrays._structureTilesCentralRightTop,
			//CentralCellArrays._structureTilesCentralTopBottom,
			//CentralCellArrays._structureTilesCentralLeftRightTop,
			//CentralCellArrays._structureTilesCentralLeftTopBottom,
			//CentralCellArrays._structureTilesCentralRightTopBottom,
			//CentralCellArrays._structureTilesCentralLeftRightTopBottom
		};
		public static List<int[,]> DownConnections = new List<int[,]>()
		{
			Cells._structureTilesBottom,
			Cells._structureTilesLeftBottom,
			Cells._structureTilesRightBottom,
			Cells._structureTilesTopBottom,
			Cells._structureTilesLeftRightBottom,
			Cells._structureTilesLeftTopBottom,
			Cells._structureTilesRightTopBottom,
			Cells._structureTilesLeftRightTopBottom,

			//CentralCellArrays._structureTilesCentralBottom,
			//CentralCellArrays._structureTilesCentralLeftBottom,
			//CentralCellArrays._structureTilesCentralRightBottom,
			//CentralCellArrays._structureTilesCentralTopBottom,
			//CentralCellArrays._structureTilesCentralLeftRightBottom,
			//CentralCellArrays._structureTilesCentralLeftTopBottom,
			//CentralCellArrays._structureTilesCentralRightTopBottom,
			//CentralCellArrays._structureTilesCentralLeftRightTopBottom
		};
		//public static List<int[,]> SpecialCells = new List<int[,]>()
		//{
		//	CentralCellArrays._structureTilesCentralLeft,
		//	CentralCellArrays._structureTilesCentralRight,
		//	CentralCellArrays._structureTilesCentralTop,
		//	CentralCellArrays._structureTilesCentralBottom,
		//	CentralCellArrays._structureTilesCentralLeftRight,
		//	CentralCellArrays._structureTilesCentralLeftTop,
		//	CentralCellArrays._structureTilesCentralLeftBottom,
		//	CentralCellArrays._structureTilesCentralRightTop,
		//	CentralCellArrays._structureTilesCentralRightBottom,
		//	CentralCellArrays._structureTilesCentralTopBottom,
		//	CentralCellArrays._structureTilesCentralLeftRightTop,
		//	CentralCellArrays._structureTilesCentralLeftRightBottom,
		//	CentralCellArrays._structureTilesCentralLeftTopBottom,
		//	CentralCellArrays._structureTilesCentralRightTopBottom,
		//	CentralCellArrays._structureTilesCentralLeftRightTopBottom
		//};
	}
	public class Cells
	{
		public static int[,] _structureFilled =
			new int[,]
			{
				{1,1,1,1,1,1},
				{1,1,1,1,1,1},
				{1,1,1,1,1,1},
				{1,1,1,1,1,1},
				{1,1,1,1,1,1},
				{1,1,1,1,1,1}
			};
		public static int[,] _structureWalls =
			new int[,]
			{
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0}
			};
		public static int[,] _structureTilesRight =
			new int[,]
			{
				{1,1,1,1,1,1},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,1,1,1,1,1}
			};
		public static int[,] _structureTilesTop =
			new int[,]
			{
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,1,1,1,1,1}
			};
		public static int[,] _structureTilesLeft =
			new int[,]
			{
				{1,1,1,1,1,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{1,1,1,1,1,1}
			};
		public static int[,] _structureTilesBottom =
			new int[,]
			{
				{1,1,1,1,1,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1}
				};
		public static int[,] _structureTilesRightBottom =
			new int[,]
			{
				{1,1,1,1,1,1},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,1}
			};
		public static int[,] _structureTilesTopBottom =
			new int[,]
			{
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1},
				{1,0,0,0,0,1}
			};
		public static int[,] _structureTilesLeftBottom =
			new int[,]
			{
				{1,1,1,1,1,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{1,0,0,0,0,1}
			};
		public static int[,] _structureTilesRightTop =
			new int[,]
			{
				{1,0,0,0,0,1},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,1,1,1,1,1}
			};
		public static int[,] _structureTilesLeftRight =
			new int[,]
			{
				{1,1,1,1,1,1},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{1,1,1,1,1,1}
			};
		public static int[,] _structureTilesLeftTop =
			new int[,] {
				{1,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{1,1,1,1,1,1}
			};
		public static int[,] _structureTilesRightTopBottom =
			new int[,]
			{
				{1,0,0,0,0,1},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,0},
				{1,0,0,0,0,1}
			};
		public static int[,] _structureTilesLeftRightTop =
			new int[,]
			{
				{1,0,0,0,0,1},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{1,1,1,1,1,1}
			};
		public static int[,] _structureTilesLeftTopBottom =
			new int[,]
			{
				{1,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{0,0,0,0,0,1},
				{1,0,0,0,0,1}
			};
		public static int[,] _structureTilesLeftRightBottom =
			new int[,]
			{
				{1,1,1,1,1,1},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{1,0,0,0,0,1}
			};
		public static int[,] _structureTilesLeftRightTopBottom =
			new int[,]
			{
				{1,0,0,0,0,1},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{0,0,0,0,0,0},
				{1,0,0,0,0,1}
			};
	}
	//public class Cells
	//{
	//	public static int[,] _structureFilled =
	//		new int[,]
	//		{
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
	//		};
	//	public static int[,] _structureWalls =
	//		new int[,]
	//		{
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
	//		};
	//	public static int[,] _structureTilesRight =
	//		new int[,]
	//		{
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
	//		};
	//	public static int[,] _structureTilesTop =
	//		new int[,]
	//		{
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
	//		};
	//	public static int[,] _structureTilesLeft =
	//		new int[,]
	//		{
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
	//		};
	//	public static int[,] _structureTilesBottom =
	//		new int[,]
	//		{
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}
	//			};
	//	public static int[,] _structureTilesRightBottom =
	//		new int[,]
	//		{
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}
	//		};
	//	public static int[,] _structureTilesTopBottom =
	//		new int[,]
	//		{
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}
	//		};
	//	public static int[,] _structureTilesLeftBottom =
	//		new int[,]
	//		{
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}
	//		};
	//	public static int[,] _structureTilesRightTop =
	//		new int[,]
	//		{
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
	//		};
	//	public static int[,] _structureTilesLeftRight =
	//		new int[,]
	//		{
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
	//		};
	//	public static int[,] _structureTilesLeftTop =
	//		new int[,] {
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
	//		};
	//	public static int[,] _structureTilesRightTopBottom =
	//		new int[,]
	//		{
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}
	//		};
	//	public static int[,] _structureTilesLeftRightTop =
	//		new int[,]
	//		{
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
	//		};
	//	public static int[,] _structureTilesLeftTopBottom =
	//		new int[,]
	//		{
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}
	//		};
	//	public static int[,] _structureTilesLeftRightBottom =
	//		new int[,]
	//		{
	//			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}
	//		};
	//	public static int[,] _structureTilesLeftRightTopBottom =
	//		new int[,]
	//		{
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	//			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}
	//		};
	//}

	public class CentralCells
	{
		public static int[,] _structureWallsCentral =
			new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0}
			};
		public static int[,] _structureTilesCentralLeft =
			new int[,]
			{
				{1,1,1,1,1,0,1,1,1,1,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{1,1,1,1,1,0,1,1,1,1,1}
			};
		public static int[,] _structureTilesCentralLeftRight =
			new int[,]
			{
				{1,1,1,1,1,0,1,1,1,1,1},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{1,1,1,1,1,0,1,1,1,1,1}
			};
		public static int[,] _structureTilesCentralLeftTop =
			new int[,]
			{
				{1,0,0,0,0,0,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{1,1,1,1,1,0,1,1,1,1,1}
			};
		public static int[,] _structureTilesCentralLeftBottom =
			new int[,]
			{
				{1,1,1,1,1,0,1,1,1,1,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,1}
			};
		public static int[,] _structureTilesCentralLeftTopBottom =
			new int[,]
			{
				{1,0,0,0,0,0,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,1}
			};
		public static int[,] _structureTilesCentralLeftRightTop =
			new int[,]
			{
				{1,0,0,0,0,0,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{1,1,1,1,1,0,1,1,1,1,1}
			};
		public static int[,] _structureTilesCentralLeftRightBottom =
			new int[,]
			{
				{1,1,1,1,1,0,1,1,1,1,1},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,0,0,0,0,0,1}
			};

		public static int[,] _structureTilesCentralRight =
			new int[,]
			{
				{1,1,1,1,1,0,1,1,1,1,1},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,1,1,1,1,0,1,1,1,1,1}
			};
		public static int[,] _structureTilesCentralRightTop =
			new int[,]
			{
				{1,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,1,1,1,1,0,1,1,1,1,1}
			};
		public static int[,] _structureTilesCentralRightTopBottom =
			new int[,]
			{
				{1,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,0,0,0,0,0,1}
			};
		public static int[,] _structureTilesCentralRightBottom =
			new int[,]
			{
				{1,1,1,1,1,0,1,1,1,1,1},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,0,0,0,0,0,1}
			};
		public static int[,] _structureTilesCentralTop =
			new int[,]
			{
				{1,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,1,1,1,1,0,1,1,1,1,1}
			};
		public static int[,] _structureTilesCentralTopBottom =
			new int[,]
			{
				{1,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,1}
			};
		public static int[,] _structureTilesCentralBottom =
			new int[,]
			{
				{1,1,1,1,1,0,1,1,1,1,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,2,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,1}
			};

		public static int[,] _structureTilesCentralLeftRightTopBottom =
			new int[,]
			{
				{1,0,0,0,0,0,0,0,0,0,1},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,2,0,0,0,0,0},
				{1,0,0,0,0,0,0,0,0,0,1}
			};
	}
}
