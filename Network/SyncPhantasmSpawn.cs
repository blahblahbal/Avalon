using Avalon.NPCs.Bosses.Hardmode.Phantasm;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

public class SyncPhantasmSpawn
{
	public static void SendPacket(int x, int y, int player)
	{
		x /= 16;
		y /= 16;
		if (Main.netMode == NetmodeID.SinglePlayer)
			return;
		ModPacket packet = MessageHandler.GetPacket(MessageID.SyncPhantasmSpawn);
		packet.Write((ushort)x);
		packet.Write((ushort)y);
		packet.Write((byte)player);
		packet.Send();
	}
	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		ushort x = reader.ReadUInt16();
		ushort y = reader.ReadUInt16();
		byte player = reader.ReadByte();
		NPC.SpawnBoss(x * 16,y * 16, ModContent.NPCType<Phantasm>(), Main.myPlayer);
	}
}
