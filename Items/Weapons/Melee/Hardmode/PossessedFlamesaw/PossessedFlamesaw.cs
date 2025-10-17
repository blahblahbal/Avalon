using Avalon.Common.Extensions;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.PossessedFlamesaw;

public class PossessedFlamesaw : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<PossessedFlamesawProj>(), 95, 9f, 20f, 15, 15, false, noMelee: true, width: 46, height: 16);
		Item.noUseGraphic = true;
		Item.rare = ModContent.RarityType<BlueRarity>();
		Item.value = Item.sellPrice(0, 40);
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source,position, velocity, type, damage, knockback,player.whoAmI, ai2: player.altFunctionUse);
		return false;
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}
public class PossessedFlamesawProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<PossessedFlamesaw>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<PossessedFlamesaw>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.light = 0.9f;
		Projectile.width = Projectile.height = 30;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 10;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.MaxUpdates = 1;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		int num34 = 10;
		int num35 = 10;
		Vector2 vector7 = new Vector2(Projectile.position.X + Projectile.width / 2 - num34 / 2, Projectile.position.Y + Projectile.height / 2 - num35 / 2);
		Projectile.velocity = Collision.TileCollision(vector7, Projectile.velocity, num34, num35, true, true, 1);
		Projectile.ai[0] = 1f;
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		return false;
	}
	public override void AI()
	{
		for (int i = 0; i < 2; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(32, 32), DustID.DesertTorch, Projectile.velocity * 0.3f);
			d.velocity += Main.rand.NextVector2Circular(2, 2);
			d.noGravity = true;
			Dust d2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(32, 32), DustID.InfernoFork, Projectile.velocity * 0.3f);
			d2.velocity += Main.rand.NextVector2Circular(2, 2);
			d2.noGravity = true;
		}
		if (Main.rand.NextBool(3))
		{
			Dust d3 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(32, 32), DustID.Smoke, Projectile.velocity * 0.1f);
			d3.velocity += Main.rand.NextVector2Circular(2, 2);
			d3.noGravity = Main.rand.NextBool();
		}
		if (Projectile.soundDelay == 0)
		{
			Projectile.soundDelay = 8;
			SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
		}
		if (Projectile.ai[0] == 0f)
		{
			if (Projectile.ai[2] == 2 && Main.tile[(int)(Projectile.position.X / 16f), (int)(Projectile.position.Y / 16f)].HasTile && Main.tile[(int)(Projectile.position.X / 16f), (int)(Projectile.position.Y / 16f)].TileType == 5)
			{
				WorldGen.KillTile((int)(Projectile.position.X / 16f), (int)(Projectile.position.Y / 16f), false, false, false);
			}
			Projectile.ai[1] += 1f;
			if (Projectile.velocity.X > 0f)
			{
				Projectile.spriteDirection = 1;
			}
			else if (Projectile.velocity.X < 0f)
			{
				Projectile.spriteDirection = -1;
			}
			var num89 = Projectile.position.X;
			var num90 = Projectile.position.Y;
			var flag2 = false;
			if (Projectile.ai[1] > 10f)
			{
				for (var num91 = 0; num91 < 200; num91++)
				{
					if (Main.npc[num91].active && !Main.npc[num91].dontTakeDamage && !Main.npc[num91].friendly && Main.npc[num91].lifeMax > 5)
					{
						var num92 = Main.npc[num91].position.X + Main.npc[num91].width / 2;
						var num93 = Main.npc[num91].position.Y + Main.npc[num91].height / 2;
						var num94 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num92) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num93);
						if (num94 < 800f && Collision.CanHit(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[num91].position, Main.npc[num91].width, Main.npc[num91].height))
						{
							num89 = num92;
							num90 = num93;
							flag2 = true;
						}
					}
				}
			}
			if (!flag2)
			{
				num89 = Projectile.position.X + Projectile.width / 2 + Projectile.velocity.X * 100f;
				num90 = Projectile.position.Y + Projectile.height / 2 + Projectile.velocity.Y * 100f;
				if (Projectile.ai[1] >= 30f)
				{
					Projectile.ai[0] = 1f;
					Projectile.ai[1] = 0f;
					Projectile.netUpdate = true;
				}
			}
			var num95 = 12f;
			var num96 = 0.25f;
			var vector3 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
			var num97 = num89 - vector3.X;
			var num98 = num90 - vector3.Y;
			var num99 = (float)Math.Sqrt(num97 * num97 + num98 * num98);
			num99 = num95 / num99;
			num97 *= num99;
			num98 *= num99;
			if (Projectile.velocity.X < num97)
			{
				Projectile.velocity.X = Projectile.velocity.X + num96;
				if (Projectile.velocity.X < 0f && num97 > 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X + num96 * 2f;
				}
			}
			else if (Projectile.velocity.X > num97)
			{
				Projectile.velocity.X = Projectile.velocity.X - num96;
				if (Projectile.velocity.X > 0f && num97 < 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X - num96 * 2f;
				}
			}
			if (Projectile.velocity.Y < num98)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + num96;
				if (Projectile.velocity.Y < 0f && num98 > 0f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y + num96 * 2f;
				}
			}
			else if (Projectile.velocity.Y > num98)
			{
				Projectile.velocity.Y = Projectile.velocity.Y - num96;
				if (Projectile.velocity.Y > 0f && num98 < 0f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - num96 * 2f;
				}
			}
			else if (Projectile.ai[1] >= 30f)
			{
				Projectile.ai[0] = 1f;
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
		}
		else
		{
			Projectile.tileCollide = false;
			var num100 = 16f;
			var num101 = 1.2f;
			var vector4 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
			var num102 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector4.X;
			var num103 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector4.Y;
			var num104 = (float)Math.Sqrt(num102 * num102 + num103 * num103);
			if (num104 > 3000f)
			{
				Projectile.Kill();
			}
			num104 = num100 / num104;
			num102 *= num104;
			num103 *= num104;
			if (Projectile.velocity.X < num102)
			{
				Projectile.velocity.X = Projectile.velocity.X + num101;
				if (Projectile.velocity.X < 0f && num102 > 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X + num101;
				}
			}
			else if (Projectile.velocity.X > num102)
			{
				Projectile.velocity.X = Projectile.velocity.X - num101;
				if (Projectile.velocity.X > 0f && num102 < 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X - num101;
				}
			}
			if (Projectile.velocity.Y < num103)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + num101;
				if (Projectile.velocity.Y < 0f && num103 > 0f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y + num101;
				}
			}
			else if (Projectile.velocity.Y > num103)
			{
				Projectile.velocity.Y = Projectile.velocity.Y - num101;
				if (Projectile.velocity.Y > 0f && num103 < 0f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - num101;
				}
			}
			if (Main.myPlayer == Projectile.owner)
			{
				var rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
				var value4 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
				if (rectangle.Intersects(value4))
				{
					Projectile.Kill();
				}
			}
		}
		Projectile.rotation += 0.4f * Projectile.direction;
	}
}