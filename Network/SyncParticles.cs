using Avalon.Common.Players;
using Avalon.Particles.OldParticleSystem;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

internal class SyncParticles
{
	/// <summary>
	/// Simply returns before doing anything when called in singleplayer, so you can safely call this whenever.<br></br>
	/// Might still want to check if Main.netMode == NetmodeID.MultiplayerClient so the method doesn't run at all though, idk.
	/// </summary>
	public static void SendPacket(ParticleType type, Vector2 position, Vector2 velocity, int toClient = -1, int ignoreClient = -1)
	{
		if (Main.netMode == NetmodeID.SinglePlayer)
		{
			return; // no sending packets in singleplayer :D
		}
		ModPacket packet = MessageHandler.GetPacket(MessageID.SyncParticle);
		packet.Write((byte)type);
		packet.WriteVector2(position);
		packet.WriteVector2(velocity);
		packet.Send(toClient, ignoreClient);
	}
	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		byte type = reader.ReadByte();
		Vector2 pos = reader.ReadVector2();
		Vector2 vel = reader.ReadVector2();

		if (Main.netMode == NetmodeID.Server)
		{
			SendPacket((ParticleType)type, pos, vel, -1, fromWho);
		}
		else
		{
			OldParticleSystemDeleteSoon.AddParticle(type, pos, vel, default, default, default, default);
		}
	}
}
