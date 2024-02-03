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
            //On_WorldGen.PlaceTile += On_WorldGen_PlaceTile;
        }

        private bool On_WorldGen_PlaceTile(On_WorldGen.orig_PlaceTile orig, int i, int j, int Type, bool mute, bool forced, int plr, int style)
        {
            if (Data.Sets.Tile.AvalonPlanterBoxes[Main.tile[i, j + 1].TileType] &&
                (Main.tile[i, j].TileType == TileID.ImmatureHerbs || Main.tile[i, j].TileType == TileID.MatureHerbs ||
                Main.tile[i, j].TileType == TileID.BloomingHerbs || Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Barfbush>() ||
                Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Bloodberry>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Holybird>() ||
                Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Sweetstem>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.TwilightPlume>()))
            {
                return false;
            }
            return orig.Invoke(i, j, Type, mute, forced, plr, style);
        }

        private bool On_WorldGen_CanCutTile(On_WorldGen.orig_CanCutTile orig, int x, int y, TileCuttingContext context)
        {
            if (Data.Sets.Tile.AvalonPlanterBoxes[Main.tile[x, y + 1].TileType])
            {
                return false;
            }
            return orig(x, y, context);
        }
        
        private static bool OnPlaceAlch(On_WorldGen.orig_PlaceAlch orig, int x, int y, int style)
        {
            if (Data.Sets.Tile.AvalonPlanterBoxes[Main.tile[x, y + 1].TileType])
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
            if (!Data.Sets.Tile.AvalonPlanterBoxes[Main.tile[x, y + 1].TileType])
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
