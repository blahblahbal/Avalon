using Avalon.Common.Interfaces;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Guns;

public class EnergyLaser : ModProjectile, ISyncedOnHitEffect
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[Type] = 4800;
	}
	public override void SetDefaults()
	{
		Projectile.width = 9;
		Projectile.height = 9;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.tileCollide = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
		Projectile.timeLeft = 20000;
		Projectile.extraUpdates = 600;
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 0; height = 0;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public override void AI()
	{
		bool small = Projectile.ai[2] == 1;
		if (!small && Projectile.localAI[2] == 0)
		{
			SoundEngine.PlaySound(SoundID.Item91 with {Volume = 0.5f,PitchVariance = 0.3f,MaxInstances = 7,Pitch = 1.6f}, Projectile.position);
			Vector2 muzzlePos = Projectile.Center + Vector2.Normalize(Projectile.velocity) * 48;
			var p = VanillaParticles.RequestPrettySparkleParticle();
			p.ColorTint = Color.Cyan;
			p.Scale = new Vector2(5f, 1.1f);
			p.Rotation = Projectile.velocity.ToRotation() + Main.rand.NextFloat(-0.2f,0.2f);
			p.LocalPosition = muzzlePos;
			p.TimeToLive = 15;
			p.FadeInEnd = 2;
			p.FadeOutStart = 4;
			Main.ParticleSystem_World_OverPlayers.Add(p);
			for(int i = 0; i < 5; i++)
			{
				Dust d = Dust.NewDustPerfect(muzzlePos, DustID.Electric, Projectile.velocity.RotatedByRandom(0.6f) * Main.rand.NextFloat(0.3f, 2f));
				d.noGravity = true;
				d.scale = 0.5f;
				d.position += d.velocity * 5;
			}
		}
		if (Main.rand.NextBool(15) && Projectile.velocity != Vector2.Zero)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, Projectile.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(2f));
			d.noGravity = true;
			d.scale = Main.rand.NextFloat();
		}
		Projectile.localAI[2]++;
		if (small && Projectile.localAI[2] == 100)
			Projectile.velocity = Vector2.Zero;
		if (Projectile.localAI[2] == 600)
		{
			Projectile.alpha += 15;
			Projectile.extraUpdates = 0;
			Projectile.damage = 0;
			Projectile.velocity = Vector2.Zero;
			if (!small)
			{
				for (int i = 0; i < 7; i++)
				{
					Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Smoke, Main.rand.NextVector2Circular(3, 3), 24);
					d.noGravity = Main.rand.NextBool();
				}
				for (int i = 0; i < 10; i++)
				{
					Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(3, 3), 24);
					d.scale *= 2;
					d.fadeIn = Main.rand.NextFloat(2);
					d.noGravity = true;
				}
				Gore g = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), Projectile.Center - new Vector2(15), Main.rand.NextVector2Circular(1, 1), Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1), Main.rand.NextFloat(0.25f, 0.75f));
				g.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
			}
			else
			{
				for (int i = 0; i < 7; i++)
				{
					Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric);
					d.noGravity = Main.rand.NextBool();
					d.velocity *= 2;
				}
			}
		}

		if(Projectile.alpha > 0)
			Projectile.alpha += 20;
		if (Projectile.alpha >= 255)
		{
			Projectile.Kill();
		}
		// fix to the laser not rendering if fired near the edges of the world, PROBABLY won't have issues with world size mods but... idk
		if (Projectile.position.X <= 16 || Projectile.position.X >= (Main.maxTilesX - 1) * 16 || Projectile.position.Y <= 16 || Projectile.position.Y >= (Main.maxTilesY - 1) * 16)
		{
			Projectile.velocity = Vector2.Zero;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Projectile.damage = (int)(Projectile.damage * 0.9f);
		if (Main.rand.NextBool(6))
			target.AddBuff(BuffID.OnFire3, 60 * 3);
		if (target.HasBuff<Buffs.Debuffs.EnergyRevolver>() && Projectile.ai[2] != 1)
		{
			Vector2 newPos = Main.rand.NextVector2FromRectangle(target.Hitbox);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), newPos, Main.rand.NextVector2CircularEdge(5,5), Type, Projectile.damage / 2, Projectile.knockBack / 2, Projectile.owner, newPos.X, newPos.Y,1);
		}
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (Main.rand.NextBool(6))
			target.AddBuff(BuffID.OnFire3, 60 * 3);
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity = Vector2.Zero;
		return false;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		//Rectangle screen = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
		Vector2 StartPos = new Vector2(Projectile.ai[0], Projectile.ai[1]); //screen.ClosestPointInRect(new Vector2(Projectile.ai[0], Projectile.ai[1]));
		Vector2 EndPos = Projectile.Center;//screen.ClosestPointInRect(Projectile.Center);
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, EndPos - Main.screenPosition, new Rectangle(0, 0, TextureAssets.Projectile[Type].Value.Width, TextureAssets.Projectile[Type].Value.Height), new Color(Projectile.Opacity, Projectile.Opacity * 1.3f, 1f, Projectile.Opacity * 0.75f), EndPos.DirectionTo(StartPos).ToRotation() + MathHelper.PiOver2, new Vector2(TextureAssets.Projectile[Type].Value.Width / 2f, TextureAssets.Projectile[Type].Value.Height), new Vector2(Projectile.Opacity * 1.4f * (1f - Projectile.ai[2] * 0.5f), EndPos.Distance(StartPos)), SpriteEffects.None);
		return false;
	}

	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		var p = VanillaParticles.RequestPrettySparkleParticle();
		p.ColorTint = new Color(0f,Main.rand.NextFloat(),1,0);
		p.Scale = new Vector2(6f, 1.2f);
		p.Rotation = Projectile.velocity.ToRotation() + Main.rand.NextFloat(-0.2f,0.2f);
		p.DrawVerticalAxis = false;
		p.LocalPosition = Projectile.Center;
		p.TimeToLive = 35;
		p.FadeInEnd = 2;
		p.FadeOutStart = 4;
		Main.ParticleSystem_World_OverPlayers.Add(p);

		for (int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(3, 3), 24);
			d.scale *= 2;
			d.noGravity = true;
		}
	}
}
