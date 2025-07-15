using Avalon.Common;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Light;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	internal class StainedGlassHook : ModHook
	{
		protected override void Apply()
		{
			On_TileLightScanner.ApplySurfaceLight += On_TileLightScanner_ApplySurfaceLight;
			On_TileLightScanner.ApplyHellLight += On_TileLightScanner_ApplyHellLight;
		}

		private static ushort BrownSG;
		private static ushort LimeSG;
		private static ushort CyanSG;
		private static ushort ChartreuseSG;

		public override void SetStaticDefaults()
		{
			// Reduces lag, trust me bro
			BrownSG = (ushort)ModContent.WallType<Walls.BrownStainedGlass>();
			LimeSG = (ushort)ModContent.WallType<Walls.LimeStainedGlass>();
			CyanSG = (ushort)ModContent.WallType<Walls.CyanStainedGlass>();
			if (ExxoAvalonOrigins.ThoriumContentEnabled)
			{
				ChartreuseSG = (ushort)ModContent.WallType<ModSupport.Thorium.Walls.ChartreuseStainedGlass>();
			}
		}

		private void On_TileLightScanner_ApplyHellLight(On_TileLightScanner.orig_ApplyHellLight orig, TileLightScanner self, Tile tile, int x, int y, ref Vector3 lightColor)
		{
			orig.Invoke(self, tile, x, y, ref lightColor);

			float num4 = 0.55f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.08f;
			float finalR = num4;
			float finalG = num4 * 0.6f;
			float finalB = num4 * 0.2f;

			if (CombinedGlassHooks(tile, ref finalR, ref finalG, ref finalB))
			{
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

		private static bool CombinedGlassHooks(Tile tile, ref float finalR, ref float finalG, ref float finalB)
		{
			if ((!tile.HasTile || tile.IsHalfBlock || !Main.tileNoSunLight[tile.TileType]) && tile.LiquidAmount < 255)
			{
				if (tile.WallType == BrownSG)
				{
					finalR *= 1.1f;
					finalG *= 0.75f;
					finalB *= 0.5f;
					return true;
				}
				else if (tile.WallType == LimeSG)
				{
					finalR *= 0.714f;
					finalG *= 1f;
					finalB *= 0f;
					return true;
				}
				else if (tile.WallType == CyanSG)
				{
					finalR *= 0f;
					finalG *= 1f;
					finalB *= 1f;
					return true;
				}
				else if (ExxoAvalonOrigins.ThoriumContentEnabled && tile.WallType == ChartreuseSG)
				{
					finalR *= 0.745f;
					finalG *= 0.925f;
					finalB *= 0.1f;
					return true;
				}
			}
			return false;
		}

		private void On_TileLightScanner_ApplySurfaceLight(On_TileLightScanner.orig_ApplySurfaceLight orig, TileLightScanner self, Tile tile, int x, int y, ref Vector3 lightColor)
		{
			orig.Invoke(self, tile, x, y, ref lightColor);
			float finalR = Main.tileColor.R / 255f;
			float finalG = Main.tileColor.G / 255f;
			float finalB = Main.tileColor.B / 255f;

			if (CombinedGlassHooks(tile, ref finalR, ref finalG, ref finalB))
			{
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
}
