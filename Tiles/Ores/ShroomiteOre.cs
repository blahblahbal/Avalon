using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class ShroomiteOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 5f;
        AddMapEntry(Color.CornflowerBlue, this.GetLocalization("MapEntry"));
        Main.tileSolid[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 705;
        Main.tileBlockLight[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1400;
        HitSound = SoundID.Tink;
        MinPick = 205;
        DustType = DustID.Clentaminator_Blue;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.OreMergesWithMud[Type] = true;
        TileID.Sets.Ore[Type] = true;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
