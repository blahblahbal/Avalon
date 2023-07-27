using Avalon.Common;
using Avalon.Tiles.Contagion;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                desiredStyle = 7;
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
