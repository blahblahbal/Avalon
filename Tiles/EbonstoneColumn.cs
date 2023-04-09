using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class EbonstoneColumn : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(85, 62, 94));
        //Main.tileBeam[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Beam.EbonstoneColumn>();
        HitSound = SoundID.Tink;
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.CorruptionThorns;
    }
}
