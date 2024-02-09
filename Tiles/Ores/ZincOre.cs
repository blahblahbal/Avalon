using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class ZincOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(168, 155, 168), this.GetLocalization("MapEntry"));
        Data.Sets.Tile.RiftOres[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1025;
        Main.tileOreFinderPriority[Type] = 255;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.ZincDust>();
        TileID.Sets.Ore[Type] = true;
    }
}
