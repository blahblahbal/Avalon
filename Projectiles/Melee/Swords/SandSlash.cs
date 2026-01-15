using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Swords;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class SandSlash : EnergySlashTemplate
{
	public override string Texture => ModContent.GetInstance<DesertLongsword>().Texture;
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.penetrate = 3;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
	public override void AI()
	{
		for (int i = 0; i < 2; i++)
		{
			float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
			Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
			Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
			Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), DustID.Sand, vector3 * 3f, 0, default, 1f);
			dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.5f;
			dust2.noGravity = true;
			dust2.velocity.X += Main.player[Projectile.owner].direction * Main.rand.NextFloat(2, 5);
			dust2.alpha = Main.rand.Next(0, 128);
		}
		//Projectile.ai[2] += 0.02f;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Main.rand.NextBool(7))
			target.AddBuff(BuffID.Confused, 120);
	}
}
