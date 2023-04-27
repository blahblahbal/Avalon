using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class OrangeTiledWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.OrangeTiledWall>();
        AddMapEntry(new Color(63, 36, 24));
        DustType = ModContent.DustType<Dusts.OrangeDungeonDust>();
    }
}
