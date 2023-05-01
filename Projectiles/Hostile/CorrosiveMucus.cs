using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile; 

public class CorrosiveMucus : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.BulletDeadeye);
        Projectile.hide = true;
        Projectile.aiStyle = -1;
        Projectile.Size = new Vector2(16);
        Projectile.light = 0;
    }
    public override void AI()
    {
        if(Projectile.tileCollide)
        Projectile.velocity.Y += 0.05f;

        if (Projectile.ai[1] == 0)
        {
            int D2 = Dust.NewDust(Projectile.Center + new Vector2(0,-4), 0, 0, DustID.PirateStaff, Projectile.velocity.X, Projectile.velocity.Y);
            Main.dust[D2].noGravity = true;
            Main.dust[D2].velocity = Projectile.velocity * 0.5f;
            Main.dust[D2].scale = 1.5f;
            Main.dust[D2].alpha = 128;
        }
        else
        {
            int D = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PirateStaff, Projectile.velocity.X, Projectile.velocity.Y);
            Main.dust[D].noGravity = !Main.rand.NextBool(20);
            Main.dust[D].scale = 1.5f;
            Main.dust[D].alpha = 128;
        }
        //Lighting.AddLight(Projectile.Center, 0.3f, 0.35f, 0f);
        Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0.2f);
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.Poisoned, 5 * 60);
        target.AddBuff(BuffID.Slow, 3 * 60);
    }
    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        fallThrough = false;
        return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.damage /= 2;
        Projectile.ai[1]++;
        SoundEngine.PlaySound(SoundID.NPCHit1, Projectile.Center);
        Projectile.timeLeft = 45 * 60;
        if (Main.expertMode)
            Projectile.timeLeft += 25 * 60;
        Projectile.Resize(32, 24);
        Projectile.velocity *= 0;
        Projectile.tileCollide = false;
        return false;
    }
}
