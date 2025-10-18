using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode.PrimeArms;
public class PriminiCannon : ModProjectile
{
	int scaleSize1;
	int scaleSize2;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		Main.projPet[Projectile.type] = true;
		Main.projFrames[Projectile.type] = 3;
	}
	public override void SetDefaults()
	{
		Projectile.netImportant = true;
		Projectile.width = 42;
		Projectile.height = 42;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.timeLeft *= 5;
		Projectile.minion = true;
		Projectile.minionSlots = 0f;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.friendly = true;
		scaleSize1 = (int)(Projectile.width * 1.35f);
		scaleSize2 = (int)(Projectile.height * 1.7f);
	}
	//public bool isTooFarFromPlayer { get => Convert.ToBoolean(Projectile.ai[0]); set => Projectile.ai[0] = value.ToInt(); }
	public ref float ShootDelay => ref Projectile.ai[1];
	public override void AI()
	{
		Player owner = Main.player[Projectile.owner];
		if (owner.dead)
		{
			owner.GetModPlayer<AvalonPlayer>().PrimeMinion = false;
		}
		if (owner.GetModPlayer<AvalonPlayer>().PrimeMinion)
		{
			Projectile.timeLeft = 2;
		}
		PrimeArmsCounter.ModifyPrimeMinionStats(Projectile, owner);

		if (Projectile.frame == 1)
		{
			Projectile.Resize(scaleSize1, scaleSize1);
		}
		if (Projectile.frame == 2)
		{
			Projectile.Resize(scaleSize2, scaleSize2);
		}
		SurroundOwner(owner);

		float maxDist = 800f;
		AvalonGlobalProjectile.GetMinionTarget(Projectile, owner, out bool hasTarget, out NPC target, out float targetDist, maxDist);
		if (!hasTarget || targetDist > maxDist || owner.Center.DistanceSQ(target.Center) > maxDist * maxDist)
		{
			if (Projectile.Center.Distance(owner.Center) < 400f)
			{
				//Projectile.rotation = Projectile.Center.AngleTo(owner.Center);
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(owner.Center), 0.1f);
			}
			else
			{
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(owner.Center), 0.1f);
			}
			if (ShootDelay > 0)
			{
				ShootDelay -= 0.5f;
			}
			//Projectile.rotation = MathF.PI - MathHelper.PiOver4;
			//Projectile.rotation = MathF.PI;
			return;
		}
		AvalonGlobalProjectile.AvoidOwnedMinions(Projectile);
		//SurroundTarget(target, targetDist);
		Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(target.Center) + MathHelper.Pi, 0.1f);

		ShootDelay++;
		if (ShootDelay > 240f/* && Collision.CanHit(Projectile, target)*/)
		{
			var bombProj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.Grenade, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			bombProj.velocity = Projectile.Center.SafeDirectionTo(target.Center) * 8f;
			bombProj.timeLeft = 100;
			bombProj.friendly = true;
			bombProj.hostile = false;
			ShootDelay = 0f;
		}
	}
	//private void SurroundTarget(NPC target, float targetDist) // todo: move this to avalonglobalprojectile? (gastromini version has unique behaviour, though)
	//{
	//	Vector2 targetDir = Projectile.Center.SafeDirectionTo(target.Center);
	//	if (targetDist > 200f)
	//	{
	//		float dirMult = 8f;
	//		targetDir *= dirMult;
	//		Projectile.velocity = (Projectile.velocity * 40f + targetDir) / 41f;
	//	}
	//	else
	//	{
	//		float dirMult = 4f;
	//		targetDir *= -dirMult;
	//		Projectile.velocity = (Projectile.velocity * 40f + targetDir) / 41f;
	//	}
	//}
	private void SurroundOwner(Player owner)
	{
		if (Projectile.BottomLeft.Y > owner.Center.Y - Main.rand.Next(28, 33) - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.Y > 0f)
			{
				Projectile.velocity.Y *= 0.96f;
			}
			Projectile.velocity.Y -= 0.2f;
			if (Projectile.velocity.Y > 6f)
			{
				Projectile.velocity.Y = 6f;
			}
		}
		else if (Projectile.BottomLeft.Y < owner.Center.Y - Main.rand.Next(28, 33) - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.Y < 0f)
			{
				Projectile.velocity.Y *= 0.96f;
			}
			Projectile.velocity.Y += 0.2f;
			if (Projectile.velocity.Y < -6f)
			{
				Projectile.velocity.Y = -6f;
			}
		}
		if (Projectile.BottomLeft.X > owner.Center.X + Main.rand.Next(38, 43) + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.X > 0f)
			{
				Projectile.velocity.X *= 0.94f;
			}
			Projectile.velocity.X -= 0.2f;
			if (Projectile.velocity.X > 8f)
			{
				Projectile.velocity.X = 8f;
			}
		}
		else if (Projectile.BottomLeft.X < owner.Center.X + Main.rand.Next(38, 43) + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.X < 0f)
			{
				Projectile.velocity.X *= 0.94f;
			}
			Projectile.velocity.X += 0.2f;
			if (Projectile.velocity.X < -8f)
			{
				Projectile.velocity.X = -8f;
			}
		}
		//Projectile.BottomLeft = owner.Center + new Vector2(55f, -70f);
		//Projectile.velocity = Vector2.Zero;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
	public override bool MinionContactDamage()
	{
		return false;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type].Value;
		Rectangle sourceRect = new(0, tex.Height / 3 * Projectile.frame, tex.Width, tex.Height / 3);
		Vector2 drawOrigin = new(tex.Width / 2, tex.Height / 6);

		for (int k = Projectile.oldPos.Length - 1; k >= 0; k--)
		{
			Color color = Projectile.GetAlpha(lightColor);
			int alphaMod = 10 - (k * 2 + 1);
			color.R = (byte)(color.R * alphaMod / 20);
			color.G = (byte)(color.G * alphaMod / 20);
			color.B = (byte)(color.B * alphaMod / 20);
			color.A = (byte)(color.A * alphaMod / 20);
			Vector2 drawPos = Projectile.oldPos[k] + Projectile.Size * 0.5f - Main.screenPosition;
			Main.EntitySpriteDraw
			(
				tex,
				drawPos,
				sourceRect,
				color, Projectile.rotation,
				drawOrigin,
				Projectile.scale,
				SpriteEffects.None,
				0f
			);
		}

		Main.EntitySpriteDraw
		(
			tex,
			Projectile.Center - Main.screenPosition,
			sourceRect,
			lightColor,
			Projectile.rotation,
			drawOrigin,
			Projectile.scale,
			SpriteEffects.None
		);
		return false;
	}
}
public class PriminiLaser : ModProjectile
{
	int scaleSize1;
	int scaleSize2;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		Main.projPet[Projectile.type] = true;
		Main.projFrames[Projectile.type] = 3;
	}
	public override void SetDefaults()
	{
		Projectile.netImportant = true;
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.timeLeft *= 5;
		Projectile.minion = true;
		Projectile.minionSlots = 0f;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.friendly = true;
		scaleSize1 = (int)(Projectile.width * 1.35f);
		scaleSize2 = (int)(Projectile.height * 1.7f);
	}
	public ref float ShootDelay => ref Projectile.ai[1];
	public override void AI()
	{
		Player owner = Main.player[Projectile.owner];
		if (owner.dead)
		{
			owner.GetModPlayer<AvalonPlayer>().PrimeMinion = false;
		}
		if (owner.GetModPlayer<AvalonPlayer>().PrimeMinion)
		{
			Projectile.timeLeft = 2;
		}
		PrimeArmsCounter.ModifyPrimeMinionStats(Projectile, owner);

		if (Projectile.frame == 1)
		{
			Projectile.Resize(scaleSize1, scaleSize1);
		}
		if (Projectile.frame == 2)
		{
			Projectile.Resize(scaleSize2, scaleSize2);
		}

		SurroundOwner(owner);

		float maxDist = 800f;
		AvalonGlobalProjectile.GetMinionTarget(Projectile, owner, out bool hasTarget, out NPC target, out float targetDist, maxDist);
		if (!hasTarget || targetDist > maxDist || owner.Center.Distance(target.Center) > maxDist) // todo: replace last check with IsPlayerTooFarFromNPC method inside the gastromini minion
		{
			if (Projectile.Center.Distance(owner.Center) < 400f)
			{
				//Projectile.rotation = Projectile.Center.AngleTo(owner.Center);
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(owner.Center), 0.1f);
			}
			else
			{
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(owner.Center), 0.1f);
			}
			if (ShootDelay > 0)
			{
				ShootDelay -= 0.5f;
			}
			//Projectile.rotation = MathHelper.PiOver4;
			//Projectile.rotation = 0f;
			return;
		}
		AvalonGlobalProjectile.AvoidOwnedMinions(Projectile);
		//SurroundTarget(target, targetDist);
		Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(target.Center) + MathHelper.Pi, 0.1f);

		ShootDelay++;
		if (ShootDelay >= 95f/* && Collision.CanHit(Projectile, target)*/)
		{
			var proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.MiniRetinaLaser, Projectile.damage, 0f, Projectile.owner, 0f, 0f);
			proj.velocity = Projectile.Center.SafeDirectionTo(target.Center) * 8f;
			ShootDelay = 51f;
		}
	}
	//private void SurroundTarget(NPC target, float targetDist) // todo: move this to avalonglobalprojectile? (gastromini version has unique behaviour, though)
	//{
	//	Vector2 targetDir = Projectile.Center.SafeDirectionTo(target.Center);
	//	if (targetDist > 200f)
	//	{
	//		float dirMult = 8f;
	//		targetDir *= dirMult;
	//		Projectile.velocity = (Projectile.velocity * 40f + targetDir) / 41f;
	//	}
	//	else
	//	{
	//		float dirMult = 4f;
	//		targetDir *= -dirMult;
	//		Projectile.velocity = (Projectile.velocity * 40f + targetDir) / 41f;
	//	}
	//}
	private void SurroundOwner(Player owner)
	{
		if (Projectile.BottomRight.Y > owner.Center.Y - Main.rand.Next(28, 33) - 1f - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.Y > 0f)
			{
				Projectile.velocity.Y *= 0.96f;
			}
			Projectile.velocity.Y -= 0.2f;
			if (Projectile.velocity.Y > 6f)
			{
				Projectile.velocity.Y = 6f;
			}
		}
		else if (Projectile.BottomRight.Y < owner.Center.Y - Main.rand.Next(28, 33) - 1f - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.Y < 0f)
			{
				Projectile.velocity.Y *= 0.96f;
			}
			Projectile.velocity.Y += 0.2f;
			if (Projectile.velocity.Y < -6f)
			{
				Projectile.velocity.Y = -6f;
			}
		}
		if (Projectile.BottomRight.X > owner.Center.X - Main.rand.Next(38, 43) - 2f - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.X > 0f)
			{
				Projectile.velocity.X *= 0.94f;
			}
			Projectile.velocity.X -= 0.2f;
			if (Projectile.velocity.X > 8f)
			{
				Projectile.velocity.X = 8f;
			}
		}
		else if (Projectile.BottomRight.X < owner.Center.X - Main.rand.Next(38, 43) - 2f - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.X < 0f)
			{
				Projectile.velocity.X *= 0.94f;
			}
			Projectile.velocity.X += 0.2f;
			if (Projectile.velocity.X < -8f)
			{
				Projectile.velocity.X = -8f;
			}
		}
		//Projectile.BottomRight = owner.Center + new Vector2(-55f, -70f) + new Vector2(-2f, -1f);
		//Projectile.velocity = Vector2.Zero;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
	public override bool MinionContactDamage()
	{
		return false;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type].Value;
		Rectangle sourceRect = new(0, tex.Height / 3 * Projectile.frame, tex.Width, tex.Height / 3);
		Vector2 drawOrigin = new(tex.Width / 2, tex.Height / 6);

		for (int k = Projectile.oldPos.Length - 1; k >= 0; k--)
		{
			Color color = Projectile.GetAlpha(lightColor);
			int alphaMod = 10 - (k * 2 + 1);
			color.R = (byte)(color.R * alphaMod / 20);
			color.G = (byte)(color.G * alphaMod / 20);
			color.B = (byte)(color.B * alphaMod / 20);
			color.A = (byte)(color.A * alphaMod / 20);
			Vector2 drawPos = Projectile.oldPos[k] + Projectile.Size * 0.5f - Main.screenPosition;
			Main.EntitySpriteDraw
			(
				tex,
				drawPos,
				sourceRect,
				color, Projectile.rotation,
				drawOrigin,
				Projectile.scale,
				SpriteEffects.None,
				0f
			);
		}

		Main.EntitySpriteDraw
		(
			tex,
			Projectile.Center - Main.screenPosition,
			sourceRect,
			lightColor,
			Projectile.rotation,
			drawOrigin,
			Projectile.scale,
			SpriteEffects.None
		);
		return false;
	}
}
public class PriminiSaw : ModProjectile
{
	int scaleSize1;
	int scaleSize2;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		Main.projPet[Projectile.type] = true;
		Main.projFrames[Projectile.type] = 3;
	}
	public override void SetDefaults()
	{
		Projectile.netImportant = true;
		Projectile.width = 42;
		Projectile.height = 42;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.timeLeft *= 5;
		Projectile.minion = true;
		Projectile.minionSlots = 0f;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		scaleSize1 = (int)(Projectile.width * 1.35f);
		scaleSize2 = (int)(Projectile.height * 1.7f);
	}
	public ref float AttackDelay => ref Projectile.ai[1];
	public ref float ChargeAngle => ref Projectile.ai[2];
	public override void AI()
	{
		Player owner = Main.player[Projectile.owner];
		if (owner.dead)
		{
			owner.GetModPlayer<AvalonPlayer>().PrimeMinion = false;
		}
		if (owner.GetModPlayer<AvalonPlayer>().PrimeMinion)
		{
			Projectile.timeLeft = 2;
		}
		PrimeArmsCounter.ModifyPrimeMinionStats(Projectile, owner);

		if (Projectile.frame == 1)
		{
			Projectile.Resize(scaleSize1, scaleSize1);
		}
		if (Projectile.frame == 2)
		{
			Projectile.Resize(scaleSize2, scaleSize2);
		}

		// todo: have it ignore tiles as it only attacks nearby enemies
		float maxDist = 400f;
		AvalonGlobalProjectile.GetMinionTarget(Projectile, owner, out bool hasTarget, out NPC target, out float targetDist, maxDist, true);
		if (!hasTarget || targetDist > maxDist || owner.Center.Distance(target.Center) > maxDist)
		{
			if (Projectile.Center.Distance(owner.Center) < 400f)
			{
				//Projectile.rotation = Projectile.Center.AngleTo(owner.Center);
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(owner.Center), 0.1f);
			}
			else
			{
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(owner.Center), 0.1f);
			}
			AttackDelay = 0f;
			ChargeAngle = 0f;
			SurroundOwner(owner);
			return;
		}

		AvalonGlobalProjectile.AvoidOwnedMinions(Projectile);
		//SurroundTarget(target, targetDist);

		Vector2 rotationTarget = target.Center;
		float rotationMod = 0f;
		float rotationAmount = 0.1f;
		if (targetDist < 250f)
		{
			if (AttackDelay < 50f)
			{
				AttackDelay += 1f;
				rotationTarget = target.Top;
				if (Projectile.velocity.Length() > 2f)
				{
					Projectile.velocity *= 0.95f;
				}
				if (AttackDelay == 50f)
				{
					ChargeAngle = Projectile.Center.AngleTo(target.Center);
				}
			}
			else if (AttackDelay < 65f)
			{
				AttackDelay += 1f;
				rotationAmount = 0.2f;
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, ChargeAngle.ToRotationVector2() * 15f, 0.1f);
			}
			// todo: fix this shit, I want it to slash at the enemy once and then just move to its centre
			// what I should probably do is use an ai[] field to set an angle to charge towards while AttackDelay is between certain values
			//else if (targetDist >= 20f)
			//{
			//	//Projectile.velocity *= 0.003f;
			//}
			//else
			//{
			//	Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.SafeDirectionTo(rotationTarget) * 4f, 0.1f);
			//}
			else if (targetDist >= 20f)
			{
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.SafeDirectionTo(rotationTarget) * 4f, 0.1f);
			}
		}
		else
		{
			AttackDelay = 0f;
			ChargeAngle = 0f;
			rotationTarget = target.Top + new Vector2(0, -50);
			Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.SafeDirectionTo(rotationTarget) * 9f, 0.1f);
		}
		Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleFrom(rotationTarget) + rotationMod, rotationAmount);
	}
	private void SurroundOwner(Player owner)
	{
		if (Projectile.TopRight.Y > owner.Center.Y + Main.rand.Next(23, 28) - 12f + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.Y > 0f)
			{
				Projectile.velocity.Y *= 0.96f;
			}
			Projectile.velocity.Y -= 0.2f;
			if (Projectile.velocity.Y > 6f)
			{
				Projectile.velocity.Y = 6f;
			}
		}
		else if (Projectile.TopRight.Y < owner.Center.Y + Main.rand.Next(23, 28) - 12f + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.Y < 0f)
			{
				Projectile.velocity.Y *= 0.96f;
			}
			Projectile.velocity.Y += 0.2f;
			if (Projectile.velocity.Y < -6f)
			{
				Projectile.velocity.Y = -6f;
			}
		}
		if (Projectile.TopRight.X > owner.Center.X - Main.rand.Next(28, 33) - 2f - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.X > 0f)
			{
				Projectile.velocity.X *= 0.94f;
			}
			Projectile.velocity.X -= 0.2f;
			if (Projectile.velocity.X > 8f)
			{
				Projectile.velocity.X = 8f;
			}
		}
		else if (Projectile.TopRight.X < owner.Center.X - Main.rand.Next(28, 33) - 2f - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.X < 0f)
			{
				Projectile.velocity.X *= 0.94f;
			}
			Projectile.velocity.X += 0.2f;
			if (Projectile.velocity.X < -8f)
			{
				Projectile.velocity.X = -8f;
			}
		}
		//Projectile.TopRight = owner.Center + new Vector2(-55f, 55f) + new Vector2(-2f, -12f);
		////Projectile.rotation = 0f;
		////Projectile.rotation = -MathHelper.PiOver4;
		//Projectile.velocity = Vector2.Zero;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
	public override bool MinionContactDamage()
	{
		return true;
	}
	public override bool? CanHitNPC(NPC target)
	{
		if (target.type == NPCID.TargetDummy) return false;
		return base.CanHitNPC(target);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type].Value;
		Rectangle sourceRect = new(0, tex.Height / 3 * Projectile.frame, tex.Width, tex.Height / 3);
		Vector2 drawOrigin = new(tex.Width / 2, tex.Height / 6);

		for (int k = Projectile.oldPos.Length - 1; k >= 0; k--)
		{
			Color color = Projectile.GetAlpha(lightColor);
			int alphaMod = 10 - (k * 2 + 1);
			color.R = (byte)(color.R * alphaMod / 20);
			color.G = (byte)(color.G * alphaMod / 20);
			color.B = (byte)(color.B * alphaMod / 20);
			color.A = (byte)(color.A * alphaMod / 20);
			Vector2 drawPos = Projectile.oldPos[k] + Projectile.Size * 0.5f - Main.screenPosition;
			Main.EntitySpriteDraw
			(
				tex,
				drawPos,
				sourceRect,
				color, Projectile.rotation,
				drawOrigin,
				Projectile.scale,
				SpriteEffects.None,
				0f
			);
		}

		Main.EntitySpriteDraw
		(
			tex,
			Projectile.Center - Main.screenPosition,
			sourceRect,
			lightColor,
			Projectile.rotation,
			drawOrigin,
			Projectile.scale,
			SpriteEffects.None
		);
		return false;
	}
}
public class PriminiVice : ModProjectile
{
	int scaleSize1;
	int scaleSize2;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		Main.projPet[Projectile.type] = true;
		Main.projFrames[Projectile.type] = 3;
	}
	public override void SetDefaults()
	{
		Projectile.netImportant = true;
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.timeLeft *= 5;
		Projectile.minion = true;
		Projectile.minionSlots = 0f;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		scaleSize1 = (int)(Projectile.width * 1.35f);
		scaleSize2 = (int)(Projectile.height * 1.7f);
	}
	public ref float AttackDelay => ref Projectile.ai[1];
	public ref float ChargeAngle => ref Projectile.ai[2];
	public override void AI()
	{
		Player owner = Main.player[Projectile.owner];
		if (owner.dead)
		{
			owner.GetModPlayer<AvalonPlayer>().PrimeMinion = false;
		}
		if (owner.GetModPlayer<AvalonPlayer>().PrimeMinion)
		{
			Projectile.timeLeft = 2;
		}
		PrimeArmsCounter.ModifyPrimeMinionStats(Projectile, owner);

		if (Projectile.frame == 1)
		{
			Projectile.Resize(scaleSize1, scaleSize1);
		}
		if (Projectile.frame == 2)
		{
			Projectile.Resize(scaleSize2, scaleSize2);
		}

		// todo: have it ignore tiles as it only attacks nearby enemies
		float maxDist = 400f;
		AvalonGlobalProjectile.GetMinionTarget(Projectile, owner, out bool hasTarget, out NPC target, out float targetDist, maxDist, true);
		if (!hasTarget || targetDist > maxDist || owner.Center.Distance(target.Center) > maxDist)
		{
			if (Projectile.Center.Distance(owner.Center) < 400f)
			{
				//Projectile.rotation = Projectile.Center.AngleTo(owner.Center);
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(owner.Center), 0.1f);
			}
			else
			{
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleTo(owner.Center), 0.1f);
			}
			AttackDelay = 0f;
			ChargeAngle = 0f;
			SurroundOwner(owner);
			return;
		}

		AvalonGlobalProjectile.AvoidOwnedMinions(Projectile);
		//SurroundTarget(target, targetDist);

		// todo: fix jank rotation when it changes targets while attacking (possibly also for saw idk)
		Vector2 rotationTarget = target.Center;
		float rotationMod = 0f;
		float rotationAmount = 0.1f;
		if (targetDist < 250f)
		{
			if (AttackDelay < 50f)
			{
				AttackDelay += 1f;
				//rotationTarget = target.Top;
				if (Projectile.velocity.Length() > 2f)
				{
					Projectile.velocity *= 0.95f;
				}
				if (AttackDelay == 50f)
				{
					ChargeAngle = Projectile.Center.AngleTo(target.Center);
				}
			}
			else if (AttackDelay < 65f)
			{
				AttackDelay += 1f;
				rotationAmount = 0f;
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, ChargeAngle.ToRotationVector2() * 15f, 0.1f);
			}
			else if (targetDist >= 20f)
			{
				AttackDelay += 1f;
				if (AttackDelay >= 125f)
				{
					AttackDelay = 49f;
				}
				//float rot = (Utils.Remap(Projectile.rotation, -MathF.PI, MathF.PI, 0f, 5f) > 2.5f ? -MathHelper.PiOver4 * 0.65f : MathHelper.PiOver4 * 0.65f) * MathF.Sign(MathF.Cos(Projectile.rotation));
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.SafeDirectionTo(rotationTarget).RotatedBy(MathF.Sign(MathF.Cos(Projectile.Center.AngleTo(owner.Center))) * MathHelper.PiOver4 * 0.25f) * 4f, 0.05f);
			}
			//Main.NewText(Projectile.velocity.ToRotation());
		}
		else
		{
			AttackDelay = 0f;
			ChargeAngle = 0f;
			//rotationTarget = target.Top + new Vector2(0, -50);
			Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.SafeDirectionTo(rotationTarget) * 9f, 0.1f);
		}
		Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.AngleFrom(rotationTarget) + rotationMod, rotationAmount);
	}
	private void SurroundOwner(Player owner)
	{
		if (Projectile.TopLeft.Y > owner.Center.Y + Main.rand.Next(23, 28) - 6f + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.Y > 0f)
			{
				Projectile.velocity.Y *= 0.96f;
			}
			Projectile.velocity.Y -= 0.2f;
			if (Projectile.velocity.Y > 6f)
			{
				Projectile.velocity.Y = 6f;
			}
		}
		else if (Projectile.TopLeft.Y < owner.Center.Y + Main.rand.Next(23, 28) - 6f + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.Y < 0f)
			{
				Projectile.velocity.Y *= 0.96f;
			}
			Projectile.velocity.Y += 0.2f;
			if (Projectile.velocity.Y < -6f)
			{
				Projectile.velocity.Y = -6f;
			}
		}
		if (Projectile.TopLeft.X > owner.Center.X + Main.rand.Next(28, 33) + 6f + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.X > 0f)
			{
				Projectile.velocity.X *= 0.94f;
			}
			Projectile.velocity.X -= 0.2f;
			if (Projectile.velocity.X > 8f)
			{
				Projectile.velocity.X = 8f;
			}
		}
		else if (Projectile.TopLeft.X < owner.Center.X + Main.rand.Next(28, 33) + 6f + Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
		{
			if (Projectile.velocity.X < 0f)
			{
				Projectile.velocity.X *= 0.94f;
			}
			Projectile.velocity.X += 0.2f;
			if (Projectile.velocity.X < -8f)
			{
				Projectile.velocity.X = -8f;
			}
		}
		//Projectile.TopLeft = owner.Center + new Vector2(55f, 55f) + new Vector2(6f, -6f);
		////Projectile.rotation = MathF.PI;
		////Projectile.rotation = MathF.PI + MathHelper.PiOver4;
		//Projectile.velocity = Vector2.Zero;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
	public override bool MinionContactDamage()
	{
		return true;
	}
	public override bool? CanHitNPC(NPC target)
	{
		if (target.type == NPCID.TargetDummy) return false;
		return base.CanHitNPC(target);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type].Value;
		Rectangle sourceRect = new(0, tex.Height / 3 * Projectile.frame, tex.Width, tex.Height / 3);
		Vector2 drawOrigin = new(tex.Width / 2, tex.Height / 6);

		for (int k = Projectile.oldPos.Length - 1; k >= 0; k--)
		{
			Color color = Projectile.GetAlpha(lightColor);
			int alphaMod = 10 - (k * 2 + 1);
			color.R = (byte)(color.R * alphaMod / 20);
			color.G = (byte)(color.G * alphaMod / 20);
			color.B = (byte)(color.B * alphaMod / 20);
			color.A = (byte)(color.A * alphaMod / 20);
			Vector2 drawPos = Projectile.oldPos[k] + Projectile.Size * 0.5f - Main.screenPosition;
			Main.EntitySpriteDraw
			(
				tex,
				drawPos,
				sourceRect,
				color, Projectile.rotation,
				drawOrigin,
				Projectile.scale,
				SpriteEffects.None,
				0f
			);
		}

		Main.EntitySpriteDraw
		(
			tex,
			Projectile.Center - Main.screenPosition,
			sourceRect,
			lightColor,
			Projectile.rotation,
			drawOrigin,
			Projectile.scale,
			SpriteEffects.None
		);
		return false;
	}
}

