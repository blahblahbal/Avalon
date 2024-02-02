using Avalon.Data.Sets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class ChaosBolt : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(16);
        Projectile.aiStyle = -1;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.timeLeft = 540;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.ignoreWater = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        //Projectile.timeLeft -= 20;
        //Projectile.velocity = oldVelocity;
        return false;
    }
    public float opacityMult = 1f;
    public override void AI()
    {
        //Lighting.AddLight(Projectile.position, 150 / 255f, 0, 100 / 255f);
        int dustAmount = 4;

        if (Projectile.ai[1] >= 20f)
        {
            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
        }
        Projectile.rotation += 0.3f * Projectile.direction;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
            return;
        }
        if (Main.tile[Projectile.Center.ToTileCoordinates()].HasTile)
        {
            Projectile.ai[2]++;
            dustAmount = 1;
            opacityMult = 0.5f;
            if (Projectile.ai[2] > 30)
            {
                Projectile.timeLeft--;
            }
            if (Projectile.ai[2] == 120)
            {
                Projectile.timeLeft = 0;
            }
        }
        else
        {
            Projectile.ai[2] = 0;
            opacityMult = 1f;
        }
        for (int num127 = 0; num127 < dustAmount; num127++)
        {
            int num128 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1.3f);
            Main.dust[num128].noGravity = true;
            Main.dust[num128].position += new Vector2(0, -4) + (Vector2.Normalize(Projectile.velocity) * 6);
            Dust dust = Main.dust[num128];
            dust.velocity *= 0.3f;
            Main.dust[num128].velocity.X -= Projectile.velocity.X * 0.2f;
            Main.dust[num128].velocity.Y -= Projectile.velocity.Y * 0.2f;
        }
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(250, 250, 250, 100) * opacityMult;
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 10; i++)
        {
            Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, Main.rand.NextVector2Circular(1.7f, 1.7f).RotatedBy(i), 70, default, 0.6f);
        }
        for (int i = 0; i < 5; i++)
        {
            Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, Main.rand.NextVector2Circular(1.3f, 1.3f).RotatedBy(i + 0.3f), 100, default, 0.9f);
        }
    }
}
