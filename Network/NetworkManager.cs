using System.Collections.Generic;

namespace ExxoAvalonOrigins.Network;

public static class NetworkManager
{
    public static readonly List<IPacketHandler> RegisteredHandlers = new();
}
