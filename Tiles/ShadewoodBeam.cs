using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class ShadewoodBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(41, 47, 50));
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.Shadewood;
    }
}
