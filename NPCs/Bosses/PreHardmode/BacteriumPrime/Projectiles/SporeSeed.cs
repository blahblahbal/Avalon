using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode.BacteriumPrime.Projectiles;

public class SporeSeed : ModProjectile
{
    public override void SetStaticDefaults()
    {
    }
    public override void SetDefaults()
    {
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.alpha = 128;
        Projectile.friendly = false;
        Projectile.timeLeft = 720;
        Projectile.ignoreWater = true;
        Projectile.hostile = true;
        Projectile.scale = 1f;
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, 0, 0, 128, default, 2);
        Main.dust[d].noGravity = true;
        //Main.NewText(Projectile.ai[0],Main.DiscoColor);
        if (Projectile.Center.X >= Projectile.ai[0] - 10 && Projectile.Center.X <= Projectile.ai[0] + 10)
        {
            Projectile.ai[2] = 1;
        }
        if (Projectile.velocity.Y < 12 && Projectile.ai[2] != 0)
        {
            Projectile.velocity.Y += 0.1f;
            Projectile.velocity.X *= 0.95f;
        }
        if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height) && Projectile.velocity.Y >= 0 && Projectile.ai[2] != 0)
        {
            Projectile.tileCollide = true;
        }
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = 1;
        return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.Center);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Bottom - new Vector2(0, 29), Vector2.Zero, ModContent.ProjectileType<MushroomWall>(), Projectile.damage * 2, 0, 255);
    }
}
