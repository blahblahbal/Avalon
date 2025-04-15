using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class NaquadahOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 3f;
        AddMapEntry(Color.Blue, this.GetLocalization("MapEntry"));
        Data.Sets.TileSets.RiftOres[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileOreFinderPriority[Type] = 635;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 900;
        HitSound = SoundID.Tink;
        MinPick = 110;
        DustType = ModContent.DustType<NaquadahDust>();
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
