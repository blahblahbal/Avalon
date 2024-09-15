using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class PearlwoodBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(75, 68, 49));
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.Pearlwood;
    }
}
