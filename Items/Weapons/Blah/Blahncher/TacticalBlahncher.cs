using Avalon;
using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah.Blahncher;

public class TacticalBlahncher : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToLauncher(160, 5f, 11f, 9, 9, crit: 7);
		Item.rare = ModContent.RarityType<BlahRarity>();
		Item.value = Item.sellPrice(1);
		Item.UseSound = SoundID.Item11;

	}

	//public override void AddRecipes() => CreateRecipe()
	//    .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 45)
	//    .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 40)
	//    .AddIngredient(ModContent.ItemType<SoulofTorture>(), 45).AddIngredient(ModContent.ItemType<TacticalExpulsor>())
	//    .AddIngredient(ItemID.RocketLauncher).AddIngredient(ItemID.GrenadeLauncher).AddIngredient(ItemID.Stynger)
	//    .AddTile(ModContent.TileType<SolariumAnvil>()).Register();

	public override Vector2? HoldoutOffset() => new Vector2(-10f, 0f);

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
							   int type, int damage, float knockback)
	{
		for (int i = 0; i < 3; i++)
		{
			Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 13.05f, Main.rand.NextFloat(-2.5f, 2.5f), ItemID.RocketI, true);
			if (Main.rand.NextBool(3))
			{
				vel *= Main.rand.NextFloat(0.2f, 1.8f);
			}

			Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<Blahcket>(), damage, knockback, player.whoAmI);
		}

		return false;
	}

	public override void HoldItem(Player player)
	{
		var vector = new Vector2(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
		float num70 = Main.mouseX + Main.screenPosition.X - vector.X;
		float num71 = Main.mouseY + Main.screenPosition.Y - vector.Y;
		if (player.gravDir == -1f)
		{
			num71 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
		}

		float num72 = (float)Math.Sqrt(num70 * num70 + num71 * num71);
		float num73 = num72;
		num72 = player.inventory[player.selectedItem].shootSpeed / num72;
		if (player.inventory[player.selectedItem].type == Item.type)
		{
			num70 += Main.rand.Next(-50, 51) * 0.03f / num72;
			num71 += Main.rand.Next(-50, 51) * 0.03f / num72;
		}

		num70 *= num72;
		num71 *= num72;
		player.itemRotation = (float)Math.Atan2(num71 * player.direction, num70 * player.direction);
	}

	public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.Next(4) >= 3;
}
public class Blahcket : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 10;
		Projectile.height = 10;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = 1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
		Projectile.position.X = Projectile.position.X + (Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
		Projectile.width = 22;
		Projectile.height = 22;
		Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);
		for (int num369 = 0; num369 < 20; num369++)
		{
			int num370 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num370].velocity *= 1.4f;
		}
		for (int num371 = 0; num371 < 10; num371++)
		{
			int num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 2.5f);
			Main.dust[num372].noGravity = true;
			Main.dust[num372].velocity *= 5f;
			num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num372].velocity *= 3f;
		}
		int num373 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Gore expr_B3F3_cp_0 = Main.gore[num373];
		expr_B3F3_cp_0.velocity.X = expr_B3F3_cp_0.velocity.X + 1f;
		Gore expr_B413_cp_0 = Main.gore[num373];
		expr_B413_cp_0.velocity.Y = expr_B413_cp_0.velocity.Y + 1f;
		num373 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Gore expr_B497_cp_0 = Main.gore[num373];
		expr_B497_cp_0.velocity.X = expr_B497_cp_0.velocity.X - 1f;
		Gore expr_B4B7_cp_0 = Main.gore[num373];
		expr_B4B7_cp_0.velocity.Y = expr_B4B7_cp_0.velocity.Y + 1f;
		num373 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Gore expr_B53B_cp_0 = Main.gore[num373];
		expr_B53B_cp_0.velocity.X = expr_B53B_cp_0.velocity.X + 1f;
		Gore expr_B55B_cp_0 = Main.gore[num373];
		expr_B55B_cp_0.velocity.Y = expr_B55B_cp_0.velocity.Y - 1f;
		num373 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Gore expr_B5DF_cp_0 = Main.gore[num373];
		expr_B5DF_cp_0.velocity.X = expr_B5DF_cp_0.velocity.X - 1f;
		Gore expr_B5FF_cp_0 = Main.gore[num373];
		expr_B5FF_cp_0.velocity.Y = expr_B5FF_cp_0.velocity.Y - 1f;
	}
	public override void AI()
	{
		if (Projectile.localAI[0] == 0)
		{
			Projectile.localAI[1] = Projectile.velocity.ToRotation();
			Projectile.localAI[0] = Projectile.velocity.Length();
		}
		Projectile.ai[0]++;
		if (Projectile.ai[0] < 45) Projectile.velocity.Y += 0.02f; //gravity
		if (Projectile.ai[0] == 45)
		{
			Vector2 dustPoint = Projectile.Center;
			Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.direction * -0.4f, -1.4f);
			Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, Projectile.direction * -0.4f, -1.4f, Scale: 1.5f);
			Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.direction * -0.4f, 1.4f);
			Dust.NewDust(dustPoint - Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, Projectile.direction * -0.4f, 1.4f, Scale: 1.5f);

			Projectile.velocity = Projectile.localAI[1].ToRotationVector2() * Projectile.localAI[0] * 2.1f;
		}
		else if (Projectile.ai[0] > 4)
		{
			for (int num126 = 0; num126 < 5; num126++)
			{
				float num127 = Projectile.velocity.X / 3f * (float)num126;
				float num128 = Projectile.velocity.Y / 3f * (float)num126;
				int num129 = 4;
				int num130 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num129, Projectile.position.Y + (float)num129), Projectile.width - num129 * 2, Projectile.height - num129 * 2, DustID.Torch, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num130].noGravity = true;
				Main.dust[num130].velocity *= 0.1f;
				Main.dust[num130].velocity += Projectile.velocity * 0.1f;
				Dust expr_62C2_cp_0 = Main.dust[num130];
				expr_62C2_cp_0.position.X = expr_62C2_cp_0.position.X - num127;
				Dust expr_62DD_cp_0 = Main.dust[num130];
				expr_62DD_cp_0.position.Y = expr_62DD_cp_0.position.Y - num128;
			}
			if (Main.rand.NextBool(5))
			{
				int num131 = 4;
				int num132 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num131, Projectile.position.Y + (float)num131), Projectile.width - num131 * 2, Projectile.height - num131 * 2, DustID.Smoke, 0f, 0f, 100, default(Color), 0.6f);
				Main.dust[num132].velocity *= 0.25f;
				Main.dust[num132].velocity += Projectile.velocity * 0.5f;
			}
		}
		float num26 = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
		float num27 = Projectile.localAI[0];
		if (num27 == 0f)
		{
			Projectile.localAI[0] = num26;
			num27 = num26;
		}
		if (Projectile.alpha > 0)
		{
			Projectile.alpha -= 25;
		}
		if (Projectile.alpha < 0)
		{
			Projectile.alpha = 0;
		}
		float num28 = Projectile.position.X;
		float num29 = Projectile.position.Y;
		float num30 = 300;
		bool flag = false;
		int num31 = 0;
		if (Projectile.ai[1] == 0f)
		{
			for (int num32 = 0; num32 < 200; num32++)
			{
				if (Main.npc[num32].active && !Main.npc[num32].dontTakeDamage && !Main.npc[num32].friendly && Main.npc[num32].lifeMax > 5 && (Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(num32 + 1)))
				{
					float num33 = Main.npc[num32].position.X + (float)(Main.npc[num32].width / 2);
					float num34 = Main.npc[num32].position.Y + (float)(Main.npc[num32].height / 2);
					float num35 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num33) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num34);
					if (num35 < num30 && Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[num32].position, Main.npc[num32].width, Main.npc[num32].height))
					{
						num30 = num35;
						num28 = num33;
						num29 = num34;
						flag = true;
						num31 = num32;
					}
				}
			}
			if (flag)
			{
				Projectile.ai[1] = (num31 + 1);
			}
		}
		if (Projectile.ai[1] != 0f)
		{
			int num36 = (int)(Projectile.ai[1] - 1f);

			if (Main.npc[num36].active)
			{
				float num37 = Main.npc[num36].position.X + (float)(Main.npc[num36].width / 2);
				float num38 = Main.npc[num36].position.Y + (float)(Main.npc[num36].height / 2);
				float num39 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num37) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num38);
				if (num39 < 1000f)
				{
					flag = true;
					num28 = Main.npc[num36].position.X + (float)(Main.npc[num36].width / 2);
					num29 = Main.npc[num36].position.Y + (float)(Main.npc[num36].height / 2);
				}
			}
		}
		if (flag)
		{
			float num40 = num27;
			Vector2 vector = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num41 = num28 - vector.X;
			float num42 = num29 - vector.Y;
			float num43 = (float)Math.Sqrt((double)(num41 * num41 + num42 * num42));
			num43 = num40 / num43;
			num41 *= num43;
			num42 *= num43;
			int num44 = 8;
			Projectile.velocity.X = (Projectile.velocity.X * (float)(num44 - 1) + num41) / (float)num44;
			Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num44 - 1) + num42) / (float)num44;
		}
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}
}
