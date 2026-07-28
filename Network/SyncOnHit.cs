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
	public static void SendPacket(bool item, int damageDealer, Player player, NPC target, Rectangle targetHitbox, int damage, float knockback, bool crit, int hitDirection)
	{
		if (Main.netMode == NetmodeID.SinglePlayer)
			return;
		ModPacket packet = MessageHandler.GetPacket(MessageID.SyncOnHit);
		packet.WriteFlags(crit, hitDirection == 1, item);
		packet.Write(damage);
		packet.Write(knockback);
		packet.Write((short)damageDealer);
		packet.Write((byte)player.whoAmI);
		packet.Write((short)target.whoAmI);
		packet.Write(targetHitbox.X);
		packet.Write(targetHitbox.Y);
		packet.Write(targetHitbox.Width);
		packet.Write(targetHitbox.Height);
		packet.Send(ignoreClient: player.whoAmI);
	}
	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		reader.ReadFlags(out bool crit, out bool hitDir, out bool item);
		int damage = reader.ReadInt32();
		float knockback = reader.ReadSingle();
		short damagedealer = reader.ReadInt16();
		byte player = reader.ReadByte();
		short target = reader.ReadInt16();
		Rectangle hitbox = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());

		if (Main.netMode == NetmodeID.Server)
			SendPacket(item, damagedealer, Main.player[player], Main.npc[target], hitbox, damage, knockback, crit, hitDir ? 1 : -1);

		if (item)
		{
			Item damageItem = ContentSamples.ItemsByType[damagedealer];
			if (damageItem.ModItem is ISyncedOnHitEffect i)
				i.SyncedOnHitNPC(Main.player[player], Main.npc[target], hitbox, damage, knockback, crit, hitDir ? 1 : -1, null);
			foreach (GlobalItem gi in damageItem.Globals)
			{
				if (gi is ISyncedOnHitEffect i2)
					i2.SyncedOnHitNPC(Main.player[player], Main.npc[target], hitbox, damage, knockback, crit, hitDir ? 1 : -1, null);
			}
		}
		else
		{
			Projectile damageProj = Main.projectile.FirstOrDefault(x => x.identity == damagedealer && x.owner == player);
			if (damageProj != null)
			{
				if (damageProj.ModProjectile is ISyncedOnHitEffect i)
					i.SyncedOnHitNPC(Main.player[player], Main.npc[target], hitbox, damage, knockback, crit, hitDir ? 1 : -1, damageProj);
				foreach (GlobalProjectile gp in damageProj.Globals)
				{
					if (gp is ISyncedOnHitEffect i2)
						i2.SyncedOnHitNPC(Main.player[player], Main.npc[target], hitbox, damage, knockback, crit, hitDir ? 1 : -1, damageProj);
				}
			}
		}
	}
}
