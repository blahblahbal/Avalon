using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic.SackofToys;

public class Table : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 80;
        Projectile.height = 96;
        Projectile.aiStyle = 1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = -1;
        Projectile.alpha = 0;
        Projectile.friendly = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = -oldVelocity.X * 0.7f;
        }
        if (Projectile.velocity.Y != oldVelocity.Y)
        {
            Projectile.velocity.Y = -oldVelocity.Y * 0.7f;
        }
        return false;
    }
    
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() / 2;
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 5)
        {
            Projectile.velocity.Y += 0.3f;
            Projectile.ai[0] = 0;
        }
    }
}
