using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Other;

public class PhantomKnife : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 8;
		Projectile.height = 8;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
		Projectile.extraUpdates = 0;
		Projectile.alpha = -1000;
	}
	public override bool PreAI()
	{
		Lighting.AddLight(Projectile.position, 35 / 255f, 67 / 255f, 67 / 255f);
		return true;
	}
	public override void AI()
	{
		Projectile.localAI[1]++;

		Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;

		if (Projectile.type == ModContent.ProjectileType<PhantomKnife>())
		{
			Projectile.ai[0]++;
			if (Projectile.ai[0] >= 30f)
			{
				Projectile.alpha += 10;
				if (Projectile.alpha >= 255)
				{
					Projectile.active = false;
				}
			}
			if (Projectile.ai[0] < 30f)
			{
				Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
			}
		}
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ghostHurt(Projectile.damage, Projectile.position);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		ghostHurt(Projectile.damage, Projectile.position);
	}

	public void ghostHurt(int dmg, Vector2 Position)
	{
		if (Projectile.DamageType != DamageClass.Magic || Projectile.damage <= 0)
		{
			return;
		}
		int num = Projectile.damage;
		if (dmg <= 1)
		{
			return;
		}
		int[] array = new int[200];
		int num4 = 0;
		_ = new int[200];
		int num5 = 0;
		foreach (var npc in Main.ActiveNPCs)
		{
			if (!npc.CanBeChasedBy(this))
			{
				continue;
			}
			float num6 = Math.Abs(npc.position.X + (float)(npc.width / 2) - Projectile.position.X + (float)(Projectile.width / 2)) + Math.Abs(npc.position.Y + (float)(npc.height / 2) - Projectile.position.Y + (float)(Projectile.height / 2));
			if (num6 < 800f)
			{
				if (Collision.CanHit(Projectile.position, 1, 1, npc.position, npc.width, npc.height) && num6 > 50f)
				{
					array[num5] = npc.whoAmI;
					num5++;
				}
				else if (num5 == 0)
				{
					array[num4] = npc.whoAmI;
					num4++;
				}
			}
		}
		if (num4 != 0 || num5 != 0)
		{
			int num2 = ((num5 <= 0) ? array[Main.rand.Next(num4)] : array[Main.rand.Next(num5)]);
			float num7 = Main.rand.Next(-100, 101);
			float num8 = Main.rand.Next(-100, 101);
			float num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
			num9 = 4f / num9;
			num7 *= num9;
			num8 *= num9;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Position, new Vector2(num7, num8), ModContent.ProjectileType<PhantomKnifeSplit>(), num, 0f, Projectile.owner, num2);
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int num461 = 0; num461 < 3; num461++)
		{
			int num462 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 250, default, 0.8f);
			Main.dust[num462].noGravity = true;
			Dust dust = Main.dust[num462];
			dust.velocity *= 1.2f;
			dust = Main.dust[num462];
			dust.velocity -= Projectile.oldVelocity * 0.3f;
		}
	}
}
