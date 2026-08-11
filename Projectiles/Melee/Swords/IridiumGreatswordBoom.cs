using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class IridiumGreatswordBoomLarge : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 5;
	}
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(128);
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 10;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type];
		var frame = tex.Frame(1, Main.projFrames[Type], 0, Projectile.frame);
		Main.EntitySpriteDraw(tex.Value, Projectile.Bottom - Main.screenPosition, frame, Color.White with { A = 128}, 0, new Vector2(frame.Width / 2, frame.Height), 1, SpriteEffects.None);
		return false;
	}
	public override void AI()
	{
		if (Projectile.timeLeft == 9)
		{
			SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode with { MaxInstances = 10 }, Projectile.position);
			int dustType = ModContent.DustType<SimpleColorableGlowyDust>();
			for (int i = 0; i < 15; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Bottom, dustType, new Vector2(Main.rand.NextFloat(-Projectile.width,Projectile.width) * 0.1f, Main.rand.NextFloat(-Projectile.width, 0) * 0.13f));
				d.noGravity = true;
				d.color = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0.5f);
				d.scale += Main.rand.NextFloat();
				d.fadeIn = Main.rand.NextFloat(1.5f);

				Dust d2 = Dust.NewDustPerfect(Projectile.Bottom, DustID.Wraith, new Vector2(Main.rand.NextFloat(-Projectile.width, Projectile.width) * 0.1f, Main.rand.NextFloat(-Projectile.width, 0) * 0.1f));
				d2.noGravity = true;
				d2.scale += Main.rand.NextFloat(0.5f);
			}
		}
		Projectile.frame = (int)MathF.Round((1f - (Projectile.timeLeft / 10f)) * Main.projFrames[Type]);
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = target.Center.X <= Main.player[Projectile.owner].Center.X ? -1 : 1;
	}

	public static Vector2 FindSpotForSpike(Vector2 point)
	{
		Point pointInWorld = point.ToTileCoordinates();
		if (!WorldGen.InWorld(pointInWorld.X, pointInWorld.Y))
			return Vector2.Zero;
		for (int i = 0; i < 16; i++)
		{
			Point PointToCheck = pointInWorld + new Point(0, i % 2 == 0 ? i / 2 : (-i + 1) / 2);
			Vector2 worldCoords = PointToCheck.ToWorldCoordinates();
			if (!Collision.IsWorldPointSolid(worldCoords) && (Main.tile[PointToCheck.X, PointToCheck.Y + 1].LiquidAmount > 0 || Collision.IsWorldPointSolid(new Vector2(worldCoords.X, worldCoords.Y + 16))))
				return worldCoords + Vector2.UnitY * 16;
		}
		return Vector2.Zero;
	}
	public override void OnKill(int timeLeft)
	{
		Vector2 pos = FindSpotForSpike(Projectile.Bottom + new Vector2(Projectile.ai[0] * 100, 0));
		if(pos != Vector2.Zero && Main.myPlayer == Projectile.owner)
		{
			int type = ModContent.ProjectileType<IridiumGreatswordBoomMedium>();
			pos.Y -= ContentSamples.ProjectilesByType[type].height / 2;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos, Vector2.Zero, type, (int)(Projectile.damage * 0.75f),Projectile.knockBack * 0.75f, Projectile.owner, Projectile.ai[0]);
		}
	}
}
public class IridiumGreatswordBoomMedium : IridiumGreatswordBoomLarge
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 4;
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.Size = new Vector2(80);
	}
	public override void OnKill(int timeLeft)
	{
		Vector2 pos = FindSpotForSpike(Projectile.Bottom + new Vector2(Projectile.ai[0] * 80, 0));
		if (pos != Vector2.Zero && Main.myPlayer == Projectile.owner)
		{
			int type = ModContent.ProjectileType<IridiumGreatswordBoomSmall>();
			pos.Y -= ContentSamples.ProjectilesByType[type].height / 2;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos, Vector2.Zero, type, (int)(Projectile.damage * 0.75f), Projectile.knockBack * 0.75f, Projectile.owner, Projectile.ai[0]);
		}
	}
}
public class IridiumGreatswordBoomSmall : IridiumGreatswordBoomLarge
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 4;
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.Size = new Vector2(64);
	}
	public override void OnKill(int timeLeft)
	{
	}
}