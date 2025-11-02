using Avalon.Achievements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class ThrowingRock : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.alpha = 0;
        Projectile.scale = 1f;
        Projectile.timeLeft = 600;
        Projectile.tileCollide = true;
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        fallThrough = false;
        return true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = oldVelocity.X * -0.75f;
        }
        if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 1.5)
        {
            Projectile.velocity.Y = oldVelocity.Y * -0.7f;
        }
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        Point tile = Projectile.Center.ToTileCoordinates();
        if (tile.X < 10)
            tile.X = 10;
        if (tile.X > Main.maxTilesX - 10)
            tile.X = Main.maxTilesX - 10;
        if (tile.Y < 10)
            tile.Y = 10;
        if (tile.Y > Main.maxTilesY - 10)
            tile.Y = Main.maxTilesY - 10;
        for (int i = tile.X - 2; i < tile.X + 2; i++)
        {
            for (int j = tile.Y - 2; j < tile.Y + 2; j++)
            {
                if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == TileID.Glass)
                {
                    WorldGen.KillTile(i, j);
					ModContent.GetInstance<Ach_Rock>().ConditionFlag.Complete();
					Projectile.ai[1] = 1;
                }
            }
        }
        return false;
    }
    public override void AI()
    {
        if (Projectile.velocity.Y == 0f)
        {
            Projectile.velocity.X *= 0.94f;
        }
        Projectile.rotation += Projectile.velocity.X * 0.1f;
        Projectile.velocity.Y += 0.2f;
        if (Projectile.ai[1] == 1)
        {
            Projectile.Kill();
        }
    }
}
