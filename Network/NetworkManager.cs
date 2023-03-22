using System.Collections.Generic;

namespace Avalon.Network;

public static class NetworkManager
{
    public static readonly List<IPacketHandler> RegisteredHandlers = new();
}
