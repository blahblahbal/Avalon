using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class ContagionSpray : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 6 / 16;
        Projectile.height = dims.Height * 6 / 16 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.alpha = 255;
        Projectile.penetrate = -1;
        Projectile.MaxUpdates = 2;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
    }
    public override bool? CanDamage()
    {
        return false;
    }
    public override bool? CanCutTiles()
    {
        return false;
    }
    public override void AI()
    {
        int num500 = ModContent.DustType<Dusts.YellowSolutionDust>();
        if (Projectile.owner == Main.myPlayer)
        {
            AvalonWorld.Convert((int)(Projectile.position.X + Projectile.width / 2) / 16, (int)(Projectile.position.Y + Projectile.height / 2) / 16, 0, 2);
        }
        if (Projectile.timeLeft > 133)
        {
            Projectile.timeLeft = 133;
        }
        if (Projectile.ai[0] > 7f)
        {
            var num502 = 1f;
            if (Projectile.ai[0] == 8f)
            {
                num502 = 0.2f;
            }
            else if (Projectile.ai[0] == 9f)
            {
                num502 = 0.4f;
            }
            else if (Projectile.ai[0] == 10f)
            {
                num502 = 0.6f;
            }
            else if (Projectile.ai[0] == 11f)
            {
                num502 = 0.8f;
            }
            Projectile.ai[0] += 1f;
            for (var num503 = 0; num503 < 1; num503++)
            {
                var num504 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num500, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                Main.dust[num504].noGravity = true;
                Main.dust[num504].scale *= 1.75f;
                var dust57 = Main.dust[num504];
                dust57.velocity.X = dust57.velocity.X * 2f;
                var dust58 = Main.dust[num504];
                dust58.velocity.Y = dust58.velocity.Y * 2f;
                Main.dust[num504].scale *= num502;
            }
        }
        else
        {
            Projectile.ai[0] += 1f;
        }
        Projectile.rotation += 0.3f * Projectile.direction;
    }
}
