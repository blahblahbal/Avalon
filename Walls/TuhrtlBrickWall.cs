using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class TuhrtlBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(39, 31, 28));
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.TuhrtlBrickWall>();
        DustType = DustID.Silt;
    }
}
