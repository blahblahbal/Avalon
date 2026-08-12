using Avalon.Core;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Bows;

public class HellrazerStakeExplosion : ModProjectile
{
	public override string Texture => ModContent.GetInstance<HellrazerStake>().Texture;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(64);
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
		Projectile.tileCollide = false;
		Projectile.hide = true;
	}
	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		return targetHitbox.ClosestPointInRect(Projectile.Center).Distance(Projectile.Center) < 26 * (0.5f + Projectile.ai[1] * 0.5f);
	}
	public override void AI()
	{
		Projectile.ai[0]--;
		if (Projectile.ai[0] == -1)
		{
			SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/meteor_shower_", [0, 1, 2, 3]) with { MaxInstances = 15, volume = 0.8f, pitch = 0.5f }, Projectile.Center);
			//SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode with { pitchVariance = 1, MaxInstances = 10 }, Projectile.position);
			var tex = AssetReferences.Assets.Textures.FireballExplosion.Asset;
			switch (Projectile.ai[1])
			{
				case 1:
					tex = AssetReferences.Assets.Textures.FireballExplosionSmall.Asset;
					break;
				case 2:
					tex = AssetReferences.Assets.Textures.FireballExplosionMedium.Asset;
					break;
			}
			tex.Wait();
			var p = AnimatedParticle.Request();
			p.SetBasicInfo(tex, 5, Vector2.Zero, Projectile.Center);
			p.SetTypeInfo(Main.rand.Next(10,20));
			p.ColorTint = Color.White with { A = 128 };
			p.Scale = Vector2.One;
			p.Rotation = Main.rand.NextFloatDirection();
			Main.ParticleSystem_World_OverPlayers.Add(p);

			for (int i = 0; i < 20; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.DesertTorch);
				d.velocity += Main.rand.NextVector2Circular(2, 2) * Projectile.ai[1];
				d.scale += Main.rand.NextFloat();
				d.noGravity = true;
				d.fadeIn = Main.rand.NextFloat(1.5f);
			}
			for (int i = 0; i < 10; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Wraith);
				d.velocity += Main.rand.NextVector2Circular(3, 3) * Projectile.ai[1];
				d.noGravity = true;
				d.alpha = Main.rand.Next(128);
			}
		}
		else if (Projectile.ai[0] >= 0)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.DesertTorch);
			d.velocity += Vector2.UnitY * -Main.rand.NextFloat(2);
			d.scale += Main.rand.NextFloat();
			d.noGravity = true;
		}
		else if (Projectile.ai[0] < -10)
		{
			Projectile.Kill();
		}
	}
	public override bool? CanDamage()
	{
		if (Projectile.ai[0] > 0)
			return false;
		return base.CanDamage();
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = target.Center.X <= Main.player[Projectile.owner].Center.X ? -1 : 1;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire3, 60 * 4);
	}
}