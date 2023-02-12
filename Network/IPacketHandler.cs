using System.IO;

namespace ExxoAvalonOrigins.Network;

public interface IPacketHandler
{
    public void Handle(BinaryReader reader, int fromWho);
}
