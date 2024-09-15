using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class CoughwoodBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(57, 73, 47));
        TileID.Sets.IsBeam[Type] = true;
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
