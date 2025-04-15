using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class NickelOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(82, 112, 122), this.GetLocalization("MapEntry"));
        Data.Sets.TileSets.RiftOres[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1050;
        Main.tileOreFinderPriority[Type] = 235;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Savanna.Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Savanna.Loam>()][Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.NickelDust>();
        TileID.Sets.Ore[Type] = true;
    }
}
