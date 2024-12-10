using Avalon.Compatability.Thorium.Dusts;
using Avalon.Dusts;
using Avalon.Logic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Projectiles.Magic;
public class OsmiumBolt : ModProjectile
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override void SetDefaults()
    {
        Projectile.width = Projectile.height = 10;
        Projectile.alpha = 255;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.friendly = true;
        Projectile.aiStyle = -1;
		Projectile.extraUpdates = 4;
		Projectile.timeLeft = 627;
    }

    public override void AI()
    {
		Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemSapphire, Vector2.Zero);
		d.noGravity = true;
		d.scale += Projectile.ai[1] * 0.02f;

		Projectile.ai[1] += 0.07f;
		Projectile.position += Vector2.UnitY.RotatedBy(Projectile.velocity.ToRotation()) * MathF.Sin(Projectile.ai[1] - MathHelper.PiOver2) * Projectile.ai[0] * MathHelper.Clamp(Projectile.ai[1] * 0.1f,0,8);

	}

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		for(int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemSapphire, Main.rand.NextVector2Circular(4,4) + Projectile.velocity);
			d.noGravity = true;
			d.scale = 1.5f;
		}
    }
}
