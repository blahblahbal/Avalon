using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah.Staff;

public class BlahStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<BlahMeteor>(), 278, 16f, 19, 20f, 15, 15);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(2);
		Item.UseSound = SoundID.Item88;
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(1)
	//        .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 45)
	//        .AddIngredient(ModContent.ItemType<Placeable.Bar.SuperhardmodeBar>(), 40)
	//        .AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 45)
	//        .AddIngredient(ItemID.LunarFlareBook)
	//        .AddIngredient(ModContent.ItemType<PyroscoricFlareStaff>())
	//        .AddIngredient(ModContent.ItemType<OpalStaff>())
	//        .AddIngredient(ModContent.ItemType<OnyxStaff>())
	//        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
	//        .Register();
	//}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		return false;
	}
	public override bool? UseItem(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			for (int num9 = 0; num9 < 1; num9++)
			{
				Vector2 vector = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector.X = (vector.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
				vector.Y -= 100 * num9;
				float num311 = Main.mouseX + Main.screenPosition.X - vector.X;
				float num312 = Main.mouseY + Main.screenPosition.Y - vector.Y;
				float ai2 = num312 + vector.Y;
				if (num312 < 0f)
				{
					num312 *= -1f;
				}
				if (num312 < 20f)
				{
					num312 = 20f;
				}
				float num313 = (float)Math.Sqrt(num311 * num311 + num312 * num312);
				num313 = Item.shootSpeed / num313;
				num311 *= num313;
				num312 *= num313;
				Vector2 vector3 = new Vector2(num311, num312) / 2f;
				int p = Projectile.NewProjectile(Terraria.Entity.GetSource_None(), vector.X, vector.Y, vector3.X, vector3.Y, ModContent.ProjectileType<BlahMeteor>(), (int)player.GetDamage(DamageClass.Magic).ApplyTo(Item.damage), Item.knockBack, player.whoAmI, 0f, ai2);
				Main.projectile[p].owner = player.whoAmI;
			}
		}
		return true;
	}
}
public class BlahMeteor : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 40;
		Projectile.height = 40;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.penetrate = 9;
		Projectile.friendly = true;
		Projectile.scale = 0.5f;
		Projectile.damage = 100;
	}
	public override bool PreAI()
	{
		Lighting.AddLight(Projectile.position, 249 / 255f, 201 / 255f, 77 / 255f);
		return true;
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
		Projectile.position.X = Projectile.position.X + Projectile.width / 2;
		Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
		Projectile.width = 22;
		Projectile.height = 22;
		Projectile.position.X = Projectile.position.X - Projectile.width / 2;
		Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
		for (int num341 = 0; num341 < 30; num341++)
		{
			int num342 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
			Main.dust[num342].velocity *= 1.4f;
		}
		for (int num343 = 0; num343 < 20; num343++)
		{
			int num344 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
			Main.dust[num344].noGravity = true;
			Main.dust[num344].velocity *= 7f;
			num344 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
			Main.dust[num344].velocity *= 3f;
		}
		for (int num345 = 0; num345 < 2; num345++)
		{
			float scaleFactor8 = 0.4f;
			if (num345 == 1)
			{
				scaleFactor8 = 0.8f;
			}
			int num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
			Main.gore[num346].velocity *= scaleFactor8;
			Main.gore[num346].velocity.X++;
			Main.gore[num346].velocity.Y++;
			num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
			Main.gore[num346].velocity *= scaleFactor8;
			Main.gore[num346].velocity.X--;
			Main.gore[num346].velocity.Y++;
			num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
			Main.gore[num346].velocity *= scaleFactor8;
			Main.gore[num346].velocity.X++;
			Main.gore[num346].velocity.Y--;
			num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
			Main.gore[num346].velocity *= scaleFactor8;
			Main.gore[num346].velocity.X--;
			Main.gore[num346].velocity.Y--;
		}
	}
	public override void AI()
	{
		if (Projectile.ai[1] != -1f && Projectile.position.Y > Projectile.ai[1])
		{
			Projectile.tileCollide = true;
		}
		if (Projectile.position.HasNaNs())
		{
			Projectile.Kill();
			return;
		}
		bool num220 = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));
		Dust dust2 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)(Projectile.width * Projectile.scale), (int)(Projectile.height * Projectile.scale), DustID.Torch)];

		dust2.position = new Vector2(Projectile.position.X + (Projectile.width - 0.5f * Projectile.scale), Projectile.position.Y + (Projectile.height - 0.5f * Projectile.scale));
		dust2.velocity = Vector2.Zero;
		dust2.scale = 1.5f;
		dust2.noGravity = true;
		if (num220)
		{
			dust2.noLight = true;
		}
		//left side
		bool num221 = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));
		dust2 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)(Projectile.width * Projectile.scale), (int)(Projectile.height * Projectile.scale), DustID.Torch)];

		dust2.position = new Vector2(Projectile.position.X + (Projectile.width * Projectile.scale), Projectile.position.Y + (Projectile.height - 0.5f * Projectile.scale));
		dust2.velocity = Vector2.Zero;
		dust2.scale = 1.5f;
		dust2.noGravity = true;
		if (num221)
		{
			dust2.noLight = true;
		}
		//right side
		bool num222 = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));
		dust2 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)(Projectile.width * Projectile.scale), (int)(Projectile.height * Projectile.scale), DustID.Torch)];

		dust2.position = new Vector2(Projectile.position.X + (Projectile.width + 0.5f * Projectile.scale), Projectile.position.Y + (Projectile.height - 0.5f * Projectile.scale));
		dust2.velocity = Vector2.Zero;
		dust2.scale = 1.5f;
		dust2.noGravity = true;
		if (num221)
		{
			dust2.noLight = true;
		}
		Projectile.ai[0]++;
		if (Projectile.ai[0] == 20)
		{
			float speedX = Projectile.velocity.X + Main.rand.Next(20, 51) * 0.1f;
			float speedY = Projectile.velocity.Y + Main.rand.Next(20, 51) * 0.1f;
			int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(speedX, speedY), ModContent.ProjectileType<BlahStar>(), Projectile.damage, Projectile.knockBack);
			Main.projectile[p].friendly = true;
			Main.projectile[p].hostile = false;
			Main.projectile[p].owner = Projectile.owner;
			Main.projectile[p].DamageType = DamageClass.Magic;
		}
		if (Projectile.ai[0] == 40)
		{
			float speedX = Projectile.velocity.X + Main.rand.Next(-51, -20) * 0.1f;
			float speedY = Projectile.velocity.Y + Main.rand.Next(20, 51) * 0.1f;
			int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(speedX, speedY), ModContent.ProjectileType<BlahStar>(), Projectile.damage, Projectile.knockBack);
			Main.projectile[p].friendly = true;
			Main.projectile[p].hostile = false;
			Main.projectile[p].owner = Projectile.owner;
			Main.projectile[p].DamageType = DamageClass.Magic;
			Projectile.ai[0] = 0;
		}
	}
}
public class BlahStar : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.aiStyle = -1;
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.tileCollide = false;
		Projectile.penetrate = 5;
		Projectile.hostile = false;
	}
	public override bool PreAI()
	{
		Lighting.AddLight(Projectile.position, 249 / 255f, 201 / 255f, 77 / 255f);
		return true;
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		for (int i = 0; i < 2; i++)
		{
			float speedX = Projectile.velocity.X + Main.rand.Next(-51, 51) * 0.2f;
			float speedY = Projectile.velocity.Y + Main.rand.Next(-51, 51) * 0.2f;
			int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(speedX, speedY), ModContent.ProjectileType<BlahFire>(), Projectile.damage, Projectile.knockBack);
			Main.projectile[proj].hostile = false;
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].owner = Projectile.owner;
			Main.projectile[proj].timeLeft = 240;
		}
		Projectile.active = false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.ai[2] == 0)
		{
			Projectile.Kill();
			return true;
		}

		return false;
	}
	public override void AI()
	{
		if (Projectile.ai[2] == 0)
		{
			Projectile.tileCollide = true;
		}
		if (Projectile.soundDelay == 0)
		{
			Projectile.soundDelay = 20 + Main.rand.Next(40);
			SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
		}
		if (Projectile.localAI[0] == 0f)
			Projectile.localAI[0] = 1f;

		Projectile.alpha += (int)(25f * Projectile.localAI[0]);
		if (Projectile.alpha > 200)
		{
			Projectile.alpha = 200;
			Projectile.localAI[0] = -1f;
		}

		if (Projectile.alpha < 0)
		{
			Projectile.alpha = 0;
			Projectile.localAI[0] = 1f;
		}
		Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f * Projectile.direction;

		{
			Vector2 vector14 = new Vector2(Main.screenWidth, Main.screenHeight);
			if (Projectile.Hitbox.Intersects(Utils.CenteredRectangle(Main.screenPosition + vector14 / 2f, vector14 + new Vector2(400f))) && Main.rand.NextBool(20))
				Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity * 0.2f, Main.rand.Next(16, 18));

			if (Main.rand.NextBool(4))
			{
				Dust dust6 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 86, 0f, 0f, 127);
				Dust dust2 = dust6;
				dust2.velocity *= 0.7f;
				dust6.noGravity = true;
				dust2 = dust6;
				dust2.velocity += Projectile.velocity * 0.3f;
				if (Main.rand.NextBool(2))
				{
					dust2 = dust6;
					dust2.position -= Projectile.velocity * 4f;
				}
			}
		}
		if (Projectile.ai[1] == 1f)
		{
			Projectile.light = 0.9f;
			if (Main.rand.NextBool(10))
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default, 1.2f);

			if (Main.rand.NextBool(20))
				Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), Main.rand.Next(16, 18));
		}


		Projectile.hostile = false;
		Projectile.friendly = true;
		if (Main.rand.NextBool(100))
		{
			for (int i = 0; i < 3; i++)
			{
				int d = Dust.NewDust(Projectile.position, 10, 10, DustID.Torch);
				Main.dust[d].noGravity = true;
			}
		}
	}
}
public class BlahFire : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.aiStyle = -1;
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.tileCollide = true;
		Projectile.penetrate = -1;
		Projectile.hostile = false;
		Projectile.timeLeft = 180;
		DrawOriginOffsetY += 6;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1f, 1f, 1f, 0f) * Projectile.Opacity;
	}
	public override bool PreAI()
	{
		Lighting.AddLight(Projectile.position, 249 / 255f, 201 / 255f, 77 / 255f);
		return true;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity.X = oldVelocity.X * -0.1f;
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = oldVelocity.X * -0.5f;
		}
		if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 1f)
		{
			Projectile.velocity.Y = oldVelocity.Y * -0.5f;
		}
		return false;
	}
	public override void AI()
	{
		if (Projectile.timeLeft % 6 == 0)
		{
			DrawOriginOffsetY = Main.rand.Next(4, 6);
			DrawOriginOffsetX = Main.rand.Next(-4, 4);
			Projectile.spriteDirection = Main.rand.NextBool() ? 1 : -1;
			Projectile.scale = Main.rand.NextFloat(0.9f, 1.2f);
		}
		if(Projectile.timeLeft < 10)
		{
			Projectile.alpha += 20;
		}
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.ai[0]++;
		if (Projectile.ai[0] > 5f)
		{
			Projectile.ai[0] = 5f;
			if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
			{
				Projectile.velocity.X = Projectile.velocity.X * 0.97f;
				if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
				{
					Projectile.velocity.X = 0f;
					Projectile.netUpdate = true;
				}
			}
			Projectile.velocity.Y += 0.2f;
		}
		if (Projectile.ai[1] == 0f)
		{
			Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(SoundID.Item13, Projectile.position);
		}
		int num218 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default, 1f);
		Main.dust[num218].position.X -= 2f;
		Main.dust[num218].position.Y += 2f;
		Main.dust[num218].scale += Main.rand.Next(50) * 0.01f;
		Main.dust[num218].noGravity = true;
		Main.dust[num218].velocity.Y -= 2f;
		if (Main.rand.NextBool(5))
		{
			int num219 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default, 1f);
			Main.dust[num219].position.X -= 2f;
			Main.dust[num219].position.Y += 2f;
			Main.dust[num219].scale += 0.3f + Main.rand.Next(50) * 0.01f;
			Main.dust[num219].noGravity = true;
			Main.dust[num219].velocity *= 0.1f;
		}
		if (Projectile.velocity.Y < 0.25 && Projectile.velocity.Y > 0.15)
		{
			Projectile.velocity.X = Projectile.velocity.X * 0.8f;
		}
		Projectile.rotation = -Projectile.velocity.X * 0.1f;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}
}
