using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

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
