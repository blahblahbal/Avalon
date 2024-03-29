using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class RhotukaSpinnerScrambler : ModProjectile
{

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 4;
    }
    public override void SetDefaults()
    {
        Projectile.arrow = true;
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 10 / 32;
        Projectile.height = dims.Height * 10 / 32 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
    }

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation();
        if (Projectile.velocity.X < 0)
            Projectile.rotation += MathHelper.Pi;

        Projectile.frameCounter += 1;
        if(Projectile.frameCounter >= 3)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if(Projectile.frame > 3)
        {
            Projectile.frame = 0;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!target.boss)
        {
            if (!target.HasBuff(ModContent.BuffType<Scrambled>()))
            {
                AvalonGlobalNPC.ScrambleStats(target);
                target.AddBuff(ModContent.BuffType<Scrambled>(), 60 * 60);
            }
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        return true;
    }
}
