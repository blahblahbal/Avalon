using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah.Knives;

public class BlahsKnives : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<BlahKnife>(), 95, 3.75f, 14, 15f, 14, true, noUseGraphic: true, width: 18, height: 20);
		Item.rare = ModContent.RarityType<BlahRarity>();
		Item.value = Item.sellPrice(0, 50);
		Item.UseSound = SoundID.Item39;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
							   int type, int damage, float knockback)
	{
		int numberProjectiles = Main.rand.Next(4, 8); // AvalonGlobalProjectile.HowManyProjectiles(4, 8);
		for (int i = 0; i < numberProjectiles; i++)
		{
			Vector2 perturbedSpeed = AvalonUtils.GetShootSpread(velocity, position, Type, MathHelper.ToRadians(20), random: true, maxRotUnsigned: Math.PI);
			Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
		}

		return false;
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddIngredient(ModContent.ItemType<Magic.PhantomKnives>())
	//        .AddIngredient(ModContent.ItemType<Melee.KnivesoftheCorruptor>())
	//        .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 40)
	//        .AddIngredient(ModContent.ItemType<Placeable.Bar.SuperhardmodeBar>(), 35)
	//        .AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 40)
	//        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
	//        .Register();
	//}
}
public class BlahKnife : ModProjectile
{
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
		Projectile.extraUpdates = 0;
	}
	public override void AI()
	{
		Lighting.AddLight(Projectile.position, 140 / 255f, 90 / 255f, 50 / 255f);
		var num28 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
		var num29 = Projectile.localAI[0];
		if (num29 == 0f)
		{
			Projectile.localAI[0] = num28;
			num29 = num28;
		}
		var projPosStoredX = Projectile.position.X;
		var projPosStoredY = Projectile.position.Y;
		var distance = 320f;
		var flag = false;
		var npcArrayIndexStored = 0;
		if (Projectile.ai[1] == 0f)
		{
			for (var npcArrayIndex = 0; npcArrayIndex < 200; npcArrayIndex++)
			{
				if (Main.npc[npcArrayIndex].active && !Main.npc[npcArrayIndex].dontTakeDamage && !Main.npc[npcArrayIndex].friendly && Main.npc[npcArrayIndex].lifeMax > 5 && (Projectile.ai[1] == 0f || Projectile.ai[1] == npcArrayIndex + 1))
				{
					var npcCenterX = Main.npc[npcArrayIndex].position.X + Main.npc[npcArrayIndex].width / 2;
					var npcCenterY = Main.npc[npcArrayIndex].position.Y + Main.npc[npcArrayIndex].height / 2;
					var num37 = Math.Abs(Projectile.position.X + Projectile.width / 2 - npcCenterX) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - npcCenterY);
					if (num37 < distance && Collision.CanHit(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[npcArrayIndex].position, Main.npc[npcArrayIndex].width, Main.npc[npcArrayIndex].height))
					{
						distance = num37;
						projPosStoredX = npcCenterX;
						projPosStoredY = npcCenterY;
						flag = true;
						npcArrayIndexStored = npcArrayIndex;
					}
				}
			}
			if (flag)
			{
				Projectile.ai[1] = npcArrayIndexStored + 1;
			}
			flag = false;
		}
		if (Projectile.ai[1] != 0f)
		{
			var npcArrayIndexAgain = (int)(Projectile.ai[1] - 1f);
			if (Main.npc[npcArrayIndexAgain].active)
			{
				var npcCenterX = Main.npc[npcArrayIndexAgain].position.X + Main.npc[npcArrayIndexAgain].width / 2;
				var npcCenterY = Main.npc[npcArrayIndexAgain].position.Y + Main.npc[npcArrayIndexAgain].height / 2;
				var num41 = Math.Abs(Projectile.position.X + Projectile.width / 2 - npcCenterX) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - npcCenterY);
				if (num41 < 1000f)
				{
					flag = true;
					projPosStoredX = Main.npc[npcArrayIndexAgain].position.X + Main.npc[npcArrayIndexAgain].width / 2;
					projPosStoredY = Main.npc[npcArrayIndexAgain].position.Y + Main.npc[npcArrayIndexAgain].height / 2;
				}
			}
		}
		if (flag)
		{
			var num42 = num29;
			var projCenter = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
			var num43 = projPosStoredX - projCenter.X;
			var num44 = projPosStoredY - projCenter.Y;
			var num45 = (float)Math.Sqrt(num43 * num43 + num44 * num44);
			num45 = num42 / num45;
			num43 *= num45;
			num44 *= num45;
			var num46 = 8;
			Projectile.velocity.X = (Projectile.velocity.X * (num46 - 1) + num43) / num46;
			Projectile.velocity.Y = (Projectile.velocity.Y * (num46 - 1) + num44) / num46;
		}
		Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ghostHurt(Projectile.damage, Projectile.position);
		Main.player[Projectile.owner].VampireHeal((int)(Projectile.damage * 0.4f), Projectile.position);
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
			float num6 = Math.Abs(npc.position.X + npc.width / 2 - Projectile.position.X + Projectile.width / 2) + Math.Abs(npc.position.Y + npc.height / 2 - Projectile.position.Y + Projectile.height / 2);
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
			int num2 = num5 <= 0 ? array[Main.rand.Next(num4)] : array[Main.rand.Next(num5)];
			float num7 = Main.rand.Next(-100, 101);
			float num8 = Main.rand.Next(-100, 101);
			float num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
			num9 = 4f / num9;
			num7 *= num9;
			num8 *= num9;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Position, new Vector2(num7, num8), ModContent.ProjectileType<BlahKnifeSplit>(), num, 0f, Projectile.owner, num2);
		}
	}
}
public class BlahKnifeSplit : ModProjectile
{
	public override string Texture => ModContent.GetInstance<BlahKnife>().Texture;
	public override void SetDefaults()
	{
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.aiStyle = -1;
		Projectile.hide = true;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
		Projectile.friendly = false;
		Projectile.hostile = false;
		Projectile.extraUpdates = 3;
	}
	public override void AI()
	{
		Projectile.ai[1]++;
		if (Projectile.ai[1] >= 60)
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
						float num486 = Math.Abs(Main.npc[num485].Center.X - Projectile.Center.X) + Math.Abs(Main.npc[num485].Center.Y - Projectile.Center.Y);
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
			Vector2 vector27 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
			float num488 = Main.npc[num483].Center.X - vector27.X;
			float num489 = Main.npc[num483].Center.Y - vector27.Y;
			float num490 = (float)Math.Sqrt(num488 * num488 + num489 * num489);
			float num491 = num490;
			num490 = num487 / num490;
			num488 *= num490;
			num489 *= num490;
			int num492 = 30;
			Projectile.velocity.X = (Projectile.velocity.X * (num492 - 1) + num488) / num492;
			Projectile.velocity.Y = (Projectile.velocity.Y * (num492 - 1) + num489) / num492;
		}
		for (int num493 = 0; num493 < 1; num493++)
		{
			float num494 = Projectile.velocity.X * 0.2f * num493;
			float num495 = (0f - Projectile.velocity.Y * 0.2f) * num493;
			int num496 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.3f);
			Main.dust[num496].noGravity = true;
			Dust dust = Main.dust[num496];
			dust.velocity *= 0f;
			Main.dust[num496].position.X -= num494;
			Main.dust[num496].position.Y -= num495;
		}
		for (int num493 = 0; num493 < 1; num493++)
		{
			float num494 = Projectile.velocity.X * 0.2f * num493;
			float num495 = (0f - Projectile.velocity.Y * 0.2f) * num493;
			int num496 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SilverCoin, 0f, 0f, 100, default, 1.3f);
			Main.dust[num496].noGravity = true;
			Dust dust = Main.dust[num496];
			dust.velocity *= 0f;
			Main.dust[num496].position.X -= num494;
			Main.dust[num496].position.Y -= num495;
		}
	}
}
