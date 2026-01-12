using Avalon.Tiles.Furniture.Functional;
using Avalon.Tiles.Furniture.Gem;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

public class SyncWiring
{
	public enum WiringType
	{
		BookcaseTeleporter = 0,
		GemLocks = 1,
		TrappedChests = 2,
		LiquidSensor = 3
	}
	public static void SendPacket(int playerIndex, int x, int y, WiringType type, int toClient = -1, int ignoreClient = -1)
	{
		SendPacket(playerIndex, x, y, (int)type, toClient, ignoreClient);
	}
	public static void SendPacket(int playerIndex, int x, int y, int type, int toClient = -1, int ignoreClient = -1)
	{
		ModPacket packet = MessageHandler.GetPacket(MessageID.SyncWiring);
		packet.Write((short)playerIndex);
		packet.Write((short)x);
		packet.Write((short)y);
		packet.Write((short)type);
		packet.Send(toClient, ignoreClient);
	}
	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		int pindex = reader.ReadInt16();
		int x = reader.ReadInt16();
		int y = reader.ReadInt16();
		int t = reader.ReadInt16();
		Wiring.SetCurrentUser(pindex);
		if (t == (short)WiringType.BookcaseTeleporter)
		{
			BookcaseTeleporter.Trigger(x, y);
			Wiring.TripWire(x, y, 1, 1);
		}
		else if (t == (short)WiringType.GemLocks)
		{
			GemLocks.HitSwitch(x, y);
		}
		else if (t == (short)WiringType.TrappedChests)
		{
			TrappedChests.Trigger(x, y);
			Wiring.TripWire(x, y, 2, 2);
		}
		else if (t == (short)WiringType.LiquidSensor)
		{
			SoundEngine.PlaySound(SoundID.Mech, new Vector2(x, y) * 16f);
			Wiring.TripWire(x, y, 1, 1);
		}
		Wiring.SetCurrentUser();
		if (Main.netMode == NetmodeID.Server)
		{
			SendPacket(pindex, x, y, -1, fromWho);
		}
	}
}
