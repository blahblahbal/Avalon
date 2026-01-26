using Avalon.Common.Interfaces;
using Microsoft.Xna.Framework;
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
			return;
		ModPacket packet = MessageHandler.GetPacket(MessageID.SyncOnHit);
		packet.WriteFlags(crit, hitDirection == 1, item);
		packet.Write((short)damageDealer);
		packet.Write((byte)player.whoAmI);
		packet.Write((short)target.whoAmI);
		packet.Send(ignoreClient: player.whoAmI);
	}
	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		reader.ReadFlags(out bool crit, out bool hitDir, out  bool item);
		short damagedealer = reader.ReadInt16();
		byte player = reader.ReadByte();
		short target = reader.ReadInt16();
		if (Main.netMode == NetmodeID.Server)
		{
			SendPacket(item, damagedealer, Main.player[player], Main.npc[target], crit, hitDir ? 1 : -1);
		}

		if (item && ContentSamples.ItemsByType[damagedealer].ModItem is ISyncedOnHitEffect i)
		{
			i.SyncedOnHitNPC(Main.player[player], Main.npc[target], crit, hitDir ? 1 : -1);
		}
		else if (Main.projectile.FirstOrDefault(x => x.identity == damagedealer && x.owner == player).ModProjectile is ISyncedOnHitEffect i2)
		{
			i2.SyncedOnHitNPC(Main.player[player], Main.npc[target], crit, hitDir ? 1 : -1);
		}
	}
}
