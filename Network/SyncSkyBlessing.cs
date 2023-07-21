using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

internal class SyncSkyBlessing
{
    public static void SendPacket(byte PID, Vector2 position, int toClient = -1, int ignoreClient = -1)
    {
        ModPacket packet = MessageHandler.GetPacket(MessageID.SyncSkyBlessing);
        packet.Write(PID);
        packet.WriteVector2(position);
        packet.Send(toClient, ignoreClient);
    }
    public static void HandlePacket(BinaryReader reader, int fromWho)
    {
        byte PID = reader.ReadByte();
        Vector2 pos = reader.ReadVector2();

        Player p = Main.player[PID];
        for (int num214 = 0; num214 < 255; num214++)
        {
            Player player12 = Main.player[num214];
            if (!player12.active || player12.dead || (p.team != 0 && p.team != player12.team) || !(player12.Distance(pos) < 700f))
            {
                continue;
            }
            //dust stuff
            //Vector2 vel = Vector2.Normalize(p.Center - player12.Center);

            player12.GetModPlayer<AvalonPlayer>().LevelUpSkyBlessing();
        }

        if (Main.netMode == NetmodeID.Server)
        {
            SendPacket(PID, pos, -1, fromWho);
        }
    }
}
