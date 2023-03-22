using System.Diagnostics.CodeAnalysis;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

public abstract class PacketHandler<T> : IPacketHandler, ILoadable
    where T : NetworkArgs
{
    public int MessageId { get; private set; }
    [NotNull]
    protected Mod? Mod { get; private set; }

    public void Send(T args, int toClient = -1, int ignoreClient = -1)
    {
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            return;
        }

        ModPacket packet = Mod.GetPacket();
        packet.Write((byte)MessageId);
        Send(packet, args);
        packet.Send(toClient, ignoreClient);
    }

    public void Load(Mod mod)
    {
        Mod = mod;
        MessageId = NetworkManager.RegisteredHandlers.Count;
        NetworkManager.RegisteredHandlers.Add(this);
    }

    public void Unload()
    {
    }

    public void Handle(BinaryReader reader, int fromWho)
    {
        T args = Handle(reader);
        if (Main.netMode == NetmodeID.Server)
        {
            Send(args, -1, fromWho);
        }
    }

    protected abstract T Handle(BinaryReader reader);

    protected abstract void Send(ModPacket packet, T args);
}
