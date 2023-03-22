using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class RottenChunk : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(83, 65, 67));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ItemID.RottenChunk;
        DustType = DustID.Bone;
    }
}
