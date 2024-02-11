using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BleachedEbonyBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(175, 175, 175));
        //ItemDrop = ModContent.ItemType<Items.Placeable.Beam.BleachedEbonyBeam>();
        TileID.Sets.IsBeam[Type] = true;
        DustType = ModContent.DustType<BleachedEbonyDust>();
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        height = 18;
    }
}
