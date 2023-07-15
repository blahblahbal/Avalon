using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

public class SyncWiring
{
    public enum WiringType
    {
        BookcaseTeleporter = 0,
        GemLocks = 1
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
            Tiles.Furniture.BookcaseTeleporter.Trigger(x, y);
            Wiring.TripWire(x, y, 1, 1);
        }
        if (t == (short)WiringType.GemLocks)
        {
            Tiles.GemLocks.HitSwitch(x, y);
            Wiring.TripWire(x, y, 3, 3);
        }
        Wiring.SetCurrentUser();
        if (Main.netMode == NetmodeID.Server)
        {
            SendPacket(pindex, x, y, -1, fromWho);
        }
    }
}
