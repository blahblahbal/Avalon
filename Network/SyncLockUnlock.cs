using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

/// <summary>
/// This class handes locking/unlocking for the sonic screwdriver, which requires that the server temporarily sets <see cref="NPC.downedPlantBoss"/> to true.<para></para>
/// Copies the functionality of the vanilla <see cref="Terraria.ID.MessageID.LockAndUnlock"/> net message.
/// </summary>
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

		bool returnflag = false;
		if (!NPC.downedPlantBoss)
		{
			returnflag = true;
			NPC.downedPlantBoss = true;
		}

		if (lockAction == Lock)
		{
			Chest.Lock(x, y);
			if (Main.netMode == NetmodeID.Server)
			{
				SendPacket(Lock, x, y, -1, fromWho);
				NetMessage.SendTileSquare(-1, x, y, 2);
			}
		}
		if (lockAction == Unlock)
		{
			Chest.Unlock(x, y);
			if (Main.netMode == NetmodeID.Server)
			{
				SendPacket(Unlock, x, y, -1, fromWho);
				NetMessage.SendTileSquare(-1, x, y, 2);
			}
		}

		if (returnflag)
		{
			NPC.downedPlantBoss = false;
		}
	}
}
