using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Projectiles.Magic.Other;
public abstract class ToysBase : ModProjectile
{
	public virtual SoundStyle? CollideSound => null;
	public virtual SoundStyle? DeathSound => null;
	public virtual float Friction => 0.03f;
	public virtual bool Scale => false;
	public virtual float BounceStrength => 1f;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Type] = 3;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
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
			Projectile.velocity.Y += 0.3f;
		}
		else if (Scale)
		{
			Projectile.scale = Utils.Remap(Projectile.ai[1], 0, 11, 0, 1);
		}
		Projectile.rotation += Projectile.velocity.X * 0.05f;
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
			if (Projectile.velocity.X != oldVelocity.X)
			{
				playSound = true;
				Projectile.velocity.X = oldVelocity.X * -BounceStrength;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				if (!(Projectile.velocity.Y is >= 0 and <= 0.3f && oldVelocity.Y < 1.2f))
				{
					playSound = true;
					Projectile.velocity.Y = oldVelocity.Y * -BounceStrength;
					Projectile.velocity.Y += 0.3f;
				}
			}
			if (playSound) SoundEngine.PlaySound(CollideSound, Projectile.Center);
			Projectile.velocity.X *= (1f - Friction);
		}
		return false;
	}
	//public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	//{
	//	OnHitEntity(target);
	//}
	//public override void OnHitPlayer(Player target, Player.HurtInfo info)
	//{
	//	OnHitEntity(target);
	//}
	//public void OnHitEntity(Entity target)
	//{
	//}
	public override bool PreDraw(ref Color lightColor)
	{
		Rectangle frame = TextureAssets.Projectile[Type].Frame(verticalFrames: Main.projFrames[Type], frameY: Projectile.frame);
		Vector2 frameOrigin = frame.Size() / 2f;

		DrawData d = new(TextureAssets.Projectile[Type].Value, Vector2.Zero, frame, lightColor, 0, frameOrigin, Projectile.scale, SpriteEffects.None);

		for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
		{
			Main.EntitySpriteDraw(d with { position = Projectile.oldPos[i] + (Projectile.Size / 2) - Main.screenPosition, rotation = Projectile.oldRot[i], color = lightColor with { A = 200 } * (1f - (i / (float)Projectile.oldPos.Length)) * 0.75f });
		}
		Main.EntitySpriteDraw(d with { position = Projectile.Center - Main.screenPosition, rotation = Projectile.rotation, color = lightColor });
		return false;
	}
}
public class Toys_Lego : ToysBase
{
	public override float Friction => 0.05f;
	public override float BounceStrength => 0.5f;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.projFrames[Type] = 3;
	}
}
public class Toys_Marble : ToysBase
{
	public override float Friction => 0.01f;
	public override float BounceStrength => 0.675f;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.projFrames[Type] = 3;
	}
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
	public override float BounceStrength => 0.5f;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.projFrames[Type] = 2;
	}
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
public class Toys_Doll : ToysBase
{
	public override float Friction => 0.05f;
	public override float BounceStrength => 0.25f;
	public override bool Scale => true;
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
public class Toys_Teddy : Toys_Doll { }
public class Toys_RockingHorse : ToysBase
{
	public override float Friction => 0.04f;
	public override float BounceStrength => 0.25f;
	public override bool Scale => true;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		ProjectileID.Sets.TrailCacheLength[Type] = 1;
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 36;
		Projectile.height = 36;
	}
}
public class Toys_Table : Toys_RockingHorse
{
	public override string Texture => $"Terraria/Images/Tiles_{TileID.Tables}";
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.projFrames[Type] = 13;
	}
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
			11 => ContentSamples.ItemsByType[ItemID.BalloonTable].placeStyle,
			12 => ContentSamples.ItemsByType[ItemID.AshWoodTable].placeStyle,
			_ => Projectile.frame
		};
	}
	public override bool PreDraw(ref Color lightColor)
	{
		int style = GetTableStyle();
		DrawData d = new(Projectile.frame >= 10 ? TextureAssets.Tile[TileID.Tables2].Value : TextureAssets.Projectile[Type].Value, Vector2.Zero, null, lightColor, 0, Vector2.Zero, Projectile.scale, SpriteEffects.None);
		TileObjectData tod = TileObjectData.GetTileData(Projectile.frame >= 10 ? TileID.Tables2 : TileID.Tables, style);

		d.position = Projectile.Center - Main.screenPosition;
		d.rotation = Projectile.rotation;
		d.color = lightColor;
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