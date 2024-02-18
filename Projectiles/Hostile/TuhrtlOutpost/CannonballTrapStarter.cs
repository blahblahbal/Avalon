using Avalon.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.TuhrtlOutpost;

public class CannonballTrapStarter : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.alpha = 128;
        Projectile.friendly = true;
        Projectile.timeLeft = 21;
        Projectile.ignoreWater = true;
        Projectile.hostile = false;
        Projectile.scale = 0.4f;
        Projectile.extraUpdates = 1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }

    public override void AI()
    {
        Projectile.ai[0]++;
        if (Projectile.ai[0] == 1)
        {
            Vector2 vel = Vector2.Zero;
            if (Projectile.ai[1] == 0)
            {
                vel.X = -8f;
            }
            if (Projectile.ai[1] == 1)
            {
                vel.X = 8f;
            }
            if (Projectile.ai[1] == 2)
            {
                vel.Y = -8f;
            }
            if (Projectile.ai[1] == 3)
            {
                vel.Y = 8f;
            }
            SoundEngine.PlaySound(SoundID.Item11, Projectile.position);
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, vel, ModContent.ProjectileType<CannonballTrap>(), 72, Projectile.knockBack, Main.myPlayer);
        }
    }
}
