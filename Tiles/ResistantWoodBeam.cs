using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class ResistantWoodBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(41, 41, 41));
        //ItemDrop = ModContent.ItemType<Items.Placeable.Beam.ResistantWoodBeam>();
        TileID.Sets.IsBeam[Type] = true;
        DustType = ModContent.DustType<ResistantWoodDust>();
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        height = 18;
    }
}
