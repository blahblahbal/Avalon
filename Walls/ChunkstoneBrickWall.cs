using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ChunkstoneBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ChunkstoneBrickWall>();
        AddMapEntry(new Color(67, 83, 61));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
