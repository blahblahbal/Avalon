using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class ResistantWoodBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(235, 166, 135));
        ItemDrop = ModContent.ItemType<Items.Placeable.Beam.ResistantWoodBeam>();
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.Wraith;
    }
}
