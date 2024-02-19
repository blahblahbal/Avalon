using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Tropics;

public class TuhrtlBackgroundBlobs : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(99, 89, 85));
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = false;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.addTile(Type);
        DustType = DustID.Silt;
    }
}
