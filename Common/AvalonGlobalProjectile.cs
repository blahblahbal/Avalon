using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common; 

internal class AvalonGlobalProjectile : GlobalProjectile
{
    public override bool PreAI(Projectile projectile)
    {
        if (projectile.type == ProjectileID.TerraBlade2 && projectile.localAI[0] == 0)
        {
            projectile.localAI[0] = 1;
            SoundEngine.PlaySound(SoundID.Item8, projectile.position);
            return true;
        }
        return base.PreAI(projectile);
    }
    public override void AI(Projectile projectile)
    {
        if (projectile.type == ProjectileID.TerraBlade2)
        {
            if (projectile.localAI[0] == 1)
            {
                projectile.localAI[0] = 2;
                SoundEngine.PlaySound(SoundID.Item8, projectile.position);
            }
        }
    }
}
