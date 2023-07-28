using Avalon.Tiles;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Data.Sets
{
    internal class Tile
    {
        public static List<ushort> LivingBlocks = new()
        {
            TileID.LivingFire, TileID.LivingCursedFire, TileID.LivingDemonFire, TileID.LivingFrostFire, TileID.LivingIchor,
            TileID.LivingUltrabrightFire, (ushort)ModContent.TileType<LivingLightning>()
        };
    }
}
