using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Tiles;

public class BoiledBlood : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(67, 0, 0));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        DustType = DustID.Blood;
    }
}
