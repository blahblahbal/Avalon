using Avalon.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.TuhrtlOutpost;

public class FireballTrapStarter : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.alpha = 128;
        Projectile.friendly = true;
        Projectile.timeLeft = 120;
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
        if (Projectile.ai[0] % 30 == 0)
        {
            Vector2 vel = Vector2.Zero;
            if (Projectile.ai[1] == 0)
            {
                vel.X = -6f;
                vel.Y = Main.rand.NextFloat(-3f, 3f);
            }
            if (Projectile.ai[1] == 1)
            {
                vel.X = 6f;
                vel.Y = Main.rand.NextFloat(-3f, 3f);
            }
            if (Projectile.ai[1] == 2)
            {
                vel.X = Main.rand.NextFloat(-3f, 3f);
                vel.Y = -6f;
            }
            if (Projectile.ai[1] == 3)
            {
                vel.X = Main.rand.NextFloat(-3f, 3f);
                vel.Y = 6f;
            }
            SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, vel, ModContent.ProjectileType<FireballTrap>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
        }
        //Lighting.AddLight(Projectile.Center, new Vector3(0.4f, 0.2f, 0.5f) * Projectile.scale * Projectile.Opacity * 0.3f);
    }
}
