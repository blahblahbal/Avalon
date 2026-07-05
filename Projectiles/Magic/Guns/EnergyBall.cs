using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Guns;

public class EnergyBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 3;
	}
	public override void SetDefaults()
	{
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.tileCollide = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		Projectile.timeLeft = 120;
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 8; height = 8;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public override void AI()
	{
		Projectile.velocity *= 0.99f;
		Projectile.ai[0]++;
		if (Projectile.ai[0] == 1)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, Projectile.velocity.RotatedByRandom(0.6f) * Main.rand.NextFloat(0.3f, 1f));
				d.noGravity = true;
				d.scale = 0.75f;
				d.position += d.velocity * 5;
			}
			SoundEngine.PlaySound(SoundID.Item91, Projectile.position);
		}
		Projectile.frameCounter++;
		if(Projectile.frameCounter > 4)
		{
			Projectile.frameCounter = 0;
			Projectile.frame++;
			if (Projectile.frame == 3)
				Projectile.frame = 0;
		}
		if (Projectile.Opacity < 1)
			return;
		if (Main.rand.NextBool(4))
		{
			Dust d = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Electric);
			d.noGravity = !Main.rand.NextBool(4);
			d.scale = 0.75f;
			d.velocity *= 1.5f;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Buffs.Debuffs.EnergyRevolver>(), 60 * 5);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D tex = TextureAssets.Projectile[Type].Value;
		Rectangle frame = tex.Frame(1, 3, 0, Projectile.frame);
		//ulong seed4 = Utils.RandomNextSeed((ulong)Main.timeForVisualEffects / 3);
		//float iterations = 6f;
		//for (int i = 0; i < iterations; i++)
		//{
		//	Vector2 rand = new Vector2(Utils.RandomInt(ref seed4, -2 + (i * -2), 3 + (i * 2)), Utils.RandomInt(ref seed4, -2 + (i * -2), 3 + (i * 2)));
		//	Main.EntitySpriteDraw(tex, Projectile.Center + rand - Main.screenPosition - Projectile.velocity * i * 2, frame, new Color(1, 1, 1f, 0f) * Projectile.Opacity * 0.5f * (1f - i / iterations), Projectile.rotation + MathHelper.PiOver4, new Vector2(21), Projectile.scale - (i / iterations / 2), SpriteEffects.None);
		//}

		Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, frame, new Color(0.25f, 0.5f, 1f, 0.6f) * Projectile.Opacity * 0.5f, Projectile.rotation + MathHelper.PiOver4, new Vector2(21), Projectile.scale * 1.5f, SpriteEffects.None);
		Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, frame, new Color(1f, 1f, 1f, 0.6f) * Projectile.Opacity, Projectile.rotation, new Vector2(21), Projectile.scale, SpriteEffects.None);
		return false;
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath56,Projectile.position);
		if (!TextureAssets.Projectile[ProjectileID.ScytheWhipProj].IsLoaded)
			Main.instance.LoadProjectile(ProjectileID.ScytheWhipProj);
		for (int i = 0; i < 5; i++)
		{
			var asset = TextureAssets.Projectile[ProjectileID.ScytheWhipProj];
			var p = VanillaParticles.RequestRandomizedFrameParticle();
			int time = Main.rand.Next(15, 30);
			p.SetBasicInfo(asset, null, Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(5, 9), Projectile.Center);
			p.SetTypeInfo(Main.projFrames[ProjectileID.ScytheWhipProj], 2, time);

			p.ColorTint = new Color(0.1f, Main.rand.NextFloat(0.5f, 1f), 1f, 0.5f);
			p.Scale = new Vector2(1.25f, 0.75f);
			p.FadeInNormalizedTime = 0.2f;
			p.FadeOutNormalizedTime = 0.2f;
			p.Rotation = p.Velocity.ToRotation();

			p.AccelerationPerFrame = -p.Velocity / time;
			Main.ParticleSystem_World_BehindPlayers.Add(p);
			for (int i2 = 0; i2 < 5; i2++)
			{
				Dust d = Dust.NewDustPerfect(p.LocalPosition, DustID.Electric);
				d.noGravity = true;
				d.scale = 0.85f;
				d.velocity = p.Velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat();
			}
		}
		int interations = 35;
		for (int i = 0; i < interations; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
			d.noGravity = true;
			d.velocity = Vector2.UnitY.RotatedBy((i * MathHelper.TwoPi / interations) + Main.rand.NextFloat(-0.1f,0.1f)) * Main.rand.NextFloat(8);
			d.scale = 0.5f;
		}
	}
}
