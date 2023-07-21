using Avalon.Common;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Hooks
{
    internal class PlanterBoxHooks : ModHook
    {
        protected override void Apply()
        {
            On_WorldGen.PlaceAlch += OnPlaceAlch;
            On_WorldGen.CheckAlch += OnCheckAlch;
            On_WorldGen.CanCutTile += On_WorldGen_CanCutTile;
        }
        private bool On_WorldGen_CanCutTile(On_WorldGen.orig_CanCutTile orig, int x, int y, TileCuttingContext context)
        {
            if (Main.tile[x, y + 1].TileType == ModContent.TileType<Tiles.PlanterBoxes>())
            {
                return false;
            }
            return orig(x, y, context);
        }
        private static bool OnPlaceAlch(On_WorldGen.orig_PlaceAlch orig, int x, int y, int style)
        {
            if (Main.tile[x, y + 1].TileType == ModContent.TileType<Tiles.PlanterBoxes>())
            {
                Main.tile[x, y].Active(true);
                Main.tile[x, y].TileType = TileID.ImmatureHerbs;
                Main.tile[x, y].TileFrameX = (short)(18 * style);
                Main.tile[x, y].TileFrameY = 0;
                return true;
            }
            return orig(x, y, style);
        }
        private static void OnCheckAlch(On_WorldGen.orig_CheckAlch orig, int x, int y)
        {
            bool flag = false;
            if (!Main.tile[x, y + 1].HasUnactuatedTile)
            {
                flag = true;
            }
            if (Main.tile[x, y + 1].IsHalfBlock)
            {
                flag = true;
            }
            if (Main.tile[x, y + 1].TileType != ModContent.TileType<Tiles.PlanterBoxes>())
            {
                flag = true;
            }
            int style = Main.tile[x, y].TileFrameX / 18;
            Main.tile[x, y].TileFrameY = 0;

            if (flag)
            {
                orig(x, y);
            }
        }
    }
}
