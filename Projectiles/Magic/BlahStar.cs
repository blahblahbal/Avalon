using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic;

public class BlahStar : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.aiStyle = 5;
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.tileCollide = true;
        Projectile.penetrate = 5;
        Projectile.hostile = false;
    }
    public override bool PreAI()
    {
        Lighting.AddLight(Projectile.position, 249 / 255, 201 / 255, 77 / 255);
        return true;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int i = 0; i < 2; i++)
        {
            float speedX = Projectile.velocity.X + Main.rand.Next(-51, 51) * 0.2f;
            float speedY = Projectile.velocity.Y + Main.rand.Next(-51, 51) * 0.2f;
            int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(speedX, speedY), ModContent.ProjectileType<BlahFire>(), Projectile.damage, Projectile.knockBack);
            Main.projectile[proj].hostile = false;
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].owner = Projectile.owner;
            Main.projectile[proj].timeLeft = 240;
        }
        Projectile.active = false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.Kill();
        return true;
    }
    public override void AI()
    {
        Projectile.hostile = false;
        Projectile.friendly = true;
        if (Main.rand.NextBool(100))
        {
            for (int i = 0; i < 3; i++)
            {
                int d = Dust.NewDust(Projectile.position, 10, 10, DustID.Torch);
                Main.dust[d].noGravity = true;
            }
        }
    }
}
