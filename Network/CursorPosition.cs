using System.IO;
using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Network;

public class CursorPosition
{
    public static void SendPacket(Vector2 mousePosition, int playerIndex, int toClient = -1, int ignoreClient = -1)
    {
        ModPacket message = MessageHandler.GetPacket(MessageID.CursorPosition);
        message.WriteVector2(mousePosition);
        message.Write(playerIndex);
        message.Send(toClient, ignoreClient);
    }

    public static void HandlePacket(BinaryReader reader, int fromWho)
    {
        Vector2 position = reader.ReadVector2();
        int whoAmI = reader.ReadInt32();
        // If server recieved a message, forward this to all clients, ignoring the sender
        if (Main.netMode == NetmodeID.Server)
        {
            SendPacket(position, whoAmI, -1, fromWho);
        }
        else
        {
            Player player = Main.player[whoAmI];
            AvalonPlayer modPlayer = player.GetModPlayer<AvalonPlayer>();
            modPlayer.MousePosition = position;
        }
    }
}
