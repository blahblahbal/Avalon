using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

public class SyncLockUnlock
{
	public const byte Lock = 0;
	public const byte Unlock = 1;
	public static void SendPacket(byte lockAction, int x, int y, int toClient = -1, int ignoreClient = -1)
	{
		ModPacket packet = MessageHandler.GetPacket(MessageID.SyncLockUnlock);
		packet.Write(lockAction);
		packet.Write((short)x);
		packet.Write((short)y);
		packet.Send(toClient, ignoreClient);
	}
	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		byte lockAction = reader.ReadByte();
		int x = reader.ReadInt16();
		int y = reader.ReadInt16();
		if (lockAction == Lock)
		{
			Tiles.Furniture.LockedChests.Lock(x, y);
		}
		else if (lockAction == Unlock)
		{
			Tiles.Furniture.LockedChests.Unlock(x, y);
		}
		if (Main.netMode == NetmodeID.Server)
		{
			SendPacket(lockAction, x, y, -1, fromWho);
			NetMessage.SendTileSquare(-1, x, y, 2);
		}
	}
}
