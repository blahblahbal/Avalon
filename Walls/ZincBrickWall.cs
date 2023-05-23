using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ZincBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ZincBrickWall>();
        AddMapEntry(new Color(76, 65, 75));
        DustType = ModContent.DustType<Dusts.ZincDust>();
    }
}
