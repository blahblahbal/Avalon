using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Avalon.Projectiles.Magic.Other;
public abstract class ToysBase : ModProjectile
{
	public virtual SoundStyle? CollideSound => null;
	public virtual SoundStyle? DeathSound => null;
	public virtual float Friction => 0.03f;
	public virtual bool Scale => false;
	public virtual float BounceStrength => 1f;
	public virtual int Variants => 1;
	public virtual int TrailAfterimageCount => 2;
	public virtual float Gravity => 0.3f;
	public override void SetStaticDefaults()
	{
		if (TrailAfterimageCount > 0)
		{
			ProjectileID.Sets.TrailCacheLength[Type] = TrailAfterimageCount + 1;
			ProjectileID.Sets.TrailingMode[Type] = 2;
		}
		Main.projFrames[Type] = Variants;
	}
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.timeLeft = 300;
	}
	public override void OnSpawn(IEntitySource source)
	{
		Projectile.frame = Main.rand.Next(Main.projFrames[Type]);
	}
	public override void AI()
	{
		Projectile.ai[1]++;
		if (Projectile.ai[1] > 15)
		{
			if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
			{
				if ((double)Projectile.velocity.X > -0.01 && (double)Projectile.velocity.X < 0.01)
				{
					Projectile.velocity.X = 0f;
					Projectile.netUpdate = true;
				}
			}
			Projectile.velocity.Y += Gravity;
		}
		else if (Scale)
		{
			Projectile.scale = Utils.Remap(Projectile.ai[1], 0, 11, 0, 1);
		}
		Projectile.rotation += Projectile.velocity.X * 0.05f;

		Projectile.Opacity = Projectile.timeLeft / 120f;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.penetrate > 0)
		{
			Projectile.penetrate--;
		}
		if (Projectile.penetrate == 0)
		{
			SoundEngine.PlaySound(DeathSound, Projectile.Center);
			Projectile.Kill();
		}
		else
		{
			bool playSound = false;

			Vector2 newOldVelocity = Projectile.velocity;
			if (newOldVelocity.X != oldVelocity.X)
			{
				// todo: fix this stopping a couple pixels before the wall, it would probably be similar to the abs gravity check which adds newOldVelocity to pos and sets velocity to 0
				Projectile.velocity.X = oldVelocity.X * -BounceStrength;
			}
			if (newOldVelocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = oldVelocity.Y * -BounceStrength + Gravity;
				Roll(newOldVelocity, oldVelocity);
			}
			if (Math.Abs(Projectile.velocity.Y) <= Gravity)
			{
				Projectile.position.Y += newOldVelocity.Y;
				Projectile.velocity.Y = 0;
			}

			if (playSound) SoundEngine.PlaySound(CollideSound, Projectile.Center);
		}
		return false;
	}
	public virtual void Roll(Vector2 velocityAtStartOfCollide, Vector2 oldVelocity)
	{
		Projectile.velocity.X *= (1f - Friction);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Rectangle frame = TextureAssets.Projectile[Type].Frame(verticalFrames: Main.projFrames[Type], frameY: Projectile.frame);
		Vector2 frameOrigin = frame.Size() / 2f;
		frameOrigin.Y -= 1;

		lightColor *= Projectile.Opacity;
		DrawData d = new(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, frame, lightColor, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None);


		for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
		{
			Main.EntitySpriteDraw(d with { position = Projectile.oldPos[i] + (Projectile.Size / 2) - Main.screenPosition, rotation = Projectile.oldRot[i], color = lightColor with { A = (byte)(200 * Projectile.Opacity) } * (1f - (i / (float)Projectile.oldPos.Length)) * 0.75f });
		}
		Main.EntitySpriteDraw(d);
		return false;
	}
}
public class Toys_Lego : ToysBase
{
	public override float Friction => 0.05f;
	public override float BounceStrength => 0.3f;
	public override int Variants => 3;
}
public class Toys_Monkey : Toys_Lego
{
	public override void OnSpawn(IEntitySource source)
	{
		base.OnSpawn(source);
		Projectile.frame = Projectile.ai[2] > 0 ? (int)Projectile.ai[2] - 1 : Projectile.frame;
	}
}
public class Toys_Ball : ToysBase
{
	public override float Friction => 0.015f;
	public override float BounceStrength => 0.95f;
	public override int Variants => 4;
}
public class Toys_Marble : ToysBase
{
	public override float Friction => 0.01f;
	public override float BounceStrength => 0.45f;
	public override int Variants => 3;
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 12;
		Projectile.height = 12;
	}
}
public class Toys_Die : ToysBase
{
	public override float Friction => 0.05f;
	public override float BounceStrength => 0.35f;
	public override int Variants => 2;
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 16;
		Projectile.height = 16;
	}
}
//public class Toys_Vase : ToysBase
//{
//	public override float Friction => base.Friction;
//	public override bool Scale => true;
//}
public abstract class Toys_Plush : ToysBase
{
	public override float Friction => 0.07f;
	public override float BounceStrength => 0.05f;
	public override int TrailAfterimageCount => 0;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		ProjectileID.Sets.TrailCacheLength[Type] = 1;
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 24;
		Projectile.height = 24;
	}
}
public class Toys_PlushDoll : Toys_Plush { }
public class Toys_PlushTeddy : Toys_Plush { }
public class Toys_PlushSanta : Toys_Plush { }
public abstract class Toys_LargeWooden : ToysBase
{
	public override float Friction => 0.04f;
	public override float BounceStrength => 0.225f;
	public override bool Scale => true;
	public override int TrailAfterimageCount => 0;
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 36;
		Projectile.height = 36;
	}
}
public class Toys_RockingHorse : Toys_LargeWooden
{
	public override void Roll(Vector2 velocityAtStartOfCollide, Vector2 oldVelocity)
	{
		base.Roll(velocityAtStartOfCollide, oldVelocity);

		float rotWrapped = MathHelper.WrapAngle(Projectile.rotation);
		float b = MathF.Abs(rotWrapped) / MathF.PI;
		if (b > 0.5f) b = Utils.Clamp(1f - b, 0.05f, 1);
		float a = MathF.Pow(b, 0.75f) * -MathF.Sign(rotWrapped) * 0.25f;

		if (Collision.FindCollisionDirection(out int dir, Projectile.position + new Vector2(Projectile.velocity.X + a, 0), Projectile.width, Projectile.height))
		{
			if (dir == 1 && MathF.Sign(a) == -1) return;
			if (dir == 0 && MathF.Sign(a) == 1) return;
		}
		Projectile.velocity.X += a;
	}
}
// todo: make this also roll to stand upright, except unlike the rocking horse, make it also able to roll to being upside down
public class Toys_Table : Toys_LargeWooden
{
	public override string Texture => $"Terraria/Images/Tiles_{TileID.Tables}";
	public override int Variants => 12;
	public int GetTableStyle()
	{
		return Projectile.frame switch
		{
			4 => ContentSamples.ItemsByType[ItemID.ShadewoodTable].placeStyle,
			5 => ContentSamples.ItemsByType[ItemID.BanquetTable].placeStyle,
			6 => ContentSamples.ItemsByType[ItemID.Bar].placeStyle,
			7 => ContentSamples.ItemsByType[ItemID.PineTable].placeStyle,
			8 => ContentSamples.ItemsByType[ItemID.PalmWoodTable].placeStyle,
			9 => ContentSamples.ItemsByType[ItemID.BorealWoodTable].placeStyle,
			10 => ContentSamples.ItemsByType[ItemID.BambooTable].placeStyle,
			11 => ContentSamples.ItemsByType[ItemID.AshWoodTable].placeStyle,
			_ => Projectile.frame
		};
	}
	public override bool PreDraw(ref Color lightColor)
	{
		int style = GetTableStyle();
		lightColor *= Projectile.Opacity;
		DrawData d = new(Projectile.frame >= 10 ? TextureAssets.Tile[TileID.Tables2].Value : TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, Vector2.Zero, Projectile.scale, SpriteEffects.None);
		TileObjectData tod = TileObjectData.GetTileData(Projectile.frame >= 10 ? TileID.Tables2 : TileID.Tables, style);

		for (int i = 0; i < tod.Width * tod.Height; i++)
		{
			int segmentX = i % tod.Width;
			int segmentY = i / tod.Width;
			int width = tod.CoordinateWidth + tod.CoordinatePadding;
			int height = segmentY > 0 ? tod.CoordinateHeights[segmentY - 1] + tod.CoordinatePadding : 0;
			int styleOffset = style * width * tod.Width;
			Rectangle frame = new(styleOffset + segmentX * width, segmentY * height, tod.CoordinateWidth, tod.CoordinateHeights[segmentY]);
			// offsets to prevent gaps when rotated
			float originOffsetX = (segmentX - tod.Width * 0.5f + 0.5f) * 0.0035f;
			float originOffsetY = (segmentY - tod.Height * 0.5f + 0.5f) * 0.0035f;
			// the Y origin is maybe incorrect but uhhhh idk I tried using the sum of coord heights and it just had a gap between segments, whatever
			Main.EntitySpriteDraw(d with { sourceRect = frame, origin = new Vector2(tod.CoordinateWidth * tod.Width / 2 - segmentX * tod.CoordinateWidth + originOffsetX, tod.CoordinateHeights[segmentY] * tod.Height / 2 - segmentY * tod.CoordinateHeights[segmentY] + originOffsetY) });
		}
		return false;
	}
}
public class Toys_MonkeyBarrel : ToysBase
{
	public override int Variants => 3;
	public override int TrailAfterimageCount => 0;
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 24;
		Projectile.height = 24;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.Kill();
		return false;
	}
	public override void PostAI()
	{
		if (Projectile.timeLeft <= ContentSamples.ProjectilesByType[Type].timeLeft / 2) Projectile.Kill();
	}
	public override void OnKill(int timeLeft)
	{
		// If we are the original projectile running on the owner, spawn the 5 child projectiles.
		if (Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < Main.rand.Next(3, 7); i++)
			{
				Vector2 vel = Main.rand.NextVector2CircularEdge(9f, 9f);
				int monkeyFrame = (Projectile.frame + 1) % 3 + 1;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<Toys_Monkey>(), Projectile.damage, Projectile.knockBack, Projectile.owner, ai2: monkeyFrame);
			}
		}

		// Play explosion sound
		SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

		for (int i = 0; i < 7; i++)
		{
			Dust d = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.2f);
			d.velocity *= 1.1f;
		}
		//for (int i = 0; i < 5; i++)
		//{
		//	Dust d = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
		//	d.noGravity = true;
		//	d.velocity *= 5f;
		//	d = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
		//	d.velocity *= 3f;
		//}
		Gore g = Gore.NewGoreDirect(new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.NextFromList([GoreID.Smoke1, GoreID.Smoke2, GoreID.Smoke3]), 0.75f);
		g.velocity *= 0.2f;
		g.velocity += Main.rand.NextVector2CircularEdge(1f, 1f);
		g = Gore.NewGoreDirect(new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.NextFromList([GoreID.Smoke1, GoreID.Smoke2, GoreID.Smoke3]), 0.75f);
		g.velocity *= 0.2f;
		g.velocity += Main.rand.NextVector2CircularEdge(1f, 1f);
		g = Gore.NewGoreDirect(new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.NextFromList([GoreID.Smoke1, GoreID.Smoke2, GoreID.Smoke3]), 0.75f);
		g.velocity *= 0.2f;
		g.velocity += Main.rand.NextVector2CircularEdge(1f, 1f);
	}
}