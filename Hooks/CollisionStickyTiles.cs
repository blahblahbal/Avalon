using Terraria;
using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;

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
            return orig(Position, Velocity, Width, Height);
        }
    }
}
