using System.IO;
using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Network.Handlers;

[Autoload]
public class SyncMouse : PacketHandler<BasicPlayerNetworkArgs>
{
    protected override BasicPlayerNetworkArgs Handle(BinaryReader reader)
    {
        byte playerIndex = reader.ReadByte();
        Player player = Main.player[playerIndex];
        AvalonPlayer exxoPlayer = player.GetModPlayer<AvalonPlayer>();
        exxoPlayer.MousePosition = reader.ReadVector2();

        return new BasicPlayerNetworkArgs(player);
    }

    protected override void Send(ModPacket packet, BasicPlayerNetworkArgs args)
    {
        Player player = args.Player;
        AvalonPlayer exxoPlayer = player.GetModPlayer<AvalonPlayer>();
        packet.Write((byte)player.whoAmI);
        packet.WriteVector2(exxoPlayer.MousePosition);
    }
}
