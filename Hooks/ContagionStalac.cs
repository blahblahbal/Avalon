using Avalon.Common;
using Avalon.Tiles.Contagion;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class ContagionStalac : ModHook
    {
        protected override void Apply()
        {
            //IL_WorldGen.GetDesiredStalagtiteStyle += IL_WorldGen_GetDesiredStalagtiteStyle;
            On_WorldGen.GetDesiredStalagtiteStyle += On_WorldGen_GetDesiredStalactgmiteStyle;
            IL_WorldGen.PlaceTight += IL_WorldGen_PlaceTight;
            On_WorldGen.IsFitToPlaceFlowerIn += On_WorldGen_IsFitToPlaceFlowerIn;
        }

        private bool On_WorldGen_IsFitToPlaceFlowerIn(On_WorldGen.orig_IsFitToPlaceFlowerIn orig, int x, int y, int typeAttemptedToPlace)
        {
            if (y < 1 || y > Main.maxTilesY - 1)
            {
                return false;
            }
            Tile tile = Main.tile[x, y + 1];
            if (tile.HasTile && tile.Slope == Terraria.ID.SlopeType.Solid && !tile.IsHalfBlock)
            {
                if (((tile.TileType != 2 && tile.TileType != 78 && tile.TileType != 380 && tile.TileType != 477 && tile.TileType != 579) ||
                    typeAttemptedToPlace != 3) && ((tile.TileType != 23 && tile.TileType != 661) ||
                    typeAttemptedToPlace != 24) && ((tile.TileType != 109 && tile.TileType != 492) ||
                    typeAttemptedToPlace != 110) && ((tile.TileType != 199 && tile.TileType != 662) ||
                    typeAttemptedToPlace != 201) &&
                    ((tile.TileType != ModContent.TileType<Ickgrass>() && tile.TileType != ModContent.TileType<ContagionJungleGrass>()) || typeAttemptedToPlace != ModContent.TileType<ContagionShortGrass>()))
                {
                    if (tile.TileType == 633)
                    {
                        return typeAttemptedToPlace == 637;
                    }
                    return false;
                }
                return true;
            }
            return false;
        }

        private void IL_WorldGen_PlaceTight(ILContext il)
        {
            Utilities.AddAlternativeIdChecks(il, 165, id => Data.Sets.Tile.Stalac[id]);
        }

        //private void IL_WorldGen_GetDesiredStalagtiteStyle(ILContext il)
        //{
        //    var c = new ILCursor(il);

        //    c.GotoNext(i => i.MatchLdcI4(368));

        //    c.Index++;

        //    c.EmitDelegate<Func<int, int>>((w) =>
        //    {
        //        if (w == ModContent.TileType<Chunkstone>())
        //        {
        //            w = 0;
        //        }
        //    });
        //}

        private void On_WorldGen_GetDesiredStalactgmiteStyle(On_WorldGen.orig_GetDesiredStalagtiteStyle orig, int x, int j, out bool fail, out int desiredStyle, out int height, out int y)
        {
            fail = false;
            height = 1;
            y = j;
            if (Main.tile[x, y].TileFrameY == 72)
            {
                desiredStyle = Main.tile[x, y - 1].TileType;
            }
            else if (Main.tile[x, y].TileFrameY == 90)
            {
                desiredStyle = Main.tile[x, y + 1].TileType;
            }
            else if (Main.tile[x, y].TileFrameY >= 36)
            {
                if (Main.tile[x, y].TileFrameY == 54)
                {
                    y--;
                }
                height = 2;
                desiredStyle = Main.tile[x, y + 2].TileType;
            }
            else
            {
                if (Main.tile[x, y].TileFrameY == 18)
                {
                    y--;
                }
                height = 2;
                desiredStyle = Main.tile[x, y - 1].TileType;
            }
            if (desiredStyle == 1 || Main.tileMoss[desiredStyle])
            {
                desiredStyle = 0;
            }
            else if (desiredStyle == ModContent.TileType<Chunkstone>())
            {
                desiredStyle = 0;
                Main.tile[x, y].TileType = (ushort)ModContent.TileType<ContagionStalactgmites>();
                Main.tile[x, y].TileFrameX -= 54;
                if (height == 2)
                {
                    if (Main.tile[x, y - 1].TileType == 165)
                    {
                        Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<ContagionStalactgmites>();
                        Main.tile[x, y - 1].TileFrameX -= 54;
                    }
                    else if (Main.tile[x, y + 1].TileType == 165)
                    {
                        Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<ContagionStalactgmites>();
                        Main.tile[x, y + 1].TileFrameX -= 54;
                    }
                }
            }
            else if (desiredStyle == 200)
            {
                desiredStyle = 10;
            }
            else if (desiredStyle == 164)
            {
                desiredStyle = 8;
            }
            else if (desiredStyle == 163)
            {
                desiredStyle = 9;
            }
            else if (desiredStyle == 117 || desiredStyle == 402 || desiredStyle == 403)
            {
                desiredStyle = 1;
            }
            else if (desiredStyle == 25 || desiredStyle == 398 || desiredStyle == 400)
            {
                desiredStyle = 2;
            }
            else if (desiredStyle == 203 || desiredStyle == 399 || desiredStyle == 401)
            {
                desiredStyle = 3;
            }
            else if (desiredStyle == 396 || desiredStyle == 397)
            {
                desiredStyle = 4;
            }
            else if (desiredStyle == 367)
            {
                desiredStyle = 6;
            }
            else if (desiredStyle == 368)
            {
                desiredStyle = 5;
            }
            else if (desiredStyle == 161 || desiredStyle == 147)
            {
                desiredStyle = 7;
            }
            else
            {
                fail = true;
            }
        }
    }
}
