using Avalon.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Map;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class PrimordialOreMapColor : ModHook
    {
        protected override void Apply()
        {
            //On_MapHelper.GetMapTileXnaColor += OnGetMapTileXnaColor;
            On_Main.DoUpdate_AnimateDiscoRGB += OnDoUpdate_AnimateDiscoRGB;
        }
        private static Color OnGetMapTileXnaColor(On_MapHelper.orig_GetMapTileXnaColor orig, ref MapTile tile)
        {
            Color[] ColorLookup = (Color[])typeof(MapHelper).GetField("colorLookup", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            Color oldColor = ColorLookup[tile.Type];

            if (oldColor == new Color(0, 1, 2, 127) || oldColor == ExxoAvalonOrigins.LastDiscoRGB)
            {
                tile.Light = 0;
                return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            }

            return orig(ref tile);
        }
        private static void OnDoUpdate_AnimateDiscoRGB(On_Main.orig_DoUpdate_AnimateDiscoRGB orig, Main self)
        {
            ExxoAvalonOrigins.LastDiscoRGB = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            orig(self);
        }
    }
}
