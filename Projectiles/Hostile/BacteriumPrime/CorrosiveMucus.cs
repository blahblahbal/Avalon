using Avalon.NPCs.Bosses.PreHardmode;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.BacteriumPrime;

public class CorrosiveMucus : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.BulletDeadeye);
        Projectile.hide = true;
        Projectile.aiStyle = -1;
        Projectile.Size = new Vector2(16);
        Projectile.light = 0;
        Projectile.tileCollide = false;
    }
    public override void AI()
    {
        if (Projectile.tileCollide)
            Projectile.velocity.Y += 0.05f;

        if (Projectile.ai[1] == 0)
        {
            int D2 = Dust.NewDust(Projectile.Center + new Vector2(0, -4), 0, 0, DustID.PirateStaff, Projectile.velocity.X, Projectile.velocity.Y);
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
        if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
        {
            Projectile.tileCollide = true;
        }
        //Lighting.AddLight(Projectile.Center, 0.3f, 0.35f, 0f);
        Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0.2f);
        bool KillFast = true;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NPCs.Bosses.PreHardmode.BacteriumPrime>())
            {
                KillFast = false;
            }
        }
        if (KillFast)
        {
            Projectile.timeLeft -= 120;
        }
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
        Projectile.timeLeft = 60 * 10;
        if (Main.expertMode)
            Projectile.timeLeft += 30 * 60;
        Projectile.Resize(32, 24);
        Projectile.velocity *= 0;
        Projectile.tileCollide = false;
        Point p = Projectile.Center.ToTileCoordinates();

        //MakeBoogers(p.X, p.Y, 2);
        return false;
    }
    public static void MakeBoogers(int x, int y, int radius)
    {
        int xmin = (int)(x - radius);
        int ymin = (int)(y - radius);
        int xmax = (int)(x + radius);
        int ymax = (int)(y + radius);

        for (int i = xmin; i <= xmax; i++)
        {
            for (int j = ymin; j <= ymax; j++)
            {
                if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) <= radius && !Main.tile[i, j].HasTile)
                {
                    WorldGen.PlaceTile(i, j, ModContent.TileType<Booger>());
                }
                if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) <= radius + 1 && WorldGen.genRand.NextBool(4) && !Main.tile[i, j].HasTile)
                {
                    WorldGen.PlaceTile(i, j, ModContent.TileType<Booger>());
                }
            }
        }
    }
}
