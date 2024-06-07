using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class SpiritPoppy : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.netImportant = true;
        Projectile.width = 14;
        Projectile.height = 14;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 10000;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
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
        //projectile.Kill();
        return false;
    }
    public override void AI()
    {
		Lighting.AddLight(Projectile.position, new Color(0, 233, 255).ToVector3());
        if (Projectile.velocity.Y == 0f)
        {
            Projectile.velocity.X = Projectile.velocity.X * 0.99f;
        }
        Projectile.rotation += Projectile.velocity.X * 0.1f;
        //projectile.velocity.Y = projectile.velocity.Y + 0.2f;
        Projectile.ai[0]++;
        if (Projectile.ai[0] >= 50 && Projectile.ai[0] <= 100)
        {
            Projectile.velocity.Y *= 0.6f;
        }
        if (Projectile.ai[0] == 101)
        {
            Projectile.velocity.Y *= -1;
        }
        if (Projectile.ai[0] > 101)
        {
            Projectile.velocity.Y += 0.2f;
            if (Projectile.velocity.Y > 4) Projectile.velocity.Y = 4;
        }


        int x = (int)(Projectile.position.X / 16);
        int y = (int)(Projectile.position.Y / 16);

        if (!Main.tile[x, y].HasTile)
        {
            Projectile.tileCollide = true;
        }

        if (Projectile.owner == Main.myPlayer)
        {
            int xpos = (int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f);
            int ypos = (int)((Projectile.position.Y + (float)Projectile.height - 4f) / 16f);
            if (Main.tile[xpos, ypos] != null && !Main.tile[xpos, ypos].HasTile)
            {
                if (Main.tile[xpos, ypos].TileType != 21 || Main.tile[xpos + 1, ypos].TileType != 21 || Main.tile[xpos - 1, ypos].TileType != 21)
                {
                    if (Main.tileSolid[Main.tile[xpos + 1, ypos].TileType] && Main.tileSolid[Main.tile[xpos - 1, ypos].TileType])
                    {
                        WorldGen.KillTile(xpos, ypos);
                        WorldGen.KillTile(xpos + 1, ypos);
                        WorldGen.KillTile(xpos - 1, ypos);

                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, NetworkText.Empty, 1, xpos, ypos, 0);
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, NetworkText.Empty, 1, xpos + 1, ypos, 0);
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, NetworkText.Empty, 1, xpos - 1, ypos, 0);
                        }
                    }
                }
                WorldGen.PlaceTile(xpos, ypos, ModContent.TileType<Tiles.SpiritPoppy>(), false, true);

                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, NetworkText.Empty, 1, xpos, ypos, ModContent.TileType<Tiles.SpiritPoppy>(), 0);
                }

                if (Main.tile[xpos, ypos].HasTile)
                {
                    Projectile.Kill();
                }
            }
        }
    }
}
