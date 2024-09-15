using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class PalmWoodBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(85, 62, 27));
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.PalmWood;
    }
}
