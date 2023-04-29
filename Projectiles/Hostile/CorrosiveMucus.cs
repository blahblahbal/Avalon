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
        int D = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, Projectile.velocity.X, Projectile.velocity.Y);
        Main.dust[D].noGravity = true;
        Main.dust[D].scale = 1.5f;
        Main.dust[D].alpha = 128;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.Poisoned, 5 * 60);
        target.AddBuff(BuffID.Slow, 10 * 60);
    }
    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        fallThrough = false;
        return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.NPCHit1, Projectile.Center);
        Projectile.timeLeft = 16 * 60;
        Projectile.Size *= 2f;
        Projectile.velocity *= 0;
        Projectile.tileCollide = false;
        return false;
    }
}
