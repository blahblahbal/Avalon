using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

public class SyncWiring
{
    public static void SendPacket(int playerIndex, int x, int y, int toClient = -1, int ignoreClient = -1)
    {
        ModPacket packet = MessageHandler.GetPacket(MessageID.SyncWiring);
        packet.Write((short)playerIndex);
        packet.Write((short)x);
        packet.Write((short)y);
        packet.Send(toClient, ignoreClient);
    }
    public static void HandlePacket(BinaryReader reader, int fromWho)
    {
        int pindex = reader.ReadInt16();
        int x = reader.ReadInt16();
        int y = reader.ReadInt16();
        Wiring.SetCurrentUser(pindex);
        Tiles.Furniture.BookcaseTeleporter.Trigger(x, y);
        Wiring.TripWire(x, y, 1, 1);
        Wiring.SetCurrentUser();
        if (Main.netMode == NetmodeID.Server)
        {
            SendPacket(pindex, x, y, -1, fromWho);
        }
    }
}
