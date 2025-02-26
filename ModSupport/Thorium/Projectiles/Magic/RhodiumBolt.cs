using Avalon.ModSupport.Thorium.Dusts;
using Avalon.Dusts;
using Avalon.Logic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.Thorium.Projectiles.Magic;
public class RhodiumBolt : ModProjectile
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override void SetDefaults()
    {
        Projectile.width = Projectile.height = 22;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.friendly = true;
        Projectile.aiStyle = -1;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 200;
		Projectile.penetrate = 2;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 60;
    }
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 8;
		height = 8;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public override void AI()
    {
		Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<RhodiumDust>(), Vector2.Zero);
		d.noGravity = true;
		d.alpha = 128;
		Projectile.ai[1] -= 0.02f;
		Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.ai[0] * 0.004f * Projectile.ai[1]);

		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
	}

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		for(int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<RhodiumDust>(), Main.rand.NextVector2Circular(4,4) + Projectile.velocity);
			d.noGravity = true;
			d.scale = 1.5f;
		}
    }
}
