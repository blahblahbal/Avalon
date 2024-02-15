using System.IO;
using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Network.Handlers;

[Autoload]
public class SyncStingerProbe : PacketHandler<BasicPlayerNetworkArgs>
{
    protected override BasicPlayerNetworkArgs Handle(BinaryReader reader)
    {
        byte playerIndex = reader.ReadByte();
        Player player = Main.player[playerIndex];
        AvalonPlayer buffPlayer = player.GetModPlayer<AvalonPlayer>();
        buffPlayer.StingerProbeRotation = reader.ReadSingle();
        return new BasicPlayerNetworkArgs(player);
    }

    protected override void Send(ModPacket packet, BasicPlayerNetworkArgs args)
    {
        Player player = args.Player;
        AvalonPlayer buffPlayer = player.GetModPlayer<AvalonPlayer>();
        packet.Write((byte)player.whoAmI);
        packet.Write(buffPlayer.StingerProbeRotation);
    }
}
