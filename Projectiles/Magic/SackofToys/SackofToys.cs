using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic.SackofToys;

public class SackofToys : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = 1;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = -1;
        Projectile.alpha = 0;
        Projectile.friendly = true;
    }
    
    public override void AI()
    {
        if (Main.player[Projectile.owner].channel)
        {
            Projectile.timeLeft = 2;
        }
        Projectile.position = Main.player[Projectile.owner].position;
    }
}
