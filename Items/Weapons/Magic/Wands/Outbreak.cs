using Avalon;
using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Wands;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Wands;
public class Outbreak : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToStaff(ProjectileID.WoodenArrowFriendly /* it was set to this before and honestly idk what happens if you set it to 0 */, 90, 0f, 20, 6f, 45, 45, true);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 8);
		Item.UseSound = SoundID.Item46;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		const int Rad = 100;
		const int AttackRad = 130;
		Vector2 AttackPosition = Main.MouseWorld;
		player.LimitPointToPlayerReachableArea(ref AttackPosition);
		float iterationDistance = ContentSamples.ItemsByType[Type].shootSpeed;
		for (float i = 0; i < position.Distance(AttackPosition); i += iterationDistance)
		{
			Vector2 curCheckPos = position + position.SafeDirectionTo(AttackPosition) * i;
			if (position.DistanceSQ(curCheckPos) > position.DistanceSQ(AttackPosition)) break;

			if (!Collision.CanHit(curCheckPos - position.SafeDirectionTo(AttackPosition) * iterationDistance, 0, 0, curCheckPos, 0, 0))
			{
				AttackPosition = curCheckPos;
				break;
			}
		}
		for (int j = 0; j < 15; j++)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust d = Dust.NewDustPerfect(AttackPosition, DustID.Stone, Main.rand.NextVector2CircularEdge(Rad, Rad), 0, Color.Lerp(Color.OliveDrab, Color.MediumPurple, Main.rand.NextFloat()), 1.5f);
				d.velocity *= 0.1f;
				d.noGravity = true;
				d.color.A = 200;

				Dust d2 = Dust.NewDustPerfect(AttackPosition, DustID.Stone, Main.rand.NextVector2CircularEdge(Rad, Rad), 0, Color.Lerp(Color.OliveDrab, Color.MediumPurple, Main.rand.NextFloat()), 1f);
				d2.velocity *= 0.05f;
				d2.noGravity = true;
				d2.color.A = 200;
			}

			Vector2 d3VelRand = Main.rand.NextVector2CircularEdge(AttackRad, AttackRad);
			Dust d3 = Dust.NewDustPerfect(AttackPosition + d3VelRand, DustID.Stone, -d3VelRand.RotatedByRandom(MathHelper.PiOver2) * 0.25f, 0, Color.Lerp(Color.OliveDrab, Color.MediumPurple, Main.rand.NextFloat()), 1.5f);
			d3.velocity *= Main.rand.NextFloat(0.068f, 0.072f);
			d3.noGravity = true;
			d3.color.A = 200;

			Vector2 d4VelRand = Main.rand.NextVector2CircularEdge(AttackRad, AttackRad);
			Dust d4 = Dust.NewDustPerfect(AttackPosition + d4VelRand, DustID.Stone, -d4VelRand.RotatedByRandom(MathHelper.PiOver2) * 0.25f, 0, Color.Lerp(Color.OliveDrab, Color.MediumPurple, Main.rand.NextFloat()), 1f);
			d4.velocity *= 0.05f;
			d4.noGravity = true;
			d4.color.A = 200;
		}

		List<NPC> hitTargets = [];
		foreach (var npc in Main.ActiveNPCs)
		{
			if (Vector2.Distance(npc.Center, AttackPosition) < AttackRad && !npc.dontTakeDamage && (!npc.friendly || npc.type == NPCID.Guide && player.killGuide || npc.type == NPCID.Clothier && player.killClothier))
			{
				hitTargets.Add(npc);
			}
		}
		if (hitTargets.Count > 0)
		{
			foreach (var npc in hitTargets)
			{
				int DPS = npc.SimpleStrikeNPC((int)(damage / (1f + (hitTargets.Count - 1) / 10f)), player.direction, Main.rand.NextBool(player.GetWeaponCrit(player.HeldItem), 100), knockback, DamageClass.Magic, true, player.luck);
				player.addDPS(DPS);

				if (Main.rand.NextBool(5))
				{
					npc.AddBuff(ModContent.BuffType<Pathogen>(), Math.Max(TimeUtils.SecondsToTicks(1), (int)(TimeUtils.SecondsToTicks(10) / (1f + (hitTargets.Count - 1) / 5f))));
				}
				if (Main.rand.NextBool(3))
				{
					npc.AddBuff(BuffID.Poisoned, Math.Max(TimeUtils.SecondsToTicks(1), (int)(TimeUtils.SecondsToTicks(10) / (1f + (hitTargets.Count - 1) / 5f))));
				}

				for (int j = 0; j < 10; j++)
				{
					Dust d = Dust.NewDustPerfect(npc.Center, DustID.Stone, Main.rand.NextVector2Circular(npc.width, npc.height), 0, Color.Lerp(Color.OliveDrab, Color.MediumPurple, Main.rand.NextFloat()), 1.5f);
					d.velocity *= 0.1f;
					d.noGravity = true;
					d.color.A = 200;
				}
			}
		}
		//if (hitTargets.Count > 1)
		//{
		//	hitTargets.Sort(SortByDist);
		//}
		for (int i = 0; i < Math.Min(hitTargets.Count, 1); i++)
		{
			//Projectile.NewProjectile(source, AttackPosition, Vector2.UnitX.RotatedByRandom(Math.Tau), ModContent.ProjectileType<OutbreakProj>(), (int)(damage / (1f + (hitTargets.Count - 1) / 10f)), knockback, player.whoAmI, hitTargets[Main.rand.Next(hitTargets.Count)].whoAmI, player.GetWeaponCrit(player.HeldItem), Main.rand.Next());

			NPC target = hitTargets[Main.rand.Next(hitTargets.Count)];
			int currentLatchCount = 0;
			foreach (Projectile proj in Main.ActiveProjectiles)
			{
				if (proj.type == ModContent.ProjectileType<OutbreakProj>() && proj.ai[0] == target.whoAmI)
				{
					currentLatchCount++;
				}
			}
			if (currentLatchCount < 6 && Main.rand.NextBool(currentLatchCount / 3 + 1))
			{
				Projectile.NewProjectile(source, AttackPosition, Vector2.UnitX, ModContent.ProjectileType<OutbreakProj>(), (int)(damage / (1f + (hitTargets.Count - 1) / 10f)), knockback, player.whoAmI, target.whoAmI, Main.rand.Next());
			}
		}
		//for (int i = 0; i < 20; i++)
		//{
		//	Projectile.NewProjectile(source, AttackPosition, new Vector2(Rad, 0).RotatedBy(MathF.Tau / 20 * i) * Main.rand.NextFloat(0.02f, 0.03f), ModContent.ProjectileType<OutbreakProj>(), 0, 0, player.whoAmI, Main.rand.NextFloat(MathF.Tau), Main.rand.NextFloat(MathF.Tau), i < hitTargets.Count ? hitTargets[i].whoAmI : -1);
		//}
		return false;
	}
	public override Vector2? HoldoutOrigin()
	{
		return new Vector2(0, 0);
	}
	public static int SortByDist(NPC x, NPC y)
	{
		if (x == null)
		{
			if (y == null)
			{
				return 0;
			}
			else
			{
				return -1;
			}
		}
		else
		{
			if (y == null)
			{
				return 1;
			}
			else
			{
				float xDist = x.Distance(Main.MouseWorld);
				float yDist = y.Distance(Main.MouseWorld);
				int retval = xDist.CompareTo(yDist);

				if (retval != 0)
				{
					return retval;
				}
				else
				{
					return 0;
				}
			}
		}
	}
}