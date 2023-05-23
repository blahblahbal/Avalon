using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class BronzePlatingWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallLargeFrames[Type] = 1;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.BronzePlatingWall>();
        AddMapEntry(new Color(61, 29, 26));
        DustType = ModContent.DustType<Dusts.BronzeDust>();
    }
}
