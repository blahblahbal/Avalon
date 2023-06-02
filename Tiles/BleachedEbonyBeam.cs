using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BleachedEbonyBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(235, 166, 135));
        //ItemDrop = ModContent.ItemType<Items.Placeable.Beam.BleachedEbonyBeam>();
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.SnowBlock;
    }
}
