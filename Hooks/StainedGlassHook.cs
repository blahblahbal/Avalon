using Avalon.Common;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Light;
using Terraria.ID;
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
		private static readonly float[]?[] StainedGlassRGBMults = WallID.Sets.Factory.CreateCustomSet<float[]?>(null);

		public override void SetStaticDefaults()
		{
			// Reduces lag, trust me bro
			StainedGlassRGBMults[ModContent.WallType<Walls.BrownStainedGlass>()] = [1.1f, 0.75f, 0.5f];
			StainedGlassRGBMults[ModContent.WallType<Walls.LimeStainedGlass>()] = [0.714f, 1f, 0f];
			StainedGlassRGBMults[ModContent.WallType<Walls.CyanStainedGlass>()] = [0f, 1f, 1f];
			//if (ExxoAvalonOrigins.ThoriumContentEnabled)
			//{
			//	StainedGlassRGBMults[ModContent.WallType<ModSupport.Thorium.Walls.ChartreuseStainedGlass>()] = [0.745f, 0.925f, 0.1f];
			//}
		}

		private void On_TileLightScanner_ApplyHellLight(On_TileLightScanner.orig_ApplyHellLight orig, TileLightScanner self, Tile tile, int x, int y, ref Vector3 lightColor)
		{
			orig.Invoke(self, tile, x, y, ref lightColor);

			if (tile.WallType != WallID.None && (!tile.HasTile || tile.IsHalfBlock || !Main.tileNoSunLight[tile.TileType]) && tile.LiquidAmount < 255)
			{
				if (StainedGlassRGBMults[tile.WallType] != null)
				{
					float num4 = 0.55f + MathF.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.08f;
					float finalR = num4 * StainedGlassRGBMults[tile.WallType]![0];
					float finalG = num4 * 0.6f * StainedGlassRGBMults[tile.WallType]![1];
					float finalB = num4 * 0.2f * StainedGlassRGBMults[tile.WallType]![2];

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

		private void On_TileLightScanner_ApplySurfaceLight(On_TileLightScanner.orig_ApplySurfaceLight orig, TileLightScanner self, Tile tile, int x, int y, ref Vector3 lightColor)
		{
			orig.Invoke(self, tile, x, y, ref lightColor);

			if (tile.WallType != WallID.None && (!tile.HasTile || tile.IsHalfBlock || !Main.tileNoSunLight[tile.TileType]) && tile.LiquidAmount < 255)
			{
				if (StainedGlassRGBMults[tile.WallType] != null)
				{
					float finalR = Main.tileColor.R / 255f * StainedGlassRGBMults[tile.WallType]![0];
					float finalG = Main.tileColor.G / 255f * StainedGlassRGBMults[tile.WallType]![1];
					float finalB = Main.tileColor.B / 255f * StainedGlassRGBMults[tile.WallType]![2];

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
}
