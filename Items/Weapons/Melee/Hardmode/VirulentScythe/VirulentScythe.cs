using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.VirulentScythe;

public class VirulentScythe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBoomerang(ModContent.ProjectileType<VirulentScytheProj>(), 50, 2.5f, 18, 15f, true, width: 34, height: 36);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 20);
	}
}
public class VirulentScytheProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<VirulentScythe>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<VirulentScythe>().DisplayName;

	public float seconds = 0.5f;
	public float returnSpeed = 2.5f;

	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(32);
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.ignoreWater = true;
		DrawOffsetX = -8;
		DrawOriginOffsetY = -8;
		Projectile.timeLeft = 2400;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 10;
	}

	public float timer;
	public float time;
	public bool willReturn;
	public bool runOnce;
	public float nextCloud;
	public override void AI()
	{
		Vector2 startPosition = Projectile.Center;
		Vector2 target = Main.player[Projectile.owner].Center;

		Projectile.spriteDirection = Projectile.direction;
		Projectile.rotation += 0.5f * Projectile.spriteDirection;

		timer++;
		if (willReturn)
		{
			Projectile.Center = Vector2.SmoothStep(startPosition, target, time);

			time += (returnSpeed / 100);
			time = MathHelper.Clamp(time, 0f, 1f);
			Projectile.velocity *= 0f;

			if (Main.player[Projectile.owner].getRect().Intersects(Projectile.getRect()))
			{
				Projectile.Kill();
			}
		}
		if (timer == (60f * seconds))
		{
			willReturn = true;
		}
		int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ContagionDust>(), Projectile.velocity.X * 0, Projectile.velocity.Y * 0, default, default, 1f);
		Main.dust[dust1].noGravity = true;
		Main.dust[dust1].alpha = 128;

		nextCloud++;
		int randomCloud = Main.rand.Next([ModContent.ProjectileType<VirulentCloud>(), ModContent.ProjectileType<VirulentCloudSmall>()]);

		if (nextCloud > 5 + Main.rand.Next(25))
		{
			Vector2 randomDir = new Vector2(Main.rand.NextFloat(-100f, 100f), Main.rand.NextFloat(-100f, 100f));
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(randomDir) * 1.5f, randomCloud, 0, 0, Main.player[Projectile.owner].whoAmI);
			nextCloud = 0;
		}
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = -oldVelocity.X;
		}
		if (Projectile.velocity.Y != oldVelocity.Y)
		{
			Projectile.velocity.Y = -oldVelocity.Y;
		}
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		return false;
	}
}
public class VirulentCloud : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(44, 42);
		Projectile.scale = 1.15f;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.ignoreWater = true;
		Projectile.timeLeft = 9000;
		Projectile.alpha = 150;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		Projectile.tileCollide = false;
	}
	public float Timer;
	public override void AI()
	{
		Projectile.spriteDirection = Projectile.direction;
		Projectile.rotation += (Projectile.velocity.X + Projectile.velocity.Y) * 0.1f * Projectile.spriteDirection;
		Projectile.velocity *= 0.980f;
		if (!Main.rand.NextBool(3))
		{
			Projectile.alpha++;
		}
		if (Projectile.alpha >= 255)
		{
			Projectile.Kill();
		}
		Timer++;
		if (Timer <= 40)
		{
			Projectile.scale *= 1.007f;
		}
		if (Timer >= 40)
		{
			Projectile.scale *= 0.993f;
		}
		if (Timer == 80)
		{
			Timer = 0;
			Projectile.scale = 1.15f;
		}

		foreach (NPC n in Main.npc)
		{
			if (n.active && n.life > 0 && n.lifeMax > 5 && !n.dontTakeDamage && !n.townNPC)
			{
				if (n.getRect().Intersects(Projectile.getRect()))
				{
					n.AddBuff(ModContent.BuffType<Buffs.Debuffs.Pathogen>(), 60 * 8);
				}
			}
		}
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
}
public class VirulentCloudSmall : VirulentCloud;
