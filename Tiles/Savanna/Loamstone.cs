using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Savanna;

public class Loamstone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(95, 38, 12));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[Type][TileID.Stone] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Loam>()][Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.TropicalMudDust>();
    }
}
