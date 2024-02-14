using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Avalon.Logic;

public static class Collision
{
    public static bool TouchingTile(Vector2 position, int width, int height)
    {
        int num = (int)(position.X / 16f) - 1;
        int num2 = (int)((position.X + width) / 16f) + 2;
        int num3 = (int)(position.Y / 16f) - 1;
        int num4 = (int)((position.Y + height) / 16f) + 4;
        if (num < 0)
        {
            num = 0;
        }

        if (num2 > Main.maxTilesX)
        {
            num2 = Main.maxTilesX;
        }

        if (num3 < 0)
        {
            num3 = 0;
        }

        if (num4 > Main.maxTilesY)
        {
            num4 = Main.maxTilesY;
        }

        for (int i = num; i < num2; i++)
        {
            for (int j = num3; j < num4; j++)
            {
                if (Main.tile[i, j] != null && !Main.tile[i, j].IsActuated && Main.tile[i, j].HasTile &&
                    Main.tileSolid[Main.tile[i, j].TileType] && (!Main.tileSolidTop[Main.tile[i, j].TileType] || TileID.Sets.Platforms[Main.tile[i, j].TileType]))
                {
                    Vector2 vector;
                    vector.X = i * 16;
                    vector.Y = j * 16;
                    int num5 = 16;
                    if (Main.tile[i, j].IsHalfBlock)
                    {
                        vector.Y += 8f;
                        num5 -= 8;
                    }

                    if (position.Y + height >= vector.Y)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
