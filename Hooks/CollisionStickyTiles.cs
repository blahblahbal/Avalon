using Terraria;
using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using Terraria.ID;

namespace Avalon.Hooks
{
    internal class CollisionStickyTiles : ModHook
    {
        protected override void Apply()
        {
            On_Collision.StickyTiles += On_Collision_StickyTiles;
        }

        private Vector2 On_Collision_StickyTiles(On_Collision.orig_StickyTiles orig, Vector2 Position, Vector2 Velocity, int Width, int Height)
        {
            if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().NoSticky)
            {
                return new Vector2(-1f, -1f);
            }
            Vector2 vector = Position;
            int num = (int)(Position.X / 16f) - 1;
            int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num3 = (int)(Position.Y / 16f) - 1;
            int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
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
            Vector2 vector2 = default(Vector2);
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    if (Main.tile[i, j].TileType == ModContent.TileType<Tiles.Tropics.Bramble>())
                    {
                        int num5 = 0;
                        vector2.X = i * 16;
                        vector2.Y = j * 16;
                        if (vector.X + (float)Width > vector2.X - (float)num5 && vector.X < vector2.X + 16f + (float)num5 && vector.Y + (float)Height > vector2.Y && (double)vector.Y < (double)vector2.Y + 16.01)
                        {
                            return new Vector2(i, j);
                        }
                    }
                }
            }
            return orig(Position, Velocity, Width, Height);
        }
    }
}
