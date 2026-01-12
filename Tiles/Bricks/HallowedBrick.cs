using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Bricks;

public class HallowedBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(219, 183, 0));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileLighted[Type] = true;
        HitSound = SoundID.Tink;
        DustType = DustID.Enchanted_Gold;
    }
}
