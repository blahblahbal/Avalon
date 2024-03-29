using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.Projectiles.Magic.SackofToys;

public class Die : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 2;
    }
    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = 1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = -1;
        Projectile.alpha = 0;
        Projectile.friendly = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        Projectile.ai[2]++;
        if (Projectile.ai[2] >= 4f)
        {
            Projectile.position += Projectile.velocity;
            Projectile.Kill();
        }
        else
        {
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
        }
        return false;
    }

    public override void OnSpawn(IEntitySource source)
    {
        switch (Main.rand.Next(2))
        {
            case 0:
                Projectile.frame = 0;
                break;
            case 1:
                Projectile.frame = 1;
                break;

        }
    }

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation();
    }
}
