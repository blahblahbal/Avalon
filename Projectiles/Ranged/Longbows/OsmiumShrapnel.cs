using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Longbows;

public class OsmiumShrapnel : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Type] = 5;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.aiStyle = -1;
		Projectile.width = Projectile.height = 10;
		Projectile.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
		Projectile.penetrate = 3;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
	}
	public override void AI()
	{
		Projectile.rotation += Projectile.velocity.X * 0.03f;
		Projectile.velocity.Y += 0.1f;
		if (Projectile.ai[0] != -1 && !Projectile.Hitbox.Intersects(Main.npc[(int)Projectile.ai[0]].Hitbox))
			Projectile.ai[0] = -1;
	}
	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 3; i++)
		{
			Gore g = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(1, 1), Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1), Main.rand.NextFloat(0.25f, 0.75f));
			g.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
		}
		int type = ModContent.DustType<OsmiumDust>();
		for(int i = 0; i < 5; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, type);
			d.velocity.Y -= 1f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Tink with { Pitch = -0.2f, PitchVariance = 1f, MaxInstances = 10, Volume = 0.5f }, Projectile.position);
		Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
		return true;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return target.whoAmI != Projectile.ai[0];
	}
	public override bool PreDraw(ref Color lightColor)
	{
		SpriteEffects effect = Projectile.identity % 2 == 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
		for(int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i+= 2)
		{
			float percent = (1f - (i / 5f));
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, lightColor with { A = 128 } * percent * 0.4f, Projectile.oldRot[i], new Vector2(10), Projectile.scale, effect, 0);
		}
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value,Projectile.Center - Main.screenPosition, null,lightColor,Projectile.rotation, new Vector2(10), Projectile.scale, effect, 0);
		return false;
	}
}
