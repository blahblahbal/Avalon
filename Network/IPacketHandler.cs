using System.IO;

namespace Avalon.Network;

public interface IPacketHandler
{
    public void Handle(BinaryReader reader, int fromWho);
}
