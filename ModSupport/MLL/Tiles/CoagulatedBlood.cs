using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Tiles;

public class CoagulatedBlood : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(127, 0, 0));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        DustType = DustID.Blood;
    }
}
