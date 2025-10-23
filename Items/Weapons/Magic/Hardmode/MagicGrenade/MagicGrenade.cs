using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.MagicGrenade;

public class MagicGrenade : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<MagicGrenadeProj>(), 85, 8f, 40, 8f, 22, true, noUseGraphic: true, height: 16, width: 20);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10);
		Item.UseSound = SoundID.Item1;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.MagicDagger)
			.AddIngredient(ItemID.Grenade, 10)
			.AddIngredient(ItemID.SoulofFright, 20)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class MagicGrenadeProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<MagicGrenade>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<MagicGrenade>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 16;
		Projectile.alpha = 0;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.friendly = true;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 240;
		DrawOriginOffsetY = -2;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = oldVelocity.X * -0.4f;
		}
		if (Projectile.velocity.Y != oldVelocity.Y && (double)oldVelocity.Y > 0.7)
		{
			Projectile.velocity.Y = oldVelocity.Y * -0.4f;
		}
		return false;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Projectile.timeLeft = 3;
	}
	public override void AI()
	{
		for (var i = 0; i < 1 + (int)(MathHelper.Clamp(Projectile.velocity.Length(), 0, 1)); i++)
		{
			var dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.HallowedWeapons, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.2f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 0.6f;
		}

		if (Main.myPlayer == Projectile.owner && Projectile.timeLeft <= 3)
		{
			Projectile.tileCollide = false;
			Projectile.ai[1] = 0f;
			Projectile.alpha = 255;
			Projectile.knockBack = 8f;
			Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<MagicGrenadeBoom>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
		}

		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] > 5)
		{
			Projectile.ai[0] = 10f;
			if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
			{
				Projectile.velocity.X = Projectile.velocity.X * 0.97f;
				if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
				{
					Projectile.velocity.X = 0f;
					Projectile.netUpdate = true;
				}
			}
			Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
		}

		Projectile.rotation += Projectile.velocity.X * 0.1f;

	}
}
public class MagicGrenadeBoom : ModProjectile
{
	public override string Texture => ModContent.GetInstance<MagicGrenade>().Texture;
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 128;
		Projectile.alpha = 255;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.friendly = true;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 1;
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

		for (int num369 = 0; num369 < 20; num369++)
		{
			int num370 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
			Main.dust[num370].velocity *= 1.4f;
		}
		for (int num371 = 0; num371 < 10; num371++)
		{
			int num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.HallowedWeapons, 0f, 0f, 100, default, 2.5f);
			Main.dust[num372].noGravity = true;
			Main.dust[num372].velocity *= 5f;
			num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.HallowedWeapons, 0f, 0f, 100, default, 1.5f);
			Main.dust[num372].velocity *= 3f;
		}
		int num373 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Main.gore[num373].velocity.X++;
		Main.gore[num373].velocity.Y++;
		num373 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Main.gore[num373].velocity.X--;
		Main.gore[num373].velocity.Y++;
		num373 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Main.gore[num373].velocity.X++;
		Main.gore[num373].velocity.Y--;
		num373 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64), 1f);
		Main.gore[num373].velocity *= 0.4f;
		Main.gore[num373].velocity.X--;
		Main.gore[num373].velocity.Y--;
	}
}
