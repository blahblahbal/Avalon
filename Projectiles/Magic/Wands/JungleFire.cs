using Avalon.Dusts;
using Avalon.Items.Weapons.Magic.Wands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Wands;

public class JungleFire : ModProjectile
{
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = 24;
		Projectile.height = 24;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
		DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 1.5f;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.ai[2] > 0 && Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<JunglePetal>()] < 16)
		{
			for (int i = 0; i < Main.rand.Next(2, 4); i++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2CircularEdge(1.9f, 1.9f), ModContent.ProjectileType<JunglePetal>(), (int)(Projectile.damage * 0.4f), Projectile.knockBack * 0.1f, Projectile.owner);
			}
		}
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] < 5f)
		{
			Projectile.ai[2] = -30;
		}
		else if (Projectile.ai[0] == 5f)
		{
			Projectile.ai[2] = -15;
		}
		float radius = 2.6f;
		if (Projectile.ai[2] < 0)
		{
			radius = 2.1f;
		}
		if (Projectile.ai[0] >= 6f)
		{
			Projectile.position += Projectile.velocity;
			for (int i = 0; i < Main.rand.Next(2, 4); i++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2CircularEdge(radius, radius), ModContent.ProjectileType<JunglePetal>(), (int)(Projectile.damage * 0.5f), Projectile.knockBack * 0.1f, Projectile.owner);
			}
			Projectile.Kill();
		}
		else
		{
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
		}
		return false;
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		for (int i = 0; i < Main.rand.Next(2, 4); i++)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2CircularEdge(1.9f, 1.9f), ModContent.ProjectileType<JunglePetal>(), (int)(Projectile.damage * 0.5f), Projectile.knockBack * 0.1f, Projectile.owner);
		}
		for (int i = 0; i < Main.rand.Next(2, 4); i++)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2CircularEdge(2.6f, 2.6f), ModContent.ProjectileType<JunglePetal>(), (int)(Projectile.damage * 0.5f), Projectile.knockBack * 0.1f, Projectile.owner);
		}
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
	}
	public override void AI()
	{
		Projectile.ai[2]++;
		Lighting.AddLight(Projectile.position, 0.2f, 0.2f, 0.1f);
		if (Projectile.type == ModContent.ProjectileType<JungleFire>())
		{
			for (var num157 = 0; num157 < 2; num157++)
			{
				if (Main.rand.NextBool(15))
				{
					var d1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.JungleGrass, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1.3f);
					Main.dust[d1].noGravity = true;
					Main.dust[d1].velocity *= 0.3f;
				}
				if (Main.rand.NextBool(5))
				{
					Dust d2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2CircularEdge(10f, 10f), ModContent.DustType<JunglePetalDust>(), Projectile.velocity * 0.2f, 40, default(Color), 1.5f);
					d2.noGravity = true;
					d2.velocity *= 0.3f;
					d2.rotation = Main.rand.NextFloat(MathHelper.Pi * 2);
				}
			}
		}
		Projectile.ai[1]++;
		if (Projectile.ai[1] >= 20f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
		}
		Projectile.rotation += 0.2f * Projectile.direction;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
		if (Projectile.ai[1] % 20 == 0 && Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<JunglePetal>()] < 24)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(Main.rand.Next(-14, 15), Main.rand.Next(-14, 15)), new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f + 2f), ModContent.ProjectileType<JunglePetal>(), (int)(Projectile.damage * 0.35f), Projectile.knockBack * 0.1f, Projectile.owner);
		}
	}
}
