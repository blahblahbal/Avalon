using System;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.Templates; 

/// <summary>
/// Currently only contains behaviour for piercing boomerangs.
/// </summary>
public abstract class PiercingBoomerangTemplate : ModProjectile // Thanks example mod ! ! ! 
{
	public bool Returning { get => Convert.ToBoolean(Projectile.ai[0]); set => Projectile.ai[0] = Convert.ToSingle(value); }
	public float ReturnSpeed = 9f;
	public float ReturnAccel = 0.4f;
	public float SpinSpeed = 0.4f;
	public int TimeBeforeReturn = 30;
	public SoundStyle FlyingSound = SoundID.Item7;
	/// <summary>
	/// If true, sets spriteDirection to 1 if spinning clockwise, and -1 if spinning anti-clockwise.
	/// </summary>
	public bool SpriteSpinDirectionFlip = false;
	public override void AI()
	{
		if (Projectile.soundDelay == 0)
		{
			Projectile.soundDelay = 8;
			SoundEngine.PlaySound(FlyingSound, Projectile.position);
		}
		if (!Returning)
		{
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] >= TimeBeforeReturn)
			{
				Returning = true;
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
		}
		else
		{
			Projectile.tileCollide = false;
			float velCapX = Main.player[Projectile.owner].Center.X - Projectile.Center.X;
			float velCapY = Main.player[Projectile.owner].Center.Y - Projectile.Center.Y;
			float distToOwner = MathF.Sqrt(velCapX * velCapX + velCapY * velCapY);
			if (distToOwner > 3000f)
			{
				Projectile.Kill();
			}
			distToOwner = ReturnSpeed / distToOwner;
			velCapX *= distToOwner;
			velCapY *= distToOwner;
			if (Projectile.velocity.X < velCapX)
			{
				Projectile.velocity.X = Projectile.velocity.X + ReturnAccel;
				if (Projectile.velocity.X < 0f && velCapX > 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X + ReturnAccel;
				}
			}
			else if (Projectile.velocity.X > velCapX)
			{
				Projectile.velocity.X = Projectile.velocity.X - ReturnAccel;
				if (Projectile.velocity.X > 0f && velCapX < 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X - ReturnAccel;
				}
			}
			if (Projectile.velocity.Y < velCapY)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + ReturnAccel;
				if (Projectile.velocity.Y < 0f && velCapY > 0f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y + ReturnAccel;
				}
			}
			else if (Projectile.velocity.Y > velCapY)
			{
				Projectile.velocity.Y = Projectile.velocity.Y - ReturnAccel;
				if (Projectile.velocity.Y > 0f && velCapY < 0f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - ReturnAccel;
				}
			}
			if (Main.myPlayer == Projectile.owner)
			{
				if (Projectile.Hitbox.Intersects(Main.player[Projectile.owner].Hitbox))
				{
					Projectile.Kill();
				}
			}
		}
		if (Projectile.ai[2] == 0)
		{
			Projectile.ai[2] = Math.Sign(Projectile.velocity.X);
		}
		Projectile.direction = (int)Projectile.ai[2];
		Projectile.rotation += SpinSpeed * Projectile.direction;
		if (SpriteSpinDirectionFlip) Projectile.spriteDirection = Projectile.direction;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		int collideWidth = 10;
		int collideHeight = 10;
		Projectile.velocity = Collision.TileCollision(Projectile.Center - new Vector2(collideWidth / 2, collideHeight / 2), Projectile.velocity, collideWidth, collideHeight, true, true, 1);
		Returning = true;
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		return false;
	}
}
