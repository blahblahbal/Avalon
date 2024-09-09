using Avalon.Common.Players;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common
{
    internal class DarkMatterWorld : ModSystem
    {
        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            float DarkMatterStrength = 0; // Main.LocalPlayer.GetModPlayer<AvalonPlayer>().DarkMatterMonolith && MonolithTilesInRange(Main.LocalPlayer) ? ModContent.GetInstance<BiomeTileCounts>().DarkMonolithTiles / 1 : 0;
            if (MonolithTilesInRange(Main.LocalPlayer))
            {
				DarkMatterStrength = 1f;
			}
			if (DarkMatterStrength != 0)
			{
				DarkMatterStrength = Math.Min(DarkMatterStrength, 1f);

				int sunR = backgroundColor.R;
				int sunG = backgroundColor.G;
				int sunB = backgroundColor.B;
				//byte readableSunR = 129;
				//byte readableSunG = 184;
				//byte readableSunB = 148;
				//sunR -= (int)((byte.MaxValue - readableSunR) * DarkMatterStrength / 2f * (backgroundColor.R / 255f));
				//sunG -= (int)((byte.MaxValue - readableSunG) * DarkMatterStrength / 2f * (backgroundColor.G / 255f));
				//sunB -= (int)((byte.MaxValue - readableSunB) * DarkMatterStrength / 2f * (backgroundColor.B / 255f));
				byte readableSunR = 15;
				byte readableSunG = 15;
				byte readableSunB = 15;
				sunR -= (int)((byte.MaxValue - readableSunR) * DarkMatterStrength);
				sunG -= (int)((byte.MaxValue - readableSunG) * DarkMatterStrength);
				sunB -= (int)((byte.MaxValue - readableSunB) * DarkMatterStrength);
				sunR = Utils.Clamp(sunR, 15, 255);
				sunG = Utils.Clamp(sunG, 15, 255);
				sunB = Utils.Clamp(sunB, 15, 255);
				backgroundColor.R = (byte)sunR;
				backgroundColor.G = (byte)sunG;
				backgroundColor.B = (byte)sunB;

				int backgroundColorAverage = (int)((backgroundColor.R + backgroundColor.G + backgroundColor.B) / 3f);
				byte readableTint_R = 129;
				byte readableTint_G = 184;
				byte readableTint_B = 148;
				int tileTint_R = (byte)((byte.MaxValue - readableTint_R) * DarkMatterStrength * (backgroundColorAverage / 255f));
				int tileTint_G = (byte)((byte.MaxValue - readableTint_G) * DarkMatterStrength * (backgroundColorAverage / 255f));
				int tileTint_B = (byte)((byte.MaxValue - readableTint_B) * DarkMatterStrength * (backgroundColorAverage / 255f));
				tileTint_R = (int)(tileTint_R - (DarkMatterStrength * 7f));
				tileTint_G = (int)(tileTint_G - (DarkMatterStrength * 7f));
				tileTint_B = (int)(tileTint_B - (DarkMatterStrength * 7f));

				tileColor.R = (byte)Math.Clamp(tileColor.R <= tileTint_R ? 1 : tileColor.R - tileTint_R, DarkMatterStrength * 20f, Math.Clamp(255 - (DarkMatterStrength * 255), 20, 255));
				tileColor.G = (byte)Math.Clamp(tileColor.G <= tileTint_G ? 1 : tileColor.G - tileTint_G, DarkMatterStrength * 20f, Math.Clamp(255 - (DarkMatterStrength * 255), 20, 255));
				tileColor.B = (byte)Math.Clamp(tileColor.B <= tileTint_B ? 1 : tileColor.B - tileTint_B, DarkMatterStrength * 20f, Math.Clamp(255 - (DarkMatterStrength * 255), 20, 255));
			}
		}
        public override void ModifyLightingBrightness(ref float scale)
        {
            if (MonolithTilesInRange(Main.LocalPlayer))
            {
                scale = 0.8f;
			}
        }
        public static bool MonolithTilesInRange(Player p)
        {
            for (int i = (int)(p.position.X - 88.375f * 16) >> 4; i < (int)(p.position.X + 90.625f * 16) >> 4; i++)
            {
                for (int j = (int)(p.position.Y - 60.625f * 16) >> 4; j < (int)(p.position.Y + 64.375f * 16) >> 4; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    if (t.TileType == ModContent.TileType<Tiles.DarkMatter.DarkMatterMonolith>() && t.TileFrameX >= 36)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
