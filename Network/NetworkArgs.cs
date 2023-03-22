using Terraria;

namespace Avalon.Network;

public class NetworkArgs
{
    public static readonly NetworkArgs Empty = new();
}

public class BasicPlayerNetworkArgs : NetworkArgs
{
    public readonly Player Player;

    public BasicPlayerNetworkArgs(Player player)
    {
        Player = player;
    }
}
