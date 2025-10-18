using Avalon;
using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Projectiles.Hostile.DesertBeak;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.EggCannon
{
	public class EggCannon : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToRangedWeapon(50, 20, ModContent.ProjectileType<ExplosiveEgg>(), AmmoID.None, 35, 8f, 16f, 35, 35, true);
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(silver: 54);
			Item.UseSound = SoundID.Item61;
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (ModContent.GetInstance<Common.AvalonClientConfig>().AdditionalScreenshakes)
			{
				UseStyles.gunStyle(player, 0.03f, 5f, 1.5f);
			}
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 0.04f, random: true);
			Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<ExplosiveEgg>(), damage, knockback, player.whoAmI, ai2: Main.rand.NextBool(3) ? 1 : 0);
			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-35, 0);
		}
	}
	public class ExplosiveEgg : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
		}

		public override void AI()
		{
			Projectile.frame = (int)Projectile.ai[2];
			Projectile.ai[0]++;
			Projectile.rotation += Projectile.velocity.X * 0.03f;
			if (Projectile.ai[0] > 20)
			{
				Projectile.velocity.Y += 0.3f;
				Projectile.velocity.X *= 0.99f;
			}
		}
		public override void OnKill(int timeLeft)
		{
			if (ModContent.GetInstance<AvalonClientConfig>().AdditionalScreenshakes)
			{
				PunchCameraModifier modifier = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2Circular(1, 1), 7, 15f, 15, 800f, Projectile.Name);
				Main.instance.CameraModifiers.Add(modifier);
			}

			if (Projectile.ai[2] == 1)
			{
				for (int i = 0; i < 8; i++)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, Main.rand.NextFloat(7, 9)).RotatedBy((MathHelper.TwoPi / 8) * i).RotatedByRandom(0.4f), ModContent.ProjectileType<ExplosiveEggShrapnel>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
				}
			}

			Projectile.maxPenetrate = -1;
			Projectile.penetrate = -1;

			int explosionArea = 75;
			Vector2 oldSize = Projectile.Size;
			Projectile.position = Projectile.Center;
			Projectile.Size += new Vector2(explosionArea);
			Projectile.Center = Projectile.position;

			Projectile.tileCollide = false;
			Projectile.velocity *= 0.01f;
			Projectile.Damage();
			Projectile.scale = 0.01f;

			Projectile.position = Projectile.Center;
			Projectile.Size = new Vector2(10);
			Projectile.Center = Projectile.position;

			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

			for (int i = 0; i < 20; i++)
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (Projectile.ai[2] == 0) ? DustID.Torch : DustID.DesertTorch, 0, 0, 0, default, 2f);
				Main.dust[d].velocity = Main.rand.NextVector2Circular(6, 6);
				Main.dust[d].noGravity = true;
				Main.dust[d].fadeIn = 2.3f;
			}
			for (int i = 0; i < 20; i++)
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 0, default, 1.4f);
				Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-3, 0).RotatedBy(Projectile.velocity.ToRotation());
				Main.dust[d].noGravity = !Main.rand.NextBool(10);
			}
			for (int i = 0; i < 7; i++)
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0, 0, 0, default, 1.4f);
				Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-5, 0).RotatedBy(Projectile.velocity.ToRotation());
				Main.dust[d].noGravity = Main.rand.NextBool(3);
			}
			for (int i = 0; i < 9; i++)
			{
				int g = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(10, 6) + new Vector2(-1, 0).RotatedBy(Projectile.velocity.ToRotation()), Main.rand.Next(61, 63), 0.8f);
				Main.gore[g].alpha = 128;
			}
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
		}
	}
	public class ExplosiveEggShrapnel : EggShrapnel
	{
		public override void SetDefaults()
		{
			Projectile.Size = new Vector2(8);
			Projectile.aiStyle = 1;
			AIType = ProjectileID.Bullet;
			Projectile.tileCollide = true;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.timeLeft = 25;
			Projectile.penetrate = 3;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.ignoreWater = true;
			Projectile.extraUpdates = 1;
		}
	}

}