using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class CoughwoodFence : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.CoughwoodFence>();
        AddMapEntry(new Color(106, 116, 90));
        DustType = ModContent.DustType<Dusts.CoughwoodDust>();
    }
}
