using Avalon;
using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Superhardmode.DroneSwarm;

public class DroneSwarm : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Type] = true;
	}
	public override Vector2? HoldoutOrigin() => new Vector2(10f, 10f);
	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<Drone>(), 25, 4f, 5, 12f, 4, 12, true, width: 36, height: 36);
		Item.scale = 1.2f;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(gold: 5);
		Item.UseSound = SoundID.Item8;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 255);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 pos = player.Center + new Vector2(30, 0).RotatedBy(player.AngleTo(Main.MouseWorld));
		for (int i = 0; i < 3; i++)
		{
			Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 0.167f, Main.rand.NextFloat(-2f, 2f), random: true);
			Projectile.NewProjectile(source, pos, vel, type, damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}
}
public class Drone : ModProjectile
{
	private int tileCollideCounter;
	public bool readyToHome = true;
	public float maxSpeed = 10f + Main.rand.NextFloat(5f);
	public float homeDistance = 400;
	public float homeStrength = 5f;
	public float homeDelay;
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		Projectile.timeLeft = 120;
		Projectile.ArmorPenetration = 15;
		DrawOriginOffsetY = -2;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		if (Projectile.timeLeft < 118)
		{
			return new Color(255, 255, 255, 200);
		}
		return new Color(0, 0, 0, 0);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		readyToHome = false;
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(tileCollideCounter);
		writer.Write(readyToHome);
		writer.Write(homeDelay);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		tileCollideCounter = reader.ReadInt32();
		readyToHome = reader.ReadBoolean();
		homeDelay = reader.ReadSingle();
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.type == ModContent.ProjectileType<Drone>())
		{
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			tileCollideCounter++;
			if (tileCollideCounter >= 4f)
			{
				Projectile.position += Projectile.velocity;
				Projectile.Kill();
			}
			else
			{
				if (Projectile.velocity.Y != oldVelocity.Y)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
				if (Projectile.velocity.X != oldVelocity.X)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}
			}
		}
		return false;
	}

	public override void AI()
	{
		Lighting.AddLight(Projectile.position, 219 / 510f, 205 / 510f, 79 / 510f);
		if (Projectile.timeLeft < 118)
		{
			Dust dust = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<Dusts.DroneDust>(), Vector2.Zero, default, default, 1.2f);
			dust.noGravity = true;
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		if (!readyToHome)
		{
			homeDelay++;
			if (homeDelay >= 15)
			{
				readyToHome = true;
				homeDelay = 0;
			}
			Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.Pi / 10);
		}

		Vector2 startPosition = Projectile.Center;
		int closest = Projectile.FindClosestNPC(homeDistance, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
		if (closest != -1 && readyToHome)
		{
			if (Collision.CanHit(Main.npc[closest], Projectile))
			{
				Vector2 target = Main.npc[closest].Center;
				float distance = Vector2.Distance(target, startPosition);
				Vector2 goTowards = Vector2.Normalize(target - startPosition) * ((homeDistance - distance) / (homeDistance / homeStrength));

				Projectile.velocity += goTowards;

				if (Projectile.velocity.Length() > maxSpeed)
				{
					Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxSpeed;
				}
			}
		}
	}
	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 6; i++)
		{
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.DroneDust>(), 0f, 0f, default, default, 1.2f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 1f;
		}
	}
}