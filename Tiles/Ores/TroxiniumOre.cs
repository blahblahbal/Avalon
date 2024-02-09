using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class TroxiniumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 4f;
        AddMapEntry(Color.Goldenrod, this.GetLocalization("MapEntry"));
        Data.Sets.Tile.RiftOres[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileOreFinderPriority[Type] = 660;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 875;
        HitSound = SoundID.Tink;
        MinPick = 150;
        DustType = ModContent.DustType<TroxiniumDust>();
        TileID.Sets.Ore[Type] = true;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
