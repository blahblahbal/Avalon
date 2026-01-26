using Avalon.Network;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common.Interfaces;

public interface ISyncedOnHitEffect
{
	void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection);
}
public class SyncedOnHitGlobalItem : GlobalItem
{
	public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if(item.ModItem is ISyncedOnHitEffect i)
		{
			i.SyncedOnHitNPC(player, target, hit.Crit, hit.HitDirection);
			SyncOnHit.SendPacket(true, item.type, player, target, hit.Crit, hit.HitDirection);
		}
	}
}
public class SyncedOnHitGlobalProjectile : GlobalProjectile
{
	public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (projectile.ModProjectile is ISyncedOnHitEffect i)
		{
			i.SyncedOnHitNPC(Main.player[projectile.owner], target, hit.Crit, hit.HitDirection);
			SyncOnHit.SendPacket(false, projectile.identity, Main.player[projectile.owner], target, hit.Crit, hit.HitDirection);
		}
	}
}