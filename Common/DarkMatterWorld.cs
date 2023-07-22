using Avalon.Common.Players;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common
{
    internal class DarkMatterWorld : ModSystem
    {
        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            float DarkMatterStrength = Main.LocalPlayer.GetModPlayer<AvalonPlayer>().DarkMatterMonolith && MonolithTilesInRange(Main.LocalPlayer) ? ModContent.GetInstance<BiomeTileCounts>().DarkMonolithTiles / 1 : 0;
            if (MonolithTilesInRange(Main.LocalPlayer))
            {
                DarkMatterStrength = 1f;
            }
            DarkMatterStrength = Math.Min(DarkMatterStrength, 1f);
            int sunR = backgroundColor.R;
            int sunG = backgroundColor.G;
            int sunB = backgroundColor.B;
            sunR -= (int)(126f * DarkMatterStrength / 2 * (backgroundColor.R / 255f));
            sunB -= (int)(71f * DarkMatterStrength / 2 * (backgroundColor.B / 255f));
            sunG -= (int)(107f * DarkMatterStrength / 2 * (backgroundColor.G / 255f));
            sunR = Utils.Clamp(sunR, 15, 255);
            sunG = Utils.Clamp(sunG, 15, 255);
            sunB = Utils.Clamp(sunB, 15, 255);
            backgroundColor.R = (byte)sunR;
            backgroundColor.G = (byte)sunG;
            backgroundColor.B = (byte)sunB;
            tileColor.R -= (byte)(126f * DarkMatterStrength / 2.2f * (backgroundColor.R / 255f) * 1.6f);
            tileColor.G -= (byte)(71f * DarkMatterStrength / 2.4f * (backgroundColor.G / 255f) * 1.5f);
            tileColor.B -= (byte)(107f * DarkMatterStrength / 1.8f * (backgroundColor.B / 255f) * 1.8f);
        }
        public static bool MonolithTilesInRange(Player p)
        {
            Point point = p.position.ToTileCoordinates();

            for (int i = point.X - 200; i < point.X + 200; i++)
            {
                for (int j = point.Y - 200; j < point.Y + 200; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    if (t.TileType == ModContent.TileType<Tiles.DarkMatter.DarkMatterMonolith>())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
