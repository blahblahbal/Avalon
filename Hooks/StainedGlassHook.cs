using Terraria;
using Terraria.Graphics.Light;
using Terraria.ModLoader;
using Avalon.Common;
using System;
using Microsoft.Xna.Framework;

namespace Avalon.Hooks
{
    internal class StainedGlassHook : ModHook
    {
        protected override void Apply()
        {
            On_TileLightScanner.ApplySurfaceLight += On_TileLightScanner_ApplySurfaceLight;
            On_TileLightScanner.ApplyHellLight += On_TileLightScanner_ApplyHellLight;
        }

        private void On_TileLightScanner_ApplyHellLight(On_TileLightScanner.orig_ApplyHellLight orig, TileLightScanner self, Tile tile, int x, int y, ref Vector3 lightColor)
        {
            orig.Invoke(self, tile, x, y, ref lightColor);
            float finalR = 0f;
            float finalG = 0f;
            float finalB = 0f;
            float num4 = 0.55f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.08f;
            if ((!tile.HasTile || tile.IsHalfBlock || !Main.tileNoSunLight[tile.TileType]) &&
               (tile.WallType == ModContent.WallType<Walls.BrownStainedGlass>() ||
               tile.WallType == ModContent.WallType<Walls.LimeStainedGlass>() ||
               tile.WallType == ModContent.WallType<Walls.CyanStainedGlass>() ||
				(ExxoAvalonOrigins.ThoriumContentEnabled && tile.WallType == ModContent.WallType<Compatability.Thorium.Walls.ChartreuseStainedGlass>())) && tile.LiquidAmount < 255)
            {
                finalR = num4;
                finalG = num4 * 0.6f;
                finalB = num4 * 0.2f;
                if (tile.WallType == ModContent.WallType<Walls.BrownStainedGlass>())
                {
                    finalR *= 1.1f;
                    finalG *= 0.75f;
                    finalB *= 0.5f;
                }
                else if (tile.WallType == ModContent.WallType<Walls.LimeStainedGlass>())
                {
                    finalR *= 0.714f;
                    finalG *= 1f;
                    finalB *= 0f;
                }
                else if (tile.WallType == ModContent.WallType<Walls.CyanStainedGlass>())
                {
                    finalR *= 0f;
                    finalG *= 1f;
                    finalB *= 1f;
                }
                else if (tile.WallType == ModContent.WallType<Compatability.Thorium.Walls.ChartreuseStainedGlass>())
                {
                    finalR *= 0.745f;
                    finalG *= 0.925f;
                    finalB *= 0.1f;
                }
            }
            if (lightColor.X < finalR)
            {
                lightColor.X = finalR;
            }
            if (lightColor.Y < finalG)
            {
                lightColor.Y = finalG;
            }
            if (lightColor.Z < finalB)
            {
                lightColor.Z = finalB;
            }
        }

        private void On_TileLightScanner_ApplySurfaceLight(On_TileLightScanner.orig_ApplySurfaceLight orig, TileLightScanner self, Tile tile, int x, int y, ref Vector3 lightColor)
        {
			orig.Invoke(self, tile, x, y, ref lightColor);
			float finalR = 0f;
			float finalG = 0f;
			float finalB = 0f;
			float num6 = Main.tileColor.R / 255f;
			float num7 = Main.tileColor.G / 255f;
			float num8 = Main.tileColor.B / 255f;
			if ((!tile.HasTile || tile.IsHalfBlock || !Main.tileNoSunLight[tile.TileType]) &&
				(tile.WallType == ModContent.WallType<Walls.BrownStainedGlass>() ||
				tile.WallType == ModContent.WallType<Walls.LimeStainedGlass>() ||
				tile.WallType == ModContent.WallType<Walls.CyanStainedGlass>() ||
				(ExxoAvalonOrigins.ThoriumContentEnabled && tile.WallType == ModContent.WallType<Compatability.Thorium.Walls.ChartreuseStainedGlass>())) && tile.LiquidAmount < 255)
			{
				finalR = num6;
				finalG = num7;
				finalB = num8;
				if (tile.WallType == ModContent.WallType<Walls.BrownStainedGlass>())
				{
					finalR *= 1.1f;
					finalG *= 0.75f;
					finalB *= 0.5f;
				}
				else if (tile.WallType == ModContent.WallType<Walls.LimeStainedGlass>())
				{
					finalR *= 0.714f;
					finalG *= 1f;
					finalB *= 0f;
				}
				else if (tile.WallType == ModContent.WallType<Walls.CyanStainedGlass>())
				{
					finalR *= 0f;
					finalG *= 1f;
					finalB *= 1f;
				}
				else if (tile.WallType == ModContent.WallType<Compatability.Thorium.Walls.ChartreuseStainedGlass>())
				{
					finalR *= 0.745f;
					finalG *= 0.925f;
					finalB *= 0.1f;
				}
			}
			float num3 = 1f - Main.shimmerDarken;
			finalR *= num3;
			finalG *= num3;
			finalB *= num3;
			if (lightColor.X < finalR)
			{
				lightColor.X = finalR;
			}
			if (lightColor.Y < finalG)
			{
				lightColor.Y = finalG;
			}
			if (lightColor.Z < finalB)
			{
				lightColor.Z = finalB;
			}
		}
    }
}
