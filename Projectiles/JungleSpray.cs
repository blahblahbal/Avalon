using System;
using Avalon.WorldGeneration.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class JungleSpray : ModProjectile
{
    private int Progress
    {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.WindPhysicsImmunity[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.DefaultToSpray();
        Projectile.aiStyle = 0;
    }

    public override bool? CanDamage()
    {
        return false;
    }
    
    public override void AI()
    {
        var largerSize = (int)Projectile.ai[1] == 1;
        var dustType = ModContent.DustType<Dusts.JungleSpray>();

        if (Projectile.owner == Main.myPlayer)
        {
            var size = largerSize ? 3 : 2;
            var point = Projectile.Center.ToTileCoordinates();
            ConversionHelper.ConvertToJungle(point.X, point.Y, size);
        }

        Projectile.timeLeft = Math.Min(Projectile.timeLeft, 133);
        var progressThreshold = largerSize ? 3 : 7;

        if (Progress > progressThreshold)
        {
            var dustScale = (Progress - progressThreshold) switch{
                1 => 0.2f,
                2 => 0.4f,
                3 => 0.6f,
                4 => 0.8f,
                _ => 1f
            };

            var dustOffset = 0;
            if (largerSize)
            {
                dustScale *= 1.2f;
                dustOffset = (int)(12 * dustScale);
            }

            Progress++;

            var dust = Dust.NewDustDirect(new Vector2(Projectile.position.X - dustOffset, Projectile.position.Y - dustOffset), Projectile.width + dustOffset * 2, Projectile.height + dustOffset * 2, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);

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
