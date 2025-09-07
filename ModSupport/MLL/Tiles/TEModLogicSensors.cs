using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.ModSupport.MLL.Tiles;
//Here is the Tile Entity for sensors, ported from TELogicSensor to have modded sensors that detects modded liquids
public class TEModLogicSensors : ModTileEntity
{
	//This enum contains the types of all the avaliable sensors
	public enum LogicCheckType
	{
		None, //The default, never meant to be used, but a fallback to prevent issues with defaulting to a used type
		Acid,
		Blood,
	}

	private static readonly List<Point16> tripPoints = [];

	private static readonly List<int> markedIDsForRemoval = [];

	private static bool inUpdateLoop;

	public LogicCheckType logicCheck = LogicCheckType.None;

	public bool On = false;

	public int CountedData;

	public override void NetPlaceEntityAttempt(int x, int y)
	{
		NetPlaceEntity(x, y);
	}

	public void NetPlaceEntity(int x, int y)
	{
		int iD = Place(x, y);
		((TEModLogicSensors)ByID[iD]).FigureCheckState();
		NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, iD, x, y);
	}

	public override bool IsTileValidForEntity(int x, int y)
	{
		return ValidTile(x, y);
	}

	public override void PreGlobalUpdate()
	{
		inUpdateLoop = true;
		markedIDsForRemoval.Clear();
	}

	public override void Update()
	{
		bool state = GetState(Position.X, Position.Y, logicCheck, this);
		switch (logicCheck)
		{
			case LogicCheckType.Acid:
			case LogicCheckType.Blood:
				if (On != state)
				{
					ChangeState(state, TripWire: true);
				}
				break;
		}
	}

	public override void PostGlobalUpdate()
	{
		inUpdateLoop = false;
		foreach (Point16 tripPoint in tripPoints)
		{
			SoundEngine.PlaySound(SoundID.Mech, tripPoint.ToVector2() * 16f);
			Wiring.TripWire(tripPoint.X, tripPoint.Y, 1, 1);
			if (Main.netMode == NetmodeID.Server)
			{
				Network.SyncWiring.SendPacket(-1, tripPoint.X, tripPoint.Y, Network.SyncWiring.WiringType.LiquidSensor);
			}
		}
		tripPoints.Clear();
		foreach (int item in markedIDsForRemoval)
		{
			if (ByID.TryGetValue(item, out var value) && value.type == Type)
			{
				lock (EntityCreationLock)
				{
					ByID.Remove(item);
					ByPosition.Remove(value.Position);
				}
			}
		}
		markedIDsForRemoval.Clear();
	}

	public void ChangeState(bool onState, bool TripWire)
	{
		if (onState == On || SanityCheck(Position.X, Position.Y))
		{
			Main.tile[Position.X, Position.Y].TileFrameX = (short)(onState ? 18 : 0);
			On = onState;
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, Position.X, Position.Y);
			}
			if (TripWire && Main.netMode != NetmodeID.MultiplayerClient)
			{
				tripPoints.Add(Position);
			}
		}
	}

	public static bool ValidTile(int x, int y)
	{
		if (!Main.tile[x, y].HasTile || Main.tile[x, y].TileType != ModContent.TileType<LiquidSensorTiles>() || Main.tile[x, y].TileFrameY % 18 != 0 || Main.tile[x, y].TileFrameX % 18 != 0)
		{
			return false;
		}
		return true;
	}

	public static LogicCheckType FigureCheckType(int x, int y, out bool on)
	{
		on = false;
		if (!WorldGen.InWorld(x, y))
		{
			return LogicCheckType.None;
		}
		Tile tile = Main.tile[x, y];
		LogicCheckType result = LogicCheckType.None;
		switch (tile.TileFrameY / 18)
		{
			case 0:
				result = LogicCheckType.Acid;
				break;
			case 1:
				result = LogicCheckType.Blood;
				break;
		}
		on = GetState(x, y, result);
		return result;
	}

	public static bool GetState(int x, int y, LogicCheckType type, TEModLogicSensors? instance = null)
	{
		switch (type)
		{
			case LogicCheckType.Acid:
			case LogicCheckType.Blood:
			{
				if (instance == null)
				{
					return false;
				}
				Tile tile = Main.tile[x, y];
				bool switched = false;
				if (tile.LiquidType == LiquidLoader.LiquidType<Acid>() && type == LogicCheckType.Acid)
				{
					switched = true;
				}
				if (tile.LiquidType == LiquidLoader.LiquidType<Blood>() && type == LogicCheckType.Blood)
				{
					switched = true;
				}
				if (tile.LiquidAmount == 0)
				{
					switched = false;
				}
				if (!switched && instance.On)
				{
					if (instance.CountedData == 0)
					{
						instance.CountedData = 15;
					}
					else if (instance.CountedData > 0)
					{
						instance.CountedData--;
					}
					switched = instance.CountedData > 0;
				}
				return switched;
			}
			default:
				return false;
		}
	}

	public void FigureCheckState()
	{
		logicCheck = FigureCheckType(Position.X, Position.Y, out On);
		GetFrame(Position.X, Position.Y, logicCheck, On);
	}

	public static void GetFrame(int x, int y, LogicCheckType type, bool on)
	{
		Tile tile = Main.tile[x, y];
		tile.TileFrameX = (short)(on ? 18 : 0);
		tile.TileFrameY = type switch
		{
			LogicCheckType.Acid => 0,
			LogicCheckType.Blood => 18,
			_ => 0,
		};
	}

	public bool SanityCheck(int x, int y)
	{
		if (!Main.tile[x, y].HasTile || Main.tile[x, y].TileType != ModContent.TileType<LiquidSensorTiles>())
		{
			Kill(x, y);
			return false;
		}
		return true;
	}

	public override int Hook_AfterPlacement(int x, int y, int type, int style, int direction, int alternate)
	{
		LogicCheckType logicCheckType = FigureCheckType(x, y, out bool on);
		GetFrame(x, y, logicCheckType, on);
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			NetMessage.SendTileSquare(Main.myPlayer, x, y);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, x, y, (int)Type);
			return -1;
		}
		int placeID = Place(x, y);
		((TEModLogicSensors)ByID[placeID]).FigureCheckState();
		return placeID;
	}

	public override void OnKill()
	{
		int x = Position.X;
		int y = Position.Y;
		if (!ByPosition.TryGetValue(new Point16(x, y), out var value) || value.type != Type)
		{
			return;
		}
		bool switched = false;
		if (((TEModLogicSensors)value).logicCheck == LogicCheckType.Acid && ((TEModLogicSensors)value).On)
		{
			switched = true;
		}
		else if (((TEModLogicSensors)value).logicCheck == LogicCheckType.Blood && ((TEModLogicSensors)value).On)
		{
			switched = true;
		}
		if (switched)
		{
			SoundEngine.PlaySound(SoundID.Mech, Position.ToVector2() * 16f);
			Wiring.TripWire(value.Position.X, value.Position.Y, 1, 1);
			NetMessage.SendData(MessageID.HitSwitch, -1, -1, null, value.Position.X, value.Position.Y);
		}
		if (inUpdateLoop)
		{
			markedIDsForRemoval.Add(value.ID);
			return;
		}
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write((byte)logicCheck);
		writer.Write(On);
	}

	public override void NetReceive(BinaryReader reader)
	{
		logicCheck = (LogicCheckType)reader.ReadByte();
		On = reader.ReadBoolean();
	}

	public override void LoadData(TagCompound tag)
	{
		logicCheck = (LogicCheckType)tag.GetByte(nameof(logicCheck));
		On = tag.GetBool(nameof(On));
	}

	public override void SaveData(TagCompound tag)
	{
		tag[nameof(logicCheck)] = (byte)logicCheck;
		tag[nameof(On)] = On;
	}

	public override string ToString()
	{
		return Position.X + "x  " + Position.Y + "y " + logicCheck;
	}
}
