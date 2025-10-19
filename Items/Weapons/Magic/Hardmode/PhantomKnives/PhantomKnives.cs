using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.PhantomKnives;

public class PhantomKnives : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<PhantomKnife>(), 51, 3.75f, 18, 15f, 16, true, noUseGraphic: true, width: 18, height: 20);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 30);
		Item.UseSound = SoundID.Item39;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
							   int type, int damage, float knockback)
	{
		int numberProjectiles = Main.rand.Next(4, 8);
		for (int i = 0; i < numberProjectiles; i++)
		{
			Vector2 perturbedSpeed = AvalonUtils.GetShootSpread(velocity, position, Type, MathHelper.ToRadians(20), random: true, maxRotUnsigned: Math.PI);
			Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
		}

		return false;
	}
}
public class PhantomKnife : ModProjectile
{
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = dims.Width * 8 / 30;
		Projectile.height = dims.Height * 8 / 30 / Main.projFrames[Projectile.type];
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
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Position, new Vector2(num7, num8), ModContent.ProjectileType<SpectreSplit>(), num, 0f, Projectile.owner, num2);
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
public class SpectreSplit : ModProjectile
{
	public override string Texture => ModContent.GetInstance<PhantomKnife>().Texture;
	public override void SetDefaults()
	{
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.aiStyle = -1;
		Projectile.alpha = 255;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.friendly = false;
		Projectile.hostile = false;
		Projectile.extraUpdates = 3;
	}
	public override void AI()
	{
		Projectile.ai[1] += 1f;
		if (Projectile.ai[1] >= 60f)
		{
			Projectile.friendly = true;
			int num483 = (int)Projectile.ai[0];
			if (!Main.npc[num483].active)
			{
				num483 = -1;
				int[] array2 = new int[200];
				int num484 = 0;
				for (int num485 = 0; num485 < 200; num485++)
				{
					if (Main.npc[num485].CanBeChasedBy(this))
					{
						float num486 = Math.Abs(Main.npc[num485].position.X + (float)(Main.npc[num485].width / 2) - Projectile.position.X + (float)(Projectile.width / 2)) + Math.Abs(Main.npc[num485].position.Y + (float)(Main.npc[num485].height / 2) - Projectile.position.Y + (float)(Projectile.height / 2));
						if (num486 < 800f)
						{
							array2[num484] = num485;
							num484++;
						}
					}
				}
				if (num484 == 0)
				{
					Projectile.Kill();
					return;
				}
				num483 = array2[Main.rand.Next(num484)];
				Projectile.ai[0] = num483;
			}
			float num487 = 4f;
			Vector2 vector27 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num488 = Main.npc[num483].Center.X - vector27.X;
			float num489 = Main.npc[num483].Center.Y - vector27.Y;
			float num490 = (float)Math.Sqrt(num488 * num488 + num489 * num489);
			float num491 = num490;
			num490 = num487 / num490;
			num488 *= num490;
			num489 *= num490;
			int num492 = 30;
			Projectile.velocity.X = (Projectile.velocity.X * (float)(num492 - 1) + num488) / (float)num492;
			Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num492 - 1) + num489) / (float)num492;
		}
		for (int num493 = 0; num493 < 1; num493++)
		{
			float num494 = Projectile.velocity.X * 0.2f * (float)num493;
			float num495 = (0f - Projectile.velocity.Y * 0.2f) * (float)num493;
			int num496 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 100, default(Color), 1.3f);
			Main.dust[num496].noGravity = true;
			Dust dust = Main.dust[num496];
			dust.velocity *= 0f;
			Main.dust[num496].position.X -= num494;
			Main.dust[num496].position.Y -= num495;
		}
	}
}
