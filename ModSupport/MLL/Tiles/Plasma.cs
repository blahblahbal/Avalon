using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Tiles;

public class Plasma : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(241, 167, 220));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        DustType = DustID.ShimmerSpark;
    }
}
