using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode.Gastropod;

public class GastropodStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
		ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

		ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
	}
	public override void SetDefaults()
	{
		Item.DefaultToMinionWeapon(ModContent.ProjectileType<GastrominiSummon0>(), ModContent.BuffType<Gastropod>(), 40, 4.5f, 30);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 1);
		Item.UseSound = SoundID.Item44;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.CrystalShard, 30)
			.AddIngredient(ItemID.Gel, 100)
			.AddIngredient(ItemID.SoulofLight, 20)
			.AddIngredient(ItemID.PixieDust, 20)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		position = Main.MouseWorld;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		player.AddBuff(Item.buffType, 2);

		switch (Main.rand.Next(4))
		{
			case 0:
				type = Item.shoot;
				break;
			case 1:
				type = ModContent.ProjectileType<GastrominiSummon1>();
				break;
			case 2:
				type = ModContent.ProjectileType<GastrominiSummon2>();
				break;
			case 3:
				type = ModContent.ProjectileType<GastrominiSummon3>();
				break;
		}

		// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
		var projectile = Projectile.NewProjectileDirect(source, position, Vector2.Zero, type, damage, knockback, Main.myPlayer);
		projectile.originalDamage = Item.damage;
		return false;
	}
}
public class Gastropod : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[Type] = true;
		Main.buffNoSave[Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<GastrominiSummon2>()] > 0 ||
			player.ownedProjectileCounts[ModContent.ProjectileType<GastrominiSummon3>()] > 0 ||
			player.ownedProjectileCounts[ModContent.ProjectileType<GastrominiSummon1>()] > 0 ||
			player.ownedProjectileCounts[ModContent.ProjectileType<GastrominiSummon0>()] > 0)
		{
			player.GetModPlayer<AvalonPlayer>().GastroMinion = true;
		}
		if (!player.GetModPlayer<AvalonPlayer>().GastroMinion)
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
		else
		{
			player.buffTime[buffIndex] = 18000;
		}
	}
}
public abstract class GastrominiSummon : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 5;
		Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
																	  //ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.netImportant = true;
		Projectile.width = 22;
		Projectile.height = 36;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 2;
		Projectile.minion = true;
		Projectile.minionSlots = 1f;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Summon;
		Projectile.ignoreWater = true;
		Projectile.friendly = true;
		Projectile.aiStyle = -1;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
	public bool isTooFarFromPlayer { get => Convert.ToBoolean(Projectile.ai[0]); set => Projectile.ai[0] = value.ToInt(); }
	public float shootDelay { get => Projectile.ai[1]; set => Projectile.ai[1] = value; }
	public override void AI()
	{
		Player player = Main.player[Projectile.owner];

		AvalonPlayer.MinionRemoveCheck(player, ModContent.BuffType<Gastropod>(), Projectile);

		AvoidOwnedMinions();

		GetTarget(out bool target, out Vector2 targetPos, out float targetDist, player);

		CheckPlayerDistance(target, player);

		if (target && !isTooFarFromPlayer)
		{
			SurroundTarget(targetPos);
		}
		else
		{
			FollowPlayer(player);
		}

		Animate(target, player);

		FireLaser(target, targetPos);
	}
	private void AvoidOwnedMinions()
	{
		float idleAccel = 0.05f;
		foreach (var otherProj in Main.ActiveProjectiles)
		{
			if (otherProj.whoAmI != Projectile.whoAmI && otherProj.owner == Projectile.owner && Math.Abs(Projectile.position.X - otherProj.position.X) + Math.Abs(Projectile.position.Y - otherProj.position.Y) < Projectile.width)
			{
				if (Projectile.position.X < otherProj.position.X)
				{
					Projectile.velocity.X = Projectile.velocity.X - idleAccel;
				}
				else
				{
					Projectile.velocity.X = Projectile.velocity.X + idleAccel;
				}
				if (Projectile.position.Y < otherProj.position.Y)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - idleAccel;
				}
				else
				{
					Projectile.velocity.Y = Projectile.velocity.Y + idleAccel;
				}
			}
		}
	}
	private void GetTarget(out bool target, out Vector2 targetPos, out float targetDist, Player player)
	{
		targetPos = Projectile.position;
		targetDist = 400f;
		target = false;

		if (!isTooFarFromPlayer)
		{
			Projectile.tileCollide = true;
		}
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
			{
				targetPos = npc.Center;
				targetDist = Vector2.Distance(Projectile.Center, targetPos);
				target = true;
			}
		}
		if (!player.HasMinionAttackTargetNPC || !target)
		{
			foreach (var npc in Main.ActiveNPCs)
			{
				if (npc.CanBeChasedBy(this, false))
				{
					float distance = Vector2.Distance(npc.Center, Projectile.Center);
					if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
					{
						targetDist = distance;
						targetPos = npc.Center;
						target = true;
					}
				}
			}
		}
	}
	private void CheckPlayerDistance(bool target, Player player)
	{
		int maxDistToPlayer = 500;
		if (target)
		{
			maxDistToPlayer = 1000;
		}
		if (Vector2.Distance(player.Center, Projectile.Center) > maxDistToPlayer)
		{
			isTooFarFromPlayer = true;
			Projectile.tileCollide = false;
			Projectile.netUpdate = true;
		}
	}
	private void SurroundTarget(Vector2 targetPos)
	{
		Vector2 targetDir = Projectile.Center.DirectionTo(targetPos);
		float toTargetNPCDist = (targetPos - Projectile.Center).Length();
		if (toTargetNPCDist > 200f)
		{
			float dirMult = 6f;
			targetDir *= dirMult;
			Projectile.velocity = (Projectile.velocity * 40f + targetDir) / 41f;
		}
		else
		{
			float dirMult = 4f;
			targetDir *= -dirMult;
			Projectile.velocity = (Projectile.velocity * 40f + targetDir) / 41f;
		}
	}
	private void FollowPlayer(Player player)
	{
		var distMult = 6f;
		if (isTooFarFromPlayer)
		{
			distMult = 15f;
		}
		Vector2 hoverPos = player.Center - Projectile.Center + new Vector2(0f, -60f);
		var hoverPosDist = hoverPos.Length();
		if (hoverPosDist > 200f && distMult < 8f)
		{
			distMult = 8f;
		}
		if (hoverPosDist < 150f && isTooFarFromPlayer && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
		{
			isTooFarFromPlayer = false;
			Projectile.netUpdate = true;
		}
		if (hoverPosDist > 2000f)
		{
			Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
			Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
			Projectile.netUpdate = true;
		}
		if (hoverPosDist > 70f)
		{
			hoverPos.Normalize();
			hoverPos *= distMult;
			Projectile.velocity = (Projectile.velocity * 40f + hoverPos) / 41f;
		}
		else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
		{
			Projectile.velocity.X = -0.15f;
			Projectile.velocity.Y = -0.05f;
		}
	}
	private void Animate(bool target, Player player)
	{
		if (Projectile.localAI[0] == 0)
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter <= 20)
			{
				Projectile.frame = 0;
			}
			else if (Projectile.frameCounter <= 40)
			{
				Projectile.frame = 1;
			}
			else
			{
				Projectile.frame = 0;
				Projectile.frameCounter = 0;
			}
			Projectile.spriteDirection = Math.Abs(Projectile.velocity.X) > 2.25f || !target && Projectile.Center.Distance(player.Center) > 300f ? -Math.Sign(Projectile.velocity.X) : Projectile.spriteDirection;
		}
		else if (Projectile.localAI[0] > 0)
		{
			Projectile.frameCounter = 0;
			Projectile.localAI[0]++;
			if (Projectile.localAI[0] <= 4)
			{
				Projectile.frame = 2;
			}
			else if (Projectile.localAI[0] <= 8)
			{
				Projectile.frame = 3;
			}
			else if (Projectile.localAI[0] <= 12)
			{
				Projectile.frame = 4;
			}
			else
			{
				Projectile.frame = 0;
				Projectile.localAI[0] = 0;
			}
		}
	}
	private void FireLaser(bool target, Vector2 targetPos)
	{

		if (shootDelay > 0f)
		{
			shootDelay += Main.rand.Next(1, 4);
		}
		if (shootDelay > 90f)
		{
			shootDelay = 0f;
			Projectile.netUpdate = true;
		}
		if (!isTooFarFromPlayer)
		{
			float dirMult = 8f;
			if (target && shootDelay == 0f)
			{
				shootDelay += 1f;
				Projectile.localAI[0] = 1;
				Projectile.frameCounter = 0;
				if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, targetPos, 0, 0))
				{
					Projectile.spriteDirection = -(int)((targetPos.X - Projectile.Center.X) / Math.Abs(targetPos.X - Projectile.Center.X));
					Vector2 targetDir = Projectile.Center.DirectionTo(targetPos);
					targetDir *= dirMult;
					int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, targetDir.X, targetDir.Y, ProjectileID.MiniRetinaLaser, (int)(Projectile.damage * 1.5f), 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[p].timeLeft = 300;
					Projectile.netUpdate = true;
				}
			}
		}
	}
}


public class GastrominiSummon0 : GastrominiSummon { }
public class GastrominiSummon1 : GastrominiSummon { }
public class GastrominiSummon2 : GastrominiSummon { }
public class GastrominiSummon3 : GastrominiSummon { }

