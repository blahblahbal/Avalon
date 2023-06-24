using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.CrystalMines;

public class CrystalColumn : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(154, 149, 247));
        HitSound = SoundID.Tink;
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.PinkCrystalShard;
        MinPick = 400;
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        height = 18;
    }
}
