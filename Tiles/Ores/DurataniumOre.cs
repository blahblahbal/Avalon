using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class DurataniumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 2f;
        AddMapEntry(new Color(107, 20, 80), this.GetLocalization("MapEntry"));
        Data.Sets.TileSets.RiftOres[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileOreFinderPriority[Type] = 615;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 925;
        HitSound = SoundID.Tink;
        MinPick = 100;
        DustType = ModContent.DustType<DurataniumDust>();
        TileID.Sets.Ore[Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Savanna.Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Savanna.Loam>()][Type] = true;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
