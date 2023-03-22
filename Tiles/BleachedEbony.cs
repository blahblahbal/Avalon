using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BleachedEbony : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(200, 200, 200));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BleachedEbony>();
        DustType = DustID.SnowBlock;
    }
}
