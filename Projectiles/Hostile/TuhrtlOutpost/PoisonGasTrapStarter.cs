using Avalon.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.TuhrtlOutpost;

public class PoisonGasTrapStarter : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.alpha = 128;
        Projectile.friendly = true;
        Projectile.timeLeft = 240;
        Projectile.ignoreWater = true;
        Projectile.hostile = false;
        Projectile.scale = 0.4f;
        Projectile.extraUpdates = 1;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }

    public override void AI()
    {
        Projectile.ai[0]++;
        if (Projectile.ai[0] % 20 == 0)
        {
            Vector2 vel = Vector2.Zero;
            if (Projectile.ai[1] == 0)
            {
                vel.X = -6f;
            }
            if (Projectile.ai[1] == 1)
            {
                vel.X = 6f;
            }
            if (Projectile.ai[1] == 2)
            {
                vel.Y = -6f;
            }
            if (Projectile.ai[1] == 3)
            {
                vel.Y = 6f;
            }
            SoundEngine.PlaySound(SoundID.Item34, Projectile.position);
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, vel, ModContent.ProjectileType<PoisonGasTrap>(), 53, Projectile.knockBack, Main.myPlayer);
        }
    }
}
