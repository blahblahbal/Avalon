using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class SepsisBlockWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.SepsisBlockWall>();
        AddMapEntry(new Color(75, 79, 46));
        DustType = DustID.BrownMoss;
    }
}
