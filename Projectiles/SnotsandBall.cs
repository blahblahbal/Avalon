using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class SnotsandBall : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.knockBack = 6f;
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.friendly = true;
        Projectile.hostile = true;
        Projectile.penetrate = -1;
        Projectile.aiStyle = -1;
    }

    public override void AI()
    {
        if (Main.rand.NextBool(2))
        {
            int i = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.SnotsandDust>(), 0f, Projectile.velocity.Y * 0.5f);
            Main.dust[i].velocity.X *= 0.2f;
        }
        Projectile.velocity.Y += 0.2f;
        Projectile.rotation += 0.1f;
        if (Projectile.velocity.Y > 10f)
        {
            Projectile.velocity.Y = 10f;
        }
        base.AI();
    }

    public override void OnKill(int timeLeft)
    {
        Point p = Projectile.Center.ToTileCoordinates();
        if (p.X >= 0 && p.X < Main.maxTilesX && p.Y >= 0 && p.Y < Main.maxTilesY)
        {
            Tile t = Main.tile[p.X, p.Y];
            if (t.IsHalfBlock && Projectile.velocity.Y > 0f && Math.Abs(Projectile.velocity.Y) > Math.Abs(Projectile.velocity.X))
            {
                t = Main.tile[p.X, --p.Y];
            }
            if (!t.HasTile && t.TileType != TileID.MinecartTrack)
            {
                WorldGen.PlaceTile(p.X, p.Y, ModContent.TileType<Tiles.Contagion.Snotsand>());
                WorldGen.SquareTileFrame(p.X, p.Y);
            }
        }
    }
    public override bool? CanDamage() => Projectile.localAI[1] != -1f;
}
