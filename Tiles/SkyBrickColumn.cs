using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class SkyBrickColumn : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(181, 172, 190));
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.Smoke;
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        height = 18;
    }
}
