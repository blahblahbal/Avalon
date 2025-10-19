using Avalon.Common.Extensions;
using Avalon.Items.Weapons.Ranged.Superhardmode.FleshBoiler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class Canister : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}
	public override void SetDefaults()
	{
		Item.DefaultToCanister(9, ModContent.ProjectileType<CanisterFire>());
		Item.value = Item.sellPrice(0, 0, 0, 2);
		Item.rare = ItemRarityID.Red;
	}
}
public class CanisterFire : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames;
	public override void SetDefaults()
	{
		Projectile.width = 6;
		Projectile.height = 6;
		Projectile.alpha = 255;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.penetrate = 5;
		Projectile.timeLeft = 50;
		Projectile.ignoreWater = false;
		Projectile.tileCollide = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.extraUpdates = 2;
		Projectile.usesIDStaticNPCImmunity = true;
		Projectile.idStaticNPCHitCooldown = 10;
	}
	public override void AI()
	{
		if (Projectile.ai[0] > 1f)
		{
			float num418 = 1f;
			if (Projectile.ai[0] == 8f)
			{
				num418 = 0.25f;
			}
			else if (Projectile.ai[0] == 9f)
			{
				num418 = 0.5f;
			}
			else if (Projectile.ai[0] == 10f)
			{
				num418 = 0.75f;
			}
			Projectile.ai[0] += 1f;
			int num419 = 6;
			if (num419 == 6 || Main.rand.NextBool(3))
			{
				for (int num420 = 0; num420 < 1; num420++)
				{
					int num421 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num419, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
					Dust dust98;
					Dust dust189;
					if (!Main.rand.NextBool(3) || (num419 == 75 && Main.rand.NextBool(3)))
					{
						Main.dust[num421].noGravity = true;
						dust98 = Main.dust[num421];
						dust189 = dust98;
						dust189.scale *= 3f;
						Main.dust[num421].velocity.X *= 2f;
						Main.dust[num421].velocity.Y *= 2f;
					}
					if (Projectile.type == 188)
					{
						dust98 = Main.dust[num421];
						dust189 = dust98;
						dust189.scale *= 1.25f;
					}
					else
					{
						dust98 = Main.dust[num421];
						dust189 = dust98;
						dust189.scale *= 1.5f;
					}
					Main.dust[num421].velocity.X *= 1.2f;
					Main.dust[num421].velocity.Y *= 1.2f;
					dust98 = Main.dust[num421];
					dust189 = dust98;
					dust189.scale *= num418;
				}
			}
		}
		else
		{
			Projectile.ai[0] += 1f;
		}
		Projectile.rotation += 0.3f * (float)Projectile.direction;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<CanisterFireLinger>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
		return true;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 240);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.OnFire, 240, false);
	}
	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int size = 20;
		hitbox.X -= size;
		hitbox.Y -= size;
		hitbox.Width += size * 2;
		hitbox.Height += size * 2;
	}
}
public class CanisterFireLinger : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames;
	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.alpha = 255;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 250 + Main.rand.Next(50, 100);
		Projectile.ignoreWater = false;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.usesIDStaticNPCImmunity = true;
		Projectile.idStaticNPCHitCooldown = 10;
	}
	public override void AI()
	{
		for (int i = 0; i < 1; i++)
		{
			int num421 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 3f);
			Main.dust[num421].velocity.X *= 3f;
			Main.dust[num421].velocity.Y *= 3.5f;
			Main.dust[num421].noGravity = true;
		}
	}
	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int size = 15;
		hitbox.X -= size;
		hitbox.Y -= size;
		hitbox.Width += size * 2;
		hitbox.Height += size * 2;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 240);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.OnFire, 240, false);
	}
}

