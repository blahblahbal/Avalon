using Avalon;
using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Blah.Staff;
using Avalon.PlayerDrawLayers;
using Avalon.Projectiles.Magic;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah.EnergyBlade;

public class BlahsEnergyBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemGlowmask.AddGlow(this, new Color(250, 250, 250, 250));
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<BlahEnergySlash>(), 250, 20f, 14f, 14, 14, shootsEveryUse: true, noMelee: true, crit: 10, width: 60, height: 70);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(0, 25);
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 255);
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		player.itemLocation = Vector2.Lerp(player.itemLocation, player.MountedCenter, 0.5f);
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(1)
	//        .AddIngredient(ModContent.ItemType<Phantoplasm>(), 45)
	//        .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 40)
	//        .AddIngredient(ModContent.ItemType<SoulofTorture>(), 45)
	//        .AddIngredient(ModContent.ItemType<ElementalExcalibur>())
	//        .AddIngredient(ModContent.ItemType<BerserkerBlade>())
	//        .AddIngredient(ModContent.ItemType<PumpkingsSword>())
	//        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
	//        .Register();
	//}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem) * 1.4f;
		Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax * 1.2f, adjustedItemScale5 * 1.4f);
		NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
		for (int i = 0; i < 4; i++)
		{
			Vector2 vel = AvalonUtils.GetShootSpread(velocity, position, Type, 0.143f, Main.rand.NextFloat(-2f, 2f), random: true);
			Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<BlahBeam>(), damage, knockback, player.whoAmI);
		}
		return false;
	}
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		player.VampireHeal(hit.Damage, target.position);
	}
}
public class BlahEnergySlash : EnergySlashTemplate
{
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TheHorsemansBlade}";
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.penetrate = -1;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Color[] Colors = { new Color(255, 66, 0), new Color(249, 201, 77), new Color(247, 255, 177), new Color(216, 131, 0) };
		Color Color1 = ClassExtensions.CycleThroughColors(Colors, 60, 0) * 0.5f;
		Color1.A = 64;

		DrawSlash(Color1, Color1 * 0.8f, Color1 * 0.6f, Color.Lerp(Color1, Color.White, 0.5f), 0, 1f, 0.78f, -MathHelper.Pi / 24, 0, true);
		Color1 = ClassExtensions.CycleThroughColors(Colors, 60, 40) * 0.5f;
		DrawSlash(Color1, Color1 * 0.3f, Color1 * 0.6f, Color.Lerp(Color1, new Color(255, 66, 0), 0.4f), 0, 0.8f, 0, -MathHelper.Pi / 24, 0, true);
		Color1 = ClassExtensions.CycleThroughColors(Colors, 60, 80) * 0.5f;
		DrawSlash(Color1, Color1 * 0.8f, Color1 * 0.6f, Color.Lerp(Color1, Color.White, 0.3f), 0, 0.8f, 0.78f, -MathHelper.Pi / 6, 0, true);

		return false;
	}
	public override void AI()
	{
		float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
		Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
		Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
		int[] Dusts = { DustID.Torch, DustID.HallowedTorch, DustID.SolarFlare };
		if (Main.rand.NextFloat() * 0.5f < Projectile.Opacity)
		{
			Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), Dusts[Main.rand.Next(3)], vector3 * 3f, 0, default, 1f);
			dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.5f;
			dust2.noGravity = true;
		}
	}
}
public class BlahBeam : ModProjectile
{
	private int tileCollideCounter;
	public bool readyToHome = true;
	public float maxSpeed = 10f + Main.rand.NextFloat(10f);
	public float homeDistance = 600;
	public float homeStrength = 5f;
	public float homeDelay;
	public byte timer;
	public override void SetDefaults()
	{
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.aiStyle = 27;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.penetrate = 5;
		Projectile.alpha = 255;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 20;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		if (Projectile.localAI[1] >= 15f)
		{
			return new Color(255, 255, 255, Projectile.alpha);
		}
		if (Projectile.localAI[1] < 5f)
		{
			return Color.Transparent;
		}
		int num7 = (int)((Projectile.localAI[1] - 5f) / 10f * 255f);
		return new Color(num7, num7, num7, num7);
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(tileCollideCounter);
		writer.Write(readyToHome);
		writer.Write(homeDelay);
		writer.Write(timer);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		tileCollideCounter = reader.ReadInt32();
		readyToHome = reader.ReadBoolean();
		homeDelay = reader.ReadSingle();
		timer = reader.ReadByte();
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		timer++;
		int randomNum = Main.rand.Next(7);
		if (randomNum == 0) target.AddBuff(20, 300);
		else if (randomNum == 1) target.AddBuff(24, 200);
		else if (randomNum == 2) target.AddBuff(31, 120);
		else if (randomNum == 3) target.AddBuff(39, 300);
		else if (randomNum == 4) target.AddBuff(44, 300);
		else if (randomNum == 5) target.AddBuff(70, 240);
		else if (randomNum == 6) target.AddBuff(69, 300);

		//SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
		if (timer == 1)
		{
			Vector2 StarSpawn = Projectile.position - new Vector2(Main.rand.Next(60, 180) * Projectile.Owner().direction, Main.rand.Next(-75, 75)).RotatedByRandom(MathHelper.TwoPi * 4);
			Projectile P = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), StarSpawn, StarSpawn.DirectionTo(target.Center) * 10f, ModContent.ProjectileType<BlahStar>(), (int)(Projectile.damage * 0.6f), Projectile.knockBack * 0.1f, Projectile.owner, 0, Main.rand.Next(-20, -10), 1);
			P.DamageType = DamageClass.Melee;
			P.tileCollide = false;
			P.timeLeft = 420;
			timer = 0;
		}
		readyToHome = false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.type == ModContent.ProjectileType<BlahBeam>())
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
		Lighting.AddLight(Projectile.position, 255 / 255f, 175 / 255f, 0);

		if (!readyToHome)
		{
			homeDelay++;
			if (homeDelay >= 20)
			{
				readyToHome = true;
				homeDelay = 0;
			}
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
}
