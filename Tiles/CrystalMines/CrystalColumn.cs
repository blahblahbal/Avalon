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
}
