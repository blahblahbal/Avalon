using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class NickelBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.NickelBrickWall>();
        AddMapEntry(new Color(52, 78, 85));
        DustType = ModContent.DustType<Dusts.NickelDust>();
    }
}
