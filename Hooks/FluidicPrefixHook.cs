using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Reflection;
using Microsoft.Xna.Framework;
using Terraria;

namespace Avalon.Hooks
{
    internal class FluidicPrefixHook : ModHook
    {
        protected override void Apply()
        {
            On_Player.WaterCollision += On_Player_WaterCollision;
            On_Player.HoneyCollision += On_Player_HoneyCollision;
            On_Player.ShimmerCollision += On_Player_ShimmerCollision;
        }

        private void On_Player_ShimmerCollision(On_Player.orig_ShimmerCollision orig, Player self, bool fallThrough, bool ignorePlats, bool noCollision)
        {
            int num = ((!self.onTrack) ? self.height : (self.height - 20));
            Vector2 vector = self.velocity;
            if (!noCollision)
            {
                self.velocity = Collision.TileCollision(self.position, self.velocity, self.width, num, fallThrough, ignorePlats, (int)self.gravDir);
            }
            Vector2 vector2 = self.velocity * (0.375f + self.GetModPlayer<AvalonPlayer>().FluidicModifier);
            if (self.velocity.X != vector.X)
            {
                vector2.X = self.velocity.X;
            }
            if (self.velocity.Y != vector.Y)
            {
                vector2.Y = self.velocity.Y;
            }
            self.position += vector2;
            if (self.shimmerImmune && !noCollision)
            {
                PlayerHelper.TryFloatingInFluid(self);
            }
        }

        private void On_Player_HoneyCollision(On_Player.orig_HoneyCollision orig, Player self, bool fallThrough, bool ignorePlats)
        {
            int num = ((!self.onTrack) ? self.height : (self.height - 20));
            Vector2 vector = self.velocity;
            self.velocity = Collision.TileCollision(self.position, self.velocity, self.width, num, fallThrough, ignorePlats, (int)self.gravDir);
            Vector2 vector2 = self.velocity * (0.25f + self.GetModPlayer<AvalonPlayer>().FluidicModifier);
            if (self.velocity.X != vector.X)
            {
                vector2.X = self.velocity.X;
            }
            if (self.velocity.Y != vector.Y)
            {
                vector2.Y = self.velocity.Y;
            }
            self.position += vector2;

            PlayerHelper.TryFloatingInFluid(self);
        }

        private void On_Player_WaterCollision(On_Player.orig_WaterCollision orig, Player self, bool fallThrough, bool ignorePlats)
        {
            int num = ((!self.onTrack) ? self.height : (self.height - 20));
            Vector2 vector = self.velocity;
            self.velocity = Collision.TileCollision(self.position, self.velocity, self.width, num, fallThrough, ignorePlats, (int)self.gravDir);
            Vector2 vector2 = self.velocity * (0.5f + self.GetModPlayer<AvalonPlayer>().FluidicModifier);
            if (self.velocity.X != vector.X)
            {
                vector2.X = self.velocity.X;
            }
            if (self.velocity.Y != vector.Y)
            {
                vector2.Y = self.velocity.Y;
            }
            self.position += vector2;

            PlayerHelper.TryFloatingInFluid(self);
        }
    }
}
