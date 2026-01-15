using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Avalon.Projectiles.Melee.Misc;

public class HellboundHalberdSpear : SpearTemplate
{
	protected override float HoldoutRangeMax => 230;
	protected override float HoldoutRangeMin => 40;
	public override bool PreDraw(ref Color lightColor)
	{
		return base.PreDraw(ref lightColor);
	}
	public override void AI()
	{
		if (Projectile.Owner().direction == -1)
		{
			Projectile.Owner().SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * Projectile.Owner().gravDir + MathHelper.Pi + MathHelper.PiOver2);
		}
		else
		{
			Projectile.Owner().SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * Projectile.Owner().gravDir + (Projectile.Owner().gravDir == -1 ? MathHelper.Pi : 0));
		}
		base.AI();
	}
	public override void PostAI()
	{
		Projectile.scale = 1.35f;
		if (!Main.rand.NextBool(4))
		{
			int S = Dust.NewDust(Projectile.position + Main.rand.NextVector2Circular(10, 10), Projectile.width, Projectile.height, DustID.Torch);
			Main.dust[S].noGravity = true;
			Main.dust[S].velocity = Projectile.velocity * 2;
			Main.dust[S].fadeIn = Main.rand.NextFloat(0, 1.5f);
			int H = Dust.NewDust(Projectile.position + Main.rand.NextVector2Circular(10, 10), Projectile.width, Projectile.height, DustID.SolarFlare);
			Main.dust[H].noGravity = true;
			Main.dust[H].velocity = Projectile.velocity * -3;
			Main.dust[H].fadeIn = Main.rand.NextFloat(0, 1.5f);
		}
		if (Main.rand.NextBool(3))
		{
			int SSmall = Dust.NewDust(Projectile.position + Main.rand.NextVector2Circular(25, 25), Projectile.width, Projectile.height, DustID.Torch);
			Main.dust[SSmall].noGravity = true;
			Main.dust[SSmall].velocity = Projectile.oldVelocity * 2;
			Main.dust[SSmall].fadeIn = Main.rand.NextFloat(0, 0.7f);
			Main.dust[SSmall].scale = 0.7f;
			int HSmall = Dust.NewDust(Projectile.position + Main.rand.NextVector2Circular(25, 25), Projectile.width, Projectile.height, DustID.SolarFlare);
			Main.dust[HSmall].noGravity = true;
			Main.dust[HSmall].velocity = Projectile.velocity * -3;
			Main.dust[HSmall].fadeIn = Main.rand.NextFloat(0, 0.7f);
			Main.dust[HSmall].scale = 0.7f;
		}
	}
}

