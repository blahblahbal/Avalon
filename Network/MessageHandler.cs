using System.IO;
using Terraria.ModLoader;

namespace Avalon.Network;

public class MessageHandler
{
	public static ModPacket GetPacket(MessageID messageID, int capacity = 256)
	{
		ModPacket message = ExxoAvalonOrigins.Mod.GetPacket(capacity);
		message.Write((byte)messageID);
		return message;
	}

	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		MessageID id = (MessageID)reader.ReadByte();
		switch (id)
		{
			//case MessageID.SyncTime:
			//    SyncTime.HandlePacket(reader, fromWho);
			//    break;
			case MessageID.SyncWiring:
				SyncWiring.HandlePacket(reader, fromWho);
				break;
			case MessageID.SyncLockUnlock:
				SyncLockUnlock.HandlePacket(reader, fromWho);
				break;
			case MessageID.SyncSkyBlessing:
				SyncSkyBlessing.HandlePacket(reader, fromWho);
				break;
			//case MessageID.SyncParticle:
			//	SyncParticles.HandlePacket(reader, fromWho);
			//	break;
			case MessageID.SyncOnHit:
				SyncOnHit.HandlePacket(reader, fromWho);
				break;
			case MessageID.SyncLongbowArrowEffect:
				SyncLongbowArrowEffect.HandlePacket(reader, fromWho);
				break;
			case MessageID.SyncPhantasmSpawn:
				SyncPhantasmSpawn.HandlePacket(reader, fromWho);
				break;
				//case MessageID.StaminaHeal:
				//    StaminaHeal.HandlePacket(reader, fromWho);
				//    break;
		}
	}
}
