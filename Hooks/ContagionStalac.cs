using Avalon.Common;
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
            //On_WorldGen.GetDesiredStalagtiteStyle += On_WorldGen_GetDesiredStalactgmiteStyle;
        }

        private void On_WorldGen_GetDesiredStalactgmiteStyle(On_WorldGen.orig_GetDesiredStalagtiteStyle orig, int x, int j, out bool fail, out int desiredStyle, out int height, out int y)
        {
            fail = false;
            desiredStyle = 0;
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
            if (desiredStyle == ModContent.TileType<Tiles.Contagion.Chunkstone>())
            {
                desiredStyle = 0;
            }
            orig(x, j, out fail, out desiredStyle, out height, out y);

        }
    }
}
