using Avalon.Tiles;
using Avalon.Walls;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Structures;

public enum HellcastleRoomType
{
	Regular,
	Arena,
}
[Flags]
public enum RoomConnectionOnSide
{
	Top = 0,
	Bottom = 1,
	Left = 2,
	Right = 4,
}
public record struct HellcastleHallway
{
	public HellcastleHallway(Point start, Point end, int width)
	{
		Start = start;
		End = end;
		Width = width;
	}
	public Point Start { get; set; }
	public Point End { get; set; }
	public int Width { get; set; }
	public void Generate(bool HollowOutInsides)
	{
		int halfWidth = Width / 2;
		ushort brickType = (ushort)ModContent.TileType<ImperviousBrick>();
		ushort wallType = (ushort)ModContent.WallType<ImperviousBrickWallUnsafe>();
		int iterations = Math.Max(Math.Abs(Start.X - End.X), Math.Abs(Start.Y - End.Y));
		for (int i = 0; i < iterations; i += 2)
		{
			float percent = i / (float)iterations;
			int x = (int)Math.Round(MathHelper.Lerp(End.X, Start.X, percent)) - halfWidth;
			int y = (int)Math.Round(MathHelper.Lerp(End.Y, Start.Y, percent)) - halfWidth;

			if (!HollowOutInsides)
				WorldUtils.Gen(new Point(x - 6, y - 6), new Shapes.Rectangle(Width + 12, Width + 12), Actions.Chain(new Actions.PlaceWall(wallType, false), new Modifiers.Expand(1), new Actions.SetTileKeepWall(brickType, true, true)));
			else
			{
				WorldUtils.Gen(new Point(x, y), new Shapes.Rectangle(Width, Width), new Actions.ClearTile(true));
			}
		}
	}
}
public record struct HellcastleRoomConnectionPoint
{
	public HellcastleRoomConnectionPoint(Point position, RoomConnectionOnSide type)
	{
		Type = type;
		Position = position;
		InUse = false;
	}
	public RoomConnectionOnSide Type { get; set; }
	public Point Position { get; set; }
	public bool InUse { get; set; }
}
public record struct HellcastleRoom
{
	public HellcastleRoom(HellcastleRoomType type)
	{
		int connectionIntendedSideOffset = 6;
		int connectionWrongSideOffset = 10;
		Position = Point.Zero;
		Type = type;

		switch (type)
		{
			case HellcastleRoomType.Regular:
				Size = new Point(Main.rand.Next(30, 50), Main.rand.Next(20, 30));

				switch (Main.rand.Next(3))
				{
					case 0: // horizontal only
						ConnectionPoints = [
							new HellcastleRoomConnectionPoint(new Point(Rect.Left + connectionIntendedSideOffset, Main.rand.Next(Rect.Top + connectionWrongSideOffset, Rect.Bottom - connectionWrongSideOffset)), RoomConnectionOnSide.Left),
							new HellcastleRoomConnectionPoint(new Point(Rect.Right - connectionIntendedSideOffset, Main.rand.Next(Rect.Top + connectionWrongSideOffset, Rect.Bottom - connectionWrongSideOffset)), RoomConnectionOnSide.Right)];
						break;
					case 1: // Vertical only
						ConnectionPoints = [
							new HellcastleRoomConnectionPoint(new Point(Main.rand.Next(Rect.Left + connectionWrongSideOffset, Rect.Right - connectionWrongSideOffset), Rect.Top + connectionIntendedSideOffset), RoomConnectionOnSide.Top),
							new HellcastleRoomConnectionPoint(new Point(Main.rand.Next(Rect.Left + connectionWrongSideOffset, Rect.Right - connectionWrongSideOffset), Rect.Bottom - connectionIntendedSideOffset), RoomConnectionOnSide.Bottom)];
						break;
					case 2: // Vertical only
						ConnectionPoints = [
							new HellcastleRoomConnectionPoint(new Point(Rect.Left + connectionIntendedSideOffset, Main.rand.Next(Rect.Top + connectionWrongSideOffset, Rect.Bottom - connectionWrongSideOffset)), RoomConnectionOnSide.Left),
							new HellcastleRoomConnectionPoint(new Point(Rect.Right - connectionIntendedSideOffset, Main.rand.Next(Rect.Top+ connectionWrongSideOffset, Rect.Bottom - connectionWrongSideOffset)), RoomConnectionOnSide.Right),
							new HellcastleRoomConnectionPoint(new Point(Main.rand.Next(Rect.Left + connectionWrongSideOffset, Rect.Right - connectionWrongSideOffset), Rect.Top + connectionIntendedSideOffset), RoomConnectionOnSide.Top),
							new HellcastleRoomConnectionPoint(new Point(Main.rand.Next(Rect.Left + connectionWrongSideOffset, Rect.Right - connectionWrongSideOffset), Rect.Bottom - connectionIntendedSideOffset), RoomConnectionOnSide.Bottom)];
						break;
				}
				break;
			case HellcastleRoomType.Arena:
				Size = new Point(Main.rand.Next(80, 100), Main.rand.Next(50, 70));
				int halfHeight = Size.Y / 2;
				ConnectionPoints = [
					new HellcastleRoomConnectionPoint(new Point(Rect.Left + connectionIntendedSideOffset, Main.rand.Next(Rect.Top + halfHeight + connectionWrongSideOffset, Rect.Bottom - connectionWrongSideOffset)), RoomConnectionOnSide.Left),
					new HellcastleRoomConnectionPoint(new Point(Rect.Right - connectionIntendedSideOffset, Main.rand.Next(Rect.Top + halfHeight + connectionWrongSideOffset, Rect.Bottom - connectionWrongSideOffset)), RoomConnectionOnSide.Right),
					new HellcastleRoomConnectionPoint(new Point(Rect.Left + connectionIntendedSideOffset, Main.rand.Next(Rect.Top + connectionWrongSideOffset, Rect.Bottom - halfHeight - connectionWrongSideOffset)), RoomConnectionOnSide.Left),
					new HellcastleRoomConnectionPoint(new Point(Rect.Right - connectionIntendedSideOffset, Main.rand.Next(Rect.Top + connectionWrongSideOffset, Rect.Bottom - halfHeight - connectionWrongSideOffset)), RoomConnectionOnSide.Right),
				];
				break;
		}
	}
	public Point Position { get; set; }
	public Point Size { get; set; }
	public HellcastleRoomConnectionPoint[] ConnectionPoints { get; set; }
	public Rectangle Rect { get => new Rectangle(Position.X, Position.Y, Size.X, Size.Y); }
	public HellcastleRoomType Type { get; set; }

	public void Generate(bool beforeHallways)
	{
		ushort brickType = (ushort)ModContent.TileType<ImperviousBrick>();
		ushort wallType = (ushort)ModContent.WallType<ImperviousBrickWallUnsafe>();

		if(beforeHallways)
			WorldUtils.Gen(Position - new Point(10,10), new Shapes.Rectangle(Size.X + 20, Size.Y + 20), Actions.Chain(Actions.Chain(new Modifiers.SkipWalls(wallType), new Actions.PlaceWall(wallType, false), new Modifiers.Expand(2), new Actions.SetTileKeepWall(brickType, true, true))));

		switch (Type)
		{
			case HellcastleRoomType.Regular:
				if (beforeHallways)
				{
					WorldUtils.Gen(Position, new Shapes.Rectangle(Size.X, Size.Y), Actions.Chain(new Actions.PlaceWall(wallType, false), new Modifiers.Expand(1), new Actions.SetTileKeepWall(brickType, true, true)));
				}
				else
				{
					WorldUtils.Gen(Position + new Point(3, 3), new Shapes.Rectangle(Size.X - 6, Size.Y - 6), new Actions.ClearTile(true));
				}
				break;
			case HellcastleRoomType.Arena:
				if (beforeHallways)
				{
					WorldUtils.Gen(Position, new Shapes.Rectangle(Size.X, Size.Y), Actions.Chain(new Actions.PlaceWall(wallType, false), new Modifiers.Expand(1), new Actions.SetTileKeepWall(brickType, true, true)));
				}
				else
				{
					WorldUtils.Gen(Position + new Point(3, 3), new Shapes.Rectangle(Size.X - 6, Size.Y - 6), new Actions.ClearTile(true));
					WorldGen.PlaceTile(Position.X + (Size.X / 2), Position.Y + Size.Y - 4, ModContent.TileType<LibraryAltar>(), false, true);
				}
				break;
		}
		if (!beforeHallways)
		{
			for (int i = 0; i < ConnectionPoints.Length; i++)
			{
				WorldUtils.Gen(ConnectionPoints[i].Position, new Shapes.Rectangle(1, 1), new Actions.SetTile((ushort)(TileID.AmethystGemspark + ConnectionPoints[i].Type)));
			}
		}
	}
}
public class JonasHellcastle
{
	public static void Generate(int x, int y)
	{
		//MakeTunnel(Main.LocalPlayer.Center.ToTileCoordinates(), Main.MouseWorld.ToTileCoordinates(), 3, false);
		//return;
		int hellcastleWidth = 400;
		int hellcastleHeight = 260;
		Rectangle hellcastleRandomGenArea = new Rectangle(x - hellcastleWidth / 2, y - hellcastleHeight / 2, hellcastleWidth, hellcastleHeight);
		WorldUtils.Gen(new Point(hellcastleRandomGenArea.X - 100, hellcastleRandomGenArea.Y - 100), new Shapes.Rectangle(hellcastleWidth + 200, hellcastleHeight + 200), new Actions.Clear());

		//WorldUtils.Gen(new Point(hellcastleRandomGenArea.X - 10, hellcastleRandomGenArea.Y - 10), new Shapes.Rectangle(hellcastleWidth + 20, hellcastleHeight + 20), new Actions.SetTile((ushort)ModContent.TileType<ImperviousBrick>(),true,true));

		List<HellcastleRoom> rooms = new();
		List<HellcastleHallway> halls = new();

		HellcastleRoom arena = new HellcastleRoom(HellcastleRoomType.Arena);
		ShuffleRoomPosition(ref arena, hellcastleRandomGenArea);
		rooms.Add(arena);

		// decide what rooms to make
		PopulateRooms(HellcastleRoomType.Regular, 40, ref rooms, hellcastleRandomGenArea);

		// generate the rooms
		for(int i = 0; i < rooms.Count; i++)
		{
			HellcastleRoom r = rooms[i];
			HellcastleHallway? hallway = TryToMakeATunnel(ref r, ref rooms);
			if (hallway != null)
			{
				HellcastleHallway hall = (HellcastleHallway)hallway;
				hall.Generate(false);
				r.Generate(true);
				halls.Add(hall);
			}
			else
			{
				bool couldntGen = true;
				for (int i2 = 0; i2 < rooms[i].ConnectionPoints.Length; i2++)
				{
					if (rooms[i].ConnectionPoints[i2].InUse)
					{
						couldntGen = false;
						r.Generate(true);
						break;
					}
				}
				if (couldntGen)
				{
					rooms.RemoveAt(i);
					i--;
				}
			}
		}
		foreach (HellcastleHallway hallway in halls)
		{
			hallway.Generate(true);
		}
		foreach(HellcastleRoom r in rooms)
		{
			r.Generate(false);
		}
	}
	private static void PopulateRooms(HellcastleRoomType type, int amount, ref List<HellcastleRoom> rooms, Rectangle genArea)
	{
		for (int i = 0; i < amount; i++)
		{
			HellcastleRoom room = new HellcastleRoom(HellcastleRoomType.Regular);
			ShuffleRoomPosition(ref room, genArea);
			bool isOverlapping = false;
			for (int t = 0; t < 10; t++)
			{
				foreach (HellcastleRoom r in rooms)
				{
					if (room.Rect.Intersects(r.Rect.Expand(6, 6)))
					{
						isOverlapping = true;
						break;
					}
					if (isOverlapping)
						break;
				}
			}
			if (!isOverlapping)
				rooms.Add(room);
		}
	}
	private static void ShuffleRoomPosition(ref HellcastleRoom room, Rectangle rect)
	{
		for (int i = 0; i < room.ConnectionPoints.Length; i++)
		{
			room.ConnectionPoints[i].Position -= room.Position;
		}
		room.Position = new Point(WorldGen.genRand.Next(rect.X, rect.X + rect.Width - room.Size.X), WorldGen.genRand.Next(rect.Y, rect.Y + rect.Height - room.Size.Y));
		for(int i = 0; i < room.ConnectionPoints.Length; i++)
		{
			room.ConnectionPoints[i].Position += room.Position;
		}
	}
	private static HellcastleHallway? TryToMakeATunnel(ref HellcastleRoom room, ref List<HellcastleRoom> rooms)
	{
		int tunnelWidth = Main.rand.Next(6,9);
		List<int> possibleStarts = new();
		List<int> possibleEndPointIndexinRoomsList = new();
		List<int> possibleEndsConnectPoint = new();
		for (int startConnect = 0; startConnect < room.ConnectionPoints.Length; startConnect++)
		{
			for (int otherRoom = 0; otherRoom < rooms.Count; otherRoom++)
			{
				if (rooms[otherRoom] == room)
					continue;
				for (int endConnect = 0; endConnect < rooms[otherRoom].ConnectionPoints.Length; endConnect++)
				{
					if (CanThesePointsConnect(ref room.ConnectionPoints[startConnect], ref rooms[otherRoom].ConnectionPoints[endConnect]))
					{
						bool validRoom = true;
						float useless = 0f;
						foreach(HellcastleRoom r in rooms)
						{
							if (r == room || r == rooms[otherRoom])
								continue;
							if (Collision.CheckAABBvLineCollision(r.Position.ToVector2(), r.Size.ToVector2(), room.ConnectionPoints[startConnect].Position.ToVector2(), rooms[otherRoom].ConnectionPoints[endConnect].Position.ToVector2(), tunnelWidth - 3, ref useless))
							{
								validRoom = false;
								break;
							}
						}
						if(validRoom)
						{
							possibleStarts.Add(startConnect);
							possibleEndPointIndexinRoomsList.Add(otherRoom);
							possibleEndsConnectPoint.Add(endConnect);
						}
					}
				}
			}
		}
		if (possibleStarts.Count == 0)
			return null;
		else
		{
			int p = Main.rand.Next(possibleStarts.Count);

			room.ConnectionPoints[possibleStarts[p]].InUse = true;
			rooms[possibleEndPointIndexinRoomsList[p]].ConnectionPoints[possibleEndsConnectPoint[p]].InUse = true;
			return new HellcastleHallway(room.ConnectionPoints[possibleStarts[p]].Position, rooms[possibleEndPointIndexinRoomsList[p]].ConnectionPoints[possibleEndsConnectPoint[p]].Position, tunnelWidth);
		}
	}
	private static bool CanThesePointsConnect(ref HellcastleRoomConnectionPoint a, ref HellcastleRoomConnectionPoint b)
	{
		if (a.InUse || b.InUse)
			return false;
		if (Math.Abs(a.Position.X - b.Position.X) > Math.Abs(a.Position.Y - b.Position.Y))
		{
			if (a.Type.HasFlag(RoomConnectionOnSide.Left) && b.Type.HasFlag(RoomConnectionOnSide.Right) && a.Position.X > b.Position.X)
			{
				return true;
			}
			else if (a.Type.HasFlag(RoomConnectionOnSide.Right) && b.Type.HasFlag(RoomConnectionOnSide.Left) && a.Position.X < b.Position.X)
			{
				return true;
			}
		}
		else
		{
			if (a.Type.HasFlag(RoomConnectionOnSide.Top) && b.Type.HasFlag(RoomConnectionOnSide.Bottom) && a.Position.Y < b.Position.Y)
			{
				return true;
			}
			else if (a.Type.HasFlag(RoomConnectionOnSide.Bottom) && b.Type.HasFlag(RoomConnectionOnSide.Top) && a.Position.Y > b.Position.Y)
			{
				return true;
			}
		}
		return false;
	}
}
