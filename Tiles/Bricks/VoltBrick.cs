using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Bricks;

public class VoltBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        HitSound = SoundID.Tink;
        DustType = DustID.VilePowder;
    }
}
