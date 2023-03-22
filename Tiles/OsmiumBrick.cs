using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class OsmiumBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(0, 148, 255));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.OsmiumBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.OsmiumDust>();
    }
}
