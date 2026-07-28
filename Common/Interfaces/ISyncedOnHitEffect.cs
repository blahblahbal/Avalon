using Avalon.Network;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common.Interfaces;

public interface ISyncedOnHitEffect
{
	/// <summary>
	/// projectile is only for GlobalProjectiles.
	/// </summary>
	/// <param name="player"></param>
	/// <param name="target"></param>
	/// <param name="attackHitbox"></param>
	/// <param name="damage"></param>
	/// <param name="knockback"></param>
	/// <param name="crit"></param>
	/// <param name="hitDirection"></param>
	/// <param name="projectile"></param>
	/// <returns></returns>
	bool SyncedOnHitNPC(Player player, NPC target, Rectangle attackHitbox, int damage, float knockback, bool crit, int hitDirection, Projectile? projectile);
}
public class SyncedOnHitGlobalItem : GlobalItem
{
	public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		bool sync = false;
		player.ItemCheck_GetMeleeHitbox(item, Item.GetDrawHitbox(item.type, player), out var dontAttack, out var itemRectangle);
		if (item.ModItem is ISyncedOnHitEffect i)
		{
			if (i.SyncedOnHitNPC(player, target, itemRectangle, hit.Damage, hit.Knockback, hit.Crit, hit.HitDirection, null))
				sync = true;
		}
		foreach (GlobalItem gi in item.Globals)
		{
			if (gi is ISyncedOnHitEffect i2)
				if (i2.SyncedOnHitNPC(player, target, itemRectangle, hit.Damage, hit.Knockback, hit.Crit, hit.HitDirection, null))
					sync = true;
		}
		if (sync)
		{
			SyncOnHit.SendPacket(true, item.type, player, target, itemRectangle, hit.Damage, hit.Knockback, hit.Crit, hit.HitDirection);
		}
	}
}
public class SyncedOnHitGlobalProjectile : GlobalProjectile
{
	public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
	{
		bool sync = false;
		if (projectile.ModProjectile is ISyncedOnHitEffect i)
		{
			if (i.SyncedOnHitNPC(Main.player[projectile.owner], target, projectile.Hitbox, hit.Damage, hit.Knockback, hit.Crit, hit.HitDirection, projectile))
				sync = true;
		}
		foreach (GlobalProjectile gp in projectile.Globals)
		{
			if (gp is ISyncedOnHitEffect i2)
				if (i2.SyncedOnHitNPC(Main.player[projectile.owner], target, projectile.Hitbox, hit.Damage, hit.Knockback, hit.Crit, hit.HitDirection, projectile))
					sync = true;
		}
		if (sync)
		{
			SyncOnHit.SendPacket(false, projectile.identity, Main.player[projectile.owner], target, projectile.Hitbox, hit.Damage, hit.Knockback, hit.Crit, hit.HitDirection);
		}
	}
}