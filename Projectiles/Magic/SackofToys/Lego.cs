using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.Projectiles.Magic.SackofToys;

public class Lego : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 3;
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
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = -oldVelocity.X;
        }
        if (Projectile.velocity.Y != oldVelocity.Y)
        {
            Projectile.velocity.Y = 0f;
        }
        return false;
    }

    public override void OnSpawn(IEntitySource source)
    {
        switch (Main.rand.Next(3))
        {
            case 0:
                Projectile.frame = 0;
                break;
            case 1:
                Projectile.frame = 1;
                break;
            case 2:
                Projectile.frame = 2;
                break;

        }
    }

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation();
    }
}
