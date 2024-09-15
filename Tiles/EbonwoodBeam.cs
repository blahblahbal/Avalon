using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class EbonwoodBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(70, 67, 87));
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.Ebonwood;
    }
}
