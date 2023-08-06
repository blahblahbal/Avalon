using Avalon.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class PathogenSmoke : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.alpha = 128;
        Projectile.friendly = true;
        Projectile.timeLeft = 720;
        Projectile.ignoreWater = true;
        Projectile.hostile = false;
        Projectile.scale = 0.8f;
        Projectile.extraUpdates = 1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }

    public override void AI()
    {
        Projectile.ai[1]++;
        if (Projectile.ai[2] > 1)
        {
            Projectile.alpha += 1;
            if (Projectile.ai[1] % 10 == 0)
            {
                Projectile.damage--;
            }
        }
        else
            Projectile.alpha -= 3;

        if (Projectile.alpha <= 100)
            Projectile.ai[2]++;

        if (Projectile.alpha == 255) Projectile.Kill();

        Projectile.velocity = Projectile.velocity.RotatedByRandom(0.05f) * 0.99f;
        Projectile.rotation += MathHelper.Clamp(Projectile.velocity.Length() * 0.03f, -0.3f, 0.3f);
        Projectile.scale += 0.001f;
        Projectile.Resize((int)(32 * Projectile.scale), (int)(32 * Projectile.scale));

        //if (Main.rand.NextBool(3))
        //{
        //    int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Venom, 0, 0, (int)(Projectile.alpha * 1.4f), default, 0.5f);
        //    Main.dust[d].velocity *= 0.5f;
        //    Main.dust[d].fadeIn = 2f;
        //    Main.dust[d].noGravity = true;
        //}
        Lighting.AddLight(Projectile.Center, new Vector3(0.4f, 0.2f, 0.5f) * Projectile.scale * Projectile.Opacity * 0.3f);
    }
    public override bool? CanHitNPC(NPC target)
    {
        return (Projectile.alpha < 220 || Projectile.ai[2] < 1) && !target.friendly;
    }

    public override bool CanHitPvp(Player target)
    {
        return Projectile.alpha < 220;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<Pathogen>(), 7 * 60);
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(ModContent.BuffType<Pathogen>(), 7 * 60);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        ClassExtensions.DrawGas(Texture, lightColor * 0.8f, Projectile, 4, 6);
        return false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity = Projectile.oldVelocity * 0.3f;
        return false;
    }
}
