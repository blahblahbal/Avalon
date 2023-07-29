using Avalon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Avalon.Tiles.Ores;
using Terraria.ID;

namespace Avalon.Hooks
{
    internal class BacciliteReplacement : ModHook
    {
        protected override void Apply()
        {
            On_WorldGen.TileRunner += On_WorldGen_TileRunner;
        }

        private void On_WorldGen_TileRunner(On_WorldGen.orig_TileRunner orig, int i, int j, double strength, int steps, int type, bool addTile, double speedX, double speedY, bool noYChange, bool overRide, int ignoreTileType)
        {
            if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion && type == TileID.Demonite)
            {
                type = ModContent.TileType<BacciliteOre>();
            }
            orig(i, j, strength, steps, type, addTile, speedX, speedY, noYChange, overRide, ignoreTileType);
        }
    }
}
