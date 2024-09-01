using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class BronzeOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(121, 50, 42), this.GetLocalization("MapEntry"));
        Data.Sets.Tile.RiftOres[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1100;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 215;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Savanna.Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Savanna.Loam>()][Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.BronzeDust>();
        TileID.Sets.Ore[Type] = true;
    }
}
