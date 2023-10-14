using System;
using Avalon.Dusts;
using Avalon.WorldGeneration.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class ContagionSpray : ModProjectile
{
    private int Progress
    {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    public override void SetDefaults()
    {
        Projectile.DefaultToSpray();
        Projectile.aiStyle = 0;
    }

    public override void AI()
    {
        var dustType = ModContent.DustType<YellowSolutionDust>();

        if (Projectile.owner == Main.myPlayer)
        {
            ConversionHelper.ConvertToContagion((int)(Projectile.position.X + Projectile.width * 0.5f) / 16, (int)(Projectile.position.Y + Projectile.height * 0.5f) / 16, 2);
        }

        Projectile.timeLeft = Math.Min(Projectile.timeLeft, 133);

        if (Progress > 7)
        {
            var dustScale = Progress switch{
                8 => 0.2f,
                9 => 0.4f,
                10 => 0.6f,
                11 => 0.8f,
                _ => 1f
            };

            Progress++;

            var dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);

            dust.noGravity = true;
            dust.scale *= 1.75f;
            dust.velocity.X *= 2f;
            dust.velocity.Y *= 2f;
            dust.scale *= dustScale;
        }
        else
        {
            Progress++;
        }

        Projectile.rotation += 0.3f * Projectile.direction;
    }
}
