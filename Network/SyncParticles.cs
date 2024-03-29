using Avalon.Common.Players;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

internal class SyncParticles
{
    public static void SendPacket(ParticleSystem.ParticleType type, Vector2 position, Vector2 velocity, Color color, float ai1 = 0, float ai2 = 0, float ai3 = 0, int toClient = -1, int ignoreClient = -1)
    {
        ModPacket packet = MessageHandler.GetPacket(MessageID.SyncParticle);
        packet.Write((int)type);
        packet.WriteVector2(position);
        packet.WriteVector2(velocity);
        packet.WriteRGB(color);
        packet.Write(ai1);
        packet.Write(ai2);
        packet.Write(ai3);
        packet.Send(toClient, ignoreClient);
    }
    public static void HandlePacket(BinaryReader reader, int fromWho)
    {
        ParticleSystem.ParticleType type = (ParticleSystem.ParticleType)reader.ReadInt32();
        Vector2 pos = reader.ReadVector2();
        Vector2 vel = reader.ReadVector2();
        Color c = reader.ReadRGB();
        float ai1 = reader.ReadSingle();
        float ai2 = reader.ReadSingle();
        float ai3 = reader.ReadSingle();

        if (type == ParticleSystem.ParticleType.SanguineCuts)
        {
            ParticleSystem.AddParticle(new SanguineCuts(), pos, vel, c, ai1, ai2, ai3);
        }
        if (type == ParticleSystem.ParticleType.CrystalSparkle)
        {
            ParticleSystem.AddParticle(new CrystalSparkle(), pos, vel, c, ai1, ai2, ai3);
        }

        if (Main.netMode == NetmodeID.Server)
        {
            SendPacket(type, pos, vel, c, ai1, ai2, ai3, -1, fromWho);
        }
    }
}
