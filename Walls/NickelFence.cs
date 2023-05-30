using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class NickelFence : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.NickelFence>();
        AddMapEntry(new Color(52, 78, 85));
        DustType = ModContent.DustType<Dusts.NickelDust>();
        Main.wallLight[Type] = true;
    }
}
