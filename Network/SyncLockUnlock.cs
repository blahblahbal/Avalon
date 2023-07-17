using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

public class SyncLockUnlock
{
    public static void SendPacket(int type, int x, int y, int toClient = -1, int ignoreClient = -1)
    {
        ModPacket packet = MessageHandler.GetPacket(MessageID.SyncLockUnlock);
        packet.Write((short)type);
        packet.Write((short)x);
        packet.Write((short)y);
        packet.Send(toClient, ignoreClient);
    }
    public static void HandlePacket(BinaryReader reader, int fromWho)
    {
        int type = reader.ReadInt16();
        int x = reader.ReadInt16();
        int y = reader.ReadInt16();
        if (type == 0)
        {
            Tiles.Furniture.LockedChests.Lock(x, y);
        }
        else if (type == 1)
        {
            Tiles.Furniture.LockedChests.Unlock(x, y);
        }
        if (Main.netMode == NetmodeID.Server)
        {
            SendPacket(type, x, y, -1, fromWho);
        }
    }
}
