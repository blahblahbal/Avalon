using Avalon.Common.Interfaces;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Network;

public class SyncOnHit
{
	public static void SendPacket(bool item, int damageDealer, Player player, NPC target, bool crit, int hitDirection)
	{
		if (Main.netMode == NetmodeID.SinglePlayer)
		{
			return; // no sending packets in singleplayer :D
		}
		ModPacket packet = MessageHandler.GetPacket(MessageID.SyncOnHit);
		packet.WriteFlags(crit, hitDirection == 1, item);
		packet.Write(damageDealer);
		packet.Write((byte)player.whoAmI);
		packet.Write(target.whoAmI);
		packet.Send(ignoreClient: player.whoAmI);
	}
	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		reader.ReadFlags(out bool crit, out bool hitDir, out  bool item);
		int damagedealer = reader.ReadInt32();
		byte player = reader.ReadByte();
		int target = reader.ReadInt32();
		if (player == Main.myPlayer)
			return;
		if (item)
		{
			if (Main.netMode == NetmodeID.Server)
			{
				SendPacket(true, damagedealer, Main.player[player], Main.npc[target],crit,hitDir? 1 : -1);
			}
			if (ContentSamples.ItemsByType[damagedealer].ModItem is ISyncedOnHitEffect i)
			{
				i.SyncedOnHitNPC(Main.player[player], Main.npc[target], crit, hitDir ? 1 : -1);
			}
		}
		else
		{
			if (Main.netMode == NetmodeID.Server)
			{
				SendPacket(false, damagedealer, Main.player[player], Main.npc[target], crit, hitDir ? 1 : -1);
			}
			if (Main.projectile.FirstOrDefault(x => x.identity == damagedealer).ModProjectile is ISyncedOnHitEffect i)
			{
				i.SyncedOnHitNPC(Main.player[player], Main.npc[target], crit, hitDir ? 1 : -1);
			}
		}
	}
}
